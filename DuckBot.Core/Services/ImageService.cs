using System;
using System.IO;
using DuckBot.Core.ADB;
using OpenCvSharp;

namespace DuckBot.Core.Services
{
    public static class ImageService
    {
        public static bool FindOnScreen(string adbPath, string deviceId, string templatePath, double threshold = 0.85)
        {
            byte[] screenBytes = AdbClient.ScreenCap(adbPath, deviceId);
            string tmp = Path.GetTempFileName();
            File.WriteAllBytes(tmp, screenBytes);

            using var screen = Cv2.ImRead(tmp, ImreadModes.Color);
            using var template = Cv2.ImRead(templatePath, ImreadModes.Color);

            if (screen.Empty() || template.Empty())
                return false;

            using var result = new Mat();
            Cv2.MatchTemplate(screen, template, result, TemplateMatchModes.CCoeffNormed);
            Cv2.MinMaxLoc(result, out _, out double maxVal, out _, out _);

            return maxVal >= threshold;
        }
    }
}
