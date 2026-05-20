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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            HienThiUserControl(new UC_QLSV());
        }

        private void HienThiUserControl(UserControl userControl)
        {
            pnl_main.Controls.Clear();
            userControl.AutoScroll = true;
            userControl.Dock = DockStyle.Fill;
            pnl_main.Controls.Add(userControl);
        }

        private void quanLySinhVienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HienThiUserControl(new UC_QLSV());
        }

        private void quanLyLopHocToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HienThiUserControl(new UC_QLLH());
        }
    }
}
