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

namespace DVLD.Controls
{
    public partial class cntlDriverInterLicenseInfo : UserControl
    {
        public cntlDriverInterLicenseInfo()
        {
            InitializeComponent();
        }

        public void LoadInternationalLicenseInfo(clsInternationalLicense ILicense)
        {
            clsPerson person = clsPerson.GetPersonByID(clsApplication.GetApplication(ILicense.ApplicationID).ApplicantPersonID);
            
            pbimage.ImageLocation = person.ImagePath;
            lblPersonName.Text = person.FullName();
            lblintLicenseID.Text = ILicense.InternationalLicenseID.ToString();
            lblLicenseID.Text = ILicense.IssuedUsingLocalLicenseID.ToString();
            lblNationalNo.Text = person.NationalNo;

            if (person.Gender == 0)
                lblGender.Text = "Male";
            else
                lblGender.Text = "Female";

            lblIsuueDate.Text = ILicense.IssueDate.ToString("dd'/'MMM'/'yyyy");
            lblApplicationID.Text = ILicense.ApplicationID.ToString();

            if (ILicense.IsActive == true)
                lblisActive.Text = "Yes";
            else
                lblisActive.Text = "No";

            lblDateofBirth.Text = person.DateOfBirth.ToString("dd'/'MMM'/'yyyy");
            lblDriverID.Text = ILicense.DriverID.ToString();
            lblExDate.Text = ILicense.ExpirationDate.ToString("dd'/'MMM'/'yyyy");

        }
    }
}
