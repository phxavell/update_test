using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LIMPAR_TESTE
{
    public partial class LIMPAR_TESTE : MaterialSkin.Controls.MaterialForm
    {
        public LIMPAR_TESTE()
        {
            InitializeComponent();
            BarraDeProgresso1();
        }

        private void BarraDeProgressoValor()
        {
            //Este processo vai passando à cada 10 sempre que acionado.
            if (barraProgresso1.Value < 100)
            {
                barraProgresso1.Value = barraProgresso1.Value + 20;
            }
            if (barraProgresso1.Value == 100)
            {
                timerBarra1.Stop();
                //MessageBox.Show("Chegou a 100% progresso");
            }
        }

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

        private void BarraDeProgresso1()
        {
            Timer relogio = new Timer();
            relogio.Interval = 1000;
            int tempo = 5;

            relogio.Tick += delegate {
                tempo -= 1;
                lblTime.Text = tempo.ToString();
                if (tempo == 0)
                {
                    relogio.Stop();
                    LimparTestes_Avell();

                    lblTime.Text = "TESTES_AVELL - OK!";
                    lblOK1.Text = "OK";
                    lblOK1.ForeColor = System.Drawing.Color.Green;

                    BarraDeProgressoValor();
                    //Fechar o SysPrep=============================
                    TaskKillSysprep();
                    //=============================================
                    BarraDeProgresso2();
                }
            };
            relogio.Start();
        }

        private void BarraDeProgresso2()
        {
            Timer relogio = new Timer();
            relogio.Interval = 1000;
            int tempo = 5;

            relogio.Tick += delegate {
                tempo -= 1;
                lblTime.Text = tempo.ToString();
                if (tempo == 0)
                {
                    relogio.Stop();
                    LimparPastaEC();

                    lblTime.Text = "DESK EC - OK!";
                    lblOK2.Text = "OK";
                    lblOK2.ForeColor = System.Drawing.Color.Green;

                    BarraDeProgressoValor();
                    BarraDeProgresso3();
                }
            };
            relogio.Start();
        }

        private void BarraDeProgresso3()
        {
            Timer relogio = new Timer();
            relogio.Interval = 1000;
            int tempo = 5;

            relogio.Tick += delegate {
                tempo -= 1;
                lblTime.Text = tempo.ToString();
                if (tempo == 0)
                {
                    relogio.Stop();
                    LimparPastaTesteOld();

                    lblTime.Text = "DESK TESTE - OK!";
                    lblOK3.Text = "OK";
                    lblOK3.ForeColor = System.Drawing.Color.Green;

                    BarraDeProgressoValor();
                    BarraDeProgresso4();
                }
            };
            relogio.Start();
        }

        private void BarraDeProgresso4()
        {
            Timer relogio = new Timer();
            relogio.Interval = 1000;
            int tempo = 5;

            relogio.Tick += delegate {
                tempo -= 1;
                lblTime.Text = tempo.ToString();
                if (tempo == 0)
                {
                    relogio.Stop();
                    //Remove Residuos de teste Burnin
                    ResiduosTestes();

                    BarraDeProgressoValor();
                    BarraDeProgresso5();
                }
            };
            relogio.Start();
        }

        private void BarraDeProgresso5()
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
                    BarraDeProgressoValor();
                    VerificaLicenca();
                }
            };
            relogio.Start();
        }

        public void VerificaLicenca()
        {
            try
            {
                //Verifica se a máquina tem licença, se tiver vai para o Sysprep, senão vai informar
                Process process = new Process();
                process.StartInfo.FileName = "wmic";
                process.StartInfo.Arguments = "path softwarelicensingservice get OA3xOriginalProductKey";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                // Se retornar uma valor diferente deste: OA3xOriginalProductKey
                // Vai executar o Sysprep
                int index = output.IndexOf("OA3xOriginalProductKey");
                if (index != -1)
                {
                    string keySubstring = output.Substring(index + "OA3xOriginalProductKey".Length).Trim();

                    if (keySubstring != "")
                    {
                        lblKey.Text = keySubstring;
                        lblKey.ForeColor = Color.DarkGreen;
                        //FinalizarLimpeza();
                        VerificaBurnin();
                    }
                    else
                    {
                        lblKey.Text = "MÁQUINA SEM SISTEMA, USE O DESTRUIR!";
                        lblKey.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não consegui verificar MSKEY", "MSKEY FALHA!");
            }
        }

        private void LimparTestes_Avell()
        {
            try
            {
                string localizarTESTES_AVELL = @"C:\TESTES_AVELL";
                if (Directory.Exists(localizarTESTES_AVELL))
                {
                    System.IO.Directory.Delete(localizarTESTES_AVELL, true);
                }
            }
            catch { }
        }

        private void LimparPastaEC()
        {
            try
            {
                string localizarEC = @"C:\Users\Administrador\Desktop\EC";
                if (Directory.Exists(localizarEC))
                {
                    System.IO.Directory.Delete(localizarEC, true);
                }
                else
                {
                    //Somente para desencargo se tiver um usuário na máquina
                    string Usuario = System.Environment.UserName;
                    string PastasDesktopEC = @"C:\\Users\\" + Usuario + "\\Desktop\\EC";
                    if (Directory.Exists(PastasDesktopEC))
                    {
                        System.IO.Directory.Delete(PastasDesktopEC, true);
                    }
                }
            }
            catch { }
        }

        private void LimparPastaTesteOld()
        {
            try
            {
                string localizarEC = @"C:\Users\Administrador\Desktop\Teste";
                if (Directory.Exists(localizarEC))
                {
                    System.IO.Directory.Delete(localizarEC, true);
                }
                else
                {
                    //Somente para desencargo se tiver um usuário na máquina
                    string Usuario = System.Environment.UserName;
                    string PastasDesktopEC = @"C:\\Users\\" + Usuario + "\\Desktop\\Teste";
                    if (Directory.Exists(PastasDesktopEC))
                    {
                        System.IO.Directory.Delete(PastasDesktopEC, true);
                    }
                }
            }
            catch { }
        }

        private void ResiduosTestes()
        {
            //Verifica se a pasta de Burnin ainda está na máquina
            String CaminhoPastaBurnin = @"C:\BurnInTest test files";
            if (Directory.Exists (CaminhoPastaBurnin))
            {
                System.IO.Directory.Delete(CaminhoPastaBurnin, true);
                lblTime.Text = "LIMPEZA DE PASTA BURNIN";
                lblOK4.Text = "OK";
                lblOK4.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblTime.Text = "PASTA BURNIN OK";
                lblOK4.Text = "OK";
                lblOK4.ForeColor = System.Drawing.Color.Green;
            }
            //Apagar também a pasta Temp
            PastaTemp();
        }

        private void PastaTemp()
        {
            //Apaga também a pasta temp
            String CaminhoPastaTemp = @"C:\Temp";
            if (Directory.Exists (CaminhoPastaTemp))
            {
                System.IO.Directory.Delete(CaminhoPastaTemp, true);
                lblTime.Text = "LIMPEZA DA PASTA TEMP";
                lblOK4.Text = "OK";
                lblOK4.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblTime.Text = "PASTA TEMP OK";
                lblOK4.Text = "OK";
                lblOK4.ForeColor = System.Drawing.Color.Green;
            }
            PastaPerfLogs();
        }

        private void PastaPerfLogs()
        {
            //Apaga também a pasta temp
            String CaminhoPastaPerfLogs = @"C:\PerfLogs";
            if (Directory.Exists(CaminhoPastaPerfLogs))
            {
                System.IO.Directory.Delete(CaminhoPastaPerfLogs, true);
                lblTime.Text = "LIMPEZA DA PASTA PERFLOGS";
                lblOK4.Text = "OK";
                lblOK4.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblTime.Text = "PASTA PERFLOGS OK";
                lblOK4.Text = "OK";
                lblOK4.ForeColor = System.Drawing.Color.Green;
            }
        }

        /// <summary>
        /// Aqui revejo e confiro mais 1X se todas as pastas
        /// estão apagadas da unidade - (Conferindo)
        /// </summary>
        private void VerificaBurnin()
        {
            //Verificar se a pasta de Burnin ainda está na máquina.
            String VerificaPastaBurnin = @"C:\BurnInTest test files";
            if (Directory.Exists(VerificaPastaBurnin))
            {
                MessageBox.Show("APAGUE A PASTA: C:\\BurnInTest test files", "AVISO!");
            }
            else
            {
                VerificaTestesAvell();
            }
        }

        private void VerificaTestesAvell()
        {
            //Verificar se a pasta Testes_Avell ainda está na máquina.
            String VerificaPastaBurnin = @"C:\TESTES_AVELL";
            if (Directory.Exists(VerificaPastaBurnin))
            {
                MessageBox.Show("APAGUE A PASTA: C:\\TESTES_AVELL", "AVISO!");
            }
            else
            {
                FinalizarLimpeza();
            }
        }

        private void FinalizarLimpeza()
        {
            lblTime.Text = "EXECUTANDO SYSPREP...";
            lblTime.ForeColor = System.Drawing.Color.DarkOrange;
            ChamarSysPrep();
        }

        private void ChamarSysPrep()
        {
            try
            {
                String CaminhoSysprepDesk = @"C:\Users\Administrador\Desktop\LimparTestes";
                if (Directory.Exists(CaminhoSysprepDesk))
                {
                    String CaminhoSysprepArq = @"C:\Users\Administrador\Desktop\LimparTestes\SysPrep.exe";
                    Process.Start(CaminhoSysprepArq);
                }
                else
                {
                    MessageBox.Show("SysPrep.exe não foi encontrado", "FALHA - Sysprep");
                }
            }
            catch { }
        }

        ////
        ///COMANDO UTILIZADO PELO ARQUIVO EXECUTÁVEL DO SYSPREP
        ///@if exist "C:\Users\administrador\Desktop\lic.log" (@%WINDIR%\System32\Sysprep\sysprep.exe /oobe /shutdown /unattend:%userprofile%\Desktop\Teste\unattend-WORK.xml) else (@msg_mr.vbs)
        ///Obs: Pode ser melhorado porque como pode ser visto depende da existência do arquivo lic.log
        ///Não é útil deixar a execução do Sysprep amarrado nisso.
        ///
    }
}
