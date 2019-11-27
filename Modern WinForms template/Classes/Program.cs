using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Windows.Forms;

namespace BorderlessForm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.Run(new MainForm());
        }
    }
}
