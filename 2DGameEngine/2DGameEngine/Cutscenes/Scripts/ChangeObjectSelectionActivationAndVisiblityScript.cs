using _2DGameEngine.Abstract_Object_Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Cutscenes.Scripts
{
    public class ChangeObjectSelectionActivationAndVisiblityScript : Script
    {
        #region Properties and Fields

        private bool IsSelected { get; set; }
        private bool IsVisible { get; set; }
        private bool IsActive { get; set; }
        private BaseObject BaseObject { get; set; }

        #endregion

        public ChangeObjectSelectionActivationAndVisiblityScript(BaseObject baseObject, bool isSelected, bool isVisible, bool isActive, bool canRun = true)
            : base(canRun)
        {
            IsSelected = isSelected;
            IsVisible = isVisible;
            IsActive = isActive;
            BaseObject = baseObject;
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void LoadAndInit(ContentManager content)
        {

        }

        public override void Run(GameTime gameTime)
        {
            BaseObject.IsSelected = IsSelected;
            BaseObject.Visible = IsVisible;
            BaseObject.Active = IsActive;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }

        public override void DrawUI(SpriteBatch spriteBatch)
        {

        }

        public override void HandleInput()
        {

        }

        public override void CheckDone()
        {
            Done = true;
        }

        public override void PerformImmediately()
        {
            BaseObject.IsSelected = IsSelected;
            BaseObject.Visible = IsVisible;
            BaseObject.Active = IsActive;
        }

        #endregion
    }
}
