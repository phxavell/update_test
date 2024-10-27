using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
using System.Windows.Forms;

namespace CONTROL_COOLER
{
    public partial class COOLERS2 : MaterialSkin.Controls.MaterialForm
    {
        public string ControlCooler;
        public string Coordenada1;
        public string Coordenada2;
        public string Coordenada3;
        public string Coordenada4;

        public string Coolers2;

        Click c = new Click();

        public COOLERS2()
        {
            InitializeComponent();
            StartFireBaseServices();
            Color1();
            StartCustomControl();
            InteracaoGPU_CPU();
        }

        public void InteracaoGPU_CPU()
        {
            //https://www.naturalreaders.com/online/ - Cria vozes
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\ControlCoolers.mp3";
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

        public void Color1()
        {
            try
            {
                Timer relogio = new Timer();
                relogio.Interval = 1000;
                int tempo = 2;

                relogio.Tick += delegate {
                    tempo -= 1;
                    //lblTime.Text = tempo.ToString();
                    if (tempo == 0)
                    {
                        relogio.Stop();

                        lblInfomacao.ForeColor = Color.Red;

                        Color2();
                    }
                };
                relogio.Start();
            }
            catch (Exception ex) { }
        }

        public void Color2()
        {
            try
            {
                Timer relogio = new Timer();
                relogio.Interval = 1000;
                int tempo = 2;

                relogio.Tick += delegate {
                    tempo -= 1;
                    //lblTime.Text = tempo.ToString();
                    if (tempo == 0)
                    {
                        relogio.Stop();

                        lblInfomacao.ForeColor = Color.Orange;
                        Color1();
                    }
                };
                relogio.Start();
            }
            catch (Exception ex) { }
        }

        //Esse timer inicializa todo o processo!
        public void StartCustomControl()
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

                        VerificarOpenCustom();
                    }
                };
                relogio.Start();
            }
            catch (Exception ex) { }
        }

        public void VerificarOpenCustom()
        {
            if (Process.GetProcessesByName("GamingCenter3_Cross").Length > 0)
            //if (Process.GetProcessesByID("Avell").Length > 0)
            {
                MinimizarForm();
                TimeCustomControl();
            }
            else
            {
                AbrirCustomControl();
                //Volta para checar novamente
                StartCustomControl();
            }
        }
        /*
        public void AbrirCustomControl()
        {
            //Verificar se o atalho do Custom Control está instalado
            String CaminhoAtalhoCustom = @"C:\Users\Public\Desktop\Avell Custom.lnk";

            if (File.Exists(CaminhoAtalhoCustom))
            {
                try
                {
                    //Primeiro tenta encontrar pelo atalho
                    Process CustomControl = new Process();
                    string CaminhoCustomControl = @"C:\Users\Public\Desktop\Avell Custom.lnk";
                    CustomControl.StartInfo.FileName = CaminhoCustomControl;
                    CustomControl.Start();
                    //CustomControl.WaitForExit();--Não esperar para sair
                    TimeCustomControl();
                }
                catch (Exception ex) { }
            }
            else
            {
                //Se não conseguir abrir no atalho, vai tentar abrir diretamente pelo executável
                try
                {
                    Process CustomControl = new Process();
                    string CaminhoCustomControl = @"C:\Program Files\OEM\Avell Custom\GamingCenter\ControlCenterU.exe";
                    CustomControl.StartInfo.FileName = CaminhoCustomControl;
                    CustomControl.Start();

                    //CustomControl.WaitForExit();--Não esperar para sair
                    TimeCustomControl();
                }
                catch (Exception ex) { }
            }
        }
        */

        public void AbrirCustomControl()
        {
            // Verificar primeiro se o atalho Avell Custom.lnk está instalado
            string caminhoAtalhoCustom = @"C:\Users\Public\Desktop\Avell Custom.lnk";
            string caminhoAtalhoControl = @"C:\Users\Public\Desktop\Avell Custom Control.lnk";

            string caminhoAtalhoEncontrado = null;

            if (File.Exists(caminhoAtalhoCustom))
            {
                caminhoAtalhoEncontrado = caminhoAtalhoCustom;
            }
            else if (File.Exists(caminhoAtalhoControl))
            {
                caminhoAtalhoEncontrado = caminhoAtalhoControl;
            }

            // Se encontrou algum atalho válido
            if (caminhoAtalhoEncontrado != null)
            {
                try
                {
                    // Cria um processo para abrir o custom control
                    Process customControlProcess = new Process();
                    customControlProcess.StartInfo.FileName = caminhoAtalhoEncontrado;
                    customControlProcess.Start();
                    TimeCustomControl(); // Função que você chamou para aguardar um período de tempo
                }
                catch (Exception ex)
                {
                    // Tratamento de exceção, se necessário
                    Console.WriteLine("Erro ao tentar abrir o custom control pelo atalho: " + ex.Message);
                }
            }
            else
            {
                // Se nenhum dos atalhos foi encontrado, tenta abrir diretamente pelo executável
                string caminhoExecutavel = @"C:\Program Files\OEM\Avell Custom\GamingCenter\ControlCenterU.exe";

                try
                {
                    Process customControlProcess = new Process();
                    customControlProcess.StartInfo.FileName = caminhoExecutavel;
                    customControlProcess.Start();
                    TimeCustomControl(); // Função que você chamou para aguardar um período de tempo
                }
                catch (Exception ex)
                {
                    // Tratamento de exceção, se necessário
                    Console.WriteLine("Erro ao tentar abrir o custom control pelo executável: " + ex.Message);
                }
            }
        }

        //Tempo para dar tempo de abrir o Custom Control
        public void TimeCustomControl()
        {
            try
            {
                Timer relogio = new Timer();
                relogio.Interval = 1000;
                int tempo = 15;

                relogio.Tick += delegate {
                    tempo -= 1;
                    lblTime.Text = tempo.ToString();
                    if (tempo == 0)
                    {
                        relogio.Stop();

                        //Verificar a versão do Windows
                        //WinVer(); - Não precisa mais desta opçao

                        ClickCControl_W11();
                    }
                };
                relogio.Start();
            }
            catch (Exception ex) { }
        }

        //*******************************************************************
        public void SelecionarCustomControl()
        {//AJUSTAR COM WINHANDLER
            try
            {
                Timer relogio = new Timer();
                relogio.Interval = 1000;
                int tempo = 3;
                relogio.Tick += delegate
                {
                    tempo -= 1;
                    if (tempo == 0)
                    {
                        //Windows10
                        relogio.Stop();
                        Point p = new Point();
                        p.X = 250;
                        p.Y = 250;
                        c.leftClick(p);
                    }
                };
                relogio.Start();
            }catch (Exception ex) { }
        }
        //*******************************************************************


        //public void WinVer()//Agora só há Windows 11
        //{
        //    // Consulta para obter informações sobre o sistema operacional
        //    ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
        //    ManagementObjectCollection information = searcher.Get();

        //    foreach (ManagementObject obj in information)
        //    {
        //        string edition = obj["Caption"].ToString();
        //        lblWinVer.Text = ($"Versão: {edition}");

        //        string Win10 = "Microsoft Windows 10 Home Single Language";
                
        //        if ( edition == Win10 )
        //        {
        //            //ClickCControl_W10();
        //        }
        //        else
        //        {
        //            //ClickCControl_W11();
        //        }
        //    }
        //}


        //================================================== WINDOWS 11 ==================================================
        public void ClickCControl_W11()
        {
            ControlCooler = "Active1";

            SelectQuery tecladoAvell = new SelectQuery("Win32_BIOS");
            ManagementObjectSearcher objOSDetails = new ManagementObjectSearcher(tecladoAvell);
            ManagementObjectCollection osDetailsCollection = objOSDetails.Get();
            foreach (ManagementObject filtroPorBios in osDetailsCollection)
            {
                string selecaoVersao = (filtroPorBios["SMBIOSBIOSVersion"].ToString());

                if (selecaoVersao == "1.07.11TBN1")
                {
                    lblModelo.Text = "BIOS: 1.07.11TBN1";
                    //SelecionarCustomControl();
                    CUSTOM_A55HYB_W11();
                }
                if (selecaoVersao == "1.07.02TBN1")
                {
                    lblModelo.Text = "BIOS: 1.07.02TBN1";
                    //B.ON SMART 2024;
                    CUSTOM_A55HYB_W11();
                }

                if (selecaoVersao == "N.1.07AVE01")//*********************
                {
                    lblModelo.Text = "BIOS: N.1.07AVE01";
                    //SelecionarCustomControl();
                    CUSTOM_A70MOB_W11();
                }

                if (selecaoVersao == "N.1.05AVE04")
                {
                    lblModelo.Text = "BIOS: N.1.05AVE04";
                    //SelecionarCustomControl();
                    CUSTOM_A52MOB_W11();
                }

                if (selecaoVersao == "N.1.22AVE00")
                {
                    lblModelo.Text = "BIOS: N.1.22AVE00";
                    //SelecionarCustomControl();
                    CUSTOM_STORMX_W11();
                }

                if (selecaoVersao == "N.1.13AVE01")
                {
                    lblModelo.Text = "BIOS: N.1.13AVE01";
                    //SelecionarCustomControl();
                    CUSTOM_A52ION_STORMBS_W11();
                }

                if (selecaoVersao == "N.1.10AVE01")
                {
                    lblModelo.Text = "A 70 / A72-ION";
                    //SelecionarCustomControl();
                    CUSTOM_A72ION_W11();
                }

                if (selecaoVersao == "N.1.08AVE04")
                {
                    lblModelo.Text = "A52-HYB NEW / STORM BS";
                    //SelecionarCustomControl();
                    CUSTOM_A52HYBNEW_W11();
                }

                if (selecaoVersao == "N.1.10AVE03")
                {
                    lblModelo.Text = "A70HYB / STORM 2";
                    //SelecionarCustomControl();
                    CUSTOM_A70HYB_STORM2_W11();
                }
                
                if (selecaoVersao == "N.1.10AVE04")
                {
                    lblModelo.Text = "A70HYB 15''/ STORM 2 17''";
                    //SelecionarCustomControl();
                    CUSTOM_A70HYB_STORM2_W11();
                }

                if (selecaoVersao == "N.1.13AVE05")
                {
                    lblModelo.Text = "A70HYB / STORM 2";
                    //SelecionarCustomControl();
                    CUSTOM_A70HYB_STORM2_W11();
                }

                if (selecaoVersao == "N.1.07AVE02")
                {
                    lblModelo.Text = "A70HYB";
                    //SelecionarCustomControl();
                    CUSTOM_A70HYB_3050_W11();
                }

                if (selecaoVersao == "N.1.09AVE02")
                {
                    lblModelo.Text = "A70HYB / 480 / 490";
                    //SelecionarCustomControl();
                    CUSTOM_A70HYB_3060_W11();
                }

                if (selecaoVersao == "N.1.04AVE00")
                {
                    lblModelo.Text = "A70MOB 3050";
                    //SelecionarCustomControl();
                    CUSTOM_A70MOB_3050_W11();
                }

                if (selecaoVersao == "N.1.07AVE00")
                {
                    lblModelo.Text = "A70MOB / LIV / 460 / 470";
                    //SelecionarCustomControl();
                    CUSTOM_A70MOB_W11();
                }

                if (selecaoVersao == "1.07.06TBN")
                {
                    lblModelo.Text = "B.ON LITE";
                    //SelecionarCustomControl();
                    CUSTOM_BONLITE_W11();
                }

                if (selecaoVersao == "N.1.09AVE00")
                {
                    lblModelo.Text = "STORM GO - A65ION";
                    //SelecionarCustomControl();
                    CUSTOM_STORMGO_W11();
                }

                if (selecaoVersao == "N.1.01")
                {
                    lblModelo.Text = "C65 LIV STORMGO";
                    //SelecionarCustomControl();
                    CUSTOM_STORMGO_W11();
                }

                if (selecaoVersao == "N.1.01AVE00")
                {
                    lblModelo.Text = "B11 MOB";
                    //SelecionarCustomControl();
                    CUSTOM_B11MOB_W11();
                }

                if (selecaoVersao == "N.1.09AVE01")
                {
                    lblModelo.Text = "A65ION";
                    //SelecionarCustomControl();
                    CUSTOM_A65_W11();
                }

                if (selecaoVersao == "N.1.03")
                {
                    lblModelo.Text = "A62 LIV";
                    //SelecionarCustomControl();
                    CUSTOM_A62_W11();
                }

                if (selecaoVersao == "N.1.02")
                {//MÁQUINA NÃO MAIS PRODUZIDA
                    lblModelo.Text = "A62 LIV";
                    //SelecionarCustomControl();
                    CUSTOM_A62_W11();
                }
                if (selecaoVersao == "N.1.04Ave01")
                {
                    lblModelo.Text = "A52i / 350 / 450";
                    //SelecionarCustomControl();
                    CUSTOM_A52HYBNEW_W11();
                }
                if (selecaoVersao == "N.1.24AVE00")
                {
                    lblModelo.Text = "A52r / 350r / 450r";
                    //SelecionarCustomControl();
                    CUSTOM_A52HYBNEW_W11();
                }
                // JÁ TEM ESSA VERSAO DE BIOS, DEIXAR COMENTADO PARA CASO ATUALIZE OS NOVOS STORMS 460 E 470
                /*
                if (selecaoVersao == "N.1.07AVE00")
                {
                    lblModelo.Text = "460 / 470";
                    //SelecionarCustomControl();
                    CUSTOM_STORMGO_W11();
                }
                if (selecaoVersao == "N.1.09AVE02")
                {
                    lblModelo.Text = "480 / 490";
                    //SelecionarCustomControl();
                    CUSTOM_A70HYB_3060_W11();
                }
                */
                if (selecaoVersao != "1.07.11TBN1" && selecaoVersao != "N.1.07AVE01" && selecaoVersao != "N.1.10AVE03" &&
                    selecaoVersao != "N.1.05AVE04" && selecaoVersao != "N.1.22AVE00" && selecaoVersao != "N.1.13AVE01" &&
                    selecaoVersao != "N.1.10AVE01" && selecaoVersao != "N.1.08AVE04" && selecaoVersao != "N.1.10AVE04" &&
                    selecaoVersao != "N.1.13AVE05" && selecaoVersao != "N.1.07AVE02" && selecaoVersao != "N.1.09AVE02" &&
                    selecaoVersao != "N.1.04AVE00" && selecaoVersao != "N.1.07AVE00" && selecaoVersao != "1.07.06TBN" &&
                    selecaoVersao != "N.1.09AVE00" && selecaoVersao != "N.1.01" && selecaoVersao != "N.1.01AVE00" &&
                    selecaoVersao != "N.1.09AVE01" && selecaoVersao != "N.1.03" && selecaoVersao != "N.1.02" &&
                    selecaoVersao != "N.1.04Ave01" && selecaoVersao != "N.1.24AVE00" && selecaoVersao != "N.1.02" && selecaoVersao != "1.07.02TBN1")

                   
                {
                    NAOCADASTRADO formNaoCadastrada = new NAOCADASTRADO();
                    this.Hide();
                    formNaoCadastrada.Show();
                }
                break;
            }
        }

        //Métodos de Click
        public void CUSTOM_A55HYB_W11()
        {
            try
            {
                if (Coordenada1 != "OK")
                {
                    AbrirCustomControl();
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 5;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada1 = "OK";
                            Point p = new Point();
                            p.X = 1100;
                            p.Y = 650;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
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
                            p.X = 170;
                            p.Y = 250;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                ValidaOK();
            }
            catch (Exception ex) { MessageBox.Show("Não Consegui executar a inicialização do Burnin" + ex); }
        }

        public void CUSTOM_A52MOB_W11()
        {
            try
            {
                if (Coordenada1 != "OK")
                {
                    AbrirCustomControl();
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 5;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada1 = "OK";
                            Point p = new Point();
                            p.X = 1100;
                            p.Y = 650;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
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
                            p.X = 170;
                            p.Y = 250;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                ValidaOK();

            }
            catch (Exception ex) { MessageBox.Show("Não Consegui executar a inicialização do Burnin" + ex); }
        }

        public void CUSTOM_STORMX_W11()
        {
            try
            {
                if (Coordenada1 != "OK")
                {
                    AbrirCustomControl();
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 5;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada1 = "OK";
                            Point p = new Point();
                            p.X = 1000;
                            p.Y = 670;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
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
                            p.X = 160;
                            p.Y = 251;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                ValidaOK();
            }
            catch (Exception ex) { MessageBox.Show("Não Consegui executar a inicialização do Burnin" + ex); }
        }

        public void CUSTOM_A52ION_STORMBS_W11()//AJUSTANDO **************************************************
        {
            try
            {
                if (Coordenada1 != "OK")
                {
                    AbrirCustomControl();
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 10;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada1 = "OK";
                            Point p = new Point();
                            p.X = 910;
                            p.Y = 580;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                if (Coordenada2 != "OK")
                {
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 15;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada2 = "OK";
                            Point p = new Point();
                            p.X = 170;
                            p.Y = 220;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                ValidaOK();
            }
            catch (Exception ex) { MessageBox.Show("Não Consegui executar a inicialização do Burnin" + ex); }
        }

        public void CUSTOM_A72ION_W11()
        {
            try
            {
                if (Coordenada1 != "OK")
                {
                    AbrirCustomControl();
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 5;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada1 = "OK";
                            Point p = new Point();
                            p.X = 1000;
                            p.Y = 680;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
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
                            p.X = 170;
                            p.Y = 250;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                ValidaOK();
            }
            catch (Exception ex) { MessageBox.Show("Não Consegui executar a inicialização do Burnin" + ex); }
        }

        public void CUSTOM_A52HYBNEW_W11()
        {
            try
            {
                if (Coordenada1 != "OK")
                {
                    AbrirCustomControl();
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 5;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada1 = "OK";
                            Point p = new Point();
                            p.X = 1100;
                            p.Y = 650;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
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
                            p.X = 170;
                            p.Y = 250;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                ValidaOK();
            }
            catch (Exception ex) { MessageBox.Show("Não Consegui executar a inicialização do Burnin" + ex); }
        }

        public void CUSTOM_A70HYB_STORM2_W11()
        {//FAZENDO MALABARISMOS PARA CONSEGUIR FAZER FUNCIONAR O A70HYB E O STORM2
            try
            {
                if (Coordenada1 != "OK")
                {
                    AbrirCustomControl();
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 5;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada1 = "OK";
                            Point p = new Point();
                            p.X = 1100;
                            p.Y = 650;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                if (Coordenada2 != "OK")
                {
                    AbrirCustomControl();
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
                            p.X = 910;
                            p.Y = 615;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                if (Coordenada3 != "OK")
                {
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 15;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada3 = "OK";
                            Point p = new Point();
                            p.X = 170;
                            p.Y = 250;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                ValidaOK();
            }
            catch (Exception ex) { MessageBox.Show("Não Consegui executar a inicialização do Burnin" + ex); }
        }

        public void CUSTOM_A70HYB_3050_W11()
        {
            try
            {
                if (Coordenada1 != "OK")
                {
                    AbrirCustomControl();
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 5;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada1 = "OK";
                            Point p = new Point();
                            p.X = 1100;
                            p.Y = 650;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
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
                            p.X = 170;
                            p.Y = 250;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                ValidaOK();
            }
            catch (Exception ex) { MessageBox.Show("Não Consegui executar a inicialização do Burnin" + ex); }
        }

        /// <summary>
        /// AJUSTADO.................................
        /// </summary>
        public void CUSTOM_A70HYB_3060_W11()
        {
            try
            {
                if (Coordenada1 != "OK")
                {
                    AbrirCustomControl();
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 5;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada1 = "OK";
                            Point p = new Point();
                            p.X = 910;
                            p.Y = 615;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
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
                            p.X = 170;
                            p.Y = 250;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                ValidaOK();
            }
            catch (Exception ex) { MessageBox.Show("Não Consegui executar a inicialização do Burnin" + ex); }
        }

        public void CUSTOM_A70MOB_3050_W11()
        {
            try
            {
                if (Coordenada1 != "OK")
                {
                    AbrirCustomControl();
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 5;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada1 = "OK";
                            Point p = new Point();
                            p.X = 900;
                            p.Y = 615;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
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
                            p.X = 200;
                            p.Y = 250;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                ValidaOK();
            }
            catch (Exception ex) { MessageBox.Show("Não Consegui executar a inicialização do Burnin" + ex); }
        }

        public void CUSTOM_A70MOB_W11()
        {
            try
            {
                if (Coordenada1 != "OK")
                {
                    AbrirCustomControl();
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 5;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada1 = "OK";
                            Point p = new Point();
                            p.X = 950;
                            p.Y = 650;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
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
                            p.X = 210;
                            p.Y = 280;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                ValidaOK();
            }
            catch (Exception ex) { MessageBox.Show("Não Consegui executar a inicialização do Burnin" + ex); }
        }

        public void CUSTOM_A70MOB_W11CLK2()
        {
            try
            {
                if (Coordenada1 != "OK")
                {
                    AbrirCustomControl();
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 5;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada1 = "OK";
                            Point p = new Point();
                            p.X = 910;
                            p.Y = 615;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
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
                            p.X = 180;
                            p.Y = 250;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                ValidaOK();
            }
            catch (Exception ex) { MessageBox.Show("Não Consegui executar a inicialização do Burnin" + ex); }
        }

        public void CUSTOM_BONLITE_W11()//BON Lite não tem Custom - Tirar isso daqui!
        {
            try
            {
                if (Coordenada1 != "OK")
                {
                    AbrirCustomControl();
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 5;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada1 = "OK";
                            Point p = new Point();
                            p.X = 1100;
                            p.Y = 650;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
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
                            p.X = 170;
                            p.Y = 250;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                ValidaOK();
            }
            catch (Exception ex) { MessageBox.Show("Não Consegui executar a inicialização do Burnin" + ex); }
        }

        public void CUSTOM_STORMGO_W11()
        {
            try
            {
                if (Coordenada1 != "OK")
                {
                    AbrirCustomControl();
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 10;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada1 = "OK";
                            Point p = new Point();
                            p.X = 1000;
                            p.Y = 680;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                if (Coordenada2 != "OK")
                {
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 15;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada2 = "OK";
                            Point p = new Point();
                            p.X = 170;
                            p.Y = 250;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                ValidaOK();
            }
            catch (Exception ex) { MessageBox.Show("Não Consegui executar a inicialização do Burnin" + ex); }
        }

        public void CUSTOM_B11MOB_W11()
        {
            try
            {
                if (Coordenada1 != "OK")
                {
                    AbrirCustomControl();
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 5;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada1 = "OK";
                            Point p = new Point();
                            p.X = 1100;
                            p.Y = 650;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
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
                            p.X = 170;
                            p.Y = 250;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                ValidaOK();
            }
            catch (Exception ex) { MessageBox.Show("Não Consegui executar a inicialização do Burnin" + ex); }
        }

        public void CUSTOM_A65_W11()
        {
            try
            {
                if (Coordenada1 != "OK")
                {
                    AbrirCustomControl();
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 5;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada1 = "OK";
                            Point p = new Point();
                            p.X = 1100;
                            p.Y = 650;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
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
                            p.X = 170;
                            p.Y = 250;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                ValidaOK();
            }
            catch (Exception ex) { MessageBox.Show("Não Consegui executar a inicialização do Burnin" + ex); }
        }

        public void CUSTOM_A62_W11()
        {
            try
            {
                if (Coordenada1 != "OK")
                {
                    AbrirCustomControl();
                    Timer relogio = new Timer();
                    relogio.Interval = 1000;
                    int tempo = 5;
                    relogio.Tick += delegate
                    {
                        tempo -= 1;
                        if (tempo == 0)
                        {
                            relogio.Stop();
                            Coordenada1 = "OK";
                            Point p = new Point();
                            p.X = 1100;
                            p.Y = 650;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
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
                            p.X = 170;
                            p.Y = 250;
                            c.leftClick(p);
                        }
                    };
                    relogio.Start();
                }
                ValidaOK();
            }
            catch (Exception ex) { MessageBox.Show("Não Consegui executar a inicialização do Burnin" + ex); }
        }
        //================================================== WINDOWS 11 ==================================================

        public void ValidaOK()
        {
            InsertAvellWeb();
            //WindowState = FormWindowState.Minimized;
            //MinimizarForm();
            this.Close();
        }

        public void MinimizarForm()
        {
            WindowState = FormWindowState.Minimized;
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
                    String DadosFirebase1 = "CControl Executado:" + dataHoraMinuto;
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
