using System;
using System.Windows.Forms;
using Employees.Views;

namespace Employees
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
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new ScreensaverForm());
            Application.Run(new StartForm());
        }
    }
}