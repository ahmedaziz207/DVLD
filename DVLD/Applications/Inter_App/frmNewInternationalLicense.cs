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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD
{
    public partial class frmNewInternationalLicense : Form
    {
        clsApplication Iapp;
        clsInternationalLicense ILicense;
        clsLicense LocalLicense;

        public frmNewInternationalLicense()
        {
            InitializeComponent();
        }

        private void frmNewInternationalLicense_Load(object sender, EventArgs e)
        {
            lblAppDate.Text = DateTime.Now.ToString("dd'/'MMM'/'yyyy");
            lblIssueDate.Text = DateTime.Now.ToString("dd'/'MMM'/'yyyy");
            DateTime date = DateTime.Now;
            date = date.AddYears(1);
            lblExDate.Text = date.ToString("dd'/'MMM'/'yyyy");
            lblFees.Text = Convert.ToInt32(clsApplicationType.GetApplicationType(6).ApplicationFees).ToString();
            lblCreatedBy.Text = clsSettings.UserID.ToString();

            btnIssue.Enabled = false;
            linkLHistory.Enabled = false;
            linkLincenseInfo.Enabled = false;

        }

        private void SaveInternationalApplication()
        {
            Iapp = new clsApplication();
            Iapp.ApplicantPersonID = clsApplication.GetApplication(LocalLicense.ApplicationID).ApplicantPersonID;
            Iapp.ApplicationDate = DateTime.Now;
            Iapp.ApplicationTypeID = 6; //New International License Application
            Iapp.ApplicationStatus = 3; //Completed
            Iapp.LastStatusDate = DateTime.Now;
            Iapp.PaidFees = clsApplicationType.GetApplicationType(6).ApplicationFees;
            Iapp.CreatedByUserID = clsSettings.UserID;
            Iapp.Save();
        }

        private void SaveInternationalLicense()
        {
            ILicense = new clsInternationalLicense();
            ILicense.ApplicationID = Iapp.ApplicationID;
            ILicense.DriverID = LocalLicense.DriverID;
            ILicense.IssuedUsingLocalLicenseID = LocalLicense.LicenseID;
            ILicense.IssueDate = DateTime.Now;
            DateTime date = DateTime.Now;
            date = date.AddYears(1);
            ILicense.ExpirationDate = date;
            ILicense.IsActive = true;
            ILicense.CreatedByUserID = clsSettings.UserID;
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (LocalLicense.IsActive == true)
            {
                if (!clsInternationalLicense.IsInternationalLicenseExist(LocalLicense.LicenseID))
                {
                    if (MessageBox.Show("Are you sure you want to issue the License?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        SaveInternationalApplication();
                        SaveInternationalLicense();
                        if (ILicense.Save())
                        {
                            lblintLicenseID.Text = ILicense.InternationalLicenseID.ToString();
                            MessageBox.Show($"International License Issued Successfully with ID = {ILicense.InternationalLicenseID}", "license Issued", MessageBoxButtons.OK, MessageBoxIcon.None);
                            btnIssue.Enabled = false;
                            linkLincenseInfo.Enabled = true;
                            cntlDriverLicenseWithFilter1.DisableGBFilter();
                        }
                        else
                        {
                            MessageBox.Show("Failed To Issue International License!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else // click 'No'
                    {
                        cntlDriverLicenseWithFilter1.LoadEmptyData();
                    }
                }
                else
                {
                    MessageBox.Show("Person Already has International License", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("License is Not Active", "Not Active", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = clsApplication.GetApplication(LocalLicense.ApplicationID).ApplicantPersonID;
            frmPersonLicenseHistory frm = new frmPersonLicenseHistory(clsPerson.GetPersonByID(PersonID));
            frm.ShowDialog();
        }

        private void linkLincenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInternationalLicense frm = new frmShowInternationalLicense(ILicense);
            frm.ShowDialog();
        }

        private void cntlDriverLicenseWithFilter1_OnLicenseSelected(int obj)
        {
            LocalLicense = clsLicense.GetLicense(obj);
            if (LocalLicense == null)
            {
                lblLocalLicenseId.Text = "????";
                btnIssue.Enabled = false;
                linkLHistory.Enabled = false;
                
            }
            else
            {
                if (clsLicense.IsLicenseOfClass3Exist(obj))
                {
                    cntlDriverLicenseWithFilter1.LoadDriverData();
                    lblLocalLicenseId.Text = LocalLicense.LicenseID.ToString();
                    btnIssue.Enabled = true;
                    linkLHistory.Enabled = true;
                }
                else
                {
                    MessageBox.Show($"License with ID = [{LocalLicense.LicenseID}] is Not From Class 3", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cntlDriverLicenseWithFilter1.LoadEmptyData();
                    lblLocalLicenseId.Text = "????";
                    btnIssue.Enabled = false;
                    linkLHistory.Enabled = false;
                }
                
            }
        }

        
    }
}
