using System.IO;
using System.Text;
using SkiaSharp;
using ZXing;
using ZXing.Common;
using ZXing.OneD;
using ZXing.Datamatrix;
using ZXing.PDF417;
using System;

namespace PitneyBowes.Developer.Drawing
{
    public abstract class SkiaSharpRenderer : IRenderer
    {
        public static readonly float POINTS = 72.0f;
        public static readonly string FNC1 = '\u00f1'.ToString();
        protected Page _page;
        protected SKCanvas _canvas;
        public Rectangle Margin { get; set; }
        public IFormatProvider FormatProvider { get; set; }
        protected object _rootObject { get; set; }

        public abstract void Render(Stream stream, IDrawing shape, Page page, int pageNumber, object boundObject = null);
        public abstract void Render(Stream stream, Stream bitmapStream, IDrawing shape, Page page, int pageNumber, object boundObject = null);


        public virtual SKPaint TextBrush(IShape shape)
        {
            var brush = new SKPaint();
            brush.TextSize = shape.DefaultText.FontSize/POINTS;
            brush.Typeface = SKTypeface.FromFamilyName(shape.DefaultText.FontName, shape.DefaultText.Style.ToSK());
            brush.IsAntialias = true;
            brush.Color = shape.DefaultColors.Foreground.ToSK();
            brush.Style = SKPaintStyle.Fill;
            brush.IsStroke = false;
            brush.StrokeWidth = 3/POINTS;
            brush.TextAlign = SKTextAlign.Left;
            return brush;
        }

        public virtual SKPaint LineBrush(IShape shape, SKColor color)
        {
            var brush = new SKPaint();
            brush.Color = color;
            brush.Style = SKPaintStyle.Stroke;
            brush.StrokeWidth = shape.DefaultLine.Width/POINTS;
            return brush;
        }

        public virtual void Visit(IContainer shape)
        {
            foreach(var s in shape.Contents)
            {
                s.Accept(this);
            }
        }

        public virtual void Visit(ILine shape)
        {
            Point p0 = new Point(shape.Start);
            shape.PageCoordinate(ref p0);
            Point p1 = new Point(shape.End);
            shape.PageCoordinate(ref p1);

            using( var brush = LineBrush( shape, shape.DefaultColors.Foreground.ToSK()))
            {
                 _canvas.DrawLine(p0.X, p0.Y, p1.X, p1.Y, brush);
            }
        }

        public virtual void DrawRectangle(float x0, float y0, float x1, float y1, SKPaint brush)
        {
            var rect = new SKRect()
            {
                Left = x0,
                Right = x1,
                Top = y0,
                Bottom = y1
            };
            _canvas.DrawRect(rect, brush);
        }

        public virtual void Visit(IBox shape)
        {
            Point p0 = new Point(shape.TopLeft);
            shape.PageCoordinate(ref p0);
            Point p1 = new Point(shape.BottomRight);
            shape.PageCoordinate(ref p1);
            var rect = new SKRect()
            {
                Left = p0.X,
                Right = p1.X,
                Top = p0.Y,
                Bottom = p1.Y
            };
            using (var brush = LineBrush(shape, shape.DefaultColors.Foreground.ToSK()))
            {
                _canvas.DrawRect(rect, brush);
            }
        }

        public virtual void Visit(ITextBox shape)
        {
            using (var brush = TextBrush(shape))
            {
                Point p0 = new Point(shape.Anchor);
                shape.PageCoordinate(ref p0);
                float lineHeight = brush.TextSize * 1.2f;
                var lines = shape.Text.Text(_rootObject, FormatProvider).Split(System.Environment.NewLine.ToCharArray());
                var height = lines.Length * lineHeight;
                var y = p0.Y + shape.Margin/POINTS;
                foreach (var line in lines)
                {
                    y += lineHeight;
                    float x = p0.X;
                    float w = brush.MeasureText(line);
                    switch(shape.HorizontalAlignment)
                    {
                        case HorizontalAlignment.Left:
                            x = p0.X + shape.Margin / POINTS;
                            break;
                        case HorizontalAlignment.Center:
                            x = p0.X + (shape.Width + shape.Margin / POINTS - w)/2;
                            break;
                        case HorizontalAlignment.Right:
                            x = p0.X + shape.Width - shape.Margin / POINTS - w;
                            break;
                    }
                    _canvas.DrawText(line, x, y, brush);
                }
            }
            if (shape.DrawBoundingBox)
            {
                using (var brush = LineBrush(shape, shape.DefaultColors.Foreground.ToSK()))
                {
                    Point p0 = new Point(shape.BoundingBox.TopLeft);
                    shape.PageCoordinate(ref p0);
                    Point p1 = new Point(shape.BoundingBox.BottomRight);
                    shape.PageCoordinate(ref p1);
                    DrawRectangle(p0.X, p0.Y, p1.X, p1.Y, brush);
                }
            }
        }

