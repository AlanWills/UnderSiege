using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Extra_Components
{
    public class InputHandler : GameComponent
    {
        #region Properties and Fields

        public static KeyboardState KeyboardState
        {
            get;
            private set;
        }

        public static KeyboardState PreviousKeyboardState
        {
            get;
            private set;
        }

        #endregion

        public InputHandler(Game game)
            : base(game)
        {
            KeyboardState = Keyboard.GetState();
        }

        #region Methods

        // Makes the KeyPressed and KeyReleased methods return false by setting the previous keyboardstate to the current keyboardstate
        public static void Flush()
        {
            PreviousKeyboardState = KeyboardState;
        }

        public static bool KeyReleased(Keys key)
        {
            return KeyboardState.IsKeyUp(key) && PreviousKeyboardState.IsKeyDown(key);
        }

        public static bool KeyPressed(Keys key)
        {
            return KeyboardState.IsKeyDown(key) && PreviousKeyboardState.IsKeyUp(key);
        }

        public static bool KeyDown(Keys key)
        {
            return KeyboardState.IsKeyDown(key);
        }

        #endregion

        #region Virtual Methods

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            PreviousKeyboardState = KeyboardState;
            KeyboardState = Keyboard.GetState();
        }

        #endregion
    }
}
