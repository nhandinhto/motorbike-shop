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
    public partial class frmQuanLy : Form
    {
        public frmQuanLy()
        {
            InitializeComponent();
        }

        private void product_Click(object sender, EventArgs e)
        {
            Program.frmXe = new Xe();
            Program.frmXe.Show();
            this.Hide();
        }

        private void logOut_Click(object sender, EventArgs e)
        {
            Program.formQL.Close();
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

        private void account_Click(object sender, EventArgs e)
        {
            Program.formDK = new DangKy();
            Program.formDK.Show();
            this.Hide();
        }

        private void Staff_Click(object sender, EventArgs e)
        {
            Program.formNV_QL = new FrNhanVien();
            this.Hide();
            Program.formNV_QL.Show();
        }

        private void NCC_Click(object sender, EventArgs e)
        {
            Program.formNCC = new FrNhaCC();
            this.Hide();
            Program.formNCC.Show();
        }


    }
}
