using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Managers;
using _2DGameEngine.Maths;
using _2DGameEngine.Maths.Primitives;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
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

        public Beam(ShipBeamTurret parentTurret, string dataAsset = "", bool addRigidBody = true)
            : base(dataAsset, parentTurret, addRigidBody)
        {
            ParentTurret = parentTurret;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadContent()
        {
            base.LoadContent();

            BeamData = AssetManager.GetData<BeamData>(DataAsset);
            Colour = new Color(BeamData.BeamColour);
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

            LocalPosition = new Vector2(0, -Size.Y * 0.5f);

            float sinRot = (float)Math.Sin(WorldRotation);
            float cosRot = (float)Math.Cos(WorldRotation);
            float range = ParentTurret.ShipTurretData.Range;
            BeamLine.EndPoint = ParentTurret.WorldPosition + new Vector2(sinRot * range, -cosRot * range);
        }

        #endregion
    }
}