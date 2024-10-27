using System;
using System.Windows.Forms;
using MaterialSkin;
using FireSharp.Config;
using FireSharp.Interfaces;

namespace DISPLAPORTC
{
    public partial class CONFIRMADPORTC : MaterialSkin.Controls.MaterialForm
    {
        public CONFIRMADPORTC()
        {
            InitializeComponent();
            Interacao();
        }

        public void Interacao()
        {
            //Trocar áudio do form
            //https://www.naturalreaders.com/online/ - Cria vozes
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\TesteHDMIConfirma.mp3";
            wplayer.controls.play();
        }

        private void btnSim_Click(object sender, EventArgs e)
        {
            //DISPLAYPORTAUDIO formDISPPORTAudio = new DISPLAYPORTAUDIO();
            //this.Hide();
            //formDISPPORTAudio.ShowDialog();
        }

        private void btnNao_Click(object sender, EventArgs e)
        {
            REPROVAFALHA formReprovaFalha = new REPROVAFALHA();
            this.Hide();
            formReprovaFalha.ShowDialog();
        }
    }
}
