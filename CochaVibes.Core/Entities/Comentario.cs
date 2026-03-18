using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CochaVibes.Core.Entities
{
    public partial class Comentario
    {
        public int IdComentario { get; set; }

        public string Contenido { get; set; } = null!;

        public DateTime Fecha { get; set; }

        public string Estado { get; set; } = null!;

        public int IdUsuario { get; set; }

        public int IdEvento { get; set; }

        public virtual Usuario? Usuario { get; set; }

        public virtual Evento? Evento { get; set; }
    }
}