using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using mTickLibs.IcCardAdapter;

namespace mTicketClient
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Launcher lancher = new Launcher();
            lancher.ShowDialog();
            if (lancher.DialogResult == DialogResult.OK)
            {
                Application.Run(new Scanner(lancher.ResultIpAddr,lancher.ResultPort));
            }

//            Application.Run(new Scanner("127.0.0.1", 8000));
        }
    }
}
