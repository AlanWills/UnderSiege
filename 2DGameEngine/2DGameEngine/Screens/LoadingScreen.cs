using _2DGameEngine.Managers;
using _2DGameEngine.UI_Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Screens
{
    public class LoadingScreen<T> : BaseScreen where T : BaseScreen
    {
        #region Properties and Fields

        private float lifeTime;

        #endregion

        public LoadingScreen(ScreenManager screenManager, string dataAsset= "Data\\Screens\\LoadingScreen")
            : base(screenManager, dataAsset)
        {
            AddScreenUIObject(new Label("Loading", ScreenCentre, Color.Cyan), "Loading Label");
        }

        #region Methods

        #endregion

        #region Virtual Methods

        public override void AddMusic(QueueType queueType = QueueType.WaitForCurrent)
        {
            base.AddMusic(QueueType.WaitForCurrent);
        }

        public override void Begin()
        {
            base.Begin();

            //Transition((T)Activator.CreateInstance(typeof(T), ScreenManager));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            lifeTime += (float)gameTime.ElapsedGameTime.Milliseconds / 1000f;
            if (lifeTime > 1.5f)
            {
                Transition((T)Activator.CreateInstance(typeof(T), ScreenManager));
            }
        }

        #endregion
    }
}
