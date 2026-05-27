using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySV
{
    public partial class UC_QLLH : UserControl
    {
        private databaseDataContext db = new databaseDataContext();
        public UC_QLLH()
        {
            InitializeComponent();
        }

        private void UC_QLLH_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        public void LoadData()
        {
            dgv_QLLH.DataSource = db.tbl_lophocs.ToList();

        }
    }
}
