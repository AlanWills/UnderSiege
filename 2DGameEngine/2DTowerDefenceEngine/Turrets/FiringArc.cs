using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DTowerDefenceEngine.Turrets
{
    public class FiringArc : UIObject
    {
        #region Properties and Fields

        private Turret ParentTurret
        {
            get;
            set;
        }

        public float ArcWidth
        {
            get { return ParentTurret.TurretData.ArcWidth; }
        }

        public float ArcLength
        {
            get { return ParentTurret.TurretData.Range; }
        }

        // Fixed value for the turret's firing arc orientation
        public float Orientation
        {
            get { return ParentTurret.Orientation; }
        }

        #endregion

        public FiringArc(Turret parentTurret, string dataAsset = "")
            : base(parentTurret.WorldPosition, dataAsset, parentTurret)
        {
            ParentTurret = parentTurret;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        #endregion
    }
}
