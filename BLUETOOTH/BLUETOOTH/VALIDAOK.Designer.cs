﻿namespace BLUETOOTH
{
    partial class VALIDAOK
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
            this.panelOK = new System.Windows.Forms.Panel();
            this.lblTime = new System.Windows.Forms.Label();
            this.pictureBoxOK = new System.Windows.Forms.PictureBox();
            this.lblConfirme = new System.Windows.Forms.Label();
            this.lblFirebase = new System.Windows.Forms.Label();
            this.panelOK.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOK)).BeginInit();
            this.SuspendLayout();
            // 
            // panelOK
            // 
            this.panelOK.BackColor = System.Drawing.Color.ForestGreen;
            this.panelOK.Controls.Add(this.lblTime);
            this.panelOK.Controls.Add(this.pictureBoxOK);
            this.panelOK.Controls.Add(this.lblConfirme);
            this.panelOK.Location = new System.Drawing.Point(0, 64);
            this.panelOK.Name = "panelOK";
            this.panelOK.Size = new System.Drawing.Size(457, 413);
            this.panelOK.TabIndex = 18;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTime.Location = new System.Drawing.Point(405, 285);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(21, 24);
            this.lblTime.TabIndex = 14;
            this.lblTime.Text = "3";
            // 
            // pictureBoxOK
            // 
            this.pictureBoxOK.Image = global::BLUETOOTH.Properties.Resources.confirmarOK;
            this.pictureBoxOK.Location = new System.Drawing.Point(121, 45);
            this.pictureBoxOK.Name = "pictureBoxOK";
            this.pictureBoxOK.Size = new System.Drawing.Size(218, 194);
            this.pictureBoxOK.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxOK.TabIndex = 13;
            this.pictureBoxOK.TabStop = false;
            // 
            // lblConfirme
            // 
            this.lblConfirme.AutoSize = true;
            this.lblConfirme.BackColor = System.Drawing.Color.Transparent;
            this.lblConfirme.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirme.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblConfirme.Location = new System.Drawing.Point(25, 285);
            this.lblConfirme.Name = "lblConfirme";
            this.lblConfirme.Size = new System.Drawing.Size(374, 24);
            this.lblConfirme.TabIndex = 12;
            this.lblConfirme.Text = "SEGUINDO PARA O PRÓXIMO TESTE";
            // 
            // lblFirebase
            // 
            this.lblFirebase.AutoSize = true;
            this.lblFirebase.BackColor = System.Drawing.Color.Transparent;
            this.lblFirebase.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirebase.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblFirebase.Location = new System.Drawing.Point(223, 30);
            this.lblFirebase.Name = "lblFirebase";
            this.lblFirebase.Size = new System.Drawing.Size(14, 20);
            this.lblFirebase.TabIndex = 39;
            this.lblFirebase.Text = ".";
            // 
            // VALIDAOK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 477);
            this.Controls.Add(this.lblFirebase);
            this.Controls.Add(this.panelOK);
            this.Name = "VALIDAOK";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TESTE BLUETOOTH OK!";
            this.TopMost = true;
            this.panelOK.ResumeLayout(false);
            this.panelOK.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOK)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelOK;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.PictureBox pictureBoxOK;
        private System.Windows.Forms.Label lblConfirme;
        private System.Windows.Forms.Label lblFirebase;
    }
}