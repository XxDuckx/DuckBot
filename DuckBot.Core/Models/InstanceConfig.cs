namespace DuckBot.Core.Models
{
    public class InstanceConfig
    {
        public string InstanceName { get; set; } = "New Instance";
        public string Emulator { get; set; } = "LDPlayer";
        public string MailAccount { get; set; } = string.Empty;
        public string[] Scripts { get; set; } = new string[0];
    }
}
