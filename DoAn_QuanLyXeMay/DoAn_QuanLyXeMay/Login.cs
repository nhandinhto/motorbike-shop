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
    public partial class Login : Form
    {
        NhanVien nv;
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            nv = new NhanVien();

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string id = txtID.Text;
            string pw = txtPW.Text;
            string chucvu = null;
            foreach (Control ctr in tableLayoutPanel1.Controls)
            {
                if (ctr.GetType() == typeof(RadioButton))
                {
                    RadioButton rd = ctr as RadioButton;
                    if (rd.Checked == true)
                    {
                        chucvu = rd.Text;
                        break;
                    }
                }
            }

            if (nv.login(id, pw,chucvu))
            {
                if (chucvu.Equals("Quản lý"))
                {
                    MessageBox.Show("Đăng nhập thành công");
                    Program.formQL = new frmQuanLy();
                    Program.formQL.Show();
                    this.Hide();
                    Program.formQL.Text = id;
                }
                else
                {
                    MessageBox.Show("Đăng nhập thành công");
                    Program.formNV = new frmNhanVien();
                    Program.formNV.Show();
                    this.Hide();
                    Program.formNV.Text = id;
                }
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác");
                return;
            }
        }

        /// <summary>
        /// Người dùng không được để trống thông tin
        /// Input : Text từ textbox control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkEmptyText(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text.Trim().Length <= 0)
            {
                this.errorProvider1.SetError(txt, "Vui lòng không để trống mục này");
                txt.Focus();
            }
            else
            {
                this.errorProvider1.Clear();
            }
        }

        /// <summary>
        /// Người dùng ko được nhập quá 16 ký tự
        /// </summary>
        /// Input : Text từ textbox control
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkMaxLength_16(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt.Text.Trim().Length > 16)
            {
                this.errorProvider2.SetError(txt, "Vui lòng nhập không quá 16 kí tự");
                txt.Focus();
            }
            else
            {
                this.errorProvider2.Clear();
            }
        }

        /// <summary>
        /// Kiểm tra sau khi click vào TextBox control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtID_Leave(object sender, EventArgs e)
        {
            checkEmptyText(sender, e);
        }

        /// <summary>
        /// Kiểm tra sau khi click vào TextBox control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPW_Leave(object sender, EventArgs e)
        {
            checkEmptyText(sender, e);
            checkMaxLength_16(sender, e);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
           DialogResult r = MessageBox.Show("Xác nhận thoát chương trình ","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1);
           if (r == DialogResult.Yes)
           {
               this.Close();
           }
          
        }

        /// <summary>
        /// Set hokey cho 2 nút Đăng Nhập và thoát
        /// Input : Dữ liều từ I/O khi nhấn vàn phím 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(btnLogin,new EventArgs());
            }
            else if (e.KeyCode == Keys.Escape)
            {
                btnThoat_Click(btnThoat, new EventArgs());
            }
        }

    }
}
