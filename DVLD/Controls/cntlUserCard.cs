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
    public partial class cntlUserCard : UserControl
    {
        clsUser _user;
        public cntlUserCard()
        {
            InitializeComponent();
        }
        public void LoadUserInfo(clsUser user)
        {
            _user = user;
            if (user == null)
            {
                MessageBox.Show("No User with UserID = " + user.UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillUserInfo();
        }

        private void _FillUserInfo()
        {
            clsPerson person = clsPerson.GetPersonByID(_user.PersonID);
            cntlPersonCard1.LoadPersonCardData(person);

            lblUserID.Text = _user.UserID.ToString();
            lblUserName.Text = _user.UserName.ToString();
            lblIsActive.Text = _user.IsActive ? "Yes":"No";

        }
        
    }
}
