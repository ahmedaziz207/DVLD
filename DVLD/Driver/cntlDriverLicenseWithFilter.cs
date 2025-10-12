using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Controls
{
    public partial class cntlDriverLicenseWithFilter : UserControl
    {
        clsLicense license;
        public cntlDriverLicenseWithFilter()
        {
            InitializeComponent();
        }

        public event Action<int> OnLicenseSelected;
        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSelected;
            if (handler != null)
            { 
                handler(LicenseID);
            }
        }

        private void btnFindLicense_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                return;
            }
            
            if(clsLicense.IsLicenseExist(int.Parse(textBox1.Text)))
            {
                if (OnLicenseSelected != null)
                    OnLicenseSelected(int.Parse(textBox1.Text));
            }
            else
            {
                MessageBox.Show($"License with ID = [{int.Parse(textBox1.Text)}] Is Not Exist!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cntlDriverLicenseInfo1.LoadEmptyData();
                textBox1.Text = string.Empty;
                if (OnLicenseSelected != null)
                    OnLicenseSelected(-1);
            }
            
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        public void DisableGBFilter()
        {
            textBox1 .Text = license.LicenseID.ToString();
            gbFilter.Enabled = false;
        }
        public void LoadEmptyData()
        {
            textBox1.Text = string.Empty;
            cntlDriverLicenseInfo1.LoadEmptyData();
        }
        public void LoadDriverData()
        {
            license = clsLicense.GetLicense(int.Parse(textBox1.Text));
            cntlDriverLicenseInfo1.LoadDriverLicenseInfo(license);
        }

        public void LoadDriverDataUsingLicenseID(int licenseID)
        {
            license = clsLicense.GetLicense(licenseID);
            cntlDriverLicenseInfo1.LoadDriverLicenseInfo(license);
        }
    }
}
