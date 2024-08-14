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

namespace DVLD.Applications.Detained
{
    public partial class frmDetainedLicense : Form
    {
        public frmDetainedLicense()
        {
            InitializeComponent();
        }

        //Delegate to refresh after added successfully
        public delegate void DataBackEventHandler(object sender);
        public event DataBackEventHandler DataBack;


        private int _selectedLicenseID;
        private clsLicense _selectedLicense = new clsLicense();
        private clsDetainedLicense _detainedLicense = new clsDetainedLicense();
        private Decimal _fineAmount;

        private void _IfNullValue()
        {
            MessageBox.Show($"No License with License ID={_selectedLicenseID}, choose another one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ctrlShowLienseInfo1.LoadControlInfo(null);
            gbFilter.Enabled = true;

            btnDetain.Enabled = false;
            llShowNewLicenseInfo.Enabled = false;
            llShowLicenseHistory.Enabled = false;
        }

        private void _IfAlreadyDetain()
        {
            MessageBox.Show($"License with License ID={_selectedLicenseID}, Already Detained.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ctrlShowLienseInfo1.LoadControlInfo(_selectedLicense);
            ctrlDetainInfo1.LoadBeforeDetained();

            gbFilter.Enabled = true;
            btnDetain.Enabled = false;
            llShowNewLicenseInfo.Enabled = false;
            llShowLicenseHistory.Enabled = true;
        }

        private bool _DetainProcess()
        {
            if (txtFineFees.Text == "")
                _fineAmount = 5;
            else
                _fineAmount = Convert.ToDecimal(txtFineFees.Text);

            if (clsDetainedLicense.isDetainExistByLicenseID(_selectedLicenseID))
            {

                _detainedLicense = clsDetainedLicense.FindByLicenseID(_selectedLicenseID);
                _detainedLicense.IsReleased = false;
                _detainedLicense.ReleaseDate = DateTime.MinValue;
                _detainedLicense.ReleasedByUserID = 0;
                _detainedLicense.ReleaseApplicationID = 0;

                //nulldate
                _detainedLicense.DetainDate = DateTime.Now;
                if (_detainedLicense.Save())
                    return true;

            }
            else
            {
                _detainedLicense.LicenseID = _selectedLicenseID;
                _detainedLicense.DetainDate = DateTime.Now;
                _detainedLicense.FineFees = _fineAmount;
                _detainedLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                _detainedLicense.IsReleased = false;

                //nulldate
                _detainedLicense.ReleaseDate = DateTime.MinValue;

                if (_detainedLicense.Save())
                    return true;
            }

            return false;
        }

        private void _IfDetained()
        {
            MessageBox.Show("License Detained Successfully.", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ctrlDetainInfo1.LoadAfterDetained(_detainedLicense.DetainID, _detainedLicense.LicenseID, clsGlobal.CurrentUser.UserName);

            gbFilter.Enabled = false;

            btnDetain.Enabled = false;
            llShowNewLicenseInfo.Enabled = true;
            llShowLicenseHistory.Enabled = true;

            txtFineFees.Enabled = false;
            _selectedLicense.IsActive = false;
            if (_selectedLicense.Save())
                DataBack?.Invoke(this);

        }

        private void _IfNotDetained()
        {
            MessageBox.Show("License Detained Failed.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ctrlDetainInfo1.LoadBeforeDetained();

            gbFilter.Enabled = false;

            btnDetain.Enabled = false;
            llShowNewLicenseInfo.Enabled = false ;
            llShowLicenseHistory.Enabled = true;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {

            _selectedLicenseID = Convert.ToInt16(txtFilterValue.Text);
            _selectedLicense = clsLicense.Find(_selectedLicenseID);

            if (_selectedLicense == null)
            {
                _IfNullValue();
                return;
            }

            //bool isFound = clsDetainedLicense.isDetainExistByLicenseID(_selectedLicenseID);
            //bool isDetained = clsDetainedLicense.FindByLicenseID(_selectedLicenseID).IsReleased;
            clsDetainedLicense dlicense = clsDetainedLicense.FindByLicenseID(_selectedLicenseID);

            if (dlicense != null && dlicense.IsReleased == false)
            {
                _IfAlreadyDetain();
                return;
            }

            ctrlDetainInfo1.LoadBeforeDetained();
            ctrlShowLienseInfo1.LoadControlInfo(_selectedLicense);

            btnDetain.Enabled = true;
            llShowLicenseHistory.Enabled = true;
            llShowNewLicenseInfo.Enabled = true;


           
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignore the input if not a digit
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignore the input if not a digit
            }
        }

        private void frmDetainedLicense_Load(object sender, EventArgs e)
        {

        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Detain this license ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {


                if (_DetainProcess())
                {
                    _IfDetained();
                }
                else
                {
                    _IfNotDetained();
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









        /*

        //Adding New Application
        private bool _AddApplication()
        {
            _newApp = new clsApplication();
            _newApp.ApplicantPersonID = clsApplication.FindBaseApplication(_oldLicense.ApplicationID).ApplicantPersonID;
            _newApp.ApplicationDate = DateTime.Now;


            _newApp.ApplicationTypeID = (Mode == enMode.Damaged) ? 4 : 3;
            _newApp.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            _newApp.LastStatusDate = DateTime.Now;
            _newApp.PaidFees = (Mode == enMode.Damaged) ? 5 : 10;
            _newApp.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_newApp.Save())
            {
                return true;
            }

            return false;
        }

        //Reomve Added Application
        private void _RemoveApplication()
        {
            _newApp.Delete();
        }
        */


    }
}
