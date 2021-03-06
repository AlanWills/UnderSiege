﻿using _2DGameEngine.Extra_Components;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _2DGameEngine.Managers
{
    public enum QueueType { WaitForCurrent, PlayImmediately}

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
            MediaPlayer.Volume = Options.MusicVolume;
            QueueSongNames = new Queue<string>();
            Songs = new Dictionary<string, Song>();

            try
            {
                string[] musicFiles = Directory.GetFiles(content.RootDirectory + "\\Music", ".", SearchOption.AllDirectories);
                for (int i = 0; i < musicFiles.Length; i++)
                {
                    // Remove the Content\\ from the start
                    musicFiles[i] = musicFiles[i].Remove(0, 8);

                    // Remove the .xnb at the end
                    musicFiles[i] = musicFiles[i].Split('.')[0];

                    // Remove the Music\\ from the start
                    string key = musicFiles[i].Remove(0, 6);

                    if (!Songs.ContainsKey(key))
                    {
                        Songs.Add(key, content.Load<Song>(musicFiles[i]));
                    }
                }
            }
            catch { }
        }

        public static void QueueSong(string songName, QueueType queueType = QueueType.WaitForCurrent)
        {
            QueueSongNames.Enqueue(songName);

            if (queueType == QueueType.PlayImmediately)
            {
                PlayNextSong();
            }
        }

        public static void QueueSongs(List<string> songs, QueueType queueType = QueueType.WaitForCurrent)
        {
            // If we are not adding any songs, don't bother doing the rest of this function
            if (songs.Count == 0)
                return;

            foreach (string song in songs)
            {
                QueueSongNames.Enqueue(song);
            }

            if (queueType == QueueType.PlayImmediately)
            {
                PlayNextSong();
            }
        }

        public static void ClearQueue()
        {
            CurrentSong = null;
            MediaPlayer.Stop();
            QueueSongNames.Clear();
        }

        public static void PlaySongImmediately(string songName)
        {
            CurrentSong = Songs[songName];
            MediaPlayer.Play(CurrentSong);
        }

        public static void PlayNextSong()
        {
            if (QueueSongNames.Count > 0)
            {
                PlaySongImmediately(QueueSongNames.Dequeue());
            }
        }

        public static void Update()
        {
            if (MediaPlayer.State == MediaState.Stopped)
            {
                // If we have songs still queued then dequeue the next song name
                if (QueueSongNames.Count > 0)
                {
                    CurrentSong = Songs[QueueSongNames.Dequeue()];
                    MediaPlayer.Play(CurrentSong);
                }
                // Else just keep playing the song last played
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
