using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using LearningASPweb.Data;
using LearningASPweb.Data.DTOs;
using LearningASPweb.GlobalManagers.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace LearningASPweb.GlobalManagers;

public class AuthManager : IAuthManager
{
    private readonly IMapper _mapper;
    private readonly UserManager<APIUserModel> _userManager;
    private readonly IConfiguration _configuration;
    private const string LOGIN_PROVIDER = "TumoLubCybersecurityAPI"; 
    private const string REFRESH_TOKEN = "RefreshToken";

    public AuthManager(IMapper mapper , UserManager<APIUserModel> userManager ,IConfiguration configuration)
    {
        _mapper = mapper;
        _userManager = userManager;
        _configuration = configuration;
    }
    public async Task<IEnumerable<IdentityError>> AuthorizeUser(ApiUserDto userDto)
    {
        var userModel = _mapper.Map<APIUserModel>(userDto);
        userModel.UserName = userDto.Email;
        var result = await _userManager.CreateAsync(userModel, userDto.Password);

        if (result.Succeeded)
            await _userManager.AddToRoleAsync(userModel, "User");
        return result.Errors;
    }

    public async Task<APIUserLoginResponseDto> LoginUser(APIUserLoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, dto.Password);
        
        if(!isPasswordValid) return null;
        
        var response = new APIUserLoginResponseDto()
        {
            UserID = user.Id,
            Token = await GenerateJwtTokenAsync(user),
            RefreshToken = await GenerateRefreshToken(user)
        };
        return response;
    }
    public async Task<string> GenerateRefreshToken(APIUserModel user)
    {
        await _userManager.RemoveAuthenticationTokenAsync(user , LOGIN_PROVIDER, REFRESH_TOKEN);
        var newRefreshToken = await _userManager.GenerateUserTokenAsync(user,LOGIN_PROVIDER, REFRESH_TOKEN);
        var result = await _userManager.SetAuthenticationTokenAsync(user, LOGIN_PROVIDER, REFRESH_TOKEN, newRefreshToken);
        if (result.Succeeded) return newRefreshToken;
        throw new Exception($"Refresh Token Error: {result.Errors.FirstOrDefault()?.Description}");
    }

    public async Task<APIUserLoginResponseDto> ValidateRefreshToken(APIUserLoginResponseDto loginDto)
    {
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = jwtSecurityTokenHandler.ReadJwtToken(loginDto.Token);
        var userName = jwtToken.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var user = await _userManager.FindByNameAsync(userName);
        
        if (user == null || user.Id != loginDto.UserID) return null;
        
        var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(user,LOGIN_PROVIDER  , REFRESH_TOKEN,  loginDto.RefreshToken);
        APIUserLoginResponseDto response = null;
        if (isValidRefreshToken)
             response = new APIUserLoginResponseDto
            {
                UserID = user.Id,
                Token = await GenerateJwtTokenAsync(user),
                RefreshToken = await GenerateRefreshToken(user),
            };
        
        await _userManager.UpdateSecurityStampAsync(user);
        return response;
    }

    private async Task<string> GenerateJwtTokenAsync(APIUserModel user)
    {
        var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));

        var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
        var userClaims = await _userManager.GetClaimsAsync(user);

        var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
            }
            .Union(userClaims).Union(roleClaims);

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}