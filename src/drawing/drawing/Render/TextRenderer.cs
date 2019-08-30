using System;
using System.IO;

namespace PitneyBowes.Developer.Drawing
{
    public class TextRenderer : IRenderer
    {
        private int _indent;
        public TextRenderer()
        {
        }

        public Rectangle Margin { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IFormatProvider FormatProvider { get; set; }

        public void Render(Stream stream, IDrawing shape, Page page, int pageNumber, object boundObject = null)
        {
            Console.WriteLine("Rendering label: {0}", page);
            Visit((IContainer)shape);
        }

        public void Render(Stream stream, Stream bitmapStream, IDrawing shape, Page page, int pageNumber, object boundObject = null)
        {
            Render( stream, shape, page, pageNumber, boundObject);
        }

        public void Visit(IContainer shape)
        {
            Console.WriteLine("{0} {1}", new string(' ', _indent), shape);
            _indent += 4;
            shape.Contents.ForEach(s=>s.Accept(this));
            _indent -= 4;
        }

        public void Visit(ILine shape)
        {
            Console.WriteLine(new string(' ', _indent) + shape);
        }

        public void Visit(IBox shape)
        {
            Console.WriteLine(new string(' ', _indent) + shape);
        }

        public void Visit(ITextBox shape)
        {
            Console.WriteLine(new string(' ', _indent) + shape);
        }

        public void Visit(IBarcode shape)
        {
            Console.WriteLine(new string(' ', _indent) + shape);
        }

        public void Visit(IBitmap shape)
        {
            Console.WriteLine(new string(' ', _indent) + shape);
        }
        public void Visit(IChildLabel shape)
        {
            shape.ApplyChanges();
            shape.Shape.Accept(this);
        }
    }
}
