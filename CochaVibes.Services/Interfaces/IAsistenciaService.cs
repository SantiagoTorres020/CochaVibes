using CochaVibes.Core.DTOs;
using CochaVibes.Core.QueryFilters;

namespace CochaVibes.Services.Interfaces
{
    public interface IAsistenciaService
    {
        Task<IEnumerable<AsistenciaListaDto>> GetAsistenciasByEventoAsync(int idEvento);

        Task<int> GetCantidadAsistenciasByEventoAsync(int idEvento);

        Task<AsistenciaDto?> GetAsistenciaAsync(int idUsuario, int idEvento);

        Task InsertAsistencia(AsistenciaDto asistenciaDto);

        Task UpdateAsistencia(AsistenciaDto asistenciaDto);

        Task DeleteAsistencia(int idUsuario, int idEvento);
        Task<IEnumerable<AsistenciaListaDto>> GetAsistenciasFiltradasAsync(AsistenciaQueryFilter filtro);
    }
}