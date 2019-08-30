using System.IO;
using System.Text;
using SkiaSharp;
using ZXing;
using ZXing.Common;
using ZXing.OneD;
using ZXing.Datamatrix;
using ZXing.PDF417;


namespace PitneyBowes.Developer.Drawing
{
    public class SkiaSharpBitmapRenderer : SkiaSharpRenderer
    {

        public override void Render(Stream stream, IDrawing shape, Page page, int pageNumber, object boundObject = null)
        {
            _page = page;
            _rootObject = boundObject;
            using (var surface = SKSurface.Create(page.ToPixels(page.Width), page.ToPixels(page.Height), SKImageInfo.PlatformColorType, SKAlphaType.Premul))
            {
                _canvas = surface.Canvas;
                _canvas.SetMatrix(SKMatrix.MakeScale(_page.Resolution, _page.Resolution));
                Visit(shape);
                _canvas.Flush();
                var pngEncodedFile = surface.Snapshot().Encode();
                pngEncodedFile.AsStream().CopyTo(stream);
            }
        }
        public override void Render(Stream stream, Stream bitmapStream, IDrawing shape, Page page, int pageNumber, object boundObject = null)
        {
            _rootObject = boundObject;
            _page = page;
            var bitmap = SKBitmap.Decode(bitmapStream);
            _canvas = new SKCanvas(bitmap);
            _canvas.SetMatrix(SKMatrix.MakeScale(_page.Resolution, _page.Resolution));
            Visit(shape);
            _canvas.Flush();
            var mstream = new SKManagedWStream(stream);
            bitmap.Encode(mstream, SKEncodedImageFormat.Png, 1);
        }
    }
}
