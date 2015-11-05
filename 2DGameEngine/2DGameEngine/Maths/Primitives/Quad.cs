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
    public class Quad : Primitive
    {
        #region Properties and Fields

        public float Width { get; set; }
        public float Height { get; set; }
        public float Rotation { get { return BaseObject.WorldRotation; } }

        public Vector2 Centre { get { return BaseObject.WorldPosition; } }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)(Centre.X - Width * 0.5f), (int)(Centre.Y - Height * 0.5f), (int)(Width), (int)(Height));
            }
        }

        public BaseObject BaseObject { get; private set; }

        #endregion

        public Quad(BaseObject baseObject, float width, float height)
            : base()
        {
            BaseObject = baseObject;
            Width = width;
            Height = height;
        }

        public Quad(BaseObject baseObject, float width, float height, Color colour, float opacity)
            : base(colour, opacity)
        {
            BaseObject = baseObject;
            Width = width;
            Height = height;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        // WITH 0 ROTATION, THE VERTICES ARE ORDERED:
        /*
         * 1   3
         * 0   2
         */

        public override VertexPositionColor[] GetVertices()
        {
            VertexPositionColor[] vertices = new VertexPositionColor[4];

            float sinRot = (float)Math.Sin(Rotation);
            float cosRot = (float)Math.Cos(Rotation);

            // Not in the right order any more
            // To find coefficients, look at component now, is it pos or neg - this is cos.  Then rotate 90 degrees and repeat, but with sin
            vertices[0] = new VertexPositionColor(new Vector3(-cosRot * Width * 0.5f - sinRot * Height * 0.5f + Centre.X, cosRot * Height * 0.5f - sinRot * Width * 0.5f + Centre.Y, 0), Colour);
            vertices[1] = new VertexPositionColor(new Vector3(-cosRot * Width * 0.5f + sinRot * Height * 0.5f + Centre.X, -cosRot * Height * 0.5f - sinRot * Width * 0.5f + Centre.Y, 0), Colour);
            vertices[2] = new VertexPositionColor(new Vector3(cosRot * Width * 0.5f - sinRot * Height * 0.5f + Centre.X, cosRot * Height * 0.5f + sinRot * Width * 0.5f + Centre.Y, 0), Colour);
            vertices[3] = new VertexPositionColor(new Vector3(cosRot * Width * 0.5f + sinRot * Height * 0.5f + Centre.X, -cosRot * Height * 0.5f + sinRot * Width * 0.5f + Centre.Y, 0), Colour);

            return vertices;
        }

        public override void Render()
        {
            //basicEffect.CurrentTechnique.Passes[0].Apply();

            //ScreenManager.Graphics.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, GetVertices(), 0, 2);
        }
        #endregion
    }
}
