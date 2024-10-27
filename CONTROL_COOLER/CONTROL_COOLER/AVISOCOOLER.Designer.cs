namespace CONTROL_COOLER
{
    partial class AVISOCOOLER
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AVISOCOOLER));
            this.panelOK = new System.Windows.Forms.Panel();
            this.btnRepetir = new System.Windows.Forms.Button();
            this.lblInforme1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelOK.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelOK
            // 
            this.panelOK.BackColor = System.Drawing.Color.Gold;
            this.panelOK.Controls.Add(this.pictureBox2);
            this.panelOK.Controls.Add(this.btnRepetir);
            this.panelOK.Controls.Add(this.pictureBox1);
            this.panelOK.Controls.Add(this.lblInforme1);
            this.panelOK.Location = new System.Drawing.Point(-1, 64);
            this.panelOK.Name = "panelOK";
            this.panelOK.Size = new System.Drawing.Size(458, 413);
            this.panelOK.TabIndex = 22;
            // 
            // btnRepetir
            // 
            this.btnRepetir.BackColor = System.Drawing.Color.White;
            this.btnRepetir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepetir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepetir.Location = new System.Drawing.Point(156, 356);
            this.btnRepetir.Name = "btnRepetir";
            this.btnRepetir.Size = new System.Drawing.Size(150, 36);
            this.btnRepetir.TabIndex = 14;
            this.btnRepetir.Text = "REPETIR";
            this.btnRepetir.UseVisualStyleBackColor = false;
            this.btnRepetir.Click += new System.EventHandler(this.btnRepetir_Click);
            // 
            // lblInforme1
            // 
            this.lblInforme1.AutoSize = true;
            this.lblInforme1.BackColor = System.Drawing.Color.Transparent;
            this.lblInforme1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInforme1.ForeColor = System.Drawing.Color.Black;
            this.lblInforme1.Location = new System.Drawing.Point(12, 312);
            this.lblInforme1.Name = "lblInforme1";
            this.lblInforme1.Size = new System.Drawing.Size(434, 24);
            this.lblInforme1.TabIndex = 12;
            this.lblInforme1.Text = "PROGRAMA CONTROL NÃO ENCONTRADO";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::CONTROL_COOLER.Properties.Resources.Control;
            this.pictureBox2.Location = new System.Drawing.Point(241, 139);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(205, 159);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 16;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CONTROL_COOLER.Properties.Resources.FalhaCooler;
            this.pictureBox1.Location = new System.Drawing.Point(16, 29);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(420, 258);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // AVISOCOOLER
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 477);
            this.Controls.Add(this.panelOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AVISOCOOLER";
            this.Text = "AVISO COOLERS";
            this.panelOK.ResumeLayout(false);
            this.panelOK.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelOK;
        private System.Windows.Forms.Button btnRepetir;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblInforme1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}