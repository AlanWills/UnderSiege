using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Managers
{
    public static class MusicManager
    {
        #region Properties and Fields

        private static Dictionary<string, Song> Songs
        {
            get;
            set;
        }

        private static Queue<string> QueueSongNames
        {
            get;
            set;
        }

        private static Song CurrentSong
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public static void LoadAssets(ContentManager content)
        {
            QueueSongNames = new Queue<string>();
            Songs = new Dictionary<string, Song>();

            string[] musicFiles = Directory.GetFiles(content.RootDirectory + "\\Music", ".", SearchOption.AllDirectories);
            for (int i = 0; i < musicFiles.Length; i++)
            {
                // Remove the Content\\Music\\ from the start
                musicFiles[i] = musicFiles[i].Remove(0, 8);

                // Remove the .xnb at the end
                musicFiles[i] = musicFiles[i].Split('.')[0];

                string key = musicFiles[i].Remove(0, 6);

                if (!Songs.ContainsKey(key))
                {
                    Songs.Add(key, content.Load<Song>(musicFiles[i]));
                }
            }
        }

        public static void AddSong(string songName)
        {
            QueueSongNames.Enqueue(songName);
        }

        public static void PlaySongImmediately(string songName)
        {
            CurrentSong = Songs[songName];
            MediaPlayer.Play(CurrentSong);
        }

        public static void Update()
        {
            if (MediaPlayer.State == MediaState.Stopped)
            {
                if (QueueSongNames.Count > 0)
                {
                    CurrentSong = Songs[QueueSongNames.Dequeue()];
                    MediaPlayer.Play(CurrentSong);
                }
                else if (CurrentSong != null)
                {
                    MediaPlayer.Play(CurrentSong);
                }
            }
        }

        #endregion

        #region Virtual Methods

        #endregion
    }
}
