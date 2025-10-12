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
    public partial class frmDriversList : Form
    {
        private static DataTable dtAllDrivers;
        public frmDriversList()
        {
            InitializeComponent();
        }

        private void _FillcbFilter()
        {
            cbFilter.Items.Add("None");

            DataTable dt = clsDriver.GetDrivers();
            foreach (DataColumn dc in dt.Columns)
            {
                cbFilter.Items.Add(dc.ColumnName);
            }

            cbFilter.Items.Remove("Date");

            cbFilter.SelectedIndex = cbFilter.FindString("None");
        }
        private void _RefreshDriversList()
        {
            dtAllDrivers = clsDriver.GetDrivers();
            dataGridView1.DataSource = dtAllDrivers;
            dataGridView1.Columns[0].FillWeight = 70;
            dataGridView1.Columns[1].FillWeight = 70;
            dataGridView1.Columns[2].FillWeight = 70;
            dataGridView1.Columns[3].FillWeight = 190;
            dataGridView1.Columns[5].FillWeight = 80;
            lblRecords.Text = dataGridView1.Rows.Count.ToString();
        }

        private void frmDriversList_Load(object sender, EventArgs e)
        {
            _RefreshDriversList();
            _FillcbFilter();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FilterRecordsInDataGridView()
        {
            string FilterColumn = cbFilter.SelectedItem.ToString();
            string FilterValue = txtFilter.Text;

            if (txtFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                dtAllDrivers.DefaultView.RowFilter = "";
                lblRecords.Text = dataGridView1.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "National No" || FilterColumn == "Full Name")
                dtAllDrivers.DefaultView.RowFilter = $"[{FilterColumn}] LIKE '{FilterValue}%'";
            else
                dtAllDrivers.DefaultView.RowFilter = $"[{FilterColumn}] = {FilterValue}";

            lblRecords.Text = dataGridView1.Rows.Count.ToString();

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
            if (cbFilter.SelectedIndex == 1 || cbFilter.SelectedIndex == 2 || cbFilter.SelectedIndex == 5)
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dataGridView1.CurrentRow.Cells[1].Value;
            frmPersonLicenseHistory frm = new frmPersonLicenseHistory(clsPerson.GetPersonByID(PersonID));
            frm.ShowDialog();
            frmDriversList_Load(null,null);
        }
    }
}
