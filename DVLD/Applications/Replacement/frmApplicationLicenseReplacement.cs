using DVLD.Applications.Renew_License_Application;
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

namespace DVLD.Applications.Replacement
{
    public partial class frmApplicationLicenseReplacement : Form
    {
        private bool _isIssued = false;
        private int _oldLicenseID;
        private clsLicense _oldLicense;
        private clsLicense _newLicense;
        private clsApplication _newApp;
        private clsLocalDrivingLicenseApplication _localDrivingLicense;
        
        enum enMode { Damaged , Lost};
        private enMode Mode = enMode.Damaged;

        public frmApplicationLicenseReplacement()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void frmApplicationLicenseReplacement_Load(object sender, EventArgs e)
        {
            rbDamagedLicense.Checked = true ;
            btnIssue.Enabled = false;
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignore the input if not a digit
            }
        }

        private void _LoadDefaultNull()
        {
            ctrlAppInfoLicenseReplacement1.FillDefault();
            ctrlShowLienseInfo1.LoadControlInfo(null);
            btnIssue.Enabled = false;
            llShowLicenseHistory.Enabled = false;
            llShowNewLicenseInfo.Enabled = false;
        }

        private void _LoadDefaultNotActive()
        {
            int appFees = (rbDamagedLicense.Checked) ? 5 : 10;
            ctrlAppInfoLicenseReplacement1.FillBeforeIssue(appFees , _oldLicenseID);
            ctrlShowLienseInfo1.LoadControlInfo(_oldLicense);
            btnIssue.Enabled = false;
            llShowLicenseHistory.Enabled = true ;
            llShowNewLicenseInfo.Enabled = false;
        }

        private void _LoadDefaultValidLicense()
        {


            if (rbDamagedLicense.Checked)
                ctrlAppInfoLicenseReplacement1.FillBeforeIssue(5, _oldLicenseID);
            else
                ctrlAppInfoLicenseReplacement1.FillBeforeIssue(10, _oldLicenseID);

            btnIssue.Enabled = true;
            llShowLicenseHistory.Enabled = true;

            if(_isIssued)
            {
                ctrlShowLienseInfo1.LoadControlInfo(_newLicense);
                ctrlAppInfoLicenseReplacement1.FillAfterIssued(_newLicense, Convert.ToInt16(_newApp.PaidFees), _oldLicenseID);
            }
            else
                ctrlShowLienseInfo1.LoadControlInfo(_oldLicense);

            llShowNewLicenseInfo.Enabled = (_isIssued) ? true : false;
          
        }



        private void btnFind_Click(object sender, EventArgs e)
        {

            _oldLicenseID = Convert.ToInt16(txtFilterValue.Text);
            _oldLicense = clsLicense.Find(_oldLicenseID);

            if (_oldLicense == null) 
            {
                MessageBox.Show($"No Licenses was License ID = {_oldLicenseID}" , "Error" , MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LoadDefaultNull();
                return;
            }

            //if (!clsLicense.isLicenseExist(_oldLicense.LicenseID))
            //{
            //    MessageBox.Show($"Choose Local Driving License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    _LoadDefaultNull();
            //    return;
            //}


            if (_oldLicense.IsActive == false)
            {
                MessageBox.Show($"Licenses was License ID = {_oldLicenseID} was Not Active", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LoadDefaultNotActive();
                return;
            }

            _LoadDefaultValidLicense();

        }




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

        //Adding new Local Driving License
        private bool _AddLicense()
        {

            _newLicense = new clsLicense();
            _newLicense.ApplicationID = _newApp.ApplicationID;
            _newLicense.DriverID = _oldLicense.DriverID;
            _newLicense.LicenseClass = _oldLicense.LicenseClass;
            _newLicense.IssueDate = DateTime.Now;
            _newLicense.ExpirationDate = DateTime.Today.AddYears(clsLicenseClass.Find(_oldLicense.LicenseClass).DefaultValidityLength);
            _newLicense.Notes = _oldLicense.Notes;
            _newLicense.PaidFees = Convert.ToDecimal(clsLicenseClass.Find(_newLicense.LicenseClass).ClassFees);
            _newLicense.IsActive = true;
            _newLicense.IssueReason = 2;
            _newLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_newLicense.Save())
            {
                _oldLicense.IsActive = false;
                _oldLicense.Save();

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

           

            _localDrivingLicense = new clsLocalDrivingLicenseApplication();
            _localDrivingLicense.ApplicationID = _newApp.ApplicationID;
            _localDrivingLicense.LicenseClassID = _oldLicense.LicenseClass;

            clsLocalDrivingLicenseApplication _oldLDLApp = clsLocalDrivingLicenseApplication.FindByApplicationID(_oldLicense.ApplicationID);


            if (_localDrivingLicense.SaveJustLocal())
            {

                if (clsTestAppointment.ReplaceLocalIDFromOldLicenseToNewLicense(
                    _localDrivingLicense.LocalDrivingLicenseApplicationID, _oldLDLApp.LocalDrivingLicenseApplicationID))
                {
                    return true;
                }

                _localDrivingLicense.Delete();
            }

            _RemoveApplication();
            _RemoveLicense();
            return false;

        }



        private void btnIssue_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to Issue a Replacement for the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            if (_AddApplication())
            {

                if (_AddLicense())
                {

                    if (_AddLocalLicense())
                    {
                        clsApplication oldApp = clsApplication.FindBaseApplication(_oldLicense.ApplicationID);
                        oldApp.ApplicationStatus = clsApplication.enApplicationStatus.Cancelled;
                        oldApp.Save();
                        MessageBox.Show($"License Replaced Successefully with ID={_newLicense.LicenseID}.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _isIssued = true;
                        _LoadDefaultValidLicense();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Replaced Failed Operation", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _oldLicense.IsActive = true;
                        _oldLicense.Save();
                        return;
                    }

                }

            }
            else
            {
                MessageBox.Show("Replaced Failed Operation", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }




        }

        private void llShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicense frm = new frmShowLicense(_localDrivingLicense.LocalDrivingLicenseApplicationID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDriverLicenseHistory frm = new frmShowDriverLicenseHistory(clsDriver.Find(_oldLicense.DriverID));
            frm.ShowDialog();
        }


        //set form by mode
        private void _SetFormByReplacementType(string replacementType)
        {
            lblTitle.Text = $"Replacement for {replacementType} License";
            this.Text = $"Replacement for {replacementType} License"; 
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            Mode = enMode.Damaged;
            _SetFormByReplacementType("Damaged");
            ctrlAppInfoLicenseReplacement1.FillBeforeIssue(5, _oldLicenseID);
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            Mode = enMode.Lost;
            _SetFormByReplacementType("Lost");
            ctrlAppInfoLicenseReplacement1.FillBeforeIssue(10, _oldLicenseID);
        }
    }
}
