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
    public class CircleCollider : Collider
    {
        #region Properties and Fields

        // Can now debug render by calling Circle.Render()
        public Circle Circle { get; set; }

        #endregion

        public CircleCollider(BaseObject parent, float radius)
            : base(parent)
        {
            Circle = new Circle(parent.WorldPosition, radius, Color.Red, 0.5f);
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void UpdateCollider()
        {
            Circle.Centre = Parent.WorldPosition;
        }

        public override bool CheckCollisionWith(Vector2 point)
        {
            return _2DGeometry.CircleContainsPoint(Circle, point);
        }

        public override bool CheckCollisionWith(Line line)
        {
            return _2DGeometry.LineIntersectsCircle(Circle, line);
        }

        public override bool CheckCollisionWith(Arc arc)
        {
            return _2DGeometry.CircleIntersectsArc(Circle, arc);
        }

        public override bool CheckCollisionWith(CircleCollider circleCollider)
        {
            return _2DGeometry.CircleIntersectsCircle(circleCollider.Circle, Circle);
        }

        public override bool CheckCollisionWith(RectangleCollider rectangleCollider)
        {
            return _2DGeometry.RectangleIntersectsCircle(rectangleCollider.Bounds.Rectangle, Circle);
        }

        #endregion
    }
}
