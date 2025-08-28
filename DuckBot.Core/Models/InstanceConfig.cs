using System.Collections.Generic;

namespace DuckBot.Core.Models
{
    public class InstanceConfig
    {
        public string InstanceName { get; set; } = "New Instance";

        // Emulator(s) assigned to this bot instance
        public List<string> Emulators { get; set; } = new();

        // Scripts assigned to this instance
        public List<string> Scripts { get; set; } = new();

        // Mail accounts
        public List<MailAccount> MailAccounts { get; set; } = new();

        // Break settings
        public int BreakMinMinutes { get; set; } = 0;
        public int BreakMaxMinutes { get; set; } = 0;
        public bool StopAfterLoop { get; set; } = false;
        public bool IgnoreCooldowns { get; set; } = false;
    }

    public class MailAccount
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string Emulator { get; set; } = "";
        public string Pin { get; set; } = "";
        public bool Active { get; set; } = true;
    }
}
