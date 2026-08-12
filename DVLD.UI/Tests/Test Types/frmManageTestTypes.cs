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

namespace DVLD.UI.Tests
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

        
        private void _LoadData()
        {
            dgvListTestTypes.DataSource = clsTestType.GetAllTestTypes();
            lblNumOfRecordsResult.Text = dgvListTestTypes.RowCount.ToString();

            dgvListTestTypes.Columns[0].Width = 100; 
            dgvListTestTypes.Columns[1].Width = 185; 
            dgvListTestTypes.Columns[2].Width = 420; 
            dgvListTestTypes.Columns[3].Width = 148; 
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
