namespace LiftBuddy.Models
{
    public class StrengthExercise : Exercise
    {
        public int Sets { get; set; }
        public int Repetitions { get; set; }
        public int Weight { get; set; }
    }
}