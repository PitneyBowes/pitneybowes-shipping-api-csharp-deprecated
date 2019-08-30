using System;
namespace PitneyBowes.Developer.Drawing
{
    public class MoveWithinContainer : IConstraint
    {
        public bool CheckConstraint(IShape shape, Point movement )
        {
            if (shape.Container != null)
            {
                var r = shape.BoundingBox.Duplicate();
                r.MoveRelative(movement.X, movement.Y);
                if (!shape.Container.BoundingBox.Contains(r)) return false;
            }
            return true;
        }
    }
}
