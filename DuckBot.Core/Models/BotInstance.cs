using System;

namespace DuckBot.Core.Models
{
    public class BotInstance
    {
        public int Id { get; set; }
        public bool IsSelected { get; set; }
        public string Name { get; set; } = "New Bot";
        public string Game { get; set; } = "N/A";
        public string Emulator { get; set; } = "N/A";
        public string CurrentScript { get; set; } = "N/A";
        public TimeSpan Runtime { get; set; }
        public int ScriptsProcessed { get; set; }
    }
}
