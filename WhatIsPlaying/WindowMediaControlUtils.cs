using System;
using System.Threading.Tasks;
using Windows.Media.Control;


namespace WhatIsPlaying
{
    internal class WindowMediaControlUtils
    {
        public static string UnableToEstablishMediaMessage => "Unable to establish media...";
        public static string NoMediaSessionFound => "No media player found...";

        private static String currentSongName = UnableToEstablishMediaMessage;
        private static GlobalSystemMediaTransportControlsSessionManager gsmtcsm;



        WindowMediaControlUtils()
        {
            GetSessionManager();
        }

        internal static String GetCurrentPlayedMedia()
        {
            try
            {
                if (gsmtcsm == null)
                {
                    GetSessionManager();
                }
                GetCurrentMediaTask();
            }
            catch 
            {
                // silencio :)
            }
            return currentSongName;
        }

        private static async void GetSessionManager()
        {
            gsmtcsm = await GetSystemMediaTransportControlsSessionManager();
         }

        private static async void GetCurrentMediaTask()
        {
            if (gsmtcsm != null)
            {
                GlobalSystemMediaTransportControlsSession session = gsmtcsm.GetCurrentSession();
                if (session != null)
                {
                    var mediaProperties = await GetMediaProperties(session);
                    if (mediaProperties != null)
                    {
                        currentSongName = String.Format("🎵 {0} - {1}", mediaProperties.Artist, mediaProperties.Title);
                    }
                }
                else
                {
                    currentSongName = NoMediaSessionFound;
                }
            }
            else 
            {
                currentSongName = UnableToEstablishMediaMessage; 
            }
        }

        private static async Task<GlobalSystemMediaTransportControlsSessionManager> GetSystemMediaTransportControlsSessionManager() =>
            await GlobalSystemMediaTransportControlsSessionManager.RequestAsync();

        private static async Task<GlobalSystemMediaTransportControlsSessionMediaProperties> GetMediaProperties(GlobalSystemMediaTransportControlsSession session) =>
            await session.TryGetMediaPropertiesAsync();
    }
}
