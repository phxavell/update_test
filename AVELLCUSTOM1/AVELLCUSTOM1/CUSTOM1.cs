using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using MaterialSkin;
using FireSharp.Config;
using FireSharp.Interfaces;
using System.Management;
using FireSharp.Response;
using System.Drawing;

namespace AVELLCUSTOM1
{
    public partial class CUSTOM1 : MaterialSkin.Controls.MaterialForm
    {
        public string CUSTOMOK;
        public string CONFIRMACUSTOMOK;
        public string BON_LITE;
        public string TIMECUSTOM;
        public string TASKKILLCUSTOM;

        //FLAG PARA EVITAR LOOP DA CHAMADO DO PROGRAMA
        public string CHAMARCUSTOM;

        public CUSTOM1()
        {
            InitializeComponent();
            StartFireBaseServices();
            
            ChamarProcesso();//Chamar probramaCustomControl

            VerificaArquivoSistema();
            Interacao();
            //Precisa ter um Delay nesta etapa
            TimeStart();
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
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\AvellCustm.mp3";
            wplayer.controls.play();
        }

        public void VerificaArquivoSistema()
        {
            string sistemaTxt = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
            bool caminho = File.Exists(sistemaTxt);
            if (caminho == false)
            {
                try
                {
                    //GerarLogsTeste
                    ProcessStartInfo GerarLogSistema = new ProcessStartInfo("cmd.exe", @"/C " + "systeminfo > C:\\TESTES_AVELL\\systemInfo\\sistema.txt");
                    GerarLogSistema.UseShellExecute = false;
                    GerarLogSistema.CreateNoWindow = true;
                    Process.Start(GerarLogSistema);
                }
                catch (Exception ex) { }
            }
        }

        private void ChamarProcesso()
        {
            if (CHAMARCUSTOM != "OK")
            {
                ProcModelosNosLogs();
            }
        }

