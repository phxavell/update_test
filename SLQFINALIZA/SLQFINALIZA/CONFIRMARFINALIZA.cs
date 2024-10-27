using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;

namespace SLQFINALIZA
{
    public partial class CONFIRMARFINALIZA : MaterialSkin.Controls.MaterialForm
    {
        public string RESET;

        public CONFIRMARFINALIZA()
        {
            InitializeComponent();
            Interacao();
        }

        //Firebase
        IFirebaseConfig ifc = new FirebaseConfig()
        {
            //Utilizando RealTimeDatabase do TestesAvell - OK Atualizado
            AuthSecret = "ni99x0Zyr1HfIsjyF92bKJuoazt3cc7HtsNDrcMF",
            BasePath = "https://testesavell-default-rtdb.firebaseio.com/"
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
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\TestesAprovados.mp3";
            wplayer.controls.play();
        }

        private void btnSim_Click(object sender, EventArgs e)
        {
            DesconectarUnidadesYZ();
            //Vou precisar primeiro apagar o TESTE_MAQUINAS.lnk
            //LimparTesteCSharp();
            ApagarAtalhoTeste();
            //FinalizarAplicacao();
        }

        private void ApagarAtalhoTeste()
        {
            string LimparAtalhoTeste = @"C:\Users\Administrador\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup\TESTE_MAQUINAS.lnk";
            //Verifica primeiro se o arquivo não Existe:
            if (File.Exists(LimparAtalhoTeste))
            {
                //Se atalho ainda existir vai ficar em loop aqui!
                File.Delete(LimparAtalhoTeste);

                LimparTesteCSharp();
            }
            else
            {
                LimparTesteCSharp();
            }
        }

        private void LimparTesteCSharp()
        {

            //Aqui repito o processo para apagr o atalho antigo que precisa ser apagado!
            string LimparAtalhoTeste = @"C:\Users\Administrador\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup\TESTE_MAQUINAS.lnk";
            if (File.Exists(LimparAtalhoTeste))
            {
                ApagarAtalhoTeste();
            }
            else
            {
                string OrigemLimparTeste = @"C:\Users\Administrador\Desktop\LimparTestes\LIMPAR_TESTE.lnk";
                string DestinoLimparTeste = @"C:\Users\Administrador\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup\LIMPAR_TESTE.lnk";
                try
                {
                    //Copiar arquivo para o Startup
                    File.Copy(OrigemLimparTeste, DestinoLimparTeste, true);

                    //Somente depois que certificar-se que está tudo OK, finaliza aplicação!
                    FinalizarAplicacao();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Não foi possível copiar atalho para ShellStartup", "LimparTestes - Atalho Erro!");
                }
            }
        }

        public void FinalizarAplicacao()
        {
            TimeReset();
        }

        private void btnNao_Click(object sender, EventArgs e)
        {          
            try
            {
                MessageBox.Show("AVISO!", "SE ALGUM ITEM DE TESTES NÃO TIVER FUNCIONADO, ENVIE PARA REPARO!");
                ENVIAREPARO formEnviarReparo = new ENVIAREPARO();
                this.Hide();
                formEnviarReparo.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("AVISO!", "SE ALGUM ITEM DE TESTES NÃO TIVER FUNCIONADO, ENVIE PARA REPARO!");
            }
        }

        private void DesconectarUnidadesYZ()
        {
            try
            {
                ProcessStartInfo DesconectarZ = new ProcessStartInfo("cmd.exe", @"/C " + "@net use Z: /delete");
                DesconectarZ.UseShellExecute = false;
                DesconectarZ.CreateNoWindow = true;
                Process.Start(DesconectarZ);

                ProcessStartInfo DesconectarY = new ProcessStartInfo("cmd.exe", @"/C " + "@net use Y: /delete");
                DesconectarY.UseShellExecute = false;
                DesconectarY.CreateNoWindow = true;
                Process.Start(DesconectarY);
            }
            catch (Exception e) { }
        }

        public void TimeReset()
        //Preciso adicionar este time para tirar o bug de abrir o form antes de finalizar o processo.
        {
            if (RESET != "OK!")
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

                        RESET = "OK!";
                        ReiniciarMaquina();
                        Application.Exit();

                    }
                };
                relogio.Start();
            }
        }

        private void ReiniciarMaquina()
        {
            try
            {
                //ProcessStartInfo ReiniciarMaquina = new ProcessStartInfo("cmd.exe", @"/C " + "shutdown -force -restart");
                ProcessStartInfo ReiniciarMaquina = new ProcessStartInfo("cmd.exe", @"/C " + "shutdown /r");
                ReiniciarMaquina.UseShellExecute = false;
                ReiniciarMaquina.CreateNoWindow = true;
                Process.Start(ReiniciarMaquina);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi Possivel reiniciar a máquina");
            }
        }
    }
}
