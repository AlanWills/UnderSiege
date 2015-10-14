using _2DGameEngine.Abstract_Object_Classes;
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
    public class ShipAddOnAbilityUI : Menu
    {
        #region Properties and Fields

        private ShipAddOn ParentShipAddOn { get; set; }

        private const float abilityImageSize = 50;
        private const int padding = 10;
        private const int columns = 2;

        #endregion

        public ShipAddOnAbilityUI(Vector2 position,  ShipAddOn parentAddOn)
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
            int totalRows = 1 + (totalObjects / columns);
            Size = new Vector2(columns * (abilityImageSize + padding) + padding, totalRows * (abilityImageSize + padding) + padding);

            int counter = 0;
            foreach (AddOnAbility ability in ParentShipAddOn.Abilities)
            {
                int row = (counter / columns);
                int column = counter % columns;

                // Weird constants and multiplies are for padding purposes;
                Image abilityImage = new Image(new Vector2(-Size.X * 0.5f + (column + 0.5f) * (abilityImageSize + padding) + 0.5f * padding, -Size.Y * 0.5f + (row + 0.5f) * (abilityImageSize + padding) + 0.5f * padding), new Vector2(abilityImageSize, abilityImageSize), ability.AddOnAbilityData.TextureAsset, this);
                abilityImage.HoverUI = new Label(ability.AddOnAbilityData.DisplayName, new Vector2(0, Size.Y * 0.5f + SpriteFont.LineSpacing * 0.5f), Color.White, abilityImage);
                abilityImage.StoredObject = ability;
                abilityImage.OnSelect += RunAbility;

                AddUIObject(abilityImage, ability.AddOnAbilityData.DisplayName + " Image");

                counter++;
            }
        }

        private void RunAbility(object sender, EventArgs e)
        {
            Image image = sender as Image;
            AddOnAbility ability = image.StoredObject as AddOnAbility;

            ability.Run();
        }

        #endregion

        #region Virtual Methods

        #endregion
    }
}
