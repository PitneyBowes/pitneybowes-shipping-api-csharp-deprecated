namespace PitneyBowes.Developer.Drawing
{
    public struct Rectangle
    {
        private static float Min(float x, float y) => x < y ? x : y;
        private static float Max(float x, float y) => x > y ? x : y;

        public Rectangle(float x0, float y0, float x1, float y1)
        {
            var xLeft = Min(x0, x1);
            var xRight = Max(x0, x1);
            var yTop = Min(y0, y1);
            var yBottom = Max(y0, y1);
            TopLeft = new Point(xLeft, yTop);
            BottomRight = new Point(xRight, yBottom);
        }
        public Rectangle(Point topLeft, Point bottomRight) : this(topLeft.X, topLeft.Y, bottomRight.X, bottomRight.Y)
        {
        }
        public Rectangle(Rectangle b) : this(b.TopLeft.X, b.TopLeft.Y, b.BottomRight.X, b.BottomRight.Y)
        {
        }
        public Rectangle Duplicate()
        {
            return new Rectangle(this);
        }
        public void MoveRelative(float deltaX, float deltaY)
        {
            TopLeft.MoveRelative(deltaX, deltaY);
            BottomRight.MoveRelative(deltaX, deltaY);
        }
        public bool IsInside(Point p)
        {
            return (p.X >= TopLeft.X )&& (p.X <= BottomRight.X) && (p.Y >= TopLeft.Y) && (p.Y <= BottomRight.Y); 
        }
        public void ExpandToCover(Point p)
        {
            if (p.X < TopLeft.X) TopLeft.MoveX(p.X);
            if (p.X > BottomRight.X) BottomRight.MoveX(p.X);
            if (p.Y < TopLeft.Y) TopLeft.MoveY(p.Y);
            if (p.X > BottomRight.X) BottomRight.MoveY(p.Y);

        }
        public void ExpandToCover(Rectangle r)
        {
            ExpandToCover(r.TopLeft);
            ExpandToCover(r.BottomRight);
        }
        private bool _intervalContains(float x0, float x1, float x)
        {
            return (x0 <= x && x <= x1) || (x1 <= x && x <= x0);
        }
        public bool Contains( Point p )
        {
            return _intervalContains(TopLeft.X, BottomRight.X, p.X) && _intervalContains(TopLeft.Y, BottomRight.Y, p.Y);
        }
        public bool Contains( Rectangle r)
        {
            var TopRight = new Point(r.BottomRight.X, r.TopLeft.Y);
            var BottomLeft = new Point(r.BottomRight.X, r.TopLeft.Y);
            return Contains(r.TopLeft) && Contains(TopRight) && Contains(BottomLeft) && Contains(r.BottomRight);
        }
        public bool Overlaps(Rectangle r)
        {
            
            return Contains(r.TopLeft) || Contains(r.BottomRight) || Contains(new Point(r.BottomRight.X, r.TopLeft.Y)) || Contains(new Point(r.BottomRight.X, r.TopLeft.Y)) 
                || r.Contains(TopLeft) || r.Contains(BottomRight) || r.Contains(new Point(BottomRight.X, TopLeft.Y)) || r.Contains(new Point(BottomRight.X, TopLeft.Y));
        }

        public Point TopLeft { get; }
        public Point BottomRight { get; }
        public override string ToString()
        {
            return string.Format("[Rectangle: TopLeft={0}, BottomRight={1}]", TopLeft, BottomRight);
        }
    }
}
