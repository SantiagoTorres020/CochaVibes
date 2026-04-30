namespace CochaVibes.Infrastructure.Queries.Comentarios
{
    public static class FiltrarComentariosQuery
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
        where (@IdEvento is null or c.id_evento = @IdEvento)
        and (@IdUsuario is null or c.id_usuario = @IdUsuario)
        and (@Estado is null or c.estado = @Estado)
        and (@Fecha is null or date(c.fecha) = date(@Fecha))
        and (@Texto is null or c.contenido like concat('%', @Texto, '%'))
        order by c.fecha desc;";
    }
}