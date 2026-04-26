using CochaVibes.Api.Filters;
using CochaVibes.Core.DTOs;
using CochaVibes.Core.Interfaces;
using CochaVibes.Infrastructure.Data;
using CochaVibes.Infrastructure.Mappings;
using CochaVibes.Infrastructure.Repositories;
using CochaVibes.Services.Interfaces;
using CochaVibes.Services.Services;
using CochaVibes.Services.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CochaVibes.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("ConnectionMySql");

            builder.Services.AddDbContext<CochaVibesContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            builder.Services.AddTransient<IEventoService, EventoService>();

            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
            builder.Services.AddScoped<IDapperContext, DapperContext>();

            builder.Services.AddTransient<IValidator<EventoBusquedaDto>, EventoBusquedaDtoValidator>();
            builder.Services.AddTransient<IValidator<EventoIdDto>, EventoIdDtoValidator>();
            builder.Services.AddScoped<CrearEventoDtoValidator>();
            builder.Services.AddScoped<ActualizarEventoDtoValidator>();

            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });

            builder.Services.AddAutoMapper(typeof(EventoProfile).Assembly);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}