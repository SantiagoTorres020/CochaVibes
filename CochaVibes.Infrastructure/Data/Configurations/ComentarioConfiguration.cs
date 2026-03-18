using CochaVibes.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CochaVibes.Infrastructure.Data.Configurations
{
    public class ComentarioConfiguration : IEntityTypeConfiguration<Comentario>
    {
        public void Configure(EntityTypeBuilder<Comentario> entity)
        {
            entity.HasKey(e => e.IdComentario).HasName("PRIMARY");

            entity.ToTable("comentario");

            entity.HasIndex(e => e.IdUsuario, "FK_Comentario_Usuario");
            entity.HasIndex(e => e.IdEvento, "FK_Comentario_Evento");

            entity.Property(e => e.IdComentario).HasColumnName("id_comentario");
            entity.Property(e => e.Contenido).HasColumnName("contenido").HasMaxLength(500);
            entity.Property(e => e.Fecha).HasColumnName("fecha").HasColumnType("datetime");
            entity.Property(e => e.Estado).HasColumnName("estado").HasMaxLength(50);
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");

            entity.HasOne(d => d.Usuario)
                .WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comentario_Usuario");

            entity.HasOne(d => d.Evento)
                .WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.IdEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comentario_Evento");
        }
    }
}