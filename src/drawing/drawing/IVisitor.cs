namespace PitneyBowes.Developer.Drawing
{
    public interface IVisitor
    {
        void Visit(IContainer shape);
        void Visit(ILine shape);
        void Visit(IBox shape);
        void Visit(ITextBox shape);
        void Visit(IBarcode shape);
        void Visit(IBitmap shape);
        void Visit(IChildLabel shape);
    }
}
