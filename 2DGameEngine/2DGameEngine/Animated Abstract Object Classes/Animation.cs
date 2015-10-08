using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Game_Objects
{
    public class Animation
    {
        #region Animation Properties

        // The pixel dimensions of one frame of the animation
        public Vector2 FrameDimensions
        {
            get;
            set;
        }

        public Point Frames
        {
            get;
            set;
        }

        public int CurrentFrame
        {
            get;
            private set;
        }

        private int DefaultFrame
        {
            get;
            set;
        }

        public float TimePerFrame
        {
            get;
            set;
        }

        public bool IsPlaying
        {
            get;
            set;
        }

        public bool Continual
        {
            get;
            set;
        }

        public bool Finished
        {
            get;
            private set;
        }

        private float currentTimeOnFrame = 0;

        #endregion

        public Animation(int framesInX, int framesInY, float timePerFrame, bool isPlaying = true, bool continual = true, int defaultFrame = 0)
        {
            Frames = new Point(framesInX, framesInY);
            TimePerFrame = timePerFrame;
            IsPlaying = isPlaying;
            Continual = continual;
            DefaultFrame = defaultFrame;
            CurrentFrame = defaultFrame;
        }

        #region Methods

        public void SetFrameDimensions(Texture2D texture)
        {
            FrameDimensions = new Vector2(texture.Width / Frames.X, texture.Height / Frames.Y);
        }

        public void Update(GameTime gameTime)
        {
            if (IsPlaying)
            {
                currentTimeOnFrame += (float)(gameTime.ElapsedGameTime.Milliseconds) / 1000;

                if (currentTimeOnFrame >= TimePerFrame)
                {
                    CurrentFrame++;

                    // If the animation should only play once, we remove it once it reaches the last frame
                    if (!Continual)
                    {
                        if (CurrentFrame == Frames.X * Frames.Y - 1)
                        {
                            Finished = true;
                        }
                    }
                    else
                    {
                        CurrentFrame %= Frames.X * Frames.Y;
                    }

                    currentTimeOnFrame = 0;
                }
            }
            else
            {
                CurrentFrame = DefaultFrame;
            }
        }

        #endregion
    }
}
