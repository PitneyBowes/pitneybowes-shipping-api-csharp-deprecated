namespace PitneyBowes.Developer.Drawing
{
    public class ChangeFontSize : IShapeChange
    {
        public float FontSize { get; set; }
        public bool RelativeChange { get; set; }
        public bool CheckConstraints(IShape shape)
        {
            if (shape.Constraints == null) return true;
            foreach (var constraint in shape.Constraints)
            {
                var c = constraint as MinimumFontSize;
                if (c == null) continue;
                if (RelativeChange)
                {
                    if (shape.DefaultText.FontSize + FontSize < c.Minimum) return false;
                }
                else
                {
                    if ( FontSize < c.Minimum) return false;
                }
            }
            return true;

        }
        public void Apply(IShape shape)
        {
            if (!CheckConstraints(shape)) return;
            shape.DefaultText = new TextProperties(shape.DefaultText);
            if (RelativeChange)
                shape.DefaultText.FontSize += FontSize;
            else
                shape.DefaultText.FontSize = FontSize;
        }
    }
}
