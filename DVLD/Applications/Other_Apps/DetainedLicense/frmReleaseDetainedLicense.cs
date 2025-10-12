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
    public partial class frmReleaseDetainedLicense : Form
    {
        clsLicense License;
        clsDetainedLicense DLicense;
        clsApplication RDLapp;
        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
            btnRelease.Enabled = false;
            linkLHistory.Enabled = false;
            linkLincenseInfo.Enabled = false;
        }

        public frmReleaseDetainedLicense(int LicenseID)
        {
            InitializeComponent();
            License = clsLicense.GetLicense(LicenseID);
            cntlDriverLicenseWithFilter1.LoadDriverDataUsingLicenseID(LicenseID);
            cntlDriverLicenseWithFilter1.DisableGBFilter();
            DLicense = clsDetainedLicense.GetDetainedLicenseByLicenseID(LicenseID);
            lblDetainID.Text = DLicense.DetainID.ToString();
            lblLicenseId.Text = DLicense.LicenseID.ToString();
            lblFineFees.Text = Convert.ToInt32(DLicense.FineFees).ToString();
            lblTotalFees.Text = ((int)clsApplicationType.GetApplicationType(5).ApplicationFees + (int)DLicense.FineFees).ToString();

            btnRelease.Enabled = true;
            linkLHistory.Enabled = true;
            linkLincenseInfo.Enabled = false;
        }

        private void frmReleaseDetainedLicense_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = DateTime.Now.ToString("dd'/'MMM'/'yyyy");
            lblCreatedBy.Text = clsSettings.UserName.ToString();
            lblappFees.Text =  Convert.ToInt32(clsApplicationType.GetApplicationType(5).ApplicationFees).ToString();

            
        }

        private void SaveReleaseLicenseApplication()
        {
            RDLapp = new clsApplication();
            RDLapp.ApplicantPersonID = clsApplication.GetApplication(License.ApplicationID).ApplicantPersonID;
            RDLapp.ApplicationDate = DateTime.Now;
            RDLapp.ApplicationTypeID = 5; //Release Detained License Application
            RDLapp.ApplicationStatus = 3; //Completed
            RDLapp.LastStatusDate = DateTime.Now;
            RDLapp.PaidFees = clsApplicationType.GetApplicationType(5).ApplicationFees;
            RDLapp.CreatedByUserID = clsSettings.UserID;
            RDLapp.Save();
        }
        private void SaveDetaindLicense()
        {
            DLicense.IsReleased = true;
            DLicense.ReleaseDate = DateTime.Now;
            DLicense.ReleasedByUserID = clsSettings.UserID;
            DLicense.ReleaseApplicationID = RDLapp.ApplicationID;
        }
        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Release the Detain License?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SaveReleaseLicenseApplication();
                SaveDetaindLicense();

                if (DLicense.Save())
                {
                    lblRDLappID.Text = RDLapp.ApplicationID.ToString();
                    MessageBox.Show($"License Released Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.None);
                    btnRelease.Enabled = false;
                    linkLincenseInfo.Enabled = true;
                    cntlDriverLicenseWithFilter1.DisableGBFilter();
                }
                else
                {
                    MessageBox.Show("Failed To Release License!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        }

        private void linkLincenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseinfo frm = new frmShowLicenseinfo(License);
            frm.ShowDialog();
        }

        private void cntlDriverLicenseWithFilter1_OnLicenseSelected(int obj)
        {
            License = clsLicense.GetLicense(obj);

            if (License == null)
            {
                lblLicenseId.Text = "????";

                btnRelease.Enabled = false;
                linkLHistory.Enabled = false;
                linkLincenseInfo.Enabled = false;

            }
            else
            {
                DLicense = clsDetainedLicense.GetDetainedLicenseByLicenseID(License.LicenseID);
    
                if (!clsDetainedLicense.IsLicenseDetained(obj))
                {
                    cntlDriverLicenseWithFilter1.LoadDriverData();
                    lblDetainID.Text = "????";
                    lblLicenseId.Text = "????";
                    lblFineFees.Text = "????";
                    lblTotalFees.Text = "????";
                    MessageBox.Show($"Selected License is Not Detained, Choose Another One.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnRelease.Enabled = false;
                    linkLHistory.Enabled = true;
                    linkLincenseInfo.Enabled = false;
                }
                else
                {
                    cntlDriverLicenseWithFilter1.LoadDriverData();
                    lblDetainID.Text = DLicense.DetainID.ToString();
                    lblLicenseId.Text = DLicense.LicenseID.ToString();
                    lblFineFees.Text = Convert.ToInt32(DLicense.FineFees).ToString();
                    lblTotalFees.Text = ((int)clsApplicationType.GetApplicationType(5).ApplicationFees + (int)DLicense.FineFees).ToString();
                    btnRelease.Enabled = true;
                    linkLHistory.Enabled = true;
                    linkLincenseInfo.Enabled = false;
                }

            }
        }
    }
}
