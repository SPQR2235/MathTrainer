namespace MathTrainer.Models.Scores
{
    public class Score
    {
        public int CorrectAnswers { get; set; }
        public int TotalAnswers { get; set; }
        public int IncorrectAnswers => TotalAnswers - CorrectAnswers;
        public double Accuracy { get; set; }
    }
}
