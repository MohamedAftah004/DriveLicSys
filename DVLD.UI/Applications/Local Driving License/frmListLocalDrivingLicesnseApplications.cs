using DVLD.UI.Applications;
using DVLD.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;
using DVLD.UI.Tests.Schedule_Tests;
using DVLD.UI.Licenses;
using DVLD.UI.Drivers;
using System.Data.Odbc;

namespace DVLD.UI.Tests
{
    public partial class frmListLocalDrivingLicesnseApplications : Form
    {
        private DataTable _dtAllLocalDrivingLicenseApplications;
        public frmListLocalDrivingLicesnseApplications()
        {
            InitializeComponent();
        }

        private int _PassedTests;

        private void frmListLocalDrivingLicesnseApplications_Load(object sender, EventArgs e)
        {
            _dtAllLocalDrivingLicenseApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvLocalDrivingLicenseApplications.DataSource = _dtAllLocalDrivingLicenseApplications;
            
            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
            if (dgvLocalDrivingLicenseApplications.Rows.Count>0)
            {

                dgvLocalDrivingLicenseApplications.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLocalDrivingLicenseApplications.Columns[0].Width = 120;

                dgvLocalDrivingLicenseApplications.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplications.Columns[1].Width = 300;

                dgvLocalDrivingLicenseApplications.Columns[2].HeaderText = "National No.";
                dgvLocalDrivingLicenseApplications.Columns[2].Width = 150;

                dgvLocalDrivingLicenseApplications.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplications.Columns[3].Width = 350;

                dgvLocalDrivingLicenseApplications.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplications.Columns[4].Width = 170;

                dgvLocalDrivingLicenseApplications.Columns[5].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenseApplications.Columns[5].Width = 150;
            }

            cbFilterBy.SelectedIndex = 0;


        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            frmLocalDrivingLicenseApplicationInfo frm =
                        new frmLocalDrivingLicenseApplicationInfo((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value , _PassedTests);
            frm.ShowDialog();
            
            frmListLocalDrivingLicesnseApplications_Load(null, null);

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

            _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {

            string FilterColumn = "";
            
            switch (cbFilterBy.Text)
            {

                case "L.D.L.AppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Status":
                    FilterColumn = "Status";
                    break;


                default:
                    FilterColumn = "None";
                    break;

            }

            
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "LocalDrivingLicenseApplicationID")
                
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            frmAddUpdateLocalDrivingLicesnseApplication frm =
                         new frmAddUpdateLocalDrivingLicesnseApplication(LocalDrivingLicenseApplicationID);
            frm.ShowDialog();

            frmListLocalDrivingLicesnseApplications_Load(null, null);
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (cbFilterBy.Text == "L.D.L.AppID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void vistionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {



        }

        private void writtenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void streetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }


        
        private void _Reload_DataBack(object sender)
        {
            frmListLocalDrivingLicesnseApplications_Load(null, null);
        }
      
        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSchduleAllTest frm = new frmSchduleAllTest("Vission Test Appointment" , 1 ,
                (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value , 0);
            frm.DataBack += _Reload_DataBack;
            frm.ShowDialog();

        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            
            frmSchduleAllTest frm = new frmSchduleAllTest("Written Test Appointment", 2,
            (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value, 1);

            frm.DataBack += _Reload_DataBack;
            frm.ShowDialog();


        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmSchduleAllTest frm = new frmSchduleAllTest("Street Test Appointment", 3,
            (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value, 2);
            frm.DataBack += _Reload_DataBack;
            frm.ShowDialog();


        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicesnseApplication frm = new frmAddUpdateLocalDrivingLicesnseApplication();
            frm.ShowDialog();
            
            frmListLocalDrivingLicesnseApplications_Load(null, null);
        }


        
        private void _ConvertStatusAfterIssue_DataBack(object sender)
        {
            clsLocalDrivingLicenseApplication LDLApp = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            clsApplication app = clsApplication.FindBaseApplication(LDLApp.ApplicationID);
            app.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            app.Save();
            _Reload_DataBack(sender);
        }
        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueDrivingLicense_FirstTime frm = new frmIssueDrivingLicense_FirstTime((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm.DataBack += _ConvertStatusAfterIssue_DataBack;
            frm.ShowDialog();
        }

        
        private void _DisabledAllItems()
        {
            showDetailsToolStripMenuItem.Enabled = false;
            editToolStripMenuItem.Enabled = false;
            DeleteApplicationToolStripMenuItem.Enabled = false;
            CancelApplicaitonToolStripMenuItem.Enabled = false;
            ScheduleTestsMenue.Enabled = false;
            showLicenseToolStripMenuItem.Enabled = false;
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
            showPersonLicenseHistoryToolStripMenuItem.Enabled = false;
        }

        
        private void _EnabledOn_Cancelled()
        {
            showDetailsToolStripMenuItem.Enabled = true;
            DeleteApplicationToolStripMenuItem.Enabled = true;
            showPersonLicenseHistoryToolStripMenuItem.Enabled = true;
        }
        
        private void _EnabledOn_Completed()
        {
            showDetailsToolStripMenuItem.Enabled = true;
            showLicenseToolStripMenuItem.Enabled = true;
            showPersonLicenseHistoryToolStripMenuItem.Enabled = true;

        }
        
        private void _EnabledOn_New(int passTests)
        {

            if(passTests == 3)
            {
                showDetailsToolStripMenuItem.Enabled = true;
                editToolStripMenuItem.Enabled = true;
                DeleteApplicationToolStripMenuItem.Enabled = true;
                CancelApplicaitonToolStripMenuItem.Enabled = true;
                ScheduleTestsMenue.Enabled = false;
                showLicenseToolStripMenuItem.Enabled = false;
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = true;
                showPersonLicenseHistoryToolStripMenuItem.Enabled = true;
                return;

            }
            else
            {
                showDetailsToolStripMenuItem.Enabled = true;
                editToolStripMenuItem.Enabled = true;
                DeleteApplicationToolStripMenuItem.Enabled = true;
                CancelApplicaitonToolStripMenuItem.Enabled = true;
                ScheduleTestsMenue.Enabled = true;
                showLicenseToolStripMenuItem.Enabled = false;
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                showPersonLicenseHistoryToolStripMenuItem.Enabled = true;
            }


            switch (passTests)
            {
                case 0:
                    {
                        scheduleVisionTestToolStripMenuItem.Enabled = true;
                        scheduleWrittenTestToolStripMenuItem.Enabled = false;
                        scheduleStreetTestToolStripMenuItem.Enabled = false;
                        break;
                    }

                case 1:
                    {
                        scheduleVisionTestToolStripMenuItem.Enabled = false;
                        scheduleWrittenTestToolStripMenuItem.Enabled = true;
                        scheduleStreetTestToolStripMenuItem.Enabled = false;
                        break;
                    }

                case 2:
                    {
                        scheduleVisionTestToolStripMenuItem.Enabled = false;
                        scheduleWrittenTestToolStripMenuItem.Enabled = false;
                        scheduleStreetTestToolStripMenuItem.Enabled = true;
                        break;
                    }

                
                

                
                
                
            }
        }

        
        private void EnableDisableCMSItems(int passTests , string status)
        {

            _DisabledAllItems();

            switch (status)
            {
                case "Cancelled":
                    _EnabledOn_Cancelled();
                    break;

                case "Completed":
                    _EnabledOn_Completed();
                    break;

                case "New":
                    _EnabledOn_New(passTests);
                    break;

            }

            
        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            if (dgvLocalDrivingLicenseApplications.RowCount < 1)
            {
                e.Cancel = true;
                return;
            }
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            int TotalPassedTests = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[5].Value;
            _PassedTests = TotalPassedTests;

            string Status = (string)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[6].Value;
            
            EnableDisableCMSItems(TotalPassedTests , Status);

        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLicense frm = new frmShowLicense((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void CancelApplicaitonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to cancel this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Cancel())
                {
                    MessageBox.Show("Application Cancelled Successfully.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    frmListLocalDrivingLicesnseApplications_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not cancel applicatoin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DeleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Delete())
                {
                    MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    frmListLocalDrivingLicesnseApplications_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not delete applicatoin, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            

            
            int personID = clsPerson.Find((string)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[2].Value).PersonID;
            clsDriver driver = clsDriver.FindByPersonID(personID);
            if(driver != null)
            {
            frmShowDriverLicenseHistory frm = new frmShowDriverLicenseHistory(driver);
            frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Cannot Load Licenses History, Because not a driver.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void ScheduleTestsMenue_Click(object sender, EventArgs e)
        {

        }
    }
}
