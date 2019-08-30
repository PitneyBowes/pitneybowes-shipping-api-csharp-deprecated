using Newtonsoft.Json;

namespace PitneyBowes.Developer.Drawing
{
    public class BarcodeDataSerializationWrapper : IBarcodeData
    {

        public BarcodeDataSerializationWrapper(IBarcodeData wrapped)
        {
            Wrapped = wrapped;
        }

        [JsonIgnore]
        public IBarcodeData Wrapped { get; set; }

        public bool ShouldSerializeStaticBarcodeData() => StaticBarcodeData != null;
        public StaticBarcodeData StaticBarcodeData { get => Wrapped as StaticBarcodeData; }

        public bool ShouldSerializeBoundBarcodeData() => BoundBarcodeData != null;
        public BoundBarcodeData BoundBarcodeData { get => Wrapped as BoundBarcodeData; }

        [JsonIgnore]
        public IShape Shape { get => Wrapped.Shape; set => Wrapped.Shape = value; }

        public string BoundObjectPath { get => Wrapped.BoundObjectPath; set { Wrapped.BoundObjectPath = value;  } }

        public byte[] BarcodeData(object boundObject) => Wrapped.BarcodeData(boundObject);

        public IBarcodeData Duplicate()
        {
            return new BarcodeDataSerializationWrapper(Wrapped.Duplicate());
        }
    }
}
