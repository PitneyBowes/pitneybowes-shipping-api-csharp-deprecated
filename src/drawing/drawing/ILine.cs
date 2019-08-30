namespace PitneyBowes.Developer.Drawing
{
    public interface ILine : IShape
    {
        Point Start { get; set; }
        Point End { get; set; }
    }
}
