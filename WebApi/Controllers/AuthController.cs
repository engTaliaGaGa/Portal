using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Singleton.Interface;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public IActionResult Token([FromBody] dynamic data)
        {
            if (_authService.ValidateLogin(data.UserName, data.Password))
            {
                var date = DateTime.UtcNow;
                var expireDate = TimeSpan.FromHours(5);
                var expireDateTime = date.Add(expireDate);
                var token = _authService.GenerateToken(date, data.UserName, data.IdApp, expireDate);

                return Ok(new
                {
                    Token = token,
                    ExpireAt = expireDateTime
                });
            }
            else
            {
                return StatusCode(401);
            }
        }
    }
}
