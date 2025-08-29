using System.IO;
using System.Text.Json;
using DuckBot.Core.Models;

namespace DuckBot.Core.Services
{
    public static class ConfigService
    {
        private static readonly JsonSerializerOptions _options = new() { WriteIndented = true };
        private static readonly string ConfigDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configs");

        static ConfigService()
        {
            if (!Directory.Exists(ConfigDir))
                Directory.CreateDirectory(ConfigDir);
        }

        public static void SaveConfig(string fileName, InstanceConfig config)
        {
            var path = Path.Combine(ConfigDir, fileName + ".json");
            var json = JsonSerializer.Serialize(config, _options);
            File.WriteAllText(path, json);
        }

        public static InstanceConfig LoadConfig(string fileName)
        {
            var path = Path.Combine(ConfigDir, fileName + ".json");
            if (!File.Exists(path)) return new InstanceConfig();
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<InstanceConfig>(json, _options) ?? new InstanceConfig();
        }
    }
}
