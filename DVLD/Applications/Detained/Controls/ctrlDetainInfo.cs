using DVLD.Classes;
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
    public partial class ctrlDetainInfo : UserControl
    {
        public ctrlDetainInfo()
        {
            InitializeComponent();
        }

        public void LoadBeforeDetained()
        {

            lblDetainID.Text = "[???]";
            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            lblLicenseID.Text = "[???]";
            lblCreatedBy.Text = "[???]";
            
        }

        public void LoadAfterDetained(int detainedID , int licenseID , string createdBy )
        {
            lblDetainID.Text = detainedID.ToString();
            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            lblLicenseID.Text = licenseID.ToString();
            lblCreatedBy.Text = createdBy;
        }


        private void ctrlDetainInfo_Load(object sender, EventArgs e)
        {
        }

        private void gbDetainInfo_Enter(object sender, EventArgs e)
        {

        }
    }
}
