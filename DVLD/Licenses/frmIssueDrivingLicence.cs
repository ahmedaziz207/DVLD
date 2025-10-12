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
    public partial class frmIssueDrivingLicence : Form
    {
        clsApplication _app;
        clsLDL_Application _appLDL;
        public frmIssueDrivingLicence(clsLDL_Application appLDL, clsApplication app)
        {
            InitializeComponent();
            _app = app;
            _appLDL = appLDL;
        }

        private void frmIssueDrivingLicence_Load(object sender, EventArgs e)
        {
            cntlDLApplicationInfo1.LoadDLApplicationInfo(_appLDL, _app, clsTest.CountPassedTestsfromThree(_appLDL.LocalDrivingLicenseApplicationID));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsDriver driver = new clsDriver();
            int driverId = clsDriver.ReturnDriverIDIsExist(_app.ApplicantPersonID);

            if (driverId != -1)
            {
                driver.DriverID = driverId;
            }
            else
            {
                
                driver.PersonID = _app.ApplicantPersonID;
                driver.CreatedByUserID = clsSettings.UserID;
                driver.CreatedDate = DateTime.Now;
                bool savedriver = driver.Save();
            }

            clsLicense license = new clsLicense();
            license.ApplicationID = _app.ApplicationID;
            license.DriverID = driver.DriverID;
            license.LicenseClassID = _appLDL.LicenseClassID;
            license.IssueDate = DateTime.Now;
            license.ExpirationDate = license.IssueDate.AddYears(clsLicenseClass.GetLicenseClassByID(_appLDL.LicenseClassID).DefaultValidityLength);
            license.Notes = txtNotes.Text;
            license.PaidFees = clsLicenseClass.GetLicenseClassByID(_appLDL.LicenseClassID).ClassFees;
            license.IsActive = true;
            license.IssueReason = 1; // FirstTimeIssue
            license.CreatedByUserID = clsSettings.UserID;

            if (license.Save())
            {
                MessageBox.Show($"License Issued Successfully with License ID = {license.LicenseID}", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Failed to Issue License!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _app.ApplicationStatus = 3;
            _app.Save();
            this.Close();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
