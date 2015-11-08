using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Managers;
using _2DGameEngine.Maths;
using _2DGameEngine.Maths.Primitives;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnderSiege.Gameplay_Objects.Ship_Add_Ons;
using UnderSiegeData.Gameplay_Objects;

namespace UnderSiege.Gameplay_Objects
{
    public class Beam : GameObject
    {
        #region Properties and Fields

        public BeamData BeamData { get; private set; }
        public ShipBeamTurret ParentTurret { get; set; }
        public Line BeamLine { get; set; }

        #endregion

        public Beam(ShipBeamTurret parentTurret, string dataAsset = "")
            : base(dataAsset, parentTurret)
        {
            ParentTurret = parentTurret;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadContent()
        {
            base.LoadContent();

            Debug.Assert(BaseData != null);
            BeamData = BaseData as BeamData;

            Debug.Assert(BeamData != null);
            Colour = new Color(BeamData.BeamColour);
        }

        public override void AddCollider()
        {
            
        }

        public override void Initialize()
        {
            base.Initialize();

            Size = new Vector2(Size.X, ParentTurret.ShipTurretData.Range);
            LocalPosition = new Vector2(0, -Size.Y * 0.5f);

            float sinRot = (float)Math.Sin(WorldRotation);
            float cosRot = (float)Math.Cos(WorldRotation);
            float range = ParentTurret.ShipTurretData.Range;
            BeamLine = new Line(ParentTurret.WorldPosition, ParentTurret.WorldPosition + new Vector2(cosRot * range, -sinRot * range));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Size changes in beam turret so we need to move the beam so that it starts and ends at the correct place
            LocalPosition = new Vector2(0, -Size.Y * 0.5f);

            float sinRot = (float)Math.Sin(WorldRotation);
            float cosRot = (float)Math.Cos(WorldRotation);
            float range = ParentTurret.ShipTurretData.Range;
            BeamLine.EndPoint = ParentTurret.WorldPosition + new Vector2(sinRot * range, -cosRot * range);
        }

        public override void HandleInput()
        {
            
        }

        #endregion
    }
}