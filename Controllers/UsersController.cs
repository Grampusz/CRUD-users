using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestTast.DTO;
using TestTast.Helpers;
using TestTast.Models;
using TestTast.Services;

namespace TestTast.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        public UsersController(UserService userService) 
        {
            _userService = userService;
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateRequest createRequest)
        {
            string token = AuthHelper.GetToken(HttpContext);
            var (success, message) = _userService.CreateUser(createRequest, token);
            if (success) return Ok(message);
            return BadRequest(message);
            //var isAdmin = IsUserAdminHelper.IsAdmin(token);
            //bool isUnique = true;
            //if(UserStore.Users.FirstOrDefault(u => u.Login == createRequest.login) != null) isUnique = false;

            //if (isAdmin && isUnique)
            //{
            //    UserStore.Tokens.TryGetValue(token, out var currentUser);
            //    UserStore.AddUser(createRequest, currentUser.Name);
            //    return Ok("Пользователь создан");
            //}
            //if (isAdmin == false)
            //{
            //    return BadRequest("Только для админов");
            //}
            //if (isUnique == false)
            //{
            //     return BadRequest("Логин должен быть уникальным");   
            //}
            //return BadRequest("Неизвестная ошибка");
        }
        [HttpPost("tokens")]
        public IActionResult GetTokens()
        {
            string token = AuthHelper.GetToken(HttpContext);
            var isAdmin = IsUserAdminHelper.IsAdmin(token);
            if (isAdmin)
            {
                return Ok(new { tokens = UserStore.Tokens.Keys.ToList() });
            }
            return Unauthorized("только для админов");
        }
        [HttpGet("readActive")]
        public IActionResult GetUsers()
        {
            string token = AuthHelper.GetToken(HttpContext);
            var (success, message) = _userService.GetUsers(token);
            if (success) return Ok(message);
            return BadRequest(message);
            
        }
        [HttpGet("readByLogin")]
        public IActionResult GetUserByLogin([FromQuery] ReadByLoginRequest request)
        {
            string token = AuthHelper.GetToken(HttpContext);
            var (success, message) = _userService.GetUserByLogin(request, token);
            if (success) return Ok(message);
            return BadRequest(message);
        }
        [HttpGet("readByLoginAndPass")]
        public IActionResult GetUserByLoginAndPass([FromQuery] ReadByLoginAndPassRequest request)
        {
            string token = AuthHelper.GetToken(HttpContext);
            var (success, message) = _userService.GetUserByLoginAndPass(request, token);
            if (success) return Ok(success);
            return BadRequest(message);
        }
        [HttpGet("readByAge")]
        public IActionResult GetUsersByAge([FromQuery] ReadByAgeRequest request)
        {
            string token = AuthHelper.GetToken(HttpContext);
            var (success, message) = _userService.GetUsersByAge(request, token);
            if (success) return Ok(message);
            return BadRequest(message);
        }
        [HttpPost("updateProfile")]
        public IActionResult UpdateProfile([FromBody] UpdateProfileRequest request)
        {
            string token = AuthHelper.GetToken(HttpContext);
            var (success, message) = _userService.UpdateProfile(request, token);
            if (success) return Ok(message);
            return BadRequest(message);
        }
        [HttpPost("changeLogin")]
        public IActionResult ChangeLogin([FromBody] ChangeLoginRequest request)
        {
            string token = AuthHelper.GetToken(HttpContext);
            var (success, message) = _userService.ChangeLogin(request, token);
            if (success) return Ok(message);
            return BadRequest(message);
        }
        [HttpPost("changePassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordRequest request)
        {
            string token = AuthHelper.GetToken(HttpContext);
            var (success, message) = _userService.ChangePassword(request, token);
            if (success) return Ok(message);
            return BadRequest(message);
        }
        [HttpPost("deleteUser")]
        public IActionResult DeleteUser([FromBody] DeleteUserRequest request)
        {
            string token = AuthHelper.GetToken(HttpContext);
            var (success, message) = _userService.DeleteUser(request, token);
            if (success) return Ok(message);
            return BadRequest(message);
        }
        [HttpPost("restoreUser")]
        public IActionResult RestoreUser([FromBody] RestoreUserRequest request)
        {
            string token = AuthHelper.GetToken(HttpContext);
            var (success, message) = _userService.RestoreUser(request, token);
            if (success) return Ok(message);
            return BadRequest(message);
        }
    }
}
