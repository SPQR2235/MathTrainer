using MathTrainer.Models.Conversion;
using MathTrainer.Models.Settings;

namespace MathTrainer.Models.Problem
{
    class ConversionProblem : Problem
    {
        protected override Setting Setting { get; set; }
        private readonly ConversionMode _mode;

        private readonly int _sourceValue;


        public override string Headline => $"Your {_mode.ToString().Replace("To", " to ")} problem:";
        public override string Value { get; protected set;  }
        public override string Result { get; protected set;  }

        public ConversionProblem(Setting settings, ConversionMode mode)
        {
            Setting = settings;
            _mode = mode;

            _sourceValue = GenerateProblem();
            Value = Convert(_sourceValue, GetSourceSystem());
            Result = Convert(_sourceValue, GetTargetSystem());
        }

        private int GenerateProblem() => Random.Shared.Next(
                0,
                GetMaxNumber(GetDigitsCount()) + 1);

        private static string Convert(int value, NumberSystem numberSystem)
        {
            return numberSystem switch
            {
                NumberSystem.Decimal =>
                    value.ToString(),

                NumberSystem.Hexadecimal =>
                    System.Convert.ToString(value, 16)
                        .ToUpper(),

                NumberSystem.Octal =>
                    System.Convert.ToString(value, 8),

                NumberSystem.Binary =>
                    System.Convert.ToString(value, 2),

                _ => throw new ArgumentOutOfRangeException(nameof(numberSystem))
            };
        }

        private NumberSystem GetSourceSystem()
        {
            return _mode switch
            {
                ConversionMode.DecimalToHexadecimal => NumberSystem.Decimal,
                ConversionMode.DecimalToOctal => NumberSystem.Decimal,
                ConversionMode.DecimalToBinary => NumberSystem.Decimal,

                ConversionMode.HexadecimalToDecimal => NumberSystem.Hexadecimal,
                ConversionMode.HexadecimalToOctal => NumberSystem.Hexadecimal,
                ConversionMode.HexadecimalToBinary => NumberSystem.Hexadecimal,

                ConversionMode.OctalToDecimal => NumberSystem.Octal,
                ConversionMode.OctalToHexadecimal => NumberSystem.Octal,
                ConversionMode.OctalToBinary => NumberSystem.Octal,

                ConversionMode.BinaryToDecimal => NumberSystem.Binary,
                ConversionMode.BinaryToHexadecimal => NumberSystem.Binary,
                ConversionMode.BinaryToOctal => NumberSystem.Binary,

                _ => throw new ArgumentOutOfRangeException(nameof(_mode))
            };
        }

        private NumberSystem GetTargetSystem()
        {
            return _mode switch
            {
                ConversionMode.DecimalToHexadecimal => NumberSystem.Hexadecimal,
                ConversionMode.DecimalToOctal => NumberSystem.Octal,
                ConversionMode.DecimalToBinary => NumberSystem.Binary,

                ConversionMode.HexadecimalToDecimal => NumberSystem.Decimal,
                ConversionMode.HexadecimalToOctal => NumberSystem.Octal,
                ConversionMode.HexadecimalToBinary => NumberSystem.Binary,

                ConversionMode.OctalToDecimal => NumberSystem.Decimal,
                ConversionMode.OctalToHexadecimal => NumberSystem.Hexadecimal,
                ConversionMode.OctalToBinary => NumberSystem.Binary,

                ConversionMode.BinaryToDecimal => NumberSystem.Decimal,
                ConversionMode.BinaryToHexadecimal => NumberSystem.Hexadecimal,
                ConversionMode.BinaryToOctal => NumberSystem.Octal,

                _ => throw new ArgumentOutOfRangeException(nameof(_mode))
            };
        }

        private int GetDigitsCount() => Setting.FixedDigits
            ? Setting.MaxDigits
            : Random.Shared.Next(1, Setting.MaxDigits + 1);

        private static int GetMaxNumber(int digits) =>
            (int)Math.Pow(10, digits) - 1;
    }
}
