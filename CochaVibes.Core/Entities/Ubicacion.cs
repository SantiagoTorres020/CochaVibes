using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CochaVibes.Core.Entities
{
    public partial class Ubicacion
    {
        public int IdUbicacion { get; set; }

        public string NombreLugar { get; set; } = null!;

        public string Zona { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();
    }
}