using System.Diagnostics;

namespace DuckBot.Core.ADB
{
    public static class AdbClient
    {
        public static string RunCommand(string adbPath, string deviceId, string args)
        {
            ProcessStartInfo psi = new()
            {
                FileName = adbPath,
                Arguments = $"-s {deviceId} {args}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using Process proc = Process.Start(psi)!;
            return proc.StandardOutput.ReadToEnd();
        }

        public static void Tap(string adbPath, string deviceId, int x, int y) =>
            RunCommand(adbPath, deviceId, $"shell input tap {x} {y}");

        public static void Swipe(string adbPath, string deviceId, int x1, int y1, int x2, int y2, int duration = 500) =>
            RunCommand(adbPath, deviceId, $"shell input swipe {x1} {y1} {x2} {y2} {duration}");

        public static void InputText(string adbPath, string deviceId, string text) =>
            RunCommand(adbPath, deviceId, $"shell input text \"{text}\"");

        public static byte[] ScreenCap(string adbPath, string deviceId)
        {
            var output = RunCommand(adbPath, deviceId, "exec-out screencap -p");
            return System.Text.Encoding.UTF8.GetBytes(output);
        }
    }
}
