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

            tbl_sinhvien sinhvien = new tbl_sinhvien();

            sinhvien.MaSV = txtMSSV.Text;
            sinhvien.HovaTen = txtHoTen.Text;
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
                MessageBox.Show(ex.Message);
            }
            db.tbl_sinhviens.InsertOnSubmit(sinhvien);
            db.SubmitChanges();
            LoadData();
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

    }
}

