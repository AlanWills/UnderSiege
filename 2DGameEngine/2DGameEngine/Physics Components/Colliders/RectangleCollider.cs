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
            Bounds = new Quad(parent, width, height, Color.Red, 0.5f);
        }

        public RectangleCollider(BaseObject parent, Vector2 dimensions)
            : this(parent, dimensions.X, dimensions.Y)
        {

        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override bool CheckCollisionWith(Vector2 point)
        {
            // Adjust for rotation here - this horrible syntax is for optimisation purposes
            Vector2 newPoint = Vector2.Add(Bounds.Centre, Vector2.Transform(Vector2.Subtract(point, Bounds.Centre), Matrix.CreateRotationZ(-Bounds.Rotation)));

            return _2DGeometry.RectangleContainsPoint(Bounds.Rectangle, newPoint);
        }

        public override bool CheckCollisionWith(Line line)
        {
            line.StartPoint = Vector2.Add(Bounds.Centre, Vector2.Transform(Vector2.Subtract(line.StartPoint, Bounds.Centre), Matrix.CreateRotationZ(-Bounds.Rotation)));
            line.EndPoint = Vector2.Add(Bounds.Centre, Vector2.Transform(Vector2.Subtract(line.EndPoint, Bounds.Centre), Matrix.CreateRotationZ(-Bounds.Rotation)));
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
