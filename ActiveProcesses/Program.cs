using FYK.ITTools.Common;
using System;
using System.Windows.Forms;

namespace FYK.SQLTools.ActiveProcesses
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var arguments = new Arguments(args);
            bool preFill = (arguments["prefill"] != null);
            Application.Run(new MainForm(preFill));
        }
    }
}
