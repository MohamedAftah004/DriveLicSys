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

namespace DVLD.Drivers
{
    public partial class frmShowDriverLicenseHistory : Form
    {

        private int _DriverID;
        private int _PersonID;
        private clsDriver _driver;
       
        public frmShowDriverLicenseHistory(clsDriver driver)
        {
            InitializeComponent();

            _driver = driver;
            //_LDLApp = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LDLAppID);
            _DriverID = _driver.DriverID;
            _PersonID = _driver.PersonID ;
        }



        private void _FillFormInfo()
        {
            ctrlPersonCardWithFilter1.FilterEnabled = false;
            ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonID);

            ctrlDriverLicensesHistory1.LoadControlInfo(_driver);
        }


        private void frmShowDriverLicenseHistory_Load(object sender, EventArgs e)
        {
            _FillFormInfo();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlDriverLicensesHistory1_Load(object sender, EventArgs e)
        {

        }
    }
}
