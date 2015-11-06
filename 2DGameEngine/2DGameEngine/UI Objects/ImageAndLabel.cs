using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnderSiege.UI
{
    public class ImageAndLabel : ScreenUIObject
    {
        #region Properties and Fields

        private Image Image { get; set; }
        private Label Label { get; set; }

        public Vector2 Dimensions
        {
            get
            {
                return new Vector2(Image.Size.X + Label.TextDimensions.X + padding, (float)Math.Max(Image.Size.Y, Label.TextDimensions.Y));
            }
        }

        private const float padding = 5f;

        #endregion

        public ImageAndLabel(string imageDataAsset, string labelText, Vector2 position, BaseObject parent = null)
            : base(position, "", parent)
        {
            Image = new Image(imageDataAsset, this);
            Label = new Label(labelText, Color.White, this);
        }

        public ImageAndLabel(string imageDataAsset, string labelText, Vector2 position, Color textColour, BaseObject parent = null)
            : base(position, "", parent)
        {
            Image = new Image(imageDataAsset, this);
            Label = new Label(labelText, textColour, this);
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadContent()
        {
            Image.LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();

            Image.Initialize();
            Image.LocalPosition = new Vector2(-Dimensions.X * 0.5f + Image.Size.X * 0.5f, 0);
            Label.Initialize();
            Label.LocalPosition = new Vector2(Dimensions.X * 0.5f - Label.TextDimensions.X * 0.5f, 0);
        }

        public override void AddCollider()
        {
            // Don't need to add a collider
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
            Label.Draw(spriteBatch);
        }

        public override void HandleInput()
        {
            
        }

        #endregion
    }
}
