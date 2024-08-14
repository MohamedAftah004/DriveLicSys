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
    public partial class frmInternationalLicenseInformation : Form
    {
        private clsInternationalLicense _IntLicense;
        public frmInternationalLicenseInformation(clsInternationalLicense IntLicense)
        {
            InitializeComponent();
            _IntLicense = IntLicense;
        }

        private void frmInternationalLicenseInformation_Load(object sender, EventArgs e)
        {
            ctrlInternationalLicenseInfo1.FillControlInfo(_IntLicense);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