        public void ProcModelosNosLogs()
        {
            try
            {
                //PRECISA POSTERIORMENTE NA VERSÃO 2 DEIXAR DE FORMA DINÂMICA - PERSISTIR DADOS
                //Ajustar para o usuários que estiver localdo
                string modeloA52 = "A52";
                string filePathA52 = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerA52 = new StreamReader(filePathA52);

                while (!readerA52.EndOfStream)
                {
                    string lineA52 = readerA52.ReadLine();
                    if (lineA52.Contains(modeloA52))
                    {
                        CHAMARCUSTOM = "OK";
                        System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                        pProcess.StartInfo.FileName = @"C:\Users\Public\Desktop\Avell Custom.lnk";
                        pProcess.Start();
                        break;
                    }
                }
                readerA52.Close();

                //-----------------------------------------------------------------
                string modeloA55 = "A55";
                string filePathA55 = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerA55 = new StreamReader(filePathA55);

                while (!readerA55.EndOfStream)
                {
                    string lineA55 = readerA55.ReadLine();
                    if (lineA55.Contains(modeloA55))
                    {
                        CHAMARCUSTOM = "OK";
                        System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                        pProcess.StartInfo.FileName = @"C:\Users\Public\Desktop\Avell Custom.lnk";
                        pProcess.Start();
                        break;
                    }
                }
                readerA55.Close();

                //-----------------------------------------------------------------
                string modeloA57 = "A57";
                string filePathA57 = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerA57 = new StreamReader(filePathA57);

                while (!readerA57.EndOfStream)
                {
                    string lineA57 = readerA57.ReadLine();
                    if (lineA57.Contains(modeloA57))
                    {
                        CHAMARCUSTOM = "OK";
                        System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                        pProcess.StartInfo.FileName = @"C:\Users\Public\Desktop\Avell Custom.lnk";
                        pProcess.Start();
                        break;
                    }
                }
                readerA57.Close();

                //-----------------------------------------------------------------
                string modeloA70 = "A70";
                string filePathA70 = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerA70 = new StreamReader(filePathA70);

                while (!readerA70.EndOfStream)
                {
                    string lineA70 = readerA70.ReadLine();
                    if (lineA70.Contains(modeloA70))
                    {
                        CHAMARCUSTOM = "OK";
                        System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                        pProcess.StartInfo.FileName = @"C:\Users\Public\Desktop\Avell Custom.lnk";
                        pProcess.Start();
                        break;
                    }
                }
                readerA70.Close();

                //-----------------------------------------------------------------
                string modeloA72 = "A72";
                string filePathA72 = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerA72 = new StreamReader(filePathA72);

                while (!readerA72.EndOfStream)
                {
                    string lineA72 = readerA72.ReadLine();
                    if (lineA72.Contains(modeloA72))
                    {
                        CHAMARCUSTOM = "OK";
                        System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                        pProcess.StartInfo.FileName = @"C:\Users\Public\Desktop\Avell Custom.lnk";
                        pProcess.Start();
                        break;
                    }
                }
                readerA72.Close();

                //-----------------------------------------------------------------

                string modeloC65 = "C65";
                string filePathC65 = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerC65 = new StreamReader(filePathC65);

                while (!readerC65.EndOfStream)
                {
                    string lineC65 = readerC65.ReadLine();
                    if (lineC65.Contains(modeloC65))
                    {
                        CHAMARCUSTOM = "OK";
                        System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                        pProcess.StartInfo.FileName = @"C:\Users\Public\Desktop\Avell Custom Control.lnk";
                        pProcess.Start();
                        break;
                    }
                }
                readerC65.Close();

                //-----------------------------------------------------------------
                string modeloBON = "B.ON";
                string filePathBON = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerBON = new StreamReader(filePathBON);

                while (!readerBON.EndOfStream)
                {
                    string lineBON = readerBON.ReadLine();
                    if (lineBON.Contains(modeloBON))
                    {
                        CHAMARCUSTOM = "OK";
                        ChamarB_ON();
                        break;
                    }
                }
                readerBON.Close();

                //-----------------------------------------------------------------
                string modeloB11 = "B11";
                string filePathB11 = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerB11 = new StreamReader(filePathB11);

                while (!readerB11.EndOfStream)
                {
                    string lineB11 = readerB11.ReadLine();
                    if (lineB11.Contains(modeloB11))
                    {
                        CHAMARCUSTOM = "OK";
                        System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                        pProcess.StartInfo.FileName = @"C:\Users\Public\Desktop\Avell Custom.lnk";
                        pProcess.Start();
                        break;
                    }
                }
                readerB11.Close();
                //-----------------------------------------------------------------
                //Para o Custom, não foi preciso adicionar em separado "Storm BS", só com a palavra chave
                //"Storm" foi possível detectar a configuração do CUSTOM.
                string modeloStorm = "STORM";
                string filePathStorm = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerStorm = new StreamReader(filePathStorm);

                while (!readerStorm.EndOfStream)
                {
                    string lineStorm = readerStorm.ReadLine();
                    if (lineStorm.Contains(modeloStorm))
                    {
                        CHAMARCUSTOM = "OK";
                        System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                        pProcess.StartInfo.FileName = @"C:\Users\Public\Desktop\Avell Custom.lnk";
                        pProcess.Start();
                        break;
                    }
                }
                readerStorm.Close();
                //-----------------------------------------------------------------

                string modeloA65 = "A65";
                string filePathA65 = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerA65 = new StreamReader(filePathA65);

                while (!readerA65.EndOfStream)
                {
                    string lineA65 = readerA65.ReadLine();
                    if (lineA65.Contains(modeloA65))
                    {
                        CHAMARCUSTOM = "OK";
                        System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                        pProcess.StartInfo.FileName = @"C:\Users\Public\Desktop\Avell Custom.lnk";
                        pProcess.Start();
                        break;
                    }
                }
                readerA65.Close();
                //-----------------------------------------------------------------
                //-----------------------------------------------------------------
                //-----------------------------------------------------------------
                //-----------------------------------------------------------------
                
                string modeloA52i = "A52i";
                string filePathA52i = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerA52i = new StreamReader(filePathA52i);

                while (!readerA52i.EndOfStream)
                {
                    string lineA52i = readerA52i.ReadLine();
                    if (lineA52i.Contains(modeloA52i))
                    {
                        CHAMARCUSTOM = "OK";
                        System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                        pProcess.StartInfo.FileName = @"C:\Users\Public\Desktop\Avell Custom Control.lnk";
                        pProcess.Start();
                        break;
                    }
                }
                readerA52i.Close();
                //-----------------------------------------------------------------
                //-----------------------------------------------------------------

                string modeloA52r = "A52r";
                string filePathA52r = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerA52r = new StreamReader(filePathA52r);

                while (!readerA52r.EndOfStream)
                {
                    string lineA52r = readerA52r.ReadLine();
                    if (lineA52r.Contains(modeloA52r))
                    {
                        CHAMARCUSTOM = "OK";
                        System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                        pProcess.StartInfo.FileName = @"C:\Users\Public\Desktop\Avell Custom Control.lnk";
                        pProcess.Start();
                        break;
                    }
                }
                readerA52r.Close();
                //-----------------------------------------------------------------
                //-----------------------------------------------------------------

                string modelo350 = "350";
                string filePath350 = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader reader350 = new StreamReader(filePath350);

                while (!reader350.EndOfStream)
                {
                    string line350 = reader350.ReadLine();
                    if (line350.Contains(modelo350))
                    {
                        CHAMARCUSTOM = "OK";
                        System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                        pProcess.StartInfo.FileName = @"C:\Users\Public\Desktop\Avell Custom Control.lnk";
                        pProcess.Start();
                        break;
                    }
                }
                reader350.Close();
                //-----------------------------------------------------------------
                //-----------------------------------------------------------------

                string modelo350r = "350r";
                string filePath350r = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader reader350r = new StreamReader(filePath350r);

                while (!reader350r.EndOfStream)
                {
                    string line350r = reader350r.ReadLine();
                    if (line350r.Contains(modelo350r))
                    {
                        CHAMARCUSTOM = "OK";
                        System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                        pProcess.StartInfo.FileName = @"C:\Users\Public\Desktop\Avell Custom Control.lnk";
                        pProcess.Start();
                        break;
                    }
                }
                reader350r.Close();
                //-----------------------------------------------------------------
                //-----------------------------------------------------------------

                string modelo450 = "450";
                string filePath450 = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader reader450 = new StreamReader(filePath450);

                while (!reader450.EndOfStream)
                {
                    string line450 = reader450.ReadLine();
                    if (line450.Contains(modelo450))
                    {
                        CHAMARCUSTOM = "OK";
                        System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                        pProcess.StartInfo.FileName = @"C:\Users\Public\Desktop\Avell Custom Control.lnk";
                        pProcess.Start();
                        break;
                    }
                }
                reader450.Close();
                //-----------------------------------------------------------------
				//-----------------------------------------------------------------

                string modelo450r = "450r";
                string filePath450r = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader reader450r = new StreamReader(filePath450r);

                while (!reader450r.EndOfStream)
                {
                    string line450r = reader450r.ReadLine();
                    if (line450r.Contains(modelo450r))
                    {
                        CHAMARCUSTOM = "OK";
                        System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                        pProcess.StartInfo.FileName = @"C:\Users\Public\Desktop\Avell Custom Control.lnk";
                        pProcess.Start();
                        break;
                    }
                }
                reader450r.Close();
                //-----------------------------------------------------------------
				//-----------------------------------------------------------------

                string modelo460 = "460";
                string filePath460 = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader reader460 = new StreamReader(filePath460);

                while (!reader460.EndOfStream)
                {
                    string line460 = reader460.ReadLine();
                    if (line460.Contains(modelo460))
                    {
                        CHAMARCUSTOM = "OK";
                        System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                        pProcess.StartInfo.FileName = @"C:\Users\Public\Desktop\Avell Custom Control.lnk";
                        pProcess.Start();
                        break;
                    }
                }
                reader460.Close();
                //-----------------------------------------------------------------
				//-----------------------------------------------------------------

                string modelo470 = "470";
                string filePath470 = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader reader470 = new StreamReader(filePath470);

                while (!reader470.EndOfStream)
                {
                    string line470 = reader470.ReadLine();
                    if (line470.Contains(modelo470))
                    {
                        CHAMARCUSTOM = "OK";
                        System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                        pProcess.StartInfo.FileName = @"C:\Users\Public\Desktop\Avell Custom Control.lnk";
                        pProcess.Start();
                        break;
                    }
                }
                reader470.Close();
                //-----------------------------------------------------------------
				//-----------------------------------------------------------------

                string modelo480 = "480";
                string filePath480 = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader reader480 = new StreamReader(filePath480);

                while (!reader480.EndOfStream)
                {
                    string line480 = reader480.ReadLine();
                    if (line480.Contains(modelo480))
                    {
                        CHAMARCUSTOM = "OK";
                        System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                        pProcess.StartInfo.FileName = @"C:\Users\Public\Desktop\Avell Custom Control.lnk";
                        pProcess.Start();
                        break;
                    }
                }
                reader480.Close();
                //-----------------------------------------------------------------
				//-----------------------------------------------------------------

                string modelo490 = "490";
                string filePath490 = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader reader490 = new StreamReader(filePath490);

                while (!reader490.EndOfStream)
                {
                    string line490 = reader490.ReadLine();
                    if (line490.Contains(modelo490))
                    {
                        CHAMARCUSTOM = "OK";
                        System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                        pProcess.StartInfo.FileName = @"C:\Users\Public\Desktop\Avell Custom Control.lnk";
                        pProcess.Start();
                        break;
                    }
                }
                reader490.Close();
                //-----------------------------------------------------------------

                //-----------------------------------------------------------------
                string modeloSmart = "Smart";
                string filePathSmart = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerSmart = new StreamReader(filePathSmart);

                while (!readerSmart.EndOfStream)
                {
                    string lineSmart = readerSmart.ReadLine();
                    if (lineSmart.Contains(modeloSmart))
                    {
                        CHAMARCUSTOM = "OK";
                        ChamarB_ON();
                        break;
                    }
                }
                readerSmart.Close();
                //-----------------------------------------------------------------

                //-----------------------------------------------------------------
                string modelo145 = "145";
                string filePath145 = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader reader145 = new StreamReader(filePath145);

                while (!reader145.EndOfStream)
                {
                    string line145 = reader145.ReadLine();
                    if (line145.Contains(modelo145))
                    {
                        CHAMARCUSTOM = "OK";
                        ChamarB_ON();
                        break;
                    }
                }
                reader145.Close();
                //-----------------------------------------------------------------

                //-----------------------------------------------------------------
                string modelo147 = "147";
                string filePath147 = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader reader147 = new StreamReader(filePath147);

                while (!reader147.EndOfStream)
                {
                    string line147 = reader147.ReadLine();
                    if (line147.Contains(modelo147))
                    {
                        CHAMARCUSTOM = "OK";
                        ChamarB_ON();
                        break;
                    }
                }
                reader147.Close();
                //-----------------------------------------------------------------

                string modeloA70i = "A70i";
                string filePathA70i = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerA70i = new StreamReader(filePathA70i);

                while (!readerA70i.EndOfStream)
                {
                    string lineA70i = readerA70i.ReadLine();
                    if (lineA70i.Contains(modeloA70i))
                    {
                        CHAMARCUSTOM = "OK";
                        System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                        pProcess.StartInfo.FileName = @"C:\Users\Public\Desktop\Avell Custom Control.lnk";
                        pProcess.Start();
                        break;
                    }
                }
                readerA70i.Close();
                //-----------------------------------------------------------------
				
            }
            catch
            {
                MessageBox.Show("Arquivo sistema.txt não encontrado em: C:\\TESTES_AVELL\\systemInfo", "ARQUIVO NÃO ENCONTRADO!");
            }
        }
        
