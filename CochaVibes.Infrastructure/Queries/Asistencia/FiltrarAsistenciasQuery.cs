namespace CochaVibes.Infrastructure.Queries.Asistencias
{
    public static class FiltrarAsistenciasQuery
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
        where (@IdEvento is null or a.id_evento = @IdEvento)
        and (@IdUsuario is null or a.id_usuario = @IdUsuario)
        and (@Estado is null or a.estado = @Estado)
        order by e.titulo asc, u.nombre asc;";
    }
}