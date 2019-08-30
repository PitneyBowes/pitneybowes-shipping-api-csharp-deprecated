using Newtonsoft.Json;

namespace PitneyBowes.Developer.Drawing
{
    
    public class Barcode : Shape, IBarcode
    {
        private IBarcodeData _barcodeData;
        public Point Anchor { get; set; }
        public float Height { get; set; }
        public float MaxWidth { get; set; }
        public Barcode() : this(0, 0, 0, 0, Symbology.Code128, null)
        {
        }
        public Barcode(float x, float y, float height, float width, Symbology symbology, IBarcodeData barcodeData) : base() 
        {
            Anchor = new Point(x, y);
            Height = height;
            MaxWidth = width;
            Symbology = symbology;
            if (barcodeData != null)
            {
                _barcodeData = barcodeData;
            }
        }

        public Barcode(Barcode b) : this(b.Anchor.X, b.Anchor.Y, b.Height, b.MaxWidth, b.Symbology, b._barcodeData)
        {
            _barcodeData = b._barcodeData.Duplicate();
            _barcodeData.Shape = this;
        }
        public override IShape Duplicate() => new Barcode(this);
        public Symbology Symbology { get; set; }
        public IBarcodeData Data 
        { 
            get => _barcodeData; 
            set
            {
                _barcodeData = value;
                _barcodeData.Shape = this;
            }
        }
        public bool ShouldSerializeBoundingBox() => false;
        public override Rectangle BoundingBox => new Rectangle(Anchor.X, Anchor.Y, Anchor.X + MaxWidth, Anchor.Y + Height);

        public override void Accept(IVisitor visitor) => visitor.Visit(this);
        public override void MoveRelative(float deltaX, float deltaY)
        {
            Anchor.MoveRelative(deltaX, deltaY);
        }
        public override string ToString()
        {
            return string.Format("[Barcode: Anchor={0}, Height={1}, Width={2}, Symbology={3}, Data={4}, BoundingBox={5}]", Anchor, Height, MaxWidth, Symbology, Data, BoundingBox);
        }
    }

}
