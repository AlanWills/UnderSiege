using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Maths
{
    public abstract class Primitive
    {
        #region Properties and Fields

        public Color Colour { get; set; }

        private float opacity;
        public float Opacity 
        {
            set
            {
                opacity = value;
                //basicEffect.Alpha = opacity;
            }
        }

        //protected BasicEffect basicEffect;

        #endregion

        public Primitive()
        {
            Colour = Color.White;

            /*basicEffect = new BasicEffect(ScreenManager.Graphics);
            basicEffect.VertexColorEnabled = true;
            basicEffect.Projection = Matrix.CreateOrthographicOffCenter
               (0, ScreenManager.Viewport.Width,
                ScreenManager.Viewport.Height, 0,
                0, 1);

            Opacity = 0;*/
        }

        public Primitive(Color colour, float opacity)
        {
            Colour = colour;

            /*basicEffect = new BasicEffect(ScreenManager.Graphics);
            basicEffect.VertexColorEnabled = true;
            basicEffect.Projection = Matrix.CreateOrthographicOffCenter
               (0, ScreenManager.Viewport.Width,
                ScreenManager.Viewport.Height, 0,
                0, 1);

            Opacity = opacity;*/
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public abstract VertexPositionColor[] GetVertices();
        public abstract void Render();

        #endregion
    }
}
