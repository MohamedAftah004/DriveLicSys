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

namespace DVLD.Applications.Renew_License_Application
{
    public partial class ctrlRenewApplicationLicenseInfo : UserControl
    {

        public ctrlRenewApplicationLicenseInfo()
        {
            InitializeComponent();
        }

        private void ctrlRenewApplicationLicenseInfo_Load(object sender, EventArgs e)
        {

        }

        public void FillControlInfoBeforeIssued(clsLicense License)
        {
            if (License == null)
                return;

            lblAppDate.Text = DateTime.Today.ToShortDateString();
            lblOldLicenseID.Text = License.LicenseID.ToString();
            lblIssueDate.Text = DateTime.Today.ToShortDateString();
            lblExpirationDate.Text = DateTime.Today.AddYears(clsLicenseClass.Find(License.LicenseClass).DefaultValidityLength).ToShortDateString();
            lblAppFees.Text = clsApplicationType.Find(2).Fees.ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
            lblLicenseFees.Text = clsLicenseClass.Find(License.LicenseClass).ClassFees.ToString();
            lblTotalFees.Text = (Convert.ToInt16(lblAppFees.Text) + Convert.ToInt16(lblLicenseFees.Text)).ToString();

        }


        public void FillControlInfoAfterIssued(clsLicense License , int RenewdLicenseAppID , int RenewedLicenseID)
        {

            lblRLAppID.Text = RenewdLicenseAppID.ToString();
            lblRenewLicenseID.Text = RenewedLicenseID.ToString();
            FillControlInfoBeforeIssued(License);

        }

        private void gbApplicationNewLicenseInfo_Enter(object sender, EventArgs e)
        {

        }
    }
}
