using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Game_Objects;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Gameplay_Objects;

namespace UnderSiege.UI.In_Game_UI
{
    public class EngineBlaze : AnimatedObject<InGameImage>
    {
        #region Properties and Fields

        private GameObject MovingObject { get; set; }

        #endregion

        public EngineBlaze(GameObject movingObject, Vector2 localPosition, Vector2 size, string dataAsset, int framesInX, int framesInY, float timePerFrame, bool isPlaying = false, bool continual = true, BaseObject parent = null)
            : base(localPosition, size, dataAsset, framesInX, framesInY, timePerFrame, isPlaying, continual, parent)
        {
            MovingObject = movingObject;
            Object.Opacity = 0;

            Active = false;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            bool valid = MovingObject.RigidBody.LinearVelocity.LengthSquared() > 1f;
            Active = valid;

            if (Active)
            {
                Object.Opacity = (float)MathHelper.Lerp(Object.Opacity, 1, (float)gameTime.ElapsedGameTime.Milliseconds / 1000f);
            }
            else
            {
                Object.Opacity = (float)MathHelper.Lerp(Object.Opacity, 0, (float)gameTime.ElapsedGameTime.Milliseconds / 1000f);
            }
        }

        #endregion
    }
}
