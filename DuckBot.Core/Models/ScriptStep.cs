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
        public string Param1 { get; set; } = "";
        public string Param2 { get; set; } = "";
        public string Param3 { get; set; } = "";
        public int DelayMs { get; set; } = 500;
    }
}
