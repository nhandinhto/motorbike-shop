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
    public partial class FrSanPham_NV_ : Form
    {
        SanPham_NV_ spnv = new SanPham_NV_();
        public FrSanPham_NV_()
        {
            InitializeComponent();
        }

        private void FrSanPham_NV__Load(object sender, EventArgs e)
        {
            dtgv_Xe.DataSource = spnv.LoadXe();
        }

        private void btn_TimKiem_Click(object sender, EventArgs e)
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
                DataTable dt = (DataTable)dtgv_Xe.DataSource;
                if (dt != null)
                {
                    dt.Clear();
                }
                dt = spnv.TimKiem(txt_TimKiem.Text);
                dtgv_Xe.DataSource = dt;
            }
            else
            {
                dtgv_Xe.DataSource = spnv.LoadXe();
            }
        }

        private void txt_TimKiem_TextChanged(object sender, EventArgs e)
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
                DataTable dt = (DataTable)dtgv_Xe.DataSource;
                if (dt != null)
                {
                    dt.Clear();
                }
                dt = spnv.TimKiem(txt_TimKiem.Text);
                dtgv_Xe.DataSource = dt;
            }
            else
            {
                dtgv_Xe.DataSource = spnv.LoadXe();
            }
        }

        private void FrSanPham_NV__FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.formSP.Hide();
            Program.formNV = new frmNhanVien();
            Program.formNV.Show();
        }
    }
}
