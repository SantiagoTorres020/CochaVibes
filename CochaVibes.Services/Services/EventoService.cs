using CochaVibes.Core.DTOs;
using CochaVibes.Core.Entities;
using CochaVibes.Core.Interfaces;
using CochaVibes.Services.Interfaces;

namespace CochaVibes.Services.Services
{
    public class EventoService : IEventoService
    {
        private readonly IEventoRepository _eventoRepository;

        public EventoService(IEventoRepository eventoRepository)
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

            var eventos = await _eventoRepository.BuscarEventosAsync(
                filtro.Texto,
                filtro.IdCategoria,
                fecha,
                filtro.IdUbicacion);

            return eventos
                .Where(e => EsVisibleAlPublico(e.Estado))
                .OrderBy(e => e.Fecha)
                .ThenBy(e => e.HoraInicio);
        }

        public async Task<Evento?> GetEventoDetalleByIdAsync(int id)
        {
            var evento = await _eventoRepository.GetEventoDetalleByIdAsync(id);

            if (evento == null)
                return null;

            if (!EsVisibleAlPublico(evento.Estado))
                return null;

            return evento;
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