using _2DGameEngine.Abstract_Object_Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.UI_Objects
{
    public class DialogBox : PictureBox
    {
        #region Properties and Fields

        public Label Label { get; set; }

        #endregion

        public DialogBox(string text, Vector2 position, string dataAsset = "Sprites\\UI\\Menus\\DialogBox", BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(position, SpriteFont.MeasureString(text) + new Vector2(20, 10), dataAsset, parent, lifeTime)
        {
            Label = new Label(text, Vector2.Zero, Color.White, this, lifeTime);
        }

        public DialogBox(string text, Vector2 position, Vector2 size, string dataAsset = "Sprites\\UI\\Menus\\DialogBox", BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(position, size, dataAsset, parent, lifeTime)
        {
            Label = new Label(text, Vector2.Zero, Color.White, this, lifeTime);
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadContent()
        {
            base.LoadContent();

            Label.LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();

            Label.Initialize();
            Opacity = 0.25f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Active)
            {
                Label.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (Visible)
            {
                Label.Draw(spriteBatch);
            }
        }

        public override void HandleInput()
        {
            base.HandleInput();

            if (Active)
            {
                Label.HandleInput();
            }
        }

        public override void Hide()
        {
            base.Hide();

            Label.Visible = false;
        }

        public override void Show()
        {
            base.Show();

            if (Label != null)
                Label.Visible = true;
        }

        public override void Activate()
        {
            base.Activate();

            if (Label != null)
                Label.Active = true;
        }

        public override void Deactivate()
        {
            base.Deactivate();

            Label.Active = false;
        }

        #endregion
    }
}
