using System.Collections.Generic;
using Newtonsoft.Json;

namespace PitneyBowes.Developer.Drawing
{
    public class Container : Shape, IContainer
    {
        private List<IShape> _contents = new List<IShape>();
        public virtual List<IShape> Contents
        {
            get { return _contents; }
            set
            {
                _contents = value;
                _contents.ForEach(s => s.Container = this);
            }
        }
        public Point Anchor { get; set; }

        public bool ShouldSerializeBoundingBox() => false;
        public override Rectangle BoundingBox
        {
            get
            {
                var r = new Rectangle(Anchor, Anchor);
                foreach (var s in Contents) r.ExpandToCover(s.BoundingBox);
                return r;
            }
        }
        public Container() : base()
        {
            Anchor = new Point(0, 0);
        }
        public Container(float x, float y) : base() 
        {
            Anchor = new Point(x, y);
        }
        public Container(Container c) : this(c.Anchor.X, c.Anchor.Y)
        {
            _contents = new List<IShape>();
            c._contents.ForEach(s => _contents.Add(s));
        }
        public override void MoveRelative(float deltaX, float deltaY) => Anchor.MoveRelative(deltaX, deltaY);

        public override IShape Duplicate()
        {
            return new Container(this);
        }
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public virtual Container AddShape(IShape shape)
        {
            shape.Container = this;
            _contents.Add(shape);
            return this;
        }
        public override string ToString()
        {
            return string.Format("[Container: Anchor={0}, BoundingBox={1}]", Anchor, BoundingBox);
        }
    }
}
