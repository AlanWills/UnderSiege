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

        private Ship ParentShip { get; set; }

        #endregion

        public EngineBlaze(Ship parentShip, Vector2 localPosition, Vector2 size, string dataAsset, int framesInX, int framesInY, float timePerFrame, bool isPlaying = false, bool continual = true, BaseObject parent = null)
            : base(localPosition, size, dataAsset, framesInX, framesInY, timePerFrame, isPlaying, continual, parent)
        {
            ParentShip = parentShip;

            Visible = false;
            Active = false;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            bool valid = ParentShip.RigidBody.LinearVelocity.LengthSquared() > 1f;
            Active = valid;
            Visible = valid;

            // Do opacity editing here
        }

        #endregion
    }
}
