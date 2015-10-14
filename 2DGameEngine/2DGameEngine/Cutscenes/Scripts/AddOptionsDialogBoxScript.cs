using _2DGameEngine.Abstract_Object_Classes;
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

        public AddOptionsDialogBoxScript(string text, Vector2 localPosition, EventHandler leftButtonEvent, EventHandler rightButtonEvent, string leftButtonText = "Cancel", string rightButtonText = "Confirm", bool canRun = true, BaseObject parent = null, float lifeTime = float.MaxValue, bool shouldUpdateGame = false)
            : base(text, localPosition, canRun, parent, lifeTime, shouldUpdateGame)
        {
            OptionsDialogBox = new OptionsDialogBox(text, localPosition, leftButtonEvent, rightButtonEvent, leftButtonText, rightButtonText, "Sprites\\UI\\Menus\\DialogBox", parent, lifeTime);
            DialogBox = OptionsDialogBox;
        }

        public AddOptionsDialogBoxScript(string text, Vector2 localPosition, Vector2 size, EventHandler leftButtonEvent, EventHandler rightButtonEvent, string leftButtonText = "Cancel", string rightButtonText = "Confirm", bool canRun = true, BaseObject parent = null, float lifeTime = float.MaxValue, bool shouldUpdateGame = false)
            : base(text, localPosition, size, canRun, parent, lifeTime, shouldUpdateGame)
        {
            OptionsDialogBox = new OptionsDialogBox(text, localPosition, size, leftButtonEvent, rightButtonEvent, leftButtonText, rightButtonText, "Sprites\\UI\\Menus\\DialogBox", parent, lifeTime);
            DialogBox = OptionsDialogBox;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void CheckDone()
        {
            Done = DialogBox.Alive == false || OptionsDialogBox.LeftButton.IsSelected || OptionsDialogBox.RightButton.IsSelected;
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
