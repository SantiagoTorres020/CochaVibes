using CochaVibes.Core.Entities;
using CochaVibes.Core.Interfaces;
using CochaVibes.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CochaVibes.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly CochaVibesContext _context;

        protected readonly DbSet<T> _entities;

        public BaseRepository(CochaVibesContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _entities;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<T?> GetById(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _entities;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var keyName = GetPrimaryKeyName();

            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, keyName) == id);
        }

        public async Task Add(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }

        public async Task Delete(int id)
        {
            T? entity = await GetById(id);

            if (entity == null)
                throw new Exception("Registro no encontrado");

            _entities.Remove(entity);
        }

        private string GetPrimaryKeyName()
        {
            var entityType = _context.Model.FindEntityType(typeof(T));

            if (entityType == null)
                throw new Exception("No se encontró la entidad en el modelo.");

            var primaryKey = entityType.FindPrimaryKey();

            if (primaryKey == null)
                throw new Exception("No se encontró la clave primaria de la entidad.");

            var property = primaryKey.Properties.FirstOrDefault();

            if (property == null)
                throw new Exception("No se encontró la propiedad de clave primaria.");

            return property.Name;
        }
    }
}