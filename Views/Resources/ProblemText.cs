namespace MathTrainer.Views.Resources
{
    public class ProgramText
    {
        public const string SymbolExitToMainMenuPrompt = "n - Main Menu";
        public const string NextProblemPrompt = $"1 - Next problem";
        public static string CorrectInputPrompt(int attempts) => $"Correct! It took {attempts} attempts.";
        public static string InvalidInputPrompt(int attempts) => $"Incorrect answer. Please try again. Attempts: {attempts}";
    }
}
