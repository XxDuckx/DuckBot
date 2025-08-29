namespace DuckBot.Core.Models
{
    public class GameAccount
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Game { get; set; } = "West Game";
        public bool TutorialComplete { get; set; } = false;
    }
}
