namespace TESTE_MAQUINAS
{
    partial class FONTEDESCONECTADA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FONTEDESCONECTADA));
            this.pictureBoxFonte = new System.Windows.Forms.PictureBox();
            this.lblTesteLcd = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFonte)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxFonte
            // 
            this.pictureBoxFonte.Image = global::TESTE_MAQUINAS.Properties.Resources.semEnergia;
            this.pictureBoxFonte.Location = new System.Drawing.Point(12, 72);
            this.pictureBoxFonte.Name = "pictureBoxFonte";
            this.pictureBoxFonte.Size = new System.Drawing.Size(266, 129);
            this.pictureBoxFonte.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxFonte.TabIndex = 14;
            this.pictureBoxFonte.TabStop = false;
            // 
            // lblTesteLcd
            // 
            this.lblTesteLcd.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.lblTesteLcd.AutoSize = true;
            this.lblTesteLcd.BackColor = System.Drawing.Color.Transparent;
            this.lblTesteLcd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTesteLcd.ForeColor = System.Drawing.Color.Red;
            this.lblTesteLcd.Location = new System.Drawing.Point(14, 214);
            this.lblTesteLcd.Name = "lblTesteLcd";
            this.lblTesteLcd.Size = new System.Drawing.Size(262, 24);
            this.lblTesteLcd.TabIndex = 18;
            this.lblTesteLcd.Text = "FONTE DESCONECTADA!";
            // 
            // FONTEDESCONECTADA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 255);
            this.Controls.Add(this.lblTesteLcd);
            this.Controls.Add(this.pictureBoxFonte);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FONTEDESCONECTADA";
            this.Opacity = 0.85D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FONTE DESCONECTADA!";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFonte)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxFonte;
        private System.Windows.Forms.Label lblTesteLcd;
    }
}