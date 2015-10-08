using _2DGameEngine.Abstract_Object_Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Game_Objects
{
    // Want this class to drive the position of the object T, but nothing else
    public class AnimatedObject<T> : BaseObject where T : BaseObject
    {
        #region Properties

        public Animation Animation
        {
            get;
            private set;
        }

        public T Object { get; private set; }

        #endregion

        public AnimatedObject(string dataAsset, int framesInX, int framesInY, float timePerFrame, bool isPlaying = false, bool continual = true, BaseObject parent = null)
            : base("", parent)
        {
            Animation = new Animation(framesInX, framesInY, timePerFrame, isPlaying, continual);
            Object = (T)Activator.CreateInstance(typeof(T), dataAsset, this);
        }

        public AnimatedObject(Vector2 localPosition, string dataAsset, int framesInX, int framesInY, float timePerFrame, bool isPlaying = false, bool continual = true, BaseObject parent = null)
            : base(localPosition, "", parent)
        {
            Animation = new Animation(framesInX, framesInY, timePerFrame, isPlaying, continual);
            Object = (T)Activator.CreateInstance(typeof(T), dataAsset, this);
        }

        public AnimatedObject(Vector2 localPosition, Vector2 size, string dataAsset, int framesInX, int framesInY, float timePerFrame, bool isPlaying = false, bool continual = true, BaseObject parent = null)
            : base(localPosition, "", parent)
        {
            Animation = new Animation(framesInX, framesInY, timePerFrame, isPlaying, continual);
            Object = (T)Activator.CreateInstance(typeof(T), Vector2.Zero, size, dataAsset, this);
        }

        public AnimatedObject(T objectToAnimate, int framesInX, int framesInY, float timePerFrame, bool isPlaying = false, bool continual = true)
            : base(objectToAnimate.LocalPosition, "", objectToAnimate.Parent)
        {
            Animation = new Animation(framesInX, framesInY, timePerFrame, isPlaying, continual);
            Object = objectToAnimate;
            Object.LocalPosition = Vector2.Zero;
            Object.Parent = this;
        }

        public override void LoadContent()
        {
            Object.LoadContent();
            Object.Size *= new Vector2(Animation.Frames.X, Animation.Frames.Y);
        }

        public override void Initialize()
        {
            Object.Initialize();
            Animation.SetFrameDimensions(Object.Texture);
            SetSourceRectangleBasedOnFrame(Animation.CurrentFrame);

            Object.LocalPosition += new Vector2(Object.Texture.Width * 0.5f, 0);
        }

        // frame is 0 indexed
        private void SetSourceRectangleBasedOnFrame(int frame)
        {
            int cols = Animation.Frames.X;

            Object.SourceRectangle = new Rectangle(
                                    (int)((frame % cols) * Animation.FrameDimensions.X),
                                    (int)((frame / cols) * Animation.FrameDimensions.Y),
                                    (int)Animation.FrameDimensions.X,
                                    (int)Animation.FrameDimensions.Y);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);            

            if (Object.Active)
            {
                Object.Update(gameTime);
                Animation.Update(gameTime);
                SetSourceRectangleBasedOnFrame(Animation.CurrentFrame);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Animation.IsPlaying)
                Object.Draw(spriteBatch);
        }

        public override void DrawInGameUI(SpriteBatch spriteBatch)
        {
            Object.DrawInGameUI(spriteBatch);
        }

        public override void HandleInput()
        {
            Object.HandleInput();
        }

        public override void Show()
        {
            base.Show();

            // This check is because we become visible before we create our object
            if (Object != null)
                Object.Visible = true;
        }

        public override void Hide()
        {
            base.Hide();

            Object.Visible = false;
        }

        public override void Activate()
        {
            base.Activate();

            // This check is because we activate before we create our animation
            if (Animation != null)
                Play();

            // This check is because we activate before we create our object
            if (Object != null)
                Object.Active = true;
        }

        public override void Deactivate()
        {
            base.Deactivate();

            Stop();
            Object.Active = false;
        }

        public override void Die()
        {
            base.Die();

            Object.Alive = false;
        }

        public void Play()
        {
            Animation.IsPlaying = true;
        }

        public void Stop()
        {
            Animation.IsPlaying = false;
        }
    }
}
