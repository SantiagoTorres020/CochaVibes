namespace CochaVibes.Infrastructure.Queries.Comentarios
{
    public static class ContarComentariosPorEventoQuery
    {
        public static string MySql = @"
        select count(*)
        from comentario
        where id_evento = @IdEvento;";
    }
}