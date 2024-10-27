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
    public partial class CONFIRMAOK : MaterialSkin.Controls.MaterialForm
    {
        public string BURNINOK;
        public string BURNINOK2;

        public CONFIRMAOK()
        {
            InitializeComponent();
            PararTestes();
            Interacao();
            StartFireBaseServices();
            TimeStart();
        }

        public void PararTestes()
        {
            //Finaliza o processo após a confirmação
            try
            {
                string Burnin = "bit";
                var processes = Process.GetProcessesByName(Burnin);
                foreach (var BurninTeste in processes)
                BurninTeste.Kill();
                //Esse precisa ficar aqui porque ele abre a contagem para liberar para o proximo teste.
                LimparTemp();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possivel finalizar o processo");
            }
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
            if (BURNINOK != "Active")
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
                        BURNINOK = "Active";
                        GerarLogIndividual();
                        InsertAvellWeb();
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
                Arquivo.WriteLine("TESTE BURNIN REALIZADO: " + dataHoraMinutoOK);
                Arquivo.Close();
            }
            catch (Exception ex) { }
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
                string Furmark = "C:\\TESTES_AVELL\\MySqlData\\" + NomeArquivo + "\\Burnin_OK.txt";
                var Arquivo = File.AppendText(Furmark);
                Arquivo.WriteLine("BURNIN OK: " + dataHoraMinutoOK);
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
                    String DadosFirebase1 = "Burnin OK:" + dataHoraMinuto;
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
            if (BURNINOK2 != "OK")
            {
                BURNINOK2 = "OK";
                TECLADO.TESTETECLADO formAuditorStart = new TECLADO.TESTETECLADO();
                this.Hide();
                formAuditorStart.ShowDialog();
            }
        }
    }
}
