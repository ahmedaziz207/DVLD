using System.Diagnostics;
using Microsoft.Win32;
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

        static string keyPath = @"SOFTWARE\DVLD";
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
                    LogInfoToEventViewer("User \"" + user.UserName + "\" logged in successfully.", EventLogEntryType.Information);
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
                        LogInfoToEventViewer("User has exceeded maximum (3) login Trials.", EventLogEntryType.Error);
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

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyPath))
            {
                txtUserName.Text = key?.GetValue("Username")?.ToString() ?? string.Empty;
                txtPassword.Text = key?.GetValue("Password")?.ToString() ?? string.Empty;
            }

            if (!string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtPassword.Text))
            {
                chkRememberMe.Checked = true;
            }
            else
            {
                chkRememberMe.Checked = false;
            }

        }

        private void RememberMe()
        {
            if(!chkRememberMe.Checked)
            {
                try
                {
                    using (RegistryKey key = Registry.CurrentUser.CreateSubKey(keyPath))
                    {
                        key.DeleteValue("Username", false);
                        key.DeleteValue("Password", false);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to delete registry value. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return;
            }

            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(keyPath))
                {
                    key.SetValue("Username", txtUserName.Text.Trim(), RegistryValueKind.String);
                    key.SetValue("Password", txtPassword.Text.Trim(), RegistryValueKind.String);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to set registry value. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void LogInfoToEventViewer(string message, EventLogEntryType type)
        {
            string source = "DVLD_App";
            string log = "Application";
            if (!EventLog.SourceExists(source))
            {
                EventLog.CreateEventSource(source, log);
            }
            using (EventLog eventLog = new EventLog(log))
            {
                eventLog.Source = source;
                eventLog.WriteEntry(message, type);
            }
        }
    }
}
