using DVLD.Applications;
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
    public partial class frmManageTestTypes : Form
    {
        private DataTable _dtAllTestTypes;

        public frmManageTestTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        //Load Data
        private void _LoadData()
        {
            dgvListTestTypes.DataSource = clsTestType.GetAllTestTypes();
            lblNumOfRecordsResult.Text = dgvListTestTypes.RowCount.ToString();

            dgvListTestTypes.Columns[0].Width = 100; // عرض عمود الـ ID
            dgvListTestTypes.Columns[1].Width = 185; // عرض عمود الـ Name
            dgvListTestTypes.Columns[2].Width = 420; // عرض عمود الـ Name +11
            dgvListTestTypes.Columns[3].Width = 148; // عرض عمود الـ Salary
        }


        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateTestType frm = new frmUpdateTestType((int)dgvListTestTypes.CurrentRow.Cells[0].Value);
            frm.DataBack += _RefreshForm_DataBack;
            frm.ShowDialog();

        }

        private void _RefreshForm_DataBack(object sender)
        {
            _LoadData();
        }

    }
}
