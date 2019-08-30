using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PitneyBowes.Developer.Drawing
{
    public enum MovementRestriction
    {
        None,
        Vertical,
        Horizontal
    }

    public class RestrictedMovement : IConstraint
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public MovementRestriction MovementRestriction {get;set;}
        public bool CheckConstraint(Point movement)
        {
            if (MovementRestriction == MovementRestriction.None) return false;
            if (MovementRestriction == MovementRestriction.Vertical && movement.X != 0) return false;
            if (MovementRestriction == MovementRestriction.Horizontal && movement.Y != 0) return false;
            return true;
        }
    }
}
