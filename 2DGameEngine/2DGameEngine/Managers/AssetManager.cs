using _2DGameEngine.Abstract_Object_Classes;
using _2DGameEngine.UI_Objects;
using _2DGameEngineData;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Managers
{
    public class AssetManager
    {
        public static Dictionary<string, Texture2D> Textures
        {
            get;
            private set;
        }

        public static Dictionary<string, BaseData> Data
        {
            get;
            private set;
        }

        public static Dictionary<string, SpriteFont> SpriteFonts
        {
            get;
            private set;
        }

        public static void LoadAssets(ContentManager content)
        {
            SpriteFonts = new Dictionary<string, SpriteFont>();

            try
            {
                string[] spriteFontFiles = Directory.GetFiles(content.RootDirectory + "\\SpriteFonts", "*.*", SearchOption.AllDirectories);
                for (int i = 0; i < spriteFontFiles.Length; i++)
                {
                    // Remove the Content\\ from the start
                    spriteFontFiles[i] = spriteFontFiles[i].Remove(0, 8);

                    // Remove the .xnb at the end
                    spriteFontFiles[i] = spriteFontFiles[i].Split('.')[0];

                    SpriteFonts.Add(spriteFontFiles[i], content.Load<SpriteFont>(spriteFontFiles[i]));
                }
            }
            catch { }

            Textures = new Dictionary<string, Texture2D>();

            try
            {
                string[] textureFiles = Directory.GetFiles(content.RootDirectory + "\\Sprites", "*.*", SearchOption.AllDirectories);
                for (int i = 0; i < textureFiles.Length; i++)
                {
                    // Remove the Content\\ from the start
                    textureFiles[i] = textureFiles[i].Remove(0, 8);

                    // Remove the .xnb at the end
                    textureFiles[i] = textureFiles[i].Split('.')[0];

                    Textures.Add(textureFiles[i], content.Load<Texture2D>(textureFiles[i]));
                }
            }
            catch { }

            // Can't tell whether this will load all the data in as BaseData or will be clever and load as it should be
            Data = new Dictionary<string, BaseData>();

            try
            {
                string[] dataFiles = Directory.GetFiles(content.RootDirectory + "\\Data", "*.*", SearchOption.AllDirectories);
                for (int i = 0; i < dataFiles.Length; i++)
                {
                    // Remove the Content\\ from the start
                    dataFiles[i] = dataFiles[i].Remove(0, 8);

                    // Remove the .xnb at the end
                    dataFiles[i] = dataFiles[i].Split('.')[0];

                    Data.Add(dataFiles[i], content.Load<BaseData>(dataFiles[i]));
                }
            }
            catch { }

            // These must go here cos we need textures and stuff
            UIObject.LoadPresetContent();
            Button.LoadPresetContent();
            Menu.LoadPresetContent();
        }

        public static SpriteFont GetSpriteFont(string name)
        {
            if (SpriteFonts.ContainsKey(name))
                return SpriteFonts[name];

            return null;
        }

        public static Texture2D GetTexture(string name)
        {
            if (Textures.ContainsKey(name))
                return Textures[name];

            return null;
        }

        public static T GetData<T>(string name) where T : BaseData
        {
            if (Data.ContainsKey(name))
            {
                T data = Data[name] as T;
                return data;
            }

            try
            {
                return ScreenManager.Content.Load<T>(name);
            }
            catch
            {
                return null;
            }
        }

        public static List<T> GetAllData<T>() where T : BaseData
        {
            List<T> dataOfType = new List<T>();

            foreach (BaseData data in Data.Values)
            {
                T newData = data as T;
                if (newData != null)
                    dataOfType.Add(newData);
            }

            return dataOfType;
        }

        public static string GetKeyFromData(BaseData data)
        {
            return Data.FirstOrDefault(x => x.Value == data).Key;
        }
    }
}
