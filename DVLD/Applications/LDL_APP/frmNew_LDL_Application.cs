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
    public partial class frmNew_LDL_Application : Form
    {
        clsPerson person;
        clsApplication app;
        clsLDL_Application _LDL_app;

        public frmNew_LDL_Application()
        {
            InitializeComponent();
            
        }

        private void _FIllcblicenseClass()
        {

            DataTable dt = clsLicenseClass.GetAllLicenseClasses();
            foreach (DataRow Row in dt.Rows)
            {
                cblicenseClass.Items.Add(Row["ClassName"]);
            }

            cblicenseClass.SelectedIndex = cblicenseClass.FindString("Class 3 - Ordinary driving license");
        }
        private void frmNew_LDL_Application_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblApplicationFees.Text = Convert.ToInt32(clsApplicationType.GetApplicationType(1).ApplicationFees).ToString();  // New Local Driving License Service = 1
            lblCreatedBy.Text = clsSettings.UserName.ToString();
            _FIllcblicenseClass();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (cntlPersonCardWithFilter1.PersonIDforThisCard() == -1)
            {
                MessageBox.Show("There is No Person Info!", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (clsUser.isUserExistByPersonID(cntlPersonCardWithFilter1.PersonIDforThisCard()))
            {
                MessageBox.Show($"Selected Person already has a user,Choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                tabControl1.SelectedTab = tabPage2;
            }
        }

        private bool SaveApplication()
        {
            app = new clsApplication();
            app.ApplicantPersonID = cntlPersonCardWithFilter1.PersonIDforThisCard();
            app.ApplicationDate = DateTime.Now;
            app.ApplicationTypeID = 1; // this is New Local Driving License
            app.LastStatusDate = DateTime.Now;
            app.PaidFees = Convert.ToDecimal(lblApplicationFees.Text);
            app.CreatedByUserID = clsSettings.UserID;

            return app.Save();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            person = clsPerson.GetPersonByID(cntlPersonCardWithFilter1.PersonIDforThisCard());
            TimeSpan Diff = DateTime.Now - person.DateOfBirth;
            int PersonAgeByYears = (int)(Diff.TotalDays / 365.25);

            if(clsDriver.DoesDriverHaveThisLicense(person.PersonID, cblicenseClass.Text))
            {
                MessageBox.Show($"Person Already has a License with the same applied driving class. Choose diffirent driving class.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clsLDL_Application.IsApplicationWithThisLicenseClassExist(person.NationalNo,cblicenseClass.Text))
            {
                MessageBox.Show($"Choose another License Class, The Selected Person Already has an Active Application for Selected License Class.", " Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((!(cblicenseClass.SelectedIndex == 0 || cblicenseClass.SelectedIndex == 2)) && PersonAgeByYears < 21)
            {
                MessageBox.Show($"Person Age Must be Above 21 For This License.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (SaveApplication())
            {
                _LDL_app = new clsLDL_Application();

                _LDL_app.ApplicationID = app.ApplicationID;
                _LDL_app.LicenseClassID = clsLicenseClass.GetLicenseClassByName(cblicenseClass.Text).LicenseClassID;
                if (_LDL_app.Save())
                {
                    lbldlappID.Text = _LDL_app.LocalDrivingLicenseApplicationID.ToString();
                    MessageBox.Show($"Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Failed to Save Local Driving License Application.", " Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show($"Failed to Save Application.", " Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
