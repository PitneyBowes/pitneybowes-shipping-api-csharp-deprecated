namespace PitneyBowes.Developer.Drawing
{
    public struct Point
    {

        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }
        public Point(Point p) : this(p.X, p.Y) {}

        public Point Duplicate() =>new Point(this);

        public float X { get; set; }
        public float Y { get; set; }
        public void MoveRelative(float deltaX, float deltaY)
        {
            X += deltaX;
            Y += deltaY;
        }
        public void MoveAbsolute(float x, float y)
        {
            X = x;
            Y = y;
        }
        public void MoveX(float x) => X = x;
        public void MoveY(float y) => Y = y;
        public override string ToString()
        {
            return string.Format("[Point: X={0}, Y={1}]", X, Y);
        }
    }
}
