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
    public class PictureBox : ScreenUIObject
    {
        #region Properties and Fields

        #endregion

        public PictureBox(string dataAsset = "", BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(dataAsset, parent, lifeTime)
        {

        }

        public PictureBox(Vector2 position, string dataAsset = "", BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(position, dataAsset, parent, lifeTime)
        {

        }

        public PictureBox(Vector2 position, Vector2 size, string dataAsset = "", BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(position, size, dataAsset, parent, lifeTime)
        {

        }

        public PictureBox(Vector2 position, Rectangle source, string dataAsset = "", BaseObject parent = null, float lifeTime = float.MaxValue)
            : this(position, dataAsset, parent, lifeTime)
        {
            SourceRectangle = source;
        }

        public PictureBox(Vector2 position, Vector2 size, Rectangle source, string dataAsset = "", BaseObject parent = null, float lifeTime = float.MaxValue)
            : this(position, size, dataAsset, parent, lifeTime)
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
