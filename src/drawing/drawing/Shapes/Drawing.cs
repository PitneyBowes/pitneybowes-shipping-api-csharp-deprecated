namespace PitneyBowes.Developer.Drawing
{
    public class Drawing : Container, IDrawing
    {
        public Drawing() : base(0,0)
        {
            DefaultColors = new ColorProperties(new RGBColor("Black"), new RGBColor("White") );
            DefaultLine = new LineProperties(1);
            DefaultText = new TextProperties("Arial", 12, FontStyle.Normal );
        }
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit((Container)this);
        }
    }
}
