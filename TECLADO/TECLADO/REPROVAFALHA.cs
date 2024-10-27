using System;
using System.IO;
using System.Management;
using System.Windows.Forms;
using MaterialSkin;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace TECLADO
{
    public partial class REPROVAFALHA : MaterialSkin.Controls.MaterialForm
    {
        public string FALHA;

        public REPROVAFALHA()
        {
            InitializeComponent();
            StartFireBaseServices();
            Interacao();
            CriarLogFalha();
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
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\TecladoFalha.mp3";
            wplayer.controls.play();
        }

        private void btnReteste_Click(object sender, EventArgs e)
        {
            //Não faz muito sentido ativar esta TAG porque só toma ação
            //se o botão do reteste for pressionado
            FALHA = "OK";

            InsertAvellWeb();
            TESTETECLADO formTesteTeclado = new TESTETECLADO();
            this.Hide();
            formTesteTeclado.ShowDialog();
        }

        public void CriarLogFalha()
        {
            try
            {
                var dataHoraMinuto = DateTime.Now.ToString("dd-MM-yyyy-HH-mms-s");
                //Criar log de voz
                System.IO.StreamWriter sw2 = new StreamWriter(@"C:\TESTES_AVELL\logs_teclado\Falha" + dataHoraMinuto + ".log");
                //System.IO.StreamWriter sw2 = new StreamWriter(@"C:\TESTES_AVELL\logs_usb\falha.log");
                sw2.WriteLine("Falha em Testes Dia:" + dataHoraMinuto);
                sw2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        public void InsertAvellWeb()
        {
            ManagementObjectSearcher mSearcher = new ManagementObjectSearcher("SELECT SerialNumber, ReleaseDate FROM Win32_BIOS");
            ManagementObjectCollection collection = mSearcher.Get();
            foreach (ManagementObject obj in collection)
            {
                //Dll: System.Management.dll - Para conseguir informações da BIOS
                string NomeArquivo = (string)obj["SerialNumber"];

                //Gera Log de OK para o MySql
                var dataHoraMinutoOK = DateTime.Now.ToString("dd-MM-yyyy-HH:mm:s:s");
                string Furmark = "C:\\TESTES_AVELL\\MySqlData\\" + NomeArquivo + "\\Furmark_FALHA.txt";
                var Arquivo = File.AppendText(Furmark);
                Arquivo.WriteLine("TECLADO FALHA!: " + dataHoraMinutoOK);
                Arquivo.Close();
                break;
            }

            try
            {
                //Firebase - Atualizado
                var dataHoraMinuto = DateTime.Now.ToString("dd/MM/yyyy - HH:mm");
                ManagementObjectSearcher MOS1 = new ManagementObjectSearcher("Select * From Win32_BIOS");
                foreach (ManagementObject getserial in MOS1.Get())
                {
                    string SerialAvell = getserial["SerialNumber"].ToString();
                    String DadosFirebase1 = "Teclado FALHA!:" + dataHoraMinuto;
                    var teste = new teclado
                    {
                        Serial = SerialAvell,
                        TTeclado = DadosFirebase1
                    };
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTE/" + SerialAvell, teste);
                    SerialAvell = string.Empty;
                    DadosFirebase1 = string.Empty;
                    break;
                }
                //Firebase - Atualizado

                //Firebase - Atualizado
                foreach (ManagementObject getserial in MOS1.Get())
                {
                    string SerialAvell = getserial["SerialNumber"].ToString();
                    String DadosFirebase1 = "Teclado FALHA!:" + dataHoraMinuto;
                    var teste = new teclado
                    {
                        Serial = SerialAvell,
                        TTeclado = DadosFirebase1
                    };
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTEFALHA/" + SerialAvell, teste);
                    SerialAvell = string.Empty;
                    DadosFirebase1 = string.Empty;
                    break;
                }
                //Firebase - Atualizado
            }
            catch
            {
                lblFirebase.Text = "Firebase Off-Line";
                lblFirebase.ForeColor = Color.Red;
            }
        }
    }
}
