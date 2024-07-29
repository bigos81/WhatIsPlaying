using Microsoft.Win32;
using System.Drawing;
using Windows.Security.Cryptography.Core;
using Windows.UI;

namespace WhatIsPlaying
{
    internal class RegistryManager
    {

        private static readonly string SubKey = "WhatIsPlaying";
        private static readonly string FontColorKey = "FontColor";
        private static readonly string BackgroundKey = "BgColor";
        private static readonly string animateKey = "AnimateFlag";
        private static readonly string FontKey = "Font";

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
            return this.GetColorFromReg(FontColorKey, System.Drawing.Color.Lime);
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
            this.RegistryKey.SetValue(FontColorKey, color.ToArgb());
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

        public Font GetFont()
        {
            FontConverter converter = new FontConverter();
            this.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(SubKey, true);
            var fontKey = this.RegistryKey.GetValue(FontKey);
            if (fontKey == null)
            {
                string fontStr = converter.ConvertToString(new Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))));
                this.RegistryKey.SetValue(FontKey, fontStr);
            }

            Font result = converter.ConvertFromString(this.RegistryKey.GetValue(FontKey) as string) as Font;
            this.RegistryKey.Close();

            return result;
        }

        public void SetFont(Font font)
        {
            FontConverter converter = new FontConverter();
            this.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(SubKey, true);
            this.RegistryKey.SetValue(FontKey, converter.ConvertToString(font));
            this.RegistryKey.Close();
        }
    }
}
