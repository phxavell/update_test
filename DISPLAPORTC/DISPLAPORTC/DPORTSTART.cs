using System;
using System.Windows.Forms;
using MaterialSkin;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Drawing;
using System.Management;

namespace DISPLAPORTC
{
    public partial class DPORTSTART : MaterialSkin.Controls.MaterialForm
    {
        public string DISPLAYPORTOK1;

        public DPORTSTART()
        {
            InitializeComponent();
            StartFireBaseServices();
            Interacao();
            TimeStart();
        }

        public void Interacao()
        {
            //Asterisk/Beep/Exclamation/Hand/Question - Não utilizados Aqui
            //SystemSounds.Hand.Play();
            //https://www.naturalreaders.com/online/ - Cria vozes
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\DisplayPortTeste.mp3";
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
            if (DISPLAYPORTOK1 == null)//Somente se não tiver nenhum valor na variável
            {
                Timer relogio = new Timer();
                relogio.Interval = 1000;
                int tempo = 15;

                relogio.Tick += delegate {
                    tempo -= 1;
                    lblTime.Text = tempo.ToString();
                    if (tempo == 0)
                    {
                        try
                        {
                            relogio.Stop();
                            //Inserir Start Firebase
                            InsertAvellWeb();
                            //Limpa se já houver algum arquivo
                            DporteTeste();

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

        public void DporteTeste()
        {
            if (DISPLAYPORTOK1 != "TESTADO")
            {
                DISPLAYPORTOK1 = "TESTADO";
                DISPLAYPORTC formDisplayC = new DISPLAYPORTC();
                this.Hide();
                formDisplayC.ShowDialog();
            }
        }

        public void InsertAvellWeb()
        {
            try
            {
                var dataHoraMinuto = DateTime.Now.ToString("dd/MM/yyyy - HH:mm");
                ManagementObjectSearcher MOS1 = new ManagementObjectSearcher("Select * From Win32_BIOS");
                foreach (ManagementObject getserial in MOS1.Get())
                {
                    string SerialAvell = getserial["SerialNumber"].ToString();
                    String DadosFirebase1 = "HDMI Início:" + dataHoraMinuto;
                    var teste = new dport1
                    {
                        Serial = SerialAvell,
                        TDisplayPortC = DadosFirebase1
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
