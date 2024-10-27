using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Management;
using FireSharp.Response;
using FireSharp.Config;
using FireSharp.Interfaces;
using System.Linq;

namespace LICENCA_WINDOWS
{
    public partial class WINDOWSKEY : Form
    {
        //Vou guardar a versão do Windows
        public string WINVER;
        public string CONFIRMA;
        public string ANTILOOP;

        public string ATIVADOOK;

        public WINDOWSKEY()
        {
            InitializeComponent();
            StartFireBaseServices();
            MSKEYInfo();
            //VerificaMSKEY();
            CheckStartNetCMD();
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

        public void CheckStartNetCMD()
        {
            //Precisa criar essa flag para não ficar em loop depois de confirmado
            if (CONFIRMA != "OK")
            {
                var quantidadeLog = Directory.GetFiles(@"C:\TESTES_AVELL\logs_licenca", "*.log", SearchOption.TopDirectoryOnly).Count().ToString();
                int valor = int.Parse(quantidadeLog);
                if (valor >= 5)
                {
                    CONFIRMA = "OK";

                    CONFIRMAR_LICENCA formConfirmaLicenca = new CONFIRMAR_LICENCA();
                    this.Hide();
                    formConfirmaLicenca.ShowDialog();
                }
                else
                {
                    VerificaMSKEY();
                }
            }
        }


        //Verificar a versão do Windows
        public void VerificaMSKEY()
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
                        lblTime.Text = keySubstring;
                        lblKey.Text = keySubstring;
                        lblTime.ForeColor = Color.DarkGreen;
                        lblLicencaWindows.Text = "CHAVE MICROSOFT OK!";
                        lblLicencaWindows.ForeColor = Color.DarkGreen;

                        InsertAvellWeb();
                        ChamarAutidor();
                    }
                    else
                    {
                        TimeStart1();
                        GerarLog_ajuste();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não consegui verificar MSKEY", "MSKEY FALHA!");
            }
        }

        public void TimeStart1()
        {
            //Inserir uma flag para tirar o loop
            if (ANTILOOP != "OK")
            {
                Timer relogio = new Timer();
                relogio.Interval = 1000;
                int tempo = 3;

                relogio.Tick += delegate {
                    tempo -= 1;
                    lblTime.Text = tempo.ToString();
                    if (tempo == 0)
                    {
                        relogio.Stop();
                        RemoverUnidades();
                    }
                };
                relogio.Start();
            }
        }

        public void RemoverUnidades()
        {
            //Se existir as unidades mapeadas, vai tirar o mapeamento para fazer novamente
            try
            {
                ProcessStartInfo DesconectarY = new ProcessStartInfo("cmd.exe", @"/C " + "@net use Y: /delete");
                DesconectarY.UseShellExecute = false;
                DesconectarY.CreateNoWindow = true;
                Process.Start(DesconectarY);
                //==============================================================================================
                ProcessStartInfo DesconectarZ = new ProcessStartInfo("cmd.exe", @"/C " + "@net use Z: /delete");
                DesconectarZ.UseShellExecute = false;
                DesconectarZ.CreateNoWindow = true;
                Process.Start(DesconectarZ);
                lblTime.Text = "Y:/Z: Desconetar";

                TimeStart2();
            }
            catch (Exception ex) { }
        }

        public void TimeStart2()
        //Preciso adicionar este time para tirar o bug de abrir o form antes de finalizar o processo.
        {
            Timer relogio = new Timer();
            relogio.Interval = 1000;
            int tempo = 3;

            relogio.Tick += delegate {
                tempo -= 1;
                lblTime.Text = tempo.ToString();
                if (tempo == 0)
                {
                    relogio.Stop();
                    MapearUnidade();
                }
            };
            relogio.Start();
        }

        public void MapearUnidade()
        {
            //Aqui mapea novamente as unidades de rede()
            try
            {
                ProcessStartInfo MapearCMDLic = new ProcessStartInfo("cmd.exe", @"/C " + "@net use Z: \\\\tucuma\\windowssetup$ /user:localhost\\netadmin 8fc6716fa3");
                MapearCMDLic.UseShellExecute = false;
                MapearCMDLic.CreateNoWindow = true;
                Process.Start(MapearCMDLic);

                ProcessStartInfo MapearCMDLog = new ProcessStartInfo("cmd.exe", @"/C " + "@net use Y: \\\\tucuma\\windowssetup$\\avell-test-2023 /user:localhost\\netadmin 8fc6716fa3");
                MapearCMDLog.UseShellExecute = false;
                MapearCMDLog.CreateNoWindow = true;
                Process.Start(MapearCMDLog);

                lblTime.Text = "Y:/Z: Reconectar";

                TimeStart3();
            }
            catch (Exception ex) { }
        }

        public void TimeStart3()
        //Preciso adicionar este time para tirar o bug de abrir o form antes de finalizar o processo.
        {
            Timer relogio = new Timer();
            relogio.Interval = 1000;
            int tempo = 3;

            relogio.Tick += delegate {
                tempo -= 1;
                lblTime.Text = tempo.ToString();
                if (tempo == 0)
                {
                    relogio.Stop();
                    InserirLicencaMicrosoft();
                }
            };
            relogio.Start();
        }

