using CONTROL_COOLER;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Windows.Forms;
//https://www.youtube.com/watch?v=mydcHC6379A&t=2s

namespace ROBOTESTE
{
    public partial class ROBOTESTE : Form
    {
        public string BurninTeste;
        public string Coordenada1;
        public string Coordenada2;
        public string Coordenada3;
        public string Validado;
        //String para evitar loop!
        public string TEMPORIZADOR;
        public string CUSTOMCONTROLINSTALADO;

        Click c = new Click();

        public ROBOTESTE()
        {
            InitializeComponent();
            //Tudo começa com o filtro para verificar se a máquina é BON!
            VerificarMMaquina();
            StartFireBaseServices();
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
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\InicioBurnin.mp3";
            wplayer.controls.play();
        }


        public void VerificarMMaquina()
        {
            //Aqui no verificador preciso colocar um filtro em casos de ser B.ON
            SelectQuery tecladoAvell = new SelectQuery("Win32_BIOS");
            ManagementObjectSearcher objOSDetails = new ManagementObjectSearcher(tecladoAvell);
            ManagementObjectCollection osDetailsCollection = objOSDetails.Get();
            foreach (ManagementObject filtroPorBios in osDetailsCollection)
            {
                string selecaoVersao = (filtroPorBios["SMBIOSBIOSVersion"].ToString());
                if (selecaoVersao == "1.07.08TBN1")
                {
                    AbrirPrograma();
                    Interacao();
                }
                if (selecaoVersao == "1.07.06TBN")
                {
                    AbrirPrograma();
                    Interacao();
                }
                if (selecaoVersao == "1.07.07TBN")
                {
                    AbrirPrograma();
                    Interacao();
                }
                if (selecaoVersao == "1.07.08TBN")
                {
                    AbrirPrograma();
                    Interacao();
                }
                if (selecaoVersao == "1.07.09TBN")
                {
                    AbrirPrograma();
                    Interacao();
                }
                if (selecaoVersao == "1.07.10TBN")
                {
                    AbrirPrograma();
                    Interacao();
                }
                //Adicionado Home Office
                if (selecaoVersao == "1.07.09iTBN")
                {
                    AbrirPrograma();
                    Interacao();
                }
                //Adicionado B.ON SMART
                if (selecaoVersao == "1.07.02TBN1")
                {
                    AbrirPrograma();
                    Interacao();
                }
                //Adicionado Home Office
                if (selecaoVersao != "1.07.08TBN1" && selecaoVersao != "1.07.06TBN" && selecaoVersao != "1.07.07TBN" && selecaoVersao != "1.07.09iTBN" && selecaoVersao != "1.07.02TBN1" && selecaoVersao != "1.07.08TBN" && selecaoVersao != "1.07.09TBN" && selecaoVersao != "1.07.10TBN")
                {
                    VerificarCustomControlInst();
                }
                break;
            }
        }

        public void VerificarCustomControlInst()
        {
            String CaminhoPastaCustom = @"C:\Program Files\OEM";
            if (Directory.Exists(CaminhoPastaCustom))
            {
                VerificaQuantLogs();
            }
            else
            {
                InstalarCControl_CCenter();
            }
        }

        public void VerificaQuantLogs()
        {
            //Verifica novamente se a pasta do Custom Control está presente!
            String CaminhoPastaCustom = @"C:\Program Files\OEM";
            if (Directory.Exists(CaminhoPastaCustom))
            {
                //Se a quantidade de lgos for maior que 1, apresentou erros e retestes, senão abre o programa
                var quantidadeLog = Directory.GetFiles(@"C:\TESTES_AVELL\logs_burnin", "*.log", SearchOption.TopDirectoryOnly).Count().ToString();
                int valor = int.Parse(quantidadeLog);
                if (valor < 2)
                {
                    AbrirPrograma();
                    Interacao();
                }
                else if (valor > 1)
                {
                    //Utilizar este método melhorado para chamar todos os outros forms do teste.
                    using (ENVIAREPARO formEnviarReparo = new ENVIAREPARO())
                    {
                        this.Hide();
                        formEnviarReparo.ShowDialog();
                    }
                }
            }
            else
            {
                //Se tudo der errado vai recomeçar, desde a verificação se a máquina
                //Não e um BON Lite, então fará o processo de tentar instalar novamente
                VerificarMMaquina();
            }
        }

