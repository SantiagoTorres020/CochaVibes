namespace CochaVibes.Core.Entities
{
    public partial class Ubicacion : BaseEntity
    {
        public int IdUbicacion { get; set; }

        public override int Id => IdUbicacion;

        public string NombreLugar { get; set; } = null!;

        public string Zona { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();
    }
}