namespace Cura.Task.App.Helper
{
    public static class SessionData
    {
        public static bool IsHaveSession()
        {
            return !string.IsNullOrEmpty(SessionName) ? true : false;
        }

        public static string? SessionName { get; set ; }
        public static string UserEmail { get; set ; }
    }
}
