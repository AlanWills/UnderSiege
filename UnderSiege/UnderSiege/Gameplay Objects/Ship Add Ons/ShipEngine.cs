using _2DGameEngine.Game_Objects;
using _2DGameEngine.Managers;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.UI.In_Game_UI;
using UnderSiegeData.Gameplay_Objects;

namespace UnderSiege.Gameplay_Objects.Ship_Add_Ons
{
    public class ShipEngine : ShipAddOn
    {
        #region Properties and Fields

        public ShipEngineData ShipEngineData { get; set; }
        private EngineBlaze EngineBlaze { get; set; }

        #endregion

        public ShipEngine(Vector2 hardPointOffset, string dataAsset, Ship parent, bool addRigidBody = true)
            : base(hardPointOffset, dataAsset, parent, addRigidBody)
        {
            ParentShip = parent;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadContent()
        {
            base.LoadContent();

            ShipEngineData = AssetManager.GetData<ShipEngineData>(DataAsset);
            Ship parent = Parent as Ship;
            parent.TotalThrust += ShipEngineData.Thrust;
            // Parent = parent; ?? Do we need this??
        }

        public override void Initialize()
        {
            base.Initialize();

            EngineBlaze = new EngineBlaze(ParentShip, new Vector2(0, Size.Y * 0.5f), new Vector2(10, 50), "Sprites\\GameObjects\\FX\\EngineBlaze", 8, 1, 0.1f, false, true, this);
            EngineBlaze.LoadContent();
            EngineBlaze.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            EngineBlaze.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            EngineBlaze.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

        public override void Die()
        {
            base.Die();

            Ship parent = Parent as Ship;
            parent.TotalThrust -= ShipEngineData.Thrust;
        }

        #endregion
    }
}
