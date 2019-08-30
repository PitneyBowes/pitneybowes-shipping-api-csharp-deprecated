using System.IO;
using SkiaSharp;

namespace PitneyBowes.Developer.Drawing
{
    public class SkiaSharpSvgRenderer : SkiaSharpRenderer
    {
        public override void Render(Stream stream, IDrawing shape, Page page, int pageNumber, object boundObject = null)
        {
            _page = page;
            _rootObject = boundObject;
            var pageRect = new SKRect(0, 0, page.ToPixels(page.Width), page.ToPixels(page.Height));
            var skStream = new SKManagedWStream(stream);
            var xmlStream = new SKXmlStreamWriter(skStream);
            _canvas = SKSvgCanvas.Create(pageRect, xmlStream);
            _canvas.SetMatrix(SKMatrix.MakeScale(_page.Resolution, _page.Resolution));
            Visit(shape);
            _canvas.Flush();
            _canvas.Dispose();
            skStream.Flush();
        }
        public override void Render(Stream stream, Stream bitmapStream, IDrawing shape, Page page, int pageNumber, object boundObject = null)
        {
            Render(stream, shape, page, pageNumber, boundObject);
        }

    }
}
