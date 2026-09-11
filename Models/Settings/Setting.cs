namespace MathTrainer.Models.Settings
{
    public class Setting
    {
        public int MaxDigits { get; set; } = 3;
        public int MaxOperations { get; set; } = 5;
        public bool FixedDigits { get; set; }
        public bool FixedOperations { get; set; }
    }
}
