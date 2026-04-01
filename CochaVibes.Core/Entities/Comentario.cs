namespace CochaVibes.Core.Entities
{
    public partial class Comentario : BaseEntity
    {
        public int IdComentario { get; set; }

        public override int Id => IdComentario;

        public string Contenido { get; set; } = null!;

        public DateTime Fecha { get; set; }

        public string Estado { get; set; } = null!;

        public int IdUsuario { get; set; }

        public int IdEvento { get; set; }

        public virtual Usuario? Usuario { get; set; }

        public virtual Evento? Evento { get; set; }
    }
}