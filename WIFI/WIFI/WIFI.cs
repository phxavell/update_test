using System;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Net;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace WIFI
{
    public partial class WIFI : MaterialSkin.Controls.MaterialForm
    {
        public string WIFI1;

        public WIFI()
        {
            InitializeComponent();
            StartFireBaseServices();
            Interacao();
            TimeStart();
        }

        public void Interacao()
        {
            //Interacao - Precisa ficar dentro do evento do form devido auto start das propriedades de video.
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\WIFITeste.mp3";
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

        public void InteracaoConexaoOK()
        {
            pictureBoxStatus.Image = Image.FromFile(@"C:\TESTES_AVELL\img_wifi\wifiOk.jpg");
            lblStatus.Text = "OK, CONECTADO!";
            lblStatus.ForeColor = Color.DarkOliveGreen;
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\WIFITesteOk.mp3";
            wplayer.controls.play();

            TimeStartValidaOk();
        }

        public void TimeStartValidaOk()
        {
            if (WIFI1 != "OK")
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

                            WIFI1 = "OK";
                            //Método à executar:
                            VALIDAOK formValidaOk = new VALIDAOK();
                            this.Hide();
                            formValidaOk.ShowDialog();
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

        public void InteracaoConexaoFalha()
        {
            pictureBoxStatus.Image = Image.FromFile(@"C:\TESTES_AVELL\img_wifi\wifiFalha.jpg");
            lblStatus.Text = "NÃO CONECTADO!";
            lblStatus.BackColor = Color.DarkRed;
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\WIFITesteFalha.mp3";
            wplayer.controls.play();

            TimeStarReprovaFalha();
        }

        public void TimeStarReprovaFalha()
        {
            if (WIFI1 != "OK")
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

                            WIFI1 = "OK";

                            //Método à executar:
                            REPROVAFALHA formReprovaFalha = new REPROVAFALHA();
                            this.Hide();
                            formReprovaFalha.ShowDialog();
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

        public void TimeStart()
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
                        //Informacoes AvellWerb Start
                        InsertAvellWeb();

                        //Método à executar:
                        StatusTeste();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Não foi possivel capturar Imagem" + ex);
                    }
                }
            };
            relogio.Start();
        }

        public bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    WebProxy wp = new WebProxy();
                    client.Proxy = wp;
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public void StatusTeste()
        {
            //Método simplificado verificação de um parâmetro booleano (sim ou não)
            if (CheckForInternetConnection())
                InteracaoConexaoOK();
            else
                InteracaoConexaoFalha();
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
                    String DadosFirebase1 = "Wifi Início:" + dataHoraMinuto;
                    var teste = new wifi1
                    {
                        Serial = SerialAvell,
                        TWifi = DadosFirebase1
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
