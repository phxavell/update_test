using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MaterialSkin;
using System.Management;
using System.Reflection.Emit;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Drawing;

namespace USB
{
    public partial class QUANTIDADEARQUIVOS : MaterialSkin.Controls.MaterialForm
    {
        public string USB1;

        public QUANTIDADEARQUIVOS()
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
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\USBTotalOk.mp3";
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
            if (USB1 != "OK")
            {
                Timer relogio = new Timer();
                relogio.Interval = 1000;
                int tempo = 3;

                relogio.Tick += delegate
                {
                    tempo -= 1;
                    lblTime.Text = tempo.ToString();
                    if (tempo == 0)
                    {
                        //Para e chama o método
                        relogio.Stop();

                        USB1 = "OK";

                        //Chama o Form OK para avançar
                        VALIDAOK formValidaOk = new VALIDAOK();
                        this.Hide();
                        formValidaOk.Show();
                    }
                };
                relogio.Start();
            }
        }

        public void ApagarArquivosTemp()
        {
            try
            {
                //Exclui pasta da mídia removível
                var drives = DriveInfo.GetDrives().Where(d => d.IsReady & d.DriveType == DriveType.Removable);
                if (drives.FirstOrDefault() != null)
                {
                    string unidade = drives.FirstOrDefault().Name.ToString();
                    string destino = unidade + "\\TESTES_AVELL";
                    var Exluir = new DirectoryInfo(destino);
                    Exluir.Delete(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível excluir os arquivos" + ex);
            }
        }

        public void formPrincipal()
        {
            USB formUSB = new USB();
            this.Hide();
            formUSB.Show();
        }

        public void InsertAvellWeb()
        {
            try
            {
                //O valida OK já insere esta informação, mas deixei aqui também.
                var dataHoraMinuto = DateTime.Now.ToString("dd/MM/yyyy - HH:mm");
                ManagementObjectSearcher MOS1 = new ManagementObjectSearcher("Select * From Win32_BIOS");
                foreach (ManagementObject getserial in MOS1.Get())
                {
                    string SerialAvell = getserial["SerialNumber"].ToString();
                    String DadosFirebase1 = "Usb OK!:" + dataHoraMinuto;
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
