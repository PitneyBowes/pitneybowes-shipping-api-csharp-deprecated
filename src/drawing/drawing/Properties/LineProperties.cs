namespace PitneyBowes.Developer.Drawing
{
    public class LineProperties
    {
        public float Width { get; set; }
        public LineProperties(float width)
        {
            Width = width;
        }
        public LineProperties()
        {
            Width = 0;
        }
        public LineProperties(LineProperties p) : this(p.Width)
        {
        }
        public LineProperties Duplicate() => new LineProperties(this);

    }
}
