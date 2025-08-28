namespace DuckBot.Core.Models
{
    public enum StepType
    {
        Tap,
        Swipe,
        Input,
        Wait,
        IfImage,
        OCRWait,
        Loop
    }

    public class ScriptStep
    {
        public StepType Type { get; set; }
        public string Param1 { get; set; } = ""; // X / Text / ImagePath / LoopCount
        public string Param2 { get; set; } = ""; // Y / Duration / OCR text
        public string Param3 { get; set; } = ""; // Extra
        public int DelayMs { get; set; } = 500; // cooldown after step
    }
}
