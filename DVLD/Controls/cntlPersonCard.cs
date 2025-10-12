using DVLD.Properties;
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
using System.IO;

namespace DVLD
{
    public partial class cntlPersonCard : UserControl
    {
        public clsPerson _person;
        public cntlPersonCard()
        {
            InitializeComponent();
        }

        
        public void LoadPersonCardData(clsPerson person)
        {
            _person = person;

            lblPersonID.Text = person.PersonID.ToString();
            lblName.Text = person.FullName();
            lblNationalNo.Text = person.NationalNo;
            lblGender.Text = _person.Gender == 0 ? "Male" : "Female";
            lblEmail.Text = person.Email;
            lblPhone.Text = person.Phone;
            lblDateOfBirth.Text = person.DateOfBirth.ToShortDateString();
            lblAddress.Text = person.Address;
            lblCountry.Text = clsCountry.FindCountry(person.NationalityCountryID).CountryName;

            if (!string.IsNullOrEmpty(person.ImagePath))
            {
                if (File.Exists(person.ImagePath))
                    pictureBox1.ImageLocation = person.ImagePath;
                else
                    MessageBox.Show("Image doesn't Exist","Error",MessageBoxButtons.OK,MessageBoxIcon.Error); 
            }
            else
            {
                if (person.Gender == 0)
                    pictureBox1.Image = Resources.Male_512;
                else
                    pictureBox1.Image = Resources.Female_512;
            }
        }

        private void EditPersonlink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAdd_UpdatePerson frm = new frmAdd_UpdatePerson(_person);
            frm.ShowDialog();

            //Refresh
            LoadPersonCardData(clsPerson.GetPersonByID(_person.PersonID));

        }

        
    }
    
}
