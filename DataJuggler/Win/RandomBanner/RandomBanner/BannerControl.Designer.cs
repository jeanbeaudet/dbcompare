namespace RandomBanner
{
    partial class BannerControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ChangeBannerTimer = new System.Windows.Forms.Timer(this.components);
            this.BannerImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.BannerImage)).BeginInit();
            this.SuspendLayout();
            // 
            // ChangeBannerTimer
            // 
            this.ChangeBannerTimer.Enabled = true;
            this.ChangeBannerTimer.Interval = 30000;
            this.ChangeBannerTimer.Tick += new System.EventHandler(this.ChangeBannerTimer_Tick);
            // 
            // BannerImage
            // 
            this.BannerImage.BackgroundImage = global::RandomBanner.Properties.Resources.Teethathon_Ad_Back_2;
            this.BannerImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BannerImage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BannerImage.Location = new System.Drawing.Point(0, 0);
            this.BannerImage.MaximumSize = new System.Drawing.Size(400, 400);
            this.BannerImage.MinimumSize = new System.Drawing.Size(400, 400);
            this.BannerImage.Name = "BannerImage";
            this.BannerImage.Size = new System.Drawing.Size(400, 400);
            this.BannerImage.TabIndex = 2;
            this.BannerImage.TabStop = false;
            this.BannerImage.Click += new System.EventHandler(this.BannerControl_Click);
            this.BannerImage.MouseEnter += new System.EventHandler(this.BannerControl_MouseEnter);
            this.BannerImage.MouseLeave += new System.EventHandler(this.BannerControl_MouseLeave);
            // 
            // BannerControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.BannerImage);
            this.MaximumSize = new System.Drawing.Size(400, 400);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "BannerControl";
            this.Size = new System.Drawing.Size(400, 400);
            this.Click += new System.EventHandler(this.BannerControl_Click);
            this.MouseEnter += new System.EventHandler(this.BannerControl_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.BannerControl_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this.BannerImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer ChangeBannerTimer;
        private System.Windows.Forms.PictureBox BannerImage;
    }
}
