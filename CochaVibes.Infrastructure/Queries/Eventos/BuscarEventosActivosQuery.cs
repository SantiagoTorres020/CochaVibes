namespace CochaVibes.Infrastructure.Queries.Eventos
{
    public static class BuscarEventosActivosQuery
    {
        public static string MySql = @"
        select 
            id_evento as IdEvento,
            titulo as Titulo,
            descripcion as Descripcion,
            fecha as Fecha,
            hora_inicio as HoraInicio,
            hora_fin as HoraFin,
            precio as Precio,
            estado as Estado,
            id_categoria as IdCategoria,
            id_usuario as IdUsuario,
            id_ubicacion as IdUbicacion
        from evento
        where estado = 'activo'
        order by fecha asc, hora_inicio asc
        limit @Limit;";
    }
}