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
    public partial class frmManage_LDL_Applications : Form
    {
        clsUser _user;
        public frmManage_LDL_Applications(clsUser user)
        {
            InitializeComponent();
            _user = user;
        }

        private void _FillcbFilter()
        {
            cbFilter.Items.Add("None");

            DataTable dt = clsLDL_Application.GetAll_LDL_Applications_View();
            foreach (DataColumn dc in dt.Columns)
            {
                cbFilter.Items.Add(dc.ColumnName);
            }

            cbFilter.Items.Remove("Driving Class");
            cbFilter.Items.Remove("ApplicationDate");
            cbFilter.Items.Remove("Passed Tests");

            cbFilter.SelectedIndex = cbFilter.FindString("None");
        }
        private void _Refresh_LDL_ApplicationsList()
        {
            dataGridView1.DataSource = clsLDL_Application.GetAll_LDL_Applications_View();
            lblRecords.Text = dataGridView1.Rows.Count.ToString();
        }
        private void frmManage_LDL_Applications_Load(object sender, EventArgs e)
        {
            _FillcbFilter();
            _Refresh_LDL_ApplicationsList();
            dataGridView1.Columns[0].FillWeight = 50;
            dataGridView1.Columns[1].FillWeight = 100;
            dataGridView1.Columns[2].FillWeight = 40;
            dataGridView1.Columns[3].FillWeight = 120;
            dataGridView1.Columns[4].FillWeight = 65;
            dataGridView1.Columns[5].FillWeight = 30;
            dataGridView1.Columns[6].FillWeight = 40;

        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmNew_LDL_Application frm = new frmNew_LDL_Application();
            frm.ShowDialog();
            _Refresh_LDL_ApplicationsList();
            cbFilter.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FilterRecordsInDataGridView()
        {
            DataView dv = clsLDL_Application.GetAll_LDL_Applications_View().DefaultView;

            string columnName = cbFilter.SelectedItem.ToString();
            string filterValue = txtFilter.Text;

            if (string.IsNullOrWhiteSpace(filterValue))
            {
                dv.RowFilter = "";
            }
            else
            {

                if (columnName == "L.D.L.AppID")
                {
                    dv.RowFilter = $"{columnName} = {filterValue}";
                }
                else
                {
                    dv.RowFilter = $"{columnName} LIKE '{filterValue}%'";
                }
            }

            dataGridView1.DataSource = dv;

        }
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            FilterRecordsInDataGridView();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Clear();
            if (cbFilter.SelectedIndex != 0)
            {
                txtFilter.Visible = true;
            }
            else
            {
                txtFilter.Visible = false;
            }
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.SelectedIndex == 1)
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLappID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            clsLDL_Application LDL_App = clsLDL_Application.GetLDL_Application(LDLappID);
            clsApplication app = clsApplication.GetApplication(LDL_App.ApplicationID);

            frmShowLDL_AppDetails frm = new frmShowLDL_AppDetails(LDL_App,app);
            frm.ShowDialog();
            _Refresh_LDL_ApplicationsList();
            cbFilter.SelectedIndex = 0;
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDL_ApplicationID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            clsLDL_Application LDL_Application = clsLDL_Application.GetLDL_Application(LDL_ApplicationID);

            if (MessageBox.Show($"Are you sure you want to delete Application with ID = {LDL_ApplicationID}", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                bool isDeletedLDL = clsLDL_Application.DeleteLDL_Application(LDL_ApplicationID);
                bool isDeletedApp = clsApplication.DeleteApplication(LDL_Application.ApplicationID);
                    
                if (isDeletedLDL && isDeletedApp)
                {
                    MessageBox.Show("Application Deleted Successfully", "Successfull", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Application was not deleted because it has data linked to it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            _Refresh_LDL_ApplicationsList();
            cbFilter.SelectedIndex = 0;

        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLappID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            clsLDL_Application LDL_App = clsLDL_Application.GetLDL_Application(LDLappID);
            clsApplication app = clsApplication.GetApplication(LDL_App.ApplicationID);
            app.ApplicationStatus = 2; //Cancelled
            app.Save();
            _Refresh_LDL_ApplicationsList();
            cbFilter.SelectedIndex = 0;
        }
        
        private void schduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLappID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            clsLDL_Application LDL_App = clsLDL_Application.GetLDL_Application(LDLappID);
            clsApplication app = clsApplication.GetApplication(LDL_App.ApplicationID);

            frmManageTestAppointments frm = new frmManageTestAppointments(LDL_App, app, 1);
            frm.ShowDialog();
            _Refresh_LDL_ApplicationsList();
            cbFilter.SelectedIndex = 0;
        }

        private void schduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLappID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            clsLDL_Application LDL_App = clsLDL_Application.GetLDL_Application(LDLappID);
            clsApplication app = clsApplication.GetApplication(LDL_App.ApplicationID);

            frmManageTestAppointments frm = new frmManageTestAppointments(LDL_App, app,2);
            frm.ShowDialog();
            _Refresh_LDL_ApplicationsList();
            cbFilter.SelectedIndex = 0;
        }

        private void schduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLappID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            clsLDL_Application LDL_App = clsLDL_Application.GetLDL_Application(LDLappID);
            clsApplication app = clsApplication.GetApplication(LDL_App.ApplicationID);

            frmManageTestAppointments frm = new frmManageTestAppointments(LDL_App, app, 3);
            frm.ShowDialog();
            _Refresh_LDL_ApplicationsList();
            cbFilter.SelectedIndex = 0;
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLappID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            clsLDL_Application LDL_App = clsLDL_Application.GetLDL_Application(LDLappID);
            clsApplication app = clsApplication.GetApplication(LDL_App.ApplicationID);

            frmIssueDrivingLicence frm = new frmIssueDrivingLicence(LDL_App, app);
            frm.ShowDialog();
            _Refresh_LDL_ApplicationsList();
            cbFilter.SelectedIndex = 0;
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLappID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            frmShowLicenseinfo frm = new frmShowLicenseinfo(clsLicense.GetLicensebyLDLappID(LDLappID));
            frm.ShowDialog();
            _Refresh_LDL_ApplicationsList();
            cbFilter.SelectedIndex = 0;
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLappID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            clsLDL_Application LDL_App = clsLDL_Application.GetLDL_Application(LDLappID);
            clsApplication app = clsApplication.GetApplication(LDL_App.ApplicationID);
            clsPerson person = clsPerson.GetPersonByID(app.ApplicantPersonID);

            frmPersonLicenseHistory frm = new frmPersonLicenseHistory(person);
            frm.ShowDialog();
            _Refresh_LDL_ApplicationsList();
            cbFilter.SelectedIndex = 0;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            int passedTests = (int)dataGridView1.CurrentRow.Cells[5].Value;
            string status = (string)dataGridView1.CurrentRow.Cells[6].Value;

            deleteApplicationToolStripMenuItem.Enabled = (status == "New");
            cancelApplicationToolStripMenuItem.Enabled = (status == "New");
            editApplicationToolStripMenuItem.Enabled = (status == "New");
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = (status == "New") && passedTests == 3;
            showToolStripMenuItem.Enabled = (status == "Completed");
            schduleVisionTestToolStripMenuItem.Enabled = false;
            schduleWrittenTestToolStripMenuItem.Enabled = false;
            schduleStreetTestToolStripMenuItem.Enabled = false;
            schduleTestsToolStripMenuItem.Enabled = (status == "New") && passedTests != 3;

            switch (passedTests)
            {
                case 0:
                    schduleVisionTestToolStripMenuItem.Enabled = true;
                    break;
                case 1:
                    schduleWrittenTestToolStripMenuItem.Enabled = true;
                    break;
                case 2:
                    schduleStreetTestToolStripMenuItem.Enabled = true;
                    break;
            }

        }

        
    }
}
