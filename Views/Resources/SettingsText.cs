namespace MathTrainer.Views.Resources
{
    class SettingsText
    {
        public const string SettingsHeadlinePrompt = "Settings:";
        public static string MaxDigitsPrompt(int current) => $"1 - Change max digits in one number. Current: {current}";
        public static string MaxOperationsPrompt(int current) => $"2 - Change max operations in problem. Current: {current}";
        public static string FixedDigitsPrompt(bool enabled) => $"3 - Toggle fixed digits. Current: {(OnOff(enabled))}";
        public static string FixedOperationsPrompt(bool enabled) => $"4 - Toggle fixed operations. Current: {(OnOff(enabled))}";
        private static string OnOff(bool value) =>
            value ? "On" : "Off";

        public const string ResetToDefaultPrompt = "r - Reset to default";
        public const string NewValuePrompt = "New Value: ";
    }
}
