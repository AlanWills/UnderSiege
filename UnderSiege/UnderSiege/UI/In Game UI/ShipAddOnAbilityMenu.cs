using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Abilities.Object_Abilities;
using UnderSiege.Gameplay_Objects;
using UnderSiege.Screens;

namespace UnderSiege.UI.In_Game_UI
{
    public class ShipAddOnAbilityMenu : Menu
    {
        #region Properties and Fields

        public ShipAddOn ParentShipAddOn { get; private set; }

        public const float abilityImageSize = 50;
        private const int padding = 10;
        private const int columns = 2;

        #endregion

        public ShipAddOnAbilityMenu(Vector2 position,  ShipAddOn parentAddOn)
            : base(position, 0, 0, 0, 0, "Sprites\\UI\\Menus\\default", UnderSiegeGameplayScreen.HUD)
        {
            ParentShipAddOn = parentAddOn;
            Opacity = 0.75f;
            AddUI();
        }

        #region Methods

        private void AddUI()
        {
            int totalObjects = ParentShipAddOn.Abilities.Count;
            int totalRows = (int)Math.Ceiling((float)(totalObjects) / (float)(columns));
            Size = new Vector2(columns * (abilityImageSize + padding) + padding, totalRows * (abilityImageSize + padding) + padding);

            int counter = 0;
            foreach (AddOnAbility ability in ParentShipAddOn.Abilities)
            {
                int row = (counter / columns);
                int column = counter % columns;

                // Weird constants and multiplies are for padding purposes;
                AddOnAbilityUI abilityImage = new AddOnAbilityUI(ability, new Vector2(-Size.X * 0.5f + (column + 0.5f) * (abilityImageSize + padding) + 0.5f * padding, -Size.Y * 0.5f + (row + 0.5f) * (abilityImageSize + padding) + 0.5f * padding), new Vector2(abilityImageSize, abilityImageSize), this);

                AddUIObject(abilityImage, ability.AddOnAbilityData.DisplayName + " UI");

                counter++;
            }
        }

        #endregion

        #region Virtual Methods

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Our add on no longer exists so we should destroy this UI
            if (!ParentShipAddOn.ParentShip.AddOnExists(ParentShipAddOn))
            {
                Alive = false;
            }
            else
            {
                LocalPosition = Camera.GameToScreenCoords(ParentShipAddOn.WorldPosition - new Vector2(100, 0));
                LocalPosition = new Vector2((float)Math.Max(LocalPosition.X, 100), (float)Math.Max(LocalPosition.Y, 100));
            }
        }

        #endregion
    }
}
