using OpenCvSharp;
using System;
using System.IO;
using DuckBot.Core.ADB;

namespace DuckBot.Core.Services
{
    public static class ImageService
    {
        public static bool FindOnScreen(string adbPath, string deviceId, string templatePath, double threshold = 0.8)
        {
            byte[] screenshotBytes = AdbClient.ScreenCap(adbPath, deviceId);
            string tmpScreenshot = Path.Combine(Path.GetTempPath(), "screencap.png");
            File.WriteAllBytes(tmpScreenshot, screenshotBytes);

            using var screen = Cv2.ImRead(tmpScreenshot, ImreadModes.Color);
            using var template = Cv2.ImRead(templatePath, ImreadModes.Color);

            using var result = new Mat();
            Cv2.MatchTemplate(screen, template, result, TemplateMatchModes.CCoeffNormed);
            Cv2.MinMaxLoc(result, out _, out double maxVal, out _, out _);

            return maxVal >= threshold;
        }
    }
}
