using CochaVibes.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CochaVibes.Infrastructure.Data
{
    public partial class CochaVibesContext : DbContext
    {
        public CochaVibesContext()
        {
        }

        public CochaVibesContext(DbContextOptions<CochaVibesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }

        public virtual DbSet<Categoria> Categorias { get; set; }

        public virtual DbSet<Ubicacion> Ubicaciones { get; set; }

        public virtual DbSet<Evento> Eventos { get; set; }

        public virtual DbSet<Comentario> Comentarios { get; set; }

        public virtual DbSet<Favorito> Favoritos { get; set; }

        public virtual DbSet<Asistencia> Asistencias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}