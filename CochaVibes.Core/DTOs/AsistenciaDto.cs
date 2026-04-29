namespace CochaVibes.Core.DTOs
{
    public class AsistenciaDto
    {
        public int IdUsuario { get; set; }

        public int IdEvento { get; set; }

        public string Estado { get; set; } = null!;
    }
}