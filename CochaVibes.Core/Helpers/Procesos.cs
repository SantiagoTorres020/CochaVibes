using System.Globalization;

namespace CochaVibes.Core.Helpers
{
    public class Procesos
    {
        public static string? ParseFechaFlexible(string? fechaTexto)
        {
            if (fechaTexto is null)
                return null;

            fechaTexto = fechaTexto.Replace(" ", " ").Trim();

            string[] formatos = new[]
            {
                "yyyy-MM-dd",
                "dd-MM-yyyy",
                "dd/MM/yyyy",
                "d-M-yyyy",
                "d/M/yyyy",
                "yyyy-MM-dd HH:mm:ss",
                "dd-MM-yyyy HH:mm:ss",
                "dd/MM/yyyy HH:mm:ss"
            };

            var cultura = new CultureInfo("es-BO");

            foreach (var formato in formatos)
            {
                if (DateTime.TryParseExact(fechaTexto, formato, cultura, DateTimeStyles.None, out DateTime resultado))
                {
                    return resultado.ToString("yyyy-MM-dd");
                }
            }

            if (DateTime.TryParse(fechaTexto, out DateTime fecha))
            {
                return fecha.ToString("yyyy-MM-dd");
            }

            return null;
        }
    }
}