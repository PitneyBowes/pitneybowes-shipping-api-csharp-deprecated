using Newtonsoft.Json;

namespace PitneyBowes.Developer.Drawing
{
    public class Box : Shape, IBox
    {
        private Rectangle _box = new Rectangle();
        public Box()
        {
            _box = new Rectangle(0, 0, 0, 0);
        }

        public Box(float x0, float y0, float x1, float y1) 
        {
            _box = new Rectangle(x0, y0, x1, y1);
        }
        public Box(Box b) : base(b)
        {
            _box= b._box.Duplicate();
        }
        public override IShape Duplicate() => new Box(this);

        public Point TopLeft
        {
            get => _box.TopLeft;
            set => _box.TopLeft.MoveAbsolute(value.X, value.Y);
        }
        public Point BottomRight
        {
            get => _box.BottomRight;
            set => _box.BottomRight.MoveAbsolute(value.X, value.Y);
        }

        public bool ShouldSerializeBoundingBox() => false;
        [JsonIgnore]
        public override Rectangle BoundingBox
        {
            get => _box;
        }
        public override void MoveRelative(float deltaX, float deltaY) => _box.MoveRelative(deltaX, deltaY);
        public override void Accept(IVisitor visitor) => visitor.Visit(this);
        public override string ToString()
        {
            return string.Format("[Box: LineProperties={0}, TopLeft={1}, BottomRight={2}, BoundingBox={3}]", TopLeft, BottomRight, BoundingBox);
        }
    }
}
