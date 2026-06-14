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
   
    public partial class UC_QLSV : UserControl
    {
        private databaseDataContext db = new databaseDataContext();

        public UC_QLSV()
        {
            InitializeComponent();
            Load += UC_QLSV_Load;
            dgv_DSSV.CellClick += dgv_DSSV_CellClick;
            button2.Click += button2_Click;
            button3.Click += button3_Click;
            button4.Click += button4_Click;

            dgv_DSSV.ReadOnly = true;
            dgv_DSSV.MultiSelect = false;
            dgv_DSSV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_DSSV.AllowUserToAddRows = false;
        }

        private void UC_QLSV_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                LoadLopHoc();
                LoadData();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cboMaLop.SelectedValue == null)
            {
                MessageBox.Show("Vui l?ng ch?n m? l?p!");
                return;
            }

            tbl_sinhvien sinhvien = new tbl_sinhvien();

            sinhvien.MaSV = txtMSSV.Text.Trim();
            sinhvien.HovaTen = txtHoTen.Text.Trim();
            sinhvien.Gioitinh = cboGioiTinh.Text;
            sinhvien.NgaySinh = DT_NgaySinh.Value.Date;
            sinhvien.MaLop = cboMaLop.SelectedValue.ToString();

            try
            {
                db.tbl_sinhviens.InsertOnSubmit(sinhvien);
                db.SubmitChanges();
                MessageBox.Show("Thêm sinh viên thành công!");
                LoadData();
            }
            catch (Exception ex)
            {
                db = new databaseDataContext();
                LoadLopHoc();
                LoadData();
                MessageBox.Show(ex.Message);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string maSV = txtMSSV.Text.Trim();

            if (string.IsNullOrWhiteSpace(maSV))
            {
                MessageBox.Show("Vui l?ng ch?n sinh vi?n c?n s?a!");
                return;
            }

            tbl_sinhvien sinhvien = db.tbl_sinhviens.FirstOrDefault(sv => sv.MaSV == maSV);

            if (sinhvien == null)
            {
                MessageBox.Show("Kh?ng t?m th?y sinh vi?n!");
                return;
            }

            try
            {
                sinhvien.HovaTen = txtHoTen.Text.Trim();
                sinhvien.Gioitinh = cboGioiTinh.Text;
                sinhvien.NgaySinh = DT_NgaySinh.Value.Date;
                sinhvien.tbl_lophoc = db.tbl_lophocs.FirstOrDefault(lh => lh.MaLop == cboMaLop.SelectedValue.ToString());

                db.SubmitChanges();
                MessageBox.Show("S?a sinh vi?n th?nh c?ng!");
                LoadData();
            }
            catch (Exception ex)
            {
                db = new databaseDataContext();
                LoadLopHoc();
                LoadData();
                MessageBox.Show(ex.Message);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string maSV = txtMSSV.Text.Trim();

            if (string.IsNullOrWhiteSpace(maSV))
            {
                MessageBox.Show("Vui l?ng ch?n sinh vi?n c?n x?a!");
                return;
            }

            DialogResult result = MessageBox.Show("B?n c? ch?c mu?n x?a sinh vi?n n?y?", "X?c nh?n", MessageBoxButtons.YesNo);

            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                tbl_sinhvien sinhvien = db.tbl_sinhviens.FirstOrDefault(sv => sv.MaSV == maSV);

                if (sinhvien == null)
                {
                    MessageBox.Show("Kh?ng t?m th?y sinh vi?n!");
                    return;
                }

                db.tbl_sinhviens.DeleteOnSubmit(sinhvien);
                db.SubmitChanges();
                MessageBox.Show("X?a sinh vi?n th?nh c?ng!");
                LamMoiThongTin();
            }
            catch (Exception ex)
            {
                db = new databaseDataContext();
                LoadLopHoc();
                LoadData();
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LamMoiThongTin();
        }
        private void dgv_DSSV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dgv_DSSV.Rows[e.RowIndex];

            txtMSSV.Text = row.Cells["MaSV"].Value.ToString();
            txtHoTen.Text = row.Cells["HovaTen"].Value.ToString();
            cboGioiTinh.Text = row.Cells["Gioitinh"].Value.ToString();
            DT_NgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
            cboMaLop.SelectedValue = row.Cells["MaLop"].Value.ToString();
            txtMSSV.Enabled = false;
        }
        public void LoadLopHoc()
        {
            var lopHocs = db.tbl_lophocs
                .Select(lh => new { lh.MaLop, lh.TenLop })
                .ToList()
                .Select(lh => new { lh.MaLop, HienThi = lh.MaLop + " - " + lh.TenLop })
                .ToList();

            cboMaLop.DataSource = lopHocs;
            cboMaLop.DisplayMember = "HienThi";
            cboMaLop.ValueMember = "MaLop";
        }

        public void LoadData()
        {
            dgv_DSSV.DataSource = db.tbl_sinhviens.Select(sv => new
            {
                sv.MaSV,
                sv.HovaTen,
                sv.Gioitinh,
                sv.NgaySinh,
                sv.MaLop,
                sv.tbl_lophoc.TenLop
            }).ToList();
        }
        private void LamMoiThongTin()
        {
            txtMSSV.Clear();
            txtHoTen.Clear();
            cboGioiTinh.SelectedIndex = -1;
            DT_NgaySinh.Value = DateTime.Today;
            textBox1.Clear();
            txtMSSV.Enabled = true;
            LoadLopHoc();
            cboMaLop.SelectedIndex = -1;
            LoadData();
        }
    }
}



