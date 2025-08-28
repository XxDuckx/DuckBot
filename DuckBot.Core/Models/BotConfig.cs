using System.Collections.Generic;

namespace DuckBot.Core.Models
{
    public class BotConfig
    {
        public string Name { get; set; } = "New Bot";
        public List<ScriptStep> Scripts { get; set; } = new();
        public List<EmulatorConfig> Emulators { get; set; } = new();
        public List<MailAccount> MailAccounts { get; set; } = new();
        public BotOtherSettings OtherSettings { get; set; } = new();
    }

    public class EmulatorConfig
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string AssignedTask { get; set; } = "";
    }

    public class MailAccount
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string Emulator { get; set; } = "";
        public string Pin { get; set; } = "";
        public bool Active { get; set; } = false;
    }

    public class BotOtherSettings
    {
        public int BreakMin { get; set; }
        public int BreakMax { get; set; }
        public bool StopAfterLoop { get; set; } = false;
        public bool IgnoreCooldowns { get; set; } = false;
    }
}
