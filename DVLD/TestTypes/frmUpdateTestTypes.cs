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
    public partial class frmUpdateTestTypes : Form
    {
        clsTestType _testtype;
        public frmUpdateTestTypes(clsTestType TestType)
        {
            InitializeComponent();
            _testtype = TestType;
        }

        private void frmUpdateTestTypes_Load(object sender, EventArgs e)
        {
            lblID.Text = _testtype.TestTypeID.ToString();
            txtTitle.Text = _testtype.TestTypeTitle.ToString();
            txtDescription.Text = _testtype.TestTypeDescription.ToString();
            txtfees.Text = Convert.ToInt32(_testtype.TestTypeFees).ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            _testtype.TestTypeTitle = txtTitle.Text;
            _testtype.TestTypeDescription = txtDescription.Text;
            _testtype.TestTypeFees = Convert.ToDecimal(txtfees.Text);

            if (_testtype.Save())
            {
                MessageBox.Show($"Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Failed to Save Changes.", " Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtfees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || txtfees.Text.Contains("."));
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "Title cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtTitle, null);
            }
        }

        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescription.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDescription, "Description cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtDescription, null);
            }
        }

        private void txtfees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtfees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtfees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtfees, null);

            }

            if (!clsValidation.IsNumber(txtfees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtfees, "Invalid Number.");
                return;
            }
            else
            {
                errorProvider1.SetError(txtfees, null);

            }
        }
    }
}
