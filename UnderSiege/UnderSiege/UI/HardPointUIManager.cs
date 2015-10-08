using _2DGameEngine.Abstract_Object_Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnderSiege.Gameplay_Objects;
using UnderSiegeData.Gameplay_Objects;

namespace UnderSiege.UI
{
    public enum HardPointType { Engine, Other }

    public class HardPointUIManager
    {
        // Also have it handle the lists of hardpoints themselves?
        // Have this class handle both lists of hardpoint UIs and deal with their activation etc.
        #region Properties and Fields

        public List<HardPointUI> EngineHardPointUI { get; private set; }
        public List<HardPointUI> OtherHardPointUI { get; private set; }

        public bool DrawEngineHardPointUI { get; set; }
        public bool DrawOtherHardPointUI { get; set; }

        private BaseObject Parent { get; set; }

        #endregion

        public HardPointUIManager(BaseObject parent)
        {
            Parent = parent;
            EngineHardPointUI = new List<HardPointUI>();
            OtherHardPointUI = new List<HardPointUI>();
        }

        #region Methods

        public void Initialize(ShipData shipData)
        {
            foreach (Vector2 otherHardPoint in shipData.OtherHardPoints)
            {
                HardPointUI hardPointUI = new HardPointUI(Parent, otherHardPoint);
                hardPointUI.LoadContent();
                hardPointUI.Initialize();
                OtherHardPointUI.Add(hardPointUI);
            }

            foreach (Vector2 engineHardPoint in shipData.EngineHardPoints)
            {
                HardPointUI hardPointUI = new HardPointUI(Parent, engineHardPoint, "Sprites\\UI\\InGameUI\\EngineHardPointUI");
                hardPointUI.LoadContent();
                hardPointUI.Initialize();
                EngineHardPointUI.Add(hardPointUI);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (HardPointUI hardPointUI in OtherHardPointUI)
            {
                hardPointUI.Update(gameTime);
            }

            foreach (HardPointUI hardPointUI in EngineHardPointUI)
            {
                hardPointUI.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (DrawOtherHardPointUI)
            {
                foreach (HardPointUI hardPointUI in OtherHardPointUI)
                {
                    hardPointUI.Draw(spriteBatch);
                }
            }

            if (DrawEngineHardPointUI)
            {
                foreach (HardPointUI hardPointUI in EngineHardPointUI)
                {
                    hardPointUI.Draw(spriteBatch);
                }
            }
        }

        public void Add(Vector2 hardPoint, HardPointType type)
        {
            string dataAsset = type == HardPointType.Engine ? "Sprites\\UI\\InGameUI\\EngineHardPointUI" : "Sprites\\UI\\InGameUI\\HardPointUI";
            HardPointUI hardPointUI = new HardPointUI(Parent, hardPoint, dataAsset);

            if (type == HardPointType.Engine)
            {
                EngineHardPointUI.Add(hardPointUI);
            }
            else
            {
                OtherHardPointUI.Add(hardPointUI);
            }
        }

        public void Enable(Vector2 hardPoint, HardPointType type)
        {
            if (type == HardPointType.Engine)
            {
                HardPointUI hardPointUI = EngineHardPointUI.Find(x => x.HardPoint == hardPoint);
                hardPointUI.Active = true;
                hardPointUI.Visible = true;
            }
            else
            {
                HardPointUI hardPointUI = OtherHardPointUI.Find(x => x.HardPoint == hardPoint);
                hardPointUI.Active = true;
                hardPointUI.Visible = true;
            }
        }

        public void Disable(Vector2 hardPoint, HardPointType type)
        {
            if (type == HardPointType.Engine)
            {
                HardPointUI hardPointUI = EngineHardPointUI.Find(x => x.HardPoint == hardPoint);
                hardPointUI.Active = false;
                hardPointUI.Visible = false;
            }
            else
            {
                HardPointUI hardPointUI = OtherHardPointUI.Find(x => x.HardPoint == hardPoint);
                hardPointUI.Active = false;
                hardPointUI.Visible = false;
            }
        }

        #endregion

        #region Virtual Methods

        #endregion
    }
}
