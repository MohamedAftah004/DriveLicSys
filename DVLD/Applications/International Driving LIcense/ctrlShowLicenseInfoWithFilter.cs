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
    public partial class ctrlShowLicenseInfoWithFilter : UserControl
    {
        public ctrlShowLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        private bool _IsOrdinary = false;
        public void SetFormToOrdinaryLicense(bool IsOrdinary)
        {
            _IsOrdinary = IsOrdinary;
        }

        //Event to send data_back
        public event Action<clsLicense> OnLocalLicenseSelected;

        private clsLicense selectedLicense = new clsLicense();
        protected virtual void PersonSelected(clsLicense license)
        {
            Action<clsLicense> handler = OnLocalLicenseSelected;

            if (handler != null)
            {
                handler(license);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {

            if(_IsOrdinary)
            {
                if (String.IsNullOrEmpty(txtFilterValue.Text.Trim()))
                    return;

                selectedLicense = clsLicense.FindOrdinaryLicense(Convert.ToInt16(txtFilterValue.Text));


                if (selectedLicense == null)
                {
                    MessageBox.Show("Select another license ,'\n'Taking into account the :\n *License Class -> Ordinary Class.'\n' *License Should be Active", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlShowLienseInfo1.LoadControlInfo(-1);
                    return;
                }

                ctrlShowLienseInfo1.LoadControlInfo(selectedLicense);

                if (OnLocalLicenseSelected != null)
                    OnLocalLicenseSelected(selectedLicense);
            }
           else
            {
                if (String.IsNullOrEmpty(txtFilterValue.Text.Trim()))
                    return;

                selectedLicense = clsLicense.Find(Convert.ToInt16(txtFilterValue.Text));


                if (selectedLicense == null)
                {
                    MessageBox.Show("choose Correct LicenseID" , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlShowLienseInfo1.LoadControlInfo(-1);
                    return;
                }

                ctrlShowLienseInfo1.LoadControlInfo(selectedLicense);

                if (OnLocalLicenseSelected != null)
                    OnLocalLicenseSelected(selectedLicense);
            }


            /*
            if(clsLicense.isLicenseExist(selectedLicense.LicenseID))
            {
                ctrlShowLienseInfo1.LoadControlInfo(selectedLicense);

                if (OnLocalLicenseSelected != null)
                    OnLocalLicenseSelected(selectedLicense);

            }
            */

            //if (clsLocalDrivingLicenseApplication.FindByApplicationID(selectedLicense.ApplicationID).LocalDrivingLicenseApplicationID == null)
            //    return;

            //if(clsLocalDrivingLicenseApplication.IsApplicationExist(clsLocalDrivingLicenseApplication.FindByApplicationID(selectedLicense.ApplicationID).LocalDrivingLicenseApplicationID))
            //{
            //    ctrlShowLienseInfo1.LoadControlInfo(clsLocalDrivingLicenseApplication.FindByApplicationID(selectedLicense.ApplicationID).LocalDrivingLicenseApplicationID);

            //    if (OnLocalLicenseSelected != null)
            //        OnLocalLicenseSelected(selectedLicense);

            //}

            //int ldlappID = clsLocalDrivingLicenseApplication.FindByApplicationID(selectedLicense.ApplicationID).LocalDrivingLicenseApplicationID;

            //if (!clsLocalDrivingLicenseApplication.IsApplicationExist(ldlappID))
            //{
            //    MessageBox.Show($"No Licenses with ID = {selectedLicense.LicenseID} , Choose another one", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}



        }

        private void gbFilter_Enter(object sender, EventArgs e)
        {

        }

        private void ctrlShowLicenseInfoWithFilter_Load(object sender, EventArgs e)
        {

        }


        public void DisabledFilter()
        {
            btnFind.Enabled = false;
            txtFilterValue.Enabled = false;

        }







        /*
         
             //Event to send data_back
        public event Action<clsLicense> OnLocalLicenseSelected;

        protected virtual void PersonSelected(clsLicense license)
        {
            Action<clsLicense> handler = OnLocalLicenseSelected;

            if (handler != null)
            {
                handler(license);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            int selectedLicenseID = Convert.ToInt16(txtFilterValue.Text);
            if ((selectedLicenseID <= 0))
            {
                MessageBox.Show("Please enter valid value." , "Error" , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool isActiveLicense = clsLicense.Find(selectedLicenseID).IsActive;


            if (clsLicense.isLicenseExist(selectedLicenseID) && isActiveLicense)
            {
                ctrlShowLienseInfo1.LoadControlInfo(selectedLicenseID);


                if (OnLocalLicenseSelected != null)
                    OnLocalLicenseSelected(clsLicense.Find(selectedLicenseID));
                else
                    OnLocalLicenseSelected(null);

                return;
            }

            //MessageBox.Show($"No Licenses with LicenseID = {selectedLicenseID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        public void SetFilterDisable(bool status)
        {
            gbFilter.Enabled = !status;
        }

        private void ctrlShowLicenseInfoWithFilter_Load(object sender, EventArgs e)
        {

        }

        private void gbFilter_Enter(object sender, EventArgs e)
        {

        }
         
         */

    }
}
