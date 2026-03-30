namespace CochaVibes.Core.DTOs
{
    public class EventoDetalleDto
    {
        public int IdEvento { get; set; }

        public string Titulo { get; set; } = null!;

        public string? Descripcion { get; set; }

        public string Fecha { get; set; } = null!;

        public string HoraInicio { get; set; } = null!;

        public string HoraFin { get; set; } = null!;

        public decimal Precio { get; set; }

        public string Estado { get; set; } = null!;

        public string Categoria { get; set; } = null!;

        public string Ubicacion { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public string Organizador { get; set; } = null!;
    }
}   