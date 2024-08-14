using DVLD.Properties;
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

namespace DVLD.Applications.International_Driving_LIcense
{
    public partial class ctrlInternationalLicenseInfo : UserControl
    {
        public ctrlInternationalLicenseInfo()
        {
            InitializeComponent();
        }


        private void DefaultValue()
        {
            lblName.Text = "[???]";
            lblAppID.Text = "[???]";
            lblInternationalLicenseID.Text = "[???]";
            lblLicenseID.Text = "[???]"; 
            lblNationalNo.Text = "[???]";
            lblDateOfBirth.Text = "[???]";
            lblDriverID.Text = "[???]";
            lblIsActive.Text = "UnKnown";
            lblGendor.Text = "UnKnown";
            lblIssueDate.Text = "[???]"; 
            lblExpirationDate.Text = "[???]"; 
        }

        //set Men vlaues
        private void _HandleGendor(int gendorType , string imgPath)
        {
            
            //prifile img path
            switch(gendorType)
            {
                case 0:
                    {
                        lblGendor.Text = "Male";
                        pbGendor.Image = Resources.Man_32;
                        pbPersonImage.Image = (String.IsNullOrEmpty(imgPath)) ? Resources.Male_512 : Image.FromFile(imgPath);
                        break;
                    }
                case 1:
                    {
                        lblGendor.Text = "Female";
                        pbGendor.Image = Resources.Woman_32;
                        pbPersonImage.Image = (String.IsNullOrEmpty(imgPath)) ? Resources.Female_512 : Image.FromFile(imgPath);
                        break;
                    }
                default:
                    {
                        lblGendor.Text = "UnKnown";
                        pbGendor.Image = Resources.Man_32;
                        pbPersonImage.Image = (String.IsNullOrEmpty(imgPath)) ? Resources.Male_512 : Image.FromFile(imgPath);
                        break;
                    }
            }

        }

        public void FillControlInfo(clsInternationalLicense IntLicense)
        {

            clsPerson person = clsPerson.Find(clsLocalDrivingLicenseApplication.FindByApplicationID(IntLicense.ApplicationID).ApplicantPersonID);

            lblName.Text = clsApplication.FindBaseApplication(IntLicense.ApplicationID).ApplicantFullName;
            lblAppID.Text = IntLicense.ApplicationID.ToString();
            lblInternationalLicenseID.Text = IntLicense.InternationalLicenseID.ToString();
            lblLicenseID.Text = IntLicense.IssuedUsingLocalLicenseID.ToString();
            lblNationalNo.Text = person.NationalNo;
            lblDateOfBirth.Text = person.DateOfBirth.ToShortDateString();
            lblDriverID.Text = IntLicense.DriverID.ToString();
            lblIsActive.Text = (IntLicense.IsActive == true) ? "Yes" : "No";

            if (person.Gendor == 0)
                _HandleGendor(0, person.ImagePath);
            else
                _HandleGendor(1, person.ImagePath);


            lblIssueDate.Text = IntLicense.IssueDate.ToShortDateString();
            lblExpirationDate.Text = IntLicense.ExpirationDate.ToShortDateString();
        }

        private void ctrlInternationalLicenseInfo_Load(object sender, EventArgs e)
        {

        }

        private void gbDILInfo_Enter(object sender, EventArgs e)
        {

        }
    }
}
