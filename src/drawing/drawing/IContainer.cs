using System.Collections.Generic;


namespace PitneyBowes.Developer.Drawing
{
    public interface IContainer : IShape
    {
        Point Anchor { get; set; }
        List<IShape> Contents { get; set; }
    }
}
