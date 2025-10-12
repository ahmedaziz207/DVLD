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
    public partial class frmManageInterApplications : Form
    {
        public frmManageInterApplications()
        {
            InitializeComponent();
        }

        private void _FillcbFilter()
        {
            cbFilter.Items.Add("None");

            DataTable dt = clsInternationalLicense.GetInternationalLicenseApplications();
            foreach (DataColumn dc in dt.Columns)
            {
                cbFilter.Items.Add(dc.ColumnName);
            }

            cbFilter.Items.Remove("Issue Date");
            cbFilter.Items.Remove("Expiration Date");

            cbFilter.SelectedIndex = cbFilter.FindString("None");
        }
        private void _RefreshInternationalApplicationsList()
        {
            dataGridView1.DataSource = clsInternationalLicense.GetInternationalLicenseApplications();
            lblRecords.Text = dataGridView1.Rows.Count.ToString();
        }
        private void frmManageInterApplications_Load(object sender, EventArgs e)
        {
            _FillcbFilter();
            _RefreshInternationalApplicationsList();
        }

        private void FilterRecordsInDataGridView()
        {
            DataView dv = clsInternationalLicense.GetInternationalLicenseApplications().DefaultView;

            string columnName = cbFilter.SelectedItem.ToString();
            string filterValue = txtFilter.Text;

            if (string.IsNullOrWhiteSpace(filterValue))
            {
                dv.RowFilter = "";
            }
            else
            {
                dv.RowFilter = $"[{columnName}] = {filterValue}";
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
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicense frm = new frmNewInternationalLicense();
            frm.ShowDialog();
            _RefreshInternationalApplicationsList();
            cbFilter.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsPerson person = clsPerson.GetPersonByID(clsApplication.GetApplication((int)dataGridView1.CurrentRow.Cells[1].Value).ApplicantPersonID);
            frmPersonDetails frm = new frmPersonDetails(person);
            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsInternationalLicense ILicense = clsInternationalLicense.GetInternationalLicsense((int)dataGridView1.CurrentRow.Cells[0].Value);
            frmShowInternationalLicense frm = new frmShowInternationalLicense(ILicense);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsPerson person = clsPerson.GetPersonByID(clsApplication.GetApplication((int)dataGridView1.CurrentRow.Cells[1].Value).ApplicantPersonID);
            frmPersonLicenseHistory frm = new frmPersonLicenseHistory(person);
            frm.ShowDialog();
        }

        
    }
}
