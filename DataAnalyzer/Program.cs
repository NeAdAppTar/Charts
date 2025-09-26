using System;
using System.Windows.Forms;

namespace DataParserApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Сначала показываем SplashForm
            SplashForm splashForm = new SplashForm();
            Application.Run(splashForm);
        }
    }
}
