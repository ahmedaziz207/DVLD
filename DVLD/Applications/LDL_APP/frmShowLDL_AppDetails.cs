using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmShowLDL_AppDetails : Form
    {
        clsLDL_Application _LDLapp;
        clsApplication _app;
        public frmShowLDL_AppDetails(clsLDL_Application LDLapp,clsApplication app)
        {
            InitializeComponent();
            _LDLapp = LDLapp;
            _app = app;
        }

        private void frmShowLDL_AppDetails_Load(object sender, EventArgs e)
        {
            cntlDLApplicationInfo1.LoadDLApplicationInfo(_LDLapp, _app, clsTest.CountPassedTestsfromThree(_LDLapp.LocalDrivingLicenseApplicationID));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
