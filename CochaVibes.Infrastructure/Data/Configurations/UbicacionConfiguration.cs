using CochaVibes.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CochaVibes.Infrastructure.Data.Configurations
{
    public class UbicacionConfiguration : IEntityTypeConfiguration<Ubicacion>
    {
        public void Configure(EntityTypeBuilder<Ubicacion> entity)
        {
            entity.HasKey(e => e.IdUbicacion).HasName("PRIMARY");

            entity.ToTable("ubicacion");

            entity.Property(e => e.IdUbicacion).HasColumnName("id_ubicacion");
            entity.Property(e => e.NombreLugar).HasColumnName("nombre").HasMaxLength(100);
            entity.Property(e => e.Zona).HasColumnName("zona").HasMaxLength(100);
            entity.Property(e => e.Direccion).HasColumnName("direccion").HasMaxLength(200);
        }
    }
}