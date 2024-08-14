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

namespace DVLD.Applications.Replacement
{
    public partial class ctrlAppInfoLicenseReplacement : UserControl
    {
        public ctrlAppInfoLicenseReplacement()
        {
            InitializeComponent();
            FillDefault();
        }

        private void ctrlAppInfoLicenseReplacement_Load(object sender, EventArgs e)
        {
        }



        public void FillDefault()
        {
            lblLicenseReplacementApplicationID.Text = "[???]";
            lblAppFees.Text = "[$$$]";
            lblApplicationDate.Text = DateTime.Today.ToShortTimeString();
            lblReplacedLicenseID.Text = "[???]";
            lblOldLicenseID.Text = "[???]";
            lblCreatedBy.Text =  "[???]";

        }

        public void FillAfterIssued(clsLicense license , int appFees , int oldLicenseID)
        {
            lblLicenseReplacementApplicationID.Text = license.ApplicationID.ToString();
            lblAppFees.Text = appFees.ToString();
            lblApplicationDate.Text = DateTime.Today.ToShortTimeString();
            lblReplacedLicenseID.Text = license.LicenseID.ToString();
            lblOldLicenseID.Text = oldLicenseID.ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;

        }

        public void FillBeforeIssue(int appFees, int oldLicenseID)
        {
            lblLicenseReplacementApplicationID.Text = "[???]";
            lblAppFees.Text = appFees.ToString();
            lblApplicationDate.Text = DateTime.Today.ToShortTimeString();
            lblReplacedLicenseID.Text =  "[???]";
            lblOldLicenseID.Text = oldLicenseID.ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;

        }

        private void gbAppInfoForLicenseReplacement_Enter(object sender, EventArgs e)
        {

        }
    }
}
