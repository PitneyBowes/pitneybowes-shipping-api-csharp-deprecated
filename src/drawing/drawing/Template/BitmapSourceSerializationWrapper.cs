using System.IO;
using Newtonsoft.Json;

namespace PitneyBowes.Developer.Drawing
{
    public class BitmapSourceSerializationWrapper : IBitmapSource
    {
        public BitmapSourceSerializationWrapper(IBitmapSource wrapped)
        {
            Wrapped = wrapped;
        }

        [JsonIgnore]
        public IBitmapSource Wrapped { get; set; }

        public bool ShouldSerializeStreamBitmapSource() => StreamBitmapSource != null;
        public StreamBitmapSource StreamBitmapSource { get => Wrapped as StreamBitmapSource; }

        public bool ShouldSerializeFileBitmapSource() => FileBitmapSource != null;
        public FileBitmapSource FileBitmapSource { get => Wrapped as FileBitmapSource; }

        [JsonIgnore]
        public bool Dispose { get => Wrapped.Dispose; set => Wrapped.Dispose = value; }

        public Stream Open()
        {
            return Wrapped.Open();
        }
    }
}
