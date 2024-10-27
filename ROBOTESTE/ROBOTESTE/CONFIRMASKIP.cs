using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
using System.Windows.Forms;

namespace ROBOTESTE
{
    public partial class CONFIRMASKIP : Form
    {
        public string SKIPBURNIN;
        public string SKIPBURNIN2;

        public CONFIRMASKIP()
        {
            InitializeComponent();
            //Interacao();
            //InstalarCustom();
            StartFireBaseServices();
            TimeStart();
        }

        public void InstalarCustom()
        {
            try
            {
                //Instalar o Custom porque foi skipado antes da etapa!
                Process instalarCustom = System.Diagnostics.Process.Start(@"C:\TESTES_AVELL\.executaveisAux\InstalarCustomControl.exe");
                instalarCustom.WaitForExit();
            }
            catch (Exception ex) { MessageBox.Show("Não foi possivel instalaro o Custom Control, ou não foi possível encontrar o caminho"); }
        }

        public void LimparTemp()
        {
            //Metodo apaga pasta Temp1
            string caminhoPasta1 = @"C:\BurnInTest test files";
            if (Directory.Exists(caminhoPasta1))
            {
                try
                {
                    string apagarPasta1 = @"C:\BurnInTest test files";
                    Directory.Delete(apagarPasta1, true);
                }
                catch { MessageBox.Show("Não foi possível apagar a pasta:" + caminhoPasta1); }
            }

            ////Metodo apaga pasta Temp2
            //string caminhoPasta2 = @"C:\scratchdir";
            //if (Directory.Exists(caminhoPasta2))
            //{
            //    try
            //    {
            //        string apagarPasta2 = @"C:\scratchdir";
            //        Directory.Delete(apagarPasta2, true);
            //    }
            //    catch { MessageBox.Show("Não foi possível apagar a pasta:" + caminhoPasta2); }
            //}

            ////Metodo apaga pasta Temp3
            //string caminhoPasta3 = @"C:\ESD";
            //if (Directory.Exists(caminhoPasta3))
            //{
            //    try
            //    {
            //        string apagarPasta3 = @"C:\ESD";
            //        Directory.Delete(apagarPasta3, true);
            //    }
            //    catch { MessageBox.Show("Não foi possível apagar a pasta:" + caminhoPasta3); }
            //}
        }

