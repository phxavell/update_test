﻿using System;
using System.Drawing;
using System.Management;
using System.Windows.Forms;
using MaterialSkin;
using System.Reflection.Emit;
using System.IO;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace HDMI
{
    public partial class ENVIAREPARO : MaterialSkin.Controls.MaterialForm
    {
        public ENVIAREPARO()
        {
            InitializeComponent();
            StartFireBaseServices();//Base de Dados On-Line - Ativar ou Desativar Aqui!
            TimeStart1();
            Interacao();
            InsertAvellWeb();
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
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\EnvieParaReparo.mp3";
            wplayer.controls.play();
        }

        public void TimeStart1()
        {
            Timer relogio = new Timer();
            relogio.Interval = 1000;
            int tempo = 1;//1 minuto e 20

            relogio.Tick += delegate {
                tempo -= 1;
                //lblTime.Text = tempo.ToString();
                if (tempo == 0)
                {
                    relogio.Stop();
                    //O que vai parar
                    panelFalha.BackColor = Color.DarkRed;
                    panelFalha.Refresh();
                    TimeStart2();
                }
            };
            relogio.Start();
        }

        public void TimeStart2()
        {
            Timer relogio = new Timer();
            relogio.Interval = 1000;
            int tempo = 1;//1 minuto e 20

            relogio.Tick += delegate {
                tempo -= 1;
                //lblTime.Text = tempo.ToString();
                if (tempo == 0)
                {
                    relogio.Stop();
                    //O que vai parar
                    panelFalha.BackColor = Color.DarkOrange;
                    panelFalha.Refresh();
                    TimeStart1();

                }
            };
            relogio.Start();
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
                string Furmark = "C:\\TESTES_AVELL\\MySqlData\\" + NomeArquivo + "\\HDMI_FALHA.txt";
                var Arquivo = File.AppendText(Furmark);
                Arquivo.WriteLine("HDMI FALHA - ENVIO REPARO!: " + dataHoraMinutoOK);
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
                    String DadosFirebase1 = "HDMI FALHA!:" + dataHoraMinuto;
                    var teste = new hdmi1
                    {
                        Serial = SerialAvell,
                        THdmi = DadosFirebase1
                    };
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTE/" + SerialAvell, teste);
                    SerialAvell = string.Empty;
                    DadosFirebase1 = string.Empty;
                    break;
                }

                foreach (ManagementObject getserial in MOS1.Get())
                {
                    string SerialAvell = getserial["SerialNumber"].ToString();
                    String DadosFirebase1 = "HDMI FALHA!:" + dataHoraMinuto;
                    var teste = new hdmi1
                    {
                        Serial = SerialAvell,
                        THdmi = DadosFirebase1
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
