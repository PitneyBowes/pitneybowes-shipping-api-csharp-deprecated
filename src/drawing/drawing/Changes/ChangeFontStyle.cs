namespace PitneyBowes.Developer.Drawing
{
    public class ChangeFontStyle : IShapeChange
    {
        public FontStyle Style { get; set; }

        public bool CheckConstraints(IShape shape) => true;

        public void Apply(IShape shape)
        {
            if (!CheckConstraints(shape)) return;
            shape.DefaultText = new TextProperties(shape.DefaultText);
            shape.DefaultText.Style = Style;
        }
    }
}
