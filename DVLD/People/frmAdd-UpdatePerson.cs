using DVLD.Properties;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace DVLD
{
    public partial class frmAdd_UpdatePerson : Form
    {
        public enum enMode { AddNew, Update }
        public enMode Mode;

        clsPerson _person;

        public delegate void MyDelegate(int PersonID);
        public event MyDelegate DataBack;

        public frmAdd_UpdatePerson()
        {
            InitializeComponent();
            Mode = enMode.AddNew;
            _person = new clsPerson();
        }
        public frmAdd_UpdatePerson(clsPerson person)
        {
            InitializeComponent();
            Mode = enMode.Update;
            _person = person;
        
        }
        private void _ResetDefaultValues()
        {
            _FillcbCountry();

            if (Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Person";
            }
            else
            {
                lblTitle.Text = "Update Person";
            }

            if (rbMale.Checked)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            removeimagelink.Visible = (pbPersonImage.ImageLocation != null);

            dateTimePicker1.MaxDate = DateTime.Now.AddYears(-18);
            dateTimePicker1.Value = dateTimePicker1.MaxDate;

            dateTimePicker1.MinDate = DateTime.Now.AddYears(-100);

            cbCountry.SelectedIndex = cbCountry.FindString("Egypt");

            txtFirst.Text = "";
            txtSecond.Text = "";
            txtThird.Text = "";
            txtLast.Text = "";
            txtNationalNo.Text = "";
            rbMale.Checked = true;
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";

        }
        private void _LoadPersonData()
        {

            lblPersonID.Text = _person.PersonID.ToString();
            txtFirst.Text = _person.FirstName;
            txtSecond.Text = _person.SecondName;
            txtThird.Text = _person.ThirdName;
            txtLast.Text = _person.LastName;
            txtNationalNo.Text = _person.NationalNo;
            txtPhone.Text = _person.Phone;
            txtAddress.Text = _person.Address;
            txtEmail.Text = _person.Email;
            dateTimePicker1.Value = _person.DateOfBirth;

            if (_person.Gender == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;

            cbCountry.SelectedIndex = cbCountry.FindString(clsCountry.FindCountry(_person.NationalityCountryID).CountryName);

            if (_person.ImagePath != "")
            {
                pbPersonImage.ImageLocation = _person.ImagePath;

            }

            removeimagelink.Visible = (_person.ImagePath != "");

        }
        private void _FillcbCountry()
        {
            DataTable dt = clsCountry.GetAllCountries();

            foreach (DataRow dr in dt.Rows)
            {
                cbCountry.Items.Add(dr["CountryName"]);
            }

        }
        private void frmAdd_UpdatePerson_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (Mode==enMode.Update)
            {
                _LoadPersonData();
            }
            
        }

        private void textBox_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                e.Cancel = true;
                txt.Focus();
                errorProvider1.SetError(txt, "This field is required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txt, "");
            }
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }

            //Make sure the national number is not used by another person
            if (txtNationalNo.Text.Trim() != _person.NationalNo && clsPerson.isPersonExistByNationalNo(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "National Number is used for another person!");

            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text.Trim() == "")
                return;

            //validate email format 
            if (!clsValidation.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            }


            //if (!txtEmail.Text.EndsWith("@gmail.com")&& !string.IsNullOrWhiteSpace(txtEmail.Text))
            //{
            //    e.Cancel = true;
            //    txtEmail.Focus();
            //    errorProvider1.SetError(txtEmail, "Email Format must end with \"@gmail.com\"");
            //}
            //else
            //{
            //    e.Cancel = false;
            //    errorProvider1.SetError(txtEmail, "");
            //}
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if(pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = Properties.Resources.Male_512;
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = Properties.Resources.Female_512;
        }

        private bool _HandlePersonImage()
        {

            if (_person.ImagePath != pbPersonImage.ImageLocation)
            {
                if (_person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(_person.ImagePath);
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("Error Delete Image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                if (pbPersonImage.ImageLocation != null)
                {
                    try
                    {
                        string sourcePath = pbPersonImage.ImageLocation;
                        string destinationFolder = @"D:\My Projects C#\DVLD\DVLD-People images\";

                        string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(sourcePath);

                        string destinationPath = Path.Combine(destinationFolder, newFileName);

                        File.Copy(sourcePath, destinationPath, true);

                        pbPersonImage.ImageLocation = destinationPath;
                        return true;
                    }
                    catch
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                   
                }

            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (!_HandlePersonImage())
                return;
            

            _person.NationalNo = txtNationalNo.Text.Trim();
            _person.FirstName = txtFirst.Text.Trim();
            _person.SecondName = txtSecond.Text;
            _person.ThirdName = txtThird.Text;
            _person.LastName = txtLast.Text;
            _person.DateOfBirth = dateTimePicker1.Value;
            _person.Email = txtEmail.Text;
            _person.Phone = txtPhone.Text;
            _person.Address = txtAddress.Text;
            _person.NationalityCountryID = clsCountry.FindCountryByName(cbCountry.Text).CountryID;

            if (rbMale.Checked)
                _person.Gender = 0;
            else
                _person.Gender = 1;

            if (pbPersonImage.ImageLocation != null)
                _person.ImagePath = pbPersonImage.ImageLocation;
            else
                _person.ImagePath = "";


            if (_person.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Saved",MessageBoxButtons.OK,MessageBoxIcon.Information);
                lblPersonID.Text = _person.PersonID.ToString();

                if (int.TryParse(lblPersonID.Text, out int PersonID))
                {
                    DataBack?.Invoke(PersonID);
                }

                btnSave.Enabled = false;
            }
            else
            {
                MessageBox.Show("Failed to Save");
            }
          
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();            
        }

        private void setimagelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog1.FileName;
                pbPersonImage.Load(selectedFilePath);
                removeimagelink.Visible = true;

            }
        }

        private void removeimagelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;

            if (rbMale.Checked)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            removeimagelink.Visible = false;

        }

    }
}
