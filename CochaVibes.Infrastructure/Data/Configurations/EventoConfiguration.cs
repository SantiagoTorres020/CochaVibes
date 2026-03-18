using CochaVibes.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CochaVibes.Infrastructure.Data.Configurations
{
    public class EventoConfiguration : IEntityTypeConfiguration<Evento>
    {
        public void Configure(EntityTypeBuilder<Evento> entity)
        {
            entity.HasKey(e => e.IdEvento).HasName("PRIMARY");

            entity.ToTable("evento");

            entity.HasIndex(e => e.IdCategoria, "FK_Evento_Categoria");
            entity.HasIndex(e => e.IdUsuario, "FK_Evento_Usuario");
            entity.HasIndex(e => e.IdUbicacion, "FK_Evento_Ubicacion");

            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.Titulo).HasColumnName("titulo").HasMaxLength(150);
            entity.Property(e => e.Descripcion).HasColumnName("descripcion").HasMaxLength(1000);
            entity.Property(e => e.Fecha).HasColumnName("fecha").HasColumnType("date");
            entity.Property(e => e.HoraInicio).HasColumnName("hora_inicio");
            entity.Property(e => e.HoraFin).HasColumnName("hora_fin");
            entity.Property(e => e.Precio).HasColumnName("precio").HasPrecision(10, 2);
            entity.Property(e => e.Estado).HasColumnName("estado").HasMaxLength(50);
            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.IdUbicacion).HasColumnName("id_ubicacion");

            entity.HasOne(d => d.Categoria)
                .WithMany(p => p.Eventos)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Evento_Categoria");

            entity.HasOne(d => d.Usuario)
                .WithMany(p => p.EventosOrganizados)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Evento_Usuario");

            entity.HasOne(d => d.Ubicacion)
                .WithMany(p => p.Eventos)
                .HasForeignKey(d => d.IdUbicacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Evento_Ubicacion");
        }
    }
}