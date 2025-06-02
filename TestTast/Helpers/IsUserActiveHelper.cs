using Microsoft.AspNetCore.Identity;
using TestTast.Models;

namespace TestTast.Helpers
{
    public static class IsUserActiveHelper
    {
        public static bool isUserActive(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }
            if (!UserStore.Tokens.TryGetValue(token, out var user)) 
            {
                return false;
            }
            if (!user.RevokedOn.HasValue)
            {
                return true;
            }
            return false;
        }
    }
}
