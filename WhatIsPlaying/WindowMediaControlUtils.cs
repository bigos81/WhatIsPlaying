using System;
using System.Threading.Tasks;
using Windows.Media.Control;


namespace WhatIsPlaying
{
    internal static class WindowMediaControlUtils
    {
        public static string UnableToGetMediaManager => "Unable to get media manager...";
        public static string NoMediaPlayerFound => "No media player found...";

        private static bool failed_before = false;
        private static String currentSongName = UnableToGetMediaManager;
        private static GlobalSystemMediaTransportControlsSessionManager gsmtcsm;


        internal static String GetCurrentPlayedMedia()
        {
            try
            {
                if (gsmtcsm == null)
                    GetSessionManager();

                FetchCurrentlyPlayedSong();
            }
            catch { /* silencio */ }
            return currentSongName;
        }

        private static async void GetSessionManager()
        {
            gsmtcsm = await GetSystemMediaTransportControlsSessionManager();
        }

        private static async void FetchCurrentlyPlayedSong()
        {
            if (gsmtcsm != null)
            {
                var session = gsmtcsm.GetCurrentSession();
                if (session != null)
                {
                    var mediaProperties = await GetMediaProperties(session);
                    if (mediaProperties != null)
                    {
                        if (mediaProperties.Artist.Length > 0 && mediaProperties.Title.Length > 0)
                        {
                            currentSongName = String.Format("🎵 {0} - {1}", mediaProperties.Artist, mediaProperties.Title);
                            failed_before = false;
                        }
                    }
                }
                else
                {
                    if (failed_before)
                        currentSongName = NoMediaPlayerFound;
                    else
                        failed_before = true;
                }
            }
            else 
                currentSongName = UnableToGetMediaManager; 
        }

        private static async Task<GlobalSystemMediaTransportControlsSessionManager> GetSystemMediaTransportControlsSessionManager() =>
            await GlobalSystemMediaTransportControlsSessionManager.RequestAsync();

        private static async Task<GlobalSystemMediaTransportControlsSessionMediaProperties> GetMediaProperties(GlobalSystemMediaTransportControlsSession session) =>
            await session.TryGetMediaPropertiesAsync();
    }
}
