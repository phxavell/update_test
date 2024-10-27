using System;
using System.Windows.Forms;
using MaterialSkin;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Drawing;
using System.Management;

namespace WEBCAM
{
    public partial class WEBCAM : MaterialSkin.Controls.MaterialForm
    {
        public string WEBCAM1;

        public WEBCAM()
        {
            InitializeComponent();
            StartFireBaseServices();
            Interacao();
            CriarLog_MySQLOK();
            TimeStart();
        }

        public void Interacao()
        {
            //Interacao - Precisa ficar dentro do evento do form devido auto start das propriedades de video.
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\WebCamReconhecimento.mp3";
            wplayer.controls.play();
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

        public void TimeStart()
        {
            if (WEBCAM1 != "OK")
            {
                Timer relogio = new Timer();
                relogio.Interval = 1000;
                int tempo = 3;

                relogio.Tick += delegate {
                    tempo -= 1;
                    lblTime.Text = tempo.ToString();
                    if (tempo == 0)
                    {
                        try
                        {
                            relogio.Stop();
                            //Limpa se já houver algum arquivo
                            RecFace();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Não foi possivel capturar Imagem" + ex);
                        }
                    }
                };
                relogio.Start();
            }
        }

        public void RecFace()
        {
            WEBCAM1 = "OK";

            WEBCAM_RECFACE formWebCamRecface = new WEBCAM_RECFACE();
            this.Hide();
            formWebCamRecface.ShowDialog();
        }

        public void CriarLog_MySQLOK()
        {
            try
            {
                var dataHoraMinuto = DateTime.Now.ToString("dd/MM/yyyy - HH:mm");
                ManagementObjectSearcher MOS1 = new ManagementObjectSearcher("Select * From Win32_BIOS");
                foreach (ManagementObject getserial in MOS1.Get())
                {
                    string SerialAvell = getserial["SerialNumber"].ToString();
                    String DadosFirebase1 = "Webcam Início:" + dataHoraMinuto;
                    var teste = new webcam1
                    {
                        Serial = SerialAvell,
                        TWebcam = DadosFirebase1
                    };
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTE/" + SerialAvell, teste);
                    SerialAvell = string.Empty;
                    DadosFirebase1 = string.Empty;
                    break;
                }
            }
            catch
            {
                //MessageBox.Show("Não foi possivel inserir os dados!");
                lblFirebase.Text = "Firebase Off-Line";
                lblFirebase.ForeColor = Color.Red;
            }
        }
    }
}
