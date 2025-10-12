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
    public partial class frmPersonDetails : Form
    {
        private clsPerson _person;

        public frmPersonDetails(clsPerson person)
        {
            InitializeComponent();
            _person = person;
            cntlPersonCard1.LoadPersonCardData(_person);

        }
      
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
