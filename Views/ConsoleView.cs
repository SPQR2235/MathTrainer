using MathTrainer.Models.Conversion;
using MathTrainer.Models.Problem;
using MathTrainer.Models.Scores;
using MathTrainer.Models.Settings;
using MathTrainer.Views.Resources;

namespace MathTrainer.Views
{
    class ConsoleView
    {
        public void ClearAndMoveCursor(int x, int y, ConsoleColor? backgroundColor = null)
        {
            Console.Clear();
            if (backgroundColor.HasValue)
                SetColor(backgroundColor.Value);
            Console.SetCursorPosition(x, y);
        }

        public void SetColor(ConsoleColor color = ConsoleColor.White, bool background = false)
        {
            if (background)
            {
                Console.BackgroundColor = color;
                return;
            }

            Console.ForegroundColor = color;
        }

        public void Print(string text, ConsoleColor textColor = ConsoleColor.White, bool newLine = true, bool centered = false, bool addIndentation = false)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = textColor;
            if (centered)
                CenteredPrint(addIndentation ? text + Environment.NewLine : text, newLine);
            else if (newLine)
                Console.WriteLine(addIndentation ? text + Environment.NewLine : text);
            else
                Console.Write(text);
            Console.ForegroundColor = oldColor;
        }
        public void PrintWithPrefix(string prefix, string text, ConsoleColor prefixColor = ConsoleColor.White, ConsoleColor textColor = ConsoleColor.White)
        {
            Print("[ ", ConsoleColor.DarkGray, false);
            Print(prefix, prefixColor, false);
            Print(" ]\t", ConsoleColor.DarkGray, false);
            Print(text, textColor);
        }

        public void CenteredPrint(string text, bool newLine = true)
        {
            int spacesToAdd = Math.Max(0, (Console.WindowWidth - text.Length) / 2);
            string spaces = new(' ', spacesToAdd);
            if (newLine)
                Console.WriteLine(spaces + text);
            else
                Console.Write(spaces + text);
        }

        public ConsoleKeyInfo PrintAndReadKey(string text, ConsoleColor textColor)
        {
            Print(text, textColor, true);
            return Console.ReadKey();
        }

        public string PrintAndReadLine(string text, ConsoleColor textColor)
        {
            Print(text, textColor, true);
            return ReceiveUserInput();
        }

        public string ReceiveUserInput()
        {
            return Console.ReadLine()?.ToLowerInvariant().Trim() ?? string.Empty;
        }

        public int ReceiveIntUserInput()
        {
            return int.TryParse(ReceiveUserInput(), out int result) ? result : int.MinValue;
        }

        public int ReceiveIntUserInput(string input)
        {
            return int.TryParse(input.ToLowerInvariant().Trim(), out int result) ? result : int.MinValue;
        }

        public void RenderHeader(string text, ConsoleColor textColor = ConsoleColor.Gray)
        {
            ClearAndMoveCursor(0, 1);
            Print(text, textColor, newLine: true, centered: true);
        }

        public void RenderMainMenu(ConsoleColor textColor = ConsoleColor.White)
        {
            RenderHeader(CommonText.ProgramHeadlinePrompt);
            Print($"""

                {MainMenuText.StartMathPrompt}
                {MainMenuText.StartConversionPrompt}
                {MainMenuText.ShowSettingsPrompt}
                {MainMenuText.ShowScoresPrompt}
                {MainMenuText.ExitProgramPrompt}

                {CommonText.UserChoicePrompt}
                """, textColor, newLine: false);
        }

        public void RenderProblemMenu(int attempts, ConsoleColor textColor = ConsoleColor.White)
        {
            RenderHeader(CommonText.ProgramHeadlinePrompt);
            Print($"""
                {ProgramText.CorrectInputPrompt(attempts)}

                {ProgramText.NextProblemPrompt}
                {CommonText.ExitToMainMenuPrompt}

                {CommonText.UserChoicePrompt}
                """, textColor, newLine: false);
        }

        public void RenderSelectModeMenu(ConsoleColor textColor = ConsoleColor.White)
        {
            RenderHeader(CommonText.ProgramHeadlinePrompt);
            Print(ConversionText.SelectModeMenuPrompt, textColor, newLine: false);
        }

        public void RenderProblem(Problem problem, ConsoleColor textColor = ConsoleColor.White)
        {
            RenderHeader(CommonText.ProgramHeadlinePrompt);
            Print($"""
                {ProgramText.SymbolExitToMainMenuPrompt}

                {problem.Headline}

                {problem.Value} = 
                """, textColor, newLine: false);
        }

        public void RenderSettings(Setting setting, ConsoleColor textColor = ConsoleColor.White)
        {
            RenderHeader(CommonText.ProgramHeadlinePrompt);
            Print($"""
                {SettingsText.SettingsHeadlinePrompt}

                {SettingsText.MaxDigitsPrompt(setting.MaxDigits)}
                {SettingsText.MaxOperationsPrompt(setting.MaxOperations)}
                {SettingsText.FixedDigitsPrompt(setting.FixedDigits)}
                {SettingsText.FixedOperationsPrompt(setting.FixedOperations)}

                {SettingsText.ResetToDefaultPrompt}
                {CommonText.ExitToMainMenuPrompt}

                {CommonText.UserChoicePrompt}
                """, textColor, newLine: false);
        }

        public void RenderScores(Score mathScore, Score conversionScore, ConsoleColor textColor = ConsoleColor.White)
        {
            RenderHeader(CommonText.ProgramHeadlinePrompt);
            Print($"""
                  {ScoreText.ScoresHeadlinePrompt}
                  
                  {ScoreText.MathScoresHeadlinePrompt}
                  {ScoreText.CorrectAnswersPrompt(mathScore.CorrectAnswers)}
                  {ScoreText.TotalAnswersPrompt(mathScore.TotalAnswers)}
                  {ScoreText.IncorrectAnswersPrompt(mathScore.IncorrectAnswers)}
                  {ScoreText.ScoresAccuracyPrompt(mathScore.Accuracy)}

                  {ScoreText.ConversionScoresHeadlinePrompt}
                  {ScoreText.CorrectAnswersPrompt(conversionScore.CorrectAnswers)}
                  {ScoreText.TotalAnswersPrompt(conversionScore.TotalAnswers)}
                  {ScoreText.IncorrectAnswersPrompt(conversionScore.IncorrectAnswers)}
                  {ScoreText.ScoresAccuracyPrompt(conversionScore.Accuracy)}

                  {ScoreText.ResetScorePrompt}
                  {CommonText.ExitToMainMenuPrompt}

                  {CommonText.UserChoicePrompt}
                  """, textColor, newLine: false);
        }
    }
}
