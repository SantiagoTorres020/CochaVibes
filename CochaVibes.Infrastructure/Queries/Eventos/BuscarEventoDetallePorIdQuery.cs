namespace CochaVibes.Infrastructure.Queries.Eventos
{
    public static class BuscarEventoDetallePorIdQuery
    {
        public static string MySql = @"
        select
            e.id_evento as IdEvento,
            e.titulo as Titulo,
            e.descripcion as Descripcion,
            date_format(e.fecha, '%Y-%m-%d') as Fecha,
            e.hora_inicio as HoraInicio,
            e.hora_fin as HoraFin,
            e.precio as Precio,
            e.estado as Estado,
            c.nombre as Categoria,
            u.nombre as Organizador,
            ub.nombre as Ubicacion,
            ub.direccion as Direccion
        from evento e
        inner join categoria c on e.id_categoria = c.id_categoria
        inner join usuario u on e.id_usuario = u.id_usuario
        inner join ubicacion ub on e.id_ubicacion = ub.id_ubicacion
        where e.id_evento = @IdEvento
        and e.estado = 'activo';";
    }
}