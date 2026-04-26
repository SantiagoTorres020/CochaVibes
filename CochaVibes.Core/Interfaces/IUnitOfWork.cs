using CochaVibes.Core.Entities;
using System.Data;

namespace CochaVibes.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Evento> EventoRepository { get; }

        IBaseRepository<Usuario> UsuarioRepository { get; }

        IBaseRepository<Categoria> CategoriaRepository { get; }

        IBaseRepository<Ubicacion> UbicacionRepository { get; }

        IBaseRepository<Comentario> ComentarioRepository { get; }

        IDapperContext DapperContext { get; }

        void SaveChanges();

        Task SaveChangesAsync();

        Task BeginTransactionAsync();

        Task CommitAsync();

        Task RollbackAsync();

        IDbConnection? GetDbConnection();

        IDbTransaction? GetDbTransaction();
    }
}