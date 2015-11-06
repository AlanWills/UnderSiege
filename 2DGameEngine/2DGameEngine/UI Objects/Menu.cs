using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using _2DGameEngine.Maths;
using _2DGameEngine.Physics_Components.Colliders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2DGameEngine.UI_Objects
{
    public class Menu : ScreenUIObject
    {
        #region Properties and Fields

        public BaseObjectManager<UIObject> UIManager
        {
            get;
            protected set;
        }

        private Rectangle scissorRectangle;
        public Rectangle ScissorRectangle
        {
            get
            {
                if (scissorRectangle == Rectangle.Empty)
                {
                    scissorRectangle = DestinationRectangle;
                }

                return scissorRectangle;
            }
            set { scissorRectangle = value; }
        }

        public bool EnableScissoring { get; set; }

        private RasterizerState rasterizerState = new RasterizerState() { ScissorTestEnable = true };
        private Vector2 xPaddingVector, yPaddingVector;

        #endregion

        public static string defaultBackgroundAsset = "Sprites\\UI\\Menus\\default";
        private static Texture2D defaultBackground;

        public Menu(Vector2 position, int xLeftScissorRectanglePadding = 0, int xRightScissorRectanglePadding = 0, int yTopScissorRectanglePadding = 0, int yBottomScissorRectanglePadding = 0, string dataAsset = "Sprites\\UI\\Menus\\default", BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(position, dataAsset, parent, lifeTime)
        {
            UIManager = new BaseObjectManager<UIObject>();
            xPaddingVector = new Vector2(xLeftScissorRectanglePadding, xRightScissorRectanglePadding);
            yPaddingVector = new Vector2(yTopScissorRectanglePadding, yBottomScissorRectanglePadding);
        }

        public Menu(Vector2 position, Vector2 size, int xLeftScissorRectanglePadding = 0, int xRightScissorRectanglePadding = 0, int yTopScissorRectanglePadding = 0, int yBottomScissorRectanglePadding = 0, string dataAsset = "Sprites\\UI\\Menus\\default", BaseObject parent = null)
            : base(position, size, dataAsset, parent)
        {
            UIManager = new BaseObjectManager<UIObject>();
            xPaddingVector = new Vector2(xLeftScissorRectanglePadding, xRightScissorRectanglePadding);
            yPaddingVector = new Vector2(yTopScissorRectanglePadding, yBottomScissorRectanglePadding);
        }

        #region Methods

        new public static void LoadPresetContent()
        {
            defaultBackground = AssetManager.GetTexture(defaultBackgroundAsset);
        }

        #region UIObject Addition

        public void AddUIObject(UIObject uiObject, string tag, bool load = false, bool linkWithUIManager = true)
        {
            if (load)
            {
                uiObject.LoadContent();
                uiObject.Initialize();
            }

            UIManager.AddObject(uiObject, tag, load, linkWithUIManager);
        }

        public void AddUIObject(UIObject uiObject, string tag, Vector2 relativePosition, bool load = false, bool linkWithUIManager = true)
        {
            if (load)
            {
                uiObject.LoadContent();
                uiObject.Initialize();
            }

            uiObject.LocalPosition = relativePosition;
            UIManager.AddObject(uiObject, tag, load, linkWithUIManager);
        }

        #endregion

        public void SetPosition(Vector2 newPosition)
        {
            foreach (UIObject uiObject in UIManager.Values)
                uiObject.LocalPosition += newPosition;

            LocalPosition = newPosition;
        }

        private void CheckAttachedObjectsVisibility()
        {
            foreach (UIObject uiObject in UIManager.Values)
                CheckObjectIsVisible(uiObject);
        }

        private void CheckObjectIsVisible(BaseObject uiObject)
        {
            // uiObject.Visible = ScissorRectangle.Intersects(uiObject.Coll);
        }

        private void ShiftDown()
        {
            foreach (UIObject uiObject in UIManager.Values)
            {
                uiObject.LocalPosition -= new Vector2(0, 1);
                CheckObjectIsVisible(uiObject);
            }
        }

        private void ShiftUp()
        {
            foreach (UIObject uiObject in UIManager.Values)
            {
                uiObject.LocalPosition += new Vector2(0, 1);
                CheckObjectIsVisible(uiObject);
            }
        }

        #endregion

        #region Virtual Methods

        public override void LoadContent()
        {
            base.LoadContent();

            UIManager.LoadContent();

            ScissorRectangle = new Rectangle(
                                    (int)(WorldPosition.X - DestinationRectangle.Width * 0.5f + xPaddingVector.X), 
                                    (int)(WorldPosition.Y - DestinationRectangle.Height * 0.5f + yPaddingVector.X), 
                                    (int)(DestinationRectangle.Width - xPaddingVector.X - xPaddingVector.Y),
                                    (int)(DestinationRectangle.Height - yPaddingVector.X - yPaddingVector.Y));

            if (EnableScissoring)
                CheckAttachedObjectsVisibility();
        }

        public override void AddCollider()
        {
            // Don't need a collider for menus
        }

        public override void Initialize()
        {
            base.Initialize();

            if (Texture == null)
                Texture = defaultBackground;

            UIManager.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Active)
            {
                UIManager.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            /*spriteBatch.End();

            // To create the scissoring effect of objects when they scroll up and down
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, rasterizerState);
            spriteBatch.GraphicsDevice.ScissorRectangle = ScissorRectangle;

            UIManager.Draw(spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin();*/

            if (Visible)
            {
                UIManager.Draw(spriteBatch);
            }
        }

        public override void DrawInGameUI(SpriteBatch spriteBatch)
        {
            base.DrawInGameUI(spriteBatch);

            UIManager.DrawInGameUI(spriteBatch);
        }

        public override void DrawScreenUI(SpriteBatch spriteBatch)
        {
            base.DrawScreenUI(spriteBatch);

            UIManager.DrawScreenUI(spriteBatch);
        }

        public override void HandleInput()
        {
            // We want the menu to only be deactivated by another UIObject - like a button or something

            if (Active)
            {
                UIManager.HandleInput();
            }
        }

        protected override void IfMouseOver()
        {
            base.IfMouseOver();

            if (EnableScissoring)
            {
                if (InputHandler.KeyDown(Keys.Down))
                    ShiftDown();
                else if (InputHandler.KeyDown(Keys.Up))
                    ShiftUp();
            }
        }

        public override void Hide()
        {
            base.Hide();

            if (UIManager != null)
            {
                foreach (UIObject uiObject in UIManager.Values)
                {
                    uiObject.Visible = false;
                }
            }
        }

        public override void Show()
        {
            base.Hide();

            if (UIManager != null)
            {
                foreach (UIObject uiObject in UIManager.Values)
                {
                    uiObject.Visible = true;
                }
            }
        }

        public override void Deactivate()
        {
            base.Hide();

            if (UIManager != null)
            {
                foreach (UIObject uiObject in UIManager.Values)
                {
                    uiObject.Active = false;
                }
            }
        }

        public override void Activate()
        {
            base.Hide();

            if (UIManager != null)
            {
                foreach (UIObject uiObject in UIManager.Values)
                {
                    uiObject.Active = true;
                }
            }
        }

        #endregion
    }
}
