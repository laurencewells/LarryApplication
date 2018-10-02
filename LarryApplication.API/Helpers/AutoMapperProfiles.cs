using System.Linq;
using AutoMapper;
using LarryApplication.API.Dtos;
using LarryApplication.API.Models;

namespace LarryApplication.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
            .ForMember(dest => dest.PhotoUrl, opt => {
                opt.MapFrom(src =>src.Photos.FirstOrDefault(p => p.IsMain).Url);
            })
            .ForMember(dest => dest.Age, opt => {
                opt.ResolveUsing(d => d.DateofBirth.CalculateAge());
            });

            CreateMap<User, UserForDetailsDto>()
                .ForMember(dest => dest.PhotoUrl, opt => {
                opt.MapFrom(src =>src.Photos.FirstOrDefault(p => p.IsMain).Url);
            })
            .ForMember(dest => dest.Age, opt => {
                opt.ResolveUsing(d => d.DateofBirth.CalculateAge());
            });

            CreateMap<Photo, PhotosForDetailedDto>();

        }
    }
}