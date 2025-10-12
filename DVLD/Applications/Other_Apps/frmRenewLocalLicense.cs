using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmRenewLocalLicense : Form
    {
        clsLicense OldLocalLicense;
        clsLicense NewLocalLicense;
        clsApplication Rapp;
        clsInternationalLicense ILicense;

        public frmRenewLocalLicense()
        {
            InitializeComponent();
        }

        private void frmRenewLocalLicense_Load(object sender, EventArgs e)
        {

            lblAppDate.Text = DateTime.Now.ToString("dd'/'MMM'/'yyyy");
            lblIssueDate.Text = DateTime.Now.ToString("dd'/'MMM'/'yyyy");
            lblAppFees.Text = Convert.ToInt32(clsApplicationType.GetApplicationType(2).ApplicationFees).ToString();
            lblCreatedBy.Text = clsSettings.UserName.ToString();

            btnIssue.Enabled = false;
            linkLHistory.Enabled = false;
            linkLincenseInfo.Enabled = false;
        }

        private void SaveRenewLicenseApplication()
        {
            Rapp = new clsApplication();
            Rapp.ApplicantPersonID = clsApplication.GetApplication(OldLocalLicense.ApplicationID).ApplicantPersonID;
            Rapp.ApplicationDate = DateTime.Now;
            Rapp.ApplicationTypeID = 2; //Renew License Application
            Rapp.ApplicationStatus = 3; //Completed
            Rapp.LastStatusDate = DateTime.Now;
            Rapp.PaidFees = clsApplicationType.GetApplicationType(2).ApplicationFees;
            Rapp.CreatedByUserID = clsSettings.UserID;
            Rapp.Save();
        }

        private void SaveRenewLicense()
        {
            NewLocalLicense = new clsLicense();
            NewLocalLicense.ApplicationID = OldLocalLicense.ApplicationID;
            NewLocalLicense.DriverID = OldLocalLicense.DriverID;
            NewLocalLicense.LicenseClassID = OldLocalLicense.LicenseClassID;
            NewLocalLicense.IssueDate = DateTime.Now;
            DateTime date = DateTime.Now;
            date = date.AddYears(clsLicenseClass.GetLicenseClassByID(OldLocalLicense.LicenseClassID).DefaultValidityLength);
            NewLocalLicense.ExpirationDate = date;
            NewLocalLicense.Notes = txtNotes.Text;
            NewLocalLicense.PaidFees = clsLicenseClass.GetLicenseClassByID(OldLocalLicense.LicenseClassID).ClassFees;
            NewLocalLicense.IssueReason = 2; //Renew License
            NewLocalLicense.IsActive = true;
            NewLocalLicense.CreatedByUserID = clsSettings.UserID;
        
        }
        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (OldLocalLicense.IsActive == true)
            {
                if (MessageBox.Show("Are you sure you want to Renew the License?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SaveRenewLicenseApplication();
                    SaveRenewLicense();
                    if (NewLocalLicense.Save())
                    {
                        OldLocalLicense.IsActive =false;
                        OldLocalLicense.Save();
                        if (clsInternationalLicense.IsInternationalLicenseExist(OldLocalLicense.LicenseID))
                        { 
                            ILicense = clsInternationalLicense.GetInternationalLicsenseByLocalLicense(OldLocalLicense.LicenseID); 
                            ILicense.IssuedUsingLocalLicenseID = NewLocalLicense.LicenseID;
                            ILicense.Save();
                        }


                        lblRLAppID.Text = Rapp.ApplicationID.ToString();
                        lblRenewLId.Text = NewLocalLicense.LicenseID.ToString();
                        MessageBox.Show($"License Renewed Successfully with ID = {NewLocalLicense.LicenseID}", "License Renewed", MessageBoxButtons.OK, MessageBoxIcon.None);
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
                    lblOldLID.Text = "????";
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
            clsPerson person = clsPerson.GetPersonByID(clsApplication.GetApplication(OldLocalLicense.ApplicationID).ApplicantPersonID);
            frmPersonLicenseHistory frm = new frmPersonLicenseHistory(person);
            frm.ShowDialog();
            frmRenewLocalLicense_Load(null, null);
        }

        private void linkLincenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseinfo frm = new frmShowLicenseinfo(NewLocalLicense);
            frm.ShowDialog();
            frmRenewLocalLicense_Load(null, null);
        }

        private void cntlDriverLicenseWithFilter1_OnLicenseSelected(int obj)
        {
            OldLocalLicense = clsLicense.GetLicense(obj);
     
            if (OldLocalLicense == null)
            {
                lblOldLID.Text = "????";
                lblLFees.Text = "????";
                lblTotalFees.Text = "????";
                lblExDate.Text = "????";

                btnIssue.Enabled = false;
                linkLHistory.Enabled = false;
                linkLincenseInfo.Enabled = false;

            }
            else
            {
               

                int LFees = Convert.ToInt32(clsLicenseClass.GetLicenseClassByID(OldLocalLicense.LicenseClassID).ClassFees);
                if (!clsLicense.IsLicenseExpired(obj))
                {
                    cntlDriverLicenseWithFilter1.LoadDriverData();
                    lblOldLID.Text = OldLocalLicense.LicenseID.ToString();
                    lblLFees.Text = LFees.ToString();
                    lblTotalFees.Text = (Convert.ToInt32(clsApplicationType.GetApplicationType(2).ApplicationFees) + LFees).ToString();
                    lblExDate.Text = OldLocalLicense.ExpirationDate.ToString("dd'/'MMM'/'yyyy");
                    MessageBox.Show($"Selected License is not yet Expired, It will Expire on: {OldLocalLicense.ExpirationDate.ToString("dd'/'MMM'/'yyyy")}","Not Allowed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    btnIssue.Enabled = false;
                    linkLHistory.Enabled = true;
                    linkLincenseInfo.Enabled = false;
                }
                else
                {
                    cntlDriverLicenseWithFilter1.LoadDriverData();
                    lblOldLID.Text = OldLocalLicense.LicenseID.ToString();
                    lblLFees.Text = LFees.ToString();
                    lblTotalFees.Text = (Convert.ToInt32(clsApplicationType.GetApplicationType(2).ApplicationFees) + LFees).ToString();
                    lblExDate.Text = OldLocalLicense.ExpirationDate.ToString("dd'/'MMM'/'yyyy");
                    btnIssue.Enabled = true;
                    linkLHistory.Enabled = true;
                    linkLincenseInfo.Enabled = false;
                }

            }
        }
    }
}
