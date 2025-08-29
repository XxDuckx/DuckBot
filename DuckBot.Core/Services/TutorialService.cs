using DuckBot.Core.Models;
using System;
using System.Threading.Tasks;

namespace DuckBot.Core.Services
{
    public static class TutorialService
    {
        public static async Task CompleteTutorial(GameAccount acc, string adbPath, string emulatorId)
        {
            // Stub: Replace with real ADB tap/ocr flow
            await Task.Delay(3000);
            acc.TutorialComplete = true;
            Console.WriteLine($"Tutorial completed for {acc.Email}");
        }
    }
}
