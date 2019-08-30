using System.Collections.Generic;

namespace PitneyBowes.Developer.Drawing
{
    public class SerializationVisitor : IVisitor
    {
        public void Visit(IContainer shape)
        {
            var wrappedContents = new List<IShape>();
            shape.Contents.ForEach(s => 
            {
                if (s as ShapeSerializationWrapper == null)
                    wrappedContents.Add(new ShapeSerializationWrapper(s));
                else
                    wrappedContents.Add(s);
                s.Accept(this); 
            });
            shape.Contents = wrappedContents;
        }

        public void Visit(ILine shape) { }
        public void Visit(IBox shape) { }
        public void Visit(ITextBox shape) 
        {
            if (shape.Text as TextSerializationWrapper == null)
                shape.Text = new TextSerializationWrapper(shape.Text);
        }
        public void Visit(IBarcode shape) 
        {
            if (shape.Data as BarcodeDataSerializationWrapper == null)
                shape.Data = new BarcodeDataSerializationWrapper(shape.Data);
        }
        public void Visit(IBitmap shape) 
        {
            if (shape.BitmapSource as BitmapSourceSerializationWrapper == null)
                shape.BitmapSource = new BitmapSourceSerializationWrapper(shape.BitmapSource);
        }
        public void Visit(IChildLabel shape)
        {
            if (shape.Shape as ShapeSerializationWrapper == null)
                shape.Shape = new ShapeSerializationWrapper(shape.Shape);
        }
    }

}
