using System;
using System.Collections.Generic;

namespace PitneyBowes.Developer.Drawing
{
    public interface IShape
    {
        Guid Id { get; set; }
        string Name { get; set; }
        IShape Container { get; set; }
        IShape Duplicate();
        void PageCoordinate(ref Point p);
        void Accept(IVisitor visitor);
        Rectangle BoundingBox { get; }
        ColorProperties DefaultColors { get; set; }
        LineProperties DefaultLine { get; set; }
        TextProperties DefaultText { get; set; }
        void MoveRelative(float deltaX, float deltaY);
        //IShape Duplicate();
        bool DrawBoundingBox { get; set; }
        List<IConstraint> Constraints { get; set; }
    }
}
