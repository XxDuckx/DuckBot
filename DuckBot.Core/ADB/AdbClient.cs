using System.Diagnostics;
using System.IO;

namespace DuckBot.Core.ADB
{
    public static class AdbClient
    {
        private static string RunAdb(string adbPath, string args)
        {
            var psi = new ProcessStartInfo
            {
                FileName = adbPath,
                Arguments = args,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var proc = Process.Start(psi);
            if (proc == null) return "";
            string output = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();
            return output;
        }

        public static void Tap(string adbPath, string deviceId, int x, int y) =>
            RunAdb(adbPath, $"-s {deviceId} shell input tap {x} {y}");

        public static void Swipe(string adbPath, string deviceId, int x1, int y1, int x2, int y2, int duration = 300) =>
            RunAdb(adbPath, $"-s {deviceId} shell input swipe {x1} {y1} {x2} {y2} {duration}");

        public static void InputText(string adbPath, string deviceId, string text) =>
            RunAdb(adbPath, $"-s {deviceId} shell input text \"{text}\"");

        public static byte[] ScreenCap(string adbPath, string deviceId)
        {
            string tmpFile = Path.GetTempFileName();
            RunAdb(adbPath, $"-s {deviceId} exec-out screencap -p > \"{tmpFile}\"");
            return File.ReadAllBytes(tmpFile);
        }
    }
}
