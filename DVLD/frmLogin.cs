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
    public partial class frmLogin : Form
    {
        clsUser user; //To Reduce Memory Consuming
        byte Trials = 3;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            user = clsUser.GetUserByUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());

            if(user != null )
            {
                if (user.IsActive == true)
                {
                    clsSettings.UserID = user.UserID;
                    clsSettings.UserName = user.UserName;

                    RememberMe();
                    this.Hide();
                    frmMain frm = new frmMain();
                    frm.ShowDialog();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Your Account is not Active. Please Contact your Admin ", "System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
            else
            {
                lblTrials.Visible = true;
                switch (Trials)
                {
                    case 3:
                        Trials--;
                        lblTrials.Text = "2 Trials Left !";
                        break;
                    case 2:
                        Trials--;
                        lblTrials.Text = "1 Trials Left !";
                        break;
                    case 1:
                        Trials--;
                        lblTrials.Text = "No Trials Left !";
                        txtUserName.Enabled = false;
                        txtPassword.Enabled = false;
                        btnLogin.Enabled = false;
                        chkRememberMe.Enabled = false;
                        btnClose.Enabled = true;
                        break;
                }
                
                MessageBox.Show("Invalid Username/Password.", "Wrong Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtUserName.Text = Properties.Settings.Default.UserName;
            txtPassword.Text = Properties.Settings.Default.Password;

            if(string.IsNullOrWhiteSpace(txtUserName.Text))
                chkRememberMe.Checked = false;
            else
                chkRememberMe.Checked = true;
        }

        private void RememberMe()
        {
            if (chkRememberMe.Checked)
            {
                Properties.Settings.Default.UserName = txtUserName.Text;
                Properties.Settings.Default.Password = txtPassword.Text;

            }
            else
            {
                Properties.Settings.Default.UserName = null;
                Properties.Settings.Default.Password = null;

            }
            
            Properties.Settings.Default.Save();

        }
    }
}
