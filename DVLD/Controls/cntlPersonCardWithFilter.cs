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
    public partial class cntlPersonCardWithFilter : UserControl
    {
        public event Action<int> OnPersonSelected;
        // Create a protected method to raise the event with a parameter
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                handler(PersonID); // Raise the event with the parameter
            }
        }


        private clsPerson _person;

        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get
            {
                return _ShowAddPerson;
            }
            set
            {
                _ShowAddPerson = value;
                btnAddNewPerson.Visible = _ShowAddPerson;
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                groupBox1.Enabled = _FilterEnabled;
            }
        }

        public cntlPersonCardWithFilter()
        {
            InitializeComponent();

        }

        public int PersonIDforThisCard()
        {
            if (_person == null)
            {
                return -1;
            }
            else
            {
                return cntlPersonCard1._person.PersonID;
            }
        }

        public void LoadPersonInfo(int PersonID)
        {
            cbFilter.SelectedIndex = 0; //Person ID
            txtFilter.Text = PersonID.ToString();
            FindNow();
        }

        private void cntlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 1; // National No.
            txtFilter.Focus();
        }

        private void FindNow()
        {
            if (cbFilter.SelectedIndex == cbFilter.FindString("National No."))
            {
                if (!string.IsNullOrWhiteSpace(txtFilter.Text) && clsPerson.isPersonExistByNationalNo(txtFilter.Text))
                {
                    _person = clsPerson.GetPersonByNationalNo(txtFilter.Text);
                    cntlPersonCard1.LoadPersonCardData(_person);
                }
                else
                {
                    MessageBox.Show($"No Person with National No. = {txtFilter.Text}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(txtFilter.Text) && clsPerson.isPersonExist(int.Parse(txtFilter.Text)))
                {
                    _person = clsPerson.GetPersonByID(int.Parse(txtFilter.Text));
                    cntlPersonCard1.LoadPersonCardData(_person);
                }
                else
                {
                    MessageBox.Show($"No Person with PersonID = {txtFilter.Text}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (OnPersonSelected != null && FilterEnabled)
                // Raise the event with a parameter
                OnPersonSelected(PersonIDforThisCard());
        }
        private void btnFindPerson_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            FindNow();

        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAdd_UpdatePerson frm = new frmAdd_UpdatePerson();
            frm.DataBack += RecieveData;
            frm.ShowDialog();

        }
        private void RecieveData(int PersonID)
        {
            _person = clsPerson.GetPersonByID(PersonID);
            cbFilter.SelectedIndex = 0; //  this is Person ID
            txtFilter.Text = _person.PersonID.ToString();
            cntlPersonCard1.LoadPersonCardData(_person);
        }
        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
            {

                btnFindPerson.PerformClick();
            }

            //this will allow only digits if person id is selected
            if (cbFilter.SelectedIndex == 0)
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }
        private void txtFilter_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilter.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilter, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtFilter, null);
            }
        }
        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Clear();
            txtFilter.Focus();
        }
        public void FilterFocus()
        {
            txtFilter.Focus();
        }

    }
}
