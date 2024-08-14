using DVLD.Properties;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses
{
    public partial class ctrlShowLienseInfo : UserControl
    {

        private clsLocalDrivingLicenseApplication _LDLApp = new clsLocalDrivingLicenseApplication();
        private clsLicense _License = new clsLicense();



        public ctrlShowLienseInfo()
        {
            InitializeComponent();
        }

        private void ctrlShowLienseInfo_Load(object sender, EventArgs e)
        {

        }

        //Take issue reason number -> return by string / ex=> issueReason = 1 ---< issueReasonByString = First Time
        private string ConvertIssueReasonToString(int issueReason)
        {
            string value = "";

            switch (issueReason)
            {
                case 1: value = "First time"; break;
                case 2: value = "Renew License"; break;
                

            }

            return value;
        }

        //fill Info
        /*
           private void _FillForm()
           {

               if (_LDLApp == null)
                   return;


               bool isActive = _License.IsActive;

               lblClassName.Text = _LDLApp.LicenseClassInfo.ClassName;
               lblName.Text = _LDLApp.ApplicantFullName;
               lblLicenseID.Text = _License.LicenseID.ToString();
               lblNationalNo.Text = clsPerson.Find(clsApplication.FindBaseApplication(_LDLApp.ApplicationID).ApplicantPersonID).NationalNo;
               lblName.Text = _LDLApp.ApplicantFullName;
               lblIssueDate.Text = _License.IssueDate.ToShortDateString();
               lblExpirationDate.Text = _License.ExpirationDate.ToShortDateString();
               lblDateOfBirth.Text = clsPerson.Find(clsApplication.FindBaseApplication(_LDLApp.ApplicationID).ApplicantPersonID).DateOfBirth.ToShortDateString();
               lblIssueReason.Text = ConvertIssueReasonToString(_License.IssueReason);
               lblNotes.Text = (String.IsNullOrEmpty(_License.Notes)) ? "No Notes" : _License.Notes;
               lblDriverID.Text = _License.DriverID.ToString();

               short gendor = clsPerson.Find(clsApplication.FindBaseApplication(_LDLApp.ApplicationID).ApplicantPersonID).Gendor;

               pbGendor.Image = (gendor == 0) ? Resources.Man_32 : Resources.Woman_32;
               lblGendor.Text = (gendor == 0) ? "Male" : "Female";

               string _imgPath = clsPerson.Find(clsApplication.FindBaseApplication(_LDLApp.ApplicationID).ApplicantPersonID).ImagePath.ToString();
               if (String.IsNullOrEmpty(_imgPath))
               {
                   pbPersonImage.Image = (gendor == 0) ? Resources.Male_512 : Resources.Female_512;
               }
               else
                   pbPersonImage.Image = Image.FromFile(clsPerson.Find(clsApplication.FindBaseApplication(_LDLApp.ApplicationID).ApplicantPersonID).ImagePath);




               lblIsActive.Text = (isActive) ? "Yes" : "No";
               lblIsDetained.Text = (isActive) ? "No" : "Yes";



           }
           */

        private void _FillForm(int _LDLAppID)
        {

            if (_LDLApp == null)
                return;


            bool isActive = _License.IsActive;

            lblClassName.Text = _LDLApp.LicenseClassInfo.ClassName;
            lblName.Text = _LDLApp.ApplicantFullName;
            lblLicenseID.Text = _License.LicenseID.ToString();
            lblNationalNo.Text = clsPerson.Find(clsApplication.FindBaseApplication(_LDLApp.ApplicationID).ApplicantPersonID).NationalNo;
            lblName.Text = _LDLApp.ApplicantFullName;
            lblIssueDate.Text = _License.IssueDate.ToShortDateString();
            lblExpirationDate.Text = _License.ExpirationDate.ToShortDateString();
            lblDateOfBirth.Text = clsPerson.Find(clsApplication.FindBaseApplication(_LDLApp.ApplicationID).ApplicantPersonID).DateOfBirth.ToShortDateString();
            lblIssueReason.Text = ConvertIssueReasonToString(_License.IssueReason);
            lblNotes.Text = (String.IsNullOrEmpty(_License.Notes)) ? "No Notes" : _License.Notes;
            lblDriverID.Text = _License.DriverID.ToString();

            short gendor = clsPerson.Find(clsApplication.FindBaseApplication(_LDLApp.ApplicationID).ApplicantPersonID).Gendor;

            pbGendor.Image = (gendor == 0) ? Resources.Man_32 : Resources.Woman_32;
            lblGendor.Text = (gendor == 0) ? "Male" : "Female";

            string _imgPath = clsPerson.Find(clsApplication.FindBaseApplication(_LDLApp.ApplicationID).ApplicantPersonID).ImagePath.ToString();
            if (String.IsNullOrEmpty(_imgPath))
            {
                pbPersonImage.Image = (gendor == 0) ? Resources.Male_512 : Resources.Female_512;
            }
            else
                pbPersonImage.Image = Image.FromFile(clsPerson.Find(clsApplication.FindBaseApplication(_LDLApp.ApplicationID).ApplicantPersonID).ImagePath);




            lblIsActive.Text = (isActive) ? "Yes" : "No";

            if (clsDetainedLicense.isDetainExistByLicenseID(_License.LicenseID))
            {
                clsDetainedLicense _detainedLicense = clsDetainedLicense.FindByLicenseID(_License.LicenseID);
                lblIsDetained.Text = (_detainedLicense.IsReleased) ? "No" : "Yes";
            }
            else
                lblIsDetained.Text = "No";



        }

        private void _FillForm()
        {

            bool isActive = _License.IsActive;

            lblClassName.Text = clsLicenseClass.Find(_License.LicenseClass).ClassName;
            lblName.Text = clsApplication.FindBaseApplication(_License.ApplicationID).ApplicantFullName;
            lblLicenseID.Text = _License.LicenseID.ToString();
            lblNationalNo.Text = clsPerson.Find(clsApplication.FindBaseApplication(_License.ApplicationID).ApplicantPersonID).NationalNo;
            lblIssueDate.Text = _License.IssueDate.ToShortDateString();
            lblExpirationDate.Text = _License.ExpirationDate.ToShortDateString();
            lblDateOfBirth.Text = clsPerson.Find(clsApplication.FindBaseApplication(_License.ApplicationID).ApplicantPersonID).DateOfBirth.ToShortDateString();
            lblIssueReason.Text = ConvertIssueReasonToString(_License.IssueReason);
            lblNotes.Text = (String.IsNullOrEmpty(_License.Notes)) ? "No Notes" : _License.Notes;
            lblDriverID.Text = _License.DriverID.ToString();

            short gendor = clsPerson.Find(clsApplication.FindBaseApplication(_License.ApplicationID).ApplicantPersonID).Gendor;

            pbGendor.Image = (gendor == 0) ? Resources.Man_32 : Resources.Woman_32;
            lblGendor.Text = (gendor == 0) ? "Male" : "Female";

            string _imgPath = clsPerson.Find(clsApplication.FindBaseApplication(_License.ApplicationID).ApplicantPersonID).ImagePath.ToString();
            if (String.IsNullOrEmpty(_imgPath))
            {
                pbPersonImage.Image = (gendor == 0) ? Resources.Male_512 : Resources.Female_512;
            }
            else
                pbPersonImage.Image = Image.FromFile(clsPerson.Find(clsApplication.FindBaseApplication(_License.ApplicationID).ApplicantPersonID).ImagePath);




            lblIsActive.Text = (isActive) ? "Yes" : "No";


            if (clsDetainedLicense.isDetainExistByLicenseID(_License.LicenseID))
            {
                clsDetainedLicense _detainedLicense = clsDetainedLicense.FindByLicenseID(_License.LicenseID);
                lblIsDetained.Text = (_detainedLicense.IsReleased) ? "No" : "Yes";
            }
            else
                lblIsDetained.Text = "No";


        }

        private void _DefaultValues()
        {

            lblClassName.Text = "[???]";
            lblName.Text = "[???]";
            lblLicenseID.Text = "[???]";
            lblNationalNo.Text = "[???]";
            lblName.Text = "[???]";
            lblIssueDate.Text = "[???]";
            lblExpirationDate.Text = "[???]";
            lblDateOfBirth.Text = "[???]";
            lblIssueReason.Text = "[???]";
            lblNotes.Text = "[???]";
            lblDriverID.Text = "[???]";


            pbGendor.Image = Resources.Man_32;
            lblGendor.Text = "UnKnown";

            pbPersonImage.Image = Resources.Male_512;




            lblIsActive.Text = "UnKnown";
            lblIsDetained.Text = "UnKnown";

        }

        /*
        public void LoadControlInfo(clsLicense license)
        {
            //if (license == null)
            //{
            //    _DefaultValues();
            //    return;
            //}


            _License = license;
            if (_License == null)
                return;

            _LDLApp = clsLocalDrivingLicenseApplication.FindByApplicationID(license.ApplicationID);
            if (_LDLApp == null)
            {
                MessageBox.Show("Not a Local Driving License ,Please choose another one", "not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillForm();
        }
        */


        public void LoadControlInfo(clsLicense license)
        {
            //if (license == null)
            //{
            //    _DefaultValues();
            //    return;
            //}


            _License = license;
            if (_License == null)
                return;

            _FillForm();
        }



        public void LoadControlInfo(int LDLAppID)
        {
            if(LDLAppID == -1)
            {
                _DefaultValues();
                return;
            }

            _LDLApp = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LDLAppID);
            //_License = clsLicense.FindBuAppID(_LDLApp.ApplicationID);

            if (_LDLApp == null)
            {
                MessageBox.Show("Choose Local Driving License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _License = clsLicense.FindBuAppID(_LDLApp.ApplicationID);

            _FillForm(LDLAppID);
        }

        private void gbDriverLicenseInfo_Enter(object sender, EventArgs e)
        {

        }
    }
}
