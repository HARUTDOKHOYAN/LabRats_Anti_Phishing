using System.Security.Claims;
using LearningASPweb.Data.DTOs;
using Microsoft.AspNetCore.Identity;

namespace LearningASPweb.GlobalManagers.Contracts;

public interface IAuthManager : IRefreshToken
{
    Task<IEnumerable<IdentityError>> AuthorizeUser(ApiUserDto user);
    Task<APIUserLoginResponseDto> LoginUser(APIUserLoginDto user);
}