        public virtual void Visit(IBarcode shape)
        {
            Point p0 = new Point(shape.Anchor);
            shape.PageCoordinate(ref p0);
            Point p1 = new Point(shape.BoundingBox.BottomRight);
            shape.PageCoordinate(ref p1);

            var s = Encoding.UTF8.GetString(shape.Data.BarcodeData(_rootObject)).Replace("{FNC1}", FNC1);

            Writer barcodeWriter;
            BarcodeFormat barcodeFormat;
            BitMatrix bitMatrix = null;
            float pixelHeight = 1;
            float pixelWidth = 1;
            switch (shape.Symbology)
            {
                case Symbology.Code128:
                    barcodeWriter = new Code128Writer();
                    barcodeFormat = BarcodeFormat.CODE_128;
                    bitMatrix = barcodeWriter.encode(s, barcodeFormat, (int)(_page.Resolution * shape.MaxWidth), 1, null);
                    pixelHeight = (int)(_page.Resolution * shape.Height);
                    break;
                case Symbology.Code128C:
                    barcodeWriter = new Code128Writer();
                    barcodeFormat = BarcodeFormat.CODE_128;
                    float pixels = (int)(shape.MaxWidth / (5.5 * s.Length + 53) * _page.Resolution);
                    float width = (float)(pixels / _page.Resolution * (5.5 * s.Length + 53));
                    p0.X += (shape.MaxWidth - width - pixels/_page.Resolution) / 2;
                    p1.X -= (shape.MaxWidth - width - pixels /_page.Resolution) / 2;
                    p0.X = ((int)(p0.X * _page.Resolution)) / _page.Resolution;
                    p1.X = ((int)(p1.X * _page.Resolution)) / _page.Resolution;
                    int bits = (int)(5.5 * s.Length + 53) -20; // 10 x-dims quiet zone each side of barcode
                    bitMatrix = barcodeWriter.encode(s, barcodeFormat, bits, 1, null);
                    pixelHeight = (int)(_page.Resolution * shape.Height);
                    pixelWidth = pixels;
                    break;
                case Symbology.DataMatrix:
                    barcodeWriter = new DataMatrixWriter();
                    barcodeFormat = BarcodeFormat.DATA_MATRIX;
                    bitMatrix = barcodeWriter.encode(s, barcodeFormat, (int)(_page.Resolution * shape.MaxWidth), (int)(_page.Resolution * shape.Height), null);
                    break;
                case Symbology.PDF417:
                    barcodeWriter = new PDF417Writer();
                    barcodeFormat = BarcodeFormat.PDF_417;
                    bitMatrix = barcodeWriter.encode(s, barcodeFormat, (int)(_page.Resolution * shape.MaxWidth), (int)(_page.Resolution * shape.Height), null);
                    break;
            }
            using (var brush = new SKPaint())
            {
                brush.TextSize = shape.DefaultText.FontSize / POINTS;
                brush.Typeface = SKTypeface.FromFamilyName(shape.DefaultText.FontName, shape.DefaultText.Style.ToSK());
                brush.IsAntialias = true;
                brush.Color = shape.DefaultColors.Foreground.ToSK();
                brush.Style = SKPaintStyle.Fill;
                brush.IsStroke = false;
                brush.StrokeWidth = 1.0f /_page.Resolution;
                brush.TextAlign = SKTextAlign.Left;

                for (var x = 0; x < bitMatrix.Width; x++)
                {
                    for (var y = 0; y < bitMatrix.Height; y++)
                    {
                        if (bitMatrix[x, y])
                        {
                            int n;
                            for (n = 0; bitMatrix[x + n + 1, y]; n++) {}
                            DrawRectangle(p0.X + x * pixelWidth / _page.Resolution, p0.Y + y / _page.Resolution, p0.X + (x + n+1)*pixelWidth / _page.Resolution, p0.Y + (y + 1) * pixelHeight / _page.Resolution, brush);
                            x = x + n;
                        }
                    }
                }
            }
        }

        public virtual void Visit(IBitmap shape)
        {
            Point p0 = new Point(shape.BoundingBox.TopLeft);
            shape.PageCoordinate(ref p0);
            Point p1 = new Point(shape.BoundingBox.BottomRight);
            shape.PageCoordinate(ref p1);
            var stream = shape.BitmapSource.Open();
            try
            {
                using (var bitmap = SKBitmap.Decode(stream))
                {
                    if (shape.FitToBoundingBox)
                    {
                        var dest = new SKRect() { Left = p0.X, Top = p0.Y, Right = p1.X, Bottom = p1.Y };
                        _canvas.DrawBitmap(bitmap, dest);
                    }
                    else
                    {
                        var dest = new SKRect() { Left = p0.X, Top = p0.Y, Right = p0.X + (((float)bitmap.Width)/_page.Resolution), Bottom = p0.Y + ((float)(bitmap.Height)/_page.Resolution) };
                        _canvas.DrawBitmap(bitmap, dest);
                    }
                }
            }
            finally
            {
                if (shape.BitmapSource.Dispose)
                    stream.Dispose();
            }
        }
        public virtual void Visit(IChildLabel shape)
        {
            shape.ApplyChanges();
            shape.Shape.Accept(this);
        }
    }
}
