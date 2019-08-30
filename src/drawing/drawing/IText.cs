using System;

namespace PitneyBowes.Developer.Drawing
{
    public interface IText
    {
        string Text(object boundObject = null, IFormatProvider formatProvider = null);
        string BoundObjectPath { get; set; }
        string Format { get; set; }
        IShape Shape { get; set; }
        IText Duplicate();
    }
}
