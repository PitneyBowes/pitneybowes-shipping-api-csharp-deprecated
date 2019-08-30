using System;
using System.IO;

namespace PitneyBowes.Developer.Drawing
{
    public class FileBitmapSource : IBitmapSource
    {
        public string Filepath { get; set; }
        public string FileName { get; set; }
        public bool Dispose { get => true; set {} }

        public Stream Open()
        {
            return File.OpenRead(string.Format("{0}{1}{2}", Filepath, Path.DirectorySeparatorChar, FileName));
        }
    }
}
