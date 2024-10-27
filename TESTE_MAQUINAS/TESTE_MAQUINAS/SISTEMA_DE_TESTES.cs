using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MaterialSkin;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using ROBOTESTE;
using System.Drawing;
using TECLADO;

namespace TESTE_MAQUINAS
{
    //public partial class Sistema_Teste : MaterialSkin.Controls.MaterialForm
    public partial class Sistema_Teste : Form
    {
        public string Fonte;
        public string WinAuditOK;
        public string InstaladorDriver;
        //Sting para evitar loop!
        public string BURNIN;

        public Sistema_Teste()
        {
            InitializeComponent();
            TaskKillSysprep();
            tirarHiberna();
            InstaladorDeDrivers();
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

        public void setTime()
        {
            try
            {
                Process sincronizarhora = System.Diagnostics.Process.Start(@"C:\TESTES_AVELL\.executaveisAux\SincronizarHoraServer.exe");
                sincronizarhora.WaitForExit();
                //int saida = sincronizarhora.ExitCode;
            }
            catch (Exception ex){ }
        }

        public void tirarHiberna()//Ajustado nova versão
        {
            try
            {
                Process.Start(@"C:\TESTES_AVELL\.executaveisAux\hibernarOff.vbs");
            }
            catch (Exception ex) { MessageBox.Show("Caminho não encontrado: C:\\TESTES_AVELL\\.executaveisAux\\hibernarOff.vbs"); }
        }

        public void InstaladorDeDrivers()
        {
            var logDrivers = Directory.GetFiles(@"C:\TESTES_AVELL\log_drivers", "*.log", SearchOption.TopDirectoryOnly).Count().ToString();
            int valor = int.Parse(logDrivers);
            if (valor < 1)
            {
                CriarPastaLogs();
                //Atualizaçao, antes copias eram feitas com Python=======================================================================
                try
                {
                    //Forma dinâmica para copiar limpar testes para o usuário
                    //que estiver na máquina (Administador) ou (usuário personalizado)
                    string origem = @"C:\TESTES_AVELL\finaliza\LimparTestes";
                    string Usuario = System.Environment.UserName;
                    string destino2 = @"C:\\Users\\" + Usuario + "\\Desktop\\LimparTestes";

                    if (Directory.Exists(origem))
                    {
                        if (!Directory.Exists(destino2))
                        {
                            Directory.CreateDirectory(destino2);
                        }
                        foreach (var srcPath in Directory.GetFiles(origem))
                        {
                            File.Copy(srcPath, srcPath.Replace(origem, destino2), true);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Não foi possível copiar arquivos de limpeza", "LimparTestes - Erro");
                }
                //=======================================================================================================================

                try
                {
                    InteracaoDrivers();
                    Process InstalarDrivers = Process.Start(@"C:\TESTES_AVELL\.executaveisAux\Instalador.exe");
                    InstalarDrivers.WaitForExit();

                    var dataHoraMinutoOK = DateTime.Now.ToString("dd-MM-yyyy-HH:mm:s:s");
                    const string Drivers = @"C:\TESTES_AVELL\log_drivers\DriverInst.log";
                    var Arquivo = File.AppendText(Drivers);
                    Arquivo.WriteLine("DRIVER INSTALADO: " + dataHoraMinutoOK);
                    Arquivo.Close();
                }
                catch (Exception ex) { }
            }
            else
            {
                var logLicenca = Directory.GetFiles(@"C:\TESTES_AVELL\log_licenca", "*.log", SearchOption.TopDirectoryOnly).Count().ToString();
                int valorLicenca = int.Parse(logLicenca);
                if (valorLicenca < 1)
                {
                    //Conecta Wi-fi e instala a licença
                    WiFiConectLicenca();
                    setTime();
                }
                else
                {
                    Iniciar();
                }
            }
        }

        public void WiFiConectLicenca()
        {
            ProcessStartInfo ConectarWifi1 = new ProcessStartInfo("cmd.exe", @"/C " + "netsh wlan add profile filename=C:\\TESTES_AVELL\\.executaveisAux\\avell-infra-ultra.xml");
            ConectarWifi1.UseShellExecute = false;
            ConectarWifi1.CreateNoWindow = true;
            Process.Start(ConectarWifi1);

            //Conexão com Avell Infra-ultar
            ProcessStartInfo ConectarWifi2 = new ProcessStartInfo("cmd.exe", @"/C " + "netsh wlan connect ssid=avell-infra-ultra name=avell-infra-ultra");
            ConectarWifi2.UseShellExecute = false;
            ConectarWifi2.CreateNoWindow = true;
            Process.Start(ConectarWifi2);

            //Ativação do Windows
            WallpaperEAtivacao();
            var dataHoraMinuto2OK = DateTime.Now.ToString("dd-MM-yyyy-HH:mm:s:s");
            const string DriversLicenca = @"C:\TESTES_AVELL\log_licenca\Licenca.log";
            var Arquivo2 = File.AppendText(DriversLicenca);
            Arquivo2.WriteLine("LICENÇA VERIFICADA: " + dataHoraMinuto2OK);
            Arquivo2.Close();
            //Se entrar aqui, espera um tempo e executa
            TimeReload();
        }

        //Ativação manual do Windows
        private void pictureBoxAtivacao_Click(object sender, EventArgs e)
        {
            
        }

        //ATIVAÇÃO===================================================================================================================
        public void WallpaperEAtivacao()
        {
            //Chamnar aplicação do papel de parede
            try
            {
                Process papelDeParede = System.Diagnostics.Process.Start(@"C:\Users\Administrador\Desktop\Teste\avell_wallpaper.vbs");
                papelDeParede.WaitForExit();//aguarda o termino do processo.
                int saida = papelDeParede.ExitCode;

            }
            catch (Exception ex) { }
        }
        //ATIVAÇÃO===================================================================================================================


        public void TaskKillSysprep()
        {
            try
            {
                Process.Start("taskkill", "/F /IM sysprep.exe");
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        public void TimeReload()
        //Preciso adicionar este time para tirar o bug de abrir o form antes de finalizar o processo.
        {
            Timer relogio = new Timer();
            relogio.Interval = 1000;
            int tempo = 5;
            relogio.Tick += delegate {
                tempo -= 1;
                if (tempo == 0)
                {
                    relogio.Stop();
                    //Reloader
                    Iniciar();
                }
            };
            relogio.Start();
        }

        public void Iniciar()
        {
            CriarLogs();
            Interacao();
            TimeStart1();
            TimeStart2();
        }

        public void TimeStart1()
        //Preciso adicionar este time para tirar o bug de abrir o form antes de finalizar o processo.
        {
            Timer relogio = new Timer();
            relogio.Interval = 1000;
            int tempo = 8;

            relogio.Tick += delegate {
                tempo -= 1;
                lblTime.Text = tempo.ToString();
                if (tempo == 0)
                {
                    relogio.Stop();
                    //Chamar o próximo projeto
                    FrequenciaMonitor();
                    ModeloEquipamento();
                    lblTime.Text = "OK";
                    LeituraSerial();
                    TimeBateria1();
                    //Para enviar os dados de rastreio interno
                    StartFireBaseServices();

                    RoboTeste();
                }
            };
            relogio.Start();
        }

        public void LeituraSerial()
        {
            //Verifica se o Arquivo existe
            const string caminhoArquivoMySql = (@"C:\TESTES_AVELL\MySqlData\SerialNumber.txt");
            if (File.Exists(caminhoArquivoMySql))
            {
                //Se o Arquivo existir, vai le o conteúdo dele
                string[] lines = System.IO.File.ReadAllLines(@"C:\TESTES_AVELL\MySqlData\SerialNumber.txt");
                foreach (string line in lines)
                {
                    //Precisa adicionar um delay
                    lblSerial.Text = (line);
                }
            }
        }

        public void Interacao()
        {
            //https://www.naturalreaders.com/online/ - Cria vozes
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\IniciandoTestes.mp3";
            wplayer.controls.play();
        }

        public void InteracaoEC()
        {
            //https://www.naturalreaders.com/online/ - Cria vozes
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\InstalarEC.mp3";
            wplayer.controls.play();
        }

        public void InteracaoDrivers()
        {
            //https://www.naturalreaders.com/online/ - Cria vozes
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\InstalarDrivers.mp3";
            wplayer.controls.play();
        }

        public void CriarLogs()
        {
            try
            {
                //A Licença está sendo feita em outra etapa, conecta wi - fi, depois ativa a licença!
                //1-Conectar Wi-fi antes de tudo
                ProcessStartInfo ConectarWifi1 = new ProcessStartInfo("cmd.exe", @"/C " + "netsh wlan add profile filename=C:\\TESTES_AVELL\\.executaveisAux\\avell-infra-ultra.xml");
                ConectarWifi1.UseShellExecute = false;
                ConectarWifi1.CreateNoWindow = true;
                Process.Start(ConectarWifi1);

                //2-Conexão com Avell Infra-ultra
                ProcessStartInfo ConectarWifi2 = new ProcessStartInfo("cmd.exe", @"/C " + "netsh wlan connect ssid=avell-infra-ultra name=avell-infra-ultra");
                ConectarWifi2.UseShellExecute = false;
                ConectarWifi2.CreateNoWindow = true;
                Process.Start(ConectarWifi2);

                ////ADICIONADO*******************************************************************************************


                //3.0-Segundo vou mapear a Unidade Z:\
                ProcessStartInfo MapearCMDLic = new ProcessStartInfo("cmd.exe", @"/C " + "@net use Z: \\\\tucuma\\windowssetup$ /user:localhost\\netadmin 8fc6716fa3");
                MapearCMDLic.UseShellExecute = false;
                MapearCMDLic.CreateNoWindow = true;
                Process.Start(MapearCMDLic);

                //3.1==================================================BLOCO DE GERAÇÃO DE LOGS VERIFICAR A NECESSIDADE===============================================================================================================
                //Criar Logs de licenca - Vou chamar a licença em uma outra etapa, mas depois preciso revisar este bloco de logs e licença aqui no inicio
                ProcessStartInfo CriarLogUpk = new ProcessStartInfo("cmd.exe", @"/C " + "@c:\\windows\\system32\\cscript //NoLogo c:\\windows\\system32\\slmgr.vbs /upk >> C:\\users\\administrador\\Desktop\\lic.log");
                CriarLogUpk.UseShellExecute = false;
                CriarLogUpk.CreateNoWindow = true;
                Process.Start(CriarLogUpk);
                //
                ProcessStartInfo CriarLogIpk = new ProcessStartInfo("cmd.exe", @"/C " + "@c:\\windows\\system32\\cscript //NoLogo c:\\windows\\system32\\slmgr.vbs /ipk %Win5x5Key% >> C:\\users\\administrador\\Desktop\\lic.log");
                CriarLogIpk.UseShellExecute = false;
                CriarLogIpk.CreateNoWindow = true;
                Process.Start(CriarLogIpk);
                //
                ProcessStartInfo CriarLogAto = new ProcessStartInfo("cmd.exe", @"/C " + "@c:\\windows\\system32\\cscript //NoLogo c:\\windows\\system32\\slmgr.vbs /ato >> C:\\users\\administrador\\Desktop\\lic.log");
                CriarLogAto.UseShellExecute = false;
                CriarLogAto.CreateNoWindow = true;
                Process.Start(CriarLogAto);
                //=====================================================BLOCO DE GERAÇÃO DE LOGS VERIFICAR A NECESSIDADE================================================================================================================


                //4-Geração de logs de Testes
                //4.1-Gerar Losgs de Testes - Informações de sistemas de onde vai escolher a inicialização de Teclados e itens respectivos das máquinas
                ProcessStartInfo GerarLogSistema = new ProcessStartInfo("cmd.exe", @"/C " + "systeminfo > C:\\TESTES_AVELL\\systemInfo\\sistema.txt");
                GerarLogSistema.UseShellExecute = false;
                GerarLogSistema.CreateNoWindow = true;
                Process.Start(GerarLogSistema);
                //4.2-Gerar Logs Referente frequencia de monitor
                ProcessStartInfo GerarFrequenciaMonitor = new ProcessStartInfo("cmd.exe", @"/C " + "wmic PATH Win32_videocontroller get currentrefreshrate > C:\\TESTES_AVELL\\systemInfo\\FrequenciaMonitor.txt");
                GerarFrequenciaMonitor.UseShellExecute = false;
                GerarFrequenciaMonitor.CreateNoWindow = true;
                Process.Start(GerarFrequenciaMonitor);
                //4.3-Gerar Logs Referentes ao Número de Série da máquina
                ProcessStartInfo GerarNumeroSerie = new ProcessStartInfo("cmd.exe", @"/C " + "wmic bios get serialnumber > C:\\TESTES_AVELL\\MySqlData\\SerialNumber.txt");
                GerarNumeroSerie.UseShellExecute = false;
                GerarNumeroSerie.CreateNoWindow = true;
                Process.Start(GerarNumeroSerie);
                //4.4-Gerar Logs Referente ao Modelo da máquina
                ProcessStartInfo GerarModelo = new ProcessStartInfo("cmd.exe", @"/C " + "wmic computersystem get model > C:\\TESTES_AVELL\\systemInfo\\ModeloMaquina.txt");
                GerarModelo.UseShellExecute = false;
                GerarModelo.CreateNoWindow = true;
                Process.Start(GerarModelo);

                //4.5-Mapear unidade de rede Tucuma para os logs
                //Enviar os logs para Y: Trocar em toda a estrutura
                ProcessStartInfo CompartilhamentoTucuma = new ProcessStartInfo("cmd.exe", @"/C " + "@net use Y: \\\\tucuma\\windowssetup$\\avell-test-2023 /user:localhost\\netadmin 8fc6716fa3");
                CompartilhamentoTucuma.UseShellExecute = false;
                CompartilhamentoTucuma.CreateNoWindow = true;
                Process.Start(CompartilhamentoTucuma);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha ao criar log");
            }
        }


        public void TimeStart2()
        //Preciso adicionar este time para tirar o bug de abrir o form antes de finalizar o processo.
        {
            Timer relogio = new Timer();
            relogio.Interval = 1000;
            int tempo = 10;
            relogio.Tick += delegate {
                tempo -= 1;
                if (tempo == 0)
                {
                    relogio.Stop();
                    //MessageBox.Show("TESTE");
                    WinAudit();
                }
            };
            relogio.Start();
        }

        public void WinAudit()
        {
            try
            {
                //Verificar quantas vezes está sendo gerado os arquivos
                //Copia o arquivo tirando a palavra SerialNumber para dentro de files
                string strFile1 = File.ReadAllText(@"C:\TESTES_AVELL\MySqlData\SerialNumber.txt");
                strFile1 = Regex.Replace(strFile1, @"SerialNumber", "");
                File.WriteAllText(@"C:\TESTES_AVELL\winAudit\files1\SerialNumber.txt", strFile1);


                //Gera o arquivo com o que tem dentro da pasta files1
                List<string> novasLinhas = new List<string>();
                string[] todasAsLinhas = File.ReadAllLines(@"C:\TESTES_AVELL\winAudit\files1\SerialNumber.txt");
                foreach (string linha in todasAsLinhas)
                {
                    if (WinAuditOK != "OK!")
                    {
                        //Procura somente o que começa com AVNB
                        if (linha.Contains("AVNB"))
                        {
                            novasLinhas.Add(linha);
                            //Esse método Gerar o Arquivo
                            ProcessStartInfo GerarWinAudit = new ProcessStartInfo("cmd.exe", @"/C " + "@C:\\TESTES_AVELL\\winAudit\\files1\\WinAudit.exe /r=gsoPxuTUeERNtnzDaIbMpmidcSArCOHG /f=" + linha + ".html /T=datetime");
                            GerarWinAudit.UseShellExecute = false;
                            GerarWinAudit.CreateNoWindow = true;
                            Process.Start(GerarWinAudit);
                            WinAuditOK = "OK!";
                        }
                    }
                }
                TimeStart3();
            }
            catch (Exception ex) { }
        }

        public void TimeStart3()
        //Preciso adicionar este time para tirar o bug de abrir o form antes de finalizar o processo.
        {
            Timer relogio = new Timer();
            relogio.Interval = 1000;
            int tempo = 70;
            relogio.Tick += delegate {
                tempo -= 1;
                if (tempo == 0)
                {
                    relogio.Stop();
                    //Método de copiar entre pastas
                    CopiaArquivos();
                }
            };
            relogio.Start();
        }

        public void CopiaArquivos()
        {
            //Gerar arquivo direto para o WinAudit e não precisar mais disso aqui
            DirectoryInfo CopiaReplace = new DirectoryInfo(@"C:\TESTES_AVELL\winAudit\files1");
            string outputDir = (@"C:\TESTES_AVELL\winAudit");
            foreach (var file in CopiaReplace.GetFiles())
            {
                try
                {
                    //Copia o arquivo tirando o espaço
                    File.Copy(file.FullName, Path.Combine(outputDir, Path.GetFileName(file.FullName).Replace(" ", ""))); 
                }
                catch (Exception ex) { }
            }
            TimeStart4();
        }

        public void TimeStart4()
        //Preciso adicionar este time para tirar o bug de abrir o form antes de finalizar o processo.
        {
            Timer relogio = new Timer();
            relogio.Interval = 1000;
            int tempo = 3;
            relogio.Tick += delegate {
                tempo -= 1;
                if (tempo == 0)
                {
                    relogio.Stop();
                    //Inserir Método
                    EnviarArquivosServer();
                }
            };
            relogio.Start();
        }

        public void EnviarArquivosServer()
        {
            //Copia somente arquivo por extensão
            string sourceDir = @"C:\TESTES_AVELL\winAudit";
            string backupDir = @"Y:\";
            try
            {
                string[] txtList = Directory.GetFiles(sourceDir, "*.html");

                foreach (string f in txtList)
                {
                    try
                    {
                        //Para não dar erros se o arquivo já existir
                        string fName = f.Substring(sourceDir.Length + 1);
                        File.Copy(Path.Combine(sourceDir, fName), Path.Combine(backupDir, fName));
                    }
                    catch (Exception ex) { }
                }
            }
            catch (DirectoryNotFoundException ex) { }
        }

        public void FrequenciaMonitor()
        {
            try
            {
                //Leirura das Informações - Coloquei de carona neste Time
                lblDisplay.Text = File.ReadAllText(@"C:\TESTES_AVELL\systemInfo\FrequenciaMonitor.txt");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não consegui detectar frequencia do Monitor");
            }
        }

        public void ModeloEquipamento()
        {
            try
            {
                lblModelo.Text = File.ReadAllText(@"C:\TESTES_AVELL\systemInfo\ModeloMaquina.txt");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não consegui detectar o modelo da máquina");
            }
        }

        public void RoboTeste()
        {
            //Inserido AvellWeb**********************************************
            InsertAvellWeb();
            //Inserido AvellWeb**********************************************

            if (BURNIN != "OK")
            {
                //Sting para evitar loop!
                BURNIN = "OK";

                ROBOTESTE.ROBOTESTE projetoRoboTeste = new ROBOTESTE.ROBOTESTE();
                projetoRoboTeste.Show();
                MinimizarForm();
            }
        }

        private void timerBateria_Tick(object sender, EventArgs e)
        {
            PowerStatus energia = SystemInformation.PowerStatus;

            pbBateria.Value = (int)(energia.BatteryLifePercent * 100);
            if (energia.BatteryLifeRemaining < 0)
                lblTempoDescarga.Text = "Carregando Bateria: Fonte ligada.";
            else
                lblTempoDescarga.Text = "Tempo Restante: " + new TimeSpan(0, 0, energia.BatteryLifeRemaining);
            lblPercentual.Text = energia.BatteryLifePercent.ToString("P");
        }

        public void CheckBateria2()
        {
            PowerStatus energia = SystemInformation.PowerStatus;

            pbBateria.Value = (int)(energia.BatteryLifePercent * 100);
            if (energia.BatteryLifeRemaining < 0)
                Fonte = "Conectada";
            else
                InformmaFonte();
        }

        public void InformmaFonte()
        {
            FONTEDESCONECTADA formFonteDesconectada = new FONTEDESCONECTADA();
            formFonteDesconectada.Show();
        }
        
        public void TimeBateria1()
        //Preciso adicionar este time para tirar o bug de abrir o form antes de finalizar o processo.
        {
            Timer relogio = new Timer();
            relogio.Interval = 1000;
            int tempo = 2;

            relogio.Tick += delegate {
                tempo -= 1;
                lblTime.Text = tempo.ToString();
                if (tempo == 0)
                {
                    relogio.Stop();
                    timerBateria.Start();

                    CheckBateria2();
                }
            };
            relogio.Start();
        }

        public void TimeBateria2()
        //Preciso adicionar este time para tirar o bug de abrir o form antes de finalizar o processo.
        {
            Timer relogio = new Timer();
            relogio.Interval = 1000;
            int tempo = 2;

            relogio.Tick += delegate {
                tempo -= 1;
                lblTime.Text = tempo.ToString();
                if (tempo == 0)
                {
                    relogio.Stop();
                    //Chamar o próximo projeto
                    timerBateria.Stop();
                    TimeBateria1();
                }
            };
            relogio.Start();
        }

        public void CriarPastaLogs()
        {
            ManagementObjectSearcher mSearcher = new ManagementObjectSearcher("SELECT SerialNumber, ReleaseDate FROM Win32_BIOS");
            ManagementObjectCollection collection = mSearcher.Get();
            foreach (ManagementObject obj in collection)
            {
                //Dll: System.Management.dll - Para conseguir informações da BIOS
                string NomeArquivo = (string)obj["SerialNumber"];
                string pastaLogs = @"C:\TESTES_AVELL\MySqlData\" + NomeArquivo;

                try
                {
                    if (Directory.Exists(pastaLogs))
                    {
                        MessageBox.Show("Esta máquina já possui logos de testes!");
                    }
                    else
                    {
                        DirectoryInfo di = Directory.CreateDirectory(pastaLogs);
                    }
                }
                catch (Exception ex) { }
            }
        }

        public void MinimizarForm()
        {
            if (WindowState != FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        public void TimeStartMax()
        //Preciso adicionar este time para tirar o bug de abrir o form antes de finalizar o processo.
        {
            Timer relogio = new Timer();
            relogio.Interval = 1000;
            int tempo = 10;
            relogio.Tick += delegate {
                tempo -= 1;
                if (tempo == 0)
                {
                    relogio.Stop();
                    MaximizarForm();
                }
            };
            relogio.Start();
        }

        public void MaximizarForm()//Maximização ainda não acionada
        {
            if (WindowState != FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void lblSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InsertAvellWeb()
        {
            //Inserções para AVELLWEB
            try
            {
                //MEMORIA
                Process process1 = new Process();
                process1.StartInfo.FileName = "wmic";
                process1.StartInfo.Arguments = "memorychip get devicelocator, serialnumber";
                process1.StartInfo.UseShellExecute = false;
                process1.StartInfo.RedirectStandardOutput = true;
                process1.StartInfo.CreateNoWindow = true;
                process1.Start();
                string output1 = process1.StandardOutput.ReadToEnd();
                ManagementObjectSearcher MOS1 = new ManagementObjectSearcher("Select * From Win32_BIOS");
                foreach (ManagementObject getserial in MOS1.Get())
                {
                    string SerialAvell1 = getserial["SerialNumber"].ToString();
                    String InfoMemoria = "MEMORIA : " + output1;
                    var teste = new memoria
                    {
                        Serial = SerialAvell1,
                        Memoria = InfoMemoria
                    };
                    FirebaseResponse response = client.Update("WEB_CONSULTA_HW/" + SerialAvell1, teste);
                    break;
                }

                //SSD
                Process process2 = new Process();
                process2.StartInfo.FileName = "wmic";
                process2.StartInfo.Arguments = "diskdrive get model, name, serialnumber";
                process2.StartInfo.UseShellExecute = false;
                process2.StartInfo.RedirectStandardOutput = true;
                process2.StartInfo.CreateNoWindow = true;
                process2.Start();
                string output2 = process2.StandardOutput.ReadToEnd();
                foreach (ManagementObject getserial in MOS1.Get())
                {
                    string SerialAvell2 = getserial["SerialNumber"].ToString();
                    String InfoSsd = "SSD : " + output2;
                    var teste = new ssd
                    {
                        Serial = SerialAvell2,
                        Ssd = InfoSsd
                    };
                    FirebaseResponse response = client.Update("WEB_CONSULTA_HW/" + SerialAvell2, teste);
                    break;
                }

                //REDE
                Process process3 = new Process();
                process3.StartInfo.FileName = "wmic";
                process3.StartInfo.Arguments = "nic where PhysicalAdapter=True get MACAddress, Name";
                process3.StartInfo.UseShellExecute = false;
                process3.StartInfo.RedirectStandardOutput = true;
                process3.StartInfo.CreateNoWindow = true;
                process3.Start();
                string output3 = process3.StandardOutput.ReadToEnd();
                foreach (ManagementObject getserial in MOS1.Get())
                {
                    string SerialAvell3 = getserial["SerialNumber"].ToString();
                    String InfoRede = "REDE : " + output3;
                    var teste = new rede
                    {
                        Serial = SerialAvell3,
                        Rede = InfoRede
                    };
                    FirebaseResponse response = client.Update("WEB_CONSULTA_HW/" + SerialAvell3, teste);
                    break;
                }
            }
            catch (Exception e) { }
            CriarBaseFalha();
        }

        private void CriarBaseFalha()
        {
            try
            {
                var dataHoraMinuto = DateTime.Now.ToString("dd/MM/yyyy - HH:mm");
                ManagementObjectSearcher MOS1 = new ManagementObjectSearcher("Select * From Win32_BIOS");
                foreach (ManagementObject getserial in MOS1.Get())
                {
                    string SerialAvell = getserial["SerialNumber"].ToString();
                    String DadosFirebase1 = "";
                    var teste = new burnin
                    {
                        Serial = SerialAvell,
                        TBurnin = DadosFirebase1
                    };
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTEFALHA/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTEFALHA/" + SerialAvell, teste);
                    SerialAvell = string.Empty;
                    DadosFirebase2 = string.Empty;
                    break;
                }

                foreach (ManagementObject getserial in MOS1.Get())
                {
                    string SerialAvell = getserial["SerialNumber"].ToString();
                    String DadosFirebase3 = "";
                    var teste = new controlc
                    {
                        Serial = SerialAvell,
                        TControl = DadosFirebase3
                    };
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTEFALHA/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTEFALHA/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTEFALHA/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTEFALHA/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTEFALHA/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTEFALHA/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTEFALHA/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTEFALHA/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTEFALHA/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTEFALHA/" + SerialAvell, teste);
                    SerialAvell = string.Empty;
                    DadosFirebase12 = string.Empty;
                    break;
                }
            }
            catch
            {
                //MessageBox.Show("Não foi possivel inserir os dados!");
                lblAvellWeb.Text = "Firebase Off-Line";
                lblAvellWeb.ForeColor = Color.Red;
            }
        }

        private void lblInfoSistema_Click(object sender, EventArgs e)
        {

        }

        private void lblAtualizacao_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblAudio_Click(object sender, EventArgs e)
        {

        }

        private void Sistema_Teste_Load(object sender, EventArgs e)
        {

        }
    }
}