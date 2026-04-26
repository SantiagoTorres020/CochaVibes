using CochaVibes.Core.DTOs;
using CochaVibes.Core.Entities;

namespace CochaVibes.Services.Interfaces
{
    public interface IEventoService
    {
        Task<IEnumerable<Evento>> BuscarEventosAsync(EventoBusquedaDto filtro);

        Task<IEnumerable<Evento>> BuscarEventosDapperAsync(int limit = 10);

        Task<Evento?> GetEventoDetalleByIdAsync(int id);

        Task InsertEvento(Evento evento);

        void UpdateEvento(Evento evento);

        Task DeleteEvento(int id);
    }
}