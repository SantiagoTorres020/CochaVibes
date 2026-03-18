using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CochaVibes.Core.Entities
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }

        public string Nombre { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Contrasena { get; set; } = null!;

        public string Rol { get; set; } = null!;

        public virtual ICollection<Evento> EventosOrganizados { get; set; } = new List<Evento>();

        public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

        public virtual ICollection<Favorito> Favoritos { get; set; } = new List<Favorito>();

        public virtual ICollection<Asistencia> Asistencias { get; set; } = new List<Asistencia>();
    }
}
