using System;
using System.Windows.Forms;
using MaterialSkin;
using FireSharp.Config;
using FireSharp.Interfaces;

namespace AUDIO
{
    public partial class CONFIRMALOOP : MaterialSkin.Controls.MaterialForm
    {
        public string AUDIO1;

        public CONFIRMALOOP()
        {
            InitializeComponent();
        }

        private void CONFIRMALOOP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (AUDIO1 != "OK")
            {
                AUDIO1 = "OK";

                //Chamar o Form loopback
                LOOPBACK formLoopback = new LOOPBACK();
                this.Hide();
                formLoopback.Show();
            }
        }
    }
}
