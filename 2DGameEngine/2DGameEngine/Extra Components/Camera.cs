using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Extra_Components
{
    public enum CameraMode { Free, Follow, Fixed }

    public class Camera
    {
        #region Properties and Fields

        private static Vector2 position;
        public static Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        private float speed;
        public float Speed
        {
            get { return speed; }
            set
            {
                speed = (float)MathHelper.Clamp(value, 100f, 1000.0f);
            }
        }

        private static float zoom;
        public static float Zoom
        {
            get { return zoom; }
            set
            {
                float diffInZoom = value - zoom;
                Position += diffInZoom * ScreenManager.ScreenCentre;
                zoom = value;
            }
        }

        // Change this to use the position
        public Rectangle ViewportRectangle
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, (int)(ScreenManager.Viewport.Width / Zoom), (int)(ScreenManager.Viewport.Height / Zoom)); }
        }

        public Matrix Transformation
        {
            get { return Matrix.CreateScale(Zoom) * Matrix.CreateTranslation(new Vector3(-position, 0)); }
        }

        public CameraMode CameraMode
        {
            get;
            set;
        }

        public GameObject FocusedObject { get; set; }

        private const float zoomIncrement = 0.25f;

        #endregion

        public Camera()
        {
            speed = 500f;
            Zoom = 1f;
        }

        public Camera(Vector2 position)
            : this()
        {
            Camera.Position = position;
        }

        #region Methods

        public void Update(GameTime gameTime)
        {
            if (CameraMode == CameraMode.Fixed)
                return;

            if (InputHandler.KeyReleased(Keys.PageUp))
            {
                ZoomIn();
            }
            else if (InputHandler.KeyReleased(Keys.PageDown))
            {
                ZoomOut();
            }

            if (CameraMode == CameraMode.Follow)
            {
                if (FocusedObject != null)
                {
                    Position = FocusedObject.WorldPosition - new Vector2(ViewportRectangle.Width, ViewportRectangle.Height) * 0.5f;
                }
            }

            Vector2 diff = Vector2.Zero;

            if (InputHandler.KeyDown(Keys.Left))
                diff.X -= speed;
            else if (InputHandler.KeyDown(Keys.Right))
                diff.X += speed;

            if (InputHandler.KeyDown(Keys.Up))
                diff.Y -= speed;
            else if (InputHandler.KeyDown(Keys.Down))
                diff.Y += speed;

            if (diff != Vector2.Zero)
            {
                diff.Normalize();
                position += diff * speed * (float)gameTime.ElapsedGameTime.Milliseconds / 1000f;

                // LockCamera();
            }
        }

        private void ZoomIn()
        {
            Zoom += zoomIncrement;

            if (Zoom > 3f)
            {
                Zoom = 3f;
                return;
            }
        }

        private void ZoomOut()
        {
            Zoom -= zoomIncrement;

            if (Zoom < zoomIncrement)
            {
                Zoom = zoomIncrement;
                return;
            }
        }

        // This function prevents a camera bug when zooming out after being zoomed in.
        private void SnapToPosition(Vector2 newPosition)
        {
            position.X = newPosition.X - ViewportRectangle.Width * 0.5f;
            position.Y = newPosition.Y - ViewportRectangle.Height * 0.5f;

            // LockCamera();
        }

        /*public void LockCamera()
        {
            position.X = MathHelper.Clamp(Position.X, 0, TileMap.WidthInPixels * Zoom - ViewportRectangle.Width);
            position.Y = MathHelper.Clamp(Position.Y, 0, TileMap.HeightInPixels * Zoom - ViewportRectangle.Height);
        }*/

        /*public void LockToSprite(AnimatedSprite sprite)
        {
            position.X = (sprite.Position.X + sprite.Width * 0.5f) * Zoom - ViewportRectangle.Width * 0.5f;
            position.Y = (sprite.Position.Y + sprite.Height * 0.5f) * Zoom - ViewportRectangle.Height * 0.5f;

            LockCamera();
        }*/

        public void ToggleCameraMode()
        {
            if (CameraMode == CameraMode.Follow)
                CameraMode = CameraMode.Free;
            else if (CameraMode == CameraMode.Free)
                CameraMode = CameraMode.Follow;
        }

        public static Vector2 ScreenToGameCoords(Vector2 screenPosition)
        {
            return Position - (Zoom - 1) * ScreenManager.ScreenCentre + ScreenManager.ScreenCentre + (screenPosition - ScreenManager.ScreenCentre) / Zoom;
        }

        public static Vector2 GameToScreenCoords(Vector2 gamePosition)
        {
            return Position - (Zoom - 1) * ScreenManager.ScreenCentre + ScreenManager.ScreenCentre + (gamePosition - ScreenManager.ScreenCentre) * Zoom;
        }

        #endregion
    }
}
