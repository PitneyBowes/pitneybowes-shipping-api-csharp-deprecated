using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PitneyBowes.Developer.Drawing
{
    public class ChildLabel : Shape, IChildLabel
    {

        public Dictionary<Guid, List<IShapeChange>> Changes = new Dictionary<Guid, List<IShapeChange>>();
        private IShape _shape;

        [JsonIgnore]
        public IShape Shape { 
            get { return _shape; } 
            set { _shape = value; _shape.Container = this; } 
        }
        [JsonIgnore]
        public bool Applied { get; set; }

        public void ApplyChanges()
        {
            if (Applied) return;
            var visitor = new ChangeVisitor() { Child = this};
            Shape.Accept(visitor);
            Applied = true;
        }

        public void AddChange(Guid id, IShapeChange change)
        {
            if (!Changes.ContainsKey(id)) Changes.Add(id, new List<IShapeChange>());
            Changes[id].Add(change);
        }

        public override Rectangle BoundingBox => Shape.BoundingBox;

        public override void Accept(IVisitor visitor)
        {
            Shape.Accept(visitor);
        }

        public override IShape Duplicate()
        {
            throw new NotImplementedException();
        }

        public override void MoveRelative(float deltaX, float deltaY)
        {
            Shape.MoveRelative(deltaX, deltaY);
        }
    }

    public class ChangeVisitor : IVisitor
    {
        public ChildLabel Child { get; set; }

        public void Visit(IContainer shape)
        {
            if (Child.Changes.ContainsKey(shape.Id))
            {
                Child.Changes[shape.Id].ForEach(c => c.Apply(shape));
            }
            shape.Contents.ForEach(s=>s.Accept(this));
        }

        public void Visit(ILine shape)
        {
            if (Child.Changes.ContainsKey(shape.Id))
            {
                Child.Changes[shape.Id].ForEach(c => c.Apply(shape));
            }
        }

        public void Visit(IBox shape)
        {
            if (Child.Changes.ContainsKey(shape.Id))
            {
                Child.Changes[shape.Id].ForEach(c => c.Apply(shape));
            }
        }

        public void Visit(ITextBox shape)
        {
            if (Child.Changes.ContainsKey(shape.Id))
            {
                Child.Changes[shape.Id].ForEach(c => c.Apply(shape));
            }
        }

        public void Visit(IBarcode shape)
        {
            if (Child.Changes.ContainsKey(shape.Id))
            {
                Child.Changes[shape.Id].ForEach(c => c.Apply(shape));
            }
        }

        public void Visit(IBitmap shape)
        {
            if (Child.Changes.ContainsKey(shape.Id))
            {
                Child.Changes[shape.Id].ForEach(c => c.Apply(shape));
            }

        }

        public void Visit(IChildLabel shape)
        {
            shape.Shape.Accept(this);
        }
    }
}
