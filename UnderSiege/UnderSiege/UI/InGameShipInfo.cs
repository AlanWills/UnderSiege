using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Gameplay_Objects;
using UnderSiegeData.Gameplay_Objects;

namespace UnderSiege.UI
{
    public class InGameShipInfo : Menu
    {
        #region Properties and Fields

        private PlayerShip playerShip;
        public PlayerShip PlayerShip 
        {
            get { return playerShip; }
            set
            {
                playerShip = value;

                if (playerShip != null)
                {
                    Visible = true;
                    Active = true;
                    BuildUI();
                }
                else
                {
                    Visible = false;
                    Active = false;
                    visibleTimer = 0;
                }
            }
        }

        private const float padding = 5f;
        private const float maxVisibleTime = 5f;
        private float visibleTimer = 0f;

        #endregion

        public InGameShipInfo(PlayerShip playerShip, Vector2 position, Vector2 size, string dataAsset= "Sprites\\UI\\Menus\\default", BaseObject parent = null)
            : base(position, size, 0, 0, 0, 0, dataAsset, parent)
        {
            PlayerShip = playerShip;
            Opacity = 0.5f;
        }

        #region Methods
        
        private void BuildUI()
        {
            UIManager.Clear();
            ShipData shipData = playerShip.ShipData;

            Image image = new Image(new Vector2(0, -Size.Y * 0.25f), shipData.TextureAsset, this);
            AddUIObject(image, "Ship Image", true);

            Label label = new Label("Name: " + shipData.DisplayName, new Vector2(0, (image.Size.Y + SpriteFont.LineSpacing) * 0.5f + padding), Color.White, image);
            AddUIObject(label, "Ship Name", true);

            Label hullHealth = new Label("Hull Strength: " + shipData.Health.ToString(), new Vector2(0, SpriteFont.LineSpacing + padding), Color.White, label);
            AddUIObject(hullHealth, "Ship Hull Health", true);
        }

        // Want to do this instead, but get problems when setting the texture of the image.  This is quick for now, but if optimisation is needed, we can definitely improve this
        /*private void RebuildUI()
        {
            // We have no UI so nothing to rebuild
            if (UIManager.Dictionary.Count == 0)
                return;

            Image image = UIManager.GetItem<Image>("Ship Image");
            image.Texture = PlayerShip.Texture;

            Label label = UIManager.GetItem<Label>("Ship Name");
            label.Text = "Name: " + PlayerShip.ShipData.DisplayName;

            Label hullHealth = UIManager.GetItem<Label>("Ship Hull Health");
            hullHealth.Text = "Hull Strength: " + PlayerShip.ShipData.HullHealth.ToString();
        }*/

        #endregion

        #region Virtual Methods

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            visibleTimer += (float)gameTime.ElapsedGameTime.Milliseconds / 1000f;
            if (visibleTimer >= maxVisibleTime)
            {
                Visible = false;
                Active = false;
                visibleTimer = 0;
            }
        }

        #endregion
    }
}
