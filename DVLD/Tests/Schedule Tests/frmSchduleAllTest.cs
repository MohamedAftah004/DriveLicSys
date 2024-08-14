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

namespace DVLD.Tests.Schedule_Tests
{
    public partial class frmSchduleAllTest : Form
    {

        private int _LocalDrivingLicenseApplicationID;
        private int _TestTypeID;
        private string _FormTitle;
        private int _PassedTests;

        private bool _HasCurrentAppointment = false;

        private clsTest _Test;

        //public frmSchduleVisionTest()
        //{
        //    InitializeComponent();
        //}

        public delegate void DataBackEventHandler(object sender);
        public event DataBackEventHandler DataBack; 

        enum enTestType { Vission = 1 , Written = 2 , Street = 3}
        private enTestType eTestType = enTestType.Vission;

        private void ChooseTestType(int testType)
        {
            switch (testType)
            {
                case 1:
                    eTestType = enTestType.Vission; break;
                case 2:
                    eTestType = enTestType.Written; break;
                case 3:
                    eTestType = enTestType.Street; break;
            }
        }

        public frmSchduleAllTest(string Title, int TestTypeID ,  int LDLAppID , int PassedTests)
        {

            InitializeComponent();

            _FormTitle = Title;
            _TestTypeID = TestTypeID;
            _LocalDrivingLicenseApplicationID = LDLAppID;
            _PassedTests = PassedTests;

            ChooseTestType(TestTypeID);

            _FillControl();

        }

        private void _FillControl()
        {

            switch (eTestType)
            {
                case enTestType.Vission:
                    ctrlDrivingLicenseApplicationInfo1.FillControl(_LocalDrivingLicenseApplicationID, 0);
                    break;

                case enTestType.Written:
                    ctrlDrivingLicenseApplicationInfo1.FillControl(_LocalDrivingLicenseApplicationID, 1);
                    break;

                case enTestType.Street:
                    ctrlDrivingLicenseApplicationInfo1.FillControl(_LocalDrivingLicenseApplicationID, 2);
                    break;
            }

        }

        //set picture box - test
        private void _SetPictureBox()
        {
            
            switch(eTestType)
            {
                case enTestType.Vission:
                    pbTestImage.Image = Resources.Vision_512;
                    break;

                case enTestType.Written:
                    pbTestImage.Image = Resources.Written_Test_512;
                    break;

                case enTestType.Street:
                    pbTestImage.Image = Resources.driving_test_512;
                    break;
            }
        }

        //Load Dgv by Test Type
        private void _LoadDGVByTestType()
        {
            switch (eTestType)
            {
                case enTestType.Vission:
                    dgvTestAppointments.DataSource = clsTestAppointment.GetAllTestAppointmentsForSelected_LDLAppID_TestType(_LocalDrivingLicenseApplicationID, 1);
                    break;

                case enTestType.Written:
                    dgvTestAppointments.DataSource = clsTestAppointment.GetAllTestAppointmentsForSelected_LDLAppID_TestType(_LocalDrivingLicenseApplicationID, 2);
                    break;

                case enTestType.Street:
                    dgvTestAppointments.DataSource = clsTestAppointment.GetAllTestAppointmentsForSelected_LDLAppID_TestType(_LocalDrivingLicenseApplicationID, 3);
                    break;
            }

            lblRecordsCount.Text = dgvTestAppointments.RowCount.ToString(); 
        }

