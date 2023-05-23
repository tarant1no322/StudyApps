using FluentResults.Extensions.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiEmployees.Domain.DTOs.User;
using WebApiEmployees.Service.Interfaces;

namespace WebApiEmployees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public AuthController(IConfiguration configuration, IAuthService authService)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser(UserDto request)
        {
            return await _authService.Register(request).ToActionResult();
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUser(UserDto request)
        {
            return await _authService.Login(request).ToActionResult();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> ChangeUserRole(ChangeRoleDto changeRoleDto)
        {
            return await _authService.ChangeUserRole(changeRoleDto).ToActionResult();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteUser([FromBody] string username)
        {
            return await _authService.Delete(username).ToActionResult();
        }
    }
}
