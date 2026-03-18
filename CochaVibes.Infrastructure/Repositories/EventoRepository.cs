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