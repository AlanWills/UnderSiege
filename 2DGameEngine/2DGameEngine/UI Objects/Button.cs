using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.UI_Objects
{
    public enum ButtonState { Idle, Highlighted, Pressed, Disabled }

    public class Button : ScreenUIObject
    {
        #region Properties and Fields

        private string DefaultTextureAsset
        {
            get;
            set;
        }

        private string HighlightedTextureAsset
        {
            get;
            set;
        }

        private string PressedTextureAsset
        {
            get;
            set;
        }

        private string DisabledTextureAsset
        {
            get;
            set;
        }

        private Texture2D DefaultTexture
        {
            get;
            set;
        }

        private Texture2D PressedTexture
        {
            get;
            set;
        }

        private Texture2D HighlightedTexture
        {
            get;
            set;
        }

        private Texture2D DisabledTexture
        {
            get;
            set;
        }

        private SoundEffect ButtonHoverSound
        {
            get;
            set;
        }

        private SoundEffect ButtonPressedSound
        {
            get;
            set;
        }

        private ButtonState ButtonState
        {
            get;
            set;
        }

        public static string defaultTextureAsset = "Sprites\\UI\\Buttons\\default", pressedTextureAsset = "Sprites\\UI\\Buttons\\pressed", highlightedTextureAsset = "Sprites\\UI\\Buttons\\highlighted", disabledTextureAsset = "Sprites\\UI\\Buttons\\disabled";
        public static string defaultHoverSoundAsset = "ButtonHover", defaultClickedSoundAsset = "ButtonPressedSound";
        public static Texture2D defaultTexture, pressedTexture, highlightedTexture, disabledTexture;
        private const float resetTime = 0.02f;
        private TimeSpan pressedTime = TimeSpan.FromSeconds(resetTime);

        public static Color defaultColour = Color.Blue;
        public static Color highlightedColour = Color.Cyan;

        private bool canPlayHoverSound = false;

        #endregion

        public Button(Vector2 position, string text = "", BaseObject parent = null)
            : base(position, "", parent)
        {
            ButtonState = ButtonState.Idle;
            Text = text;
        }

        public Button(Vector2 position, Vector2 size, string text = "", BaseObject parent = null)
            : base(position, size, "", parent)
        {
            ButtonState = ButtonState.Idle;
            Text = text;
        }

        public Button(string defaultAsset, string pressedAsset, string highlightedAsset, string disabledAsset, Vector2 position, string text = "", BaseObject parent = null)
            : this(position, text, parent)
        {
            DefaultTextureAsset = defaultAsset;
            PressedTextureAsset = pressedAsset;
            HighlightedTextureAsset = highlightedAsset;
            DisabledTextureAsset = disabledAsset;
        }

        public Button(string defaultAsset, string pressedAsset, string highlightedAsset, string disabledAsset, Vector2 position, Vector2 size, string text = "", BaseObject parent = null)
            : this(position, size, text, parent)
        {
            DefaultTextureAsset = defaultAsset;
            PressedTextureAsset = pressedAsset;
            HighlightedTextureAsset = highlightedAsset;
            DisabledTextureAsset = disabledAsset;
        }

        #region Methods

        new public static void LoadPresetContent()
        {
            defaultTexture = AssetManager.GetTexture(defaultTextureAsset);
            pressedTexture = AssetManager.GetTexture(pressedTextureAsset);
            highlightedTexture = AssetManager.GetTexture(highlightedTextureAsset);
            disabledTexture = AssetManager.GetTexture(disabledTextureAsset);
        }

        private void SetTexture()
        {
            switch (ButtonState)
            {
                case ButtonState.Idle:
                    Texture = DefaultTexture;
                    break;
                case ButtonState.Highlighted:
                    Texture = HighlightedTexture;
                    break;
                case ButtonState.Pressed:
                    Texture = PressedTexture;
                    break;
                case ButtonState.Disabled:
                    Texture = DisabledTexture;
                    break;
            }
        }

        #endregion

        #region Virtual Methods

        public override void LoadContent()
        {
            if (string.IsNullOrEmpty(DefaultTextureAsset))
            {
                DefaultTextureAsset = defaultTextureAsset;
                DefaultTexture = defaultTexture;
                Opacity = 0.6f;
            }
            else
            {
                DefaultTexture = AssetManager.GetTexture(DefaultTextureAsset);
            }

            if (string.IsNullOrEmpty(PressedTextureAsset))
            {
                PressedTextureAsset = pressedTextureAsset;
                PressedTexture = pressedTexture;
            }
            else
            {
                PressedTexture = AssetManager.GetTexture(PressedTextureAsset);
            }

            if (string.IsNullOrEmpty(HighlightedTextureAsset))
            {
                HighlightedTextureAsset = highlightedTextureAsset;
                HighlightedTexture = highlightedTexture;
            }
            else
            {
                HighlightedTexture = AssetManager.GetTexture(HighlightedTextureAsset);
            }

            if (string.IsNullOrEmpty(DisabledTextureAsset))
            {
                DisabledTextureAsset = disabledTextureAsset;
                DisabledTexture = disabledTexture;
            }
            else
            {
                DisabledTexture = AssetManager.GetTexture(DisabledTextureAsset);
            }

            ButtonHoverSound = ScreenManager.SFX.SoundEffects[defaultHoverSoundAsset];
            ButtonPressedSound = ScreenManager.SFX.SoundEffects[defaultClickedSoundAsset];
            Colour = defaultColour;
        }

        public override void Initialize()
        {
            SetTexture();

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            // In the base update we call the IfMouseOver function which deals with setting the ButtonState to ButtonState.Highlighted
            base.Update(gameTime);

            if (!MouseOver)
            {
                canPlayHoverSound = true;
            }
            else
            {
                if (canPlayHoverSound)
                {
                    ButtonHoverSound.Play(Options.SFXVolume, 0, 0);
                    canPlayHoverSound = false;
                }
            }

            // Button has reently been pressed and we update accordingly
            if (pressedTime < TimeSpan.FromSeconds(resetTime))
            {
                pressedTime += gameTime.ElapsedGameTime;

                if (pressedTime >= TimeSpan.FromSeconds(resetTime))
                    ButtonState = ButtonState.Idle;
            }
            else
            {
                // If our mouse is not over the button we can just set it back to idle
                if (!MouseOver)
                    ButtonState = ButtonState.Idle;
            }

            Colour = Color.Lerp(Colour, defaultColour, (float)gameTime.ElapsedGameTime.Milliseconds / 250f);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            SetTexture();
            base.Draw(spriteBatch);
        }

        protected override void Select()
        {
            if (ButtonState != ButtonState.Disabled && ButtonState != ButtonState.Pressed)
            {
                ButtonPressedSound.Play(Options.SFXVolume, 0, 0);

                base.Select();

                ButtonState = ButtonState.Pressed;
                pressedTime = TimeSpan.FromSeconds(0);
                IsSelected = false;
            }
        }

        protected override void IfMouseOver()
        {
            base.IfMouseOver();

            if (ButtonState == ButtonState.Idle)
                ButtonState = ButtonState.Highlighted;

            Colour = highlightedColour;
        }

        #endregion
    }
}
