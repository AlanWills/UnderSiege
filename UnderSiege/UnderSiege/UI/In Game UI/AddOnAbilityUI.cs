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

namespace UnderSiege.UI.In_Game_UI
{
    public class AddOnAbilityUI : ScreenUIObject
    {
        #region Properties and Fields

        public AddOnAbility Ability { get; private set; }
        private CooldownUI CooldownUI { get; set; }

        #endregion

        public AddOnAbilityUI(AddOnAbility ability, Vector2 localPosition, Vector2 size, BaseObject parent, float lifeTime = float.MaxValue)
            : base(localPosition, size, ability.AddOnAbilityData.TextureAsset, parent, lifeTime)
        {
            Ability = ability;
            ScreenHoverUI = new Label(ability.AddOnAbilityData.DisplayName, new Vector2(0, Parent.Size.Y * 0.5f + SpriteFont.LineSpacing * 0.5f + 10), Color.White, parent);
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void Initialize()
        {
            base.Initialize();

            CooldownUI = new CooldownUI(Ability, Size, this);
            CooldownUI.LoadContent();
            CooldownUI.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            CooldownUI.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            CooldownUI.Draw(spriteBatch);
        }

        protected override void Select()
        {
            base.Select();

            ScreenManager.GameMouse.Flush();

            if (Ability.CanRun)
            {
                Ability.Run();
                Parent.Visible = false;
                Parent.Active = false;
            }
            else
            {
                Menu parent = Parent as Menu;
                parent.AddUIObject(new FlashingLabel("Ability Unavailable", new Vector2(0, -parent.Size.Y * 0.5f - SpriteFont.LineSpacing * 0.5f - 10), Color.Red, parent, 2), Ability.AddOnAbilityData.DisplayName + " On Cooldown Label");
            }
        }

        #endregion
    }
}