        public void Interacao()
        {
            //https://www.naturalreaders.com/online/ - Cria vozes
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\BurninOK.mp3";
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
            if (SKIPBURNIN != "ATIVO")
            //Adicionado variável para evitar loop
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
                        SKIPBURNIN = "ATIVO";
                        //Esse precisa ficar aqui porque ele abre a contagem para liberar para o proximo teste.
                        LimparTemp();
                        GerarLogIndividual();
                        CriarLog_MySQLOK();
                        ChamarProximoTeste();
                    }
                };
                relogio.Start();
            }
        }

        public void GerarLogIndividual()
        {
            try
            {
                //Gera Log de Instalado para os Drivers
                var dataHoraMinutoOK = DateTime.Now.ToString("dd-MM-yyyy-HH:mm:s:s");
                const string Drivers = @"C:\TESTES_AVELL\logs_burnin\Burnin.log";
                var Arquivo = File.AppendText(Drivers);
                Arquivo.WriteLine("TESTE BURNIN SKIPADO: " + dataHoraMinutoOK);
                Arquivo.Close();
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
                string Furmark = "C:\\TESTES_AVELL\\MySqlData\\" + NomeArquivo + "\\Burnin_SKIPADO.txt";
                var Arquivo = File.AppendText(Furmark);
                Arquivo.WriteLine("BURNIN SKIPADO: " + dataHoraMinutoOK);
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
                    String DadosFirebase1 = "Burnin Skipado:" + dataHoraMinuto;
                    var teste = new burnin
                    {
                        Serial = SerialAvell,
                        TBurnin = DadosFirebase1
                    };
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTE/" + SerialAvell, teste);
                    SerialAvell = string.Empty;
                    DadosFirebase1 = string.Empty;
                    break;
                }

                foreach (ManagementObject getserial in MOS1.Get())
                {
                    string SerialAvell = getserial["SerialNumber"].ToString();
                    String DadosFirebase2 = "";
                    var teste = new teclado
                    {
                        Serial = SerialAvell,
                        TTeclado = DadosFirebase2
                    };
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTE/" + SerialAvell, teste);
                    SerialAvell = string.Empty;
                    DadosFirebase2 = string.Empty;
                    break;
                }

                foreach (ManagementObject getserial in MOS1.Get())
                {
                    string SerialAvell = getserial["SerialNumber"].ToString();
                    String DadosFirebase3 = "";
                    var teste = new Control
                    {
                        Serial = SerialAvell,
                        TControl = DadosFirebase3
                    };
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTE/" + SerialAvell, teste);
                    SerialAvell = string.Empty;
                    DadosFirebase3 = string.Empty;
                    break;
                }

                foreach (ManagementObject getserial in MOS1.Get())
                {
                    string SerialAvell = getserial["SerialNumber"].ToString();
                    String DadosFirebase4 = "";
                    var teste = new touchpad
                    {
                        Serial = SerialAvell,
                        TTouchpad = DadosFirebase4
                    };
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTE/" + SerialAvell, teste);
                    SerialAvell = string.Empty;
                    DadosFirebase4 = string.Empty;
                    break;
                }

                foreach (ManagementObject getserial in MOS1.Get())
                {
                    string SerialAvell = getserial["SerialNumber"].ToString();
                    String DadosFirebase5 = "";
                    var teste = new usb
                    {
                        Serial = SerialAvell,
                        TUsb = DadosFirebase5
                    };
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTE/" + SerialAvell, teste);
                    SerialAvell = string.Empty;
                    DadosFirebase5 = string.Empty;
                    break;
                }

                foreach (ManagementObject getserial in MOS1.Get())
                {
                    string SerialAvell = getserial["SerialNumber"].ToString();
                    String DadosFirebase6 = "";
                    var teste = new webcam
                    {
                        Serial = SerialAvell,
                        TWebcam = DadosFirebase6
                    };
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTE/" + SerialAvell, teste);
                    SerialAvell = string.Empty;
                    DadosFirebase6 = string.Empty;
                    break;
                }

                foreach (ManagementObject getserial in MOS1.Get())
                {
                    string SerialAvell = getserial["SerialNumber"].ToString();
                    String DadosFirebase7 = "";
                    var teste = new lcd
                    {
                        Serial = SerialAvell,
                        TLcd = DadosFirebase7
                    };
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTE/" + SerialAvell, teste);
                    SerialAvell = string.Empty;
                    DadosFirebase7 = string.Empty;
                    break;
                }

                foreach (ManagementObject getserial in MOS1.Get())
                {
                    string SerialAvell = getserial["SerialNumber"].ToString();
                    String DadosFirebase8 = "";
                    var teste = new audio
                    {
                        Serial = SerialAvell,
                        TAudio = DadosFirebase8
                    };
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTE/" + SerialAvell, teste);
                    SerialAvell = string.Empty;
                    DadosFirebase8 = string.Empty;
                    break;
                }

                foreach (ManagementObject getserial in MOS1.Get())
                {
                    string SerialAvell = getserial["SerialNumber"].ToString();
                    String DadosFirebase9 = "";
                    var teste = new wifi
                    {
                        Serial = SerialAvell,
                        TWifi = DadosFirebase9
                    };
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTE/" + SerialAvell, teste);
                    SerialAvell = string.Empty;
                    DadosFirebase9 = string.Empty;
                    break;
                }

                foreach (ManagementObject getserial in MOS1.Get())
                {
                    string SerialAvell = getserial["SerialNumber"].ToString();
                    String DadosFirebase10 = "";
                    var teste = new bluetooth
                    {
                        Serial = SerialAvell,
                        TBluetooth = DadosFirebase10
                    };
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTE/" + SerialAvell, teste);
                    SerialAvell = string.Empty;
                    DadosFirebase10 = string.Empty;
                    break;
                }

                foreach (ManagementObject getserial in MOS1.Get())
                {
                    string SerialAvell = getserial["SerialNumber"].ToString();
                    String DadosFirebase11 = "";
                    var teste = new hdmi
                    {
                        Serial = SerialAvell,
                        THdmi = DadosFirebase11
                    };
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTE/" + SerialAvell, teste);
                    SerialAvell = string.Empty;
                    DadosFirebase11 = string.Empty;
                    break;
                }

                foreach (ManagementObject getserial in MOS1.Get())
                {
                    string SerialAvell = getserial["SerialNumber"].ToString();
                    String DadosFirebase12 = "";
                    var teste = new sistema
                    {
                        Serial = SerialAvell,
                        TSistema = DadosFirebase12
                    };
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTE/" + SerialAvell, teste);
                    SerialAvell = string.Empty;
                    DadosFirebase12 = string.Empty;
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

        public void ChamarProximoTeste()
        {
            if (SKIPBURNIN2 != "OK")
            {
                SKIPBURNIN2 = "OK";
                CONFIRMAOK formConfirmaOK = new CONFIRMAOK();
                this.Hide();
                formConfirmaOK.ShowDialog();
            }
        }
    }
}
