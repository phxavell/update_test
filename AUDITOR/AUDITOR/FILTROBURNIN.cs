using System;
using System.Windows.Forms;
using MaterialSkin;
using FireSharp.Config;
using FireSharp.Interfaces;
using System.IO;

namespace AUDITOR
{
    public partial class FILTROBURNIN : MaterialSkin.Controls.MaterialForm
    {

        public string AUDITOR = "";

        public FILTROBURNIN()
        {
            InitializeComponent();
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            ApagarPastaBurnin();
            //Seguirá para a próxima etapa
            TimeStart();
        }

        public void ApagarPastaBurnin()
        {
            string pastaResiduoBurnin = @"C:\BurnInTest test files";
            try
            {
                //MessageBox.Show("Limpez Temp:" + caminhoPasta1, "Limpeza Temp.");
                string apagarPasta1 = @"C:\BurnInTest test files";
                Directory.Delete(apagarPasta1, true);
            }
            catch { MessageBox.Show("Não foi possível apagar a pasta:" + pastaResiduoBurnin); }
        }

        public void TimeStart()
        {
            if (AUDITOR != "STARTED")
            {
                Timer relogio = new Timer();
                relogio.Interval = 1000;
                int tempo = 5;
                relogio.Tick += delegate
                {
                    tempo -= 1;
                    lblTime.Text = tempo.ToString();
                    if (tempo == 0)
                    {
                        relogio.Stop();
                        //Verificar se ficou resíduo de testes de Burnin!
                        string caminhoPasta1 = @"C:\BurnInTest test files";
                        if (Directory.Exists(caminhoPasta1))
                        {
                            MessageBox.Show("PASTA:" + caminhoPasta1 + "AINDA NÃO FOI APAGADA!");
                        }
                        else
                        {
                            AUDITOR = "STARTED";
                            AUDITOR formAuditor = new AUDITOR();
                            this.Close();
                            formAuditor.Show();
                        }
                    }
                };
                relogio.Start();
            }
        }
    }
}
