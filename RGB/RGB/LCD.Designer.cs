﻿
namespace RGB
{
    partial class LCD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LCD));
            this.panelOK = new System.Windows.Forms.Panel();
            this.lblTesteLcd = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.pictureBoxOK = new System.Windows.Forms.PictureBox();
            this.lblFirebase = new System.Windows.Forms.Label();
            this.panelOK.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOK)).BeginInit();
            this.SuspendLayout();
            // 
            // panelOK
            // 
            this.panelOK.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.panelOK.Controls.Add(this.lblTesteLcd);
            this.panelOK.Controls.Add(this.lblTime);
            this.panelOK.Controls.Add(this.pictureBoxOK);
            this.panelOK.Location = new System.Drawing.Point(-1, 64);
            this.panelOK.Name = "panelOK";
            this.panelOK.Size = new System.Drawing.Size(458, 413);
            this.panelOK.TabIndex = 18;
            // 
            // lblTesteLcd
            // 
            this.lblTesteLcd.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.lblTesteLcd.AutoSize = true;
            this.lblTesteLcd.BackColor = System.Drawing.Color.Transparent;
            this.lblTesteLcd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTesteLcd.ForeColor = System.Drawing.Color.Black;
            this.lblTesteLcd.Location = new System.Drawing.Point(81, 353);
            this.lblTesteLcd.Name = "lblTesteLcd";
            this.lblTesteLcd.Size = new System.Drawing.Size(228, 24);
            this.lblTesteLcd.TabIndex = 17;
            this.lblTesteLcd.Text = "TESTE DE TELA (LCD)";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.Black;
            this.lblTime.Location = new System.Drawing.Point(353, 353);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(21, 24);
            this.lblTime.TabIndex = 14;
            this.lblTime.Text = "3";
            // 
            // pictureBoxOK
            // 
            this.pictureBoxOK.Image = global::RGB.Properties.Resources.lcds;
            this.pictureBoxOK.Location = new System.Drawing.Point(13, 14);
            this.pictureBoxOK.Name = "pictureBoxOK";
            this.pictureBoxOK.Size = new System.Drawing.Size(433, 292);
            this.pictureBoxOK.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxOK.TabIndex = 13;
            this.pictureBoxOK.TabStop = false;
            // 
            // lblFirebase
            // 
            this.lblFirebase.AutoSize = true;
            this.lblFirebase.BackColor = System.Drawing.Color.Transparent;
            this.lblFirebase.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirebase.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblFirebase.Location = new System.Drawing.Point(101, 32);
            this.lblFirebase.Name = "lblFirebase";
            this.lblFirebase.Size = new System.Drawing.Size(13, 20);
            this.lblFirebase.TabIndex = 25;
            this.lblFirebase.Text = ".";
            // 
            // LCD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 477);
            this.Controls.Add(this.lblFirebase);
            this.Controls.Add(this.panelOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LCD";
            this.Opacity = 0.95D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LCD";
            this.TopMost = true;
            this.panelOK.ResumeLayout(false);
            this.panelOK.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOK)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelOK;
        private System.Windows.Forms.Label lblTesteLcd;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.PictureBox pictureBoxOK;
        private System.Windows.Forms.Label lblFirebase;
    }
}