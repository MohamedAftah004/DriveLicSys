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

namespace DVLD.Tests
{
    public partial class frmTakeTest : Form
    {

        //Data Members
        private string _TestName;
        private string _DLicenseClass;
        private int _TestTypeID;
        private int _LDLAppID;
        private int _Trials;
        private Image _TestImage;
        bool _IsRetakeAppointment;
        bool _IsLocked;
        private int _Result = 0;

          
        //object
        private clsTestAppointment TestAppointment;

        public delegate void DataBackEventHandler(object sender , clsTest Test_DataBack);
        public event DataBackEventHandler DataBack;


        public frmTakeTest()
        {
            InitializeComponent();
        }

        public frmTakeTest(int testAppointmentID, int Trials, Image image, bool IsRetake)
        {
            InitializeComponent();
            TestAppointment = clsTestAppointment.Find(testAppointmentID);

            _TestName = clsTestType.Find(TestAppointment.TestTypeID).TestTypeTitle;
            _DLicenseClass = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(TestAppointment.LocalDrivingLicenseApplicationID).LicenseClassInfo.ClassName;
            _TestTypeID = TestAppointment.TestTypeID;
            _LDLAppID = TestAppointment.LocalDrivingLicenseApplicationID;
            _Trials = Trials;
            _TestImage = image;
            _IsRetakeAppointment = IsRetake;

        }

        private void _FillForm()
        {

            dtpDate.Text = TestAppointment.AppointmentDate.ToShortDateString();

            //name
            this.Text = _TestName;
            lblTitle.Text = _TestName;
            gbTestName.Text = _TestName;
            pbTestImage.Image = _TestImage;


            lblLDLAppID.Text = _LDLAppID.ToString();
            lblDrivingClass.Text = _DLicenseClass.ToString();
            lblName.Text = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LDLAppID).ApplicantFullName;
            lblTrial.Text = _Trials.ToString();
            lblFees.Text = clsTestType.Find(_TestTypeID).TestTypeFees.ToString();


        }


        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            rbFail.Checked = true;

            _FillForm();
        }

        private void rbPass_CheckedChanged(object sender, EventArgs e)
        {
            _Result = 1;
        }

        private void rbFail_CheckedChanged(object sender, EventArgs e)
        {
            _Result = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            clsTest Test = new clsTest();
            Test.TestAppointmentID = TestAppointment.TestAppointmentID;
            Test.TestResult = rbPass.Checked;
            Test.Notes = txtNotes.Text;
            Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;


            if(Test == null)
            {
                MessageBox.Show("Object was null", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(MessageBox.Show("Are you sure you want to save? After that you cannot change the Pass/Fail results after you save?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {


                if (Test.Save())
                {


                    TestAppointment.IsLocked = true;
                    TestAppointment.Save();

                    lblTestID.Text = Test.TestID.ToString();

                    MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    DataBack?.Invoke(this, Test);
                    btnSave.Enabled = false;
                    return;

                }
                else
                {
                    MessageBox.Show("Data Not Saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

            }

        }
    }
}
