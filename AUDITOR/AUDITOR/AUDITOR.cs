using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data;
using MaterialSkin;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using static System.Windows.Forms.LinkLabel;
using System.Runtime.InteropServices;
using System.Management;
//Como resolver problema de reinderização do arquivo html para resolver visualização no webBrowser
//https://stackoverflow.com/questions/37350853/c-sharp-webbrowser-script-error
//Adicionar na primeira linha do arquivo Auditoria.html
//<meta http-equiv="X-UA-Compatible" content="IE=Edge" />
using System.Reflection.Emit;
using System.Security.Policy;
using FireSharp.Config;
using FireSharp.Interfaces;
using System.Drawing;

namespace AUDITOR
{
    public partial class AUDITOR : MaterialSkin.Controls.MaterialForm
    {
        //public AUDITORIAMYSQL.AUDITORIAMYSQL AUDITORIAMYSQL { get; private set; }

        public AUDITOR()
        {
            InitializeComponent();
            VerificaLicenca();
            TimeStart1();
            webAudit.ScriptErrorsSuppressed = true;
            //RelatorioAuditor();
        }

        //Somente para quando for salvar o ducumento no servidor de LOGS - botar dentro de um método
        //var DataArquivo = DateTime.Now.ToString("dd/MM/yyyy");

        public void InteracaoLicenca()
        {
            //https://www.naturalreaders.com/online/ - Cria vozes
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\LicencaWindows.mp3";
            wplayer.controls.play();
        }

        public void VerificaLicenca()
        {
            try
            {
                //Verifica se a máquina tem licença, se não tiver vai ficar no loop - Original ChatGPT
                Process process = new Process();
                process.StartInfo.FileName = "wmic";
                process.StartInfo.Arguments = "path softwarelicensingservice get OA3xOriginalProductKey";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                // Extract the product key from the output
                int index = output.IndexOf("OA3xOriginalProductKey");
                if (index != -1)
                {
                    string keySubstring = output.Substring(index + "OA3xOriginalProductKey".Length).Trim();

                    if (keySubstring != "")
                    {
                        lblKey.Text = keySubstring;
                        lblKey.ForeColor = Color.Yellow;
                    }
                    else
                    {
                        lblKey.Text = "CHAVE NÃO INSERIDA";
                        lblKey.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não consegui verificar MSKEY", "MSKEY FALHA!");
            }
        }

        public void Interacao()
        {
            //https://www.naturalreaders.com/online/ - Cria vozes
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\AuditoriaInicio.mp3";
            wplayer.controls.play();
        }

        public void Config()
        {
            try
            {
                Process configuracao = Process.Start("dxdiag");
            }
            catch (Exception ex) { }
        }

        public void TimeStart1()
        //Gera Auditoria.html
        {
            Timer relogio = new Timer();
            relogio.Interval = 1000;
            int tempo = 3;// 3segundos

            relogio.Tick += delegate {
                tempo -= 1;
                lblTime.Text = tempo.ToString();
                if (tempo == 0)
                {
                    relogio.Stop();
                    Interacao();
                    //Depois de chamar o método ele para
                    GerarWinAudit();
                    lblTime.Text = "Gerando Audit.html";
                    timerSegundos.Start();
                }
            };
            relogio.Start();
        }

        public void GerarWinAudit()
        {
            try
            {
                //Esse método vai Gerar o Arquivo, só vai conseguir porque no início dos testes, ele copia arquivo winAudit.exe para pasta raiz fora de files
                ProcessStartInfo GerarWinAudit = new ProcessStartInfo("cmd.exe", @"/C " + "@C:\\TESTES_AVELL\\winAudit\\WinAudit.exe /r=gsoPxuTUeERNtnzDaIbMpmidcSArCOHG /f=Auditoria.html");
                GerarWinAudit.UseShellExecute = false;
                GerarWinAudit.CreateNoWindow = true;
                Process.Start(GerarWinAudit);
                TimeStart2();
            }
            catch (Exception e)
            {
                MessageBox.Show("Não foi possível criar o arquivo de Auditoria");
            }
        }

        public void TimeStart2()
        //Verifica quando o arquivo for gerado
        {
            Timer relogio = new Timer();
            relogio.Interval = 1000;
            int tempo = 2;// 2segundos
            relogio.Tick += delegate {
                tempo -= 1;
                if (tempo == 0)
                {
                    relogio.Stop();
                    VerificaArquivo();
                }
            };
            relogio.Start();
        }

        public void VerificaArquivo()
        {
            string localizarArquivo = @"C:\TESTES_AVELL\winAudit\Auditoria.html";
            if (File.Exists(localizarArquivo))
            {
                //Aciona o próximo Timer assim que encontrar o arquivo na pasta.
                TimeStart3();
            }
            else
            {
                //Dá Loop até encontrar o arquivo.
                TimeStart2();
            }
        }

        public void TimeStart3()
        //Insere a correção do arquivo
        {
            Timer relogio = new Timer();
            relogio.Interval = 1000;
            int tempo = 3;// 5segundos
            relogio.Tick += delegate {
                tempo -= 1;
                if (tempo == 0)
                {
                    relogio.Stop();
                    InserirCorrecaoArquivo();
                    Config();
                }
            };
            relogio.Start();
        }

        public void InserirCorrecaoArquivo()
        {
            string arquivoAuditoria = @"C:\TESTES_AVELL\winAudit\Auditoria.html";
            if (File.Exists(arquivoAuditoria))
            {
                //Considerando como se o arquivo fosse uma lista, mas consegue inseir na primeira linha
                List<string> linhas = File.ReadAllLines(arquivoAuditoria).ToList();
                linhas.Insert(0, "<meta http-equiv='X-UA-Compatible' content='IE=Edge'>");
                File.WriteAllLines(arquivoAuditoria, linhas);

                //Abrir o arquivo no WebBrowser.
                AbrirArquivoWeb();
            }
        }

        public void AbrirArquivoWeb()
        {
            StreamReader sr = new StreamReader(@"C:\TESTES_AVELL\winAudit\Auditoria.html");
            webAudit.DocumentStream = sr.BaseStream;
            TaskKillCMD();
            timerSegundos.Stop();

            ChamarConfirma();
        }

        public void TaskKillCMD()
        {
            try
            {
                Process.Start("taskkill", "/F /IM cmd.exe");
                //Chama o Form OK
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        private void timerSegundos_Tick(object sender, EventArgs e)
        {
            if (progressBarAudit.Value != 100)
            {
                progressBarAudit.Value++;
            }
            else
            {
                timerSegundos.Stop();
            }
        }

        public void ChamarConfirma()
        {
            CONFIRMAOK formConfirma = new CONFIRMAOK();
            formConfirma.Show();
        }
    }
}
