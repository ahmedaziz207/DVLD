using DVLD.Properties;
using DVLD_Business;
using System;
using System.Windows.Forms;

namespace DVLD
{ 
    public partial class frmManageTestAppointments : Form
    {
        clsApplication _app;
        clsLDL_Application _LDLApp;
        int _TestTypeID;

        public frmManageTestAppointments(clsLDL_Application LDL_app, clsApplication app, int TestTypeID)
        {
            InitializeComponent();
            _LDLApp = LDL_app;
            _app = app;
            _TestTypeID = TestTypeID;

        }
        private void _RefreshTestAppointmentsList()
        {
            dataGridView1.DataSource = clsTestAppointment.GetAllTestAppointments(_LDLApp.LocalDrivingLicenseApplicationID, _TestTypeID);
            lblRecords.Text = dataGridView1.Rows.Count.ToString();
            cntlDLApplicationInfo1.RefreshPassedTests();
        }
        private void _LoadTestTypeImageAndTitle()
        {
            switch (_TestTypeID)
            {
                case 1:
                    {
                        lblTitle.Text = "Vision Test Appointments";
                        this.Text = lblTitle.Text;
                        pictureBox1.ImageLocation = @"D:\My Projects C#\DVLD\Icons\Vision 512.png";
                        break;
                    }

                case 2:
                    {
                        lblTitle.Text = "Written Test Appointments";
                        this.Text = lblTitle.Text;
                        pictureBox1.ImageLocation = @"D:\My Projects C#\DVLD\Icons\Written Test 512.png";
                        break;
                    }
                case 3:
                    {
                        lblTitle.Text = "Street Test Appointments";
                        this.Text = lblTitle.Text;
                        pictureBox1.ImageLocation = @"D:\My Projects C#\DVLD\Icons\driving-test 512.png";
                        break;
                    }
            }
        }
        private void frmManageTestAppointment_Load(object sender, EventArgs e)
        {
            _LoadTestTypeImageAndTitle();
            cntlDLApplicationInfo1.LoadDLApplicationInfo(_LDLApp,_app,clsTest.CountPassedTestsfromThree(_LDLApp.LocalDrivingLicenseApplicationID));
            _RefreshTestAppointmentsList();
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            if (clsTestAppointment.IsThereOpenAppointment(_LDLApp.LocalDrivingLicenseApplicationID, _TestTypeID))
            {
                MessageBox.Show("Person Already has an Active Appointment for this Test, You Canoot Add New Appointment", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(clsTest.CountPassedTestsfromThree(_LDLApp.LocalDrivingLicenseApplicationID) == _TestTypeID)
            {
                MessageBox.Show("This Person Already Passed This Test Before","Not Allowed",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                frmScheduleTest frm = new frmScheduleTest(_LDLApp, _app, _TestTypeID);
                frm.ShowDialog();
                _RefreshTestAppointmentsList();

            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            
            frmScheduleTest frm = new frmScheduleTest(_LDLApp, _app, _TestTypeID, TestAppointmentID);
            frm.ShowDialog();
            _RefreshTestAppointmentsList();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            clsTestAppointment TestAppointment = clsTestAppointment.GetTestAppointment(TestAppointmentID);

            if(TestAppointment.IsLocked == true)
            {
                MessageBox.Show("This Appointment is Locked!","Locked Appointment",MessageBoxButtons.OK,MessageBoxIcon.None);
                return;
            }

            frmTakeTest frm = new frmTakeTest(_LDLApp, _app, _TestTypeID, TestAppointment);
            frm.ShowDialog();
            _RefreshTestAppointmentsList();

        }

        
    }
}
