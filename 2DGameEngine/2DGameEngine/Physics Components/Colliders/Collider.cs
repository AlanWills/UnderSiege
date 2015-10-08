using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Maths;
using _2DGameEngine.Maths.Primitives;
using _2DGameEngine.Physics_Components.Colliders;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Physics_Components
{
    public abstract class Collider
    {
        #region Properties and Fields

        protected BaseObject Parent { get; set; }

        #endregion

        public Collider(BaseObject parent)
        {
            Parent = parent;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public abstract void UpdateCollider();

        public abstract bool CheckCollisionWith(Vector2 point);
        public abstract bool CheckCollisionWith(Line line);
        public abstract bool CheckCollisionWith(Arc arc);
        public abstract bool CheckCollisionWith(CircleCollider circle);
        public abstract bool CheckCollisionWith(RectangleCollider rectangle);

        public virtual bool CheckCollisionWith(Collider collider)
        {
            RectangleCollider rectangleCollider = collider as RectangleCollider;
            if (rectangleCollider != null)
                return CheckCollisionWith(rectangleCollider);

            CircleCollider circleCollider = collider as CircleCollider;
            if (circleCollider != null)
                return CheckCollisionWith(circleCollider);

            return false;
        }

        #endregion
    }
}
