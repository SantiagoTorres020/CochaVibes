namespace CochaVibes.Infrastructure.Queries.Asistencias
{
    public static class ContarAsistenciasPorEventoQuery
    {
        public static string MySql = @"
        select count(*)
        from asistencia
        where id_evento = @IdEvento;";
    }
}