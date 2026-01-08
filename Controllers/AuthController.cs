using BlueMediCore.DTOs;
using BlueMediCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BlueMediCore.DTOs.AuthDtos;

namespace BlueMediCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);

            if (!result.Success)
                return Unauthorized(result);

            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public IActionResult SecureEndpoint()
        {
            return Ok("Token geçerli");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AdminOnly()
        {
            return Ok("Admin yetkisi var");
        }

    }
}
