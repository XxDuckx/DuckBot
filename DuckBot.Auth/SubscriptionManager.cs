namespace DuckBot.Auth
{
    public static class SubscriptionManager
    {
        private static AuthResponse? _auth;

        public static void SetAuth(AuthResponse auth)
        {
            _auth = auth;
        }

        public static bool IsLoggedIn => _auth != null;

        public static bool IsPremium => _auth != null && (_auth.Plan == "premium" || _auth.Plan == "trial");

        public static string CurrentPlan => _auth?.Plan ?? "none";
    }
}
