using System;
using System.IO;
using System.Text.Json;
using DuckBot.Core.Models;

namespace DuckBot.Core.Services
{
    public static class ConfigService
    {
        private static string ConfigDir => Path.Combine(AppContext.BaseDirectory, "instances");

        public static InstanceConfig Load(string instanceName)
        {
            Directory.CreateDirectory(ConfigDir);
            string path = Path.Combine(ConfigDir, $"{instanceName}.json");

            if (!File.Exists(path))
                return new InstanceConfig { InstanceName = instanceName };

            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<InstanceConfig>(json) ?? new InstanceConfig { InstanceName = instanceName };
        }

        public static void Save(InstanceConfig config)
        {
            Directory.CreateDirectory(ConfigDir);
            string path = Path.Combine(ConfigDir, $"{config.InstanceName}.json");

            string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }
    }
}
