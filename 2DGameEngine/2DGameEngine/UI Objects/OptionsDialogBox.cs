using _2DGameEngine.Abstract_Object_Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.UI_Objects
{
    public class OptionsDialogBox : DialogBox
    {
        #region Properties and Fields

        public Button LeftButton { get; set; }
        public Button RightButton { get; set; }

        #endregion

        public OptionsDialogBox(string text, Vector2 position, EventHandler leftButtonEvent, EventHandler rightButtonEvent, string leftButtonText = "Cancel", string rightButtonText = "Confirm", string dataAsset = "Sprites\\UI\\Menus\\DialogBox", BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(text, position, dataAsset, parent, lifeTime)
        {
            LeftButton = new Button(new Vector2(-Size.X * 0.25f, (Size.Y + Button.defaultTexture.Height) * 0.5f), new Vector2(Math.Min(Button.defaultTexture.Width, Size.X * 0.5f), Button.defaultTexture.Height), leftButtonText, this);
            LeftButton.OnSelect += leftButtonEvent;

            RightButton = new Button(new Vector2(Size.X * 0.25f, (Size.Y + Button.defaultTexture.Height) * 0.5f), new Vector2(Math.Min(Button.defaultTexture.Width, Size.X * 0.5f), Button.defaultTexture.Height), rightButtonText, this);
            RightButton.Size = new Vector2(Math.Min(RightButton.Size.X, Size.X * 0.5f), RightButton.Size.Y);
            RightButton.OnSelect += rightButtonEvent;
        }

        public OptionsDialogBox(string text, Vector2 position, Vector2 size, EventHandler leftButtonEvent, EventHandler rightButtonEvent, string leftButtonText = "Cancel", string rightButtonText = "Confirm", string dataAsset = "Sprites\\UI\\Menus\\DialogBox", BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(text, position, size, dataAsset, parent, lifeTime)
        {
            LeftButton = new Button(new Vector2(-Size.X * 0.25f, (Size.Y + Button.defaultTexture.Height) * 0.5f), new Vector2(Math.Min(Button.defaultTexture.Width, Size.X * 0.5f), Button.defaultTexture.Height), leftButtonText, this);
            LeftButton.Size = new Vector2(Math.Min(LeftButton.Size.X, Size.X * 0.5f), LeftButton.Size.Y);
            LeftButton.OnSelect += leftButtonEvent;

            RightButton = new Button(new Vector2(Size.X * 0.25f, (Size.Y + Button.defaultTexture.Height) * 0.5f), new Vector2(Math.Min(Button.defaultTexture.Width, Size.X * 0.5f), Button.defaultTexture.Height), rightButtonText, this);
            RightButton.Size = new Vector2(Math.Min(RightButton.Size.X, Size.X * 0.5f), RightButton.Size.Y);
            RightButton.OnSelect += rightButtonEvent;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadContent()
        {
            base.LoadContent();

            LeftButton.LoadContent();
            RightButton.LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();

            LeftButton.Initialize();
            RightButton.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Active)
            {
                LeftButton.Update(gameTime);
                RightButton.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (Visible)
            {
                LeftButton.Draw(spriteBatch);
                RightButton.Draw(spriteBatch);
            }
        }

        public override void HandleInput()
        {
            base.HandleInput();

            if (Active)
            {
                LeftButton.HandleInput();
                RightButton.HandleInput();
            }
        }

        #endregion
    }
}
