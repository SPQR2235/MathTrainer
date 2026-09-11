namespace MathTrainer.Models.Mathematics.Expressions
{
    class NumberExpression(int value) : Expression
    {
        private readonly int _value = value;

        public override int Calculate()
        {
            return _value;
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public override string DebugView()
        {
            return _value.ToString();
        }
    }
}