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
        }
    }
}