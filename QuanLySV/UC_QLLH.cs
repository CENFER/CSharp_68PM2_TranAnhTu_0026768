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
        private const int pageSize = 10;
        private int currentPage = 1;
        private int totalPages = 1;
        private int totalRecords = 0;

        public UC_QLLH()
        {
            InitializeComponent();
            button1.Click += button1_Click;
            button2.Click += button2_Click;
            button3.Click += button3_Click;
            button4.Click += button4_Click;
            button5.Click += button5_Click;
            button7.Click += button7_Click;
            button8.Click += button8_Click;
            button9.Click += button9_Click;
            button10.Click += button10_Click;
            btn_DSSV.Click += btn_DSSV_Click;
            dgv_QLLH.CellClick += dgv_QLLH_CellClick;

            textBox4.Enabled = false;
            dgv_QLLH.ReadOnly = true;
            dgv_QLLH.MultiSelect = false;
            dgv_QLLH.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_QLLH.AllowUserToAddRows = false;
        }

        private void UC_QLLH_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            string keyword = textBox1.Text.Trim();
            var query = db.tbl_lophocs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                int maID;

                if (int.TryParse(keyword, out maID))
                {
                    query = query.Where(lh => lh.MaID == maID
                        || lh.MaLop.Contains(keyword)
                        || lh.TenLop.Contains(keyword)
                        || (lh.Ghichu != null && lh.Ghichu.Contains(keyword)));
                }
                else
                {
                    query = query.Where(lh => lh.MaLop.Contains(keyword)
                        || lh.TenLop.Contains(keyword)
                        || (lh.Ghichu != null && lh.Ghichu.Contains(keyword)));
                }
            }

            totalRecords = query.Count();
            totalPages = Math.Max(1, (int)Math.Ceiling((double)totalRecords / pageSize));

            if (currentPage > totalPages)
            {
                currentPage = totalPages;
            }

            dgv_QLLH.DataSource = query
                .OrderBy(lh => lh.MaID)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .Select(lh => new
                {
                    lh.MaID,
                    lh.MaLop,
                    lh.TenLop,
                    lh.Ghichu
                }).ToList();

            HienThiPhanTrang();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tbl_lophoc lophoc = new tbl_lophoc();

            lophoc.MaLop = textBox5.Text.Trim();
            lophoc.TenLop = textBox6.Text.Trim();
            lophoc.Ghichu = textBox7.Text.Trim();

            try
            {
                db.tbl_lophocs.InsertOnSubmit(lophoc);
                db.SubmitChanges();
                MessageBox.Show("Thêm lớp học thành công!");
                LoadData();
            }
            catch (Exception ex)
            {
                db = new databaseDataContext();
                LoadData();
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int maID;

            if (!int.TryParse(textBox4.Text, out maID))
            {
                MessageBox.Show("Vui lòng chọn lớp học cần sửa!");
                return;
            }

            tbl_lophoc lophoc = db.tbl_lophocs.FirstOrDefault(lh => lh.MaID == maID);

            if (lophoc == null)
            {
                MessageBox.Show("Không tìm thấy lớp học!");
                return;
            }

            try
            {
                lophoc.MaLop = textBox5.Text.Trim();
                lophoc.TenLop = textBox6.Text.Trim();
                lophoc.Ghichu = textBox7.Text.Trim();

                db.SubmitChanges();
                MessageBox.Show("Sửa lớp học thành công!");
                LoadData();
            }
            catch (Exception ex)
            {
                db = new databaseDataContext();
                LoadData();
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int maID;

            if (!int.TryParse(textBox4.Text, out maID))
            {
                MessageBox.Show("Vui lòng chọn lớp học cần xóa!");
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa lớp học này?", "Xác nhận", MessageBoxButtons.YesNo);

            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                tbl_lophoc lophoc = db.tbl_lophocs.FirstOrDefault(lh => lh.MaID == maID);

                if (lophoc == null)
                {
                    MessageBox.Show("Không tìm thấy lớp học!");
                    return;
                }

                db.tbl_lophocs.DeleteOnSubmit(lophoc);
                db.SubmitChanges();
                MessageBox.Show("Xóa lớp học thành công!");
                LamMoiThongTin();
            }
            catch (Exception ex)
            {
                db = new databaseDataContext();
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

        private void dgv_QLLH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dgv_QLLH.Rows[e.RowIndex];

            textBox4.Text = row.Cells["MaID"].Value.ToString();
            textBox5.Text = row.Cells["MaLop"].Value.ToString();
            textBox6.Text = row.Cells["TenLop"].Value.ToString();
            textBox7.Text = row.Cells["Ghichu"].Value == null ? "" : row.Cells["Ghichu"].Value.ToString();
            textBox4.Enabled = false;
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

        private void btn_DSSV_Click(object sender, EventArgs e)
        {
            string maLop = textBox5.Text.Trim();

            if (string.IsNullOrWhiteSpace(maLop)
                && dgv_QLLH.CurrentRow != null
                && dgv_QLLH.Columns.Contains("MaLop"))
            {
                object value = dgv_QLLH.CurrentRow.Cells["MaLop"].Value;
                maLop = value == null ? "" : value.ToString();
            }

            if (string.IsNullOrWhiteSpace(maLop))
            {
                MessageBox.Show("Vui lòng chọn lớp học cần xem sinh viên!");
                return;
            }

            var sinhViens = db.tbl_sinhviens
                .Where(sv => sv.MaLop == maLop)
                .Select(sv => new
                {
                    sv.MaSV,
                    sv.HovaTen,
                    sv.Gioitinh,
                    sv.NgaySinh,
                    sv.MaLop
                }).ToList();

            Form formDSSV = new Form();
            formDSSV.Text = "Danh sách sinh viên lớp " + maLop;
            formDSSV.StartPosition = FormStartPosition.CenterParent;
            formDSSV.Size = new Size(800, 450);

            DataGridView dgvSinhVien = new DataGridView();
            dgvSinhVien.Dock = DockStyle.Fill;
            dgvSinhVien.ReadOnly = true;
            dgvSinhVien.MultiSelect = false;
            dgvSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSinhVien.AllowUserToAddRows = false;
            dgvSinhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSinhVien.DataSource = sinhViens;

            formDSSV.Controls.Add(dgvSinhVien);
            formDSSV.ShowDialog();
        }

        private void LamMoiThongTin()
        {
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox1.Clear();
            textBox4.Enabled = false;
            currentPage = 1;
            LoadData();

        }
    }
}
