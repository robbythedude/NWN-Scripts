/*
 * Author: Robert Steiner (robbythedude@hotmail.com)
 * Date: 3/27/15
 * Purpose of this application is to fetch the most recent NWN Tethyr server updates and automatically install for user.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TethyrAutoUpdater
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
            Application.Run(new MainForm());
        }
    }
}
