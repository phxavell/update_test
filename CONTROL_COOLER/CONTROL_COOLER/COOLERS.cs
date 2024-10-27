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
    public partial class COOLERS : MaterialSkin.Controls.MaterialForm
    {
        public string COOLERS1;
        public string BONLITE1;

        public COOLERS()
        {
            InitializeComponent();
            VerificarModelo();
            WinVer();
            StartFireBaseServices();
            InteracaoCooler();
        }

        public void InteracaoCooler()
        {
            //https://www.naturalreaders.com/online/ - Cria vozes
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\ControlCoolers.mp3";
            wplayer.controls.play();
        }

        public void WinVer()
        {
            // Consulta para obter informações sobre o sistema operacional
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectCollection information = searcher.Get();

            foreach (ManagementObject obj in information)
            {
                string edition = obj["Caption"].ToString();
                lblWinVer.Text = ($"Edição do Windows: {edition}");
                break;
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

        public void VerificarModelo()
        {
            SelectQuery tecladoAvell = new SelectQuery("Win32_BIOS");
            ManagementObjectSearcher objOSDetails = new ManagementObjectSearcher(tecladoAvell);
            ManagementObjectCollection osDetailsCollection = objOSDetails.Get();
            foreach (ManagementObject filtroPorBios in osDetailsCollection)
            {
                string selecaoVersao = (filtroPorBios["SMBIOSBIOSVersion"].ToString());
                if (selecaoVersao == "1.07.08TBN1")
                {
                    ChamarBON();
                }
                if (selecaoVersao == "1.07.06TBN")
                {
                    ChamarBON();
                }
                if (selecaoVersao == "1.07.07TBN")
                {
                    ChamarBON();
                }
                if (selecaoVersao == "1.07.08TBN")
                {
                    ChamarBON();
                }
                if (selecaoVersao == "1.07.09TBN")
                {
                    ChamarBON();
                }
                if (selecaoVersao == "1.07.10TBN")
                {
                    ChamarBON();
                }
                //B.ON SMART
                if (selecaoVersao == "1.07.02TBN1")
                {
                    ChamarBON();
                }

                //B.ON 145
                if (selecaoVersao == "BM_BI_IDL819-10_150A_N")
                {
                    ChamarBON();
                }
                //B.ON 147
                if (selecaoVersao == "BM_BI_IDL819-10_150B_N")
                {
                    ChamarBON();
                }
                //Inserido Home Office
                if (selecaoVersao != "1.07.08TBN1" && selecaoVersao !="1.07.06TBN" && selecaoVersao != "1.07.02TBN1" && selecaoVersao != "1.07.07TBN" && selecaoVersao != "1.07.08TBN" && selecaoVersao != "1.07.09TBN" && selecaoVersao != "1.07.10TBN")
                {
                    StartTestes();
                }
                break;

            }
        }

        public void StartTestes()
        {
            try
            {
                Timer relogio = new Timer();
                relogio.Interval = 1000;
                int tempo = 40;

                relogio.Tick += delegate {
                    tempo -= 1;
                    lblTime.Text = tempo.ToString();
                    if (tempo == 0)
                    {
                        relogio.Stop();

                        Cooler2();
                        //VerificarInstalacao();
                    }
                };
                relogio.Start();
            }
            catch (Exception ex) { }
        }

        public void Cooler2()
        {//*********ADICIONADO PARA TESTES*******************************
            if (COOLERS1 != "OK")
            {
                //Adicionar Time para não gerar falha
                Timer relogio = new Timer();
                relogio.Interval = 1000;
                int tempo = 5;

                relogio.Tick += delegate {
                    tempo -= 1;
                    //lblTime.Text = tempo.ToString();
                    if (tempo == 0)
                    {
                        relogio.Stop();
                        InsertAvellWeb();
                        //Chamar form Coolers2
                        COOLERS1 = "OK";
                        COOLERS2 FormTipoFalha = new COOLERS2();
                        this.Hide();
                        FormTipoFalha.ShowDialog();
                    }
                };
                relogio.Start(); 
            }
        }

        public void ChamarBON()
        {
            if (BONLITE1 != "OK")
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
                            BONLITE1 = "OK";
                            //Chamar B.ON_Lite
                            BONLITE formBONLite = new BONLITE();
                            this.Hide();
                            formBONLite.ShowDialog();

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
                string CustomControlInstalado = "C:\\TESTES_AVELL\\MySqlData\\" + NomeArquivo + "\\CustomControlInstalado.txt";
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
                    String DadosFirebase1 = "Control Instalado:" + dataHoraMinuto;
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
