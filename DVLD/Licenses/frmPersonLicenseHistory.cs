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
    public partial class frmPersonLicenseHistory : Form
    {
        clsPerson person;
        public frmPersonLicenseHistory(clsPerson Person)
        {
            InitializeComponent();
            person = Person;
        }

        private void _RefreshLicensesList()
        {
            dataGridView1.DataSource = clsLicense.GetLocalLicenses(person.PersonID);
            
            dataGridView1.Columns[0].FillWeight = 50;
            dataGridView1.Columns[1].FillWeight = 50;
            dataGridView1.Columns[2].FillWeight = 145;
            dataGridView1.Columns[5].FillWeight = 60;
            lblRecords.Text = dataGridView1.Rows.Count.ToString();
            //////////////////////////////////////////////////////////////////////////////////////////////
            dataGridView2.DataSource = clsInternationalLicense.GetInternationalLicenses(person.PersonID);
            lblinterRecords.Text = dataGridView2.Rows.Count.ToString();
        }

        private void frmPersonLicenseHistory_Load(object sender, EventArgs e)
        {
            cntlPersonCard1.LoadPersonCardData(person);
            _RefreshLicensesList();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            frmShowLicenseinfo frm = new frmShowLicenseinfo(clsLicense.GetLicense(LicenseID));
            frm.ShowDialog();
        }

        private void showInternationalLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ILicenseID = (int)dataGridView2.CurrentRow.Cells[0].Value;
            frmShowInternationalLicense frm = new frmShowInternationalLicense(clsInternationalLicense.GetInternationalLicsense(ILicenseID));
            frm.ShowDialog();
        }
    }
}
