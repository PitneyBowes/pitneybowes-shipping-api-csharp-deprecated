using System.Text;
using Newtonsoft.Json;

namespace PitneyBowes.Developer.Drawing
{
    public class StaticBarcodeData : IBarcodeData
    {
        private byte[] _barcodeData;
        public StaticBarcodeData()
        {
            _barcodeData = new byte[] { };
        }

        public StaticBarcodeData(byte[] data)
        {
            _barcodeData = new byte[data.Length];
            data.CopyTo(_barcodeData, 0);
        }
        public IBarcodeData Duplicate()
        {
            var bd = new StaticBarcodeData(_barcodeData);
            return bd;
        }
        [JsonIgnore]
        public IShape Shape { get; set; }

        public string BoundObjectPath { get; set; }

        public byte[] BarcodeData ( object boundObject ) 
        {
            return _barcodeData;
        }
        public override string ToString()
        {
            return string.Format("[StaticBarcodeData: Shape={0}, BarcodeData={1}]", Shape, _barcodeData);
        }
    }

}
