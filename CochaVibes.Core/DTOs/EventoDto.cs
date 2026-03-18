using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CochaVibes.Core.DTOs
{
    public class EventoDto
    {
        public int IdEvento { get; set; }

        public string Titulo { get; set; } = null!;

        public string? Descripcion { get; set; }

        public string Fecha { get; set; } = null!;

        public string HoraInicio { get; set; } = null!;

        public string HoraFin { get; set; } = null!;

        public decimal Precio { get; set; }

        public string Estado { get; set; } = null!;

        public int IdCategoria { get; set; }

        public int IdUsuario { get; set; }

        public int IdUbicacion { get; set; }
    }
}