        public void InserirLicencaMicrosoft()
        {
            //Processo de inserção de licença - Chamar arquivos
            try
            {
                lblTime.Text = "Inserir Licença";

                string caminhoDoPrograma = @"Z:\scripts\startnet.cmd";
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = caminhoDoPrograma,
                };
                Process processo = new Process { StartInfo = startInfo };
                processo.Start();
                processo.WaitForExit();

                //Verificar se fica aqui gerar log
                GerarLog();
            }
            catch  (Exception ex)
            {
                //Aqui vai ser direcionado para o loop ou para o próximo teste
                lblTime.Text = "Não consegui Inserir a Chave, tentar denovo!";
                //VerificaMSKEY();
                CheckStartNetCMD();
            }
        }

        public void ChamarAutidor()
        {
            if (ATIVADOOK != "OK")
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

                        //Aqui já vai inserir direto a versão do Windows11
                        FireBaseWindows11();
                        

                        ATIVADOOK = "OK";
                        ANTILOOP = "OK";

                        AUDITOR.AUDITOR formAuditorStart = new AUDITOR.AUDITOR();
                        this.Hide();
                        formAuditorStart.ShowDialog();
                    }
                };
                relogio.Start();
            }
        }

        public void MSKEYInfo()// -- Somente para Informar se já possui licenca e a chave
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
                        lblKey.ForeColor = Color.DarkGreen;
                        lblLicencaWindows.Text = "CHAVE MICROSOFT OK!";
                        lblLicencaWindows.ForeColor = Color.DarkGreen;
                    }
                }
            }
            catch (Exception ex)
            {
                //Messagebox.Show("Não foi possível verificar", ex);
            }
        }

        public void GerarLog()
        {
            try
            {
                var dataHoraMinutoOK = DateTime.Now.ToString("dd-MM-yyyy-HH:mm:s:s");
                const string Drivers = @"C:\TESTES_AVELL\log_licenca\licenca.log";
                var Arquivo = File.AppendText(Drivers);
                Arquivo.WriteLine("LICENÇA INSERIDA: " + dataHoraMinutoOK);
                Arquivo.Close();
                lblTime.Text = "Verificando Chave Microsoft.";
                //VerificaMSKEY();
                CheckStartNetCMD();
            }
            catch (Exception e)
            {
                //Voltar para loop
            }
        }

        public void GerarLog_ajuste()
        {
            try
            {
                string logLicenca = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
                StreamWriter logControleLicenca = new StreamWriter(@"C:\TESTES_AVELL\logs_licenca\" + logLicenca + ".log");
                logControleLicenca.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível criar log" + ex);
            }
        }

        public void FireBaseWindows11()
        {
            if (ATIVADOOK != "OK")
            {
                ManagementObjectSearcher MOS1 = new ManagementObjectSearcher("Select * From Win32_BIOS");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
                ManagementObjectCollection information = searcher.Get();
                foreach (ManagementObject getserial in MOS1.Get())
                {
                    foreach (ManagementObject obj in information)
                    {
                        string edition = obj["Caption"].ToString();
                        string SerialAvell = getserial["SerialNumber"].ToString();
                        String DadosFirebase1 = "Sistema:" + edition;
                        var teste = new windows
                        {
                            Serial = SerialAvell,
                            TSistema = DadosFirebase1
                        };
                        FirebaseResponse response = client.Update("WEB_CONSULTA_TESTE/" + SerialAvell, teste);
                    }
                    break;
                }
            }
        }

        public void InsertAvellWeb()
        {//Inserir informações sobre a licença consumida

            ManagementObjectSearcher MOS1 = new ManagementObjectSearcher("Select * From Win32_BIOS");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectCollection information = searcher.Get();
            foreach (ManagementObject getserial in MOS1.Get())
            {
                foreach (ManagementObject obj in information)
                {
                    string edition = obj["Caption"].ToString();
                    string SerialAvell = getserial["SerialNumber"].ToString();
                    String DadosFirebase1 = "Licença Microsoft:" + edition;
                    var teste = new windowsLic
                    {
                        Serial = SerialAvell,
                        TLicenca = DadosFirebase1
                    };
                    FirebaseResponse response = client.Update("WEB_CONSULTA_LICENCA/" + SerialAvell, teste);
                }
                break;
            }

            foreach (ManagementObject getserial in MOS1.Get())
            {
                foreach (ManagementObject obj in information)
                {
                    string edition = obj["Caption"].ToString();
                    string SerialAvell = getserial["SerialNumber"].ToString();
                    String DadosFirebase2 = "Versao_Windows:" + edition;
                    var teste = new windowsSistem
                    {
                        Serial = SerialAvell,
                        TSistema = DadosFirebase2
                    };
                    FirebaseResponse response = client.Update("WEB_CONSULTA_TESTE/" + SerialAvell, teste);
                }
                break;
            }

            var data1 = DateTime.Now.ToString("dd/MM/yyyy");
            ManagementObjectSearcher MOS2 = new ManagementObjectSearcher("Select * From Win32_BIOS");
            foreach (ManagementObject getserial in MOS1.Get())
            {
                string SerialAvell = getserial["SerialNumber"].ToString();
                String DadosFirebase1 = data1;
                var teste = new data1
                {
                    Serial = SerialAvell,
                    TData1 = DadosFirebase1
                };
                FirebaseResponse response = client.Update("WEB_CONSULTA_LICENCA/" + SerialAvell, teste);
                SerialAvell = string.Empty;
                DadosFirebase1 = string.Empty;
                break;
            }
        }
    }
}
