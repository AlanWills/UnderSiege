﻿using _2DGameEngine.Managers;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace _2DGameEngine.Extra_Components
{
    public class OptionsData
    {
        public bool IsFullScreen
        {
            get;
            set;
        }

        public float MusicVolume
        {
            get;
            set;
        }

        public float SFXVolume
        {
            get;
            set;
        }

        public int Level
        {
            get;
            set;
        }
    }

    public static class Options
    {
        #region Properties and Fields

        public static bool IsFullScreen
        {
            get;
            set;
        }

        public static float MusicVolume
        {
            get;
            set;
        }

        public static float SFXVolume
        {
            get;
            set;
        }

        public static int Level
        {
            get;
            set;
        }

        public static string fileName = ScreenManager.Content.RootDirectory + "\\Options.xml";

        #endregion

        #region Methods

        public static void Load()
        {
            OptionsData optionsData;

            XmlSerializer mySerializer = new XmlSerializer(typeof(OptionsData));
            // To read the file, create a FileStream.

            try
            {
                FileStream myFileStream = new FileStream(fileName, FileMode.Open);
                // Call the Deserialize method and cast to the object type.
                optionsData = (OptionsData)mySerializer.Deserialize(myFileStream);

                IsFullScreen = optionsData.IsFullScreen;
                MusicVolume = optionsData.MusicVolume;
                SFXVolume = optionsData.SFXVolume;
                Level = optionsData.Level;
            }
            catch
            {
                IsFullScreen = !GlobalVariables.DEBUG;
                MusicVolume = 0;
                SFXVolume = 0;
                Level = 1;
            }

            ScreenManager.GraphicsDeviceManager.IsFullScreen = IsFullScreen;
            ScreenManager.GraphicsDeviceManager.ApplyChanges();
            MediaPlayer.Volume = MusicVolume;
        }

        public static void Save()
        {
            OptionsData optionsData = new OptionsData();
            optionsData.IsFullScreen = IsFullScreen;
            optionsData.MusicVolume = MusicVolume;
            optionsData.SFXVolume = SFXVolume;
            optionsData.Level = Level;

            XmlSerializer mySerializer = new XmlSerializer(typeof(OptionsData));
            // To write to a file, create a StreamWriter object and overriding current file
            StreamWriter myWriter = new StreamWriter(fileName, false);
            mySerializer.Serialize(myWriter, optionsData);
            myWriter.Close();
        }

        #endregion

        #region Virtual Methods

        #endregion
    }
}
