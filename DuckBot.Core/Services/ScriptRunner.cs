using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using DuckBot.Core.Models;

namespace DuckBot.Core.Services
{
    public static class ScriptRunner
    {
        public static async Task Run(ScriptInfo script, string emulator, CancellationToken token, Func<bool> isPaused)
        {
            // For now, just simulate execution
            LogService.Log(emulator, $"[ScriptRunner] Executing {script.Name}");

            for (int i = 0; i < 5; i++)
            {
                token.ThrowIfCancellationRequested();
                while (isPaused()) await Task.Delay(500, token);

                await Task.Delay(1000, token); // simulate adb step
                LogService.Log(emulator, $"[ScriptRunner] Step {i + 1}/5");
            }
        }

        private static void RunAdbCommand(string adbPath, string emulatorId, string command)
        {
            var psi = new ProcessStartInfo
            {
                FileName = adbPath,
                Arguments = $"-s {emulatorId} {command}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var proc = Process.Start(psi);
            proc?.WaitForExit();
        }
    }
}
