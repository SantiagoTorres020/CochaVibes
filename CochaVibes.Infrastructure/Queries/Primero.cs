namespace CochaVibes.Infrastructure.Queries
{
    public static class Primero
    {
        public static string unoMySql = @"
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

        public static string unoSql = @"
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
        OFFSET 0 ROWS FETCH NEXT @Limit ROWS ONLY;";
    }
}