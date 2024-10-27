using System;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;
using MaterialSkin;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Drawing;
using System.Management;
//Referencia
//http://www.aspdotnet-pools.com/2020/03/detect-pen-driveflash-drive-in-cnet.html

namespace USB
{
    public partial class USB : MaterialSkin.Controls.MaterialForm
    {
        public string Drive;
        public string EncontradoOK;
        public string Interacao1;

        public string Timer;

        public USB()
        {
            InitializeComponent();
            StartFireBaseServices();
            Verificador();
            InsertAvellWeb();
            //Interacao();
            TimeStart2();
            Drive = "false";
            ListarArquivos1();
        }

        public void Verificador()
        {
            var quantidadeLog = Directory.GetFiles(@"C:\TESTES_AVELL\logs_usb", "*.log", SearchOption.TopDirectoryOnly).Count().ToString();
            int valor = int.Parse(quantidadeLog);
            if (valor < 1)
            //if (valor < 2)
            {
                Interacao();
            }
        }

        public void Interacao()
        {
            string log = @"C:\TESTES_AVELL\logs_usb\Interacao1.txt";
            if (File.Exists(log))
            {
                WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
                wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\USBOutraUnidade.mp3";
                wplayer.controls.play();
            }
            else
            {
                WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
                wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\USBTeste.mp3";
                wplayer.controls.play();
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

        public void TimeStart1()
        {
            Timer relogio = new Timer();
            relogio.Interval = 1000;
            int tempo = 5;
            relogio.Tick += delegate
            {
                tempo -= 1;
                if (tempo == 0)
                {
                    relogio.Stop();

                    //Verifica se tem mais de um log de erros
                    var quantidadeLog = Directory.GetFiles(@"C:\TESTES_AVELL\logs_usb", "*.log", SearchOption.TopDirectoryOnly).Count().ToString();
                    int valor = int.Parse(quantidadeLog);
                    if (valor >= 2)
                    {
                        ENVIAREPARO formEnviaReparo = new ENVIAREPARO();
                        this.Hide();
                        formEnviaReparo.ShowDialog();
                    }
                    else
                    {
                        if (Drive != "true")
                        {
                            relogio.Stop();
                            //DetectarMidia();
                            TimeStart2();
                        }
                    }
                }
            };
            relogio.Start();
        }

        public void TimeStart2()//Loop
        {
            Timer relogio = new Timer();
            relogio.Interval = 1000;
            int tempo = 10;
            relogio.Tick += delegate
            {
                tempo -= 1;
                lblTime2.Text = tempo.ToString();
                if (tempo == 0)
                {
                    relogio.Stop();
                    try
                    {
                        LimparLabel();
                        DetectarMidia();
                        //TimeStart1();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Não foi possivel capturar Imagem" + ex);
                    }
                }
            };
            relogio.Start();
        }

        public void DetectarMidia()
        {
            try
            {
                var drives = DriveInfo.GetDrives().Where(d => d.IsReady & d.DriveType == DriveType.Removable);
                if (drives.FirstOrDefault() != null)
                {
                    Drive = "true";//Variável publica
                    Interacao1 = "true";
                    EncontradoOK = "OK";

                    lblName.Text = drives.FirstOrDefault().Name.ToString();
                    lblFormat.Text = drives.FirstOrDefault().DriveFormat.ToString();
                    lblType.Text = drives.FirstOrDefault().DriveType.ToString();
                    lblReady.Text = drives.FirstOrDefault().IsReady.ToString();
                    lblFreeSpace.Text = drives.FirstOrDefault().TotalFreeSpace.ToString();
                    lblTotalSpace.Text = drives.FirstOrDefault().TotalSize.ToString();
                    UnidadeDetectada();
                }
                else
                {
                    EncontradoOK = "Falha";
                    UnidadeNaoDetectada();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não detectado" + ex);
            }
        }

        public void UnidadeDetectada()
        {
            if (Drive == "true")
            {
                UNIDADEOK formUnidadeOK = new UNIDADEOK();
                this.Hide();
                formUnidadeOK.ShowDialog();
            }
        }

        public void LimparLabel()
        {
            lblFormat.Text = "";
            lblType.Text = "";
            lblFreeSpace.Text = "";
            lblReady.Text = "";
            lblName.Text = "";
            lblTotalSpace.Text = "";
        }

        public void ListarArquivos1()
        {
            try
            {
                DirectoryInfo Dir = new DirectoryInfo(@"C:\TESTES_AVELL\usb_origem");
                FileInfo[] Files = Dir.GetFiles("*", SearchOption.AllDirectories);
                foreach (FileInfo File in Files)
                {
                    //listbArquivos1.Items.Add(File.FullName); - Mostra caminho completo
                    listbArquivos1.Items.Add(File);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Localizar pastas de arquivos" + ex);
            }
        }

        public void UnidadeNaoDetectada()
        {
            if (EncontradoOK == "Falha")
            {
                TimeFalha();
            }
        }

        public void TimeFalha()//Loop
        {
            Timer relogio = new Timer();
            relogio.Interval = 1000;
            int tempo = 1;
            relogio.Tick += delegate
            {
                tempo -= 1;
                lblTime2.Text = tempo.ToString();
                if (tempo == 0)
                {
                    relogio.Stop();
                    using (REPROVAFALHA formReprovaFalha = new REPROVAFALHA())
                    {
                        this.Hide();
                        formReprovaFalha.ShowDialog();
                    }

                }
            };
            relogio.Start();
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
                    String DadosFirebase1 = "Usb Início:" + dataHoraMinuto;
                    var teste = new usb1
                    {
                        Serial = SerialAvell,
                        TUsb = DadosFirebase1
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