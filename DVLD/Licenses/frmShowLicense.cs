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

namespace DVLD.Licenses
{
    public partial class frmShowLicense : Form
    {

        public frmShowLicense(int LDLAppID)
        {
            InitializeComponent();

            ctrlShowLienseInfo1.LoadControlInfo(LDLAppID);
        }

        public frmShowLicense(clsLicense License)
        {
            InitializeComponent();

            ctrlShowLienseInfo1.LoadControlInfo(License);
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }




        private void frmShowLicense_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
