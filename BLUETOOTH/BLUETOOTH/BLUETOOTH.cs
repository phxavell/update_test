using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System.Management;
using System.IO;
using FireSharp.Response;

namespace BLUETOOTH
{
    public partial class BLUETOOTH : MaterialSkin.Controls.MaterialForm
    {
        public string BLUETOOTHOK;
        public string BLUETOOTHFALHA;
        public int QUANTIDADE;
 
        public BLUETOOTH()
        {
            InitializeComponent();
            StartFireBaseServices();
            Interacao();
            ChecarBluetooth();
            TimeStart();
        }

        public void Interacao()
        {
            //Interacao - Precisa ficar dentro do evento do form devido auto start das propriedades de video.
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\Bluetooth.mp3";
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

        public void ChecarBluetooth()
        {
            lbDriversBluetooth.Items.Clear();

            //Busca os drivers instalados na máquina - Utiliza propriedades do Win32.dll
            string query = "SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%Bluetooth%'";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection collection = searcher.Get();

            foreach (ManagementObject device in collection)
            {
                lbDriversBluetooth.Items.Add(device["Name"]);
                var quantidade = collection.Count;
                //Variável public do form
                QUANTIDADE = quantidade;
                //break;
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
                        //ChecarBluetooth();
                        InsertAvellWeb();

                        DriversInstall();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Não foi possivel capturar Imagem" + ex);
                    }
                }
            };
            relogio.Start();
        }

        public void DriversInstall()
        {
            if (QUANTIDADE >= 4)
            {
                ChamarValidaOk();
            }
            else
            {
                ChamarReprovaFalha();
            }

        }

        private void ChamarValidaOk()
        {
            //Usar esta estrutura em todos para não ficar em loop, ajustar o código em todos os forms
            if (BLUETOOTHOK != "OK" && BLUETOOTHFALHA != "FALHA")
            {
                BLUETOOTHOK = "OK";
                VALIDAOK formAprovaTeste = new VALIDAOK();
                this.Hide();
                formAprovaTeste.ShowDialog();
            }
        }

        private void ChamarReprovaFalha()
        {
            //Usar esta estrutura em todos para não ficar em loop, ajustar o código em todos os forms
            //Padronizar a verificação de quantidadede falhas no ReprovaFalhas em todos os testes.
            if (BLUETOOTHOK != "OK" && BLUETOOTHFALHA != "FALHA")
            {
                BLUETOOTHOK = "FALHA";
                REPROVAFALHA formReprovaFalha = new REPROVAFALHA();
                this.Hide();
                formReprovaFalha.ShowDialog();
            }
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
                    String DadosFirebase1 = "Bluetooth Início:" + dataHoraMinuto;
                    var teste = new bluetooth1
                    {
                        Serial = SerialAvell,
                        TBluetooth = DadosFirebase1
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

        private void BLUETOOTH_Load(object sender, EventArgs e)
        {

        }
    }
}
