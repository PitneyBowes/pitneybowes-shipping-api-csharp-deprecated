using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PitneyBowes.Developer.Drawing
{
    public enum HorizontalAlignment
    {
        Left,
        Center,
        Right
    }
    public enum VerticalAlignment
    {
        Top,
        Middle,
        Bottom
    }

    public class TextBox : Shape, ITextBox
    {
        public Point Anchor { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public float Margin { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public HorizontalAlignment HorizontalAlignment { get; set; }

        public TextBox() : this(0,0,0,0,null)
        {
        }


        public TextBox(float x, float y, float height, float width, IText text) : base() 
        {
            Anchor = new Point(x, y);
            Height = height;
            Width = width;
            if (text != null)
            {
                _text = text.Duplicate();
                _text.Shape = this;
            }
        }
        public TextBox(TextBox tb) : this(tb.Anchor.X, tb.Anchor.Y, tb.Height, tb.Width, tb.Text)
        {
        }
        public override IShape Duplicate() => new TextBox(this);

        public bool ShouldSerializeBoundingBox() => false;
        public override Rectangle BoundingBox => new Rectangle(Anchor.X, Anchor.Y, Anchor.X + Width, Anchor.Y + Height);

        private IText _text;
        public IText Text
        {
            get => _text;
            set
            {
                _text = value;
                _text.Shape = this;
            }
        }
        public override void MoveRelative(float deltaX, float deltaY)
        {
            Anchor.MoveRelative(deltaX, deltaY);
        }

        public override void Accept(IVisitor visitor) => visitor.Visit(this);
        public override string ToString()
        {
            return string.Format("[TextBox: Anchor={0}, Height={1}, Width={2}, BoundingBox={3}, Text={4}]", Anchor, Height, Width, BoundingBox, Text);
        }
    }
}
