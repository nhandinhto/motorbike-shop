using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn_QuanLyXeMay
{
    public partial class frmNhanVien : Form
    {
        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void product_Click(object sender, EventArgs e)
        {
            
            Program.formSP = new FrSanPham_NV_();
            this.Hide();
            Program.formSP.Show(this);
        }

        private void Sell_Click(object sender, EventArgs e)
        {
            Program.hoaDonBan = new frmHoaDonBan();
            this.Hide();
            Program.hoaDonBan.Show(this);
        }

        private void showCustom_Click(object sender, EventArgs e)
        {
            Program.formKH = new FrKhachHang() { Owner = this };
            this.Hide();
            Program.formKH.Show(this);
        }

        private void logOut_Click(object sender, EventArgs e)
        {
            Program.formNV.Close();
            Program.formLogin = new Login();
            Program.formLogin.Show();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có muốn thoát chương trình không", "Thông bóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {

        }



    }
}
