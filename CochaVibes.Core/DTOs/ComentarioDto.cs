namespace CochaVibes.Core.DTOs
{
    public class ComentarioDto
    {
        public int IdComentario { get; set; }

        public string Contenido { get; set; } = null!;

        public string? Fecha { get; set; }

        public string Estado { get; set; } = null!;

        public int IdUsuario { get; set; }

        public int IdEvento { get; set; }
    }
}