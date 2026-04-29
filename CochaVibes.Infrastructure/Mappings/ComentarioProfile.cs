using AutoMapper;
using CochaVibes.Core.DTOs;
using CochaVibes.Core.Entities;

namespace CochaVibes.Infrastructure.Mappings
{
    public class ComentarioProfile : Profile
    {
        public ComentarioProfile()
        {
            CreateMap<Comentario, ComentarioDto>()
                .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => src.Fecha.ToString("yyyy-MM-dd HH:mm:ss")));

            CreateMap<ComentarioDto, Comentario>()
                .ForMember(dest => dest.IdComentario, opt => opt.Ignore())
                .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src =>
                    string.IsNullOrWhiteSpace(src.Fecha)
                        ? DateTime.Now
                        : Convert.ToDateTime(src.Fecha)));

            CreateMap<Comentario, ComentarioListaDto>()
                .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => src.Fecha.ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.Usuario != null ? src.Usuario.Nombre : "Sin usuario"))
                .ForMember(dest => dest.Evento, opt => opt.MapFrom(src => src.Evento != null ? src.Evento.Titulo : "Sin evento"));
        }
    }
}