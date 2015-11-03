using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Abilities.Object_Abilities;

namespace UnderSiege.UI.In_Game_UI
{
    public class CooldownUI : Image
    {
        #region Properties and Fields

        private AddOnAbility AddOnAbility { get; set; }

        private Vector2 startingSize;

        #endregion

        public CooldownUI(AddOnAbility addOnAbility, Vector2 size, BaseObject parent = null, string dataAsset = "Sprites\\UI\\InGameUI\\CooldownUI")
            : base(Vector2.Zero, size, dataAsset, parent)
        {
            AddOnAbility = addOnAbility;

            startingSize = size;
            Opacity = 0.5f;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float amountDone = AddOnAbility.Cooldown / AddOnAbility.AddOnAbilityData.Cooldown;
            Size = new Vector2(Size.X, startingSize.Y * amountDone);
            LocalPosition = new Vector2(0, startingSize.Y * 0.5f * (1 - amountDone));
        }

        #endregion
    }
}
