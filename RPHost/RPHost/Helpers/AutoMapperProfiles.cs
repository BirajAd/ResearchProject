using System.Linq;
using AutoMapper;
using RPHost.Dtos;
using RPHost.Models;

namespace RPHost.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>().
                ForMember(dest => dest.PhotoPath, opt => 
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Path))
                    .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<User, UserForDetailedListDto>().
                ForMember(dest => dest.PhotoPath, opt => 
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Path))
                    .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotosForDetailedDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<UserForRegisterDto, User>();
        }
    }
}