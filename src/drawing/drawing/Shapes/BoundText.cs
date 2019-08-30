using System;
using Newtonsoft.Json;

namespace PitneyBowes.Developer.Drawing
{
    public class BoundText : IText
    {
        public string ObjectName { get; set; }
        public string Format { get; set; }
        public IText Duplicate() => new BoundText() { ObjectName = this.ObjectName, Format = this.Format };

        public string Text(object boundObject = null, IFormatProvider formatProvider = null)
        {
            if (boundObject == null) return "";
            boundObject = Binding.BoundObject(boundObject, BoundObjectPath);
            if (boundObject == null) return "";
            if (formatProvider != null) 
                return string.Format(formatProvider, Format, boundObject);
            else 
                return string.Format(Format, boundObject);
        }
        [JsonIgnore]
        public IShape Shape { get; set; }
        public string BoundObjectPath { get; set; }

        public override string ToString()
        {
            return string.Format("[BoundText: ObjectName={0}, PropertyName={1}, Format={2}]", ObjectName, Format, Shape);
        }
    }
}
