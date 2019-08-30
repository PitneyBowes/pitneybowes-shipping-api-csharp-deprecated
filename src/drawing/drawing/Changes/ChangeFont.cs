namespace PitneyBowes.Developer.Drawing
{
    public class ChangeFont : IShapeChange
    {
        public string FontName { get; set; }
        public bool CheckConstraints(IShape shape)
        {
            if (shape.Constraints == null) return true;
            foreach(var constraint in shape.Constraints)
            {
                var c = constraint as CannotChangeFont;
                if (c != null) return false;
            }
            return true;
        }
        public void Apply(IShape shape)
        {
            if (!CheckConstraints(shape)) return;
            shape.DefaultText = new TextProperties(shape.DefaultText);
            shape.DefaultText.FontName = FontName;
        }
    }
}
