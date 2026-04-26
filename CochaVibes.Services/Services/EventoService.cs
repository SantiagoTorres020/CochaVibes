using CochaVibes.Core.DTOs;
using CochaVibes.Core.Entities;
using CochaVibes.Core.Interfaces;
using CochaVibes.Services.Interfaces;

namespace CochaVibes.Services.Services
{
    public class EventoService : IEventoService
    {
        private readonly IBaseRepository<Evento> _eventoRepository;

        public EventoService(IBaseRepository<Evento> eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        public async Task<IEnumerable<Evento>> BuscarEventosAsync(EventoBusquedaDto filtro)
        {
            DateTime? fecha = null;

            if (!string.IsNullOrWhiteSpace(filtro.Fecha))
            {
                fecha = Convert.ToDateTime(filtro.Fecha);
            }

            var eventos = await _eventoRepository.GetAll(
                e => e.Categoria,
                e => e.Ubicacion,
                e => e.Usuario);

            var query = eventos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro.Texto))
            {
                query = query.Where(e => e.Titulo.Contains(filtro.Texto));
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

        public async Task<Evento?> GetEventoDetalleByIdAsync(int id)
        {
            var evento = await _eventoRepository.GetById(
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
            await _eventoRepository.Add(evento);
        }

        public async Task UpdateEvento(Evento evento)
        {
            await _eventoRepository.Update(evento);
        }

        public async Task DeleteEvento(int id)
        {
            await _eventoRepository.Delete(id);
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