using System;
using System.Threading.Tasks;
using Windows.Media.Control;
using Windows.Storage.Provider;


namespace WhatIsPlaying
{
    internal class WindowMediaControlUtils
    {
        public static string UnableToEstablishMediaMessage => "Unable to establish media...";
        public static string NoMediaSessionFound => "No media player found...";

        private static String currentSongName = UnableToEstablishMediaMessage;
        

        internal static String GetCurrentPlayedMedia()
        {
            try
            {
                GetCurrentMediaTask();
            }
            catch 
            { 
                // silencio :)
            }
            return currentSongName;
        }

        private static async void GetCurrentMediaTask()
        {
            var gsmtcsm = await GetSystemMediaTransportControlsSessionManager();

            if (gsmtcsm != null)
            {
                GlobalSystemMediaTransportControlsSession session = gsmtcsm.GetCurrentSession();
                if (session != null)
                {
                    var mediaProperties = await GetMediaProperties(session);
                    if (mediaProperties != null)
                    {
                        currentSongName = String.Format("{0} - {1}", mediaProperties.Artist, mediaProperties.Title);
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
