using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using DuckBot.Core.Models;

namespace DuckBot.Core.Services
{
    public static class AuthService
    {
        private static readonly string UserFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "users.json");
        private static List<UserAccount> _users = new();
        public static UserAccount? CurrentUser { get; private set; }

        static AuthService()
        {
            if (File.Exists(UserFile))
            {
                _users = JsonSerializer.Deserialize<List<UserAccount>>(File.ReadAllText(UserFile)) ?? new();
            }
            else
            {
                _users = new List<UserAccount>();
                SaveUsers();
            }

            // Add Ducko master if missing
            if (!_users.Exists(u => u.Username == "Ducko"))
            {
                _users.Add(new UserAccount { Username = "Ducko", Password = "test123", IsPremium = true });
                SaveUsers();
            }
        }

        public static bool Login(string username, string password)
        {
            var user = _users.Find(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                CurrentUser = user;
                return true;
            }
            return false;
        }

        public static bool Register(string username, string password, bool premium = false)
        {
            if (_users.Exists(u => u.Username == username)) return false;

            _users.Add(new UserAccount { Username = username, Password = password, IsPremium = premium });
            SaveUsers();
            return true;
        }

        private static void SaveUsers()
        {
            File.WriteAllText(UserFile, JsonSerializer.Serialize(_users, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}

