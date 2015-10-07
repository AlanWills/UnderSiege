using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using _2DGameEngine.Maths.Primitives;
using _2DGameEngine.Physics_Components.Colliders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Gameplay_Objects;

namespace UnderSiege.UI
{
    public class HardPointUI : InGameUIObject
    {
        #region Properties and Fields

        public Vector2 HardPoint { get; private set; }
        public static float HardPointDimension { get { return 30.0f; } }

        #endregion

        public HardPointUI(BaseObject parent, Vector2 hardPoint, string dataAsset = "Sprites\\UI\\InGameUI\\HardPointUI")
            : base(hardPoint, dataAsset, parent)
        {
            HardPoint = hardPoint;
            Opacity = 0.35f;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void Initialize()
        {
            base.Initialize();

            Size = new Vector2(HardPointDimension, HardPointDimension);
        }

        public override void AddCollider()
        {
            Collider = new RectangleCollider(this, HardPointDimension, HardPointDimension);
        }

        #endregion
    }
}