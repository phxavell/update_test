using System;
using System.IO;
using System.Windows.Forms;
using MaterialSkin;
using System.Management;
using System.Reflection.Emit;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Drawing;

namespace RGB
{
    public partial class VALIDAOK : MaterialSkin.Controls.MaterialForm
    {
        public string LCD1;

        public VALIDAOK()
        {
            InitializeComponent();
            StartFireBaseServices();
            Interacao();
            TimeStart();
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
            //Asterisk/Beep/Exclamation/Hand/Question - Não utilizados Aqui
            //SystemSounds.Hand.Play();
            //https://www.naturalreaders.com/online/ - Cria vozes
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\LCDOk.mp3";
            wplayer.controls.play();
        }

        public void TimeStart()
        //Preciso adicionar este time para tirar o bug de abrir o form antes de finalizar o processo.
        {
            if (LCD1 != "OK")
            {
                Timer relogio = new Timer();
                relogio.Interval = 1000;
                int tempo = 3;

                relogio.Tick += delegate {
                    tempo -= 1;
                    lblTime.Text = tempo.ToString();
                    if (tempo == 0)
                    {
                        relogio.Stop();

                        //CriarLogOK_MySQL();
                        CriarLog_MySQLOK();
                        GerarLogIndividual();

                        LCD1 = "OK";

                        //RelatorioAprovado();
                        AUDIO.AUDIOSTART formAudio = new AUDIO.AUDIOSTART();
                        this.Hide();
                        formAudio.ShowDialog();
                    }
                };
                relogio.Start();
            }
        }

        public void CriarLogOK_MySQL()
        {
            try
            {
                //Gera Log de OK para o MySql
                var dataHoraMinutoOK = DateTime.Now.ToString("dd-MM-yyyy-HH:mm:s:s");
                const string Lcd = @"C:\TESTES_AVELL\MySqlData\Lcd.txt";
                var Arquivo = File.AppendText(Lcd);
                Arquivo.WriteLine("LCD OK: " + dataHoraMinutoOK);
                Arquivo.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pasta não Encotrada" + ex);
            }
        }

        public void GerarLogIndividual()
        {
            try
            {
                //Gera Log de Instalado para os Drivers
                var dataHoraMinutoOK = DateTime.Now.ToString("dd-MM-yyyy-HH:mm:s:s");
                const string Drivers = @"C:\TESTES_AVELL\logs_lcd\Lcd.log";
                var Arquivo = File.AppendText(Drivers);
                Arquivo.WriteLine("TESTE LCD REALIZADO: " + dataHoraMinutoOK);
                Arquivo.Close();

                //Gera arquivo de Log da fulga de luz
                const string Drivers2 = @"C:\TESTES_AVELL\logs_lcd\FulgaLuz.log";
                var Arquivo2 = File.AppendText(Drivers2);
                Arquivo2.WriteLine("TESTE LCD REALIZADO: " + dataHoraMinutoOK);
                Arquivo2.Close();
            }
            catch (Exception ex) { }
        }

        public void CriarLog_MySQLOK()
        {
            ManagementObjectSearcher mSearcher = new ManagementObjectSearcher("SELECT SerialNumber, ReleaseDate FROM Win32_BIOS");
            ManagementObjectCollection collection = mSearcher.Get();
            foreach (ManagementObject obj in collection)
            {
                //Dll: System.Management.dll - Para conseguir informações da BIOS
                string NomeArquivo = (string)obj["SerialNumber"];

                //Gera Log de OK para o MySql
                var dataHoraMinutoOK = DateTime.Now.ToString("dd-MM-yyyy-HH:mm:s:s");
                string Furmark = "C:\\TESTES_AVELL\\MySqlData\\" + NomeArquivo + "\\LCD-Tela_OK.txt";
                var Arquivo = File.AppendText(Furmark);
                Arquivo.WriteLine("LCD TESTE RGB E FULGA DE LUZ OK: " + dataHoraMinutoOK);
                Arquivo.Close();
                break;
            }

            try
            {
                var dataHoraMinuto = DateTime.Now.ToString("dd/MM/yyyy - HH:mm");
                ManagementObjectSearcher MOS1 = new ManagementObjectSearcher("Select * From Win32_BIOS");
                foreach (ManagementObject getserial in MOS1.Get())
                {
                    string SerialAvell = getserial["SerialNumber"].ToString();
                    String DadosFirebase1 = "Lcd OK!:" + dataHoraMinuto;
                    var teste = new rgb
                    {
                        Serial = SerialAvell,
                        TLcd = DadosFirebase1
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
