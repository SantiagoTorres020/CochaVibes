using CochaVibes.Core.Entities;

namespace CochaVibes.Core.Interfaces
{
    public interface IEventoRepository : IBaseRepository<Evento>
    {
        Task<IEnumerable<Evento>> BuscarEventosAsync(
            string? texto,
            int? idCategoria,
            DateTime? fecha,
            int? idUbicacion);

        Task<Evento?> GetEventoDetalleByIdAsync(int id);
    }
}