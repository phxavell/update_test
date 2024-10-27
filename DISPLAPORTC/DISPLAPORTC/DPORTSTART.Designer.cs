namespace DISPLAPORTC
{
    partial class DPORTSTART
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DPORTSTART));
            this.lblFirebase = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblConfirme = new System.Windows.Forms.Label();
            this.pictureBoxEstender = new System.Windows.Forms.PictureBox();
            this.pictureBoxConectar = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAviso = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEstender)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxConectar)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFirebase
            // 
            this.lblFirebase.AutoSize = true;
            this.lblFirebase.BackColor = System.Drawing.Color.Transparent;
            this.lblFirebase.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirebase.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblFirebase.Location = new System.Drawing.Point(212, 36);
            this.lblFirebase.Name = "lblFirebase";
            this.lblFirebase.Size = new System.Drawing.Size(13, 20);
            this.lblFirebase.TabIndex = 34;
            this.lblFirebase.Text = ".";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTime.Location = new System.Drawing.Point(296, 565);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(32, 24);
            this.lblTime.TabIndex = 32;
            this.lblTime.Text = "15";
            // 
            // lblConfirme
            // 
            this.lblConfirme.AutoSize = true;
            this.lblConfirme.BackColor = System.Drawing.Color.Transparent;
            this.lblConfirme.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirme.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblConfirme.Location = new System.Drawing.Point(47, 565);
            this.lblConfirme.Name = "lblConfirme";
            this.lblConfirme.Size = new System.Drawing.Size(241, 24);
            this.lblConfirme.TabIndex = 31;
            this.lblConfirme.Text = "TESTES DISPLAY PORT";
            // 
            // pictureBoxEstender
            // 
            this.pictureBoxEstender.Image = global::DISPLAPORTC.Properties.Resources.selecioneEstender;
            this.pictureBoxEstender.Location = new System.Drawing.Point(47, 230);
            this.pictureBoxEstender.Name = "pictureBoxEstender";
            this.pictureBoxEstender.Size = new System.Drawing.Size(281, 329);
            this.pictureBoxEstender.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxEstender.TabIndex = 33;
            this.pictureBoxEstender.TabStop = false;
            // 
            // pictureBoxConectar
            // 
            this.pictureBoxConectar.Image = global::DISPLAPORTC.Properties.Resources.conectDPPORT;
            this.pictureBoxConectar.Location = new System.Drawing.Point(47, 119);
            this.pictureBoxConectar.Name = "pictureBoxConectar";
            this.pictureBoxConectar.Size = new System.Drawing.Size(281, 103);
            this.pictureBoxConectar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxConectar.TabIndex = 30;
            this.pictureBoxConectar.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Crimson;
            this.label1.Location = new System.Drawing.Point(42, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(289, 24);
            this.label1.TabIndex = 36;
            this.label1.Text = "CONECT O DISPLAY PORT C";
            // 
            // lblAviso
            // 
            this.lblAviso.AutoSize = true;
            this.lblAviso.BackColor = System.Drawing.Color.Transparent;
            this.lblAviso.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAviso.ForeColor = System.Drawing.Color.Crimson;
            this.lblAviso.Location = new System.Drawing.Point(71, 68);
            this.lblAviso.Name = "lblAviso";
            this.lblAviso.Size = new System.Drawing.Size(230, 24);
            this.lblAviso.TabIndex = 35;
            this.lblAviso.Text = "DESCONECTE O HDMI";
            // 
            // DPORTSTART
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 610);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblAviso);
            this.Controls.Add(this.lblFirebase);
            this.Controls.Add(this.pictureBoxEstender);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblConfirme);
            this.Controls.Add(this.pictureBoxConectar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DPORTSTART";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TESTE DISPLAY  PORT";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEstender)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxConectar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFirebase;
        private System.Windows.Forms.PictureBox pictureBoxEstender;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblConfirme;
        private System.Windows.Forms.PictureBox pictureBoxConectar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAviso;
    }
}