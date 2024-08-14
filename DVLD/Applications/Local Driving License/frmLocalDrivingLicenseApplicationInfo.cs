using DVLD.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications
{
    public partial class frmLocalDrivingLicenseApplicationInfo : Form
    {
        private int _LDLAppID = -1;
        private int _PassedTests = 0;
        public frmLocalDrivingLicenseApplicationInfo(int lDLAppID , int passedTests)
        {
            InitializeComponent();
            _LDLAppID= lDLAppID;
            _PassedTests = passedTests;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void frmLocalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            ctrlDrivingLicenseApplicationInfo1.FillControl(_LDLAppID, _PassedTests);
        }
    }
}
