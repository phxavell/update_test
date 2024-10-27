using System;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;
using System.Management;
using System.Windows.Forms;
using System.Drawing;
using FireSharp.Response;
using FireSharp.Config;
using FireSharp.Interfaces;

namespace CONTROL_COOLER
{
    public partial class BONLITE : MaterialSkin.Controls.MaterialForm
    {
        public string BONOK;
        public string INSTALARCCONTROL;

        public BONLITE()
        {
            InitializeComponent();
            InteracaoCooler();
            StartFireBaseServices();
            VerificarInstalacao();
            Color1();


            //ValidaOK();
        }

        public void InteracaoCooler()
        {
            //https://www.naturalreaders.com/online/ - Cria vozes
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\ControlCoolers.mp3";
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

        public void Color1()
        {
            try
            {
                Timer relogio = new Timer();
                relogio.Interval = 1000;
                int tempo = 2;

                relogio.Tick += delegate {
                    tempo -= 1;
                    //lblTime.Text = tempo.ToString();
                    if (tempo == 0)
                    {
                        relogio.Stop();

                        lblInfomacao0.ForeColor = Color.Red;
                        lblInfomacao1.ForeColor = Color.Red;
                        lblInfomacao2.ForeColor = Color.Red;
                        Color2();
                    }
                };
                relogio.Start();
            }
            catch (Exception ex) { }
        }

        public void Color2()
        {
            try
            {
                Timer relogio = new Timer();
                relogio.Interval = 1000;
                int tempo = 2;

                relogio.Tick += delegate {
                    tempo -= 1;
                    //lblTime.Text = tempo.ToString();
                    if (tempo == 0)
                    {
                        relogio.Stop();

                        lblInfomacao0.ForeColor = Color.Orange;
                        lblInfomacao1.ForeColor = Color.Orange;
                        lblInfomacao2.ForeColor = Color.Orange;
                        Color1();
                    }
                };
                relogio.Start();
            }
            catch (Exception ex) { }
        }


        public void VerificarInstalacao()
        {
            //Trocar pelo caminho da Clevo
            String CaminhoPastaControl = @"C:\Program Files (x86)\ControlCenter\AppInstall";

            if (Directory.Exists(CaminhoPastaControl))
            {
                //Se já tiver instalado, vai inserir e fechar
                InsertAvellWeb();
                ValidaOK();
            }
            else
            {
                if (INSTALARCCONTROL != "OK")
                {
                    InstalarCControl_CCenter();
                }
            }
        }

        public void InstalarCControl_CCenter()
        {
            try
            {
                Process instalarCustom = System.Diagnostics.Process.Start(@"C:\TESTES_AVELL\.executaveisAux\InstalarCustomControl.exe");
                instalarCustom.WaitForExit();
                INSTALARCCONTROL = "OK";

                VerificarInstalacao();
            }
            catch (Exception ex)
            {
                MessageBox.Show("NÃO FOI POSSIVEL INSTALAR O CUSTOM CONTROL", "CAMINHO DO INSTALADOR OFF!");
            }
        }

        public void ValidaOK()
        {
            if (BONOK != "OK")
            {
                try
                {
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 10;

                    relogio.Tick += delegate {
                        tempo -= 1;
                        lblTime.Text = tempo.ToString();
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            BONOK = "OK";
                            this.Close();
                        }
                    };
                    relogio.Start();
                }
                catch (Exception ex) { }
            }
        }

        public void InsertAvellWeb()
        {
            ManagementObjectSearcher mSearcher = new ManagementObjectSearcher("SELECT SerialNumber, ReleaseDate FROM Win32_BIOS");
            ManagementObjectCollection collection = mSearcher.Get();
            foreach (ManagementObject obj in collection)
            {
                string NomeArquivo = (string)obj["SerialNumber"];

                //Gera Log de OK para o MySql
                var dataHoraMinutoOK = DateTime.Now.ToString("dd-MM-yyyy-HH:mm:s:s");
                string CustomControlInstalado = "C:\\TESTES_AVELL\\MySqlData\\" + NomeArquivo + "\\ControlCenterInstalado.txt";
                var Arquivo = File.AppendText(CustomControlInstalado);
                Arquivo.WriteLine("CUSTOM CONTROL INSTALADO: " + dataHoraMinutoOK);
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
                    String DadosFirebase1 = "CCenter Instalado:" + dataHoraMinuto;
                    var teste = new install
                    {
                        Serial = SerialAvell,
                        TControl = DadosFirebase1
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
