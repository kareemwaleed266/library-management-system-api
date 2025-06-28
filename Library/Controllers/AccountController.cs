using Library.Data.Entites.IdentityEntities;
using Library.Service.UserService.Dto;
using Library.Service.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto input)
        {
            var user = await _userService.Login(input);
            if (user == null)
                return Unauthorized();

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto input)
        {
            var user = await _userService.Register(input, input.UserRole);
            if (user == null)
                return BadRequest(new Exception("Email Already Exists"));

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<UserDto>> RefreshToken()
        {
            try
            {
                var user = await _userService.RefreshTokenAsync();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }


        [Authorize]
        [HttpPost("revoke-token")]
        public async Task<ActionResult> RevokeToken([FromBody] RevokeToken? request)
        {
            try
            {
                await _userService.RevokeRefreshTokenAsync(request?.Token);
                return Ok(new { message = "Refresh token revoked successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await _userService.Logout();
            return Ok(new { message = "Logged out successfully" });
        }
    }
}
