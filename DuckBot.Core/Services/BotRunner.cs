using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DuckBot.Core.Models;

namespace DuckBot.Core.Services
{
    public class BotRunner
    {
        public InstanceConfig Config { get; }
        private CancellationTokenSource _cts;
        private bool _paused;

        public event Action<string>? LogMessage;

        public BotRunner(InstanceConfig config)
        {
            Config = config;
            _cts = new CancellationTokenSource();
        }

        public async Task StartAsync()
        {
            Log($"Bot {Config.InstanceName} starting...");
            _cts = new CancellationTokenSource();

            try
            {
                foreach (var script in Config.Scripts)
                {
                    if (!script.Enabled) continue;

                    Log($"Running script: {script.Name}");

                    await ScriptRunner.Run(script, Config.Emulator, _cts.Token, () => _paused);

                    Log($"Finished script: {script.Name}");
                }

                Log("All scripts complete.");
            }
            catch (OperationCanceledException)
            {
                Log("Bot stopped.");
            }
        }

        public void Stop()
        {
            Log("Stopping bot...");
            _cts.Cancel();
        }

        public void Pause()
        {
            Log("Bot paused.");
            _paused = true;
        }

        public void Resume()
        {
            Log("Bot resumed.");
            _paused = false;
        }

        private void Log(string msg)
        {
            LogMessage?.Invoke($"[{DateTime.Now:T}] {msg}");
            LogService.Log(Config.InstanceName, msg);
        }
    }
}
