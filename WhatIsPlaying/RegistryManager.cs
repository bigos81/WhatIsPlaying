using Microsoft.Win32;
using System.Drawing;

namespace WhatIsPlaying
{
    internal class RegistryManager
    {

        private static readonly string SubKey = "WhatIsPlaying";
        private static readonly string FontKey = "FontColor";
        private static readonly string BackgroundKey = "BgColor";

        private RegistryKey RegistryKey;

        public RegistryManager() 
        {
            this.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(SubKey);
            if (RegistryKey == null)
            {
                Registry.CurrentUser.CreateSubKey(SubKey);
                this.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(SubKey);
            }
            this.RegistryKey.Close();
        }

        public Color GetFontColor()
        {
            return this.GetColorFromReg(FontKey, Color.Lime);
        }

        public Color GetBackgroundColor()
        {
            return this.GetColorFromReg(BackgroundKey, Color.Black);
        }

        private Color GetColorFromReg(string key, Color defColor)
        {
            this.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(SubKey, true);
            var fontColor = this.RegistryKey.GetValue(key);
            if (fontColor == null)
            {
                this.RegistryKey.SetValue(key, defColor.ToArgb());
            }

            Color result = Color.FromArgb((int)this.RegistryKey.GetValue(key));
            this.RegistryKey.Close();

            return result;
        }

        public void SetFontColor(Color color)
        {
            this.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(SubKey, true);
            this.RegistryKey.SetValue(FontKey, color.ToArgb());
            this.RegistryKey.Close();
        }

        public void SetBackgroundColor(Color color)
        {
            this.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(SubKey, true);
            this.RegistryKey.SetValue(BackgroundKey, color.ToArgb());
            this.RegistryKey.Close();
        }
    }
}
