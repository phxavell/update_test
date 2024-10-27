namespace LIMPAR_TESTE
{
    partial class LIMPAR_TESTE
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LIMPAR_TESTE));
            this.barraProgresso1 = new System.Windows.Forms.ProgressBar();
            this.pbLimpar1 = new System.Windows.Forms.PictureBox();
            this.timerBarra1 = new System.Windows.Forms.Timer(this.components);
            this.lblTime = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblKey = new System.Windows.Forms.Label();
            this.lblOK4 = new System.Windows.Forms.Label();
            this.lblResiduosTeste = new System.Windows.Forms.Label();
            this.lblOK3 = new System.Windows.Forms.Label();
            this.lblPastaTesteDesk = new System.Windows.Forms.Label();
            this.lblOK2 = new System.Windows.Forms.Label();
            this.lblPastaECDesk = new System.Windows.Forms.Label();
            this.lblOK1 = new System.Windows.Forms.Label();
            this.lblPastaTestes = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblVersao = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbLimpar1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // barraProgresso1
            // 
            this.barraProgresso1.BackColor = System.Drawing.Color.White;
            this.barraProgresso1.Location = new System.Drawing.Point(12, 355);
            this.barraProgresso1.Name = "barraProgresso1";
            this.barraProgresso1.Size = new System.Drawing.Size(586, 17);
            this.barraProgresso1.TabIndex = 1;
            // 
            // pbLimpar1
            // 
            this.pbLimpar1.Image = global::LIMPAR_TESTE.Properties.Resources.windows_logo;
            this.pbLimpar1.Location = new System.Drawing.Point(12, 8);
            this.pbLimpar1.Name = "pbLimpar1";
            this.pbLimpar1.Size = new System.Drawing.Size(106, 87);
            this.pbLimpar1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLimpar1.TabIndex = 0;
            this.pbLimpar1.TabStop = false;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.lblTime.Location = new System.Drawing.Point(8, 314);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(25, 24);
            this.lblTime.TabIndex = 3;
            this.lblTime.Text = "...";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblKey);
            this.panel1.Controls.Add(this.lblOK4);
            this.panel1.Controls.Add(this.lblResiduosTeste);
            this.panel1.Controls.Add(this.lblOK3);
            this.panel1.Controls.Add(this.lblPastaTesteDesk);
            this.panel1.Controls.Add(this.lblOK2);
            this.panel1.Controls.Add(this.lblPastaECDesk);
            this.panel1.Controls.Add(this.lblOK1);
            this.panel1.Controls.Add(this.lblPastaTestes);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.pbLimpar1);
            this.panel1.Controls.Add(this.barraProgresso1);
            this.panel1.Controls.Add(this.lblTime);
            this.panel1.Location = new System.Drawing.Point(0, 64);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(610, 385);
            this.panel1.TabIndex = 4;
            // 
            // lblKey
            // 
            this.lblKey.AutoSize = true;
            this.lblKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKey.ForeColor = System.Drawing.Color.White;
            this.lblKey.Location = new System.Drawing.Point(172, 8);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(30, 24);
            this.lblKey.TabIndex = 13;
            this.lblKey.Text = "....";
            // 
            // lblOK4
            // 
            this.lblOK4.AutoSize = true;
            this.lblOK4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOK4.ForeColor = System.Drawing.SystemColors.Control;
            this.lblOK4.Location = new System.Drawing.Point(187, 243);
            this.lblOK4.Name = "lblOK4";
            this.lblOK4.Size = new System.Drawing.Size(15, 24);
            this.lblOK4.TabIndex = 12;
            this.lblOK4.Text = ".";
            // 
            // lblResiduosTeste
            // 
            this.lblResiduosTeste.AutoSize = true;
            this.lblResiduosTeste.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResiduosTeste.Location = new System.Drawing.Point(13, 243);
            this.lblResiduosTeste.Name = "lblResiduosTeste";
            this.lblResiduosTeste.Size = new System.Drawing.Size(160, 24);
            this.lblResiduosTeste.TabIndex = 11;
            this.lblResiduosTeste.Text = "Checar Residuos:";
            // 
            // lblOK3
            // 
            this.lblOK3.AutoSize = true;
            this.lblOK3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOK3.ForeColor = System.Drawing.SystemColors.Control;
            this.lblOK3.Location = new System.Drawing.Point(187, 205);
            this.lblOK3.Name = "lblOK3";
            this.lblOK3.Size = new System.Drawing.Size(15, 24);
            this.lblOK3.TabIndex = 10;
            this.lblOK3.Text = ".";
            // 
            // lblPastaTesteDesk
            // 
            this.lblPastaTesteDesk.AutoSize = true;
            this.lblPastaTesteDesk.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPastaTesteDesk.Location = new System.Drawing.Point(13, 205);
            this.lblPastaTesteDesk.Name = "lblPastaTesteDesk";
            this.lblPastaTesteDesk.Size = new System.Drawing.Size(168, 24);
            this.lblPastaTesteDesk.TabIndex = 9;
            this.lblPastaTesteDesk.Text = "Pasta Teste Desks:";
            // 
            // lblOK2
            // 
            this.lblOK2.AutoSize = true;
            this.lblOK2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOK2.ForeColor = System.Drawing.SystemColors.Control;
            this.lblOK2.Location = new System.Drawing.Point(187, 167);
            this.lblOK2.Name = "lblOK2";
            this.lblOK2.Size = new System.Drawing.Size(15, 24);
            this.lblOK2.TabIndex = 8;
            this.lblOK2.Text = ".";
            // 
            // lblPastaECDesk
            // 
            this.lblPastaECDesk.AutoSize = true;
            this.lblPastaECDesk.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPastaECDesk.Location = new System.Drawing.Point(13, 167);
            this.lblPastaECDesk.Name = "lblPastaECDesk";
            this.lblPastaECDesk.Size = new System.Drawing.Size(147, 24);
            this.lblPastaECDesk.TabIndex = 7;
            this.lblPastaECDesk.Text = "Pasta EC Desks:";
            // 
            // lblOK1
            // 
            this.lblOK1.AutoSize = true;
            this.lblOK1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOK1.ForeColor = System.Drawing.SystemColors.Control;
            this.lblOK1.Location = new System.Drawing.Point(187, 128);
            this.lblOK1.Name = "lblOK1";
            this.lblOK1.Size = new System.Drawing.Size(15, 24);
            this.lblOK1.TabIndex = 6;
            this.lblOK1.Text = ".";
            // 
            // lblPastaTestes
            // 
            this.lblPastaTestes.AutoSize = true;
            this.lblPastaTestes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPastaTestes.Location = new System.Drawing.Point(12, 128);
            this.lblPastaTestes.Name = "lblPastaTestes";
            this.lblPastaTestes.Size = new System.Drawing.Size(121, 24);
            this.lblPastaTestes.TabIndex = 5;
            this.lblPastaTestes.Text = "Pasta Testes:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::LIMPAR_TESTE.Properties.Resources.Sysprep;
            this.pictureBox1.Location = new System.Drawing.Point(262, 49);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(336, 248);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // lblVersao
            // 
            this.lblVersao.AutoSize = true;
            this.lblVersao.BackColor = System.Drawing.Color.Transparent;
            this.lblVersao.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersao.ForeColor = System.Drawing.Color.Transparent;
            this.lblVersao.Location = new System.Drawing.Point(493, 32);
            this.lblVersao.Name = "lblVersao";
            this.lblVersao.Size = new System.Drawing.Size(105, 24);
            this.lblVersao.TabIndex = 14;
            this.lblVersao.Text = "Versão: 1.0";
            // 
            // LIMPAR_TESTE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 449);
            this.Controls.Add(this.lblVersao);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LIMPAR_TESTE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LIMPEZA DE TESTE E SYSPREP";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pbLimpar1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pbLimpar1;
        private System.Windows.Forms.ProgressBar barraProgresso1;
        private System.Windows.Forms.Timer timerBarra1;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblOK1;
        private System.Windows.Forms.Label lblPastaTestes;
        private System.Windows.Forms.Label lblOK2;
        private System.Windows.Forms.Label lblPastaECDesk;
        private System.Windows.Forms.Label lblOK3;
        private System.Windows.Forms.Label lblPastaTesteDesk;
        private System.Windows.Forms.Label lblResiduosTeste;
        private System.Windows.Forms.Label lblOK4;
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.Label lblVersao;
    }
}

