namespace BLUETOOTH
{
    partial class BLUETOOTH
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BLUETOOTH));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblConect = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lbDriversBluetooth = new System.Windows.Forms.ListBox();
            this.lblFirebase = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::BLUETOOTH.Properties.Resources.BluetoothLogo;
            this.pictureBox1.Location = new System.Drawing.Point(16, 103);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(160, 190);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblConect
            // 
            this.lblConect.AutoSize = true;
            this.lblConect.BackColor = System.Drawing.Color.Transparent;
            this.lblConect.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConect.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblConect.Location = new System.Drawing.Point(345, 89);
            this.lblConect.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConect.Name = "lblConect";
            this.lblConect.Size = new System.Drawing.Size(195, 29);
            this.lblConect.TabIndex = 21;
            this.lblConect.Text = "CONECTANDO";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTime.Location = new System.Drawing.Point(568, 89);
            this.lblTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(27, 29);
            this.lblTime.TabIndex = 20;
            this.lblTime.Text = "3";
            // 
            // lbDriversBluetooth
            // 
            this.lbDriversBluetooth.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbDriversBluetooth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDriversBluetooth.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lbDriversBluetooth.FormattingEnabled = true;
            this.lbDriversBluetooth.ItemHeight = 20;
            this.lbDriversBluetooth.Location = new System.Drawing.Point(189, 175);
            this.lbDriversBluetooth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lbDriversBluetooth.Name = "lbDriversBluetooth";
            this.lbDriversBluetooth.Size = new System.Drawing.Size(396, 180);
            this.lbDriversBluetooth.TabIndex = 22;
            // 
            // lblFirebase
            // 
            this.lblFirebase.AutoSize = true;
            this.lblFirebase.BackColor = System.Drawing.Color.Transparent;
            this.lblFirebase.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirebase.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblFirebase.Location = new System.Drawing.Point(16, 7);
            this.lblFirebase.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFirebase.Name = "lblFirebase";
            this.lblFirebase.Size = new System.Drawing.Size(17, 25);
            this.lblFirebase.TabIndex = 28;
            this.lblFirebase.Text = ".";
            // 
            // BLUETOOTH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 409);
            this.Controls.Add(this.lblFirebase);
            this.Controls.Add(this.lbDriversBluetooth);
            this.Controls.Add(this.lblConect);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "BLUETOOTH";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TESTE FUNCIONAMENTO BLUETOOTH";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.BLUETOOTH_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblConect;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.ListBox lbDriversBluetooth;
        private System.Windows.Forms.Label lblFirebase;
    }
}

