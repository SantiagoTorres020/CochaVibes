using CochaVibes.Core.DTOs;
using CochaVibes.Core.Entities;

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
    }
}