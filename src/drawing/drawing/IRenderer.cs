using System;
using System.IO;


namespace PitneyBowes.Developer.Drawing
{
    public interface IRenderer : IVisitor
    {
        Rectangle Margin { get; set; }
        void Render(Stream stream, IDrawing shape, Page page, int pageNumber, object boundObject = null);
        void Render(Stream stream, Stream bitmapStream, IDrawing shape, Page page, int pageNumber, object bountObject = null);
        IFormatProvider FormatProvider { get; set; }
    }
}
