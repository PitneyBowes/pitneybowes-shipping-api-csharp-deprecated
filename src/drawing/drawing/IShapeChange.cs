namespace PitneyBowes.Developer.Drawing
{
    public interface IShapeChange
    {
        bool CheckConstraints(IShape shape);
        void Apply(IShape shape);
    }
}
