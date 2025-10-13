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
    public partial class frmDetainLicense : Form
    {
        clsLicense License;
        clsDetainedLicense DLicense;
        public frmDetainLicense()
        {
            InitializeComponent();
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = DateTime.Now.ToString("dd'/'MMM'/'yyyy");
            lblCreatedBy.Text = clsSettings.UserName.ToString();

            btnDetain.Enabled = false;
            linkLHistory.Enabled = false;
            linkLincenseInfo.Enabled = false;
        }
  
        private void SaveDetaindLicense()
        {
            DLicense = new clsDetainedLicense();
            DLicense.LicenseID = License.LicenseID;
            DLicense.DetainDate = DateTime.Now;
            DLicense.FineFees = Convert.ToDecimal(txtFineFees.Text);
            DLicense.CreatedByUserID = clsSettings.UserID;
            DLicense.IsReleased = false;
        }
        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (MessageBox.Show("Are you sure you want to Detain the License?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SaveDetaindLicense();

                if (DLicense.Save())
                {
                    lblDetainID.Text = DLicense.LicenseID.ToString();
                    MessageBox.Show($"License Detained Successfully with ID = {DLicense.DetainID}", "License Detained", MessageBoxButtons.OK, MessageBoxIcon.None);
                    btnDetain.Enabled = false;
                    linkLincenseInfo.Enabled = true;
                    cntlDriverLicenseWithFilter1.DisableGBFilter();
                }
                else
                {
                    MessageBox.Show("Failed To Detain License!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else // click 'No'
            {
                cntlDriverLicenseWithFilter1.LoadEmptyData();
                lblLicenseId.Text = "????";
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsPerson person = clsPerson.GetPersonByID(clsApplication.GetApplication(License.ApplicationID).ApplicantPersonID);
            frmPersonLicenseHistory frm = new frmPersonLicenseHistory(person);
            frm.ShowDialog();
            frmDetainLicense_Load(null, null);
        }

        private void linkLincenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseinfo frm = new frmShowLicenseinfo(License);
            frm.ShowDialog();
            frmDetainLicense_Load(null,null);
        }

        private void cntlDriverLicenseWithFilter1_OnLicenseSelected(int obj)
        {
            License = clsLicense.GetLicense(obj);

            if (License == null)
            {
                lblLicenseId.Text = "????";

                btnDetain.Enabled = false;
                linkLHistory.Enabled = false;
                linkLincenseInfo.Enabled = false;

            }
            else
            {

                if (clsDetainedLicense.IsLicenseDetained(obj))
                {
                    cntlDriverLicenseWithFilter1.LoadDriverData();
                    lblLicenseId.Text = License.LicenseID.ToString();
                    MessageBox.Show($"Selected License is Already Detained, Choose Another One.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnDetain.Enabled = false;
                    linkLHistory.Enabled = true;
                    linkLincenseInfo.Enabled = false;
                }
                else
                {
                    cntlDriverLicenseWithFilter1.LoadDriverData();
                    lblLicenseId.Text = License.LicenseID.ToString();
                    txtFineFees.Focus();
                    btnDetain.Enabled = true;
                    linkLHistory.Enabled = true;
                    linkLincenseInfo.Enabled = false;
                }

            }
        }

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFineFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "This field is required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFineFees, null);
            }
        }
    }
}
