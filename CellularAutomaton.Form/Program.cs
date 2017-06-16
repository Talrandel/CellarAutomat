#region Header
// Solution      : CellularAutomaton
// Project       : CellularAutomaton.Form
// Project type  : 
// Language      : C# 7.0
// File          : Program.cs
// Author        : Антипкин С.С.
// Created       : 10.06.2017 22:06
// Last Revision : 14.06.2017 21:39
// Description   : 
#endregion

using System;
using System.Windows.Forms;

[assembly: CLSCompliant(true)]

namespace CellarAutomatForm
{
    internal static class Program
    {
        #region Members
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
        #endregion
    }
}
