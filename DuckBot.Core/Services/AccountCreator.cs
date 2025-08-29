using DuckBot.Core.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DuckBot.Core.Services
{
    public static class AccountCreator
    {
        private static readonly string AccountFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "accounts.json");
        public static List<GameAccount> Accounts { get; private set; } = new();

        static AccountCreator()
        {
            if (File.Exists(AccountFile))
            {
                Accounts = JsonSerializer.Deserialize<List<GameAccount>>(File.ReadAllText(AccountFile)) ?? new();
            }
        }

        public static void Save()
        {
            File.WriteAllText(AccountFile, JsonSerializer.Serialize(Accounts, new JsonSerializerOptions { WriteIndented = true }));
        }

        public static void AddAccount(GameAccount acc)
        {
            Accounts.Add(acc);
            Save();
        }
    }
}
