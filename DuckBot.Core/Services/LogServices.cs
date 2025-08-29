using System;
using System.IO;

namespace DuckBot.Core.Services
{
    public static class LogService
    {
        private static readonly string LogDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");

        static LogService()
        {
            Directory.CreateDirectory(LogDir);
        }

        public static void Log(string instanceName, string message)
        {
            string path = Path.Combine(LogDir, $"{instanceName}.log");
            string line = $"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}";
            File.AppendAllText(path, line);

            // Also append to global
            string global = Path.Combine(LogDir, "global.log");
            File.AppendAllText(global, line);
        }
    }
}
