namespace CochaVibes.Core.QueryFilters
{
    public class ComentarioQueryFilter
    {
        public int? IdEvento { get; set; }

        public int? IdUsuario { get; set; }

        public string? Estado { get; set; }

        public string? Fecha { get; set; }

        public string? Texto { get; set; }
    }
}