        public void InstalarCControl_CCenter()
        {
            if (CUSTOMCONTROLINSTALADO != "OK")
            {
                try
                {
                    Process instalarCustom = System.Diagnostics.Process.Start(@"C:\TESTES_AVELL\.executaveisAux\InstalarCustomControl.exe");
                    instalarCustom.WaitForExit();
                    CUSTOMCONTROLINSTALADO = "OK";

                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 5;

                    relogio.Tick += delegate {
                        tempo -= 5;
                        lblTime.Text = tempo.ToString();
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            AbrirPrograma();
                            //WinVer();
                        }
                    };
                    relogio.Start();
                }
                catch (Exception ex)
                {
                    //Verificar se o Custom Control está instalado no próximo teste.
                    MessageBox.Show("NÃO FOI POSSÍVEL INSTALAR O CUSTOM");
                }
            }
        }

        //Verificando o loop************************************************************************
        public void AbrirPrograma()
        {
            if (BurninTeste != "Active1")
            //Para não entrar em loop
            {
                try
                {
                    //Sincronizar data e hora antes de startar o burnin-teste
                    setTime();
                    var startInfo = new ProcessStartInfo(@"C:\TESTES_AVELL\burnin\bit.exe");
                    startInfo.WindowStyle = ProcessWindowStyle.Maximized;
                    Process.Start(startInfo);
                    TimeStart();

                }
                catch (Exception e)
                {
                    BurninTeste = "Active1";
                    SemBurnin();
                }
            }
        }

