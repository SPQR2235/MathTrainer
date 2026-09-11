namespace MathTrainer.Models.Mathematics.Expressions
{
    public abstract class Expression
    {
        public abstract int Calculate();
        public abstract override string ToString();
        public abstract string DebugView();
    }
}