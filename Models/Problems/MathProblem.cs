using MathTrainer.Models.Mathematics;
using MathTrainer.Models.Mathematics.Expressions;
using MathTrainer.Models.Settings;

namespace MathTrainer.Models.Problem
{
    class MathProblem : Problem
    {
        protected override Setting Setting { get; set; }
        private readonly Expression _expression;

        public override string Headline => "Your Problem: ";
        public override string Value { get; protected set; } 
        public override string Result { get; protected set; }

        public MathProblem(Setting settings)
        {
            Setting = settings;

            do
            {
                _expression = GenerateProblem();

            } while (!IsValidExpression(_expression));

            Value = _expression.ToString();
            Result = Convert.ToString(_expression.Calculate());
        }

        private bool IsValidExpression(Expression expression)
        {
            if (expression is NumberExpression)
                return true;


            if (expression is not BinaryExpression binary)
                return false;


            if (!binary.TryCalculate(out int result))
                return false;


            if (result > GetMaxNumber())
                return false;

            if (result <= 0)
                return false;


            return IsValidExpression(binary.Left)
                && IsValidExpression(binary.Right);
        }

        private Expression GenerateProblem()
        {
            int parts = GetPartsCount();

            List<Expression> expressions = [];
            List<Operation> operations = [];

            int advancedStreak = 0;

            for (int i = 0; i < parts; i++)
            {
                bool additiveOnly = Random.Shared.Next(2) == 1;

                Operation partMode = additiveOnly
                    ? GetRandomMode(true)
                    : GetRandomMode();

                expressions.Add(GeneratePartOfProblem(partMode));

                if (i > 0)
                    operations.Add(GetNextOperatorMode(ref advancedStreak));
            }

            return BuildExpression(expressions, operations, 0, expressions.Count - 1);
        }

        private Expression BuildExpression(List<Expression> expressions, List<Operation> operations, int left, int right)
        {
            if (left == right)
                return expressions[left];

            int middle = (left + right) / 2;

            Expression leftExpression =
                BuildExpression(expressions, operations, left, middle);

            Expression rightExpression =
                BuildExpression(expressions, operations, middle + 1, right);

            Operation operation = operations[middle];

            return new BinaryExpression(
                leftExpression,
                rightExpression,
                operation);
        }

        private Expression GeneratePartOfProblem(Operation mode)
        {
            int digitsInSecondNumber = GetDigitsCount();

            int firstNumber;
            int secondNumber;

            if (mode == Operation.Divide)
            {
                int maxNumber = GetMaxNumber();

                int resultDigits = GetDigitsCount();

                int result = GetRandomNumber(resultDigits);

                int maxDivisor = maxNumber / result;

                if (maxDivisor < 1)
                {
                    result = 1;
                    maxDivisor = maxNumber;
                }

                int divisorDigits = GetDigitsCount();

                int minDivisor = divisorDigits == 1
                    ? 1
                    : (int)Math.Pow(10, divisorDigits - 1);

                int divisor;

                if (maxDivisor >= minDivisor)
                    divisor = Random.Shared.Next(minDivisor, maxDivisor + 1);
                else
                    divisor = Random.Shared.Next(1, maxDivisor + 1);

                firstNumber = divisor * result;
                secondNumber = divisor;
            }
            else
            {
                int digitsInFirstNumber = GetDigitsCount();

                firstNumber = GetRandomNumber(digitsInFirstNumber);
                secondNumber = GetRandomNumber(digitsInSecondNumber);

                if (mode == Operation.Subtract || mode == Operation.Multiply || mode == Operation.Add)
                {
                    if (mode == Operation.Subtract && firstNumber == secondNumber)
                    {
                        if (firstNumber > 1)
                            secondNumber--;
                        else
                            firstNumber++;
                    }

                    if (secondNumber > firstNumber)
                        (firstNumber, secondNumber) = (secondNumber, firstNumber);
                }
                if (mode == Operation.Add)
                {
                    int maxNumber = GetMaxNumber();

                    while (firstNumber + secondNumber > maxNumber && secondNumber > 1)
                    {
                        secondNumber--;
                    }
                }
            }

            return new BinaryExpression(
                new NumberExpression(firstNumber),
                new NumberExpression(secondNumber),
                mode);
        }

        private int GetDigitsCount() => Setting.FixedDigits
            ? Setting.MaxDigits
            : Random.Shared.Next(1, Setting.MaxDigits + 1);

        private int GetPartsCount() => Setting.FixedOperations
            ? Math.Max(2, Setting.MaxOperations)
            : Random.Shared.Next(2, Math.Max(2, Setting.MaxOperations) + 1);

        private Operation GetNextOperatorMode(ref int advancedStreak)
        {
            int chance = 1 << (advancedStreak + 1);

            bool advanced = Random.Shared.Next(chance) == 0;

            if (advanced)
            {
                advancedStreak++;

                return Random.Shared.Next(2) == 0
                    ? Operation.Multiply
                    : Operation.Divide;
            }

            advancedStreak = 0;

            return Random.Shared.Next(2) == 0
                ? Operation.Add
                : Operation.Subtract;
        }

        private Operation GetRandomMode(bool additiveOperations = false)
        {
            if (additiveOperations)
                return (Operation)Random.Shared.Next(
                    (int)Operation.Add,
                    (int)Operation.Subtract + 1);

            return (Operation)Random.Shared.Next(1, Enum.GetValues<Operation>().Length + 1);
        }



        private int GetRandomNumber(int digits)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(digits, 1);

            int min = digits == 1
                ? 1
                : (int)Math.Pow(10, digits - 1);

            int max = (int)Math.Pow(10, digits) - 1;

            return Random.Shared.Next(min, max + 1);
        }

        private int GetMaxNumber() => (int)Math.Pow(10, Setting.MaxDigits) - 1;
    }
}
