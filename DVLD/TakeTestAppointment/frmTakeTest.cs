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
    public partial class frmTakeTest : Form
    {
        clsTest _Test;

        clsApplication _app;
        clsLDL_Application _LDLApp;
        clsTestAppointment _TestAppointment;
        int _TestTypeID;
        public frmTakeTest(clsLDL_Application LDL_app, clsApplication app, int TestTypeID, clsTestAppointment TestAppointment)
        {
            InitializeComponent();
            _LDLApp = LDL_app;
            _app = app;
            _TestAppointment = TestAppointment;
            _TestTypeID = TestTypeID;
        }

        private void _LoadTestTypeImageAndTitle()
        {
            switch (_TestTypeID)
            {
                case 1:
                    {
                        groupBox1.Text = "Vision Test";
                        pictureBox1.ImageLocation = @"D:\My Projects C#\DVLD\Icons\Vision 512.png";
                        break;
                    }

                case 2:
                    {
                        groupBox1.Text = "Written Test";
                        pictureBox1.ImageLocation = @"D:\My Projects C#\DVLD\Icons\Written Test 512.png";
                        break;
                    }
                case 3:
                    {
                        groupBox1.Text = "Street Test";
                        pictureBox1.ImageLocation = @"D:\My Projects C#\DVLD\Icons\driving-test 512.png";
                        break;
                    }
            }
        }
        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            _LoadTestTypeImageAndTitle();

            lblDLappId.Text = _LDLApp.LocalDrivingLicenseApplicationID.ToString();
            lblDClass.Text = clsLicenseClass.GetLicenseClassByID(_LDLApp.LicenseClassID).ClassName;
            lblName.Text = clsPerson.GetPersonByID(_app.ApplicantPersonID).FullName();
            lblTrial.Text = clsTest.CountTrialsPerTest(_LDLApp.LocalDrivingLicenseApplicationID, _TestTypeID).ToString();
            lblfees.Text = Convert.ToInt32(clsTestType.GetTestType(_TestTypeID).TestTypeFees).ToString();
            lblDate.Text = _TestAppointment.AppointmentDate.ToShortDateString();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _Test = new clsTest();
            _Test.TestAppointmentID = _TestAppointment.TestAppointmentID;
            _Test.Notes = txtnotes.Text;
            _Test.CreatedByUserID = clsSettings.UserID;

            if(rbPass.Checked)
                _Test.TestResult = true;
            else
                _Test.TestResult = false;

            _TestAppointment.IsLocked = true;
            
            if(MessageBox.Show("Are you Sure you want to Save? After that you Cannot change the Pass/Fail Results!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (_Test.Save() && _TestAppointment.Save())
                {
                    MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to Save!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
