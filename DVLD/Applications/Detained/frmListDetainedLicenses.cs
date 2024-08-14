using DVLD.Applications.Detained.Controls;
using DVLD.Drivers;
using DVLD.Licenses;
using DVLD.People;
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

namespace DVLD.Applications.Detained
{
    public partial class frmListDetainedLicenses : Form
    {
        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        private DataTable _dtAllLocalDrivingLicenseApplications;

        private void _SetDGV()
        {

        }

        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            _dtAllLocalDrivingLicenseApplications = clsDetainedLicense.ListDetainedLicenses();
            dgvListDetainedLicenses.DataSource = _dtAllLocalDrivingLicenseApplications;

            lblRecordsCount.Text = dgvListDetainedLicenses.Rows.Count.ToString();
            if (dgvListDetainedLicenses.Rows.Count > 0)
            {

                dgvListDetainedLicenses.Columns[0].HeaderText = "D.ID";
                dgvListDetainedLicenses.Columns[0].Width = 100;

                dgvListDetainedLicenses.Columns[1].HeaderText = "L.ID";
                dgvListDetainedLicenses.Columns[1].Width = 100;

                dgvListDetainedLicenses.Columns[2].HeaderText = "D.Date";
                dgvListDetainedLicenses.Columns[2].Width = 180;

                dgvListDetainedLicenses.Columns[3].HeaderText = "Is Released";
                dgvListDetainedLicenses.Columns[3].Width = 100;

                dgvListDetainedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvListDetainedLicenses.Columns[4].Width = 100;

                dgvListDetainedLicenses.Columns[5].HeaderText = "Released Date";
                dgvListDetainedLicenses.Columns[5].Width = 180;

                dgvListDetainedLicenses.Columns[6].HeaderText = "N.No.";
                dgvListDetainedLicenses.Columns[6].Width = 100;

                dgvListDetainedLicenses.Columns[7].HeaderText = "Full Name";
                dgvListDetainedLicenses.Columns[7].Width = 330;

                dgvListDetainedLicenses.Columns[8].HeaderText = "Release App.ID";
                dgvListDetainedLicenses.Columns[8].Width = 140;
            }

            cbFilterBy.SelectedIndex = 0;

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
            lblRecordsCount.Text = dgvListDetainedLicenses.Rows.Count.ToString();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            /*
             None
Detain ID
Is Released
National No.
Full Name
Release Application ID
             */

            switch (cbFilterBy.Text)
            {

                case "Detain ID":
                    FilterColumn = "DetainID";
                    break;

                case "Is Released":
                    FilterColumn = "IsReleased";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Release Application ID":
                    FilterColumn = "ReleaseApplicationID";
                    break;


                default:
                    FilterColumn = "None";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvListDetainedLicenses.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "DetainID" || FilterColumn == "ReleaseApplicationID")
                //in this case we deal with integer not string.
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = dgvListDetainedLicenses.Rows.Count.ToString();

        }

        private void _RefreshDGV(object sender)
        {
            _dtAllLocalDrivingLicenseApplications = clsDetainedLicense.ListDetainedLicenses();
            dgvListDetainedLicenses.DataSource = _dtAllLocalDrivingLicenseApplications; 
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int licenseID = Convert.ToInt16(dgvListDetainedLicenses.CurrentRow.Cells[1].Value);
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense(licenseID);
            frm.DataBack += _RefreshDGV;
            frm.ShowDialog();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.DataBack += _RefreshDGV;
            frm.ShowDialog();
        }

        private void btnDetaine_Click(object sender, EventArgs e)
        {
            frmDetainedLicense frm = new frmDetainedLicense();
            frm.DataBack += _RefreshDGV;
            frm.ShowDialog();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int _personID = clsApplication.FindBaseApplication(clsLicense.Find(Convert.ToInt16(dgvListDetainedLicenses.CurrentRow.Cells[1].Value)).ApplicationID).ApplicantPersonID;
            frmShowPersonInfo frm = new frmShowPersonInfo(_personID);
            frm.ShowDialog();
        }

        //private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    //int LicenseID = Convert.ToInt16(dgvListDetainedLicenses.CurrentRow.Cells[1].Value);

        //    clsLicense selectedLicense = clsLicense.Find(Convert.ToInt16(dgvListDetainedLicenses.CurrentRow.Cells[1].Value));

        //    clsLocalDrivingLicenseApplication LDLicense = clsLocalDrivingLicenseApplication.FindByApplicationID(selectedLicense.ApplicationID);

        //    if(LDLicense == null)
        //    {
        //        MessageBox.Show($"Warning ..\n*Selected License with ID={selectedLicense} was Not Local License.\n\n*Can't Display this license." , "Not Allowed" , MessageBoxButtons.OK , MessageBoxIcon.Error);
        //        return;
        //    }

        //    frmShowLicense frm = new frmShowLicense(LDLicense.ApplicationID);
        //    frm.ShowDialog();
        //}

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int licenseID = Convert.ToInt16(dgvListDetainedLicenses.CurrentRow.Cells[1].Value);
            clsDriver selectedDriver = clsDriver.Find(clsLicense.Find(licenseID).DriverID);
            frmShowDriverLicenseHistory frm = new frmShowDriverLicenseHistory(selectedDriver);
            frm.ShowDialog();
        }
    }
}
