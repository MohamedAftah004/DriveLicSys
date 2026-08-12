using DVLD.UI.Applications;
using DVLD.UI.Login;
using DVLD.UI.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.UI
{
    internal static class Program
    {
        
        
        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
          
          Application.Run(new frmLogin());
         


        }
    }
}
