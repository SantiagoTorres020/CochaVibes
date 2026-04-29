namespace CochaVibes.Infrastructure.Queries.Comentarios
{
    public static class BuscarComentariosPorEventoQuery
    {
        public static string MySql = @"
        select
            c.id_comentario as IdComentario,
            c.contenido as Contenido,
            date_format(c.fecha, '%Y-%m-%d %H:%i:%s') as Fecha,
            c.estado as Estado,
            u.nombre as Usuario,
            e.titulo as Evento
        from comentario c
        inner join usuario u on c.id_usuario = u.id_usuario
        inner join evento e on c.id_evento = e.id_evento
        where c.id_evento = @IdEvento
        order by c.fecha desc;";
    }
}