        public void TimeStart()
        //Preciso adicionar este time para tirar o bug de abrir o form antes de finalizar o processo.
        {
            if (BurninTeste != "Active1")
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

                        //Não utiliza mais, agora só tem Windows 11
                        //WinVer();
                        ClickBurnin_W11();
                    }
                };
                relogio.Start();
            }
        }

        public void SemBurnin()
        {
            AVISOBURNIN formSemBurnin = new AVISOBURNIN();
            this.Hide();
            formSemBurnin.ShowDialog();
        }

        public void setTime()
        {
            try
            {
                Process sincronizarhora = System.Diagnostics.Process.Start(@"C:\TESTES_AVELL\.executaveisAux\SincronizarHoraServer.exe");
                sincronizarhora.WaitForExit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "Não foi possível encontrar o Sincronizador de Data e Hora!");
            }
        }

        //public void WinVer()
        //{
        //    // Consulta para obter informações sobre o sistema operacional
        //    ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
        //    ManagementObjectCollection information = searcher.Get();

        //    foreach (ManagementObject obj in information)
        //    {
        //        string edition = obj["Caption"].ToString();
        //        string Win10 = "Microsoft Windows 10 Home Single Language";

        //        if (edition == Win10)
        //        {
        //            //ClicBurnin_W10();
        //        }
        //        else
        //        {
        //            ClicBurnin_W11();
        //        }
        //    }
        //}

        /// <summary>
        /// WINDOWS_11
        /// </summary>
        public void ClickBurnin_W11()
        {
            /// <summary>
            /// MELHORIA PARA REDUÇÃO DE CÓDIGO
            /// </summary>
            /// 
            BurninTeste = "Active1";
            SelectQuery tecladoAvell = new SelectQuery("Win32_BIOS");
            ManagementObjectSearcher objOSDetails = new ManagementObjectSearcher(tecladoAvell);
            ManagementObjectCollection osDetailsCollection = objOSDetails.Get();
            foreach (ManagementObject filtroPorBios in osDetailsCollection)
            {
                string selecaoVersao = (filtroPorBios["SMBIOSBIOSVersion"].ToString());

                if (selecaoVersao == "1.07.11TBN1")
                {
                    lblModelo.Text = "A52-MOB";
                    Validado = "OK";
                    Coordenadas8_W11();
                }

                if (selecaoVersao == "N.1.05AVE04")
                {
                    lblModelo.Text = "A52-MOB";
                    Validado = "OK";
                    Coordenadas2_W11();
                }

                if (selecaoVersao == "N.1.23AVE02")
                {
                    lblModelo.Text = "BIOS: N.1.23AVE02";
                    Validado = "OK";
                    Coordenadas4_W11();
                }
                if (selecaoVersao == "N.1.22AVE00")
                {
                    lblModelo.Text = "BIOS: N.1.22AVE00";
                    Validado = "OK";
                    Coordenadas4_W11();
                }

                if (selecaoVersao == "N.1.13AVE01")
                {
                    lblModelo.Text = "BIOS: N.1.13AVE01";
                    Validado = "OK";
                    Coordenadas2_W11();
                }

                if (selecaoVersao == "N.1.10AVE01")
                {
                    lblModelo.Text = "A70-ION / A72-ION ";
                    Validado = "OK";
                    Coordenadas4_W11();
                }

                if (selecaoVersao == "N.1.08AVE04")
                {
                    lblModelo.Text = "A52-HYB NEW / STORM BS";
                    Validado = "OK";
                    Coordenadas5_W11();
                }

                if (selecaoVersao == "N.1.10AVE03")
                {
                    lblModelo.Text = "A70HYB / STORM 2";
                    Validado = "OK";
                    Coordenadas3_W11();
                }

                if (selecaoVersao == "N.1.10AVE04")
                {
                    lblModelo.Text = "A70HYB / STORM 2";
                    Validado = "OK";
                    Coordenadas3_W11();
                }

                if (selecaoVersao == "N.1.13AVE05")
                {
                    lblModelo.Text = "A70HYB / STORM 2";
                    Validado = "OK";
                    Coordenadas3_W11();
                }

                if (selecaoVersao == "N.1.07AVE02")
                {
                    lblModelo.Text = "A70HYB";
                    Validado = "OK";
                    Coordenadas9_W11();
                }

                if (selecaoVersao == "N.1.09AVE02")
                {
                    lblModelo.Text = "A70HYB / 480 / 490";
                    Validado = "OK";
                    Coordenadas6_W11();
                }

                if (selecaoVersao == "N.1.04AVE00")
                {
                    lblModelo.Text = "A70MOB 3050";
                    Validado = "OK";
                    Coordenadas3_W11();
                }

                if (selecaoVersao == "N.1.07AVE00")
                {
                    lblModelo.Text = "A70MOB / A72LIV / A70i / 460 / 470";
                    Validado = "OK";
                    Coordenadas1_W11();
                }

                if (selecaoVersao == "N.1.07AVE01")
                {
                    lblModelo.Text = "A70MOB";
                    Validado = "OK";
                    Coordenadas1_W11();
                }

                if (selecaoVersao == "1.07.08TBN1")
                {
                    lblModelo.Text = "B.ON LITE";
                    Validado = "OK";
                    Coordenadas0_W11();
                }

                if (selecaoVersao == "1.07.06TBN")
                {
                    lblModelo.Text = "B.ON LITE";
                    Validado = "OK";
                    Coordenadas0_W11();
                }

                if (selecaoVersao == "1.07.07TBN")
                {
                    lblModelo.Text = "B.ON LITE";
                    Validado = "OK";
                    Coordenadas0_W11();
                }
                if (selecaoVersao == "1.07.08TBN")
                {
                    lblModelo.Text = "B.ON LITE";
                    Validado = "OK";
                    Coordenadas0_W11();
                }
                if (selecaoVersao == "1.07.09TBN")
                {
                    lblModelo.Text = "B.ON LITE";
                    Validado = "OK";
                    Coordenadas0_W11();
                }
                if (selecaoVersao == "1.07.10TBN")
                {
                    lblModelo.Text = "B.ON LITE";
                    Validado = "OK";
                    Coordenadas0_W11();
                }

                //Adicionado HomeOffice
                if (selecaoVersao == "1.07.09iTBN")
                {
                    lblModelo.Text = "B.ON LITE";
                    Validado = "OK";
                    Coordenadas0_W11();
                }
                //Adicionado HomeOffice
                
                if (selecaoVersao == "N.1.09AVE02")
                {
                    lblModelo.Text = "STORM GO";
                    Validado = "OK";
                    Coordenadas6_W11();
                }
                
                if (selecaoVersao == "N.1.01")
                {
                    lblModelo.Text = "C65 LIV";
                    Validado = "OK";
                    Coordenadas6_W11();
                }

                if (selecaoVersao == "N.1.01AVE00")
                {
                    lblModelo.Text = "B11 MOB";
                    Validado = "OK";
                    Coordenadas11_W11();
                }
                if (selecaoVersao == "BM_BI_IDL819-10_150A_N")
                {
                    lblModelo.Text = "145";
                    Validado = "OK";
                    Coordenadas11_W11();
                }
                if (selecaoVersao == "BM_BI_IDL819-10_150B_N")
                {
                    lblModelo.Text = "147";
                    Validado = "OK";
                    Coordenadas11_W11();
                }

                if (selecaoVersao == "N.1.09AVE01")
                {
                    lblModelo.Text = "A65ION";
                    Validado = "OK";
                    Coordenadas6_W11();
                }

                if (selecaoVersao == "N.1.03")
                {
                    lblModelo.Text = "A62 LIV";
                    Validado = "OK";
                    Coordenadas7_W11();
                }
                
                if (selecaoVersao == "N.1.02")
                {
                    lblModelo.Text = "A62 LIV";
                    Validado = "OK";
                    Coordenadas7_W11();
                }
                if (selecaoVersao == "N.1.04Ave01")
                {
                    lblModelo.Text = "A52i / 350 / 450";
                    Validado = "OK";
                    Coordenadas3_W11();
                }
                if (selecaoVersao == "N.1.24AVE00")
                {
                    lblModelo.Text = "A52r / 350r / 450r";
                    Validado = "OK";
                    Coordenadas3_W11();
                }
                if (selecaoVersao == "1.07.02TBN1")
                {
                    lblModelo.Text = "Smart";
                    Validado = "OK";
                    Coordenadas0_W11();
                }

                if (selecaoVersao != "1.07.11TBN1" && selecaoVersao != "N.1.10AVE03" && selecaoVersao != "N.1.05AVE04" &&
                    selecaoVersao != "N.1.22AVE00" && selecaoVersao != "N.1.13AVE01" && selecaoVersao != "N.1.08AVE00" &&
                    selecaoVersao != "N.1.08AVE04" && selecaoVersao != "N.1.10AVE04" && selecaoVersao != "N.1.13AVE05" &&
                    selecaoVersao != "N.1.07AVE02" && selecaoVersao == "N.1.09AVE02" && selecaoVersao != "N.1.04AVE00" &&
                    selecaoVersao != "N.1.07AVE00" && selecaoVersao != "N.1.07AVE01" && selecaoVersao != "1.07.06TBN" &&
                    selecaoVersao != "1.07.08TBN1" && selecaoVersao != "N.1.09AVE00" && selecaoVersao != "N.1.04Ave01" && selecaoVersao != "1.07.08TBN" && selecaoVersao != "1.07.09TBN" && selecaoVersao != "1.07.10TBN" &&
                    selecaoVersao != "N.1.01AVE00" && selecaoVersao != "N.1.09AVE01" && selecaoVersao != "N.1.03" && selecaoVersao != "BM_BI_IDL819-10_150A_N" && selecaoVersao != "BM_BI_IDL819-10_150B_N" &&
                    selecaoVersao != "N.1.02" && selecaoVersao != "N.1.01" && selecaoVersao != "N.1.24AVE00" && selecaoVersao != "1.07.02TBN1" && selecaoVersao != "N.1.10AVE01" && selecaoVersao != "N.1.23AVE02")
                {

                    if (Validado != "OK")
                    {
                        NAOCADASTRADA formNaoCadastrada = new NAOCADASTRADA();
                        this.Hide();
                        formNaoCadastrada.Show();
                    }
                }
                break;
            }
        }
        
        /// <summary>
        /// AQUI É A OTIMIZAÇÃO PARA COORDENADAS INCOMUM PARA MÁQUINAS COM WINDOWS11
        /// WINDOWS11=====WINDOWS11=====WINDOWS11=====WINDOWS11=====WINDOWS11=====WINDOWS11
        /// </summary>
        /// 
        private void Coordenadas0_W11()
        {
            try
            {
                if (Coordenada1 != "OK")
                {
                    Coordenada1 = "OK";
                    Point p = new Point();
                    p.X = 345;
                    p.Y = 60;
                    c.leftClick(p);
                }
                if (Coordenada2 != "OK")
                {
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 10;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada2 = "OK";
                            Point p = new Point();
                            p.X = 835;
                            p.Y = 510;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                Temporizador();
            }
            catch (Exception ex) { MessageBox.Show("Não Consegui executar a inicialização do Burnin" + ex); }
        }

        private void Coordenadas1_W11()
        {
            try
            {
                if (Coordenada1 != "OK")
                {
                    Coordenada1 = "OK";
                    Point p = new Point();
                    p.X = 340;
                    p.Y = 55;
                    c.leftClick(p);
                }
                if (Coordenada2 != "OK")
                {
                    //Coordenada3 = "OK";
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 10;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada2 = "OK";
                            Point p = new Point();
                            p.X = 920;
                            p.Y = 570;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                Temporizador();
            }
            catch (Exception ex) { MessageBox.Show("Não Consegui executar a inicialização do Burnin" + ex); }
        }

        private void Coordenadas2_W11()
        {
            try
            {
                if (Coordenada1 != "OK")
                {
                    Coordenada1 = "OK";
                    Point p = new Point();
                    p.X = 340;
                    p.Y = 60;
                    c.leftClick(p);
                }
                if (Coordenada2 != "OK")
                {
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 10;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada2 = "OK";
                            Point p = new Point();
                            p.X = 835;
                            p.Y = 510;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                Temporizador();
            }
            catch (Exception ex) { MessageBox.Show("Não Consegui executar a inicialização do Burnin" + ex); }
        }

        private void Coordenadas3_W11()
        {
            try
            {
                if (Coordenada1 != "OK")
                {
                    Coordenada1 = "OK";
                    Point p = new Point();
                    p.X = 340;
                    p.Y = 60;
                    c.leftClick(p);
                }
                if (Coordenada2 != "OK")
                {
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 10;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada2 = "OK";
                            Point p = new Point();
                            p.X = 835;
                            p.Y = 514;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                if (Coordenada3 != "OK")
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
                            Coordenada3 = "OK";
                            Point p = new Point();
                            p.X = 925;
                            p.Y = 570;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                Temporizador();
            }
            catch (Exception ex) { MessageBox.Show("Não Consegui executar a inicialização do Burnin" + ex); }
        }
        //Ajustando...
        private void Coordenadas4_W11()
        {
            try
            {
                if (Coordenada1 != "OK")
                {
                    Coordenada1 = "OK";
                    Point p = new Point();
                    p.X = 340;
                    p.Y = 60;
                    c.leftClick(p);
                }
                if (Coordenada2 != "OK")
                {
                    //Coordenada3 = "OK";
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 10;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada2 = "OK";
                            Point p = new Point();
                            p.X = 920;
                            p.Y = 625;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                Temporizador();
            }
            catch (Exception ex) { MessageBox.Show("Não Consegui executar a inicialização do Burnin" + ex); }
        }

        private void Coordenadas5_W11()
        {
            try
            {
                if (Coordenada1 != "OK")
                {
                    Coordenada1 = "OK";
                    Point p = new Point();
                    p.X = 345;
                    p.Y = 58;
                    c.leftClick(p);
                }
                if (Coordenada2 != "OK")
                {
                    Coordenada2 = "OK";
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 10;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Point p = new Point();
                            p.X = 835;
                            p.Y = 514;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                Temporizador();
            }
            catch (Exception ex) { MessageBox.Show("Não Consegui executar a inicialização do Burnin" + ex); }
        }

        private void Coordenadas6_W11()
        {
            try
            {
                if (Coordenada1 != "OK")
                {
                    Coordenada1 = "OK";
                    Point p = new Point();
                    p.X = 340;
                    p.Y = 55;
                    c.leftClick(p);
                }
                if (Coordenada2 != "OK")
                {
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 10;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada2 = "OK";
                            Point p = new Point();
                            p.X = 920;
                            p.Y = 620;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                Temporizador();
            }
            catch (Exception ex) { MessageBox.Show("Não Consegui executar a inicialização do Burnin" + ex); }
        }

        private void Coordenadas7_W11()
        {
            try
            {
                if (Coordenada1 != "OK")
                {
                    Coordenada1 = "OK";
                    Point p = new Point();
                    p.X = 340;
                    p.Y = 55;
                    c.leftClick(p);
                }
                if (Coordenada2 != "OK")
                {
                    //Coordenada3 = "OK";
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 10;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada2 = "OK";
                            Point p = new Point();
                            p.X = 835;
                            p.Y = 510;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                Temporizador();
            }
            catch (Exception ex) { MessageBox.Show("Não Consegui executar a inicialização do Burnin" + ex); }
        }

        private void Coordenadas8_W11()
        {
            try
            {
                if (Coordenada1 != "OK")
                {
                    Coordenada1 = "OK";
                    Point p = new Point();
                    p.X = 345;
                    p.Y = 55;
                    c.leftClick(p);
                }
                if (Coordenada2 != "OK")
                {
                    //Coordenada3 = "OK";
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 10;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo < 0)
                        {
                            relogio.Stop();
                            Coordenada2 = "OK";
                            Point p = new Point();
                            p.X = 830;
                            p.Y = 510;
                            c.leftClick(p);
                        }

                    };
                    relogio.Start();
                }
                Temporizador();
            }
            catch (Exception ex) { MessageBox.Show("Não Consegui executar a inicialização do Burnin" + ex); }
        }

        private void Coordenadas9_W11()
        {
            try
            {
                if (Coordenada1 != "OK")
                {
                    Coordenada1 = "OK";
                    Point p = new Point();
                    p.X = 340;
                    p.Y = 60;
                    c.leftClick(p);
                }
                if (Coordenada2 != "OK")
                {
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 10;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada2 = "OK";
                            Point p = new Point();
                            p.X = 835;
                            p.Y = 515;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                Temporizador();
            }
            catch (Exception ex) { MessageBox.Show("Não Consegui executar a inicialização do Burnin" + ex); }
        }

        private void Coordenadas10_W11()
        {
            try
            {
                if (Coordenada1 != "OK")
                {
                    Coordenada1 = "OK";
                    Point p = new Point();
                    p.X = 340;
                    p.Y = 60;
                    c.leftClick(p);
                }
                if (Coordenada2 != "OK")
                {
                    //Coordenada3 = "OK";
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 20;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada2 = "OK";
                            Point p = new Point();
                            p.X = 920;
                            p.Y = 580;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                Temporizador();
            }
            catch (Exception ex) { MessageBox.Show("Não Consegui executar a inicialização do Burnin" + ex); }
        }

        private void Coordenadas11_W11()
        {
            try
            {
                if (Coordenada1 != "OK")
                {
                    Coordenada1 = "OK";
                    Point p = new Point();
                    p.X = 335;
                    p.Y = 55;
                    c.leftClick(p);
                }
                if (Coordenada2 != "OK")
                {
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 10;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada2 = "OK";
                            Point p = new Point();
                            p.X = 788;
                            p.Y = 530;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                Temporizador();
            }
            catch (Exception ex) { MessageBox.Show("Não Consegui executar a inicialização do Burnin" + ex); }
        }
        /// <summary>
        /// AQUI É A OTIMIZAÇÃO PARA COORDENADAS INCOMUM PARA MÁQUINAS COM WINDOWS11
        /// WINDOWS11=====WINDOWS11=====WINDOWS11=====WINDOWS11=====WINDOWS11=====WINDOWS11
        /// </summary>

        public void Temporizador()
        {
            if (TEMPORIZADOR != "OK")
            {
                TEMPORIZADOR = "OK";
                TEMPORIZADOR formTemporizador = new TEMPORIZADOR();
                this.Hide();
                formTemporizador.ShowDialog();
            }
        }

        private void llabelSair_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        //Firebase AvellWeb
        public void InsertAvellWeb()
        {
            try
            {
                var dataHoraMinuto = DateTime.Now.ToString("dd/MM/yyyy - HH:mm");
                ManagementObjectSearcher MOS1 = new ManagementObjectSearcher("Select * From Win32_BIOS");
                foreach (ManagementObject getserial in MOS1.Get())
                {
                    string SerialAvell = getserial["SerialNumber"].ToString();
                    String DadosFirebase1 = "Burnin Início:" + dataHoraMinuto;
                    var teste = new burnin
                    {
                        Serial = SerialAvell,
                        TBurnin = DadosFirebase1
                    };
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTE/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTE/" + SerialAvell, teste);
                    SerialAvell = string.Empty;
                    DadosFirebase2 = string.Empty;
                    break;
                }

                foreach (ManagementObject getserial in MOS1.Get())
                {
                    string SerialAvell = getserial["SerialNumber"].ToString();
                    String DadosFirebase3 = "";
                    var teste = new Control
                    {
                        Serial = SerialAvell,
                        TControl = DadosFirebase3
                    };
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTE/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTE/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTE/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTE/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTE/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTE/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTE/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTE/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTE/" + SerialAvell, teste);
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
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTE/" + SerialAvell, teste);
                    SerialAvell = string.Empty;
                    DadosFirebase12 = string.Empty;
                    break;
                }
            }
            catch
            {
                //MessageBox.Show("Não foi possivel inserir os dados!");
                lblInformacao1.Text = "Firebase Off-Line";
                lblInformacao1.ForeColor = Color.Red;
            }
        }
    }
}