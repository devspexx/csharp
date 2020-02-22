using System;
using System.Threading;
using System.Windows.Forms;

namespace BorderlessForm
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Mutex mutex = null;

            mutex = new Mutex(true, Application.ProductName, out bool createdNew);
            if (!createdNew) {  
                return; 
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.Run(new MainForm());
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e) { }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) { }
    }
}
