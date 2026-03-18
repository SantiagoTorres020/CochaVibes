using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CochaVibes.Core.Entities
{
    public partial class Evento
    {
        public int IdEvento { get; set; }

        public string Titulo { get; set; } = null!;

        public string? Descripcion { get; set; }

        public DateTime Fecha { get; set; }

        public TimeSpan HoraInicio { get; set; }

        public TimeSpan HoraFin { get; set; }

        public decimal Precio { get; set; }

        public string Estado { get; set; } = null!;

        public int IdCategoria { get; set; }

        public int IdUsuario { get; set; }

        public int IdUbicacion { get; set; }

        public virtual Categoria? Categoria { get; set; }

        public virtual Usuario? Usuario { get; set; }

        public virtual Ubicacion? Ubicacion { get; set; }

        public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

        public virtual ICollection<Favorito> Favoritos { get; set; } = new List<Favorito>();

        public virtual ICollection<Asistencia> Asistencias { get; set; } = new List<Asistencia>();
    }
}
