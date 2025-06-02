using TestTast.Models;

namespace TestTast.Helpers
{
    public static class IsUserAdminHelper
    {
        public static bool IsAdmin(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }
            if (!UserStore.Tokens.TryGetValue(token, out var user))
            {
                return false;
            }
            if (!user.Admin)
            {
                return false;
            }
            return true;
        }
    }
}
