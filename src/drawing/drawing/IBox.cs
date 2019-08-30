namespace PitneyBowes.Developer.Drawing
{
    public interface IBox : IShape
    {
        Point TopLeft { get; set; }
        Point BottomRight { get; set; }
    }
}
