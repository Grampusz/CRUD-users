using Microsoft.AspNetCore.Http;
using TestTast.Models;

namespace TestTast.Helpers
{
    public class IsThatUserHelper
    {
        public static bool isThatUser(string token, string login)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }
            if (!UserStore.Tokens.TryGetValue(token, out var user))
            {
                return false;
            }
            if (user.Login == login)
            {
                return true;
            }
            return false;
        }
    }
}
