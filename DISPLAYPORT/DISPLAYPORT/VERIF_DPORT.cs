using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Windows.Forms;

namespace DISPLAYPORT
{
    public partial class VERIF_DPORT : MaterialSkin.Controls.MaterialForm
    {
        public string CHECKMODELO;

        public VERIF_DPORT()
        {
            InitializeComponent();
            InformacoesModelo();
            TimeStart1();
        }

        public void InformacoesModelo()
        {
            // Lista de versões de BIOS válidas
            List<string> versoesValidas = new List<string> { "N.1.13AVE01", "N.1.04Ave01", "N.1.24AVE00" };

            SelectQuery tecladoAvell = new SelectQuery("Win32_BIOS");
            ManagementObjectSearcher objOSDetails = new ManagementObjectSearcher(tecladoAvell);
            ManagementObjectCollection osDetailsCollection = objOSDetails.Get();
            foreach (ManagementObject filtroPorBios in osDetailsCollection)
            {
                string selecaoVersao = (filtroPorBios["SMBIOSBIOSVersion"].ToString());

                if (versoesValidas.Contains(selecaoVersao))
                {
                    lblModelo.Text = "MÁQUINA COM DISPLAY PORT";
                }
                else
                {
                    lblModelo.Text = "ESTA MÁQUINA NÃO TEM DISPLAY PORT";
                }
                break;
            }
        }

        public void TimeStart1()
        {
            // Preciso adicionar este time para tirar o bug de abrir o form antes de finalizar o processo.
            Timer relogio = new Timer();
            relogio.Interval = 1000;
            int tempo = 3;

            relogio.Tick += delegate {
                tempo -= 1;
                lblTime.Text = tempo.ToString();
                if (tempo == 0)
                {
                    relogio.Stop();
                    lblTime.ForeColor = Color.White;
                    VerificarModelo();
                }
            };
            relogio.Start();
        }

        public void VerificarModelo()
        {
            if (CHECKMODELO != "Active1")
            {
                // Lista de versões de BIOS válidas
                List<string> versoesValidas = new List<string> { "N.1.13AVE01", "N.1.04.Ave01", "N.1.24AVE00" };

                SelectQuery tecladoAvell = new SelectQuery("Win32_BIOS");
                ManagementObjectSearcher objOSDetails = new ManagementObjectSearcher(tecladoAvell);
                ManagementObjectCollection osDetailsCollection = objOSDetails.Get();
                foreach (ManagementObject filtroPorBios in osDetailsCollection)
                {
                    string selecaoVersao = (filtroPorBios["SMBIOSBIOSVersion"].ToString());

                    if (versoesValidas.Contains(selecaoVersao))
                    {
                        DISPLAYPORT_OK();
                    }
                    else
                    {
                        CHECKMODELO = "Active1";
                        LICENCA_WINDOWS.WINDOWSKEY formLicencaWindows = new LICENCA_WINDOWS.WINDOWSKEY();
                        this.Hide();
                        formLicencaWindows.ShowDialog();
                    }
                    break;
                }
            }
        }

        public void DISPLAYPORT_OK()
        {
            if (CHECKMODELO != "Active1")
            {
                CHECKMODELO = "Active1";
                DISPORTSTART formDisplayPortSim = new DISPORTSTART();
                this.Hide();
                formDisplayPortSim.Show();
            }
        }
    }
}



