using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DanMuJie.UI
{
    public class PlayMusicHelper
    {
        public static WaveOutEvent Wo { get; set; }

        public static Dictionary<string, MediaFoundationReader> MusicResList { get; set; }

        public static void AddPlayList(string url)
        {
            if (MusicResList == null)
            {
                MusicResList = new Dictionary<string, MediaFoundationReader>();
            }

            if (!MusicResList.Keys.Contains(url))
            {
                MusicResList.Add(url, new MediaFoundationReader(url));
            }

            Exchange(url);
        }

        public static void Exchange(string url)
        {
            if (Wo == null)
            {
                Wo = new WaveOutEvent();
            }
            if (Wo.PlaybackState != PlaybackState.Stopped)
            {
                Wo.Stop();
            }
            Wo.Init(MusicResList[url]);
            Wo.Play();
            while (Wo.PlaybackState == PlaybackState.Playing)
            {
                Thread.Sleep(1000);
            }
            Wo.Stop();

            Exchange(MusicResList.Keys.ToArray()[new Random().Next(0, MusicResList.Count - 1)]);
        }


    }
}
