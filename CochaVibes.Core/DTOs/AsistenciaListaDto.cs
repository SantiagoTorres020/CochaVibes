namespace CochaVibes.Core.DTOs
{
    public class AsistenciaListaDto
    {
        public int IdUsuario { get; set; }

        public string Usuario { get; set; } = null!;

        public int IdEvento { get; set; }

        public string Evento { get; set; } = null!;

        public string Estado { get; set; } = null!;
    }
}