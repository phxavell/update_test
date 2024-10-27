using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MaterialSkin;
using FireSharp.Config;
using FireSharp.Interfaces;
using System.Management;
using FireSharp.Response;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Drawing.Drawing2D;

namespace TECLADO
{
    public partial class TESTETECLADO : MaterialSkin.Controls.MaterialForm
    {
        public string TECLADO;

        public TESTETECLADO()
        {
            InitializeComponent();
            StartFireBaseServices();
            //Se arquivo de referencia não existir, cria.
            VerificaArquivoSistema();
            InsertAvellWeb();
            Interacao();
            TimeStart();
        }

        public void VerificaArquivoSistema()
        {
            //Verificar se o arquivo existe, se não existir, vou criar:
            //Vou usuar uma variável booleana para verificar 'true' ou false 'false'
            //Adicionar em todos os testes que fazem consulta no arquivo sistema.txt
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
            //Asterisk/Beep/Exclamation/Hand/Question - Não utilizados Aqui
            //SystemSounds.Hand.Play();
            //https://www.naturalreaders.com/online/ - Cria vozes
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\Teclado.mp3";
            wplayer.controls.play();
        }

        public void TimeStart()
        {
            if (TECLADO != "OK")
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
                        relogio.Stop();
                        ProcModelosNosLogs();
                    }
                };
                relogio.Start();
            }
        }

        public void ProcModelosNosLogs()
        {
            try
            {
                //PRECISA POSTERIORMENTE NA VERSÃO 3 DEIXAR DE FORMA DINÂMICA - PERSISTIR DADOS
                string modeloC65 = "C65";
                string filePathC65 = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerC65 = new StreamReader(filePathC65);

                while (!readerC65.EndOfStream)
                {
                    string lineC65 = readerC65.ReadLine();
                    if (lineC65.Contains(modeloC65))
                    {
                        TECLADO = "OK";
                        TECLADO1 formTeclado1 = new TECLADO1();
                        this.Hide();
                        formTeclado1.ShowDialog();
                        break;
                    }
                }
                readerC65.Close();

                //-----------------------------------------------------------------
                string modeloA55 = "A55";
                string filePathA55 = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerA55 = new StreamReader(filePathA55);

                while (!readerA55.EndOfStream)
                {
                    string lineA55 = readerA55.ReadLine();
                    if (lineA55.Contains(modeloA55))
                    {
                        TECLADO = "OK";
                        TECLADO3 formTeclado3 = new TECLADO3();
                        this.Hide();
                        formTeclado3.ShowDialog();
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
                        TECLADO = "OK";
                        TECLADO3 formTeclado3 = new TECLADO3();
                        this.Hide();
                        formTeclado3.ShowDialog();
                        break;
                    }
                }
                readerA57.Close();

                string modeloA70i = "A70i";
                string filePathA70i = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerA70i = new StreamReader(filePathA70i);

                while (!readerA70i.EndOfStream)
                {
                    string lineA70i = readerA70i.ReadLine();
                    if (lineA70i.Contains(modeloA70i))
                    {
                        TECLADO = "OK";
                        TECLADO8 formTeclado8 = new TECLADO8();
                        this.Hide();
                        formTeclado8.ShowDialog();
                        break;
                    }
                }
                readerA70i.Close();

                //-----------------------------------------------------------------
                string modeloA70 = "A70";
                string filePathA70 = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerA70 = new StreamReader(filePathA70);

                while (!readerA70.EndOfStream)
                {
                    string lineA70 = readerA70.ReadLine();
                    if (lineA70.Contains(modeloA70) && !lineA70.Contains(modeloA70i))
                    {
                        TECLADO = "OK";
                        //Nova estrutura para seleção do teclado pela BIOS, matar o while
                        SelectQuery tecladoAvell = new SelectQuery("Win32_BIOS");
                        ManagementObjectSearcher objOSDetails = new ManagementObjectSearcher(tecladoAvell);
                        ManagementObjectCollection osDetailsCollection = objOSDetails.Get();
                        foreach (ManagementObject filtroPorBios in osDetailsCollection)
                        {
                            string selecaoVersao = (filtroPorBios["SMBIOSBIOSVersion"].ToString());

                            if (selecaoVersao == "N.1.10AVE01")
                            {
                                //Se for esta versão de BIOS chamará o novo Techado
                                TECLADO7 formTeclado7 = new TECLADO7();
                                this.Hide();
                                formTeclado7.ShowDialog();
                            }
                            else
                            {
                                //Se for a versão anterior vai chamar o layout antigo
                                TECLADO2 formTecladoA70 = new TECLADO2();
                                this.Hide();
                                formTecladoA70.ShowDialog();
                            }
                        }
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
                        TECLADO = "OK";
                        //Nova estrutura para seleção do teclado pela BIOS, matar o while
                        SelectQuery tecladoAvell = new SelectQuery("Win32_BIOS");
                        ManagementObjectSearcher objOSDetails = new ManagementObjectSearcher(tecladoAvell);
                        ManagementObjectCollection osDetailsCollection = objOSDetails.Get();
                        foreach (ManagementObject filtroPorBios in osDetailsCollection)
                        {
                            string selecaoVersao = (filtroPorBios["SMBIOSBIOSVersion"].ToString());

                            if (selecaoVersao == "N.1.10AVE01")
                            {
                                //Se for esta versão de BIOS chamará o novo Techado
                                TECLADO7 formTeclado7 = new TECLADO7();
                                this.Hide();
                                formTeclado7.ShowDialog();
                            }
                            else
                            {
                                //Se for a versão anterior vai chamar o layout antigo
                                TECLADO2 formTecladoA70 = new TECLADO2();
                                this.Hide();
                                formTecladoA70.ShowDialog();
                            }
                        }
                        break;
                    }
                }
                readerA72.Close();

                //-----------------------------------------------------------------
                string modeloBON = "B.ON";
                string filePathBON = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerBON = new StreamReader(filePathBON);

                while (!readerBON.EndOfStream)
                {
                    string lineBON = readerBON.ReadLine();
                    if (lineBON.Contains(modeloBON))
                    {
                        TECLADO = "OK";
                        TECLADO3 formTeclado3 = new TECLADO3();
                        this.Hide();
                        formTeclado3.ShowDialog();
                        break;
                    }
                }
                readerBON.Close();

                //-----------------------------------------------------------------
                string modeloA52 = "A52";
                string filePathA52 = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerA52 = new StreamReader(filePathA52);

                while (!readerA52.EndOfStream)
                {
                    string lineA52 = readerA52.ReadLine();
                    if (lineA52.Contains(modeloA52))
                    {
                        TECLADO = "OK";
                        TECLADO2 formTeclado2 = new TECLADO2();
                        this.Hide();
                        formTeclado2.ShowDialog();
                        break;
                    }
                }
                readerA52.Close();

                //-----------------------------------------------------------------
                string modeloB11 = "B11";
                string filePathB11 = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerB11 = new StreamReader(filePathB11);

                while (!readerB11.EndOfStream)
                {
                    string lineB11 = readerB11.ReadLine();
                    if (lineB11.Contains(modeloB11))
                    {
                        TECLADO = "OK";
                        //Verificar se realmente o B11 é esse layout
                        TECLADO2 formTeclado2 = new TECLADO2();
                        this.Hide();
                        formTeclado2.ShowDialog();
                        break;
                    }
                }
                readerB11.Close();

                //-----------------------------------------------------------------
                string modeloStorm = "STORM";
                string filePathStorm = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerStorm = new StreamReader(filePathStorm);

                while (!readerStorm.EndOfStream)
                {
                    string lineStorm = readerStorm.ReadLine();
                    if (lineStorm.Contains(modeloStorm))
                    {
                        TECLADO = "OK";
                        //Nova estrutura para seleção do teclado pela BIOS, matar o while
                        SelectQuery tecladoAvell = new SelectQuery("Win32_BIOS");
                        ManagementObjectSearcher objOSDetails = new ManagementObjectSearcher(tecladoAvell);
                        ManagementObjectCollection osDetailsCollection = objOSDetails.Get();
                        foreach (ManagementObject filtroPorBios in osDetailsCollection)
                        {
                            string selecaoVersao = (filtroPorBios["SMBIOSBIOSVersion"].ToString());

                            if (selecaoVersao == "N.1.10AVE04")
                            {
                                TECLADO2 formTeclado2 = new TECLADO2();
                                this.Hide();
                                formTeclado2.ShowDialog();
                            }
                            else if (selecaoVersao == "N.1.09AVE02")
                            {
                                TECLADO6 formTeclado2 = new TECLADO6();
                                this.Hide();
                                formTeclado2.ShowDialog();
                            }
                            else if (selecaoVersao == "N.1.22AVE00")
                            {
                                TECLADO2 formTeclado2 = new TECLADO2();
                                this.Hide();
                                formTeclado2.ShowDialog();
                            }
                            else if (selecaoVersao == "N.1.23AVE02")
                            {
                                TECLADO2 formTeclado2 = new TECLADO2();
                                this.Hide();
                                formTeclado2.ShowDialog();
                            }
                            else if (selecaoVersao == "N.1.10AVE03")
                            {
                                TECLADO2 formTeclado2 = new TECLADO2();
                                this.Hide();
                                formTeclado2.ShowDialog();
                            }
                            else if (selecaoVersao == "N.1.13AVE05")
                            {
                                TECLADO2 formTeclado2 = new TECLADO2();
                                this.Hide();
                                formTeclado2.ShowDialog();
                            }
                            else if (selecaoVersao == "N.1.09AVE00")
                            {
                                TECLADO2 formTeclado2 = new TECLADO2();
                                this.Hide();
                                formTeclado2.ShowDialog();
                            }
                        }
                        break;
                    }
                }
                readerStorm.Close();

                //-----------------------------------------------------------------
                //Adicionado Layout de modelo antigo
                string modeloA65 = "A65";
                string filePathA65 = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerA65 = new StreamReader(filePathA65);

                while (!readerA65.EndOfStream)
                {
                    string lineA65 = readerA65.ReadLine();
                    if (lineA65.Contains(modeloA65))
                    {
                        TECLADO = "OK";
                        TECLADO2 formTeclado4 = new TECLADO2();
                        this.Hide();
                        formTeclado4.ShowDialog();
                        break;
                    }
                }
                readerA65.Close();

                //-----------------------------------------------------------------
                string modeloC62 = "C62";
                string filePathC62 = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerC62 = new StreamReader(filePathC62);

                while (!readerC62.EndOfStream)
                {
                    string lineC62 = readerC62.ReadLine();
                    if (lineC62.Contains(modeloC62))
                    {
                        TECLADO = "OK";
                        TECLADO5 formTeclado5 = new TECLADO5();
                        this.Hide();
                        formTeclado5.ShowDialog();
                        break;
                    }
                }
                readerC62.Close();

                //-----------------------------------------------------------------
                string modeloStormBS = "STORM BS";
                string filePathStormBS = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerStormBS = new StreamReader(filePathStormBS);

                while (!readerStormBS.EndOfStream)
                {
                    string lineStormBS = readerStormBS.ReadLine();
                    if (lineStormBS.Contains(modeloStormBS))
                    {
                        TECLADO = "OK";
                        TECLADO2 formTeclado2 = new TECLADO2();
                        this.Hide();
                        formTeclado2.ShowDialog();
                        break;
                    }
                }
                readerStormBS.Close();
                //-----------------------------------------------------------------

                string modeloA62Liv = "A62 LIV";
                string filePathA62Liv = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerA62Liv = new StreamReader(filePathStormBS);

                while (!readerA62Liv.EndOfStream)
                {
                    string lineA62Liv = readerA62Liv.ReadLine();
                    if (lineA62Liv.Contains(modeloA62Liv))
                    {
                        TECLADO = "OK";
                        TECLADO2 formTeclado2 = new TECLADO2();
                        this.Hide();
                        formTeclado2.ShowDialog();
                        break;
                    }
                }
                readerStormBS.Close();
                
                //-----------------------------------------------------------------


                //-----------------------------------------------------------------
                string modelo460 = "460";
                string filePath460 = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader reader460 = new StreamReader(filePath460);

                while (!reader460.EndOfStream)
                {
                    string line460 = reader460.ReadLine();
                    if (line460.Contains("Modelo do sistema:") && line460.Contains(modelo460))
                    {
                        TECLADO = "OK";
                        TECLADO8 formTeclado8 = new TECLADO8();
                        this.Hide();
                        formTeclado8.ShowDialog();
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
                    if (line470.Contains("Modelo do sistema:") && line470.Contains(modelo470))
                    {
                        TECLADO = "OK";
                        TECLADO8 formTeclado8 = new TECLADO8();
                        this.Hide();
                        formTeclado8.ShowDialog();
                        break;
                    }
                }
                reader470.Close();

                //-----------------------------------------------------------------
                string modeloSmart = "Smart";
                string filePathSmart = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerSmart = new StreamReader(filePathSmart);

                while (!readerSmart.EndOfStream)
                {
                    string lineSmart = readerSmart.ReadLine();
                    if (lineSmart.Contains(modeloSmart))
                    {
                        TECLADO = "OK";
                        TECLADO9 formTeclado9 = new TECLADO9();
                        this.Hide();
                        formTeclado9.ShowDialog();
                        break;
                    }
                }
                reader470.Close();

                //-----------------------------------------------------------------

                //-----------------------------------------------------------------
                string modelo145 = "145";
                string filePath145 = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader reader145 = new StreamReader(filePath145);

                while (!reader145.EndOfStream)
                {
                    string line145 = reader145.ReadLine();
                    if (line145.Contains("Modelo do sistema:") && line145.Contains(modelo145))
                    {
                        TECLADO = "OK";
                        TECLADO10 formTeclado10 = new TECLADO10();
                        this.Hide();
                        formTeclado10.ShowDialog();
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
                    if (line147.Contains("Modelo do sistema:") && line147.Contains(modelo147))
                    {
                        TECLADO = "OK";
                        TECLADO10 formTeclado10 = new TECLADO10();
                        this.Hide();
                        formTeclado10.ShowDialog();
                        break;
                    }
                }
                reader147.Close();

                //-----------------------------------------------------------------
                //STORM 480
                string modelo480 = "480";
                string filePath480 = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader reader480 = new StreamReader(filePath480);

                while (!reader480.EndOfStream)
                {
                    string line480 = reader480.ReadLine();
                    if (line480.Contains("Modelo do sistema:") && line480.Contains(modelo480))
                    {
                        TECLADO = "OK";
                        TECLADO2 formTeclado4 = new TECLADO2();
                        this.Hide();
                        formTeclado4.ShowDialog();
                        break;
                    }
                }
                reader480.Close();

                //-----------------------------------------------------------------
                //STORM 490
                string modelo490 = "490";
                string filePath490 = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader reader490 = new StreamReader(filePath490);

                while (!reader490.EndOfStream)
                {
                    string line490 = reader490.ReadLine();
                    if (line490.Contains("Modelo do sistema:") && line490.Contains(modelo490))
                    {
                        TECLADO = "OK";
                        TECLADO2 formTeclado4 = new TECLADO2();
                        this.Hide();
                        formTeclado4.ShowDialog();
                        break;
                    }
                }
                reader490.Close();

                //-----------------------------------------------------------------
                //STORM 490
                string modeloD68i = "D68i";
                string filePathd68i = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerD68i = new StreamReader(filePathd68i);

                while (!readerD68i.EndOfStream)
                {
                    string lineD68i = readerD68i.ReadLine();
                    if (lineD68i.Contains(modeloD68i))
                    {
                        TECLADO = "OK";
                        TECLADO8 formTeclado8 = new TECLADO8();
                        this.Hide();
                        formTeclado8.ShowDialog();
                        break;
                    }
                }
                reader490.Close();
                /*
                //-----------------------------------------------------------------
                // A52i
                //-----------------------------------------------------------------
                string modeloA52i = "A52i";
                string filePathA52i = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerA52i = new StreamReader(filePathA52i);

                while (!readerA52i.EndOfStream)
                {
                    string lineA52i = readerA52i.ReadLine();
                    if (lineA52i.Contains(modeloA52i))
                    {
                        TECLADO = "OK";
                        TECLADO2 formTeclado2 = new TECLADO2();
                        this.Hide();
                        formTeclado2.ShowDialog();
                        break;
                    }
                }
                readerA52i.Close();

                // A52r
                //-----------------------------------------------------------------
                string modeloA52r = "A52r";
                string filePathA52r = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader readerA52r = new StreamReader(filePathA52r);

                while (!readerA52r.EndOfStream)
                {
                    string lineA52r = readerA52r.ReadLine();
                    if (lineA52r.Contains(modeloA52r))
                    {
                        TECLADO = "OK";
                        TECLADO2 formTeclado2 = new TECLADO2();
                        this.Hide();
                        formTeclado2.ShowDialog();
                        break;
                    }
                }
                readerA52r.Close();
				*/
                // 350r
                //-----------------------------------------------------------------
                string modelo350r = "350r";
                string filePath350r = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader reader350r = new StreamReader(filePath350r);

                while (!reader350r.EndOfStream)
                {
                    string line350r = reader350r.ReadLine();
                    if (line350r.Contains(modelo350r))
                    {
                        TECLADO = "OK";
                        TECLADO2 formTeclado2 = new TECLADO2();
                        this.Hide();
                        formTeclado2.ShowDialog();
                        break;
                    }
                }
                reader350r.Close();

                // 350
                //-----------------------------------------------------------------
                string modelo350 = "350";
                string filePath350 = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader reader350 = new StreamReader(filePath350);

                while (!reader350.EndOfStream)
                {
                    string line350 = reader350.ReadLine();
                    if (line350.Contains(modelo350))
                    {
                        TECLADO = "OK";
                        TECLADO2 formTeclado2 = new TECLADO2();
                        this.Hide();
                        formTeclado2.ShowDialog();
                        break;
                    }
                }
                reader350.Close();
				
                // 450
                //-----------------------------------------------------------------
                string modelo450 = "450";
                string filePath450 = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader reader450 = new StreamReader(filePath450);

                while (!reader450.EndOfStream)
                {
                    string line450 = reader450.ReadLine();
                    if (line450.Contains(modelo450))
                    {
                        TECLADO = "OK";
                        TECLADO2 formTeclado2 = new TECLADO2();
                        this.Hide();
                        formTeclado2.ShowDialog();
                        break;
                    }
                }
                reader450.Close();
				
                // 450r
                //-----------------------------------------------------------------
                string modelo450r = "450r";
                string filePath450r = @"C:\TESTES_AVELL\systemInfo\sistema.txt";
                StreamReader reader450r = new StreamReader(filePath450r);

                while (!reader450r.EndOfStream)
                {
                    string line450r = reader450r.ReadLine();
                    if (line450r.Contains(modelo450r))
                    {
                        TECLADO = "OK";
                        TECLADO2 formTeclado2 = new TECLADO2();
                        this.Hide();
                        formTeclado2.ShowDialog();
                        break;
                    }
                }
                reader450r.Close();
                
                //-----------------------------------------------------------------
            }
            catch
            {
                MessageBox.Show("Arquivo sistema.txt não encontrado em: C:\\TESTES_AVELL\\systemInfo", "ARQUIVO NÃO ENCONTRADO!");
            }
        }
        /*
        
        public void ProcModelosNosLogs1()
        {
            try
            {
                string filePath = @"C:\TESTES_AVELL\systemInfo\sistema.txt";

                string[][] modelos = new string[][]
                {
            new string[] { "A52i" },
            new string[] { "350", "450" },
            new string[] { "A52r", "350r", "450r" }
                };

                foreach (var grupoModelos in modelos)
                {
                    bool encontrado = false;
                    foreach (var modelo in grupoModelos)
                    {
                        using (StreamReader reader = new StreamReader(filePath))
                        {
                            while (!reader.EndOfStream)
                            {
                                string line = reader.ReadLine();
                                if (line.Contains(modelo))
                                {
                                    encontrado = true;
                                    TECLADO = "OK";

                                    switch (modelo)
                                    {
                                        case "A52i":
                                            TECLADO2 formTeclado2A52i = new TECLADO2();
                                            this.Hide();
                                            formTeclado2A52i.ShowDialog();
                                            break;
                                        case "350":
                                        case "450":
                                            TECLADO8 formTeclado8 = new TECLADO8();
                                            this.Hide();
                                            formTeclado8.ShowDialog();
                                            break;
                                        case "A52r":
                                        case "350r":
                                        case "450r":
                                            TECLADO2 formTeclado2A52r = new TECLADO2();
                                            this.Hide();
                                            formTeclado2A52r.ShowDialog();
                                            break;
                                    }

                                    break;
                                }
                            }
                        }

                        if (encontrado)
                            break;
                    }

                    if (encontrado)
                        break;
                }
            }
            catch
            {
                MessageBox.Show("Arquivo sistema.txt não encontrado em: C:\\TESTES_AVELL\\systemInfo", "ARQUIVO NÃO ENCONTRADO!");
            }
        }
        */
        public void ProcModelosNosLogs2()
        {
            try
            {
                string filePath = @"C:\TESTES_AVELL\systemInfo\sistema.txt";

                string[] modelos = { "A52i", "350", "450", "A52r", "350r", "450r", "A70i", "460", "470", "480", "490", "145", "147" };

                foreach (var modelo in modelos)
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            if (line.Contains(modelo))
                            {
                                TECLADO = "OK";

                                // Agrupando os modelos, talvezzzz resolva o bug duplo, ajustar depois 145, 147, smart, 480 e 490
                                switch (modelo)
                                {
                                    case "A52i":
                                    case "350":
                                    case "450":
                                    case "A52r":
                                    case "350r":
                                    case "450r":
                                        TECLADO2 formTeclado2 = new TECLADO2();
                                        this.Hide();
                                        formTeclado2.ShowDialog();
                                        break;
                                    case "A70i":
                                    case "460":
                                    case "470":
                                        TECLADO8 formTeclado8 = new TECLADO8();
                                        this.Hide();
                                        formTeclado8.ShowDialog();
                                        break;
                                    case "480":
                                    case "490":
                                        TECLADO2 formTeclado2A480 = new TECLADO2();
                                        this.Hide();
                                        formTeclado2A480.ShowDialog();
                                        break;
                                    case "145":
                                    case "147":
                                        TECLADO2 formTeclado2A145 = new TECLADO2();
                                        this.Hide();
                                        formTeclado2A145.ShowDialog();
                                        break;
                                    default:
                                        // se não encontrar o modelo voltar isso, debug
                                        MessageBox.Show($"Modelo '{modelo}' não reconhecido.");
                                        break;
                                }

                                return; // Encerra tudo e partiu casa
                            }
                        }
                    }
                }

                // Se nenhum modelo correspondente for encontrado
                MessageBox.Show("Modelo não reconhecido ou arquivo sistema.txt não encontrado em: C:\\TESTES_AVELL\\systemInfo", "ERRO");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao processar modelos: {ex.Message}", "ERRO");
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
                    String DadosFirebase1 = "Teclado Início:" + dataHoraMinuto;
                    var teste = new teclado
                    {
                        Serial = SerialAvell,
                        TTeclado = DadosFirebase1
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

        private void lblConfirme_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxTeclado_Click(object sender, EventArgs e)
        {

        }
    }
}