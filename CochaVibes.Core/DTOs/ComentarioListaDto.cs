namespace CochaVibes.Core.DTOs
{
    public class ComentarioListaDto
    {
        public int IdComentario { get; set; }

        public string Contenido { get; set; } = null!;

        public string Fecha { get; set; } = null!;

        public string Estado { get; set; } = null!;

        public string Usuario { get; set; } = null!;

        public string Evento { get; set; } = null!;
    }
}