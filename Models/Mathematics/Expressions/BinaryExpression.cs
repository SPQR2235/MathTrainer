using MathTrainer.Models.Mathematics;

namespace MathTrainer.Models.Mathematics.Expressions
{
    class BinaryExpression(Expression left, Expression right, Operation operation) : Expression
    {
        private readonly Expression _left = left;
        private readonly Expression _right = right;
        private readonly Operation _operation = operation;
        public Expression Left => _left;
        public Expression Right => _right;
        public Operation Operation => _operation;

        public override int Calculate()
        {
            if (!TryCalculate(out int result))
                throw new InvalidOperationException(
                    "Invalid expression");

            return result;
        }

        public bool TryCalculate(out int result)
        {
            result = 0;

            if (!TryGetValue(_left, out int left))
                return false;

            if (!TryGetValue(_right, out int right))
                return false;

            try
            {
                result = _operation switch
                {
                    Operation.Add =>
                        checked(left + right),

                    Operation.Subtract =>
                        checked(left - right),

                    Operation.Multiply =>
                        checked(left * right),

                    Operation.Divide when right != 0 &&
                                          left % right == 0 &&
                                          left / right != 0 =>
                        left / right,

                    _ => 0
                };

                return result >= 0;
            }
            catch (OverflowException)
            {
                return false;
            }
        }

        private bool TryGetValue(Expression expression, out int value)
        {
            if (expression is BinaryExpression binary)
                return binary.TryCalculate(out value);

            try
            {
                value = expression.Calculate();
                return value >= 0;
            }
            catch
            {
                value = 0;
                return false;
            }
        }

        public override string ToString()
        {
            string left = FormatChild(_left, false);
            string right = FormatChild(_right, true);

            return _operation switch
            {
                Operation.Add =>
                    $"{left} + {right}",

                Operation.Subtract =>
                    $"{left} - {right}",

                Operation.Multiply =>
                    $"{left} * {right}",

                Operation.Divide =>
                    $"{left} / {right}",

                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private string FormatChild(Expression child, bool isRight)
        {
            if (child is not BinaryExpression binary)
                return child.ToString();


            bool brackets = binary.Priority < Priority;


            if (binary.Priority == Priority)
            {
                if (_operation == Operation.Subtract && isRight)
                    brackets = true;

                if (_operation == Operation.Divide && isRight)
                    brackets = true;
            }


            string value = binary.ToString();

            return brackets
                ? $"({value})"
                : value;
        }

        private int Priority => _operation switch
        {
            Operation.Add => 1,
            Operation.Subtract => 1,
            Operation.Multiply => 2,
            Operation.Divide => 2,
            _ => 0
        };

        public override string DebugView()
        {
            return _operation switch
            {
                Operation.Add =>
                    $"({_left.DebugView()} Add {_right.DebugView()})",

                Operation.Subtract =>
                    $"({_left.DebugView()} Subtract {_right.DebugView()})",

                Operation.Multiply =>
                    $"({_left.DebugView()} Multiply {_right.DebugView()})",

                Operation.Divide =>
                    $"({_left.DebugView()} Divide {_right.DebugView()})",

                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}