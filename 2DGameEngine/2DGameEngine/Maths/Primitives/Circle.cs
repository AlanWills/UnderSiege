using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Maths.Primitives
{
    public class Circle : Primitive
    {
        #region Properties and Fields

        public Vector2 Centre { get { return BaseObject.WorldPosition; } }
        public float Radius { get; set; }
        private BaseObject BaseObject { get; set; }

        public int triangles = 20;

        #endregion

        public Circle(BaseObject baseObject, float radius)
            : base()
        {
            BaseObject = baseObject;
            Radius = radius;
        }

        public Circle(BaseObject baseObject, float radius, Color colour, float opacity)
            : base(colour, opacity)
        {
            BaseObject = baseObject;
            Radius = radius;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        // REMEMBER THAT 0 IS STRAIGHT UP WHICH IS NEGATIVE Y
        // See if we can change this somewhat
        public override VertexPositionColor[] GetVertices()
        {
            VertexPositionColor[] vertices = new VertexPositionColor[triangles * 3];
            float angleIncrement = MathHelper.TwoPi / (float)triangles;

            // Set up in the initial triangle
            vertices[0] = new VertexPositionColor(new Vector3(Centre.X, Centre.Y, 0), Colour);
            vertices[1] = new VertexPositionColor(new Vector3(Centre.X, Centre.Y + Radius, 0), Colour);
            vertices[2] = new VertexPositionColor(new Vector3(Centre.X + (float)Math.Sin(-MathHelper.Pi + angleIncrement) * Radius, Centre.Y - (float)Math.Cos(-MathHelper.Pi + angleIncrement) * Radius, 0), Colour);

            for (int i = 1; i < triangles; i++)
            {
                vertices[3 * i] = vertices[0];
                vertices[3 * i + 1] = vertices[3 * i - 1];

                float xDelta = (float)Math.Sin(-MathHelper.Pi + (i + 1) * angleIncrement) * Radius;
                float yDelta = (float)Math.Cos(-MathHelper.Pi + (i + 1) * angleIncrement) * Radius;
                vertices[3 * i + 2] = new VertexPositionColor(new Vector3(Centre.X + xDelta, Centre.Y - yDelta, 0), Colour);
            }

            return vertices;
        }

        public override void Render()
        {
            //basicEffect.CurrentTechnique.Passes[0].Apply();

            //ScreenManager.Graphics.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, GetVertices(), 0, triangles);
        }

        #endregion
    }
}
