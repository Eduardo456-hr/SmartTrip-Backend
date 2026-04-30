using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;

namespace SocialMedia.Infrastructure.Mappings
{
    public class ViajeProfile : Profile
    {
        public ViajeProfile()
        {
            CreateMap<CrearViajeDto, Viaje>();
            CreateMap<Viaje, ViajeDto>().ReverseMap();
        }
    }
}