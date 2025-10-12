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
    public partial class cntlDLApplicationInfo : UserControl
    {
        clsPerson _person;
        clsLDL_Application _appLDL;
        public cntlDLApplicationInfo()
        {
            InitializeComponent();
        }

        public void RefreshPassedTests()
        {
            lblPassedTests.Text = $"{clsTest.CountPassedTestsfromThree(_appLDL.LocalDrivingLicenseApplicationID)}/3";
        }
        public void LoadDLApplicationInfo(clsLDL_Application appLDL, clsApplication app, int passedtests)
        {
            _appLDL = appLDL;
            _person = clsPerson.GetPersonByID(app.ApplicantPersonID);

            //L.D.L.App Info
            lblDLappId.Text = appLDL.LocalDrivingLicenseApplicationID.ToString();
            lblLicenseClass.Text = clsLicenseClass.GetLicenseClassByID(appLDL.LicenseClassID).ClassName;
            lblPassedTests.Text = $"{passedtests}/3";

            //ApplicationInfo
            
            lblAppId.Text = app.ApplicationID.ToString();
            switch (app.ApplicationStatus)
            {
                case 1:
                    lblStatus.Text = "New";
                    break;
                case 2:
                    lblStatus.Text = "Cancelled";
                    break;
                case 3:
                    lblStatus.Text = "Completed";
                    break;
            }

            lblFees.Text = Convert.ToInt32(app.PaidFees).ToString();
            lblType.Text = clsApplicationType.GetApplicationType(app.ApplicationTypeID).ApplicationTypeTitle;
            lblApplicant.Text = clsPerson.GetPersonByID(app.ApplicantPersonID).FullName();
            lblDate.Text = app.ApplicationDate.ToString("dd/MMM/yyyy");
            lblStatusDate.Text = app.LastStatusDate.ToString("dd/MMM/yyyy");
            lblCreatedByUser.Text = clsUser.GetUserByID(app.CreatedByUserID).UserName;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails(_person);
            frm.ShowDialog();
        }
    }
}
