using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PitneyBowes.Developer.Drawing
{
    public class TextProperties
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public FontStyle Style { get; set; }
        public string FontName { get; set; }
        public float FontSize { get; set; }
        public TextProperties() : this(string.Empty, 0, FontStyle.Normal){}
        public TextProperties(string fontName, float fontSize, FontStyle style) 
        {
            FontName = fontName;
            FontSize = fontSize;
            Style = style;
        }
        public TextProperties(TextProperties p) : this(p.FontName, p.FontSize, p.Style)
        {
        }
        public TextProperties Duplicate() => new TextProperties(this);

    }
}
