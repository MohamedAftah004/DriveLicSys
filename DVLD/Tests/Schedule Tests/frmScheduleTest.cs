using DVLD.Classes;
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

namespace DVLD.Tests.Schedule_Tests
{
    public partial class frmScheduleTest : Form
    {

        private string _TestName;
        private string _DLicenseClass;
        private int _TestTypeID;
        private int _LDLAppID;
        private int _Trials;
        private Image _TestImage;
        bool _IsRetakeAppointment;
        bool _IsLocked;

        private DateTime _LastDateTime;
        //declare using delegate fail = 0 , pass = 1 , not taked = -1
        int _Result = -1;
        //object
        private clsTestAppointment TestAppointment;

        enum enMode { AddNew = 0 , Update = 1}
        private enMode Mode = enMode.AddNew;


        public delegate void DataBackEventHandler(object sender);
        public event DataBackEventHandler DataBack;
        public frmScheduleTest()
        {
            InitializeComponent();
            Mode = enMode.AddNew;
        }

        public frmScheduleTest(int testAppointmentID , int Trials , Image image , bool IsRetake)
        {
            InitializeComponent();
            Mode = enMode.Update;
            TestAppointment = clsTestAppointment.Find(testAppointmentID);

            _TestName = clsTestType.Find(TestAppointment.TestTypeID).TestTypeTitle ;
            _DLicenseClass = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(TestAppointment.LocalDrivingLicenseApplicationID).LicenseClassInfo.ClassName;
            _TestTypeID = TestAppointment.TestTypeID;
            _LDLAppID = TestAppointment.LocalDrivingLicenseApplicationID;
            _Trials = Trials;
            _TestImage = image;
            _IsRetakeAppointment = IsRetake;

            _FillForm();
        }

        //for closed
        public frmScheduleTest(int testAppointmentID , int Trials, Image image, bool IsRetake , string Message)
        {
            InitializeComponent();
            Mode = enMode.Update;
            TestAppointment = clsTestAppointment.Find(testAppointmentID);

            _TestName = clsTestType.Find(TestAppointment.TestTypeID).TestTypeTitle;
            _DLicenseClass = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(TestAppointment.LocalDrivingLicenseApplicationID).LicenseClassInfo.ClassName;
            _TestTypeID = TestAppointment.TestTypeID;
            _LDLAppID = TestAppointment.LocalDrivingLicenseApplicationID;
            _Trials = Trials;
            _TestImage = image;
            _IsRetakeAppointment = IsRetake;
            this.lblMessage.Text = Message;

            _FillForm();
            dtpDate.Enabled = false;
            btnSave.Enabled = false;
        }



        // add new
        public frmScheduleTest(string TestName , string DLicenseClass , int TestTypeID , int LDLAppID , int Trials , Image testImage , bool isRetakeAppointment  )
        {
            InitializeComponent();

            Mode = enMode.AddNew;
            _TestName = TestName;
            _DLicenseClass = DLicenseClass;
            _TestTypeID = TestTypeID;
            _LDLAppID = LDLAppID;
            _Trials = Trials;
            _TestImage = testImage; 
            _IsRetakeAppointment = isRetakeAppointment;

            _FillForm();
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
        }

        //Load Form
        private void _FillForm()
        {
            //date + 5 days
            if (Mode == enMode.AddNew)
                dtpDate.Value = DateTime.Today.AddDays(5);
            else
                dtpDate.Value = TestAppointment.AppointmentDate;

            //name
            this.Text = _TestName;
            lblTitle.Text = _TestName;
            gbTestName.Text = _TestName;
            pbTestImage.Image = _TestImage;

            gbRetakeTestInfo.Enabled = _IsRetakeAppointment;

            lblLDLAppID.Text = _LDLAppID.ToString();
            lblDrivingClass.Text = _DLicenseClass.ToString();
            lblName.Text = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LDLAppID).ApplicantFullName;
            lblTrial.Text = _Trials.ToString();

            if (String.IsNullOrEmpty(clsTestType.Find(_TestTypeID).TestTypeFees.ToString()))
            {
                lblFees.Text = "0";
            }
            else
            {
            lblFees.Text = clsTestType.Find(_TestTypeID).TestTypeFees.ToString();
            }

            


            if (gbRetakeTestInfo.Enabled)
            {
                lblRetakeAppFees.Text = "5";
                lblTotalRetakeAppFees.Text = (Convert.ToDecimal(lblFees.Text) + 5).ToString();
            }
            else
                lblTotalRetakeAppFees.Text = lblFees.Text;

        }




        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gbTestName_Enter(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if(Mode == enMode.AddNew)
            {
                //Fill and add Test Appointment 
                clsTestAppointment testAppointment = new clsTestAppointment();

                testAppointment.TestTypeID = _TestTypeID;
                testAppointment.LocalDrivingLicenseApplicationID = _LDLAppID;
                testAppointment.AppointmentDate = dtpDate.Value;
                testAppointment.PaidFees = Convert.ToDecimal(lblTotalRetakeAppFees.Text);
                testAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                testAppointment.IsLocked = false;

                if (testAppointment.Save())
                {
                    MessageBox.Show("Appointment Added Successfully.", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSave.Enabled = false;
                    DataBack?.Invoke(this);
                    return;
                }
                else
                {
                    MessageBox.Show("Appointment Not Added.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            else
            {

                TestAppointment.AppointmentDate = dtpDate.Value;

                if (TestAppointment.Save())
                {
                    MessageBox.Show("Appointment Updated Successfully.", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataBack?.Invoke(this);
                    return;
                }
                else
                {
                    MessageBox.Show("Appointment Not Updated.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }


        }
    }
}
