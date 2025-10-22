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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD
{
    public partial class frmChangePassword : Form
    {
        private clsUser _user;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _user = clsUser.GetUserByID(UserID);
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            cntlUserCard1.LoadUserInfo(_user);
            txtCurrentPassword.Focus();
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCurrentPassword.Text.Trim()))
            {
                e.Cancel = true;
                txtCurrentPassword.Focus();
                errorProvider1.SetError(txtCurrentPassword, "This field is required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCurrentPassword, "");
            }

            if (clsHash.ComputeHash(txtCurrentPassword.Text) != _user.Password)
            {
                e.Cancel = true;
                txtCurrentPassword.Focus();
                errorProvider1.SetError(txtCurrentPassword, "Current Password is Incorrect");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCurrentPassword, "");
            }
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                e.Cancel = true;
                txtNewPassword.Focus();
                errorProvider1.SetError(txtNewPassword, "This field is required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNewPassword, "");
            }
        }

        private void txtConfirmpassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtConfirmpassword.Text.Trim()))
            {
                e.Cancel = true;
                txtConfirmpassword.Focus();
                errorProvider1.SetError(txtConfirmpassword, "This field is required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmpassword, "");
            }

            if (txtConfirmpassword.Text != txtNewPassword.Text)
            {
                e.Cancel = true;
                txtConfirmpassword.Focus();
                errorProvider1.SetError(txtConfirmpassword, "Confirm Password must Match New Password");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmpassword, "");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the error","Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _user.Password = txtNewPassword.Text;

            if (_user.Save())
            {
                MessageBox.Show($"Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Failed to Save User.", " Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
