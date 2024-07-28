namespace WhatIsPlaying
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SongLabel = new System.Windows.Forms.Label();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.SongLabel2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SongLabel
            // 
            this.SongLabel.AutoSize = true;
            this.SongLabel.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SongLabel.ForeColor = System.Drawing.Color.Lime;
            this.SongLabel.Location = new System.Drawing.Point(0, 0);
            this.SongLabel.Name = "SongLabel";
            this.SongLabel.Size = new System.Drawing.Size(118, 26);
            this.SongLabel.TabIndex = 0;
            this.SongLabel.Text = "Initializing...";
            this.SongLabel.SizeChanged += new System.EventHandler(this.SongLabel_SizeChanged);
            this.SongLabel.DoubleClick += new System.EventHandler(this.SongLabel_DoubleClick);
            this.SongLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.SongLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.SongLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            // 
            // refreshTimer
            // 
            this.refreshTimer.Enabled = true;
            this.refreshTimer.Interval = 33;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // SongLabel2
            // 
            this.SongLabel2.AutoSize = true;
            this.SongLabel2.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SongLabel2.ForeColor = System.Drawing.Color.Lime;
            this.SongLabel2.Location = new System.Drawing.Point(0, 0);
            this.SongLabel2.Name = "SongLabel2";
            this.SongLabel2.Size = new System.Drawing.Size(65, 26);
            this.SongLabel2.TabIndex = 1;
            this.SongLabel2.Text = "label1";
            this.SongLabel2.SizeChanged += new System.EventHandler(this.SongLabel_SizeChanged);
            this.SongLabel2.DoubleClick += new System.EventHandler(this.SongLabel_DoubleClick);
            this.SongLabel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.SongLabel2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.SongLabel2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(411, 69);
            this.ControlBox = false;
            this.Controls.Add(this.SongLabel2);
            this.Controls.Add(this.SongLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "What is Playing";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label SongLabel;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.Label SongLabel2;
    }
}

