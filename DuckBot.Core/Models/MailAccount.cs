namespace DuckBot.Core.Models
{
    /// <summary>
    /// Represents a mail login used by a bot instance
    /// for West Game verification or account creation.
    /// </summary>
    public class MailAccount
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // Which emulator this account is tied to
        public string Emulator { get; set; } = string.Empty;

        // Optional PIN (for login flows that need it)
        public string Pin { get; set; } = string.Empty;

        // Whether this account is active
        public bool Active { get; set; } = true;
    }
}

