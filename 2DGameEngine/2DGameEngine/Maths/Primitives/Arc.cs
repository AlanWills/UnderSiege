using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Maths.Primitives
{
    public class Arc : Primitive
    {
        #region Properties and Fields

        public virtual Vector2 Centre { get; set; }
        public virtual float Radius { get; set; }
        public virtual float StartingAngle { get; set; }
        public virtual float ArcWidth { get; set; }

        public int triangles = 20;

        #endregion

        public Arc(Vector2 centre, float radius, float startingAngle, float arcWidth)
            : base()
        {
            Centre = centre;
            Radius = radius;
            StartingAngle = startingAngle;
            ArcWidth = arcWidth;
        }

        public Arc(Vector2 centre, float radius, float startingAngle, float arcWidth, Color colour, float opacity)
            : base(colour, opacity)
        {
            Centre = centre;
            Radius = radius;
            StartingAngle = startingAngle;
            ArcWidth = arcWidth;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        // REMEMBER THAT 0 IS STRAIGHT UP WHICH IS NEGATIVE Y
        public override VertexPositionColor[] GetVertices()
        {
            VertexPositionColor[] vertices = new VertexPositionColor[triangles * 3];
            float angleIncrement = ArcWidth / (float)triangles;

            // Set up in the initial triangle
            vertices[0] = new VertexPositionColor(new Vector3(Centre.X, Centre.Y, 0), Colour);
            vertices[1] = new VertexPositionColor(new Vector3(Centre.X + (float)Math.Sin(StartingAngle) * Radius, Centre.Y - (float)Math.Cos(StartingAngle) * Radius, 0), Colour);
            vertices[2] = new VertexPositionColor(new Vector3(Centre.X + (float)Math.Sin(StartingAngle + angleIncrement) * Radius, Centre.Y - (float)Math.Cos(StartingAngle + angleIncrement) * Radius, 0), Colour);

            for (int i = 1; i < triangles; i++)
            {
                vertices[3 * i] = vertices[0];
                vertices[3 * i + 1] = vertices[3 * i - 1];

                float xDelta = (float)Math.Sin(StartingAngle + (i + 1) * angleIncrement) * Radius;
                float yDelta = (float)Math.Cos(StartingAngle + (i + 1) * angleIncrement) * Radius;
                vertices[3 * i + 2] = new VertexPositionColor(new Vector3(Centre.X + xDelta, Centre.Y - yDelta, 0), Colour);
            }

            return vertices;
        }

        public override void Render()
        {
            basicEffect.CurrentTechnique.Passes[0].Apply();

            ScreenManager.Graphics.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, GetVertices(), 0, triangles);
        }

        #endregion
    }
}
