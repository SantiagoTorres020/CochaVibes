namespace CochaVibes.Infrastructure.Queries.Eventos
{
    public static class ContarEventosActivosQuery
    {
        public static string MySql = @"
        select count(*)
        from evento
        where estado = 'activo';";
    }
}