using CochaVibes.Core.Entities;
using CochaVibes.Core.Interfaces;
using CochaVibes.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CochaVibes.Infrastructure.Repositories
{
    public class EventoRepository : IEventoRepository
    {
        private readonly CochaVibesContext _context;

        public EventoRepository(CochaVibesContext context)
        {
            _context = context;
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

        public async Task<IEnumerable<Evento>> GetAllEventosAsync()
        {
            var eventos = await _context.Eventos.ToListAsync();
            return eventos;
        }

        public async Task<Evento?> GetEventoByIdAsync(int id)
        {
            var evento = await _context.Eventos.FirstOrDefaultAsync(x => x.IdEvento == id);
            return evento;
        }

        public async Task InsertEvento(Evento evento)
        {
            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEvento(Evento evento)
        {
            _context.Eventos.Update(evento);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEvento(Evento evento)
        {
            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();
        }
    }
}