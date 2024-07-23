using System;
using System.Threading.Tasks;
using Windows.Media.Control;


namespace WhatIsPlaying
{
    internal class WindowMediaControlUtils
    {
        public static string UnableToEstablishMediaMessage => "Unable to establish media...";
        private static String currentSongName = UnableToEstablishMediaMessage;
        

        internal static String GetCurrentPlayedMedia()
        {
            GetCurrentMediaTask();
            return currentSongName;
        }

        private static async void GetCurrentMediaTask()
        {
            var gsmtcsm = await GetSystemMediaTransportControlsSessionManager();

            if (gsmtcsm != null)
            {
                var mediaProperties = await GetMediaProperties(gsmtcsm.GetCurrentSession());
                if (mediaProperties != null)
                {
                    currentSongName = String.Format("{0} - {1}", mediaProperties.Artist, mediaProperties.Title);
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
