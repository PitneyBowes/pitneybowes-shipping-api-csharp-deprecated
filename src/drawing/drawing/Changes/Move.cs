namespace PitneyBowes.Developer.Drawing
{
    public class Move : IShapeChange
    {
        Point Delta { get; set; }
        public bool CheckConstraints(IShape shape)
        {
            if (shape.Constraints == null) return true;
            foreach (var constraint in shape.Constraints)
            {
                if (constraint.GetType() == typeof(RestrictedMovement))
                {
                    var c = constraint as RestrictedMovement;
                    if (!c.CheckConstraint(Delta)) return false;
                }
                else if (constraint.GetType() == typeof(MoveWithinContainer))
                {
                    if (shape.Container != null)
                    {
                        var c = constraint as MoveWithinContainer;
                        if (!c.CheckConstraint(shape, Delta)) return false;
                    }
                }
            }
            return true;

        }

        public void Apply(IShape shape)
        {
            if (!CheckConstraints(shape)) return;
            shape.MoveRelative(Delta.X, Delta.Y);
        }
    }
}
