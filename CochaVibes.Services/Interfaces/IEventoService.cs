using CochaVibes.Core.DTOs;
using CochaVibes.Core.Entities;

namespace CochaVibes.Services.Interfaces
{
    public interface IEventoService
    {
        Task<IEnumerable<Evento>> BuscarEventosAsync(EventoBusquedaDto filtro);
        Task<Evento?> GetEventoDetalleByIdAsync(int id);
        Task InsertEvento(Evento evento);
        Task UpdateEvento(Evento evento);
        Task DeleteEvento(int id);
    }
}