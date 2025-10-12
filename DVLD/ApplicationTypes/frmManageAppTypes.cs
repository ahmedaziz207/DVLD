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
    public partial class frmManageAppTypes : Form
    {
        public frmManageAppTypes()
        {
            InitializeComponent();
        }

        private void _RefreshApplicationTypesList()
        {
            dataGridView1.DataSource = clsApplicationType.GetAllApplicationTypes();
            lblRecords.Text = dataGridView1.Rows.Count.ToString();
        }
        private void frmManageAppTypes_Load(object sender, EventArgs e)
        {
            _RefreshApplicationTypesList();
            dataGridView1.Columns[0].FillWeight = 45;
            dataGridView1.Columns[2].FillWeight = 40;
        
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateAppType frm = new frmUpdateAppType(clsApplicationType.GetApplicationType((int)dataGridView1.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            frmManageAppTypes_Load(null,null); // Refresh Form
        }
    }
}
