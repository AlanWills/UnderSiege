using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Cutscenes.Scripts
{
    public enum MoveCameraStyle { LERP, Linear }

    public class MoveCameraScript : Script
    {
        #region Properties and Fields

        private MoveCameraStyle MoveStyle { get; set; }
        private Vector2 TargetLocation { get; set; }
        private float Speed { get; set; }

        #endregion

        public MoveCameraScript(Vector2 targetLocation, MoveCameraStyle moveStyle, float speed = 2f, bool shouldUpdateGame = false, bool canRun = true)
            : base(shouldUpdateGame, canRun)
        {
            // Camera zero, zero is top left so need to account for this
            TargetLocation = targetLocation - ScreenManager.ScreenCentre;
            MoveStyle = moveStyle;
            Speed = speed;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadAndInit(ContentManager content)
        {
            
        }

        public override void Run(GameTime gameTime)
        {
            if (MoveStyle == MoveCameraStyle.LERP)
            {
                Camera.Position = new Vector2(MathHelper.Lerp(Camera.Position.X, TargetLocation.X, Speed * (float)gameTime.ElapsedGameTime.Milliseconds / 1000f), MathHelper.Lerp(Camera.Position.Y, TargetLocation.Y, Speed * (float)gameTime.ElapsedGameTime.Milliseconds / 1000f));
            }
            else if (MoveStyle == MoveCameraStyle.Linear)
            {
                Vector2 diff = TargetLocation - Camera.Position;
                diff.Normalize();
                Camera.Position += diff * Speed * (float)gameTime.ElapsedGameTime.Milliseconds / 1000f;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public override void DrawUI(SpriteBatch spriteBatch)
        {
            
        }

        public override void HandleInput()
        {
            
        }

        public override void CheckDone()
        {
            Done = (Camera.Position - TargetLocation).LengthSquared() <= 1f;
        }

        public override void PerformImmediately()
        {
            Camera.Position = TargetLocation;
        }

        #endregion
    }
}
