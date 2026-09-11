using System.Text;
using ConversionMode = MathTrainer.Models.Conversion.ConversionMode;

namespace MathTrainer.Views.Resources
{
    class ConversionText
    {
        public static string GetModeName(ConversionMode mode) =>
            mode.ToString()
                .Replace("To", " to ");

        private const string SelectModeHeadlinePrompt = "Select Mode:";

        public static string SelectModeMenuPrompt
        {
            get
            {
                StringBuilder builder = new();

                builder.AppendLine();
                builder.AppendLine(SelectModeHeadlinePrompt);
                builder.AppendLine();

                foreach (ConversionMode mode in Enum.GetValues<ConversionMode>())
                {
                    builder.AppendLine($"{(int)mode} - {GetModeName(mode)}");
                }


                builder.AppendLine();
                builder.AppendLine("r - Random mode");
                builder.AppendLine(CommonText.ExitToMainMenuPrompt);
                builder.AppendLine();
                builder.Append(CommonText.UserChoicePrompt);

                return builder.ToString();
            }
        }
    }
}
