using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DISPLAPORTC
{
    public partial class CONFIRMASOMDPORTC : MaterialSkin.Controls.MaterialForm
    {
        public CONFIRMASOMDPORTC()
        {
            InitializeComponent();
        }

        public void Interacao()
        {
            //https://www.naturalreaders.com/online/ - Cria vozes
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\TesteDISPLAYPORTAudio.mp3";
            wplayer.controls.play();
        }

        private void btnSim_Click(object sender, EventArgs e)
        {
            VALIDAOK formValidaOK = new VALIDAOK();
            this.Hide();
            //Testar ShowDialog com (this);
            formValidaOK.ShowDialog(this);
        }

        private void btnNao_Click(object sender, EventArgs e)
        {
            REPROVAFALHA formReprovaFalha = new REPROVAFALHA();
            this.Hide();
            formReprovaFalha.ShowDialog(this);
        }
    }
}
