using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;

using System.Windows.Forms;

namespace LICENCA_WINDOWS
{
    public partial class CONFIRMAR_LICENCA : MaterialSkin.Controls.MaterialForm
    {
        public string REINICIARLIC;

        public CONFIRMAR_LICENCA()
        {
            InitializeComponent();
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

        private void ReiniciarLicenca()
        {
            if (REINICIARLIC != "OK")
            {
                //Apagar arquivos para reiniciar processo de licenca
                System.IO.DirectoryInfo di = new DirectoryInfo(@"C:\TESTES_AVELL\logs_licenca");
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }

                //Esse apaga o Diretório inteiro
                //foreach (DirectoryInfo dir in di.GetDirectories())
                //{
                //    dir.Delete(true);
                //}

                //Simplificado:
                //System.IO.Directory.Delete(@"C:\TESTES_AVELL\logs_licenca", true);

                REINICIARLIC = "OK";
                WINDOWSKEY formWindowsKey = new WINDOWSKEY();
                this.Hide();
                formWindowsKey.ShowDialog();
            }
        }

        private void btnNao_Click(object sender, EventArgs e)
        {
            ReiniciarLicenca();
        }

        private void SeguirAuditor()
        {
            if (REINICIARLIC != "OK")
            {
                REINICIARLIC = "OK";
                AUDITOR.AUDITOR formAuditorStart = new AUDITOR.AUDITOR();
                this.Hide();
                formAuditorStart.ShowDialog();
            }
        }

        private void btnSim_Click(object sender, EventArgs e)
        {
            
            SeguirAuditor();
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
                    String DadosFirebase1 = "Versao_Windows:" + edition;
                    var teste = new windowsSistem
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
}
