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
        private const int pageSize = 10;
        private int currentPage = 1;
        private int totalPages = 1;
        private int totalRecords = 0;

        public UC_QLSV()
        {
            InitializeComponent();
            Load += UC_QLSV_Load;
            dgv_DSSV.CellClick += dgv_DSSV_CellClick;
            button2.Click += button2_Click;
            button3.Click += button3_Click;
            button4.Click += button4_Click;
            button5.Click += button5_Click;
            button7.Click += button7_Click;
            button8.Click += button8_Click;
            button9.Click += button9_Click;
            button10.Click += button10_Click;

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
                MessageBox.Show("Vui lòng chọn mã lớp!");
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
                MessageBox.Show("Vui lòng chọn sinh viên cần sửa!");
                return;
            }

            tbl_sinhvien sinhvien = db.tbl_sinhviens.FirstOrDefault(sv => sv.MaSV == maSV);

            if (sinhvien == null)
            {
                MessageBox.Show("Không tìm thấy sinh viên!");
                return;
            }

            try
            {
                sinhvien.HovaTen = txtHoTen.Text.Trim();
                sinhvien.Gioitinh = cboGioiTinh.Text;
                sinhvien.NgaySinh = DT_NgaySinh.Value.Date;
                sinhvien.tbl_lophoc = db.tbl_lophocs.FirstOrDefault(lh => lh.MaLop == cboMaLop.SelectedValue.ToString());

                db.SubmitChanges();
                MessageBox.Show("Sửa sinh viên thành công!");
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
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa!");
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa sinh viên này?", "Xác nhận", MessageBoxButtons.YesNo);

            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                tbl_sinhvien sinhvien = db.tbl_sinhviens.FirstOrDefault(sv => sv.MaSV == maSV);

                if (sinhvien == null)
                {
                    MessageBox.Show("Không tìm thấy sinh viên!");
                    return;
                }

                db.tbl_sinhviens.DeleteOnSubmit(sinhvien);
                db.SubmitChanges();
                MessageBox.Show("Xóa sinh viên thành công!");
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

        private void button5_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadData();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadData();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadData();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            currentPage = totalPages;
            LoadData();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadData();
            }
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
            string keyword = textBox1.Text.Trim();
            var query = db.tbl_sinhviens.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(sv => sv.MaSV.Contains(keyword)
                    || sv.HovaTen.Contains(keyword)
                    || sv.MaLop.Contains(keyword)
                    || sv.tbl_lophoc.TenLop.Contains(keyword));
            }

            totalRecords = query.Count();
            totalPages = Math.Max(1, (int)Math.Ceiling((double)totalRecords / pageSize));

            if (currentPage > totalPages)
            {
                currentPage = totalPages;
            }

            dgv_DSSV.DataSource = query
                .OrderBy(sv => sv.MaSV)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .Select(sv => new
            {
                sv.MaSV,
                sv.HovaTen,
                sv.Gioitinh,
                sv.NgaySinh,
                sv.MaLop,
                sv.tbl_lophoc.TenLop
            }).ToList();

            HienThiPhanTrang();
        }

        private void HienThiPhanTrang()
        {
            label21.Text = "Trang " + currentPage + "/" + totalPages;
            label22.Text = totalRecords + " Bản ghi";

            button7.Enabled = currentPage > 1;
            button8.Enabled = currentPage > 1;
            button9.Enabled = currentPage < totalPages;
            button10.Enabled = currentPage < totalPages;
        }

        private void LamMoiThongTin()
        {
            txtMSSV.Clear();
            txtHoTen.Clear();
            cboGioiTinh.SelectedIndex = -1;
            DT_NgaySinh.Value = DateTime.Today;
            textBox1.Clear();
            txtMSSV.Enabled = true;
            currentPage = 1;
            LoadLopHoc();
            cboMaLop.SelectedIndex = -1;
            LoadData();
        }

    }
}
