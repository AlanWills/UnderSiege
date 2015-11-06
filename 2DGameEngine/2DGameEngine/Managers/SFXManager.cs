using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Managers
{
    public class SFXManager
    {
        #region Properties and Fields

        public Dictionary<string, SoundEffect> SoundEffects
        {
            get;
            private set;
        }

        #endregion

        public SFXManager()
        {
            SoundEffects = new Dictionary<string, SoundEffect>();
        }

        #region Methods

        public void LoadContent(ContentManager content)
        {
            SoundEffects = new Dictionary<string, SoundEffect>();

            try
            {
                string[] sfxFiles = Directory.GetFiles(content.RootDirectory + "\\SFX", ".", SearchOption.AllDirectories);
                for (int i = 0; i < sfxFiles.Length; i++)
                {
                    // Remove the Content\\ from the start
                    sfxFiles[i] = sfxFiles[i].Remove(0, 8);

                    // Remove the .xnb at the end
                    sfxFiles[i] = sfxFiles[i].Split('.')[0];

                    // Remove the SFX\\ from the start
                    string key = sfxFiles[i].Remove(0, 4);

                    if (!SoundEffects.ContainsKey(key))
                    {
                        SoundEffects.Add(key, content.Load<SoundEffect>(sfxFiles[i]));
                    }
                }
            }
            catch { }
        }

        public SoundEffect GetSoundEffect(string sfxName)
        {
            Debug.Assert(SoundEffects.ContainsKey(sfxName));
            return SoundEffects[sfxName];
        }

        #endregion

        #region Virtual Methods

        #endregion
    }
}
