using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CONTROL_COOLER
{
    public partial class AVISOCOOLER : MaterialSkin.Controls.MaterialForm
    {
        public AVISOCOOLER()
        {
            InitializeComponent();
        }

        private void btnRepetir_Click(object sender, EventArgs e)
        {
            try
            {
                //Process instalarCustom = System.Diagnostics.Process.Start(@"C:\TESTES_AVELL\.executaveisAux\InstalarCustomControl.exe");
                //instalarCustom.WaitForExit();
                //Volta para o form principal
                ChamarCoolerStart();
            }
            catch (Exception ex) { }
        }

        public void ChamarCoolerStart()
        {
            COOLERS formCoolers = new COOLERS();
            this.Hide();
            formCoolers.ShowDialog();
        }
    }
}
