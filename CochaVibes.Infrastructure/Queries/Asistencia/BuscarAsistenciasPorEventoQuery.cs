namespace CochaVibes.Infrastructure.Queries.Asistencias
{
    public static class BuscarAsistenciasPorEventoQuery
    {
        public static string MySql = @"
        select
            a.id_usuario as IdUsuario,
            u.nombre as Usuario,
            a.id_evento as IdEvento,
            e.titulo as Evento,
            a.estado as Estado
        from asistencia a
        inner join usuario u on a.id_usuario = u.id_usuario
        inner join evento e on a.id_evento = e.id_evento
        where a.id_evento = @IdEvento
        order by u.nombre asc;";
    }
}