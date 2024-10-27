using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CONTROL_COOLER
{
    public partial class VALIDACONFIRMA : MaterialSkin.Controls.MaterialForm
    {
        public string ChamarBurnin;

        public VALIDACONFIRMA()
        {
            InitializeComponent();
            InteracaoGPU_CPU();
            //TimerValida();
        }

        public void InteracaoGPU_CPU()
        {
            //https://www.naturalreaders.com/online/ - Cria vozes
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\ControlCoolers.mp3";
            wplayer.controls.play();
        }

        public void TimerValida()
        {//SOMENTE PARA NÃO FICAR EM LOOP
            if (ChamarBurnin != "OK")
            {
                try
                {
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 20;

                    relogio.Tick += delegate {
                        tempo -= 1;
                        lblTime.Text = tempo.ToString();
                        if (tempo == 0)
                        {
                            relogio.Stop();

                            ChamarBurnin = "OK";
                            MessageBox.Show("CHAMAR O BURNIN TESTE");

                            //Fechar
                            this.Hide();
                            //this.Close();
                        }
                    };
                    relogio.Start();
                }
                catch (Exception ex) { }
            }
        }

        private void btnCustomControl_Click(object sender, EventArgs e)
        {
            COOLERS formPrincipal = new COOLERS();
            this.Hide();
            formPrincipal.ShowDialog();
        }

        private void btnBurnin_Click(object sender, EventArgs e)
        {
            //Colocar para fechar automaticamente
            //ROBOTESTE.ROBOTESTE formRoboForm = new ROBOTESTE.ROBOTESTE();
            //this.Hide();
            //formRoboForm.ShowDialog();
        }
    }
}
