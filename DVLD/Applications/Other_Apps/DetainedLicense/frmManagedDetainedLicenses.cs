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
    public partial class frmManagedDetainedLicenses : Form
    {
        public frmManagedDetainedLicenses()
        {
            InitializeComponent();
        }
        private void _FillcbFilter()
        {
            cbFilter.Items.Add("None");

            DataTable dt = clsDetainedLicense.GetAllDetainedLicenses();
            foreach (DataColumn dc in dt.Columns)
            {
                cbFilter.Items.Add(dc.ColumnName);
            }

            cbFilter.Items.Remove("L.ID");
            cbFilter.Items.Remove("DetainDate");
            cbFilter.Items.Remove("FineFees");
            cbFilter.Items.Remove("ReleaseDate");

            cbFilter.SelectedIndex = cbFilter.FindString("None");
        }
        private void _RefreshLicensesList()
        {
            DataTable dt = clsDetainedLicense.GetAllDetainedLicenses();
            if (dt != null && dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt.AsEnumerable().Reverse().CopyToDataTable();
                dataGridView1.Columns[0].FillWeight = 40;
                dataGridView1.Columns[1].FillWeight = 40;
                dataGridView1.Columns[2].FillWeight = 80;
                dataGridView1.Columns[3].FillWeight = 50;
                dataGridView1.Columns[4].FillWeight = 50;
                dataGridView1.Columns[5].FillWeight = 80;
                dataGridView1.Columns[6].FillWeight = 40;
                dataGridView1.Columns[8].FillWeight = 50;
            }

            lblRecords.Text = dataGridView1.Rows.Count.ToString();
        }

        private void frmManagedDetainedLicenses_Load(object sender, EventArgs e)
        {
            _FillcbFilter();
            _RefreshLicensesList();
        }

        private void FilterRecordsInDataGridView()
        {
            DataView dv = clsDetainedLicense.GetAllDetainedLicenses().DefaultView;

            string columnName = cbFilter.SelectedItem.ToString();
            string filterValue = txtFilter.Text;

            if (string.IsNullOrWhiteSpace(filterValue))
            {
                dv.RowFilter = "";
            }
            else
            {

                if (columnName == "D.ID" || columnName == "R.app ID")
                {
                    dv.RowFilter = $"[{columnName}] = {filterValue}";
                }
                else
                {
                    dv.RowFilter = $"[{columnName}] LIKE '{filterValue}%'";
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
            _RefreshLicensesList();
            if (cbFilter.SelectedIndex != 0)
            {
                txtFilter.Visible = true;
                cbIsReleased.Visible = false;

                if (cbFilter.SelectedIndex == 2)
                {
                    txtFilter.Visible = false;
                    cbIsReleased.Visible = true;
                    cbIsReleased.SelectedIndex = 0;
                }
            }
            else
            {
                txtFilter.Visible = false;
                cbIsReleased.Visible = false;
            }
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dv = clsDetainedLicense.GetAllDetainedLicenses().DefaultView;

            if (cbIsReleased.SelectedIndex == cbIsReleased.FindString("Yes"))
            {
                dv.RowFilter = "IsReleased = true";

            }

            if (cbIsReleased.SelectedIndex == cbIsReleased.FindString("No"))
            {
                dv.RowFilter = "IsReleased = false";
            }

            dataGridView1.DataSource = dv;
        }
        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.SelectedIndex == 1 || cbFilter.SelectedIndex == 5)
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
            _FillcbFilter();
            _RefreshLicensesList();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
            _FillcbFilter();
            _RefreshLicensesList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsPerson person = clsPerson.GetPersonByNationalNo((string)dataGridView1.CurrentRow.Cells[6].Value);
            frmPersonDetails frm = new frmPersonDetails(person);
            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLicense license = clsLicense.GetLicense((int)dataGridView1.CurrentRow.Cells[1].Value);
            frmShowLicenseinfo frm = new frmShowLicenseinfo(license);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsPerson person = clsPerson.GetPersonByNationalNo((string)dataGridView1.CurrentRow.Cells[6].Value);
            frmPersonLicenseHistory frm = new frmPersonLicenseHistory(person);
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense((int)dataGridView1.CurrentRow.Cells[1].Value);
            frm.ShowDialog();

            _FillcbFilter();
            _RefreshLicensesList();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            bool IsReleased = (bool)dataGridView1.CurrentRow.Cells[3].Value;
            if(IsReleased)
                releaseDetainedLicenseToolStripMenuItem.Enabled = false;
            else
                releaseDetainedLicenseToolStripMenuItem.Enabled = true;
        }
    }
}
