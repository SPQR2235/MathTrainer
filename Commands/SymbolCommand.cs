namespace MathTrainer.Commands
{
    static class SymbolCommand
    {
        public const string Y = "yн";
        public const string N = "nт";
        public const string R = "rк";

        public static bool IsY(string input) =>
            input.Length == 1 && Y.Contains(input);

        public static bool IsN(string input) =>
            input.Length == 1 && N.Contains(input);

        public static bool IsR(string input) =>
            input.Length == 1 && R.Contains(input);
    }
}
