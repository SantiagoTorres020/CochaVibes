using CochaVibes.Core.DTOs;
using CochaVibes.Core.Entities;
using CochaVibes.Core.Exceptions;
using CochaVibes.Core.Interfaces;
using CochaVibes.Infrastructure.Queries.Comentarios;
using CochaVibes.Services.Interfaces;
using CochaVibes.Core.Helpers;
using CochaVibes.Core.QueryFilters;
using System.Net;

namespace CochaVibes.Services.Services
{
    public class ComentarioService : IComentarioService
    {
        public readonly IUnitOfWork _unitOfWork;

        public ComentarioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ComentarioListaDto>> GetComentariosByEventoAsync(int idEvento)
        {
            var evento = await _unitOfWork.EventoRepository.GetById(idEvento);

            if (evento == null)
                throw new BussinesException("El evento no existe.", HttpStatusCode.NotFound);

            return await _unitOfWork.DapperContext.QueryAsync<ComentarioListaDto>(
                BuscarComentariosPorEventoQuery.MySql,
                new { IdEvento = idEvento });
        }

        public async Task<int> GetCantidadComentariosByEventoAsync(int idEvento)
        {
            var evento = await _unitOfWork.EventoRepository.GetById(idEvento);

            if (evento == null)
                throw new BussinesException("El evento no existe.", HttpStatusCode.NotFound);

            return await _unitOfWork.DapperContext.ExecuteScalarAsync<int>(
                ContarComentariosPorEventoQuery.MySql,
                new { IdEvento = idEvento });
        }

        public async Task<ComentarioListaDto?> GetComentarioDetalleByIdAsync(int idComentario)
        {
            return await _unitOfWork.DapperContext.QueryFirstOrDefaultAsync<ComentarioListaDto>(
                BuscarComentarioDetallePorIdQuery.MySql,
                new { IdComentario = idComentario });
        }

        public async Task<Comentario?> GetComentarioByIdAsync(int idComentario)
        {
            return await _unitOfWork.ComentarioRepository.GetById(
                idComentario,
                c => c.Usuario,
                c => c.Evento);
        }

        public async Task InsertComentario(Comentario comentario)
        {
            var usuario = await _unitOfWork.UsuarioRepository.GetById(comentario.IdUsuario);

            if (usuario == null)
                throw new BussinesException("El usuario no existe.", HttpStatusCode.BadRequest);

            var evento = await _unitOfWork.EventoRepository.GetById(comentario.IdEvento);

            if (evento == null)
                throw new BussinesException("El evento no existe.", HttpStatusCode.BadRequest);

            if (!EsVisibleAlPublico(evento.Estado))
                throw new BussinesException("No se puede comentar un evento que no está activo.", HttpStatusCode.BadRequest);

            if (string.IsNullOrWhiteSpace(comentario.Contenido))
                throw new BussinesException("El contenido del comentario es obligatorio.", HttpStatusCode.BadRequest);

            if (comentario.Fecha == default)
                comentario.Fecha = DateTime.Now;

            if (string.IsNullOrWhiteSpace(comentario.Estado))
                comentario.Estado = "visible";

            if (!EsEstadoComentarioValido(comentario.Estado))
                throw new BussinesException("El estado del comentario debe ser: visible, oculto o eliminado.", HttpStatusCode.BadRequest);

            await _unitOfWork.ComentarioRepository.Add(comentario);
            await _unitOfWork.SaveChangesAsync();
        }

        public void UpdateComentario(Comentario comentario)
        {
            if (string.IsNullOrWhiteSpace(comentario.Contenido))
                throw new BussinesException("El contenido del comentario es obligatorio.", HttpStatusCode.BadRequest);

            if (!EsEstadoComentarioValido(comentario.Estado))
                throw new BussinesException("El estado del comentario debe ser: visible, oculto o eliminado.", HttpStatusCode.BadRequest);

            _unitOfWork.ComentarioRepository.Update(comentario);
            _unitOfWork.SaveChanges();
        }

        public async Task DeleteComentario(int idComentario)
        {
            await _unitOfWork.ComentarioRepository.Delete(idComentario);
            await _unitOfWork.SaveChangesAsync();
        }

        private bool EsVisibleAlPublico(string? estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                return false;

            var estadosVisibles = new[] { "activo" };

            return estadosVisibles.Contains(estado.Trim(), StringComparer.OrdinalIgnoreCase);
        }

        private bool EsEstadoComentarioValido(string? estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                return false;

            var estadosValidos = new[] { "visible", "oculto", "eliminado" };

            return estadosValidos.Contains(estado.Trim(), StringComparer.OrdinalIgnoreCase);
        }
        public async Task<IEnumerable<ComentarioListaDto>> GetComentariosFiltradosAsync(ComentarioQueryFilter filtro)
        {
            string? fecha = null;

            if (!string.IsNullOrWhiteSpace(filtro.Fecha))
            {
                fecha = Procesos.ParseFechaFlexible(filtro.Fecha);
            }

            return await _unitOfWork.DapperContext.QueryAsync<ComentarioListaDto>(
                FiltrarComentariosQuery.MySql,
                new
                {
                    filtro.IdEvento,
                    filtro.IdUsuario,
                    Estado = string.IsNullOrWhiteSpace(filtro.Estado) ? null : filtro.Estado.Trim(),
                    Fecha = fecha,
                    Texto = string.IsNullOrWhiteSpace(filtro.Texto) ? null : filtro.Texto.Trim()
                });
        }
    }
}