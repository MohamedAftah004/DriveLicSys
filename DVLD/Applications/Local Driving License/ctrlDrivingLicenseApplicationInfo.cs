using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Buisness;
using DVLD.Classes;
using static System.Net.Mime.MediaTypeNames;
using DVLD.Tests;
using DVLD.Applications.International_Driving_LIcense;
using DVLD.Licenses;

namespace DVLD.Controls.ApplicationControls
{
    public partial class ctrlDrivingLicenseApplicationInfo: UserControl
    {

        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        private int _LocalDrivingLicenseApplicationID = -1;

        private int _LicenseID;

        private clsLicense _selectedLicense = new clsLicense();

        private int _PassedTests = 0;

        public int LocalDrivingLicenseApplicationID
        {
            get { return _LocalDrivingLicenseApplicationID; }
        }

        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

      

        public void FillControl(int localDrivingLicenseApplicationID, int passedTests)
        {
            _LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                ResetApplicationInfo();
                MessageBox.Show("No Application with ApplicationID = " + _LocalDrivingLicenseApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                _PassedTests = passedTests;
                LoadApplicationInfo();
            }
        }

        private void ctrlApplicationBasicInfo1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
           


        }

        //Load L.D.L Application Data
        private void LoadApplicationInfo()
        {
            //Load user control application
            ctrlApplicationBasicInfo1.LoadApplicationInfo(_LocalDrivingLicenseApplication.ApplicationID);

            //load this data
            lblLocalDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplicationID.ToString();
            lblAppliedFor.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName.ToString();
            lblPassedTests.Text = $"{_PassedTests}/3";

            llShowLicenceInfo.Enabled = (_PassedTests == 3);
        }

        //Reset Values
        private void ResetApplicationInfo()
        {
            //resetting Application by given a founded valid value -> result -> -1 -1 -1 values
            ctrlApplicationBasicInfo1.LoadApplicationInfo(-1);

            //load default data for this form
            lblLocalDrivingLicenseApplicationID.Text = "[????]";
            lblAppliedFor.Text = "[????]";
            lblPassedTests.Text = "0/3";
            llShowLicenceInfo.Enabled = (_PassedTests == 3);

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        //To Disableing And Enabling LinkLabel ShowLicense
        public void DisableEnableLinkLabelShowLicense(bool setEnable, clsLicense issuedLicense = null)
        {
            llShowLicenceInfo.Enabled = (setEnable) ? true : false; 
            _selectedLicense = issuedLicense;
        }

        private void llShowLicenceInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicense frm = new frmShowLicense(_selectedLicense);
            frm.ShowDialog();
        }
    }
}
