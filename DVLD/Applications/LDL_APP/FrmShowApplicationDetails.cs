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
    public partial class FrmShowApplicationDetails : Form
    {
        clsLDL_Application _appLDL;
        clsApplication _app;
        public FrmShowApplicationDetails(clsLDL_Application LDLapp,clsApplication app)
        {
            InitializeComponent();
            _appLDL = LDLapp;
             _app = app;   
        }

        private void FrmShowApplicationDetails_Load(object sender, EventArgs e)
        {
            cntlDLApplicationInfo1.LoadDLApplicationInfo(_appLDL, _app, clsTest.CountPassedTestsfromThree(_appLDL.LocalDrivingLicenseApplicationID));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
