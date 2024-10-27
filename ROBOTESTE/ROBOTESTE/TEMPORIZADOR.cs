using CONTROL_COOLER;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Windows.Forms;

namespace ROBOTESTE
{
    public partial class TEMPORIZADOR : MaterialSkin.Controls.MaterialForm
    {
        public string BURNIN;

        //String para evitar loop!
        public string CCOOLER1;
        public string CCOOLER2;
        public string CCOOLER3;

        public TEMPORIZADOR()
        {
            InitializeComponent();
            Interacao();
            TimeStart();
            //TimeTesteCooler();
        }

        public void Interacao()
        {
            //https://www.naturalreaders.com/online/ - Cria vozes
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\InicioBurnin.mp3";
            wplayer.controls.play();
        }

        public void TimeStart()
        {
            if (CCOOLER1 != "OK")
            {
                TimeTesteCooler();

                Timer relogio = new Timer();
                relogio.Interval = 1000;
                int tempo = 5;
                //int tempo = 60;
                relogio.Tick += delegate {
                    tempo -= 1;
                    lblTime.Text = tempo.ToString();
                    if (tempo == 0)
                    {
                        CCOOLER1 = "OK";
                        relogio.Stop();
                        ConfirmarBurnin();
                    }
                };
                relogio.Start();
            }
        }

        public void TimeTesteCooler()
        {
            //if (!CCOOLER1.Equals("OK")){} - Está é uma ótima forma
            if (CCOOLER2 != "OK")
            {
                Timer relogio = new Timer();
                relogio.Interval = 1000;
                int tempo = 60;
                relogio.Tick += delegate {
                    tempo -= 1;
                    //lblTime.Text = tempo.ToString();
                    if (tempo == 0)
                    {
                        CCOOLER2 = "OK";
                        relogio.Stop();
                        ChamarTesteCooler();
                    }
                };
                relogio.Start();
            }
        }

        public void ChamarTesteCooler()
        {
            //Ajustar para os forms abrir em paralelo e fecharem sem interação!
            CONTROL_COOLER.COOLERS testeCoolerControl = new CONTROL_COOLER.COOLERS();
            testeCoolerControl.ShowDialog();
        }

        private void linkAvancar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ConfirmarSkip();
        }

        public void ConfirmarBurnin()
        {
            CONFIRMABURNIN formConfirmaBurnin = new CONFIRMABURNIN();
            this.Hide();
            formConfirmaBurnin.ShowDialog();
        }

        public void ConfirmarSkip()
        {
            PararTestes();

            CONFIRMASKIP formConfirmaSkip = new CONFIRMASKIP();
            this.Hide();
            formConfirmaSkip.ShowDialog();
        }

        public void PararTestes()
        {
            //Finaliza o processo após a confirmação
            try
            {
                string Burnin = "bit";
                var processes = Process.GetProcessesByName(Burnin);
                foreach (var BurninTeste in processes)
                    BurninTeste.Kill();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possivel finalizar o processo");
            }
        }

        public void MedidorRecursosWindows()
        {
            try
            {
                var startInfo = new ProcessStartInfo(@"C:\Windows\system32\perfmon.exe");
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                Process.Start(startInfo);
            }
            catch (Exception e)
            {
                MessageBox.Show("Não foi possível abir o medidor de recursos do Windows!");
            }
        }

        private void TEMPORIZADOR_Load(object sender, EventArgs e)
        {

        }
    }
}