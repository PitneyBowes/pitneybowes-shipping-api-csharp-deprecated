namespace PitneyBowes.Developer.Drawing
{
    public class ColorProperties
    {
        public RGBColor Foreground { get; set; }
        public RGBColor Background { get; set; }
        public ColorProperties(RGBColor foreground, RGBColor background) 
        { 
            Foreground = foreground;
            Background = background;
        }
        public ColorProperties()
        {
            Foreground = new RGBColor(0,0,0);
            Background = new RGBColor(255,255,255);
        }

        public ColorProperties(ColorProperties p) : this(p.Foreground, p.Background)
        {
        }
        public ColorProperties Duplicate() => new ColorProperties(this);
    }
}
