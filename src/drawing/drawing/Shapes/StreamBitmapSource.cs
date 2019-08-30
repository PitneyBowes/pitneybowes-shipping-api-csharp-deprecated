using System;
using System.IO;
using Newtonsoft.Json;

namespace PitneyBowes.Developer.Drawing
{
    public class StreamBitmapSource : IBitmapSource
    {
        [JsonIgnore]
        public Stream Stream { get; set; }

        public bool Dispose { get => false; set {} }

        public Stream Open()
        {
            return Stream;
        }
    }
}
