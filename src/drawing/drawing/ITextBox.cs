namespace PitneyBowes.Developer.Drawing
{
    public interface ITextBox : IShape
    {
        Point Anchor { get; set; }
        float Height { get; set; }
        float Width { get; set; }
        IText Text { get; set; }
        float Margin { get; set; }
        HorizontalAlignment HorizontalAlignment { get; set; }
    }
}
