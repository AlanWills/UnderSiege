using _2DGameEngine.Abstract_Object_Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.UI_Objects
{
    public class PictureBox : UIObject
    {
        #region Properties and Fields

        #endregion

        public PictureBox(string dataAsset = "", BaseObject parent = null)
            : base(dataAsset, parent)
        {

        }

        public PictureBox(Vector2 position, string dataAsset = "", BaseObject parent = null)
            : base(position, dataAsset, parent)
        {

        }

        public PictureBox(Vector2 position, Vector2 size, string dataAsset = "", BaseObject parent = null)
            : base(position, size, dataAsset, parent)
        {

        }

        public PictureBox(Vector2 position, Rectangle source, string dataAsset = "", BaseObject parent = null)
            : this(position, dataAsset, parent)
        {
            SourceRectangle = source;
        }

        public PictureBox(Vector2 position, Vector2 size, Rectangle source, string dataAsset = "", BaseObject parent = null)
            : this(position, size, dataAsset, parent)
        {
            SourceRectangle = source;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadContent()
        {
            base.LoadContent();

            if (SourceRectangle == Rectangle.Empty && Texture != null)
            {
                SourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            }
        }

        #endregion
    }
}
