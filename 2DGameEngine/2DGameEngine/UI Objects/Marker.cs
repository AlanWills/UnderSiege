using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using _2DGameEngine.Maths;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.UI_Objects
{
    public class Marker : ScreenUIObject
    {
        #region Properties and Fields

        private Vector2 TargetPosition { get; set; }

        #endregion

        public Marker(Vector2 screenPosition, Vector2 targetPosition, string dataAsset, BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(screenPosition, dataAsset, parent, lifeTime)
        {
            TargetPosition = targetPosition;
            Opacity = 0.8f;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            LocalRotation = Trigonometry.GetAngleOfLineBetweenPositionAndTarget(Camera.ScreenToGameCoords(WorldPosition), TargetPosition) - Parent.WorldRotation;
        }

        #endregion
    }
}
