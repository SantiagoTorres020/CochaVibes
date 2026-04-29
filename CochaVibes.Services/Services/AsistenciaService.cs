using CochaVibes.Core.DTOs;
using CochaVibes.Core.Exceptions;
using CochaVibes.Core.Interfaces;
using CochaVibes.Infrastructure.Queries.Asistencias;
using CochaVibes.Services.Interfaces;
using System.Net;

namespace CochaVibes.Services.Services
{
    public class AsistenciaService : IAsistenciaService
    {
        public readonly IUnitOfWork _unitOfWork;

        public AsistenciaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AsistenciaListaDto>> GetAsistenciasByEventoAsync(int idEvento)
        {
            var evento = await _unitOfWork.EventoRepository.GetById(idEvento);

            if (evento == null)
                throw new BussinesException("El evento no existe.", HttpStatusCode.NotFound);

            return await _unitOfWork.DapperContext.QueryAsync<AsistenciaListaDto>(
                BuscarAsistenciasPorEventoQuery.MySql,
                new { IdEvento = idEvento });
        }

        public async Task<int> GetCantidadAsistenciasByEventoAsync(int idEvento)
        {
            var evento = await _unitOfWork.EventoRepository.GetById(idEvento);

            if (evento == null)
                throw new BussinesException("El evento no existe.", HttpStatusCode.NotFound);

            return await _unitOfWork.DapperContext.ExecuteScalarAsync<int>(
                ContarAsistenciasPorEventoQuery.MySql,
                new { IdEvento = idEvento });
        }

        public async Task<AsistenciaDto?> GetAsistenciaAsync(int idUsuario, int idEvento)
        {
            return await _unitOfWork.DapperContext.QueryFirstOrDefaultAsync<AsistenciaDto>(
                GestionarAsistenciaQuery.BuscarPorUsuarioYEventoMySql,
                new
                {
                    IdUsuario = idUsuario,
                    IdEvento = idEvento
                });
        }

        public async Task InsertAsistencia(AsistenciaDto asistenciaDto)
        {
            var usuario = await _unitOfWork.UsuarioRepository.GetById(asistenciaDto.IdUsuario);

            if (usuario == null)
                throw new BussinesException("El usuario no existe.", HttpStatusCode.BadRequest);

            var evento = await _unitOfWork.EventoRepository.GetById(asistenciaDto.IdEvento);

            if (evento == null)
                throw new BussinesException("El evento no existe.", HttpStatusCode.BadRequest);

            if (!EsVisibleAlPublico(evento.Estado))
                throw new BussinesException("No se puede registrar asistencia a un evento que no está activo.", HttpStatusCode.BadRequest);

            var asistenciaExistente = await GetAsistenciaAsync(
                asistenciaDto.IdUsuario,
                asistenciaDto.IdEvento);

            if (asistenciaExistente != null)
                throw new BussinesException("El usuario ya registró asistencia para este evento.", HttpStatusCode.BadRequest);

            await _unitOfWork.DapperContext.ExecuteAsync(
                GestionarAsistenciaQuery.InsertarMySql,
                asistenciaDto);
        }

        public async Task UpdateAsistencia(AsistenciaDto asistenciaDto)
        {
            var usuario = await _unitOfWork.UsuarioRepository.GetById(asistenciaDto.IdUsuario);

            if (usuario == null)
                throw new BussinesException("El usuario no existe.", HttpStatusCode.BadRequest);

            var evento = await _unitOfWork.EventoRepository.GetById(asistenciaDto.IdEvento);

            if (evento == null)
                throw new BussinesException("El evento no existe.", HttpStatusCode.BadRequest);

            var asistenciaExistente = await GetAsistenciaAsync(
                asistenciaDto.IdUsuario,
                asistenciaDto.IdEvento);

            if (asistenciaExistente == null)
                throw new BussinesException("La asistencia no existe.", HttpStatusCode.NotFound);

            await _unitOfWork.DapperContext.ExecuteAsync(
                GestionarAsistenciaQuery.ActualizarMySql,
                asistenciaDto);
        }

        public async Task DeleteAsistencia(int idUsuario, int idEvento)
        {
            var asistenciaExistente = await GetAsistenciaAsync(idUsuario, idEvento);

            if (asistenciaExistente == null)
                throw new BussinesException("La asistencia no existe.", HttpStatusCode.NotFound);

            await _unitOfWork.DapperContext.ExecuteAsync(
                GestionarAsistenciaQuery.EliminarMySql,
                new
                {
                    IdUsuario = idUsuario,
                    IdEvento = idEvento
                });
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