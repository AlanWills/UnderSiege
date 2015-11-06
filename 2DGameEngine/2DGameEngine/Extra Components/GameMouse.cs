using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Extra_Components
{
    public class GameMouse : UIObject
    {
        #region Properties and Fields

        public Vector2 InGamePosition
        {
            get { return InGameMouse.LocalPosition; }
        }

        public Vector2 LastLeftClickedPosition
        {
            get;
            private set;
        }

        public Vector2 LastMiddleClickedPosition
        {
            get;
            private set;
        }

        public Vector2 LastRightClickedPosition
        {
            get;
            private set;
        }

        public bool IsLeftClicked
        {
            get;
            private set;
        }

        public bool IsMiddleClicked
        {
            get;
            private set;
        }

        public bool IsRightClicked
        {
            get;
            private set;
        }

        private MouseState PreviousMouseState
        {
            get;
            set;
        }

        private MouseState CurrentMouseState
        {
            get;
            set;
        }

        // This is just to hold the in game transform of the mouse and so we can parent objects to it
        public InGameUIObject InGameMouse { get; private set; }

        public bool MouseAffectsCamera { get; set; }
        
        private float clickDelayTimer = 0.2f;

        #endregion
        
        public GameMouse(string dataAsset = "Sprites\\UI\\Mouse\\default", BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(dataAsset, parent, lifeTime)
        {
            CurrentMouseState = Mouse.GetState();

            IsLeftClicked = false;
            IsMiddleClicked = false;
            IsRightClicked = false;

            InGameMouse = new InGameUIObject("", parent, lifeTime);
            Flush();
        }

        #region Methods

        public void Flush()
        {
            CurrentMouseState = PreviousMouseState;
            IsLeftClicked = false;
            IsMiddleClicked = false;
            IsRightClicked = false;
        }

        // A function to change the position of the MANUAL camera if the mouse is towards the edge of the screen.
        private void UpdateCamera(GameTime gameTime)
        {
            Debug.Assert(Texture != null);
            int threshold = 5;
            Vector2 deltaPos = Vector2.Zero;

            // Mouse is within thresold of the right hand side of the screen so we need to move the camera left
            if (ScreenManager.Viewport.Width - LocalPosition.X < threshold)
            {
                deltaPos.X += ScreenManager.Camera.Speed;
            }

            // Mouse is within threshold of the left hand side of the screen so we need to move the camera right
            if (LocalPosition.X - ScreenManager.Viewport.X < threshold)
            {
                deltaPos.X -= ScreenManager.Camera.Speed;
            }

            // Mouse is within threshold of the top of the screen so we need to move the camera down
            if (LocalPosition.Y - ScreenManager.Viewport.Y < threshold)
            {
                deltaPos.Y -= ScreenManager.Camera.Speed;
            }

            // Mouse is within threshold of the bottom of the screen so we need to move the camera up
            if (ScreenManager.Viewport.Height - LocalPosition.Y < threshold + Texture.Height / 2)
            {
                deltaPos.Y += ScreenManager.Camera.Speed;
            }

            Camera.Position = Vector2.Add(Camera.Position, Vector2.Multiply(deltaPos, (float)gameTime.ElapsedGameTime.Milliseconds / 1000f));
        }

        #endregion

        #region Virtual Methods

        public override void Update(GameTime gameTime)
        {
            if (Active)
            {
                PreviousMouseState = CurrentMouseState;
                CurrentMouseState = Mouse.GetState();
                LocalPosition = new Vector2(CurrentMouseState.X, CurrentMouseState.Y);
                InGameMouse.LocalPosition = Camera.ScreenToGameCoords(LocalPosition);

                if (ScreenManager.Camera.CameraMode == CameraMode.Free)
                {
                    if (MouseAffectsCamera)
                        UpdateCamera(gameTime);
                }
            }
        }

        public override void HandleInput()
        {
            base.HandleInput();

            if (CurrentMouseState.LeftButton == ButtonState.Released && PreviousMouseState.LeftButton == ButtonState.Pressed && clickDelayTimer >= 0.2f)
            {
                IsLeftClicked = true;
                LastLeftClickedPosition = InGamePosition;
                clickDelayTimer = 0;
            }
            else
            {
                IsLeftClicked = false;
                clickDelayTimer += 0.03f;
            }

            if (CurrentMouseState.MiddleButton == ButtonState.Released && PreviousMouseState.MiddleButton == ButtonState.Pressed && clickDelayTimer >= 0.2f)
            {
                IsMiddleClicked = true;
                LastMiddleClickedPosition = InGamePosition;
                clickDelayTimer = 0;
            }
            else
            {
                IsMiddleClicked = false;
                clickDelayTimer += 0.03f;
            }

            if (CurrentMouseState.RightButton == ButtonState.Released && PreviousMouseState.RightButton == ButtonState.Pressed && clickDelayTimer >= 0.2f)
            {
                IsRightClicked = true;
                LastRightClickedPosition = InGamePosition;
                clickDelayTimer = 0;
            }
            else
            {
                IsRightClicked = false;
                clickDelayTimer += 0.03f;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Changed the origin so the mouse is drawn so it's position is at the top left
            if (Visible)
            {
                Debug.Assert(Texture != null);
                spriteBatch.Draw(Texture, DestinationRectangle, SourceRectangle, Colour * Opacity, (float)WorldRotation, Vector2.Zero, SpriteEffects.None, 0);

                IfVisible();
            }
        }

        #endregion
    }
}
