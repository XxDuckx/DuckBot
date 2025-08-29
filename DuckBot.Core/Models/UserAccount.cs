namespace DuckBot.Core.Models
{
    public class UserAccount
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsPremium { get; set; } = false;
    }
}
