using System;
using System.IO;

namespace PitneyBowes.Developer.Drawing
{
    public class Bitmap : Shape, IBitmap
    {
        Rectangle _boundingBox;

        public Bitmap()
        {
            _boundingBox = new Rectangle(0, 0, 0, 0);
        }

        public Bitmap(float x0, float y0, float x1, float y1)
        {
            _boundingBox = new Rectangle(x0, y0, x1, y1);
        }
        public override Rectangle BoundingBox { get => _boundingBox; }

        public bool FitToBoundingBox { get; set; }

        public IBitmapSource BitmapSource { get; set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override IShape Duplicate()
        {
            throw new NotImplementedException();
        }

        public override void MoveRelative(float deltaX, float deltaY)
        {
            _boundingBox.MoveRelative(deltaX, deltaY);
        }
    }
}
