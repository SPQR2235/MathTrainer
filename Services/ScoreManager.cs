using MathTrainer.Models.Scores;

namespace MathTrainer.Services
{
    class ScoreManager : JsonManager<Statistics>
    {
        private Statistics _stats;

        public ScoreManager()
            : base(Path.Combine(DataFolder, "score.json"))
        {
            _stats = Load();

            AppDomain.CurrentDomain.ProcessExit += (_, _) => Save(_stats);
            Console.CancelKeyPress += (_, _) => Save(_stats);
        }

        public override Statistics CreateDefault()
        {
            return new Statistics();
        }

        public void ChangeMathScore(
            ScoreType type,
            int count = 1,
            bool add = true) => ChangeScore(true, type, count, add);

        public void ChangeConversionScore(
            ScoreType type,
            int count = 1,
            bool add = true) => ChangeScore(false, type, count, add);

        private void ChangeScore(bool math, ScoreType type, int count = 1, bool add = true)
        {
            int value = add ? count : -count;

            var obj = math ? _stats.MathematicScore : _stats.ConversionScore;

                switch (type)
                {
                    case ScoreType.CorrectAnswers:
                        obj.CorrectAnswers = Math.Max(0, obj.CorrectAnswers + value);
                        break;
                    case ScoreType.TotalAnswers:
                        obj.TotalAnswers = Math.Max(0, obj.TotalAnswers + value);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type));
                };

                obj.Accuracy = obj.TotalAnswers > 0
                ? (double)obj.CorrectAnswers / obj.TotalAnswers
                : 0;

            Save(_stats);
        }
        
        public void ResetScore()
        {
            _stats = CreateDefault();
            Save(_stats);
        }

        public Statistics Current => new()
        {
            MathematicScore = _stats.MathematicScore,
            ConversionScore = _stats.ConversionScore
        };
    }
}
