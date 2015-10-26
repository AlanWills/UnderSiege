using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.Extra_Components;
using _2DGameEngine.Managers;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Cutscenes.Scripts
{
    public class AddOptionsDialogBoxScript : AddDialogBoxScript
    {
        #region Properties and Fields

        private OptionsDialogBox OptionsDialogBox { get; set; }

        #endregion

        public AddOptionsDialogBoxScript(string text, Vector2 localPosition, EventHandler leftButtonEvent, EventHandler rightButtonEvent, string leftButtonText = "Cancel", string rightButtonText = "Confirm", bool canRun = true, bool shouldUpdateGame = false, BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(text, localPosition, canRun, shouldUpdateGame, parent, lifeTime)
        {
            OptionsDialogBox = new OptionsDialogBox(text, localPosition, leftButtonEvent, rightButtonEvent, leftButtonText, rightButtonText, "Sprites\\UI\\Menus\\DialogBox", parent, lifeTime);
            DialogBox = OptionsDialogBox;

            // Flush the game mouse otherwise we will automatically be done
            ScreenManager.GameMouse.Flush();
        }

        public AddOptionsDialogBoxScript(string text, Vector2 localPosition, Vector2 size, EventHandler leftButtonEvent, EventHandler rightButtonEvent, string leftButtonText = "Cancel", string rightButtonText = "Confirm", bool canRun = true, bool shouldUpdateGame = false, BaseObject parent = null, float lifeTime = float.MaxValue)
            : base(text, localPosition, size, canRun, shouldUpdateGame, parent, lifeTime)
        {
            OptionsDialogBox = new OptionsDialogBox(text, localPosition, size, leftButtonEvent, rightButtonEvent, leftButtonText, rightButtonText, "Sprites\\UI\\Menus\\DialogBox", parent, lifeTime);
            DialogBox = OptionsDialogBox;

            // Flush the game mouse otherwise we will automatically be done
            ScreenManager.GameMouse.Flush();
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadAndInit(ContentManager content)
        {
            base.LoadAndInit(content);

            // Options dialog boxes should not have instruction labels
            NextDialogButton.Alive = false;
        }

        public override void CheckDone()
        {
            bool clickedOutsideOfBox = ScreenManager.GameMouse.IsLeftClicked && !DialogBox.IsSelected && !OptionsDialogBox.LeftButton.IsSelected && !OptionsDialogBox.RightButton.IsSelected;
            Done = DialogBox.Alive == false || OptionsDialogBox.LeftButton.IsSelected || OptionsDialogBox.RightButton.IsSelected || clickedOutsideOfBox;
        }

        public override void PerformImmediately()
        {
            // Automatically press the right button
            OptionsDialogBox.RightButton.IsSelected = true;
            Done = true;
        }

        #endregion
    }
}
