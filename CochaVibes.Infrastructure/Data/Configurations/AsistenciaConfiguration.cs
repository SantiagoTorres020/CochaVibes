using CochaVibes.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CochaVibes.Infrastructure.Data.Configurations
{
    public class AsistenciaConfiguration : IEntityTypeConfiguration<Asistencia>
    {
        public void Configure(EntityTypeBuilder<Asistencia> entity)
        {
            entity.HasKey(e => new { e.IdUsuario, e.IdEvento }).HasName("PRIMARY");

            entity.ToTable("asistencia");

            entity.HasIndex(e => e.IdEvento, "FK_Asistencia_Evento");

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.Estado).HasColumnName("estado").HasMaxLength(50);

            entity.HasOne(d => d.Usuario)
                .WithMany(p => p.Asistencias)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asistencia_Usuario");

            entity.HasOne(d => d.Evento)
                .WithMany(p => p.Asistencias)
                .HasForeignKey(d => d.IdEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asistencia_Evento");
        }
    }
}