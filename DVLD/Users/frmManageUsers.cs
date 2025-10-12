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
    public partial class frmManageUsers : Form
    {
        private static DataTable dtAllUsers;
        public frmManageUsers()
        {
            InitializeComponent();
        }

        private void _RefreshUsersList()
        {
            dtAllUsers = clsUser.GetAllUsers();
            dataGridView1.DataSource = dtAllUsers;
            lblRecords.Text = dataGridView1.Rows.Count.ToString();
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            _RefreshUsersList();
            cbFilter.SelectedIndex = 0;
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].FillWeight = 60;
                dataGridView1.Columns[1].FillWeight = 60;
                dataGridView1.Columns[2].HeaderText = "Full Name";
                dataGridView1.Columns[2].FillWeight = 220;
            }

        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            frmAdd_UpdateUser frm = new frmAdd_UpdateUser();
            frm.ShowDialog();
            _RefreshUsersList();
            cbFilter.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            string FilterValue = txtFilter.Text;

            switch (cbFilter.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "UserName":
                    FilterColumn = "UserName";
                    break;
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if (txtFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                dtAllUsers.DefaultView.RowFilter = "";
                lblRecords.Text = dataGridView1.Rows.Count.ToString();
                return;
            }
           
            if (FilterColumn == "PersonID" || FilterColumn == "UserID")
                dtAllUsers.DefaultView.RowFilter = $"{FilterColumn} = {FilterValue}";
            else
                dtAllUsers.DefaultView.RowFilter = $"{FilterColumn} LIKE '{FilterValue}%'";

            lblRecords.Text = dataGridView1.Rows.Count.ToString();
            
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Clear();
            _RefreshUsersList();
            if (cbFilter.Text == "Is Active")
            {
                txtFilter.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;

            }
            else
            {
                txtFilter.Visible = (cbFilter.Text != "None");
                txtFilter.Focus();
                cbIsActive.Visible = false;
            }
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cbIsActive.Text)
            {
                case "Yes":
                    dtAllUsers.DefaultView.RowFilter = "IsActive = true";
                    break;
                case "No":
                    dtAllUsers.DefaultView.RowFilter = "IsActive = false";
                    break;
                default:
                    dtAllUsers.DefaultView.RowFilter = "";
                    break;
            }

            lblRecords.Text = dataGridView1.Rows.Count.ToString();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "Person ID" || cbFilter.Text == "User ID")
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void ShowDetailstoolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserDetails frm = new frmUserDetails(clsUser.GetUserByID((int)dataGridView1.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            _RefreshUsersList();
            cbFilter.SelectedIndex = 0;
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdd_UpdateUser frm = new frmAdd_UpdateUser();
            frm.ShowDialog();
            _RefreshUsersList();
            cbFilter.SelectedIndex = 0;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdd_UpdateUser frm = new frmAdd_UpdateUser(clsUser.GetUserByID((int)dataGridView1.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            _RefreshUsersList();
            cbFilter.SelectedIndex = 0;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            if (MessageBox.Show($"Are you sure you want to delete User[{UserID}]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                if(clsUser.DeleteUser(UserID))
                {
                    MessageBox.Show("Person Deleted Successfully", "Successfull", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Person was not deleted because it has data linked to it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            _RefreshUsersList();
            cbFilter.SelectedIndex = 0;
        }

        private void changePasswordtoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsUser.GetUserByID((int)dataGridView1.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            _RefreshUsersList();
            cbFilter.SelectedIndex = 0;
        }
  
    }
}