        public void ChamarB_ON()
        {
            //Ajustado aqui para não mais abrir
            if (CUSTOMOK != "B_ON")
            {
                CHAMARCUSTOM = "OK";
                CUSTOMOK = "B_ON";
                B_ON formB_ON = new B_ON();
                this.Hide();
                formB_ON.ShowDialog();
            }
        }

        public void TimeStart()
        //Preciso adicionar este time para tirar o bug de abrir o form antes de finalizar o processo.
        {
            //Só vai iniciar a contagem se a máquina não for B.ON
            if (TIMECUSTOM != "OK")
            {
                Timer relogio = new Timer();
                relogio.Interval = 1000;
                int tempo = 21;
                relogio.Tick += delegate
                {
                    tempo -= 1;
                    lblTime.Text = tempo.ToString();
                    if (tempo == 0)
                    {
                        relogio.Stop();

                        //Inserir Inicio do TesteCustomControl
                        InsertAvellWeb();

                        TIMECUSTOM = "OK";
                        VerificaPastaCustom();
                        //De qualquer forma o Custom não está sendo fechado aqui
                        //TaskKillCustom();
                    }
                };
                relogio.Start();
            }
        }

        public void VerificaPastaCustom()
        {
            if (CONFIRMACUSTOMOK != "OK")
            {
                //Não vou usar a tratativa do try catch, vou usar esta outra
                string CaminhoCustom = @"C:\Program Files\OEM";
                DateTime fileCreatedDate = File.GetCreationTime(CaminhoCustom);

                DateTime dt1 = DateTime.Parse("01/01/2023 00:00:00");
                DateTime dt2 = fileCreatedDate;

                if (dt2.Date > dt1.Date)
                {
                    MessageBox.Show("DATA OEM CUSTOM OK!" + fileCreatedDate, "DATA OEM CUSTOM OK!");

                    CONFIRMACUSTOMOK = "OK";
                    CONFIRMACUSTOM formConfirmaCustom = new CONFIRMACUSTOM();
                    this.Hide();
                    formConfirmaCustom.ShowDialog();
                }
                else
                {
                    try
                    {
                        var dataHoraMinuto = DateTime.Now.ToString("dd/MM/yyyy - HH:mm");
                        ManagementObjectSearcher MOS1 = new ManagementObjectSearcher("Select * From Win32_BIOS");
                        foreach (ManagementObject getserial in MOS1.Get())
                        {
                            string SerialAvell = getserial["SerialNumber"].ToString();
                            String DadosFirebase1 = "Problemas de Licença Custom:" + dataHoraMinuto;
                            var teste = new custom
                            {
                                Serial = SerialAvell,
                                TControl = DadosFirebase1
                            };
                            FirebaseResponse response = client.Update("WEB_CONSULTA_TESTEFALHA/" + SerialAvell, teste);
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

        public void TaskKillCustom()
        {
            if (TASKKILLCUSTOM != "OK")
            {
                try
                {
                    TASKKILLCUSTOM = "OK";
                    Process.Start("taskkill", "/F /IM ControlCenterU.exe");
                    Process.Start("taskkill", "/F /IM Avell Custom Control.exe");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }
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
                    String DadosFirebase1 = "CControl Início:" + dataHoraMinuto;
                    var teste = new custom
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
