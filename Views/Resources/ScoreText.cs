namespace MathTrainer.Views.Resources
{
    class ScoreText
    {
        public const string ScoresHeadlinePrompt = "Your scores:";
        public const string MathScoresHeadlinePrompt = "Mathematics:";
        public const string ConversionScoresHeadlinePrompt = "Conversion:";

        public static string CorrectAnswersPrompt(int value) => $"Correct answers: {value}";
        public static string TotalAnswersPrompt(int value) => $"Total answers: {value}";
        public static string IncorrectAnswersPrompt(int value) => $"Incorrect answers: {value}";
        public static string ScoresAccuracyPrompt(double value) => $"Accuracy: {value:P}";

        public const string ResetScorePrompt = "r - Reset scores";
    }
}
