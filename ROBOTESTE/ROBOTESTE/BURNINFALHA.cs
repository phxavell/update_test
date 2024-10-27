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
    public partial class BURNINFALHA : MaterialSkin.Controls.MaterialForm
    {
        public BURNINFALHA()
        {
            InitializeComponent();
            StartFireBaseServices();
            Interacao();
            LimparTemp();
            CriarLogFalha();
            InsertAvellWeb();
        }

        public void Interacao()
        {
            //https://www.naturalreaders.com/online/ - Cria vozes
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\BurninFalha.mp3";
            wplayer.controls.play();
        }

        public void LimparTemp()
        {
            //Metodo apaga pasta Temp1
            string caminhoPasta1 = @"C:\BurnInTest test files";
            if (Directory.Exists(caminhoPasta1))
            {
                try
                {
                    //MessageBox.Show("Limpez Temp:" + caminhoPasta1, "Limpeza Temp.");
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

        public void CriarLogFalha()
        {
            try
            {
                var dataHoraMinuto = DateTime.Now.ToString("dd-MM-yyyy-HH-mms-s");
                System.IO.StreamWriter sw2 = new StreamWriter(@"C:\TESTES_AVELL\logs_burnin\Falha" + dataHoraMinuto + ".log");
                sw2.WriteLine("Falha em Burnin Data/Hora:" + dataHoraMinuto);
                sw2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
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
                string Furmark = "C:\\TESTES_AVELL\\MySqlData\\" + NomeArquivo + "\\BURNIN_FALHA.txt";
                var Arquivo = File.AppendText(Furmark);
                Arquivo.WriteLine("BURNIN FALHA: " + dataHoraMinutoOK);
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
                    String DadosFirebase1 = "Burnin Falha:" + dataHoraMinuto;
                    var teste = new burnin
                    {
                        Serial = SerialAvell,
                        TBurnin = DadosFirebase1
                    };
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTEFALHA/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTEFALHA/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTEFALHA/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTEFALHA/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTEFALHA/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTEFALHA/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTEFALHA/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTEFALHA/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTEFALHA/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTEFALHA/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTEFALHA/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTEFALHA/" + SerialAvell, teste);
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

        private void btnReteste_Click(object sender, EventArgs e)
        {
            ROBOTESTE FormRoboTeste = new ROBOTESTE();
            this.Hide();
            FormRoboTeste.Show();
        }
    }
}
