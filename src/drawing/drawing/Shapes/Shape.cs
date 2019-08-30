using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PitneyBowes.Developer.Drawing
{
    public abstract class Shape : IShape
    {
        public virtual bool ShouldSerializeId() => Id != Guid.Empty;
        [JsonProperty(Order = 1)]
        public virtual Guid Id { get; set; }

        public virtual bool ShouldSerializeName() => Name != null && Name != string.Empty;
        [JsonProperty(Order=2)]
        public string Name { get; set; }


        public string ObjectPath { get; set; }
        private ColorProperties _colorProperties;
        private LineProperties _lineProperties;
        private TextProperties _textProperties;
        [JsonIgnore]
        public IShape Container { get; set; }
        [JsonIgnore]
        public abstract Rectangle BoundingBox { get; }
        public bool ShouldSerializeConstraints() => Constraints != null;
        public List<IConstraint> Constraints { get; set; }

        public virtual bool ShouldSerializeDrawBoundingBox() => DrawBoundingBox;
        public virtual bool DrawBoundingBox { get; set; }

        public Shape() 
        {
        }
        public Shape(Shape shape)
        {
            _colorProperties = shape._colorProperties.Duplicate();
            _lineProperties = shape._lineProperties.Duplicate();
            _textProperties = shape._textProperties.Duplicate();
            Container = shape.Container;
        }

        public abstract IShape Duplicate();

        public abstract void MoveRelative(float deltaX, float deltaY);


        public bool ShouldSerializeDefaultColors() => (_colorProperties != null);
        [JsonProperty(Order=3)]
        public virtual ColorProperties DefaultColors 
        { 
            get 
            {
                if (_colorProperties != null) return _colorProperties;
                if (Container == null) return null;
                return Container.DefaultColors;
            }
            set
            {
                _colorProperties = value;
            }

        }
        public bool ShouldSerializeDefaultLine() => (_lineProperties != null);
        [JsonProperty(Order=4)]
        public LineProperties DefaultLine 
        {
            get
            {
                if (_lineProperties != null) return _lineProperties;
                if (Container == null) return null;
                return Container.DefaultLine;
            }
            set
            {
                _lineProperties = value;
            }

        }
        public bool ShouldSerializeDefaultText() => (_textProperties != null);
        [JsonProperty(Order=5)]
        public TextProperties DefaultText 
        {
            get
            {
                if (_textProperties != null) return _textProperties;
                if (Container == null) return null;
                return Container.DefaultText;
            }
            set
            {
                _textProperties = value;
            }
        }
        public abstract void Accept(IVisitor visitor);

        public virtual void PageCoordinate(ref Point p)
        {
            if (Container != null)
            {
                Container.PageCoordinate(ref p);
                p.MoveRelative(Container.BoundingBox.TopLeft.X, Container.BoundingBox.TopLeft.Y);
            }
        }
        public override string ToString()
        {
            return string.Format("[Shape: BoundingBox={0}, DefaultColors={1}, DefaultLine={2}, DefaultText={3}]", BoundingBox, DefaultColors, DefaultLine, DefaultText);
        }
    }
}
