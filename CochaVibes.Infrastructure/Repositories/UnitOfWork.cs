using System.Data;
using CochaVibes.Core.Entities;
using CochaVibes.Core.Interfaces;
using CochaVibes.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CochaVibes.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CochaVibesContext _context;

        private readonly IDapperContext _dapper;

        private IDbContextTransaction? _efTransaction;

        private IBaseRepository<Evento>? _eventoRepository;

        private IBaseRepository<Usuario>? _usuarioRepository;

        private IBaseRepository<Categoria>? _categoriaRepository;

        private IBaseRepository<Ubicacion>? _ubicacionRepository;

        private IBaseRepository<Comentario>? _comentarioRepository;

        public UnitOfWork(CochaVibesContext context, IDapperContext dapper)
        {
            _context = context;
            _dapper = dapper;
        }

        public IBaseRepository<Evento> EventoRepository =>
            _eventoRepository ??= new BaseRepository<Evento>(_context);

        public IBaseRepository<Usuario> UsuarioRepository =>
            _usuarioRepository ??= new BaseRepository<Usuario>(_context);

        public IBaseRepository<Categoria> CategoriaRepository =>
            _categoriaRepository ??= new BaseRepository<Categoria>(_context);

        public IBaseRepository<Ubicacion> UbicacionRepository =>
            _ubicacionRepository ??= new BaseRepository<Ubicacion>(_context);

        public IBaseRepository<Comentario> ComentarioRepository =>
            _comentarioRepository ??= new BaseRepository<Comentario>(_context);

        public IDapperContext DapperContext => _dapper;

        public async Task BeginTransactionAsync()
        {
            if (_efTransaction == null)
            {
                _efTransaction = await _context.Database.BeginTransactionAsync();

                var conn = _context.Database.GetDbConnection();
                var tx = _efTransaction.GetDbTransaction();

                _dapper.SetAmbientConnection(conn, tx);
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();

                if (_efTransaction != null)
                {
                    await _efTransaction.CommitAsync();
                    _efTransaction.Dispose();
                    _efTransaction = null;
                }
            }
            finally
            {
                _dapper.ClearAmbientConnection();
            }
        }

        public async Task RollbackAsync()
        {
            if (_efTransaction != null)
            {
                await _efTransaction.RollbackAsync();
                _efTransaction.Dispose();
                _efTransaction = null;
            }

            _dapper.ClearAmbientConnection();
        }

        public IDbConnection? GetDbConnection()
        {
            return _context.Database.GetDbConnection();
        }

        public IDbTransaction? GetDbTransaction()
        {
            return _efTransaction?.GetDbTransaction();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            _efTransaction?.Dispose();
        }
    }
}