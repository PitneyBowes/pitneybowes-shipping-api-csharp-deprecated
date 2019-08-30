namespace PitneyBowes.Developer.Drawing
{
    public interface IBarcodeData
    {
        byte[] BarcodeData(object boundObject);
        string BoundObjectPath { get; set; }
        IShape Shape { get; set; }
        IBarcodeData Duplicate();
    }
}
