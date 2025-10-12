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
    public partial class frmManageTestTypes : Form
    {
        public frmManageTestTypes()
        {
            InitializeComponent();
        }

        private void _RefreshTestTypesList()
        {
            dataGridView1.DataSource = clsTestType.GetAllTestTypes();
            lblRecords.Text = dataGridView1.Rows.Count.ToString();
        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            _RefreshTestTypesList();
            dataGridView1.Columns[0].FillWeight = 30;
            dataGridView1.Columns[1].FillWeight = 50;
            dataGridView1.Columns[3].FillWeight = 40;
           
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateTestTypes frm = new frmUpdateTestTypes(clsTestType.GetTestType((int)dataGridView1.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            _RefreshTestTypesList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
