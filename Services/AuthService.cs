using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using TestTast.Models;

namespace TestTast.Services
{
    public class AuthService
    {
        public (bool, string) Login(DTO.LoginRequest request)
        {
            var user = UserStore.Login(request);
            if (user == null)
            {
                return (false, "Пользователь не найден");
            }
            UserStore.RemoveUserTokens(request.login);
            var token = Guid.NewGuid().ToString();
            UserStore.AddToken(user, token);
            return (true, token);
            
        }
    }
}
