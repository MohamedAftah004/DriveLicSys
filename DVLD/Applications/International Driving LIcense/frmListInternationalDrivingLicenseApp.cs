using DVLD.Drivers;
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

namespace DVLD.Applications.International_Driving_LIcense
{
    public partial class frmListInternationalDrivingLicenseApp : Form
    {
        public frmListInternationalDrivingLicenseApp()
        {
            InitializeComponent();
        }


        private DataTable _dtInternationalLicenseApplications = new DataTable();

        private void frmListInternationalDrivingLicenseApp_Load(object sender, EventArgs e)
        {

            _dtInternationalLicenseApplications = clsApplication.Get_Clearly_AllInternationalApplications();
            dgvInternationalList.DataSource = _dtInternationalLicenseApplications;
        
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Visible = false;

            lblInternationalRecordCount.Text = dgvInternationalList.RowCount.ToString();

            if (dgvInternationalList.Rows.Count > 0)
            {
                dgvInternationalList.Columns[0].Width = 120;
                dgvInternationalList.Columns[0].HeaderText = "Int.License ID";

                dgvInternationalList.Columns[1].Width = 120;
                dgvInternationalList.Columns[1].HeaderText = "Application ID";

                dgvInternationalList.Columns[2].Width = 120;
                dgvInternationalList.Columns[2].HeaderText = "Driver ID";

                dgvInternationalList.Columns[3].Width = 120;
                dgvInternationalList.Columns[3].HeaderText = "L.License ID";

                dgvInternationalList.Columns[4].Width = 190;
                dgvInternationalList.Columns[4].HeaderText = "Issue Date";

                dgvInternationalList.Columns[5].Width = 190;
                dgvInternationalList.Columns[5].HeaderText = "Expiration Date";

                dgvInternationalList.Columns[6].Width = 140;
                dgvInternationalList.Columns[6].HeaderText = "Is Active";

            }

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {

                case "Int.License ID":
                    FilterColumn = "Int.License ID";
                    break;

                case "Application ID":
                    FilterColumn = "Application ID";
                    break;

                case "L.License ID":
                    FilterColumn = "L.License ID";
                    break;

                case "Driver ID":
                    FilterColumn = "Driver ID";
                    break;


                default:
                    FilterColumn = "None";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtInternationalLicenseApplications.DefaultView.RowFilter = "";
                lblInternationalRecordCount.Text = dgvInternationalList.Rows.Count.ToString();
                return;
            }


            _dtInternationalLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());

            //if (FilterColumn == "LocalDrivingLicenseApplicationID")
            //    //in this case we deal with integer not string.
            //    _dtInternationalLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            //else
            //    _dtInternationalLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblInternationalRecordCount.Text = dgvInternationalList.Rows.Count.ToString();

        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo(clsDriver.Find((int)dgvInternationalList.CurrentRow.Cells[2].Value).PersonID);
            frm.ShowDialog();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalLicenseInformation frm = new frmInternationalLicenseInformation(clsInternationalLicense.Find((int)dgvInternationalList.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowDriverLicenseHistory frm = new frmShowDriverLicenseHistory(clsDriver.Find((int)dgvInternationalList.CurrentRow.Cells[2].Value));
            frm.ShowDialog();
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmIssueInternationalLicense frm = new frmIssueInternationalLicense();
            frm.DataBack += _ReloadForm;
            frm.ShowDialog();
        }

        private void _ReloadForm(object sender)
        {
            _dtInternationalLicenseApplications = clsApplication.Get_Clearly_AllInternationalApplications();
            dgvInternationalList.DataSource = _dtInternationalLicenseApplications;

            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Visible = false;


            lblInternationalRecordCount.Text = dgvInternationalList.RowCount.ToString();

            if (dgvInternationalList.Rows.Count > 0)
            {
                dgvInternationalList.Columns[0].Width = 120;
                dgvInternationalList.Columns[0].HeaderText = "Int.License ID";

                dgvInternationalList.Columns[1].Width = 120;
                dgvInternationalList.Columns[1].HeaderText = "Application ID";

                dgvInternationalList.Columns[2].Width = 120;
                dgvInternationalList.Columns[2].HeaderText = "Driver ID";

                dgvInternationalList.Columns[3].Width = 120;
                dgvInternationalList.Columns[3].HeaderText = "L.License ID";

                dgvInternationalList.Columns[4].Width = 190;
                dgvInternationalList.Columns[4].HeaderText = "Issue Date";

                dgvInternationalList.Columns[5].Width = 190;
                dgvInternationalList.Columns[5].HeaderText = "Expiration Date";

                dgvInternationalList.Columns[6].Width = 140;
                dgvInternationalList.Columns[6].HeaderText = "Is Active";

            }


        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedIndex != 0)
                txtFilterValue.Visible = true;
            else
                txtFilterValue.Visible = false;
        }
    }
}
