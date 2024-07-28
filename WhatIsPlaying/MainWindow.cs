using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhatIsPlaying
{
    public partial class MainWindow : Form
    {

        int ticks = 0;
        int scale = 2;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Bounds = new Rectangle(0, 0, 0, 0);
        }
        
        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            this.SongLabel.Text = WindowMediaControlUtils.GetCurrentPlayedMedia();
            this.SongLabel2.Text = WindowMediaControlUtils.GetCurrentPlayedMedia();
            this.moveText();
            this.Refresh();
        }

        private void moveText()
        {   

            ticks = ticks + 1;
            if (Math.Abs(ticks * this.scale) > this.SongLabel2.Width)
            {
                scale = scale * -1;
                ticks = 0;
                //this.SongLabel2.Location = new Point(this.SongLabel.Location.X - this.SongLabel.Width, this.SongLabel.Location.Y);
            }
            this.SongLabel.Location = new Point(this.SongLabel.Location.X + scale, this.SongLabel.Location.Y);
            this.SongLabel2.Location = new Point(this.SongLabel.Location.X - this.SongLabel.Width, this.SongLabel.Location.Y);
        }
        
        private void SongLabel_SizeChanged(object sender, EventArgs e)
        {
            if (sender is Label)
            {
                this.Width = ((Label)sender).Bounds.Width;
                this.Height = ((Label)sender).Bounds.Height;
            }

            this.ticks = 0;
            this.SongLabel.Location = new Point(0, this.SongLabel.Location.Y);
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
            this.Close();
        }
    }
}
