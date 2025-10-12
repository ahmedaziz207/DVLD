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
    public partial class frmAdd_UpdateUser : Form
    {
        public enum enMode { AddNew, Update }
        public enMode Mode;

        clsUser _user;
        public frmAdd_UpdateUser()
        {
            InitializeComponent();
            Mode = enMode.AddNew;
            _user = new clsUser();
        }
        public frmAdd_UpdateUser(clsUser user)
        {
            InitializeComponent();
            Mode = enMode.Update;
            _user = user;
        }

        private void _ResetDefualtValues()
        {
            //this will initialize the reset the defaule values

            if (Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New User";
                this.Text = "Add New User";
                _user = new clsUser();

                tpLoginInfo.Enabled = false;

                cntlPersonCardWithFilter1.FilterFocus();
            }
            else
            {
                lblTitle.Text = "Update User";
                this.Text = "Update User";

                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;


            }

            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmpassword.Text = "";
            chkIsActive.Checked = true;


        }

        private void _LoadData()
        {
            cntlPersonCardWithFilter1.FilterEnabled = false;

            if (_user == null)
            {
                MessageBox.Show("No User with ID = " + _user.UserID, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }

            //the following code will not be executed if the person was not found
            lblUserID.Text = _user.UserID.ToString();
            txtUserName.Text = _user.UserName;
            txtPassword.Text = _user.Password;
            txtConfirmpassword.Text = _user.Password;
            chkIsActive.Checked = _user.IsActive;
            cntlPersonCardWithFilter1.LoadPersonInfo(_user.PersonID);
        }

        private void frmAdd_UpdateUser_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (Mode ==enMode.Update)
                _LoadData();
                
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpLoginInfo.Enabled = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                return;
            }

            //incase of add new mode.
            if (cntlPersonCardWithFilter1.PersonIDforThisCard() != -1)
            {
                if (clsUser.isUserExistByPersonID(cntlPersonCardWithFilter1.PersonIDforThisCard()))
                {

                    MessageBox.Show("Selected Person already has a user, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cntlPersonCardWithFilter1.FilterFocus();
                }

                else
                {
                    btnSave.Enabled = true;
                    tpLoginInfo.Enabled = true;
                    tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                }
            }
            else
            {
                MessageBox.Show("There is No Person Info!", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cntlPersonCardWithFilter1.FilterFocus();

            }

        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                e.Cancel = true;
                txtUserName.Focus();
                errorProvider1.SetError(txtUserName, "This field is required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtUserName, "");
            }


            if (Mode == enMode.AddNew)
            {

                if (clsUser.isUserExistByUserName(txtUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "username is used by another user");
                }
                else
                {
                    errorProvider1.SetError(txtUserName, null);
                }
                
            }
            else
            {
                //incase update make sure not to use anothers user name
                if (_user.UserName != txtUserName.Text.Trim())
                {
                    if (clsUser.isUserExistByUserName(txtUserName.Text.Trim()))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(txtUserName, "username is used by another user");
                        return;
                    }
                    else
                    {
                        errorProvider1.SetError(txtUserName, null);
                    }
                    
                }
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                e.Cancel = true;
                txtPassword.Focus();
                errorProvider1.SetError(txtPassword, "This field is required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtPassword, "");
            }
        }

        private void txtConfirmpassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmpassword.Text != txtPassword.Text)
            {
                e.Cancel = true;
                txtConfirmpassword.Focus();
                errorProvider1.SetError(txtConfirmpassword, "Confirm Password must Match Password");
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
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the error",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            _user.UserName = txtUserName.Text;
            _user.Password = txtPassword.Text;
            _user.PersonID = cntlPersonCardWithFilter1.PersonIDforThisCard();
            _user.IsActive = chkIsActive.Checked;

            if(_user.Save())
            {
                Mode =enMode.Update;
                lblTitle.Text = "Update User";
                this.Text = "Update User";
                lblUserID.Text = _user.UserID.ToString();
                MessageBox.Show($"Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Failed to Save User.", " Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
