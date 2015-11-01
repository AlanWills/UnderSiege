using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Maths;
using _2DGameEngine.Maths.Primitives;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Physics_Components.Colliders
{
    public class RectangleCollider : Collider
    {
        #region Properties and Fields

        // Can now debug draw - call Bounds.Draw()
        public Quad Bounds
        {
            get;
            set;
        }

        // { return new Quad(ParentObject.WorldPosition, Width, Height, 0, Color.Red, 0.5f); }

        #endregion

        public RectangleCollider(BaseObject parent, float width, float height)
            : base(parent)
        {
            Bounds = new Quad(parent.WorldPosition + (parent.Size * 0.5f - parent.Centre * parent.Scale), width, height, parent.WorldRotation, Color.Red, 0.5f);
        }

        public RectangleCollider(BaseObject parent, Vector2 dimensions)
            : this(parent, dimensions.X, dimensions.Y)
        {

        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void UpdateCollider()
        {
            Bounds.Centre = Parent.WorldPosition;
            Bounds.Rotation = Parent.WorldRotation;
        }

        public override bool CheckCollisionWith(Vector2 point)
        {
            // Adjust for rotation here
            Vector2 diff = point - Bounds.Centre;
            float cosRot = (float)Math.Cos(-Bounds.Rotation);
            float sinRot = (float)Math.Sin(-Bounds.Rotation);

            // Change the space so we imagine the bounds being upright and rotated the point accordingly
            Vector2 newPoint = Bounds.Centre + new Vector2(diff.X * cosRot - diff.Y * sinRot, -diff.X * sinRot + diff.Y * cosRot);

            return _2DGeometry.RectangleContainsPoint(Bounds.Rectangle, newPoint);
        }

        public override bool CheckCollisionWith(Line line)
        {
            return _2DGeometry.LineIntersectsRect(line, Bounds.Rectangle);
        }

        public override bool CheckCollisionWith(Arc arc)
        {
            return _2DGeometry.RectangleIntersectsArc(Bounds.Rectangle, arc);
        }

        public override bool CheckCollisionWith(CircleCollider circleCollider)
        {
            return _2DGeometry.RectangleIntersectsCircle(Bounds.Rectangle, circleCollider.Circle);
        }

        public override bool CheckCollisionWith(RectangleCollider rectangleCollider)
        {
            return _2DGeometry.RectangleIntersectsRectangle(Bounds.Rectangle, rectangleCollider.Bounds.Rectangle);
        }

        #endregion
    }
}
