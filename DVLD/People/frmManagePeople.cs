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
using System.IO;

namespace DVLD
{
    public partial class frmManagePeople : Form
    {
        public frmManagePeople()
        {
            InitializeComponent();
        }

        private void _FillcbFilter()
        {
            cbFilter.Items.Add("None");

            DataTable dt = clsPerson.GetAllPeople();
            foreach (DataColumn dc in dt.Columns)
            {
                cbFilter.Items.Add(dc.ColumnName);
            }

            cbFilter.Items.Remove("DateOfBirth");
            cbFilter.SelectedIndex = cbFilter.FindString("None");
        }
        private void _RefreshPeopleList()
        {
            dataGridView1.DataSource = clsPerson.GetAllPeople();
            lblRecords.Text = dataGridView1.Rows.Count.ToString();
        }
        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _FillcbFilter();
            _RefreshPeopleList();
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].FillWeight = 70;
                dataGridView1.Columns[1].FillWeight = 70;
                dataGridView1.Columns[6].FillWeight = 70;
                dataGridView1.Columns[8].FillWeight = 70;
            }
            
        }
        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAdd_UpdatePerson frm = new frmAdd_UpdatePerson();
            frm.ShowDialog();
            _RefreshPeopleList();
            cbFilter.SelectedIndex = 0;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FilterRecordsInDataGridView()
        {
            DataView dv = clsPerson.GetAllPeople().DefaultView;

            string columnName = cbFilter.SelectedItem.ToString();
            string filterValue = txtFilter.Text;

            if (string.IsNullOrWhiteSpace(filterValue))
            {
                dv.RowFilter = ""; 
            }
            else
            {
                
                if (columnName == "PersonID")
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

        /////////////////////////////////////////////////////////////////////////////////
        private void ShowDetailstoolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails(clsPerson.GetPersonByID((int)dataGridView1.CurrentRow.Cells["PersonID"].Value));
            frm.ShowDialog();
            _RefreshPeopleList();
            cbFilter.SelectedIndex = 0;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdd_UpdatePerson frm = new frmAdd_UpdatePerson(clsPerson.GetPersonByID((int)dataGridView1.CurrentRow.Cells["PersonID"].Value));
            frm.ShowDialog();
            _RefreshPeopleList();
            cbFilter.SelectedIndex = 0;
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdd_UpdatePerson frm = new frmAdd_UpdatePerson();
            frm.ShowDialog();
            _RefreshPeopleList();
            cbFilter.SelectedIndex = 0;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            if (MessageBox.Show($"Are you sure you want to delete person[{PersonID}]","Confirm Delete",MessageBoxButtons.OKCancel,MessageBoxIcon.Question)==DialogResult.OK)
            {
                clsPerson person = clsPerson.GetPersonByID(PersonID);

                if (!string.IsNullOrEmpty(person.ImagePath) && File.Exists(person.ImagePath))
                {
                    File.Delete(person.ImagePath);
                }

                if (clsPerson.DeletePerson(PersonID))
                {
                    MessageBox.Show("Person Deleted Successfully","Successfull",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Person was not deleted because it has data linked to it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            _RefreshPeopleList();
            cbFilter.SelectedIndex = 0;
        }

    }
}
