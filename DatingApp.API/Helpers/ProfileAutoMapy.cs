using System.Linq;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Models;

namespace DatingApp.API.Helpers
{
    public class ProfileAutoMapy : Profile
    {
        public ProfileAutoMapy()
        {
            CreateMap<User, UserForListDto>()
                .ForMember(dest => dest.ZdjecieUrl, opt => 
                    opt.MapFrom(src => src.Zdjecia.FirstOrDefault(p => p.ToMenu).Url))
                .ForMember(dest => dest.Wiek, opt => 
                    opt.MapFrom(src => src.Urodziny.WyliczWiek()));
            CreateMap<User, UserDetaleDto>()
                .ForMember(dest => dest.ZdjecieUrl, opt => 
                    opt.MapFrom(src => src.Zdjecia.FirstOrDefault(p => p.ToMenu).Url))
                .ForMember(dest => dest.Wiek, opt => 
                    opt.MapFrom(src => src.Urodziny.WyliczWiek()));
            CreateMap<Photo, DetaleDlaZdjecDto>();
        }
    }
}