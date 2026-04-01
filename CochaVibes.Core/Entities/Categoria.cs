namespace CochaVibes.Core.Entities
{
    public partial class Categoria : BaseEntity
    {
        public int IdCategoria { get; set; }

        public override int Id => IdCategoria;

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();
    }
}