using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CochaVibes.Core.Entities
{
    public partial class Categoria
    {
        public int IdCategoria { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();
    }
}