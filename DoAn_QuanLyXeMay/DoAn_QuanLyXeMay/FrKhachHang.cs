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
    public partial class FrKhachHang : Form
    {
        KhachHang kh = new KhachHang();
        public FrKhachHang()
        {
            InitializeComponent();
        }

        private void FrKhachHang_Load(object sender, EventArgs e)
        {
            dtgvKhachHang.DataSource = kh.LoadKH();
        }

        private void dtgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgvKhachHang.CurrentRow != null)
            {
                txt_MaKH.Text = dtgvKhachHang.CurrentRow.Cells[0].Value.ToString();
                txt_HoTen.Text = dtgvKhachHang.CurrentRow.Cells[1].Value.ToString();
                //pn_GioiTinh.Text = dtgvKhachHang.CurrentRow.Cells[2].Value.ToString(); sửa lại thành vòng for
                string gt = dtgvKhachHang.CurrentRow.Cells[2].Value.ToString();
                for (int i = 0; i < pn_GioiTinh.Controls.Count; i++)
                {
                    RadioButton rd = (RadioButton)pn_GioiTinh.Controls[i];
                    if (rd.Text == gt)
                    {
                        rd.Checked = true;
                    }

                }
                txt_DiaChi.Text = dtgvKhachHang.CurrentRow.Cells[3].Value.ToString();
                txt_SDT.Text = dtgvKhachHang.CurrentRow.Cells[4].Value.ToString();
                txt_CMND.Text = dtgvKhachHang.CurrentRow.Cells[5].Value.ToString();
            }
        }
        private void txt_MaKH_Leave(object sender, EventArgs e)
        {
            Control ctr = (Control)sender;
            if (ctr.Text.Length <= 0)
            {
                this.errorProvider2.SetError(ctr, "Bạn không được để trống");
                txt_MaKH.Focus();
            }
            else
            {
                this.errorProvider2.Clear();
            }
        }

        private void txt_SDT_TextChanged(object sender, EventArgs e)
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
            if (kh.ktKhoaChinh(txt_MaKH.Text.ToString()))
            {
                string gt = dtgvKhachHang.CurrentRow.Cells[2].Value.ToString();
                for (int i = 0; i < pn_GioiTinh.Controls.Count; i++)
                {
                    RadioButton rd = (RadioButton)pn_GioiTinh.Controls[i];
                    if (rd.Checked)
                    {
                        gt = rd.Text;
                    }
                }
                if (kh.themKH(txt_MaKH.Text.ToString(), txt_HoTen.Text.ToString(), gt, txt_DiaChi.Text.ToString(), txt_SDT.Text.ToString(), txt_CMND.Text.ToString()))
                {
                    MessageBox.Show("Thêm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataTable dt = (DataTable)dtgvKhachHang.DataSource;
                    dt.Clear();
                    dt = kh.LoadKH();
                    dtgvKhachHang.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Thêm thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Mã khách hàng này đã tồn tại! Hãy thử lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }                                     
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            if (!kh.ktKhoaChinh(txt_MaKH.Text.ToString()))
            {
                if (kh.xoaKH(txt_MaKH.Text.ToString()))
                {
                    MessageBox.Show("Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataTable dt = (DataTable)dtgvKhachHang.DataSource;
                    dt.Clear();
                    dt = kh.LoadKH();
                    dtgvKhachHang.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Xóa thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Mã khách hàng này không tồn tại! Hãy thử lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            string gt = dtgvKhachHang.CurrentRow.Cells[2].Value.ToString();
                for (int i = 0; i < pn_GioiTinh.Controls.Count; i++)
                {
                    RadioButton rd = (RadioButton)pn_GioiTinh.Controls[i];
                    if (rd.Checked)
                    {
                        gt = rd.Text;
                    }
                }
                if (kh.suaKH(txt_MaKH.Text, txt_HoTen.Text,gt, txt_DiaChi.Text, txt_SDT.Text, txt_CMND.Text))
                {
                    MessageBox.Show("Sửa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataTable dt = (DataTable)dtgvKhachHang.DataSource;
                    dt.Clear();
                    dt = kh.LoadKH();
                    dtgvKhachHang.DataSource = dt;
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

        private void FrKhachHang_FormClosing(object sender, FormClosingEventArgs e)
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
                Program.formNV = new frmNhanVien();
                Program.formNV.Show();
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
                DataTable dt = (DataTable)dtgvKhachHang.DataSource;
                if (dt != null)
                {
                    dt.Clear();
                }
                dt = kh.TimKiem(txt_TimKiem.Text);
                dtgvKhachHang.DataSource = dt;
            }
            else
            {
                dtgvKhachHang.DataSource = kh.LoadKH();
            }
        }

        //private void txt_TimKiem_TextChanged(object sender, EventArgs e)
        //{
        //    if (txt_TimKiem.Text.Trim().Length > 0)
        //    {
        //        //if (data_search.Rows.Count > 0)
        //        //{
        //        //    DataTable data = dtgv_Xe.DataSource as DataTable;
        //        //    if (data != null)
        //        //        data.Clear();
        //        //    data = data_search;
        //        //    dtgv_Xe.DataSource = data;
        //        //}
        //        DataTable dt = (DataTable)dtgv_Xe.DataSource;
        //        if (dt != null)
        //        {
        //            dt.Clear();
        //        }
        //        dt = spnv.TimKiem(txt_TimKiem.Text);
        //        dtgv_Xe.DataSource = dt;
        //    }
        //    else
        //    {
        //        dtgv_Xe.DataSource = spnv.LoadXe();
        //    }
        //}
     }
}
