using DVLD.UI.Classes;
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


    public partial class frmUpdateTestType : Form
    {


        
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




        

    }
}
