using System.IO;
namespace PitneyBowes.Developer.Drawing
{
    public interface IBitmap : IShape
    {
        bool FitToBoundingBox { get; set; }
        IBitmapSource BitmapSource { get; set; }
    }
}