        private void frmSchduleVisionTest_Load(object sender, EventArgs e)
        {
            lblTitle.Text = _FormTitle;
            _SetPictureBox();
            _FillControl();

            _LoadDGVByTestType();
            //
            if (dgvTestAppointments.Rows.Count > 0)
            {
                dgvTestAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvTestAppointments.Columns[0].Width = 150;

                dgvTestAppointments.Columns[1].HeaderText = "Appointment Data";
                dgvTestAppointments.Columns[1].Width = 200;

                dgvTestAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvTestAppointments.Columns[2].Width = 150;

                dgvTestAppointments.Columns[3].HeaderText = "Is Locked";
                dgvTestAppointments.Columns[3].Width = 150;
                //
            }

            lblRecordsCount.Text = dgvTestAppointments.RowCount.ToString();

            if (clsTest.isTestExist(_LocalDrivingLicenseApplicationID, _TestTypeID))
            {
                btnAdd.Enabled = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (_IsHasActiveAppointment(_TestTypeID))
            {
                MessageBox.Show("Person Already have an active appointment for this test, You cannot add new appointment", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

           // string testName = clsTestType.Find(_TestTypeID).TestTypeTitle;
            string _DLClassName = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID).LicenseClassInfo.ClassName.ToString();
            int trials = dgvTestAppointments.RowCount;
            bool isRetakeAppointment = trials > 1;
            
            frmScheduleTest frm = new frmScheduleTest(_FormTitle, _DLClassName, _TestTypeID , _LocalDrivingLicenseApplicationID , trials, pbTestImage.Image  ,isRetakeAppointment);
            frm.DataBack += LoadForm_DataBack;
            frm.ShowDialog();
        }

        //LoadDGv_DataBack
        private void LoadForm_DataBack(object sender)
        {
            dgvTestAppointments.DataSource = clsTestAppointment.GetAllTestAppointmentsForSelected_LDLAppID_TestType(_LocalDrivingLicenseApplicationID, _TestTypeID);
           
            if (clsTest.isTestExist(_LocalDrivingLicenseApplicationID, _TestTypeID))
            {
                btnAdd.Enabled = false;
            }
        }

        //check has appointment active
        private bool _IsHasActiveAppointment(int TestType)
        {
            DataTable dt = clsTestAppointment.GetAllTestAppointmentsForSelected_LDLAppID_TestType(_LocalDrivingLicenseApplicationID, TestType);
            foreach (DataRow col in dt.Rows)
            {
                if (Convert.ToInt16(col["IsLocked"]) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        private bool _IsLockedAppointment()
        {
            if ((bool)dgvTestAppointments.CurrentRow.Cells[3].Value)
            {
                return true;
            }
            return false;
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            DataBack?.Invoke(this);
            this.Close();
        }


        //Edit Every Test Type
        private void _EditFor_VisionTest()
        {
            if (!_IsLockedAppointment())
            {
                int rowCount = dgvTestAppointments.RowCount;
                frmScheduleTest frm = new frmScheduleTest((int)dgvTestAppointments.CurrentRow.Cells[0].Value, rowCount, pbTestImage.Image, rowCount > 1);
                frm.DataBack += LoadForm_DataBack;
                frm.ShowDialog();
            }
            else
            {
                int rowCount = dgvTestAppointments.RowCount;
                //int testAppointmentID, int Trials, Image image, bool IsRetake
                frmScheduleTest frm = new frmScheduleTest((int)dgvTestAppointments.CurrentRow.Cells[0].Value, rowCount, pbTestImage.Image, rowCount > 1, "Person already sat for the test, appointment locked.");
                frm.ShowDialog();
            }

        }


        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_IsLockedAppointment())
            {
                int rowCount = dgvTestAppointments.RowCount;
                frmScheduleTest frm = new frmScheduleTest((int)dgvTestAppointments.CurrentRow.Cells[0].Value, rowCount, pbTestImage.Image, rowCount > 1);
                frm.DataBack += LoadForm_DataBack;
                frm.ShowDialog();
            }
            else
            {
                int rowCount = dgvTestAppointments.RowCount;
                //int testAppointmentID, int Trials, Image image, bool IsRetake
                frmScheduleTest frm = new frmScheduleTest((int)dgvTestAppointments.CurrentRow.Cells[0].Value, rowCount, pbTestImage.Image, rowCount > 1, "Person already sat for the test, appointment locked.");
                frm.ShowDialog();
            }
        }

        private void TakeTest_DataBack(object sender , clsTest Test_DataBack)
        {
            dgvTestAppointments.DataSource = clsTestAppointment.GetAllTestAppointmentsForSelected_LDLAppID_TestType(_LocalDrivingLicenseApplicationID, _TestTypeID);
            _Test = Test_DataBack;

            if (clsTest.isTestExist(_LocalDrivingLicenseApplicationID, _TestTypeID))
            {
                btnAdd.Enabled = false;
            }

        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if(!_IsLockedAppointment())
            {
                int rowCount = dgvTestAppointments.RowCount;
                //int testAppointmentID, int Trials, Image image, bool IsRetake
                frmTakeTest frm = new frmTakeTest((int)dgvTestAppointments.CurrentRow.Cells[0].Value, rowCount, pbTestImage.Image, rowCount > 1);
                frm.DataBack += TakeTest_DataBack;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Person Already taken this Appointment", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Person already sat for the test, appointment locked.
        }
    }
}
