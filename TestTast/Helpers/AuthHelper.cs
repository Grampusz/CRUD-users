namespace TestTast.Helpers
{
    public static class AuthHelper
    {
        public static string GetToken(HttpContext context)
        {
            return context.Request.Headers["Authorization"].FirstOrDefault();
        }
    }
}
