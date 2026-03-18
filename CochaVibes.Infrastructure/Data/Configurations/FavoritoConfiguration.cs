using CochaVibes.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CochaVibes.Infrastructure.Data.Configurations
{
    public class FavoritoConfiguration : IEntityTypeConfiguration<Favorito>
    {
        public void Configure(EntityTypeBuilder<Favorito> entity)
        {
            entity.HasKey(e => new { e.IdUsuario, e.IdEvento }).HasName("PRIMARY");

            entity.ToTable("favorito");

            entity.HasIndex(e => e.IdEvento, "FK_Favorito_Evento");

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.FechaGuardado).HasColumnName("fecha_guardado").HasColumnType("datetime");

            entity.HasOne(d => d.Usuario)
                .WithMany(p => p.Favoritos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Favorito_Usuario");

            entity.HasOne(d => d.Evento)
                .WithMany(p => p.Favoritos)
                .HasForeignKey(d => d.IdEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Favorito_Evento");
        }
    }
}