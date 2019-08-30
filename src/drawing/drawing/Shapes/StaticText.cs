using System;
using Newtonsoft.Json;

namespace PitneyBowes.Developer.Drawing
{
    public class StaticText : IText
    {
        [JsonIgnore]
        public IShape Shape { get; set; }
        public string BoundObjectPath { get; set; }
        public string Format { get; set; }

        public IText Duplicate() => new StaticText(this.TextValue);

        public StaticText()
        {
            TextValue = string.Empty;
        }
        public StaticText(string textValue)
        {
            TextValue = textValue;
        }
        public string TextValue { get; set;}

        public override string ToString()
        {
            return string.Format("[StaticText: Text={0}]", TextValue);
        }

        public string Text(object boundObject = null, IFormatProvider formatProvider = null)
        {
            return TextValue;
        }
    }
}
