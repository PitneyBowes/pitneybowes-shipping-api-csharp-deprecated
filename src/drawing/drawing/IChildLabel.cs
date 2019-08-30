using System;
using System.Collections.Generic;

namespace PitneyBowes.Developer.Drawing
{
    public interface IChildLabel
    {
        IShape Shape { get; set; }
        void ApplyChanges();
        void AddChange(Guid id, IShapeChange change);
    }
}
