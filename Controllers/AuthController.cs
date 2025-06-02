using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using TestTast.DTO;
using TestTast.Models;
using TestTast.Services;

namespace TestTast.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private AuthService _authService;
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var (success, message) = _authService.Login(request);
            //var user = UserStore.Users.FirstOrDefault(u =>
            //    u.Login == request.login &&
            //    u.Password == request.password &&
            //    u.RevokedOn == null
            //);
            //if (user == null)
            //{
            //    return Unauthorized("Неверный логин или пароль");
            //}
            //foreach (var item in UserStore.Tokens.Where(item => item.Value.Login == request.login))
            //{
            //    UserStore.Tokens.Remove(item.Key);
            //}
            //var token = Guid.NewGuid().ToString();
            //UserStore.Tokens[token] = user;
            if (success)
            {
                return Ok(new { Token = message });
            }
            return BadRequest(message);
            
        }
        public AuthController (AuthService authService)
        {
            _authService = authService;
        }
    }
}
