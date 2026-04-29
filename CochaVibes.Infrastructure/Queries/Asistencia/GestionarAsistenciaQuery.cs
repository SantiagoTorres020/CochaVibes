namespace CochaVibes.Infrastructure.Queries.Asistencias
{
    public static class GestionarAsistenciaQuery
    {
        public static string BuscarPorUsuarioYEventoMySql = @"
        select
            id_usuario as IdUsuario,
            id_evento as IdEvento,
            estado as Estado
        from asistencia
        where id_usuario = @IdUsuario
        and id_evento = @IdEvento;";

        public static string InsertarMySql = @"
        insert into asistencia(id_usuario, id_evento, estado)
        values(@IdUsuario, @IdEvento, @Estado);";

        public static string ActualizarMySql = @"
        update asistencia
        set estado = @Estado
        where id_usuario = @IdUsuario
        and id_evento = @IdEvento;";

        public static string EliminarMySql = @"
        delete from asistencia
        where id_usuario = @IdUsuario
        and id_evento = @IdEvento;";
    }
}