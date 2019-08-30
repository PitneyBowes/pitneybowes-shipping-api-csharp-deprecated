using SkiaSharp;


namespace PitneyBowes.Developer.Drawing
{
    public static class SkiaExtensions
    {
        public static SKColor ToSK(this RGBColor rgb) => new SKColor(rgb.Red, rgb.Green, rgb.Blue);
        public static SKTypefaceStyle ToSK(this FontStyle s)
        {
            switch (s)
            {
                default:
                case FontStyle.Normal: return SKTypefaceStyle.Normal;
                case FontStyle.Bold: return SKTypefaceStyle.Bold;
                case FontStyle.Itallic: return SKTypefaceStyle.Italic;
                case FontStyle.BoldItallic: return SKTypefaceStyle.BoldItalic;
            }
        }
    }
}
