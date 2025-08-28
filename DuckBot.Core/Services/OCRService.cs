using System;
using System.IO;
using Tesseract;
using DuckBot.Core.ADB;

namespace DuckBot.Core.Services
{
    public static class OCRService
    {
        private static string _tessDataPath = Path.Combine(AppContext.BaseDirectory, "tessdata");

        public static string ReadText(string adbPath, string deviceId)
        {
            byte[] imageBytes = AdbClient.ScreenCap(adbPath, deviceId);

            using var engine = new TesseractEngine(_tessDataPath, "eng", EngineMode.Default);
            using var pix = Pix.LoadFromMemory(imageBytes); // ✅ Pix, not Bitmap
            using var page = engine.Process(pix);

            return page.GetText().Trim();
        }

        public static void WaitForText(string adbPath, string deviceId, string text, int timeoutMs = 5000)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < timeoutMs)
            {
                string screenText = ReadText(adbPath, deviceId);
                if (screenText.Contains(text, StringComparison.OrdinalIgnoreCase))
                    return;

                System.Threading.Thread.Sleep(500);
            }
            throw new TimeoutException($"Text '{text}' not found within {timeoutMs}ms");
        }
    }
}
