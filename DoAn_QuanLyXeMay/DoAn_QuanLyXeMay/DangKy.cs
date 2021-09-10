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
    public partial class DangKy : Form
    {
        xlDangKy dk = new xlDangKy();
        public DangKy()
        {
            InitializeComponent();
        }

        private void DangKy_Load(object sender, EventArgs e)
        {
            dtgv_TK.DataSource = dk.LoadTK();
        }

        public bool ktCheck(Panel p)
        {
            for ( int i = 0; i < p.Controls.Count; i++)
            {
                RadioButton rd = (RadioButton)p.Controls[i];
                if (rd.Checked)
                {
                    return true;
                }
            }
            return false;
        }

        private void dtgv_TK_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgv_TK.CurrentRow != null)
            {
                txt_Username.Text = dtgv_TK.CurrentRow.Cells[0].Value.ToString();
                txt_Password.Text = dtgv_TK.CurrentRow.Cells[1].Value.ToString();
                txt_ConfirmPW.Text = dtgv_TK.CurrentRow.Cells[1].Value.ToString();
                txt_MaNV.Text = dtgv_TK.CurrentRow.Cells[2].Value.ToString();
                string ltk = dtgv_TK.CurrentRow.Cells[3].Value.ToString().Trim();
                for (int i = 0; i < pn_LTK.Controls.Count; i++)
                {
                    RadioButton rd = (RadioButton)pn_LTK.Controls[i];
                    switch (ltk)
                    {
                        case "CV01":
                            if (rd.Name == "rdo_QuanLy")
                            {
                                rd.Checked = true;
                            }
                            break;
                        case "CV02":
                            if (rd.Name == "rdo_NhanVien")
                            {
                                rd.Checked = true;
                            }
                            break;
                    }
                }
            }
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DangKy_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void btn_DangKy_Click(object sender, EventArgs e)
        {
            if (dk.ktKhoaChinh(txt_Username.Text.ToString()))
            {
                if (ktCheck(pn_LTK))
                {
                    string maCV = "";
                    for(int i=0; i<pn_LTK.Controls.Count; i++)
                    {
                        RadioButton rd = (RadioButton)pn_LTK.Controls[i];
                        if(rd.Checked)
                        {
                            switch(rd.Text)
                            {
                                case "Nhân Viên":
                                    maCV = "CV02";
                                    break;
                                case "Quản Lý":
                                    maCV = "CV01";
                                    break;
                            }
                        }
                    }
                    if (dk.DangKyTK(txt_Username.Text.ToString(), txt_Password.Text.ToString(), txt_MaNV.Text.ToString(), maCV))
                    {
                        MessageBox.Show("Đăng ký tài khoản thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DataTable dt = (DataTable)dtgv_TK.DataSource;
                        dt.Clear();
                        dt = dk.LoadTK();
                        dtgv_TK.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("Thêm thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Là Wibu thì phải check", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Username này đã tồn tại! Hãy thử lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            if (!dk.ktKhoaChinh(txt_Username.Text.ToString()))
            {
                if (dk.xoaTK(txt_Username.Text.ToString()))
                {
                    MessageBox.Show("Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataTable dt = (DataTable)dtgv_TK.DataSource;
                    dt.Clear();
                    dt = dk.LoadTK();
                    dtgv_TK.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Xóa thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Username này không tồn tại! Hãy thử lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            string cv = dtgv_TK.CurrentRow.Cells[3].Value.ToString();
            if (ktCheck(pn_LTK))
            {
                string maCV = "";
                for (int i = 0; i < pn_LTK.Controls.Count; i++)
                {
                    RadioButton rd = (RadioButton)pn_LTK.Controls[i];
                    if (rd.Checked)
                    {
                        switch (rd.Text)
                        {
                            case "Nhân Viên":
                                maCV = "CV02";
                                break;
                            case "Quản Lý":
                                maCV = "CV01";
                                break;
                        }
                    }
                }
                if (dk.suaTK(txt_Username.Text.ToString(), txt_Password.Text.ToString(),txt_ConfirmPW.Text.ToString(), txt_MaNV.Text.ToString(), maCV))
                {
                    MessageBox.Show("Sửa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataTable dt = (DataTable)dtgv_TK.DataSource;
                    dt.Clear();
                    dt = dk.LoadTK();
                    dtgv_TK.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Sửa thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }   
            }
            else
            {
                MessageBox.Show("Là Wibu thì phải check", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }  
        }

        private void DangKy_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult t;
            t = MessageBox.Show("Bạn có muốn thoát không?", "Thoát", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            if (t == DialogResult.Yes)
            {
                this.Hide();
                Program.formQL.Show();
            }

        }
    }
}
