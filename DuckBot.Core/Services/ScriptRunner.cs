using System;
using System.Collections.Generic;
using System.Threading;
using DuckBot.Core.ADB;
using DuckBot.Core.Models;
using DuckBot.Core.Services;

namespace DuckBot.Core.Services
{
    public class ScriptRunner
    {
        private readonly string _adbPath;
        private readonly string _deviceId;

        public ScriptRunner(string adbPath, string deviceId)
        {
            _adbPath = adbPath;
            _deviceId = deviceId;
        }

        public void Run(List<ScriptStep> steps, string botName)
        {
            foreach (var step in steps)
            {
                try
                {
                    switch (step.Type)
                    {
                        case StepType.Tap:
                            AdbClient.Tap(_adbPath, _deviceId, int.Parse(step.Param1), int.Parse(step.Param2));
                            break;
                        case StepType.Swipe:
                            AdbClient.Swipe(_adbPath, _deviceId,
                                int.Parse(step.Param1), int.Parse(step.Param2),
                                int.Parse(step.Param3.Split(',')[0]), int.Parse(step.Param3.Split(',')[1]));
                            break;
                        case StepType.Input:
                            AdbClient.InputText(_adbPath, _deviceId, step.Param1);
                            break;
                        case StepType.Wait:
                            Thread.Sleep(int.Parse(step.Param1));
                            break;
                        case StepType.IfImage:
                            if (ImageService.FindOnScreen(_adbPath, _deviceId, step.Param1))
                                AdbClient.Tap(_adbPath, _deviceId, int.Parse(step.Param2), int.Parse(step.Param3));
                            break;
                        case StepType.OCRWait:
                            OCRService.WaitForText(_adbPath, _deviceId, step.Param1, int.Parse(step.Param2));
                            break;
                        case StepType.Loop:
                            int count = int.Parse(step.Param1);
                            for (int i = 0; i < count; i++)
                                Run(steps.GetRange(steps.IndexOf(step) + 1, steps.Count - (steps.IndexOf(step) + 1)), botName);
                            return; // stop after loop finishes
                    }
                    Thread.Sleep(step.DelayMs);
                    LogService.Log(botName, $"Executed step {step.Type}");
                }
                catch (Exception ex)
                {
                    LogService.Log(botName, $"Error executing {step.Type}: {ex.Message}");
                }
            }
        }
    }
}
