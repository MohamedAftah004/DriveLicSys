using DVLD.Classes;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Detained.Controls
{
    public partial class ctrlReleaseDetainLicense : UserControl
    {
        public ctrlReleaseDetainLicense()
        {
            InitializeComponent();
        }



        public void LoadBeforeDetained()
        {

            lblDetainID.Text = "[???]";
            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            lblFineFees.Text = "150";
            lblLicenseID.Text = "[???]";
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
            lblAppFees.Text = "15";
            lblTotalFees.Text = "165";


        }

        
        //

        public void LoadAfterDetained(clsDetainedLicense detaineLicense )
        {
           


            lblDetainID.Text = detaineLicense.DetainID.ToString();
            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            lblFineFees.Text = "150";
            lblLicenseID.Text = detaineLicense.LicenseID.ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
            lblAppFees.Text = "15";
            lblTotalFees.Text = "165";
            lblReleaseDetainAppID.Text = detaineLicense.ReleaseApplicationID.ToString();

        }


        private void ctrlReleaseDetainLicense_Load(object sender, EventArgs e)
        {

        }
    }
}
