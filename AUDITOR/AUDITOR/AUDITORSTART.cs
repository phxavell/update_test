using System;
using System.Windows.Forms;
using MaterialSkin;
using FireSharp.Config;
using FireSharp.Interfaces;
using System.IO;
using System.Diagnostics;

namespace AUDITOR
{
    public partial class AUDITORSTART : MaterialSkin.Controls.MaterialForm
    {
        public string AUDITOR = "";

        public AUDITORSTART()
        {
            InitializeComponent();
            //AtivacaoDeLicenca();
            Interacao();
            FiltroResiduoBurnin();
        }

        public void Interacao()
        {
            //https://www.naturalreaders.com/online/ - Cria vozes
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\AuditoriaInicioConfig.mp3";
            wplayer.controls.play();
        }

        public void AtivacaoDeLicenca()
        {
            try
            {
                //3.4-Etapa foi remanejada para o Auditor pois não vai conseguir ativar no início, passo foi substituido pela Lic23
                ProcessStartInfo AplicarLicenca = new ProcessStartInfo("cmd.exe", @"/C " + "@for /f \"tokens=2 delims==\" %%f in ('wmic path softwarelicensingservice get OA3xOriginalProductKey /value ^| find \"=\"') do @set Win5x5Key=%%f");
                AplicarLicenca.UseShellExecute = false;
                AplicarLicenca.CreateNoWindow = true;
                Process.Start(AplicarLicenca);
            }
            catch (Exception ex) { }
        }

        public void FiltroResiduoBurnin()
        {
            //Verificar se ficou resíduo de testes de Burnin!
            string caminhoPasta1 = @"C:\BurnInTest test files";
            if (Directory.Exists(caminhoPasta1))
            {
                FILTROBURNIN formFiltroBurnin = new FILTROBURNIN();
                this.Hide();
                formFiltroBurnin.ShowDialog();
            }
            else
            {
                TimeStart();
            }
        }

        public void TimeStart()
        {
            if (AUDITOR != "STARTED")
            {
                Timer relogio = new Timer();
                relogio.Interval = 1000;
                int tempo = 10;
                relogio.Tick += delegate
                {
                    tempo -= 1;
                    lblTime.Text = tempo.ToString();
                    if (tempo == 0)
                    {
                        relogio.Stop();

                        AUDITOR = "STARTED";
                        //Chama o form de Auditoria de máquina
                        AUDITOR formAuditor = new AUDITOR();
                        //this.Hide();
                        this.Close();
                        formAuditor.Show();
                    }
                };
                relogio.Start();
            }
        }
    }
}
