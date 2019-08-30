namespace PitneyBowes.Developer.Drawing
{
    public interface IBoundingBox
    {
        Point TopLeft { get; set; }
        Point BottomRight { get; set; }
    }
}
