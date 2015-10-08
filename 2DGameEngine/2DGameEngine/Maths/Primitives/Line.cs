using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Maths.Primitives
{
    public class Line : Primitive
    {
        #region Properties and Fields

        public float Gradient 
        { 
            get 
            {
                if (EndPoint.X == StartPoint.X)
                    return float.NaN;

                return ((EndPoint.Y - StartPoint.Y) / (EndPoint.X - StartPoint.X)); 
            } 
        }

        public float YIntercept 
        {
            get
            {
                if (Gradient == float.NaN)
                    return float.NaN;

                return StartPoint.Y - Gradient * StartPoint.X; 
            } 
        }

        public virtual Vector2 StartPoint { get; set; }
        public virtual Vector2 EndPoint { get; set; }

        #endregion

        public Line(Vector2 startPoint, Vector2 endPoint)
            : base()
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        public Line(Vector2 startPoint, Vector2 endPoint, Color colour, float opacity)
            : base(colour, opacity)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        #region Methods

        public override VertexPositionColor[] GetVertices()
        {
            VertexPositionColor[] vertices = new VertexPositionColor[2];
            vertices[0] = new VertexPositionColor(new Vector3(StartPoint.X, StartPoint.Y, 0), Colour);
            vertices[1] = new VertexPositionColor(new Vector3(EndPoint.X, EndPoint.Y, 0), Colour);

            return vertices;
        }

        public override void Render()
        {
            basicEffect.CurrentTechnique.Passes[0].Apply();

            ScreenManager.Graphics.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, GetVertices(), 0, 1);
        }

        #endregion

        #region Virtual Methods

        #endregion
    }
}
