using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace PitneyBowes.Developer.Drawing
{
    public class ShapeSerializationWrapper : IShape
    {
        public ShapeSerializationWrapper(IShape wrapped)
        {
            Wrapped = wrapped;
        }

        [JsonIgnore]
        public IShape Wrapped { get; set; }

        public bool ShouldSerializeLine() => Line != null;
        public Line Line { get => Wrapped as Line; }

        public bool ShouldSerializeBitmap() => Bitmap != null;
        public Bitmap Bitmap { get => Wrapped as Bitmap; }

        public bool ShouldSerializeBarcode() => Barcode != null;
        public Barcode Barcode { get => Wrapped as Barcode; }

        public bool ShouldSerializeBox() => Box != null;
        public Box Box { get => Wrapped as Box; }

        public bool ShouldSerializeTextbox() => Textbox != null;
        public TextBox Textbox { get => Wrapped as TextBox; }

        public bool ShouldSerializeGroup() => Group != null;
        public Container Group { get => Wrapped as Container; }

        public bool ShouldSerializeDrawing() => Drawing != null;
        public Drawing Drawing { get => Wrapped as Drawing; }

        public bool ShouldSerializeChild() => Child != null;
        public ChildLabel Child { get => Wrapped as ChildLabel; }

        // IShape methods delegated to Wrapped
        [JsonIgnore]
        public Guid Id { get => Wrapped.Id; set => Wrapped.Id = value; }
        [JsonIgnore]
        public string Name { get => Wrapped.Name; set => Wrapped.Name = value; }
        [JsonIgnore]
        public IShape Container { get => Wrapped.Container; set => Wrapped.Container = value; }
        [JsonIgnore]
        public Rectangle BoundingBox => Wrapped.BoundingBox;
        [JsonIgnore]
        public ColorProperties DefaultColors { get => Wrapped.DefaultColors; set => Wrapped.DefaultColors = value; }
        [JsonIgnore]
        public LineProperties DefaultLine { get => Wrapped.DefaultLine; set => Wrapped.DefaultLine = value; }
        [JsonIgnore]
        public TextProperties DefaultText { get => Wrapped.DefaultText; set => Wrapped.DefaultText = value; }
        [JsonIgnore]
        public bool DrawBoundingBox { get => Wrapped.DrawBoundingBox; set => Wrapped.DrawBoundingBox = value; }
        [JsonIgnore]
        public List<IConstraint> Constraints { get => Wrapped.Constraints; set => Wrapped.Constraints = value; }
        public void Accept(IVisitor visitor) => Wrapped.Accept(visitor);
        public IShape Duplicate() => Wrapped.Duplicate();
        public void MoveRelative(float deltaX, float deltaY) => Wrapped.MoveRelative(deltaX, deltaY);
        public void PageCoordinate(ref Point p) => Wrapped.PageCoordinate(ref p);

    }
}
