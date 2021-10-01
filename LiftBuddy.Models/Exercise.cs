using MongoDB.Bson.Serialization.Attributes;

namespace LiftBuddy.Models
{
    [BsonKnownTypes(typeof(StrengthExercise), typeof(CardioExercise))]
    public abstract class Exercise
    {
        public string Name { get; set; }

        public Exercise Clone()
        {
            return (Exercise)MemberwiseClone();
        }
    }
}