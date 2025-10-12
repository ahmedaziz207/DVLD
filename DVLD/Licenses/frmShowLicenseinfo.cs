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
    public partial class frmShowLicenseinfo : Form
    {
        clsLicense license;
        public frmShowLicenseinfo(clsLicense License)
        {
            InitializeComponent();
            license = License;
        }

        private void frmShowLicenseinfo_Load(object sender, EventArgs e)
        {
            cntlDriverLicenseInfo1.LoadDriverLicenseInfo(license);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
