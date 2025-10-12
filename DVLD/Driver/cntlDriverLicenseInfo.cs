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
    public partial class cntlDriverLicenseInfo : UserControl
    {
        public cntlDriverLicenseInfo()
        {
            InitializeComponent();
        }

        public void LoadDriverLicenseInfo(clsLicense license)
        {
            clsPerson person = clsPerson.GetPersonByID(clsApplication.GetApplication(license.ApplicationID).ApplicantPersonID);
            pictureBox1.ImageLocation = person.ImagePath;
            lblClassName.Text = clsLicenseClass.GetLicenseClassByID(license.LicenseClassID).ClassName;
            lblPersonName.Text = person.FullName();
            lblLicenseID.Text = license.LicenseID.ToString();
            lblNationalNo.Text = person.NationalNo;
            if (person.Gender == 0)
                lblGender.Text = "Male";
            else
                lblGender.Text = "Female";

            lblIsuueDate.Text = license.IssueDate.ToString("dd'/'MMM'/'yyyy");

            switch (license.IssueReason)
            {
                case 1:
                    lblIsuueReason.Text = "First Time";
                    break;
                case 2:
                    lblIsuueReason.Text = "Renew";
                    break;
                case 3:
                    lblIsuueReason.Text = "Replacement for Damaged";
                    break;
                case 4:
                    lblIsuueReason.Text = "Replacement for Lost";
                    break;
                    default:
                    break;

            }

            if (string.IsNullOrEmpty(license.Notes))
                lblNotes.Text = "No Notes";
            else
                lblNotes.Text = license.Notes;

            if (license.IsActive == false)
                lblisActive.Text = "No";
            else
                lblisActive.Text = "Yes";

            lblDateofBirth.Text = person.DateOfBirth.ToString("dd'/'MMM'/'yyyy");
            lblDriverID.Text = license.DriverID.ToString();
            lblExDate.Text = license.ExpirationDate.ToString("dd'/'MMM'/'yyyy");

            if(clsDetainedLicense.IsLicenseDetained(license.LicenseID))
                lblIsDetained.Text = "Yes";
            else
                lblIsDetained.Text = "No";

        }

        public void LoadEmptyData()
        {
            pictureBox1.ImageLocation = null;
            lblClassName.Text = "????";
            lblPersonName.Text = "????";
            lblLicenseID.Text = "????";
            lblNationalNo.Text = "????";
            lblGender.Text = "????";
            lblIsuueDate.Text = "????";
            lblIsuueReason.Text = "????";
            lblNotes.Text = "????";
            lblisActive.Text = "????";
            lblDateofBirth.Text = "????";
            lblDriverID.Text = "????";
            lblExDate.Text = "????";
            lblIsDetained.Text = "????";
        }
    }
}
