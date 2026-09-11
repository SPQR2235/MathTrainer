using MathTrainer.Commands;
using MathTrainer.Models.Conversion;
using MathTrainer.Models.Problem;
using MathTrainer.Models.Scores;
using MathTrainer.Services;
using MathTrainer.States;
using MathTrainer.Views;
using MathTrainer.Views.Resources;

namespace MathTrainer.Controllers
{
    class Controller(ConsoleView view, ScoreManager scoreManager, SettingsManager settingsManager)
    {
        private readonly ConsoleView _view = view;
        private readonly ScoreManager _scoreManager = scoreManager;
        private readonly SettingsManager _settingsManager = settingsManager;

        public void Run()
        {
            AppState appState = AppState.MainMenu;

            while (appState != AppState.Exit)
                appState = HandleState(appState);
        }

        private AppState HandleState(AppState appState)
        {
            return appState switch
            {
                AppState.MainMenu => ShowMainMenu(),
                AppState.GenerateMathProblem => GenerateProblem(ProblemType.Mathematics),
                AppState.GenerateConversionProblem => GenerateProblem(ProblemType.Conversion),
                AppState.Settings => ShowSettings(),
                AppState.Scores => ShowScores(),
                _ => ShowMainMenu()
            };
        }

        private AppState ShowMainMenu()
        {
            _view.RenderMainMenu();

            int userInput = _view.ReceiveIntUserInput();

            return userInput switch
            {
                (int)NumberCommand.N1 => AppState.GenerateMathProblem,
                (int)NumberCommand.N2 => AppState.GenerateConversionProblem,
                (int)NumberCommand.N3 => AppState.Settings,
                (int)NumberCommand.N4 => AppState.Scores,
                (int)NumberCommand.N0 => AppState.Exit,
                _ => AppState.MainMenu
            };
        }

        private AppState GenerateProblem(ProblemType problemType)
        {
            bool math = problemType == ProblemType.Mathematics;

            Problem problem;

            ConversionMode mode;

            Action<ScoreType> changeScore = math
                ? type => _scoreManager.ChangeMathScore(type)
                : type => _scoreManager.ChangeConversionScore(type);

            string command;
            int attempts = 1;

            if (!math)
            {
                while (true)
                {
                    _view.RenderSelectModeMenu();

                    command = _view.ReceiveUserInput();

                    if (SymbolCommand.IsR(command))
                        mode = GetRandomConversionMode();

                    else if (_view.ReceiveIntUserInput(command) == (int)NumberCommand.N0)
                        return AppState.MainMenu;

                    else if (Enum.TryParse(command, out ConversionMode selectedMode)
                             && Enum.IsDefined(selectedMode))
                        mode = selectedMode;

                    else continue;

                    problem = CreateProblem(mode);
                    break;
                }
            }
            else
                problem = CreateProblem();

            while (true)
            {

                _view.RenderProblem(problem);

                command = _view.ReceiveUserInput();

                if (SymbolCommand.IsN(command))
                {
                    return AppState.MainMenu;
                }

                if (command != problem.Result)
                {
                    attempts++;
                    changeScore(ScoreType.TotalAnswers);
                    continue;
                }

                changeScore(ScoreType.TotalAnswers);
                changeScore(ScoreType.CorrectAnswers);

                _view.RenderProblemMenu(attempts);

                while (true)
                {
                    int userInputInt = _view.ReceiveIntUserInput();

                    switch (userInputInt)
                    {
                        case (int)NumberCommand.N1:
                            return math ? AppState.GenerateMathProblem : AppState.GenerateConversionProblem;
                        case (int)NumberCommand.N0:
                            return AppState.MainMenu;
                        default:
                            continue;
                    }
                }
            }
        }


        private AppState ShowSettings()
        {
            _view.RenderSettings(_settingsManager.Current);

            string command = _view.ReceiveUserInput();

            if (SymbolCommand.IsR(command))
                return ResetSettings();

            switch (_view.ReceiveIntUserInput(command))
            {
                case (int)NumberCommand.N1:
                    _view.Print(SettingsText.NewValuePrompt, newLine: false);
                    ChangeMaxDigits();
                    return AppState.Settings;
                case (int)NumberCommand.N2:
                    _view.Print(SettingsText.NewValuePrompt, newLine: false);
                    ChangeMaxOperations();
                    return AppState.Settings;
                case (int)NumberCommand.N3:
                    _settingsManager.ToggleFixedDigits();
                    return AppState.Settings;
                case (int)NumberCommand.N4:
                    _settingsManager.ToggleFixedOperations();
                    return AppState.Settings;
                case (int)NumberCommand.N0:
                    return AppState.MainMenu;
                default:
                    return AppState.Settings;
            }
        }

        private AppState ShowScores()
        {
            _view.RenderScores(_scoreManager.Current.MathematicScore, _scoreManager.Current.ConversionScore);

            string command = _view.ReceiveUserInput();

            if (SymbolCommand.IsR(command))
                return DeleteScores();

            else if (_view.ReceiveIntUserInput(command) == (int)NumberCommand.N0)
                return AppState.MainMenu;

            else return AppState.Scores;
        }

        private AppState Reset(Action reset, AppState appState)
        {
            _view.Print(CommonText.ReceiveConfirmationPrompt, newLine: false);

            if (SymbolCommand.IsY(_view.ReceiveUserInput()))
                reset();

            return appState;
        }

        private static ConversionMode GetRandomConversionMode()
        {
            ConversionMode[] modes = Enum.GetValues<ConversionMode>();

            return modes[Random.Shared.Next(modes.Length)];
        }

        private void ChangeMaxDigits() => _settingsManager.SetMaxDigits(_view.ReceiveIntUserInput());
        private void ChangeMaxOperations() => _settingsManager.SetMaxOperations(_view.ReceiveIntUserInput());

        private AppState DeleteScores() => Reset(_scoreManager.ResetScore, AppState.Scores);
        private AppState ResetSettings() => Reset(_settingsManager.ResetSettings, AppState.Settings);

        private MathProblem CreateProblem() => new(_settingsManager.Current);
        private ConversionProblem CreateProblem(ConversionMode mode) => new(_settingsManager.Current, mode);
    }
}