using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Extra_Components
{
    public static class GameMouse
    {
        #region Properties and Fields

        public static Vector2 ScreenPosition
        {
            get { return new Vector2(CurrentMouseState.X, CurrentMouseState.Y); }
        }

        public static Vector2 InGamePosition
        {
            get { return Camera.ScreenToGameCoords(ScreenPosition); }
        }

        public static Vector2 LastLeftClickedPosition
        {
            get;
            private set;
        }

        public static Vector2 LastMiddleClickedPosition
        {
            get;
            private set;
        }

        public static Vector2 LastRightClickedPosition
        {
            get;
            private set;
        }

        public static bool IsLeftClicked
        {
            get;
            private set;
        }

        public static bool IsMiddleClicked
        {
            get;
            private set;
        }

        public static bool IsRightClicked
        {
            get;
            private set;
        }

        private static MouseState PreviousMouseState
        {
            get;
            set;
        }

        private static MouseState CurrentMouseState
        {
            get;
            set;
        }

        public static bool Enabled
        {
            get;
            set;
        }

        private static bool visible;
        public static bool Visible
        {
            get { return Texture != null && visible; }
            set { visible = value; }
        }

        public static bool MouseAffectsCamera { get; set; }
        
        private static Texture2D Texture
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public static void SetUpMouse(string mouseTextureAsset = "Sprites\\UI\\Mouse\\default")
        {
            CurrentMouseState = Mouse.GetState();
            Texture = ScreenManager.Content.Load<Texture2D>(mouseTextureAsset);

            IsLeftClicked = false;
            IsMiddleClicked = false;
            IsRightClicked = false;
            Enabled = true;
            Visible = true;

            Flush();
        }

        public static void Flush()
        {
            CurrentMouseState = PreviousMouseState;
            IsLeftClicked = false;
            IsMiddleClicked = false;
            IsRightClicked = false;
        }

        public static void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                PreviousMouseState = CurrentMouseState;
                CurrentMouseState = Mouse.GetState();

                if (CurrentMouseState.LeftButton == ButtonState.Released && PreviousMouseState.LeftButton == ButtonState.Pressed)
                {
                    IsLeftClicked = true;
                    LastLeftClickedPosition = InGamePosition;
                }
                else
                {
                    IsLeftClicked = false;
                }

                if (CurrentMouseState.MiddleButton == ButtonState.Released && PreviousMouseState.MiddleButton == ButtonState.Pressed)
                {
                    IsMiddleClicked = true;
                    LastMiddleClickedPosition = InGamePosition;
                }
                else
                {
                    IsMiddleClicked = false;
                }

                if (CurrentMouseState.RightButton == ButtonState.Released && PreviousMouseState.RightButton == ButtonState.Pressed)
                {
                    IsRightClicked = true;
                    LastRightClickedPosition = InGamePosition;
                }
                else
                {
                    IsRightClicked = false;
                }

                if (ScreenManager.Camera.CameraMode == CameraMode.Free)
                {
                    if (MouseAffectsCamera)
                        UpdateCamera(gameTime);
                }
            }
        }

        // A function to change the position of the MANUAL camera if the mouse is towards the edge of the screen.
        private static void UpdateCamera(GameTime gameTime)
        {
            if (Texture != null)
            {
                int threshold = 5;
                Vector2 deltaPos = Vector2.Zero;

                // Mouse is within thresold of the right hand side of the screen so we need to move the camera left
                if (ScreenManager.Viewport.Width - ScreenPosition.X < threshold)
                {
                    deltaPos.X += ScreenManager.Camera.Speed;
                }

                // Mouse is within threshold of the left hand side of the screen so we need to move the camera right
                if (ScreenPosition.X - ScreenManager.Viewport.X < threshold)
                {
                    deltaPos.X -= ScreenManager.Camera.Speed;
                }

                // Mouse is within threshold of the top of the screen so we need to move the camera down
                if (ScreenPosition.Y - ScreenManager.Viewport.Y < threshold)
                {
                    deltaPos.Y -= ScreenManager.Camera.Speed;
                }

                // Mouse is within threshold of the bottom of the screen so we need to move the camera up
                if (ScreenManager.Viewport.Height - ScreenPosition.Y < threshold + Texture.Height / 2)
                {
                    deltaPos.Y += ScreenManager.Camera.Speed;
                }

                Camera.Position += (deltaPos * (float)gameTime.ElapsedGameTime.Milliseconds / 1000f);
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                spriteBatch.Draw(Texture, ScreenPosition, Color.White);
            }
        }

        #endregion

        #region Virtual Methods

        #endregion
    }
}
