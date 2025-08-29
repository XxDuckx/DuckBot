using System.Collections.Generic;
using System.Threading.Tasks;
using DuckBot.Core.Models;

namespace DuckBot.Core.Services
{
    public static class BotManager
    {
        private static readonly Dictionary<string, BotRunner> _running = new();

        public static async Task StartBot(InstanceConfig config)
        {
            if (_running.ContainsKey(config.InstanceName))
                return;

            var runner = new BotRunner(config);
            _running[config.InstanceName] = runner;

            await Task.Run(() => runner.StartAsync());
        }

        public static void StopBot(string instanceName)
        {
            if (_running.TryGetValue(instanceName, out var runner))
            {
                runner.Stop();
                _running.Remove(instanceName);
            }
        }

        public static void PauseBot(string instanceName)
        {
            if (_running.TryGetValue(instanceName, out var runner))
                runner.Pause();
        }

        public static void ResumeBot(string instanceName)
        {
            if (_running.TryGetValue(instanceName, out var runner))
                runner.Resume();
        }
    }
}
