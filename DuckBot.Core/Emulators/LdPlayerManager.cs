using System.Diagnostics;
using System.IO;

namespace DuckBot.Core.Emulators
{
    public class LdPlayerManager
    {
        private readonly string _ldConsolePath;

        public LdPlayerManager(string ldPlayerPath)
        {
            _ldConsolePath = Path.Combine(ldPlayerPath, "ldconsole.exe");
        }

        public string ListInstances() => RunConsole("list2");

        public void StartInstance(string nameOrId) => RunConsole($"launch --index {nameOrId}");

        public void StopInstance(string nameOrId) => RunConsole($"quit --index {nameOrId}");

        private string RunConsole(string args)
        {
            ProcessStartInfo psi = new()
            {
                FileName = _ldConsolePath,
                Arguments = args,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using Process proc = Process.Start(psi)!;
            return proc.StandardOutput.ReadToEnd();
        }
    }
}
