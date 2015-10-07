using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using _2DGameEngine.Maths;
using _2DGameEngine.Maths.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.UI;

namespace UnderSiege.Gameplay_Objects
{
    public class FiringArc : InGameUIObject
    {
        #region Properties and Fields

        public ShipTurret ParentTurret
        {
            get;
            private set;
        }

        public float ArcWidth
        {
            get { return ParentTurret.ShipTurretData.ArcWidth; }
        }

        #endregion

        public FiringArc(ShipTurret parentTurret, string dataAsset = ""/*"Sprites\\UI\\InGameUI\\FiringArc"*/)
            : base(dataAsset, parentTurret)
        {
            ParentTurret = parentTurret;
            Opacity = 0.15f;
        }

        #region Methods

        public bool ContainsPoint(Vector2 position)
        {
            //float angle = Trigonometry.GetAngleOfLineBetweenPositionAndTarget(ParentTurret.WorldPosition, position, false);
            //return MathUtils.FloatInRange(angle, WorldRotation - ArcWidth * 0.5f, WorldRotation + ArcWidth * 0.5f);
            return true;
        }

        #endregion

        #region Virtual Methods

        public override void Initialize()
        {
            base.Initialize();

            LocalPosition = new Vector2(0, -Size.Y * 0.5f);
        }

        #endregion
    }
}
