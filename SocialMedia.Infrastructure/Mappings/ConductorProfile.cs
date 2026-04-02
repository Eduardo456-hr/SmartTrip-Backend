using AutoMapper;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;

namespace SocialMedia.Infrastructure.Mappings
{
    public class ConductorProfile : Profile
    {
        public ConductorProfile()
        {

            CreateMap<Conductore, ConductorDto>()
                .ForMember(dest => dest.Correo, opt => opt.MapFrom(src => src.Usuario.Correo))
                .ForMember(dest => dest.Contrasena, opt => opt.MapFrom(src => src.Usuario.Contrasena));

            CreateMap<ConductorDto, Conductore>()
                .ForMember(dest => dest.UsuarioId, opt => opt.MapFrom(src => src.Id))
                .ForPath(dest => dest.Usuario.Correo, opt => opt.MapFrom(src => src.Correo))
                .ForPath(dest => dest.Usuario.Contrasena, opt => opt.MapFrom(src => src.Contrasena))
                .ForPath(dest => dest.Usuario.Rol, opt => opt.MapFrom(src => "Conductor"));
        }
    }
}