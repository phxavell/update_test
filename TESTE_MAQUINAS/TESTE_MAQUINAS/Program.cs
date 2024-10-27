﻿using IronXL.Xml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TESTE_MAQUINAS
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Sistema_Teste());

            //Para o Programa não abrir duas vezes
            Process aProcess = Process.GetCurrentProcess();
            string aProcName = aProcess.ProcessName;
            if (Process.GetProcessesByName(aProcName).Length > 1)
            {
                MessageBox.Show("Programa já está sendo executado, não é necessário abrir novamente!",
                    "Testes Avell - Iformação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
