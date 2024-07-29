using Microsoft.Win32;
using System.Drawing;
using Windows.UI;

namespace WhatIsPlaying
{
    internal class RegistryManager
    {

        private static readonly string SubKey = "WhatIsPlaying";
        private static readonly string FontKey = "FontColor";
        private static readonly string BackgroundKey = "BgColor";
        private static readonly string animateKey = "AnimateFlag";

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

        public System.Drawing.Color GetFontColor()
        {
            return this.GetColorFromReg(FontKey, System.Drawing.Color.Lime);
        }

        public System.Drawing.Color GetBackgroundColor()
        {
            return this.GetColorFromReg(BackgroundKey, System.Drawing.Color.Black);
        }

        public bool GetAnimationFlag()
        {
            this.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(SubKey, true);
            var animateFlag = this.RegistryKey.GetValue(animateKey);
            if (animateFlag == null)
            {
                this.RegistryKey.SetValue(animateKey, true.ToString());
            }

            bool result = bool.Parse(this.RegistryKey.GetValue(animateKey).ToString());
            this.RegistryKey.Close();
            return result;
        }

        private System.Drawing.Color GetColorFromReg(string key, System.Drawing.Color defColor)
        {
            this.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(SubKey, true);
            var fontColor = this.RegistryKey.GetValue(key);
            if (fontColor == null)
            {
                this.RegistryKey.SetValue(key, defColor.ToArgb());
            }

            System.Drawing.Color result = System.Drawing.Color.FromArgb((int)this.RegistryKey.GetValue(key));
            this.RegistryKey.Close();

            return result;
        }

        public void SetFontColor(System.Drawing.Color color)
        {
            this.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(SubKey, true);
            this.RegistryKey.SetValue(FontKey, color.ToArgb());
            this.RegistryKey.Close();
        }

        public void SetBackgroundColor(System.Drawing.Color color)
        {
            this.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(SubKey, true);
            this.RegistryKey.SetValue(BackgroundKey, color.ToArgb());
            this.RegistryKey.Close();
        }

        public void SetAnimationFlag(bool flag)
        {
            this.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(SubKey, true);
            this.RegistryKey.SetValue(animateKey, flag);
            this.RegistryKey.Close();
        }
    }
}
