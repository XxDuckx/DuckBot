using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace DuckBot.Auth
{
    public class AuthResponse
    {
        public string Token { get; set; } = "";
        public string Plan { get; set; } = "free"; // free, trial, premium
        public string Expires { get; set; } = "";
    }

    public static class AuthService
    {
        private static readonly string AuthFile = "auth.json";

        public static async Task<AuthResponse?> Login(string username, string password)
        {
            // TODO: Replace with real API later
            await Task.Delay(500); // fake network latency

            if (NewMethod(username, password))
            {
                var auth = new AuthResponse
                {
                    Token = "dummy-token-123",
                    Plan = "premium",
                    Expires = "2099-01-01"
                };
                SaveAuth(auth);
                return auth;
            }

            return null;
        }

        private static bool NewMethod(string username, string password)
        {
            return username == "test" && password == "1234";
        }

        public static void SaveAuth(AuthResponse auth)
        {
            string json = JsonSerializer.Serialize(auth, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(AuthFile, json);
        }

        public static AuthResponse? LoadAuth()
        {
            if (!File.Exists(AuthFile)) return null;
            string json = File.ReadAllText(AuthFile);
            return JsonSerializer.Deserialize<AuthResponse>(json);
        }

        public static void Logout()
        {
            if (File.Exists(AuthFile))
                File.Delete(AuthFile);
        }
    }
}
