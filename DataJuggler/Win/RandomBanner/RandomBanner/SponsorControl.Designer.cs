namespace RandomBanner
{
    partial class SponsorControl
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
            this.TopSponserLabel = new System.Windows.Forms.Label();
            this.SponsorImageHolder = new System.Windows.Forms.Panel();
            this.SponsorButton = new System.Windows.Forms.Panel();
            this.RightMarginPanel = new System.Windows.Forms.Panel();
            this.LeftMarginPanel = new System.Windows.Forms.Panel();
            this.ThisMonthsSponsorLabel = new System.Windows.Forms.Label();
            this.ToSponsorLabel = new System.Windows.Forms.Label();
            this.MyEmailLabel = new System.Windows.Forms.Label();
            this.SponsorImageHolder.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopSponserLabel
            // 
            this.TopSponserLabel.BackColor = System.Drawing.Color.Transparent;
            this.TopSponserLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopSponserLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TopSponserLabel.ForeColor = System.Drawing.Color.LemonChiffon;
            this.TopSponserLabel.Location = new System.Drawing.Point(0, 0);
            this.TopSponserLabel.Name = "TopSponserLabel";
            this.TopSponserLabel.Size = new System.Drawing.Size(400, 36);
            this.TopSponserLabel.TabIndex = 14;
            this.TopSponserLabel.Text = "Thank you to this month\'s";
            this.TopSponserLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SponsorImageHolder
            // 
            this.SponsorImageHolder.BackColor = System.Drawing.Color.Transparent;
            this.SponsorImageHolder.Controls.Add(this.SponsorButton);
            this.SponsorImageHolder.Controls.Add(this.RightMarginPanel);
            this.SponsorImageHolder.Controls.Add(this.LeftMarginPanel);
            this.SponsorImageHolder.Dock = System.Windows.Forms.DockStyle.Top;
            this.SponsorImageHolder.Location = new System.Drawing.Point(0, 36);
            this.SponsorImageHolder.Name = "SponsorImageHolder";
            this.SponsorImageHolder.Size = new System.Drawing.Size(400, 80);
            this.SponsorImageHolder.TabIndex = 16;
            // 
            // SponsorButton
            // 
            this.SponsorButton.BackColor = System.Drawing.Color.Transparent;
            this.SponsorButton.BackgroundImage = global::RandomBanner.Properties.Resources.Sponsor;
            this.SponsorButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SponsorButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.SponsorButton.Location = new System.Drawing.Point(60, 0);
            this.SponsorButton.MaximumSize = new System.Drawing.Size(280, 80);
            this.SponsorButton.MinimumSize = new System.Drawing.Size(280, 80);
            this.SponsorButton.Name = "SponsorButton";
            this.SponsorButton.Size = new System.Drawing.Size(280, 80);
            this.SponsorButton.TabIndex = 13;
            this.SponsorButton.Click += new System.EventHandler(this.SponsorButton_Click);
            this.SponsorButton.MouseEnter += new System.EventHandler(this.Mouse_Enter);
            this.SponsorButton.MouseLeave += new System.EventHandler(this.Mouse_Leave);
            // 
            // RightMarginPanel
            // 
            this.RightMarginPanel.BackColor = System.Drawing.Color.Transparent;
            this.RightMarginPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightMarginPanel.Location = new System.Drawing.Point(340, 0);
            this.RightMarginPanel.Name = "RightMarginPanel";
            this.RightMarginPanel.Size = new System.Drawing.Size(60, 80);
            this.RightMarginPanel.TabIndex = 1;
            // 
            // LeftMarginPanel
            // 
            this.LeftMarginPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftMarginPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftMarginPanel.Name = "LeftMarginPanel";
            this.LeftMarginPanel.Size = new System.Drawing.Size(60, 80);
            this.LeftMarginPanel.TabIndex = 0;
            // 
            // ThisMonthsSponsorLabel
            // 
            this.ThisMonthsSponsorLabel.BackColor = System.Drawing.Color.Transparent;
            this.ThisMonthsSponsorLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ThisMonthsSponsorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ThisMonthsSponsorLabel.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.ThisMonthsSponsorLabel.Location = new System.Drawing.Point(0, 116);
            this.ThisMonthsSponsorLabel.Name = "ThisMonthsSponsorLabel";
            this.ThisMonthsSponsorLabel.Size = new System.Drawing.Size(400, 36);
            this.ThisMonthsSponsorLabel.TabIndex = 22;
            this.ThisMonthsSponsorLabel.Text = "The Libertarian Dictator Audio Book";
            this.ThisMonthsSponsorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ThisMonthsSponsorLabel.Click += new System.EventHandler(this.SponsorButton_Click);
            this.ThisMonthsSponsorLabel.MouseEnter += new System.EventHandler(this.Mouse_Enter);
            this.ThisMonthsSponsorLabel.MouseLeave += new System.EventHandler(this.Mouse_Leave);
            // 
            // ToSponsorLabel
            // 
            this.ToSponsorLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ToSponsorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToSponsorLabel.ForeColor = System.Drawing.Color.LemonChiffon;
            this.ToSponsorLabel.Location = new System.Drawing.Point(0, 152);
            this.ToSponsorLabel.Name = "ToSponsorLabel";
            this.ToSponsorLabel.Size = new System.Drawing.Size(400, 24);
            this.ToSponsorLabel.TabIndex = 27;
            this.ToSponsorLabel.Text = "For info about sponsoring a project";
            this.ToSponsorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MyEmailLabel
            // 
            this.MyEmailLabel.BackColor = System.Drawing.Color.Transparent;
            this.MyEmailLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.MyEmailLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MyEmailLabel.ForeColor = System.Drawing.Color.LemonChiffon;
            this.MyEmailLabel.Location = new System.Drawing.Point(0, 176);
            this.MyEmailLabel.Name = "MyEmailLabel";
            this.MyEmailLabel.Size = new System.Drawing.Size(400, 24);
            this.MyEmailLabel.TabIndex = 28;
            this.MyEmailLabel.Text = "email corby@datajuggler.com";
            this.MyEmailLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SponsorControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.MyEmailLabel);
            this.Controls.Add(this.ToSponsorLabel);
            this.Controls.Add(this.ThisMonthsSponsorLabel);
            this.Controls.Add(this.SponsorImageHolder);
            this.Controls.Add(this.TopSponserLabel);
            this.MaximumSize = new System.Drawing.Size(400, 216);
            this.MinimumSize = new System.Drawing.Size(400, 216);
            this.Name = "SponsorControl";
            this.Size = new System.Drawing.Size(400, 216);
            this.SponsorImageHolder.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label TopSponserLabel;
        private System.Windows.Forms.Panel SponsorImageHolder;
        private System.Windows.Forms.Panel SponsorButton;
        private System.Windows.Forms.Panel RightMarginPanel;
        private System.Windows.Forms.Panel LeftMarginPanel;
        private System.Windows.Forms.Label ThisMonthsSponsorLabel;
        private System.Windows.Forms.Label ToSponsorLabel;
        private System.Windows.Forms.Label MyEmailLabel;
    }
}
