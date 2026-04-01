using CochaVibes.Core.Entities;
using CochaVibes.Core.Interfaces;
using CochaVibes.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CochaVibes.Infrastructure.Repositories
{
    public class EventoRepository : BaseRepository<Evento>, IEventoRepository
    {
        public EventoRepository(CochaVibesContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Evento>> BuscarEventosAsync(
            string? texto,
            int? idCategoria,
            DateTime? fecha,
            int? idUbicacion)
        {
            var query = _context.Eventos
                .Include(e => e.Categoria)
                .Include(e => e.Ubicacion)
                .Include(e => e.Usuario)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(texto))
            {
                query = query.Where(e => e.Titulo.Contains(texto));
            }

            if (idCategoria.HasValue)
            {
                query = query.Where(e => e.IdCategoria == idCategoria.Value);
            }

            if (fecha.HasValue)
            {
                query = query.Where(e => e.Fecha.Date == fecha.Value.Date);
            }

            if (idUbicacion.HasValue)
            {
                query = query.Where(e => e.IdUbicacion == idUbicacion.Value);
            }

            return await query
                .OrderBy(e => e.Fecha)
                .ThenBy(e => e.HoraInicio)
                .ToListAsync();
        }

        public async Task<Evento?> GetEventoDetalleByIdAsync(int id)
        {
            return await _context.Eventos
                .Include(e => e.Categoria)
                .Include(e => e.Ubicacion)
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(e => e.IdEvento == id);
        }
    }
}