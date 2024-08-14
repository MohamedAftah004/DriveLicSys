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

namespace DVLD.Drivers.Controls
{
    public partial class ctrlDriverLicensesHistory : UserControl
    {

        private int _DriverID;
        private clsDriver _driver;
        public ctrlDriverLicensesHistory()
        {
            InitializeComponent();
        }

        public ctrlDriverLicensesHistory(clsDriver Driver)
        {
            InitializeComponent();
            _driver = Driver;
        }




        //1 - Load Local + recordCount           2 - Load International + recordCount    
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


        private void _LoadLocalDrivingLicensesData(int DriverID)
        {
            dgvLocalList.DataSource = clsDriver.GetLocalLicenseHistory(DriverID);

            if (dgvLocalList.Rows.Count > 0)
            {
                dgvLocalList.Columns[0].Width = 130;

                dgvLocalList.Columns[1].Width = 130;

                dgvLocalList.Columns[2].Width = 280;

                dgvLocalList.Columns[3].Width = 180;

                dgvLocalList.Columns[4].Width = 180;

                dgvLocalList.Columns[5].Width = 130;
            }


            lblLocalRecordCount.Text = dgvLocalList.RowCount.ToString();
        }

        private void _LoadInternationalDrivingLicensesData(int DriverID)
        {
            dgvInternationalList.DataSource = clsDriver.GetInternationalLicensesHistory(DriverID);

            if (dgvInternationalList.Rows.Count > 0)
            {
                dgvInternationalList.Columns[0].Width = 160;

                dgvInternationalList.Columns[1].Width = 160;

                dgvInternationalList.Columns[2].Width = 160;

                dgvInternationalList.Columns[3].Width = 200;

                dgvInternationalList.Columns[4].Width = 200;

                dgvInternationalList.Columns[5].Width = 130;
            }

            lblInternationalRecordCount.Text = dgvInternationalList.RowCount.ToString();

        }

        public void LoadControlInfo(clsDriver driver)
        {
            _driver = driver;
            _LoadLocalDrivingLicensesData(driver.DriverID);
            _SetDGV(dgvLocalList);
        }


        //SetDgv
        private void _SetDGV(DataGridView dgv)
        {
            if(dgv.Rows.Count > 0)
            {
                dgv.Columns[0].Width = 130;

                dgv.Columns[1].Width = 130;

                dgv.Columns[2].Width = 280;

                dgv.Columns[3].Width = 180;

                dgv.Columns[4].Width = 180;

                dgv.Columns[5].Width = 130;
            }

        }


        private void ctrlDriverLicensesHistory_Load(object sender, EventArgs e)
        {
        }


        private void tcLicensesHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcLicensesHistory.SelectedTab == tcLicensesHistory.TabPages["tbLocal"])
            {
                _LoadLocalDrivingLicensesData(_driver.DriverID);
            }
            else if(tcLicensesHistory.SelectedTab == tcLicensesHistory.TabPages["tbInternational"])
            {
                _LoadInternationalDrivingLicensesData(_driver.DriverID);
            }

        }

        private void tbInternational_Click(object sender, EventArgs e)
        {

        }
    }
}
