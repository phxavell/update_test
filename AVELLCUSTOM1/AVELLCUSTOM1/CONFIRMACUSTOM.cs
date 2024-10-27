using System;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using MaterialSkin;
using System.Management;
using System.Reflection.Emit;
using FireSharp.Config;
using FireSharp.Interfaces;

namespace AVELLCUSTOM1
{
    public partial class CONFIRMACUSTOM : MaterialSkin.Controls.MaterialForm
    {
        public CONFIRMACUSTOM()
        {
            InitializeComponent();
            StartFireBaseServices();
            Interacao();
        }

        //Firebase
        IFirebaseConfig ifc = new FirebaseConfig()
        {
            //Base de Dados da Avell, onde ficam os resultados
            AuthSecret = "BVBQHkHsf2fV2lqrP2GhPLjxufBMdxPoxYYg9XKP",
            BasePath = "https://avellweb-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;
        private object res;

        public void StartFireBaseServices()
        {
            try
            {
                client = new FireSharp.FirebaseClient(ifc);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível inserir os dados");
            }
        }
        //Firebase

        public void Interacao()
        {
            //https://www.naturalreaders.com/online/ - Cria vozes
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\AvellCustomInstalado.mp3";
            wplayer.controls.play();
        }

        public void CriarLogOK_MySQL()
        {
            try
            {
                //Gera Log de OK para o MySql
                var dataHoraMinutoOK = DateTime.Now.ToString("dd-MM-yyyy-HH:mm:s:s");
                const string Furmark = @"C:\TESTES_AVELL\MySqlData\AvellCustom.txt";
                var Arquivo = File.AppendText(Furmark);
                Arquivo.WriteLine("AVELLCUSTOM FALHA: " + dataHoraMinutoOK);
                Arquivo.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pasta não Encotrada" + ex);
            }
        }

        private void btnNao_Click(object sender, EventArgs e)
        {
            REPROVAFALHAS formCustomFALHA = new REPROVAFALHAS();
            this.Hide();
            formCustomFALHA.ShowDialog();
        }

        private void btnSim_Click(object sender, EventArgs e)
        {
            AVELLCUSTOMOK formCustomOK = new AVELLCUSTOMOK();
            this.Hide();
            formCustomOK.ShowDialog();
        }
    }
}
