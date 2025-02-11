using LearningASPweb.GlobalManagers.Contracts;
using Microsoft.AspNetCore.Mvc;
using LearningASPweb.Data.DTOs;
using AutoMapper;

namespace LearningASPweb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;

        public AuthController(IMapper mapper , IAuthManager authManager)
        {
            _mapper = mapper;
            _authManager = authManager;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Register(ApiUserDto userDto)
        {
            var errors =  await _authManager.AuthorizeUser(userDto);
            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return Ok();
        }
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Login(APIUserLoginDto userDto)
        {
            var loginResponse =  await _authManager.LoginUser(userDto);
            if (loginResponse == null) return Unauthorized();
            return Ok(loginResponse);
        }
        
        [HttpPost]
        [Route("refresh_token")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RefreshToken([FromBody] APIUserLoginResponseDto userDto)
        {
            var loginResponse =  await _authManager.ValidateRefreshToken(userDto);
            if (loginResponse == null) return Unauthorized();
            return Ok(loginResponse);
        }
    }
}
