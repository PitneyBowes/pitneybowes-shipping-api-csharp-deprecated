namespace PitneyBowes.Developer.Drawing
{
    public class Page
    {
        public float Height { get; set; }
        public float Width { get; set; }
        public float Resolution { get; set; }
        public int ToPixels(float f)
        {
            return (int)(f * Resolution);
        }
        public override string ToString()
        {
            return string.Format("[Page: Height={0}, Width={1}, Resolution={2}]", Height, Width, Resolution);
        }
    }
}
