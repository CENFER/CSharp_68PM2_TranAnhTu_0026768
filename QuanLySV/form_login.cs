using System;
using System.Windows.Forms;

namespace QuanLySV
{
    public partial class form_login : Form
    {
        public form_login()
        {
            InitializeComponent();
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPass.Text;

            string emailSV = "tu0026768@st.edu.vn";
            string mssv = "0026768";

            if (email == emailSV && password == mssv)
            {
                MessageBox.Show("Đăng nhập thành công!");
                Main main = new Main();
                main.FormClosed += (s, args) => this.Close();
                main.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại!");
            }
        }
        private void chkShowPass_CheckedChanged(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = !chkShowPass.Checked;
        }
    }
    
}
