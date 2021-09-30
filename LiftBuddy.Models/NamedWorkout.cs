namespace LiftBuddy.Models
{
    public class NamedWorkout
    {
        public string Name { get; set; }
        
        public NamedWorkout() { }
        public NamedWorkout(string name)
        {
            Name = name;
        }
    }
}