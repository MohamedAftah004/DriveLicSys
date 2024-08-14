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


    public partial class frmUpdateTestType : Form
    {


        //Delegate to refresh manage form by updated data
        public delegate void DataBackEventHandler(object sender);
        public event DataBackEventHandler DataBack;

        private clsTestType TestType;
        private int _testTypeID;

        public frmUpdateTestType(int testTypeID)
        {
            InitializeComponent();
            _testTypeID = testTypeID;
        }

        private void _LoadData()
        {
            TestType = clsTestType.Find(_testTypeID);

            if (TestType != null)
            {
                
                lblIDResult.Text = TestType.TestTypeID.ToString();

                txtTitle.Text = TestType.TestTypeTitle;
                txtDescription.Text = TestType.TestTypeDescription;
                txtFees.Text = TestType.TestTypeFees.ToString();
                return;
            }
            else
                MessageBox.Show("Error On Search", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtTitle.Text) || String.IsNullOrEmpty(txtDescription.Text) || String.IsNullOrEmpty(txtFees.Text))
            {
                MessageBox.Show("Some Fields are Empty.", "Error Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clsTestType.UpdateTestType(TestType.TestTypeID ,txtTitle.Text , txtDescription.Text , Convert.ToDecimal(txtFees.Text)))
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //After This Event can do any think at the prev form (Manage Tests)
                DataBack?.Invoke(this);
                this.Close();
                return;
            }
            else
            {
                MessageBox.Show("Can't Save Data.", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void frmUpdateTestType_Load(object sender, EventArgs e)
        {
            _LoadData(); 
        }




        /*
         
         
        private void frmUpdateTestType_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Loading Now.", "...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            
            if (String.IsNullOrEmpty(txtTitle.Text) || String.IsNullOrEmpty(txtDescription.Text) || String.IsNullOrEmpty(txtFees.Text))
            {
                MessageBox.Show("Some Fields are Empty.", "Error Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (TestType.UpdateTestType())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //After This Event can do any think at the prev form (Manage Tests)
                DataBack?.Invoke(this);
                this.Close();
                return;
            }
            else
            {
                MessageBox.Show("Can't Save Data.", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void frmUpdateTestType_Load_1(object sender, EventArgs e)
        {

        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {

        }

         */

    }
}
