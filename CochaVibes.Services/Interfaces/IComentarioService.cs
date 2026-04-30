using CochaVibes.Core.DTOs;
using CochaVibes.Core.Entities;
using CochaVibes.Core.QueryFilters;

namespace CochaVibes.Services.Interfaces
{
    public interface IComentarioService
    {
        Task<IEnumerable<ComentarioListaDto>> GetComentariosByEventoAsync(int idEvento);

        Task<int> GetCantidadComentariosByEventoAsync(int idEvento);

        Task<ComentarioListaDto?> GetComentarioDetalleByIdAsync(int idComentario);

        Task<Comentario?> GetComentarioByIdAsync(int idComentario);

        Task InsertComentario(Comentario comentario);

        void UpdateComentario(Comentario comentario);

        Task DeleteComentario(int idComentario);
        Task<IEnumerable<ComentarioListaDto>> GetComentariosFiltradosAsync(ComentarioQueryFilter filtro);
    }
}