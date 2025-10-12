using DVLD.Properties;
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
    public partial class frmScheduleTest : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode = enMode.AddNew;
        public enum enCreationMode { FirstTimeSchedule = 0, RetakeTestSchedule = 1 };
        private enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;


        int _TestTypeID;
        clsLDL_Application _LDLApp;
        clsApplication _app;
        int _TestAppointmentID;
        clsTestAppointment _TestAppointment;
        clsTestAppointment LastAppointment;
        public frmScheduleTest(clsLDL_Application LDL_app, clsApplication app, int TestTypeID, int AppointmentID = -1)
        {
            InitializeComponent();
            _app = app;
            _LDLApp = LDL_app;
            _TestTypeID = TestTypeID;
            _TestAppointmentID = AppointmentID;

            if(AppointmentID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;
        }

        private void DefineTestType()
        {
            switch (_TestTypeID)
            {
                case 1:
                    {
                        gbTestType.Text = "Vision Test";
                        pictureBox1.ImageLocation = @"D:\My Projects C#\DVLD\Icons\Vision 512.png";
                        break;
                    }

                case 2:
                    {
                        gbTestType.Text = "Written Test";
                        pictureBox1.ImageLocation = @"D:\My Projects C#\DVLD\Icons\Written Test 512.png";
                        break;
                    }
                case 3:
                    {
                        gbTestType.Text = "Street Test";
                        pictureBox1.ImageLocation = @"D:\My Projects C#\DVLD\Icons\driving-test 512.png";
                        break;
                    }
            }
        }
        private bool _LoadTestAppointmentData()
        {
            _TestAppointment = clsTestAppointment.GetTestAppointment(_TestAppointmentID);
            lblFees.Text = Convert.ToInt32(_TestAppointment.PaidFees).ToString();
            LastAppointment = clsTestAppointment.GetLastTestAppointmentOverAll(_LDLApp.LocalDrivingLicenseApplicationID);

            if (DateTime.Compare(DateTime.Now, LastAppointment.AppointmentDate) < 0)
                dateTimePicker1.MinDate = LastAppointment.AppointmentDate;
            else
                dateTimePicker1.MinDate = DateTime.Now;

            dateTimePicker1.Value = _TestAppointment.AppointmentDate;

            if (_TestAppointment.RetakeTestApplicationID == -1)
            {
                lblRappFees.Text = "0";
                lblRTestappId.Text = "N/A";
            }
            else
            {
                lblRappFees.Text = Convert.ToInt32(clsApplication.GetApplication(_TestAppointment.RetakeTestApplicationID).PaidFees).ToString();
                lblRTestappId.Text = _TestAppointment.RetakeTestApplicationID.ToString();
            }
            return true;
        }
        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            DefineTestType();

            if (_LDLApp.DoesAttendTestType(_TestTypeID))
                _CreationMode = enCreationMode.RetakeTestSchedule;
            else
                _CreationMode = enCreationMode.FirstTimeSchedule;


            if (_CreationMode == enCreationMode.RetakeTestSchedule)
            {
                gbRetakeTest.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRappFees.Text = Convert.ToInt32(clsApplicationType.GetApplicationType(7).ApplicationFees).ToString();
            }
            else
            {
                gbRetakeTest.Enabled = false;
                lblTitle.Text = "Schedule Test";
                lblRappFees.Text = "0";
            }

            lblDLappId.Text = _LDLApp.LocalDrivingLicenseApplicationID.ToString();
            lblDClass.Text = clsLicenseClass.GetLicenseClassByID(_LDLApp.LicenseClassID).ClassName;
            lblName.Text = clsPerson.GetPersonByID(_app.ApplicantPersonID).FullName();
            lblTrial.Text = clsTest.CountTrialsPerTest(_LDLApp.LocalDrivingLicenseApplicationID, _TestTypeID).ToString();

            if (_Mode == enMode.AddNew)
            {
                lblFees.Text = Convert.ToInt32(clsTestType.GetTestType(_TestTypeID).TestTypeFees).ToString();

                LastAppointment = clsTestAppointment.GetLastTestAppointmentOverAll(_LDLApp.LocalDrivingLicenseApplicationID);
                if (LastAppointment == null)
                    dateTimePicker1.MinDate = DateTime.Now;
                else
                    dateTimePicker1.MinDate = LastAppointment.AppointmentDate.AddDays(1);

                lblRTestappId.Text = "N/A";
                _TestAppointment = new clsTestAppointment();
            }
            else
            {
                if (!_LoadTestAppointmentData())
                    return;
            }

            lblTotalFees.Text = (int.Parse(lblFees.Text) + int.Parse(lblRappFees.Text)).ToString();

            if (_TestAppointment.IsLocked)
            {
                if (_TestAppointment.RetakeTestApplicationID == -1)
                    lblTitle.Text = "Schedule Test";

                lblTakenTest.Visible = true;
                lblTakenTest.Text = "Person already sat for the test, appointment loacked.";
                dateTimePicker1.Enabled = false;
                btnSave.Enabled = false;
            }
            else
                lblTakenTest.Visible = false;
        }
        private bool SaveRetakeTestApplication()
        {
            if (_Mode == enMode.AddNew && _CreationMode == enCreationMode.RetakeTestSchedule)
            {
                clsApplication Application = new clsApplication();

                Application.ApplicantPersonID = clsPerson.GetPersonByID(_app.ApplicantPersonID).PersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID = 7; //RetakeTestApplicationID
                Application.ApplicationStatus = 3; //Completed
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees = clsApplicationType.GetApplicationType(7).ApplicationFees;
                Application.CreatedByUserID = clsSettings.UserID;

                if (!Application.Save())
                {
                    _TestAppointment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                _TestAppointment.RetakeTestApplicationID = Application.ApplicationID;

            }
            return true;
           
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!SaveRetakeTestApplication())
                return;

            _TestAppointment.TestTypeID = _TestTypeID;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LDLApp.LocalDrivingLicenseApplicationID;
            _TestAppointment.AppointmentDate = dateTimePicker1.Value;
            _TestAppointment.PaidFees = clsTestType.GetTestType(_TestTypeID).TestTypeFees;
            _TestAppointment.CreatedByUserID= clsSettings.UserID;
            _TestAppointment.IsLocked = false;

            if (_TestAppointment.Save())
            {
                _Mode = enMode.Update;
              MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            this.Close();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
