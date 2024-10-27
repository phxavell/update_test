namespace ROBOTESTE
{
    partial class TEMPORIZADOR
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TEMPORIZADOR));
            this.lblTime = new System.Windows.Forms.Label();
            this.lblTimeInfo = new System.Windows.Forms.Label();
            this.linkAvancar = new System.Windows.Forms.LinkLabel();
            this.lblTempo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTime.Location = new System.Drawing.Point(220, 77);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(54, 24);
            this.lblTime.TabIndex = 19;
            this.lblTime.Text = "2700";
            // 
            // lblTimeInfo
            // 
            this.lblTimeInfo.AutoSize = true;
            this.lblTimeInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblTimeInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeInfo.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTimeInfo.Location = new System.Drawing.Point(6, 77);
            this.lblTimeInfo.Name = "lblTimeInfo";
            this.lblTimeInfo.Size = new System.Drawing.Size(162, 24);
            this.lblTimeInfo.TabIndex = 18;
            this.lblTimeInfo.Text = "TEMPO TESTE:";
            // 
            // linkAvancar
            // 
            this.linkAvancar.AutoSize = true;
            this.linkAvancar.BackColor = System.Drawing.Color.Transparent;
            this.linkAvancar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkAvancar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkAvancar.Location = new System.Drawing.Point(12, 113);
            this.linkAvancar.Name = "linkAvancar";
            this.linkAvancar.Size = new System.Drawing.Size(60, 18);
            this.linkAvancar.TabIndex = 20;
            this.linkAvancar.TabStop = true;
            this.linkAvancar.Text = "Skip->>";
            this.linkAvancar.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAvancar_LinkClicked);
            // 
            // lblTempo
            // 
            this.lblTempo.AutoSize = true;
            this.lblTempo.BackColor = System.Drawing.Color.Transparent;
            this.lblTempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTempo.ForeColor = System.Drawing.Color.Teal;
            this.lblTempo.Location = new System.Drawing.Point(177, 111);
            this.lblTempo.Name = "lblTempo";
            this.lblTempo.Size = new System.Drawing.Size(97, 20);
            this.lblTempo.TabIndex = 21;
            this.lblTempo.Text = "45 Minutos";
            // 
            // TEMPORIZADOR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 140);
            this.Controls.Add(this.lblTempo);
            this.Controls.Add(this.linkAvancar);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblTimeInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TEMPORIZADOR";
            this.Opacity = 0.85D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "TEMPO BURNINXXX:";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.TEMPORIZADOR_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblTimeInfo;
        private System.Windows.Forms.LinkLabel linkAvancar;
        private System.Windows.Forms.Label lblTempo;
    }
}