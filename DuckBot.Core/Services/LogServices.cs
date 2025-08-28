using System;
using System.Collections.Concurrent;
using System.IO;

namespace DuckBot.Core.Services
{
    public static class LogService
    {
        private static readonly ConcurrentDictionary<string, string> Logs = new();

        private static string LogDir = Path.Combine(AppContext.BaseDirectory, "logs");

        static LogService()
        {
            Directory.CreateDirectory(LogDir);
        }

        public static void Log(string botName, string message)
        {
            string log = $"[{DateTime.Now:HH:mm:ss}] {message}";
            Logs[botName] = Logs.ContainsKey(botName) ? Logs[botName] + "\n" + log : log;

            File.AppendAllText(Path.Combine(LogDir, $"{botName}.log"), log + "\n");
            File.AppendAllText(Path.Combine(LogDir, $"global.log"), $"[{botName}] {log}\n");
        }

        public static string GetLogs(string botName)
        {
            return Logs.TryGetValue(botName, out var log) ? log : "";
        }
    }
}
