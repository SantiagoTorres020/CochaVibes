using AutoMapper;
using CochaVibes.Core.DTOs;
using CochaVibes.Core.Entities;

namespace CochaVibes.Infrastructure.Mappings
{
    public class EventoProfile : Profile
    {
        public EventoProfile()
        {
            CreateMap<Evento, EventoDto>()
                .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => src.Fecha.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.HoraInicio, opt => opt.MapFrom(src => src.HoraInicio.ToString()))
                .ForMember(dest => dest.HoraFin, opt => opt.MapFrom(src => src.HoraFin.ToString()));

            CreateMap<EventoDto, Evento>()
                .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => Convert.ToDateTime(src.Fecha)))
                .ForMember(dest => dest.HoraInicio, opt => opt.MapFrom(src => TimeSpan.Parse(src.HoraInicio)))
                .ForMember(dest => dest.HoraFin, opt => opt.MapFrom(src => TimeSpan.Parse(src.HoraFin)));

            CreateMap<Evento, EventoListaDto>()
                .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => src.Fecha.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.HoraInicio, opt => opt.MapFrom(src => src.HoraInicio.ToString()))
                .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria != null ? src.Categoria.Nombre : "Sin categoría"))
                .ForMember(dest => dest.Ubicacion, opt => opt.MapFrom(src => src.Ubicacion != null ? src.Ubicacion.NombreLugar : "Sin ubicación"));

            CreateMap<Evento, EventoDetalleDto>()
                .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => src.Fecha.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.HoraInicio, opt => opt.MapFrom(src => src.HoraInicio.ToString()))
                .ForMember(dest => dest.HoraFin, opt => opt.MapFrom(src => src.HoraFin.ToString()))
                .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria != null ? src.Categoria.Nombre : "Sin categoría"))
                .ForMember(dest => dest.Ubicacion, opt => opt.MapFrom(src => src.Ubicacion != null ? src.Ubicacion.NombreLugar : "Sin ubicación"))
                .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Ubicacion != null ? src.Ubicacion.Direccion : "Sin dirección"))
                .ForMember(dest => dest.Organizador, opt => opt.MapFrom(src => src.Usuario != null ? src.Usuario.Nombre : "Sin organizador"));
        }
    }
}