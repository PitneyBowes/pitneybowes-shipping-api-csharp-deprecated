using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PitneyBowes.Developer.Drawing
{
    public interface IBarcode : IShape
    {
        Point Anchor { get; set; }
        float Height { get; set; }
        float MaxWidth { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        Symbology Symbology { get; set; }
        IBarcodeData Data { get; set; }
    }
}
