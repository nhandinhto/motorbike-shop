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
    public partial class FrNhaCC : Form
    {
        NhaCungCap ncc = new NhaCungCap();
        public FrNhaCC()
        {
            InitializeComponent();
        }

        private void FrNhaCC_Load(object sender, EventArgs e)
        {
            dtgv_NhaCC.DataSource = ncc.loadNhaCC();
        }

        private void dtgv_NhaCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgv_NhaCC.CurrentRow != null)
            {
                txt_MaNCC.Text = dtgv_NhaCC.CurrentRow.Cells[0].Value.ToString();
                txt_TenNCC.Text = dtgv_NhaCC.CurrentRow.Cells[1].Value.ToString();
                txt_DiaChi.Text = dtgv_NhaCC.CurrentRow.Cells[2].Value.ToString();
                txt_SDT.Text = dtgv_NhaCC.CurrentRow.Cells[3].Value.ToString();
                txt_Gmail.Text = dtgv_NhaCC.CurrentRow.Cells[4].Value.ToString();
            }
        }

        private void txt_MaNCC_Leave(object sender, EventArgs e)
        {
            Control ctr = (Control)sender;
            if (ctr.Text.Length <= 0)
            {
                this.errorProvider1.SetError(ctr, "Bạn không được để trống");
                txt_MaNCC.Focus();
            }
            else
            {
                this.errorProvider1.Clear();
            }
        }

        private void txt_SDT_TextChanged(object sender, EventArgs e)
        {
            Control ctr = (Control)sender;
            if (ctr.Text.Length > 0 && !char.IsDigit(ctr.Text[ctr.Text.Length - 1]))
                this.errorProvider2.SetError(ctr, "Chỉ được nhập số");
            else
                this.errorProvider2.Clear();
        }

        private void txt_TenNCC_Leave(object sender, EventArgs e)
        {
            Control ctr = (Control)sender;
            if (ctr.Text.Length <= 0)
            {
                this.errorProvider1.SetError(ctr, "Bạn không được để trống");
                txt_TenNCC.Focus();
            }
            else
            {
                this.errorProvider1.Clear();
            }
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            if (ncc.ktKhoaChinh(txt_MaNCC.Text.ToString()))
            {              
                if (ncc.themNCC(txt_MaNCC.Text.ToString(), txt_TenNCC.Text.ToString(),txt_DiaChi.Text.ToString(), txt_SDT.Text.ToString(), txt_Gmail.Text.ToString()))
                {
                    MessageBox.Show("Thêm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataTable dt = (DataTable)dtgv_NhaCC.DataSource;
                    dt.Clear();
                    dt = ncc.loadNhaCC();
                    dtgv_NhaCC.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Thêm thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Mã nhà cung cấp này đã tồn tại! Hãy thử lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }               
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            if (!ncc.ktKhoaChinh(txt_MaNCC.Text.ToString()))
            {
                if (ncc.xoaNCC(txt_MaNCC.Text.ToString()))
                {
                    MessageBox.Show("Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataTable dt = (DataTable)dtgv_NhaCC.DataSource;
                    dt.Clear();
                    dt = ncc.loadNhaCC();
                    dtgv_NhaCC.DataSource = dt;
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
            if (ncc.suaNCC(txt_MaNCC.Text, txt_TenNCC.Text, txt_DiaChi.Text, txt_SDT.Text, txt_Gmail.Text))
            {
                MessageBox.Show("Sửa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataTable dt = (DataTable)dtgv_NhaCC.DataSource;
                dt.Clear();
                dt = ncc.loadNhaCC();
                dtgv_NhaCC.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Sửa thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Thoa_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrNhaCC_FormClosing(object sender, FormClosingEventArgs e)
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

        private void btn_Tim_Click(object sender, EventArgs e)
        {
            if (txt_TimKiem.Text.Trim().Length > 0)
            {
                //if (data_search.Rows.Count > 0)
                //{
                //    DataTable data = dtgv_Xe.DataSource as DataTable;
                //    if (data != null)
                //        data.Clear();
                //    data = data_search;
                //    dtgv_Xe.DataSource = data;
                //}
                DataTable dt = (DataTable)dtgv_NhaCC.DataSource;
                if (dt != null)
                {
                    dt.Clear();
                }
                dt = ncc.TimKiem(txt_TimKiem.Text);
                dtgv_NhaCC.DataSource = dt;
            }
            else
            {
                dtgv_NhaCC.DataSource = ncc.loadNhaCC();
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
        //        DataTable dt = (DataTable)dtgv_NhaCC.DataSource;
        //        if (dt != null)
        //        {
        //            dt.Clear();
        //        }
        //        dt = ncc.TimKiem(txt_TimKiem.Text);
        //        dtgv_NhaCC.DataSource = dt;
        //    }
        //    else
        //    {
        //        dtgv_NhaCC.DataSource = ncc.loadNhaCC();
        //    }
        //}
    }
}
