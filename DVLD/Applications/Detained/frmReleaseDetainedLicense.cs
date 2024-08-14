using DVLD.Classes;
using DVLD.Drivers;
using DVLD.Licenses;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Detained.Controls
{
    public partial class frmReleaseDetainedLicense : Form
    {

        private int _selectedLicenseID;
        private clsLicense _selectedLicense;
        private clsDetainedLicense _detainedLicense = new clsDetainedLicense();
        private clsApplication _releaseApp = new clsApplication();
      
        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
        }

        public frmReleaseDetainedLicense(int givenLicenseID)
        {

            InitializeComponent();

            _selectedLicenseID = givenLicenseID;
            _selectedLicense = clsLicense.Find(_selectedLicenseID);


            //Find Detain License using License ID
            _detainedLicense = clsDetainedLicense.FindByLicenseID(_selectedLicenseID);
            if (_detainedLicense.IsReleased)
            {
                MessageBox.Show($"Selected License with ID={_selectedLicenseID} Already Not Detained, choose another one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ctrlShowLienseInfo1.LoadControlInfo(_selectedLicense);
            ctrlReleaseDetainLicense1.LoadBeforeDetained();

            gbFilter.Enabled = false;
            btnRelease.Enabled = true;
            llShowNewLicenseInfo.Enabled = false;
            llShowLicenseHistory.Enabled = true;

        }


        //Delegate to refresh after added successfully
        public delegate void DataBackEventHandler(object sender);
        public event DataBackEventHandler DataBack;




        private void _IfNullValue(int ErrorMsgID)
        {
            if(ErrorMsgID == 1)
            {
                MessageBox.Show($"No License with License ID={_selectedLicenseID}, choose another one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ctrlShowLienseInfo1.LoadControlInfo(null);
            gbFilter.Enabled = true;

            btnRelease.Enabled = false;
            llShowNewLicenseInfo.Enabled = false;
            llShowLicenseHistory.Enabled = false;
        }

        private void _IfAlreadyNotDetain()
        {
            MessageBox.Show($"License with License ID={_selectedLicenseID}, was Not Detained.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ctrlShowLienseInfo1.LoadControlInfo(_selectedLicense);
            ctrlReleaseDetainLicense1.LoadBeforeDetained();

            gbFilter.Enabled = true;
            btnRelease.Enabled = false;
            llShowNewLicenseInfo.Enabled = false;
            llShowLicenseHistory.Enabled = true;
        }

        private void _IfValid()
        {

            ctrlShowLienseInfo1.LoadControlInfo(_selectedLicense);
            ctrlReleaseDetainLicense1.LoadBeforeDetained();

            gbFilter.Enabled = true;
            btnRelease.Enabled = true;
            llShowNewLicenseInfo.Enabled = false;
            llShowLicenseHistory.Enabled = true;

            //Find Detain License using License ID

        }


        // if released successfully
        private void _IfReleased()
        {
            gbFilter.Enabled = false;
            btnRelease.Enabled = false;
            llShowLicenseHistory.Enabled = true;
            llShowNewLicenseInfo.Enabled = true;

            ctrlShowLienseInfo1.LoadControlInfo(_selectedLicense);
            ctrlReleaseDetainLicense1.LoadAfterDetained(_detainedLicense);


            _selectedLicense.IsActive = true;

            if (_selectedLicense.Save())
                DataBack?.Invoke(this);
        }



        //Adding New Application
        private bool _AddApplication()
        {
            _releaseApp.ApplicantPersonID = clsApplication.FindBaseApplication(_selectedLicense.ApplicationID).ApplicantPersonID;
            _releaseApp.ApplicationDate = DateTime.Now;


            _releaseApp.ApplicationTypeID = 5;
            _releaseApp.ApplicationStatus = clsApplication.FindBaseApplication(_selectedLicense.ApplicationID).ApplicationStatus;
            _releaseApp.LastStatusDate = clsApplication.FindBaseApplication(_selectedLicense.ApplicationID).LastStatusDate;
            _releaseApp.PaidFees = clsApplicationType.Find(5).Fees;
            _releaseApp.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_releaseApp.Save())
            {
                return true;
            }

            return false;
        }

        //Reomve Added Application
        private void _RemoveApplication()
        {
            _releaseApp.Delete();
        }
        //Adding Release Application

        private bool _ReleaseProcess()
        {


            _detainedLicense.IsReleased = true;
            _detainedLicense.ReleaseDate = DateTime.Now;
            _detainedLicense.ReleasedByUserID = clsGlobal.CurrentUser.UserID;


            if (_AddApplication())
            {
                _detainedLicense.ReleaseApplicationID = _releaseApp.ApplicationID;
                if (_detainedLicense.Save())
                {
                    return true;
                }
            }

            return false;
        }



        //not detained license



        private void btnFind_Click(object sender, EventArgs e)
        {

            _selectedLicenseID = Convert.ToInt16(txtFilterValue.Text);
            _selectedLicense = clsLicense.Find(_selectedLicenseID);
            
            if(_selectedLicense == null)
            {
                _IfNullValue(1);
                return;
            }

            //Find Detain License using License ID
            _detainedLicense = clsDetainedLicense.FindByLicenseID(_selectedLicenseID);
            if (_detainedLicense == null || _detainedLicense.IsReleased)
            {
                _IfAlreadyNotDetain();
                return;
            }


            _IfValid();

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignore the input if not a digit
            }
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to Release this license ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                if (_ReleaseProcess())
                {
                    MessageBox.Show("License Released Successfully.", "Released", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _IfReleased();
                }
                else
                {
                    MessageBox.Show("License Not Released, Still Detained.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _IfNullValue(2);
                }
            }
        }

        private void llShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicense frm = new frmShowLicense(_selectedLicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDriverLicenseHistory frm = new frmShowDriverLicenseHistory(clsDriver.Find(_selectedLicense.DriverID));
            frm.ShowDialog();
        }

        private void frmReleaseDetainedLicense_Load(object sender, EventArgs e)
        {

        }
    }
}
