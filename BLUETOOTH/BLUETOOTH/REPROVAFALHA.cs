﻿using System;
using System.Windows.Forms;
using MaterialSkin;
using System.Management;
using System.Reflection.Emit;
using System.IO;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Linq;
using System.Drawing;

namespace BLUETOOTH
{
    public partial class REPROVAFALHA : MaterialSkin.Controls.MaterialForm
    {
        public REPROVAFALHA()
        {
            InitializeComponent();
            StartFireBaseServices();
            Verificador();
        }

        public void Interacao()
        {
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\BluetoothFalha.mp3";
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

        private void btnReteste_Click(object sender, EventArgs e)
        {
            InsertAvellWeb();
            //Chamar novamente os testes
            BLUETOOTH formBluetooth = new BLUETOOTH();
            this.Hide();
            formBluetooth.Show();
        }

        public void Verificador()
        {
            var quantidadeLog = Directory.GetFiles(@"C:\TESTES_AVELL\logs_bluetooth", "*.log", SearchOption.TopDirectoryOnly).Count().ToString();
            int valor = int.Parse(quantidadeLog);
            if (valor < 2)
            {
                Interacao();
                CriarLogFalha();
            }
            else if (valor == 2)
            {
                //Chamar form, método mais eficiente.
                using (ENVIAREPARO formEnviarReparo = new ENVIAREPARO())
                {
                    this.Hide();
                    formEnviarReparo.ShowDialog();
                }
            }
        }

        public void CriarLogFalha()
        {
            try
            {
                var dataHoraMinuto = DateTime.Now.ToString("dd-MM-yyyy-HH-mms-s");
                //Criar log de voz
                System.IO.StreamWriter sw2 = new StreamWriter(@"C:\TESTES_AVELL\logs_bluetooth\Falha" + dataHoraMinuto + ".log");
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
                string Furmark = "C:\\TESTES_AVELL\\MySqlData\\" + NomeArquivo + "\\BLUETOOTH_FALHA.txt";
                var Arquivo = File.AppendText(Furmark);
                Arquivo.WriteLine("BLUETOOTH FALHA!: " + dataHoraMinutoOK);
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
                    String DadosFirebase1 = "BLUETOOTH FALHA!:" + dataHoraMinuto;
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

                foreach (ManagementObject getserial in MOS1.Get())
                {
                    string SerialAvell = getserial["SerialNumber"].ToString();
                    String DadosFirebase1 = "BLUETOOTH FALHA!:" + dataHoraMinuto;
                    var teste = new bluetooth1
                    {
                        Serial = SerialAvell,
                        TBluetooth = DadosFirebase1
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
