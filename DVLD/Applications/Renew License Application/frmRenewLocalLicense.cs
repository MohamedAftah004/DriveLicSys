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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Renew_License_Application
{
    public partial class frmRenewLocalLicense : Form
    {
        public frmRenewLocalLicense()
        {
            InitializeComponent();
        }

        private clsLocalDrivingLicenseApplication _LocalDrivingLicense = new clsLocalDrivingLicenseApplication();
        private clsLicense _newLicense = new clsLicense();
        private clsLicense _selectedLicense = new clsLicense();
        private clsApplication _newApp;


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool _IsFound()
        {

           

            if (clsLocalDrivingLicenseApplication.IsApplicationExist(_selectedLicense.ApplicationID))
            { 
                _LocalDrivingLicense = clsLocalDrivingLicenseApplication.FindByApplicationID(_selectedLicense.ApplicationID);
                return true;
            }

            _LocalDrivingLicense = null;
            return false;

        }

        private bool _IsActive()
        {

            if (_LocalDrivingLicense == null)
                return false;

            _selectedLicense = clsLicense.FindBuAppID(_LocalDrivingLicense.ApplicationID);
            return _selectedLicense.IsActive;
        }

        //Disabled Buttons if not valid value
        private void _DisabledSomeControlsIfNotValidLicense()
        {
            btnRenew.Enabled = false; 
            llShowNewLicenseInfo.Enabled = false;
            
        }
        //Disabled Some Buttons after issued
        private void _DisabledSomeControlAfterRenewed()
        {
            btnRenew.Enabled = false;
            llShowNewLicenseInfo.Enabled = true;
            llShowLicenseHistory.Enabled = true;
            gbFilter.Enabled = false;
        }

        //Reset form
        private void _ResetForm()
        {
            llShowLicenseHistory.Enabled = false;
            llShowNewLicenseInfo.Enabled = false;
            btnRenew.Enabled = false;
            ctrlRenewApplicationLicenseInfo1.FillControlInfoBeforeIssued(null);
            ctrlShowLienseInfo1.LoadControlInfo(null);
            txtNote.Text = "";
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            _selectedLicense = clsLicense.Find(Convert.ToInt16(txtFilterValue.Text));
            if(_selectedLicense == null)
            {
                MessageBox.Show($"No License with ID = {Convert.ToInt16(txtFilterValue.Text)}.", "Not Founded", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_IsFound())
            {

                if(!_IsActive() || _selectedLicense.ExpirationDate > DateTime.Today)
                {
                    //handle error message
                    MessageBox.Show($"This Licenses was not valied.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlShowLienseInfo1.LoadControlInfo(_selectedLicense);
                    ctrlRenewApplicationLicenseInfo1.FillControlInfoBeforeIssued(_selectedLicense);
                    txtNote.Text = _selectedLicense.Notes;
                    _DisabledSomeControlsIfNotValidLicense();
                    return;
                }
                else
                {
                    btnRenew.Enabled = true;
                    llShowLicenseHistory.Enabled = true;
                    ctrlShowLienseInfo1.LoadControlInfo(_selectedLicense);
                    ctrlRenewApplicationLicenseInfo1.FillControlInfoBeforeIssued(_selectedLicense);
                    txtNote.Text = _selectedLicense.Notes;
                    return;
                }

            }
            else
            {
                MessageBox.Show("Selected License ID not Correct choose another one.","Not Found",  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        //Adding New Application
        private bool _AddApplication()
        {
            _newApp = new clsApplication();
            _newApp.ApplicantPersonID = clsLocalDrivingLicenseApplication.FindByApplicationID(_selectedLicense.ApplicationID).ApplicantPersonID;
            _newApp.ApplicationDate = DateTime.Now;
            _newApp.ApplicationTypeID = 2;
            _newApp.ApplicationStatus = clsLocalDrivingLicenseApplication.enApplicationStatus.Completed;
            _newApp.LastStatusDate = DateTime.Now;
            _newApp.PaidFees = clsApplicationType.Find(2).Fees;
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

        //Adding new Local Driving License
        private bool _AddLicense()
        {

            _newLicense = new clsLicense();
            _newLicense.ApplicationID = _newApp.ApplicationID;
            _newLicense.DriverID = _selectedLicense.DriverID;
            _newLicense.LicenseClass = _selectedLicense.LicenseClass;
            _newLicense.IssueDate = DateTime.Now;
            _newLicense.ExpirationDate = DateTime.Today.AddYears(clsLicenseClass.Find(_selectedLicense.LicenseClass).DefaultValidityLength);
            _newLicense.Notes = (String.IsNullOrEmpty(txtNote.Text))? "No Note" : txtNote.Text;
            _newLicense.PaidFees = Convert.ToDecimal(clsLicenseClass.Find(_newLicense.LicenseClass).ClassFees);
            _newLicense.IsActive = true;
            _newLicense.IssueReason = 2;
            _newLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_newLicense.Save())
            {
                _selectedLicense.IsActive = false;
                _selectedLicense.Save();
              
                return true;
            }

            _RemoveApplication();
            return false;
        }
        //Reomve License
        private void _RemoveLicense()
        {
            clsLicense.DeleteLicense(_newLicense.LicenseID);
        }



        //Adding Local Drivingl License 
        private bool _AddLocalLicense()
        {

            _LocalDrivingLicense.ApplicationID = _newApp.ApplicationID;
            _LocalDrivingLicense.LicenseClassID = _newLicense.LicenseClass;

            if(_LocalDrivingLicense.Save())
            {
                return true;
            }

            _RemoveApplication();
            _RemoveLicense();
            return false;

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignore the input if not a digit
            }
        }



        //

        private void btnRenew_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to Renew the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                if (_AddApplication())
                {

                    if (_AddLicense())
                    {

                        if (_AddLocalLicense())
                        {
                            MessageBox.Show("Renewed Successefully.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ctrlRenewApplicationLicenseInfo1.FillControlInfoAfterIssued(_newLicense, _newLicense.ApplicationID, _newLicense.LicenseID);
                            txtNote.Text = _newLicense.Notes;
                            _DisabledSomeControlAfterRenewed();
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Renewed Failed Operation", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                    }

                }
                else
                {
                    MessageBox.Show("Renewed Failed Operation", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Renewed Failed Operation", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void llShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicense frm = new frmShowLicense(_LocalDrivingLicense.LocalDrivingLicenseApplicationID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_selectedLicense == null)
            {
                MessageBox.Show("To Show Driver Licenses History.\n* First Select Correct License ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsDriver driver = clsDriver.Find(_selectedLicense.DriverID);

            if(driver == null)
            {
                MessageBox.Show("To Show Driver Licenses History.\n* First Select Correct License ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmShowDriverLicenseHistory frm = new frmShowDriverLicenseHistory(driver);
            frm.ShowDialog();
        }
    }
}
