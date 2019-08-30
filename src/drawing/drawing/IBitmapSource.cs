using System;
using System.IO;

namespace PitneyBowes.Developer.Drawing
{
    public interface IBitmapSource
    {
        bool Dispose { get; set; }
        Stream Open();
    }
}
