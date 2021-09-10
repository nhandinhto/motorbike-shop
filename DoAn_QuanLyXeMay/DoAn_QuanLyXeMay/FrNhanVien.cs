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
    public partial class FrNhanVien : Form
    {
        NhanVien nv = new NhanVien();
        public FrNhanVien()
        {
            InitializeComponent();
        }

        private void FrNhanVien_Load(object sender, EventArgs e)
        {
            dtgvNhanVien.DataSource = nv.LoadNV();
        }

        private void dtgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgvNhanVien.CurrentRow != null)
            {
                txt_MaNV.Text = dtgvNhanVien.CurrentRow.Cells[0].Value.ToString();
                txt_HoTen.Text = dtgvNhanVien.CurrentRow.Cells[1].Value.ToString();
                //pn_GioiTinh.Text = dtgvKhachHang.CurrentRow.Cells[2].Value.ToString(); sửa lại thành vòng for
                string gt = dtgvNhanVien.CurrentRow.Cells[2].Value.ToString();
                for (int i = 0; i < pn_GioiTinh.Controls.Count; i++)
                {
                    RadioButton rd = (RadioButton)pn_GioiTinh.Controls[i];
                    if (rd.Text == gt)
                    {
                        rd.Checked = true;
                    }

                }
                txt_CMND.Text = dtgvNhanVien.CurrentRow.Cells[3].Value.ToString();
                txt_DiaChi.Text = dtgvNhanVien.CurrentRow.Cells[4].Value.ToString();
                txt_Luong.Text = dtgvNhanVien.CurrentRow.Cells[5].Value.ToString();
            }
        }

        private void txt_MaNV_Leave(object sender, EventArgs e)
        {
            Control ctr = (Control)sender;
            if (ctr.Text.Length <= 0)
            {
                this.errorProvider2.SetError(ctr, "Bạn không được để trống");
                txt_MaNV.Focus();
            }
            else
            {
                this.errorProvider2.Clear();
            }
        }

        private void txt_Luong_TextChanged(object sender, EventArgs e)
        {
            Control ctr = (Control)sender;
            if (ctr.Text.Length > 0 && !char.IsDigit(ctr.Text[ctr.Text.Length - 1]))
                this.errorProvider1.SetError(ctr, "Chỉ được nhập số");
            else
                this.errorProvider1.Clear();
        }

        private void txt_CMND_TextChanged(object sender, EventArgs e)
        {
            Control ctr = (Control)sender;
            if (ctr.Text.Length > 0 && !char.IsDigit(ctr.Text[ctr.Text.Length - 1]))
                this.errorProvider1.SetError(ctr, "Chỉ được nhập số");
            else
                this.errorProvider1.Clear();
        }

        private void txt_DiaChi_Leave(object sender, EventArgs e)
        {
            Control ctr = (Control)sender;
            if (ctr.Text.Length <= 0)
            {
                this.errorProvider2.SetError(ctr, "Bạn không được để trống");
                txt_DiaChi.Focus();
            }
            else
            {
                this.errorProvider2.Clear();
            }
        }

        private void txt_HoTen_Leave(object sender, EventArgs e)
        {
            Control ctr = (Control)sender;
            if (ctr.Text.Length <= 0)
            {
                this.errorProvider2.SetError(ctr, "Bạn không được để trống");
                txt_HoTen.Focus();
            }
            else
            {
                this.errorProvider2.Clear();
            }
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            if (nv.ktKhoaChinh(txt_MaNV.Text.ToString()))
            {
                string gt = dtgvNhanVien.CurrentRow.Cells[2].Value.ToString();
                for (int i = 0; i < pn_GioiTinh.Controls.Count; i++)
                {
                    RadioButton rd = (RadioButton)pn_GioiTinh.Controls[i];
                    if (rd.Checked)
                    {
                        gt = rd.Text;
                    }
                }
                if (nv.themKH(txt_MaNV.Text.ToString(), txt_HoTen.Text.ToString(), gt, txt_CMND.Text.ToString(), txt_DiaChi.Text.ToString(), txt_Luong.Text.ToString()))
                {
                    MessageBox.Show("Thêm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataTable dt = (DataTable)dtgvNhanVien.DataSource;
                    dt.Clear();
                    dt = nv.LoadNV();
                    dtgvNhanVien.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Thêm thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Mã nhân viên này đã tồn tại! Hãy thử lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }                             
        }

        private void btn_Tim_Click(object sender, EventArgs e)
        {
            if (txt_TimKiem.Text.Trim().Length > 0)
            {
                //if (data_search.Rows.Count > 0)
                //{
                //    DataTable data = dtgvKhachHang.DataSource as DataTable;
                //    if (data != null)
                //        data.Clear();
                //    data = data_search;
                //    dtgvKhachHang.DataSource = data;
                //}
                DataTable dt = (DataTable)dtgvNhanVien.DataSource;
                if (dt != null)
                {
                    dt.Clear();
                }
                dt = nv.TimKiem(txt_TimKiem.Text);
                dtgvNhanVien.DataSource = dt;
            }
            else
            {
                dtgvNhanVien.DataSource = nv.LoadNV();
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            if (!nv.ktKhoaChinh(txt_MaNV.Text.ToString()))
            {
                if (nv.xoaNV(txt_MaNV.Text.ToString()))
                {
                    MessageBox.Show("Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataTable dt = (DataTable)dtgvNhanVien.DataSource;
                    dt.Clear();
                    dt = nv.LoadNV();
                    dtgvNhanVien.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Xóa thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Mã nhân viên này không tồn tại! Hãy thử lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            string gt = dtgvNhanVien.CurrentRow.Cells[3].Value.ToString();
            for (int i = 0; i < pn_GioiTinh.Controls.Count; i++)
            {
                RadioButton rd = (RadioButton)pn_GioiTinh.Controls[i];
                if (rd.Checked)
                {
                    gt = rd.Text;
                }
            }
            if (nv.suaKH(txt_MaNV.Text, txt_HoTen.Text, gt,txt_CMND.Text, txt_DiaChi.Text, txt_Luong.Text))
            {
                MessageBox.Show("Sửa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataTable dt = (DataTable)dtgvNhanVien.DataSource;
                dt.Clear();
                dt = nv.LoadNV();
                dtgvNhanVien.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Sửa thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }        
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrNhanVien_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult t;
            t = MessageBox.Show("Bạn có muốn thoát không?", "Thoát", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            if (t == DialogResult.No || t == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
            else
            {
                this.Hide();
                Program.formQL = new frmQuanLy();
                Program.formQL.Show();
            }
        }

        private void FrNhanVien_FormClosed(object sender, FormClosedEventArgs e)
        {
 
        }
    }
}