/*
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Windows.Forms;

namespace DISPLAYPORT
{
    public partial class VERIF_DPORT : MaterialSkin.Controls.MaterialForm
    {
        public string CHECKMODELO;

        public VERIF_DPORT()
        {
            InitializeComponent();
            InformacoesModelo();
            TimeStart1();
        }

        public void InformacoesModelo()
        {
            // Lista de versões de BIOS válidas
            List<string> versoesValidas = new List<string> { "N.1.13AVE01", "N.1.04.Ave01", "N.1.24AVE00" };

            SelectQuery tecladoAvell = new SelectQuery("Win32_BIOS");
            ManagementObjectSearcher objOSDetails = new ManagementObjectSearcher(tecladoAvell);
            ManagementObjectCollection osDetailsCollection = objOSDetails.Get();
            foreach (ManagementObject filtroPorBios in osDetailsCollection)
            {
                string selecaoVersao = (filtroPorBios["SMBIOSBIOSVersion"].ToString());

                if (versoesValidas.Contains(selecaoVersao))
                {
                    lblModelo.Text = "A52 - ION COM DISPLAYPORT";
                }
                else
                {
                    lblModelo.Text = "ESTA MÁQUINA NÃO TEM DISPLAY PORT";
                }
                break;
            }
        }

        public void TimeStart1()
        {
            // Preciso adicionar este time para tirar o bug de abrir o form antes de finalizar o processo.
            Timer relogio = new Timer();
            relogio.Interval = 1000;
            int tempo = 3;

            relogio.Tick += delegate {
                tempo -= 1;
                lblTime.Text = tempo.ToString();
                if (tempo == 0)
                {
                    relogio.Stop();
                    lblTime.ForeColor = Color.White;
                    VerificarModelo();
                }
            };
            relogio.Start();
        }

        public void VerificarModelo()
        {
            if (CHECKMODELO != "Active1")
            {
                // Lista de versões de BIOS válidas
                List<string> versoesValidas = new List<string> { "N.1.13AVE01", "N.1.04.Ave01", "N.1.24AVE00" };

                SelectQuery tecladoAvell = new SelectQuery("Win32_BIOS");
                ManagementObjectSearcher objOSDetails = new ManagementObjectSearcher(tecladoAvell);
                ManagementObjectCollection osDetailsCollection = objOSDetails.Get();
                foreach (ManagementObject filtroPorBios in osDetailsCollection)
                {
                    string selecaoVersao = (filtroPorBios["SMBIOSBIOSVersion"].ToString());

                    if (versoesValidas.Contains(selecaoVersao))
                    {
                        DISPLAYPORT_OK();
                    }
                    else
                    {
                        CHECKMODELO = "Active1";
                        LICENCA_WINDOWS.WINDOWSKEY formLicencaWindows = new LICENCA_WINDOWS.WINDOWSKEY();
                        this.Hide();
                        formLicencaWindows.ShowDialog();
                    }
                    break;
                }
            }
        }

        public void DISPLAYPORT_OK()
        {
            if (CHECKMODELO != "Active1")
            {
                CHECKMODELO = "Active1";
                DISPORTSTART formDisplayPortSim = new DISPORTSTART();
                this.Hide();
                formDisplayPortSim.Show();
            }
        }
    }
}

*/


/*
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Windows.Forms;

namespace DISPLAYPORT
{
    public partial class VERIF_DPORT : MaterialSkin.Controls.MaterialForm
    {
        public string CHECKMODELO;

        public VERIF_DPORT()
        {
            InitializeComponent();
            InformacoesModelo();
            TimeStart1();
        }

        public void InformacoesModelo()
        {
            SelectQuery tecladoAvell = new SelectQuery("Win32_BIOS");
            ManagementObjectSearcher objOSDetails = new ManagementObjectSearcher(tecladoAvell);
            ManagementObjectCollection osDetailsCollection = objOSDetails.Get();
            foreach (ManagementObject filtroPorBios in osDetailsCollection)
            {
                string selecaoVersao = (filtroPorBios["SMBIOSBIOSVersion"].ToString());

                if (selecaoVersao == "N.1.13AVE01")
                {
                    lblModelo.Text = "A52 - ION COM DISPLAYPORT";
                }
                else
                {
                    lblModelo.Text = "ESTA MÁQUINA NÃO TEM DISPLAY PORT";
                }
                break;
            }
        }

        public void TimeStart1()
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
                    lblTime.ForeColor = Color.White;
                    VerificarModelo();
                }
            };
            relogio.Start();
        }

        public void VerificarModelo()
        {
            if (CHECKMODELO != "Active1")
            {
                SelectQuery tecladoAvell = new SelectQuery("Win32_BIOS");
                ManagementObjectSearcher objOSDetails = new ManagementObjectSearcher(tecladoAvell);
                ManagementObjectCollection osDetailsCollection = objOSDetails.Get();
                foreach (ManagementObject filtroPorBios in osDetailsCollection)
                {
                    string selecaoVersao = (filtroPorBios["SMBIOSBIOSVersion"].ToString());

                    if (selecaoVersao == "N.1.13AVE01")
                    {
                        DISPLAYPORT_OK();
                    }

                    if (selecaoVersao != "N.1.13AVE01")
                    {
                        CHECKMODELO = "Active1";
                        LICENCA_WINDOWS.WINDOWSKEY formLicencaWindows = new LICENCA_WINDOWS.WINDOWSKEY();
                        this.Hide();
                        formLicencaWindows.ShowDialog();
                    }
                    break;
                }
            }
        }

        public void DISPLAYPORT_OK()
        {
            if (CHECKMODELO != "Active1")
            {
                CHECKMODELO = "Active1";
                DISPORTSTART formDisplayPortSim = new DISPORTSTART();
                this.Hide();
                formDisplayPortSim.Show();
            }
        }
    }
}
*/