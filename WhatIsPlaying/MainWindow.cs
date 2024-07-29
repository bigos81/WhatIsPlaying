using System;
using System.Drawing;
using System.Windows.Forms;
using WhatIsPlaying.Properties;

namespace WhatIsPlaying
{
    public partial class MainWindow : Form
    {
        bool animate = true;
        int ticks = 0;
        int scale = 2;
        NotifyIcon trayIcon = new NotifyIcon();
        RegistryManager manager;
        
        public MainWindow()
        {
            InitializeComponent();
            SetupTrayIcon();
            this.manager = new RegistryManager();
        }

        private void SetupTrayIcon()
        {
            trayIcon.Icon = Resources.Icon;
            trayIcon.Visible = true;
            trayIcon.ContextMenuStrip = this.contextMenu;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Bounds = new Rectangle(0, 0, 0, 0);
            this.refreshColors();
            this.animate = this.manager.GetAnimationFlag();
        }

        private void refreshColors()
        {
            this.SongLabel.ForeColor = this.manager.GetFontColor();
            this.SongLabel.BackColor = this.manager.GetBackgroundColor();
            this.SongLabel.Font = this.manager.GetFont();
        }
        
        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            string songName = WindowMediaControlUtils.GetCurrentPlayedMedia();
            if (!String.Empty.Equals(songName))
            {

                this.SongLabel.Text = songName + ' ' + songName;
                if (animate)
                    this.moveText();

                this.Refresh();
            }
        }

        private void moveText()
        {   
            ticks = ticks + 1;
            if (Math.Abs(ticks * this.scale) > this.SongLabel.Width / 2)
            {
                scale = scale * -1;
                ticks = 0;
            }
            this.SongLabel.Location = new Point(this.SongLabel.Location.X + scale, this.SongLabel.Location.Y);
        }
        
        private void SongLabel_SizeChanged(object sender, EventArgs e)
        {
            if (sender is Label)
            {
                this.Width = ((Label)sender).Bounds.Width / 2;
                this.Height = ((Label)sender).Bounds.Height;
            }

            this.ticks = 0;
            this.SongLabel.Location = new Point(0 - this.SongLabel.Width / 2, this.SongLabel.Location.Y);
            this.scale = Math.Abs(this.scale);
        }

        private bool mouseDown;
        private Point lastLocation;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                int newLocationX = (this.Location.X - lastLocation.X) + e.X;
                int newLocationY = (this.Location.Y - lastLocation.Y) + e.Y;
               
                newLocationX = Math.Max(newLocationX, 0);
                newLocationY = Math.Max(newLocationY, 0);
                
                this.Location = new Point(newLocationX, newLocationY);
                this.Update();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void SongLabel_DoubleClick(object sender, EventArgs e)
        {
            if (e is MouseEventArgs)
            {
                if (((MouseEventArgs)e).Button == MouseButtons.Left)
                {
                    this.Close();
                }
            }
            
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem) 
            {
                ((ToolStripMenuItem)sender).Checked = !((ToolStripMenuItem)sender).Checked;
                this.animate = ((ToolStripMenuItem)sender).Checked;
                this.manager.SetAnimationFlag(this.animate);
                this.SongLabel_SizeChanged(null, null);
            }
        }

        private void ExitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FontColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = this.SongLabel.ForeColor;
            dlg.ShowDialog();
            this.SongLabel.ForeColor = dlg.Color;
            this.manager.SetFontColor(dlg.Color);
            refreshColors();
        }

        private void BackgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = this.SongLabel.BackColor;
            dlg.ShowDialog();
            this.SongLabel.BackColor = dlg.Color;
            this.manager.SetBackgroundColor(dlg.Color);
            refreshColors();
        }

        private void ContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            animateToolStripMenuItem.Checked = this.animate;
        }

        private void SongLabel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenu.Show(Cursor.Position);
            }
        }

        private void ChangeFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog dlg = new FontDialog();
            dlg.Font = this.SongLabel.Font;
            dlg.ShowDialog();
            this.SongLabel.Font = dlg.Font;
            this.manager.SetFont(dlg.Font);
            this.Refresh();
        }
    }
}
