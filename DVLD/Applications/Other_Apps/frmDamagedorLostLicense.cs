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
    public partial class frmDamagedorLostLicense : Form
    {
        clsApplication LostDamagedApp;
        clsLicense OldLocalLicense;
        clsLicense NewLocalLicense;
        clsInternationalLicense ILicense;
        public frmDamagedorLostLicense()
        {
            InitializeComponent();
        }

        private void frmDamagedorLostLicense_Load(object sender, EventArgs e)
        {
            btnIssue.Enabled = false;
            linkLHistory.Enabled = false;
            linkLincenseInfo.Enabled = false;
            rbDamaged.Checked = true;
            lblCreatedBy.Text = clsSettings.UserName;
            lblAppDate.Text = DateTime.Now.ToString("dd'/'MMM'/'yyyy");
            lblAppFees.Text = Convert.ToInt32(clsApplicationType.GetApplicationType(4).ApplicationFees).ToString();
        }

        private void rbDamaged_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = "Replacement For Damaged License";
            lblAppFees.Text = Convert.ToInt32(clsApplicationType.GetApplicationType(4).ApplicationFees).ToString();
        }

        private void rbLost_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = "Replacement For Lost License";
            lblAppFees.Text = Convert.ToInt32(clsApplicationType.GetApplicationType(3).ApplicationFees).ToString();
        }

        private void SaveLicenseApplication()
        {
            LostDamagedApp = new clsApplication();
            LostDamagedApp.ApplicantPersonID = clsApplication.GetApplication(OldLocalLicense.ApplicationID).ApplicantPersonID;
            LostDamagedApp.ApplicationDate = DateTime.Now;

            if(rbDamaged.Checked)
            { 
                LostDamagedApp.ApplicationTypeID = 4; //Damaged License Application
                LostDamagedApp.PaidFees = clsApplicationType.GetApplicationType(4).ApplicationFees;

            } 
            else
            {
                LostDamagedApp.ApplicationTypeID = 3; //Lost License Application
                LostDamagedApp.PaidFees = clsApplicationType.GetApplicationType(3).ApplicationFees;

            }

            LostDamagedApp.ApplicationStatus = 3; //Completed
            LostDamagedApp.LastStatusDate = DateTime.Now;
            LostDamagedApp.CreatedByUserID = clsSettings.UserID;
            LostDamagedApp.Save();
        }

        private void SaveNewLicense()
        {
            NewLocalLicense = new clsLicense();
            NewLocalLicense.ApplicationID = OldLocalLicense.ApplicationID;
            NewLocalLicense.DriverID = OldLocalLicense.DriverID;
            NewLocalLicense.LicenseClassID = OldLocalLicense.LicenseClassID;
            NewLocalLicense.IssueDate = DateTime.Now;
            DateTime date = DateTime.Now;
            date = date.AddYears(clsLicenseClass.GetLicenseClassByID(OldLocalLicense.LicenseClassID).DefaultValidityLength);
            NewLocalLicense.ExpirationDate = date;
            NewLocalLicense.Notes = OldLocalLicense.Notes;
            NewLocalLicense.PaidFees = clsLicenseClass.GetLicenseClassByID(OldLocalLicense.LicenseClassID).ClassFees;
            if (rbDamaged.Checked)
            {
                NewLocalLicense.IssueReason = 3; //Damaged License Application
            }
            else
            {
                NewLocalLicense.IssueReason = 4; //Lost License Application
            }
            NewLocalLicense.IsActive = true;
            NewLocalLicense.CreatedByUserID = clsSettings.UserID;
        }
        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Replace the License?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SaveLicenseApplication();
                SaveNewLicense();
                if (NewLocalLicense.Save())
                {
                    OldLocalLicense.IsActive = false;
                    OldLocalLicense.Save();
                    if (clsInternationalLicense.IsInternationalLicenseExist(OldLocalLicense.LicenseID))
                    {
                        ILicense = clsInternationalLicense.GetInternationalLicsenseByLocalLicense(OldLocalLicense.LicenseID);
                        ILicense.IssuedUsingLocalLicenseID = NewLocalLicense.LicenseID;
                        ILicense.Save();
                    }


                    lblLRAppID.Text = LostDamagedApp.ApplicationID.ToString();
                    lblReplacedLId.Text = NewLocalLicense.LicenseID.ToString();
                    MessageBox.Show($"License Replaced Successfully with ID = {NewLocalLicense.LicenseID}", "License Replaced", MessageBoxButtons.OK, MessageBoxIcon.None);
                    btnIssue.Enabled = false;
                    linkLincenseInfo.Enabled = true;
                    cntlDriverLicenseWithFilter1.DisableGBFilter();
                    gbReplacement.Enabled = false;
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsPerson person = clsPerson.GetPersonByID(clsApplication.GetApplication(OldLocalLicense.ApplicationID).ApplicantPersonID);
            frmPersonLicenseHistory frm = new frmPersonLicenseHistory(person);
            frm.ShowDialog();
            frmDamagedorLostLicense_Load(null,null);

        }

        private void linkLincenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseinfo frm = new frmShowLicenseinfo(NewLocalLicense);
            frm.ShowDialog();
            frmDamagedorLostLicense_Load(null, null);
        }

        private void cntlDriverLicenseWithFilter1_OnLicenseSelected(int obj)
        {
            OldLocalLicense = clsLicense.GetLicense(obj);

            if (OldLocalLicense == null)
            {
                lblOldLID.Text = "????";
                btnIssue.Enabled = false;
                linkLHistory.Enabled = false;
                linkLincenseInfo.Enabled = false;

            }
            else
            {
                if(OldLocalLicense.IsActive == false)
                {
                    cntlDriverLicenseWithFilter1.LoadDriverData();
                    lblOldLID.Text = OldLocalLicense.LicenseID.ToString();
                    MessageBox.Show("Selected License is Not Active, Choose an Active License.","Not Allowed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    btnIssue.Enabled = false;
                    linkLHistory.Enabled = true;
                    linkLincenseInfo.Enabled = false;
                }
                else
                {
                    cntlDriverLicenseWithFilter1.LoadDriverData();
                    lblOldLID.Text = OldLocalLicense.LicenseID.ToString();
                    btnIssue.Enabled = true;
                    linkLHistory.Enabled = true;
                    linkLincenseInfo.Enabled = false;

                }


            }
        }

        
    }
}
