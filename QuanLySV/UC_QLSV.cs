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
