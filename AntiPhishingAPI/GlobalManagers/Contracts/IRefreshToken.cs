using System.IdentityModel.Tokens.Jwt;
using LearningASPweb.Data;
using LearningASPweb.Data.DTOs;
using Microsoft.AspNetCore.Identity;

namespace LearningASPweb.GlobalManagers.Contracts;

public interface IRefreshToken
{
    Task<string> GenerateRefreshToken(APIUserModel user);
    Task<APIUserLoginResponseDto> ValidateRefreshToken(APIUserLoginResponseDto refreshToken);
}