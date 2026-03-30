using CochaVibes.Core.Entities;

namespace CochaVibes.Core.Interfaces
{
    public interface IEventoRepository
    {
        Task<IEnumerable<Evento>> BuscarEventosAsync(
            string? texto,
            int? idCategoria,
            DateTime? fecha,
            int? idUbicacion);

        Task<Evento?> GetEventoDetalleByIdAsync(int id);

        Task<IEnumerable<Evento>> GetAllEventosAsync();
        Task<Evento?> GetEventoByIdAsync(int id);
        Task InsertEvento(Evento evento);
        Task UpdateEvento(Evento evento);
        Task DeleteEvento(Evento evento);
    }
}