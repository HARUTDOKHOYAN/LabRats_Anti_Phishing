using AutoMapper;
using LearningASPweb.Data;
using LearningASPweb.Data.DTOs;

namespace LearningASPweb.Configurations;

public class MapperConfig: Profile
{
    public MapperConfig()
    {
        CreateMap<APIUserModel, ApiUserDto>().ReverseMap();
    }
}