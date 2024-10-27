namespace CONTROL_COOLER
{
    partial class VALIDACONFIRMA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VALIDACONFIRMA));
            this.lblConfirme = new System.Windows.Forms.Label();
            this.btnBurnin = new System.Windows.Forms.Button();
            this.btnCustomControl = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblConfirma = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblConfirme
            // 
            this.lblConfirme.AutoSize = true;
            this.lblConfirme.BackColor = System.Drawing.Color.Transparent;
            this.lblConfirme.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirme.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblConfirme.Location = new System.Drawing.Point(60, 318);
            this.lblConfirme.Name = "lblConfirme";
            this.lblConfirme.Size = new System.Drawing.Size(97, 24);
            this.lblConfirme.TabIndex = 27;
            this.lblConfirme.Text = "REPETIR";
            // 
            // btnBurnin
            // 
            this.btnBurnin.BackColor = System.Drawing.Color.Transparent;
            this.btnBurnin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBurnin.Image = global::CONTROL_COOLER.Properties.Resources.btnBurnin;
            this.btnBurnin.Location = new System.Drawing.Point(260, 345);
            this.btnBurnin.Name = "btnBurnin";
            this.btnBurnin.Size = new System.Drawing.Size(140, 81);
            this.btnBurnin.TabIndex = 5;
            this.btnBurnin.UseVisualStyleBackColor = false;
            this.btnBurnin.Click += new System.EventHandler(this.btnBurnin_Click);
            // 
            // btnCustomControl
            // 
            this.btnCustomControl.BackColor = System.Drawing.Color.Transparent;
            this.btnCustomControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustomControl.Image = global::CONTROL_COOLER.Properties.Resources.btnCustom;
            this.btnCustomControl.Location = new System.Drawing.Point(37, 345);
            this.btnCustomControl.Name = "btnCustomControl";
            this.btnCustomControl.Size = new System.Drawing.Size(140, 81);
            this.btnCustomControl.TabIndex = 30;
            this.btnCustomControl.UseVisualStyleBackColor = false;
            this.btnCustomControl.Click += new System.EventHandler(this.btnCustomControl_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CONTROL_COOLER.Properties.Resources.AnimaControlCooler;
            this.pictureBox1.Location = new System.Drawing.Point(12, 102);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(413, 201);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 26;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(290, 318);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 24);
            this.label1.TabIndex = 32;
            this.label1.Text = "BURNIN";
            // 
            // lblTime
            // 
            this.lblTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTime.AutoSize = true;
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTime.Location = new System.Drawing.Point(393, 306);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(32, 24);
            this.lblTime.TabIndex = 6;
            this.lblTime.Text = "20";
            // 
            // lblConfirma
            // 
            this.lblConfirma.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConfirma.AutoSize = true;
            this.lblConfirma.BackColor = System.Drawing.Color.Transparent;
            this.lblConfirma.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirma.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblConfirma.Location = new System.Drawing.Point(14, 69);
            this.lblConfirma.Name = "lblConfirma";
            this.lblConfirma.Size = new System.Drawing.Size(407, 24);
            this.lblConfirma.TabIndex = 33;
            this.lblConfirma.Text = "PRESSIONE BURNIN PARA PROSSEGUIR";
            // 
            // VALIDACONFIRMA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 438);
            this.Controls.Add(this.lblConfirma);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBurnin);
            this.Controls.Add(this.btnCustomControl);
            this.Controls.Add(this.lblConfirme);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VALIDACONFIRMA";
            this.Opacity = 0.9D;
            this.Text = "TESTE DE COOLER.";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblConfirme;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnCustomControl;
        private System.Windows.Forms.Button btnBurnin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblConfirma;
    }
}