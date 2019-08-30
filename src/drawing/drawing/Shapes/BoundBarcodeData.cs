using System;
using Newtonsoft.Json;

namespace PitneyBowes.Developer.Drawing
{
    public class BoundBarcodeData : IBarcodeData
    {
        public IBarcodeData Duplicate() => new BoundBarcodeData() { BoundObjectPath = this.BoundObjectPath };
        public byte[] BarcodeData( object boundObject)
        {

            if (boundObject == null) return new byte[] {};
            boundObject = Binding.BoundObject(boundObject, BoundObjectPath);
            if (boundObject == null) return new byte[] { };
            var rtn = boundObject as byte[];
            if (rtn == null) return new byte[] { };
            return rtn;
        }

        public string BoundObjectPath { get; set; }

        [JsonIgnore]
        public IShape Shape { get; set; }

        public override string ToString()
        {
            return string.Format("[BoundBarcodeData: ObjectName={0}, PropertyName={1}, BarcodeData={2}, Shape={3}]", BoundObjectPath, Shape);
        }
    }

}
