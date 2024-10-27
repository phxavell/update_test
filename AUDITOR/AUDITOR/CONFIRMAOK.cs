using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using MaterialSkin;
using System.Management;
using System.Reflection.Emit;
//Referencia de Maniupular arquivos:
//https://learn.microsoft.com/pt-br/dotnet/csharp/programming-guide/file-system/how-to-copy-delete-and-move-files-and-folders
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Drawing;

namespace AUDITOR
{
    public partial class CONFIRMAOK : MaterialSkin.Controls.MaterialForm
    {
        public CONFIRMAOK()
        {
            InitializeComponent();
            StartFireBaseServices();//Base de Dados On-Line - Ativar ou Desativar Aqui!
        }

        //Firebase
        IFirebaseConfig ifc = new FirebaseConfig()
        {
            //Utilizando RealTimeDatabase do TestesAvell - OK Atualizado
            AuthSecret = "v3zyDmyUJC4sGsdGHHonCePdpxvaKLGu0IN8AAHb",
            BasePath = "https://database-5c3ab-default-rtdb.firebaseio.com/"
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

        private void btnSim_Click(object sender, EventArgs e)
        {
            CriarLog_MySQLAuditoOK();
            try
            {
                ChamarAuditMySql();
                //GerarLogIndividual();
                //CriarLogOK_MySQL();
                Process.Start("taskkill", "/F /IM WinAudit.exe");
                SLQFINALIZA.SQLFINALIZA formSQLFinaliza = new SLQFINALIZA.SQLFINALIZA();
                this.Hide();
                formSQLFinaliza.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        public void CriarLogOK_MySQL()
        {
            try
            {
                //Gera Log de OK para o MySql
                var dataHoraMinutoOK = DateTime.Now.ToString("dd-MM-yyyy-HH:mm:s:s");
                const string Auditor = @"C:\TESTES_AVELL\MySqlData\Auditor.txt";
                var Arquivo = File.AppendText(Auditor);
                Arquivo.WriteLine("AUDITOR OK: " + dataHoraMinutoOK);
                Arquivo.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pasta não Encotrada" + ex);
            }
        }

        public void GerarLogIndividual()
        {
            //Está desabilitado daqui será retirado
            try
            {
                var dataHoraMinutoOK = DateTime.Now.ToString("dd-MM-yyyy-HH:mm:s:s");
                const string Drivers = @"C:\TESTES_AVELL\logs_auditor\Auditor.log";
                var Arquivo = File.AppendText(Drivers);
                Arquivo.WriteLine("TESTE AUDITOR REALIZADO: " + dataHoraMinutoOK);
                Arquivo.Close();
            }
            catch (Exception ex) { }
        }

        public void ChamarAuditMySql()
        {
            AUDITORIAMYSQL.AUDITORIAMYSQL formMySqlAudit = new AUDITORIAMYSQL.AUDITORIAMYSQL();
            formMySqlAudit.Show();
        }

        public void CriarLog_MySQLAuditoOK()
        {
            ManagementObjectSearcher mSearcher = new ManagementObjectSearcher("SELECT SerialNumber, ReleaseDate FROM Win32_BIOS");
            ManagementObjectCollection collection = mSearcher.Get();
            foreach (ManagementObject obj in collection)
            {
                //Dll: System.Management.dll - Para conseguir informações da BIOS
                string NomeArquivo = (string)obj["SerialNumber"];

                //Gera Log de OK para o MySql
                var dataHoraMinutoOK = DateTime.Now.ToString("dd-MM-yyyy-HH:mm:s:s");
                string Furmark = "C:\\TESTES_AVELL\\MySqlData\\" + NomeArquivo + "\\Auditoria_OK.txt";
                var Arquivo = File.AppendText(Furmark);
                Arquivo.WriteLine("AUDITORIA DO EQUIPAMENTO OK, MÁQUINA CONFERIDA PELO OPERADOR: " + dataHoraMinutoOK);
                Arquivo.Close();
            }

            try
            {
                //Firebase - Atualizado
                var dataHoraMinuto = DateTime.Now.ToString("dd/MM/yyyy - HH:mm");
                ManagementObjectSearcher MOS1 = new ManagementObjectSearcher("Select * From Win32_BIOS");
                foreach (ManagementObject getserial in MOS1.Get())
                {
                    string SerialAvell = getserial["SerialNumber"].ToString();
                    String InfoAuditoria = "Testes Validados-OK: " + dataHoraMinuto;
                    var teste = new Auditor1
                    {
                        Serial = SerialAvell,
                        TAuditor = InfoAuditoria
                    };
                    FirebaseResponse response = client.Update("TESTE_FUNCIONAL/" + SerialAvell, teste);
                    SerialAvell = string.Empty;
                    InfoAuditoria = string.Empty;
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
