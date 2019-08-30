using System;
using Newtonsoft.Json;

namespace PitneyBowes.Developer.Drawing
{
    public class TextSerializationWrapper : IText
    {

        public TextSerializationWrapper(IText wrapped)
        {
            Wrapped = wrapped;
        }

        [JsonIgnore]
        public IText Wrapped { get; set; }

        public bool ShouldSerializestaticText() => staticText != null;
        public StaticText staticText { get => Wrapped as StaticText; }

        public bool ShouldSerializeBoundPropertyText() => BoundPropertyText != null;
        public BoundText BoundPropertyText { get => Wrapped as BoundText; }

        [JsonIgnore]
        public IShape Shape { get => Wrapped.Shape; set => Wrapped.Shape = value; }
        [JsonIgnore]
        public string BoundObjectPath { get; set; }
        [JsonIgnore]
        public string Format { get; set; }

        public IText Duplicate()
        {
            return new TextSerializationWrapper(Wrapped.Duplicate());
        }

        public string Text(object boundObject = null, IFormatProvider formatProvider = null)
        {
            if (boundObject == null )
            {
                return string.Empty;
            }
            throw new NotImplementedException();
        }
    }
}
