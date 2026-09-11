using MathTrainer.Services;
using MathTrainer.Views;
using MathTrainer.Controllers;

namespace MathTrainer
{
    class Program
    {
        static void Main()
        {
            SettingsManager settingsManager = new();
            ScoreManager scoreManager = new();
            ConsoleView consoleView = new();
            Controller controller = new(consoleView, scoreManager, settingsManager);

            controller.Run();
        }
    }
}
    