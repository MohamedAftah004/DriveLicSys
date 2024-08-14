using DVLD.Classes;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses
{
    public partial class frmIssueDrivingLicense_FirstTime : Form
    {

        private int _LDLAppID, _PassedTestss = 3;
        private bool _isIssued = false;
        private bool _isExsistDriver = false;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApp;
        private clsDriver _Driver;


        public frmIssueDrivingLicense_FirstTime(int lDLAppID)
        {
            InitializeComponent();
        
            _LDLAppID  = lDLAppID;
            _LocalDrivingLicenseApp = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LDLAppID);
        }

        public delegate void DataBackEventHandler(object sender);
        public event DataBackEventHandler DataBack;


        //before adding driver check if driver already in system or no
        private bool _IsDriverAlreadyInSystem()
        {
            if (clsDriver.isDriverExistByPersonID(_LocalDrivingLicenseApp.ApplicantPersonID))
            {
                return true;
            }
            
                return false;

        }

        //adding driver
        private bool _AddNewDriver()
        {

            //driver already in system - why added he again
            if (_IsDriverAlreadyInSystem())
            {
                _isExsistDriver = true;
                return true;
            }

            _Driver = new clsDriver();


            _Driver.PersonID = _LocalDrivingLicenseApp.ApplicantPersonID;
            _Driver.CreatedDate = DateTime.Now;
            _Driver.CreatedByUserID = clsGlobal.CurrentUser.UserID;
        
            if(_Driver == null)
            {
                return false;
            }

            if(_Driver.Save())
            {
                return true;
            }

            return false;

        }
        //delete driver if license not issues
        private bool _DeleteDriver()
        {
            if (_Driver != null)
            {
                if (clsDriver.DeleteDriver(_Driver.DriverID))
                    return true;
            }


            return false;
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {

            if(!_AddNewDriver())
            {
                MessageBox.Show("You have Nullable Data ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_isIssued)
            {
                MessageBox.Show("Already Issued", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _DeleteDriver();
                return;
            }

            clsLicense _License = new clsLicense();
            
            if(_isExsistDriver)
            {
                _Driver = clsDriver.FindByPersonID(_LocalDrivingLicenseApp.ApplicantPersonID);
            }

            _License.ApplicationID = _LocalDrivingLicenseApp.ApplicationID;
            _License.DriverID = _Driver.DriverID;
            _License.LicenseClass = _LocalDrivingLicenseApp.LicenseClassID;
            _License.IssueDate = DateTime.Now;
            _License.ExpirationDate = DateTime.Now.AddYears(_LocalDrivingLicenseApp.LicenseClassInfo.DefaultValidityLength);
            _License.Notes = txtNotes.Text;
            _License.PaidFees = Convert.ToDecimal(_LocalDrivingLicenseApp.LicenseClassInfo.ClassFees);
            _License.IsActive = true;
            _License.IssueReason = 1;
            _License.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_License == null)
            {

                _DeleteDriver();
                MessageBox.Show("You have Nullable Data ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ;

            }

            if (_License.Save())
            {

                MessageBox.Show($"License Issued Successfully With License ID = {_License.LicenseID}", "Successed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _isIssued = true;
                ctrlDrivingLicenseApplicationInfo1.DisableEnableLinkLabelShowLicense(true , _License);
                DataBack?.Invoke(this);
                return ;
            }
        }

        private void frmIssueDrivingLicense_FirstTime_Load(object sender, EventArgs e)
        {
            ctrlDrivingLicenseApplicationInfo1.FillControl(_LDLAppID, _PassedTestss);
            ctrlDrivingLicenseApplicationInfo1.DisableEnableLinkLabelShowLicense(false);

        }
    }
}
