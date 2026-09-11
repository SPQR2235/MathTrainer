using MathTrainer.Models.Settings;

namespace MathTrainer.Models.Problem
{
    abstract class Problem
    {
        protected abstract Setting Setting { get; set; }

        public abstract string Headline { get; }
        public abstract string Value { get; protected set; }
        public abstract string Result { get; protected set; }
    }
}
