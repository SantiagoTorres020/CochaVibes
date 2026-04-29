using CochaVibes.Core.DTOs;
using CochaVibes.Core.Entities;
using CochaVibes.Core.Enum;
using CochaVibes.Core.Exceptions;
using CochaVibes.Core.Helpers;
using CochaVibes.Core.Interfaces;
using CochaVibes.Infrastructure.Queries;
using CochaVibes.Services.Interfaces;
using CochaVibes.Infrastructure.Queries.Eventos;
using System.Net;

namespace CochaVibes.Services.Services
{
    public class EventoService : IEventoService
    {
        public readonly IUnitOfWork _unitOfWork;

        public EventoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Evento>> BuscarEventosAsync(EventoBusquedaDto filtro)
        {
            DateTime? fecha = null;

            if (!string.IsNullOrWhiteSpace(filtro.Fecha))
            {
                string? fechaAux = Procesos.ParseFechaFlexible(filtro.Fecha);

                if (fechaAux != null)
                {
                    fecha = Convert.ToDateTime(fechaAux);
                }
            }

            var eventos = await _unitOfWork.EventoRepository.GetAll(
                e => e.Categoria,
                e => e.Ubicacion,
                e => e.Usuario);

            var query = eventos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro.Texto))
            {
                query = query.Where(e => e.Titulo.ToLower().Contains(filtro.Texto.ToLower()));
            }

            if (filtro.IdCategoria.HasValue)
            {
                query = query.Where(e => e.IdCategoria == filtro.IdCategoria.Value);
            }

            if (fecha.HasValue)
            {
                query = query.Where(e => e.Fecha.Date == fecha.Value.Date);
            }

            if (filtro.IdUbicacion.HasValue)
            {
                query = query.Where(e => e.IdUbicacion == filtro.IdUbicacion.Value);
            }

            return query
                .Where(e => EsVisibleAlPublico(e.Estado))
                .OrderBy(e => e.Fecha)
                .ThenBy(e => e.HoraInicio);
        }

        public async Task<IEnumerable<Evento>> BuscarEventosDapperAsync(int limit = 10)
        {
            return await _unitOfWork.DapperContext.QueryAsync<Evento>(
                BuscarEventosActivosQuery.MySql,
                new { Limit = limit });
        }

        public async Task<EventoDetalleDto?> GetEventoDetalleDapperByIdAsync(int id)
        {
            return await _unitOfWork.DapperContext.QueryFirstOrDefaultAsync<EventoDetalleDto>(
                BuscarEventoDetallePorIdQuery.MySql,
                new { IdEvento = id });
        }

        public async Task<int> GetCantidadEventosActivosAsync()
        {
            return await _unitOfWork.DapperContext.ExecuteScalarAsync<int>(
                ContarEventosActivosQuery.MySql);
        }

        public async Task<Evento?> GetEventoDetalleByIdAsync(int id)
        {
            var evento = await _unitOfWork.EventoRepository.GetById(
                id,
                e => e.Categoria,
                e => e.Ubicacion,
                e => e.Usuario);

            if (evento == null)
                return null;

            if (!EsVisibleAlPublico(evento.Estado))
                return null;

            return evento;
        }

        public async Task InsertEvento(Evento evento)
        {
            var categoria = await _unitOfWork.CategoriaRepository.GetById(evento.IdCategoria);

            if (categoria == null)
                throw new BussinesException("La categoría no existe.", HttpStatusCode.BadRequest);

            var ubicacion = await _unitOfWork.UbicacionRepository.GetById(evento.IdUbicacion);

            if (ubicacion == null)
                throw new BussinesException("La ubicación no existe.", HttpStatusCode.BadRequest);

            var usuario = await _unitOfWork.UsuarioRepository.GetById(evento.IdUsuario);

            if (usuario == null)
                throw new BussinesException("El usuario no existe.", HttpStatusCode.BadRequest);

            await _unitOfWork.EventoRepository.Add(evento);
            await _unitOfWork.SaveChangesAsync();
        }

        public void UpdateEvento(Evento evento)
        {
            _unitOfWork.EventoRepository.Update(evento);
            _unitOfWork.SaveChanges();
        }

        public async Task DeleteEvento(int id)
        {
            await _unitOfWork.EventoRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }

        private bool EsVisibleAlPublico(string? estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                return false;

            var estadosVisibles = new[] { "activo" };

            return estadosVisibles.Contains(estado.Trim(), StringComparer.OrdinalIgnoreCase);
        }
    }
}