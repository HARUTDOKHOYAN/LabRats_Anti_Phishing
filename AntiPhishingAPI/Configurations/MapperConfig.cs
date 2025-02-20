using AntiPhishingAPI.Data.DTO;
using AutoMapper;
using LearningASPweb.Data;
using LearningASPweb.Data.DTOs;

namespace LearningASPweb.Configurations;

public class MapperConfig: Profile
{
    public MapperConfig()
    {
        CreateMap<APIUserModel, ApiUserDto>().ReverseMap();
        CreateMap<string, CheckingLink>()
            .ForMember(dest => dest.Link, opt => opt.MapFrom(src => src.ToLower()))
            .ForMember(dest => dest.Dangerousity, opt => opt.Ignore());
    }
}