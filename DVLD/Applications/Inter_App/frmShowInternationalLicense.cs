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
    public partial class frmShowInternationalLicense : Form
    {
        clsInternationalLicense ILicense;
        public frmShowInternationalLicense(clsInternationalLicense Ilicense)
        {
            InitializeComponent();
            ILicense = Ilicense;
        }

        private void frmShowInternationalLicense_Load(object sender, EventArgs e)
        {
            cntlDriverInterLicenseInfo1.LoadInternationalLicenseInfo(ILicense);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
