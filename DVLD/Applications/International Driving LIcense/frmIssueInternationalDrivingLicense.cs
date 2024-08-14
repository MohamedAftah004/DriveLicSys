using DVLD.Classes;
using DVLD.Drivers;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.International_Driving_LIcense
{
    public partial class frmIssueInternationalLicense : Form
    {

        private clsLicense _selectedLicense = new clsLicense();

        private clsApplication _App = new clsApplication();
        private clsInternationalLicense newInternationalLicense = new clsInternationalLicense();
        private int _InternationalLicenseApplicationID, _I_L_LicenseID;

        //delegate to refresh the form when im called him
        public delegate void DataBackEventHandler(object sender);
        public event DataBackEventHandler DataBack;
         

        public frmIssueInternationalLicense()
        {
            InitializeComponent();
        }


        private void frmIssueInternationalDrivingLicense_Load(object sender, EventArgs e)
        {

        }

        private void ctrlShowLicenseInfoWithFilter1_OnLocalLicenseSelected(clsLicense obj)
        {
            if (obj != null)
            {
                _selectedLicense = obj;
            }
            else
            {
                llShowLicenseInfo.Enabled = false;
                llShowLicensesHistory.Enabled = false;
            }

            FillControlInfo();
        }


        private void _ResetControl()
        {

            lblInternationalLicenseAppID.Text = "[???]";
            lblAppDate.Text = "[???]";
            lblIssueDate.Text = "[???]";
            lblFees.Text = "[???]";
            lblILLicenseID.Text = "[???]";
            lblLocalLicenseID.Text = "[???]";
            lblExpirationDate.Text = "[???]";
            lblCreatedBy.Text = "[???]";

        }
        public void FillControlInfo(clsApplication app, int I_L_AppID, int I_L_LicenseID)
        {
            _App = app;
            _InternationalLicenseApplicationID = I_L_AppID + 1;
            _I_L_LicenseID = I_L_LicenseID;

            _LoadControlInfo_AfterIssued();
        }
        public void FillControlInfo()
        {
            if(_selectedLicense == null)
            {
                _ResetControl();
                return;
            }

            llShowLicensesHistory.Enabled = true;

            _App = clsApplication.FindBaseApplication(_selectedLicense.ApplicationID);
            _LoadControlInfo();
        }
        private void _LoadControlInfo()
        {

            if (_App == null)
            {
                _ResetControl();
                return;
            }


            lblInternationalLicenseAppID.Text = "[???]";
            //lblILAppID.Text = "[???]";
            lblAppDate.Text = _App.ApplicationDate.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblFees.Text = "51";
            lblILLicenseID.Text = "[???]";
            //lblILLicenseID.Text = "[???]";
            lblLocalLicenseID.Text = clsLicense.FindBuAppID(_App.ApplicationID).LicenseID.ToString();
            lblExpirationDate.Text = clsLicense.FindBuAppID(_App.ApplicationID).ExpirationDate.ToShortDateString();
            lblCreatedBy.Text = _App.CreatedByUserInfo.UserName;

        }
        private void _LoadControlInfo_AfterIssued()
        {


            if (_App == null)
            {
                _ResetControl();
                return;
            }


            lblInternationalLicenseAppID.Text = _InternationalLicenseApplicationID.ToString();
            lblAppDate.Text = _App.ApplicationDate.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblFees.Text = "51";
            lblILLicenseID.Text = _I_L_LicenseID.ToString();
           // lblLocalLicenseID.Text = clsLicense.FindBuAppID(_App.ApplicationID).LicenseID.ToString();
            lblLocalLicenseID.Text = _selectedLicense.LicenseID.ToString();
            //lblExpirationDate.Text = clsLicense.FindBuAppID(_App.ApplicationID).ExpirationDate.ToShortDateString();
            lblExpirationDate.Text = _selectedLicense.ExpirationDate.ToShortDateString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private bool _AddNewApplication()
        {
            //Fill International Application
            clsApplication app = new clsApplication();
            app.ApplicantPersonID = clsApplication.FindBaseApplication(_selectedLicense.ApplicationID).ApplicantPersonID;
            app.ApplicationDate = DateTime.Now;
            app.ApplicationTypeID = 3;
            app.ApplicationStatus = clsApplication.enApplicationStatus.New;
            app.LastStatusDate = DateTime.Now;
            app.PaidFees = clsApplicationType.Find(6).Fees;
            app.CreatedByUserID = clsGlobal.CurrentUser.UserID;


            if (app.Save())
            {
                if (app != null)
                {
                    _App = app;
                    return true;
                }
            }
            return false;
        }

        private void _DeleteApplication(clsApplication app)
        {
            if(app != null)
            app.Delete();
        }


        private bool _AddNewInternationalLicense()
        {

            clsInternationalLicense internaionalLicense = new clsInternationalLicense();

            internaionalLicense.ApplicationID = _selectedLicense.ApplicationID;

            //internaionalLicense.DriverID = _selectedLicense.DriverID - 1 ;
            internaionalLicense.DriverID = _selectedLicense.DriverID ;
            internaionalLicense.IssuedUsingLocalLicenseID = _selectedLicense.LicenseID;
            internaionalLicense.IssueDate = DateTime.Now;
            internaionalLicense.ExpirationDate = internaionalLicense.IssueDate.AddYears(10);
            internaionalLicense.IsActive = true;
            internaionalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            
            if(_selectedLicense.IsActive == false)
            {
                MessageBox.Show($"Selected License was Not Active.",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            if (internaionalLicense.Save())
            {
                if (internaionalLicense != null)
                {
                    newInternationalLicense = internaionalLicense;
                    return true;
                }
            }
            return false;

        }
        private void _DeleteInernationalLicense(clsInternationalLicense IntLicese)
        {
            if(IntLicese != null)
                clsInternationalLicense.DeleteInternationalLicense(IntLicese.InternationalLicenseID);
        }

        private void ctrlShowLicenseInfoWithFilter1_Load(object sender, EventArgs e)
        {

        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInternationalLicenseInformation frm = new frmInternationalLicenseInformation(newInternationalLicense);
            frm.ShowDialog();
        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsDriver driver = clsDriver.Find(_selectedLicense.DriverID);
            if(driver != null)
            {
            frmShowDriverLicenseHistory frm = new frmShowDriverLicenseHistory(driver);
            frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Cannot show licenses history, check inputs again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        
        private void btnIssue_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            if (clsInternationalLicense.isInternationalLicenseExist(_selectedLicense.LicenseID))
            {
                MessageBox.Show($"This License Cannot Issued, International License with Local License ID = {_selectedLicense.LicenseID} already exsist in system.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }


            if(_AddNewApplication())
            {


                if(_AddNewInternationalLicense())
                {
                    llShowLicenseInfo.Enabled = true;

                    MessageBox.Show($"International License Issued Successfully With ID = {newInternationalLicense.InternationalLicenseID}.",
                     "License Lssued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _InternationalLicenseApplicationID = newInternationalLicense.InternationalLicenseID;
                    //_InternationalLicenseApplicationID = newInternationalLicense.InternationalLicenseID + 1;

                    _App.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                    _App.LastStatusDate = DateTime.Now;
                    _App.Save();

                    FillControlInfo(_App, newInternationalLicense.ApplicationID, _InternationalLicenseApplicationID);
                    llShowLicenseInfo.Enabled = true;
                    DataBack?.Invoke(this);
                    return;

                }
                else
                {
                    MessageBox.Show($"Cannot issued, Please Check License Class.",
                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _DeleteApplication(_App);

                }


            }
            else
            {
                MessageBox.Show($"Cannot added null data in system.",
                 "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



        }













        /*


             private void btnIssue_Click(object sender, EventArgs e)
    {

        if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            if (clsInternationalLicense.isInternationalLicenseExist(_selectedLicense.LicenseID))
            {
                MessageBox.Show($"This License Cannot Issued, International License with Local License ID = {_selectedLicense.LicenseID} already exsist in system.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            //Fill International Application
            clsApplication app = new clsApplication();
            app.ApplicantPersonID = clsApplication.FindBaseApplication(_selectedLicense.ApplicationID).ApplicantPersonID;
            app.ApplicationDate = DateTime.Now;
            app.ApplicationTypeID = 3;
            app.ApplicationStatus = clsApplication.enApplicationStatus.New;
            app.LastStatusDate = DateTime.Now;
            app.PaidFees = clsApplicationType.Find(6).Fees;
            app.CreatedByUserID = clsGlobal.CurrentUser.UserID;


            //Fill International License
            clsInternationalLicense internationalLicense = new clsInternationalLicense();
            //fill international licnse info to added it in system
            internationalLicense.ApplicationID = _selectedLicense.ApplicationID;
            internationalLicense.DriverID = _selectedLicense.DriverID;
            internationalLicense.IssuedUsingLocalLicenseID = _selectedLicense.LicenseID;
            internationalLicense.IssueDate = DateTime.Now;
            internationalLicense.ExpirationDate = internationalLicense.IssueDate.AddYears(10);
            internationalLicense.IsActive = true;
            internationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (app == null || internationalLicense == null) 
            {
                MessageBox.Show($"Cannot added null data in system.",
                         "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }


            if (app.Save())
            {

                if (internationalLicense.Save())
                {
                    MessageBox.Show($"International License Issued Successfully With ID = {internationalLicense.InternationalLicenseID}.",
                             "License Lssued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _I_L_AppID = internationalLicense.InternationalLicenseID;

                    app.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                    app.LastStatusDate = DateTime.Now;
                    app.Save();


                    _App = app;
                    _LoadControlInfo_AfterIssued();

                }
                else
                {
                    app.Delete();
                    MessageBox.Show($"International License Not saved.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show($"Application Not saved.",
               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            MessageBox.Show($"Application Not saved.",
            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }


         */


    }




    }

