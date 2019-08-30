using Newtonsoft.Json;

namespace PitneyBowes.Developer.Drawing
{
    public class Line : Shape, ILine
    {
        public Line() : base()
        {
        }
        public Line(Line l) : base(l)
        {
            Start = l.Start.Duplicate();
            End = l.End.Duplicate();
        }
        public Line(Point start, Point end)
        {
            Start = start.Duplicate();
            End = end.Duplicate();
        }
        public Line(float x0, float y0, float x1, float y1) 
        { 
            Start = new Point(x0, y0);
            End = new Point(x1, y1);
        }

        public override IShape Duplicate() => new Line(this);

        public Point Start { get; set; }
        public Point End { get; set; }

        public bool ShouldSerializeBoundingBox() => false;
        [JsonIgnore]
        public override Rectangle BoundingBox => new Rectangle(Start, End);

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        public override void MoveRelative(float deltaX, float deltaY)
        {
            Start.MoveRelative(deltaX, deltaY);
            End.MoveRelative(deltaX, deltaY);
        }
        public override string ToString()
        {
            return string.Format("[Line: Start={0}, End={1}, BoundingBox={2}]", Start, End, BoundingBox);
        }
    }
}
