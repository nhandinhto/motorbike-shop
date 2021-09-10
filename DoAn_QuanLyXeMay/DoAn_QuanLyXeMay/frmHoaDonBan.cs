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
    public partial class frmHoaDonBan : Form
    {
        XuLy xuly = new XuLy();

        public frmHoaDonBan()
        {
            InitializeComponent();
        }


        private void deActiveControl()
        {
            foreach (Control ctr in this.tableLayoutPanel2.Controls)
            {
                if (ctr.GetType() == typeof(TextBox))
                {
                    ctr.Enabled = false;
                }
            }
            foreach (Control ctr in this.tableLayoutPanel3.Controls)
            {
                if (ctr.GetType() == typeof(TextBox))
                {
                    ctr.Enabled = false;
                }
            }
        }

        private void ActiveControl_text(TableLayoutPanel tb)
        {
            foreach (Control ctr in tb.Controls)
            {
                if (ctr.GetType() == typeof(TextBox))
                {
                    ctr.Enabled = true;
                }
            }
        }

        private void frmHoaDonBan_Load(object sender, EventArgs e)
        {
            deActiveControl();
            xuly.loadComBoBox_Mnv_hd(cbMNV);
            xuly.loadDataGridview_CTHD(dgv);
            xuly.loadComBoBox_HD_Xe(cbXe);
            xuly.loadComBoBox_HD(cbHD);
            xuly.loadComBoBox_KH_HD(cbKH);
        }


        private void btnTPN_Click(object sender, EventArgs e)
        {
            ActiveControl_text(this.tableLayoutPanel2);
            txtMHD.Focus();
            txtThanhTien.Enabled = false;
            cbMNV.Enabled = true;
            dateTimePicker1.Enabled = true;
            btnLPN.Enabled = true;
            DateTime today = DateTime.Now;
            string day = today.ToShortDateString();
            dateTimePicker1.Text = day;
            cbMNV.SelectedIndex = 0;
            cbKH.Enabled = true;
            cbKH.SelectedIndex = 0;
            clearTable1();
        }

        private void btnLPN_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMHD.Enabled == true)
                {
                    if (xuly.ThemHD(txtMHD.Text, cbMNV.SelectedValue.ToString(), cbKH.SelectedValue.ToString(), dateTimePicker1.Value.ToString()))
                    {
                        MessageBox.Show("Thêm hóa đơn thành công");
                        DataTable data = cbHD.DataSource as DataTable;
                        if (data != null)
                            data.Clear();
                        xuly.loadComBoBox_HD(cbHD);
                    }
                    else
                    {
                        MessageBox.Show("Thêm hóa đơn không thành công");
                    }
                }
                else
                {
                    if (xuly.EditHD(txtMHD.Text, cbMNV.SelectedValue.ToString(), cbKH.SelectedValue.ToString(), dateTimePicker1.Value.ToString()))
                    {
                        MessageBox.Show("Sửa hóa đơn thành công");
                    }
                    else
                    {
                        MessageBox.Show("Sửa hóa đơn không thành công");
                    }
                }

                foreach (Control ctr in this.tableLayoutPanel2.Controls)
                {
                    ctr.Enabled = false;
                }
                btnTPN.Enabled = true;

                deActiveControl();
            }
            catch(Exception Ex)
            {
                return;
            }
        }

        private void clearTable1()
        {
            foreach (Control ctr in tableLayoutPanel2.Controls)
            {
                if (ctr.GetType() == typeof(TextBox))
                {
                    ctr.Text = "";
                }
            }
        }

        private void clearTable2()
        {
            foreach (Control ctr in tableLayoutPanel3.Controls)
            {
                if (ctr.GetType() == typeof(TextBox))
                {
                    ctr.Text = "";
                }
            }
        }

        private void btnFixHD_Click(object sender, EventArgs e)
        {

            btnTPN.Enabled = false;
            btnLPN.Enabled = true;
            ActiveControl_text(this.tableLayoutPanel2);
            txtMHD.Enabled = false;
            dateTimePicker1.Enabled = true;
            cbMNV.Enabled = true;
            cbKH.Enabled = true;
            txtThanhTien.Enabled = false;
        }

        private void btnCHD_Click(object sender, EventArgs e)
        {
            cbHD.Enabled = true;
            ActiveControl_text(tableLayoutPanel3);
            cbXe.Enabled = true;
            txtTT.Enabled = false;
            txtDG.Enabled = false;
        }

        private void cbHD_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbHD.SelectedIndex > 0)
                {
                    string mahd = cbHD.SelectedValue.ToString();

                    DataTable data = xuly.hd.loadHDB();
                    DataRow dr_data = data.Rows.Find(mahd);

                    DataTable data_kh = cbKH.DataSource as DataTable;
                    DataRow dr_kh = data_kh.Rows.Find(dr_data["MaKH"].ToString());
                    cbKH.Text = dr_kh["HoTenKH"].ToString();

                    txtMHD.Text = mahd;

                    DataTable data_nv = xuly.nv.loadNV();
                    DataRow dr_nv = data_nv.Rows.Find(dr_data["MaNV"].ToString());
                    cbMNV.Text = dr_nv["HoTenNV"].ToString();

                    string[] money_s = dr_data["TongTien"].ToString().Split('.');
                    double tt = Convert.ToInt64(money_s[0]);
                    txtThanhTien.Text = tt.ToString();

                    string date = dr_data["NgayLap"].ToString();
                    DateTime day = DateTime.Parse(date);
                    dateTimePicker1.Text = string.Format("{0:dd/MM/yyyy}", day.ToShortDateString());
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btnTSP_Click(object sender, EventArgs e)
        {
            try
            {

                string maXe = cbXe.SelectedValue.ToString();
                string maHD = cbHD.SelectedValue.ToString();

                string []key ={maHD,maXe};
                    
                DataTable data = dgv.DataSource as DataTable;
                DataRow dr = data.Rows.Find(key);
                if (dr != null)
                {
                    int sl = int.Parse(txtSL.Text);
                    sl += int.Parse(dr["soluong"].ToString());
                    if (xuly.ThemCTHD2(dr["MaHD"].ToString(), dr["MaXe"].ToString(), sl, Convert.ToInt64(txtDG.Text), Convert.ToInt64(txtTT.Text)))
                    {
                        MessageBox.Show("Thêm thành công");
                        DataTable data_Gv = dgv.DataSource as DataTable;
                        if (data_Gv != null)
                            data_Gv.Clear();
                        xuly.loadDataGridview_CTHD(dgv);
                    }
                    else
                    {
                        MessageBox.Show("Thêm không thành công");
                    }
                }
                else
                {
                    int sl = int.Parse(txtSL.Text);
                    if (xuly.ThemCTHD(txtMHD.Text, cbXe.SelectedValue.ToString(), sl, Convert.ToInt64(txtDG.Text), Convert.ToInt64(txtTT.Text)))
                    {
                        MessageBox.Show("Thêm thành công");
                        DataTable data_Gv = dgv.DataSource as DataTable;
                        if (data_Gv != null)
                            data_Gv.Clear();
                        xuly.loadDataGridview_CTHD(dgv);
                    }
                    else
                    {
                        MessageBox.Show("Thêm không thành công");
                    }
                }
                clearTable2();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cbXe_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbXe.SelectedIndex > 0)
                {
                    string maXE = cbXe.SelectedValue.ToString();
                    DataTable data_Xe = xuly.sp.search(maXE);
                    if (data_Xe.Rows.Count < 0)
                    {
                        return;
                    }
                    txtDG.Text = data_Xe.Rows[0]["Gia"].ToString();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void txtSL_TextChanged(object sender, EventArgs e)
        {
            if (txtSL.Text.Trim().Length > 0)
            {
                int soLuong = int.Parse(txtSL.Text);
                string[] money_s = txtDG.Text.Split('.'); ;
                int gia = Convert.ToInt32(money_s[0]);
                double tien = gia * soLuong;
                txtTT.Text = "" + tien;
            }
        }

        private void dgv_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgv.CurrentRow.Index;
            string maHD = dgv.Rows[index].Cells[0].Value.ToString();

            DataTable data = cbHD.DataSource as DataTable;
            DataRow dr_hd = data.Rows.Find(maHD);
            cbHD.Text = dr_hd["MaHD"].ToString();

            string maXe = dgv.Rows[index].Cells[1].Value.ToString();
            DataTable data_xe = cbXe.DataSource as DataTable;
            DataRow dr_xe = data_xe.Rows.Find(maXe);
            cbXe.Text = dr_xe["TenXe"].ToString();

            txtDG.Text = dr_xe["Gia"].ToString();

            txtSL.Text = dgv.Rows[index].Cells[2].Value.ToString();

            txtTT.Text = dgv.Rows[index].Cells[4].Value.ToString();

            if (txtSL.Text.Trim().Length > 0)
            {
                double dg = Convert.ToInt64(txtDG.Text);
                int sl = int.Parse(txtSL.Text);
                txtTT.Text = (dg * sl).ToString();
            }

            btnFixHD.Enabled = true;

        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow != null && dgv.CurrentRow.Index >= 0 && txtSL.Text.Trim().Length >0)
            {
                if (xuly.DeleteCTHD(dgv.CurrentRow.Cells[0].Value.ToString(), dgv.CurrentRow.Cells[1].Value.ToString()))
                {
                    MessageBox.Show("Xóa thành công");
                    DataTable data_Gv = dgv.DataSource as DataTable;
                    if (data_Gv != null)
                        data_Gv.Clear();
                    xuly.loadDataGridview_CTHD(dgv);
                }
                else
                {
                    MessageBox.Show("Xóa không thành công");
                }

            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng dữ liệu");
            }
        }

        private void Edit_Click(object sender, EventArgs e)
        {
                if (dgv.CurrentRow != null && dgv.CurrentRow.Index >= 0 && txtSL.Text.Trim().Length > 0)
                {
                    cbHD.Enabled = false; ;
                    ActiveControl_text(tableLayoutPanel3);
                    cbXe.Enabled = true;
                    txtTT.Enabled = false;
                    txtDG.Enabled = false;
                }
 
        }

        private void btnEditHD_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.CurrentRow != null && dgv.CurrentRow.Index >= 0)
                {
                    if (xuly.UpdateCTHD(cbHD.SelectedValue.ToString(), cbXe.SelectedValue.ToString(), int.Parse(txtSL.Text), Convert.ToInt64(txtDG.Text), Convert.ToInt64(txtTT.Text)))
                    {
                        MessageBox.Show("Cập nhật thành công");
                        DataTable data_Gv = dgv.DataSource as DataTable;
                        if (data_Gv != null)
                            data_Gv.Clear();
                        xuly.loadDataGridview_CTHD(dgv);
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật không thanh công");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn dòng dữ liệu");
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMHD.Text.Trim().Length > 0)
                {
                        printPreviewDialog1.Document = new System.Drawing.Printing.PrintDocument();
                        printPreviewDialog1.Document = printDocument1;
                        printPreviewDialog1.Show();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                e.Graphics.DrawString("Chi tiết hóa đơn",new Font("Arial",34,FontStyle.Bold),Brushes.Black,new PointF(250,30));
                e.Graphics.DrawString("Mã hóa đơn : " + txtMHD.Text, new Font("Arial", 18, FontStyle.Regular), Brushes.Black, new PointF(0, 90));
                e.Graphics.DrawString("Nhân viên bán : " + cbMNV.Text, new Font("Arial", 18, FontStyle.Regular), Brushes.Black, new PointF(0, 150));
                e.Graphics.DrawString("Ngày lập hóa đơn : " + dateTimePicker1.Text, new Font("Arial", 18, FontStyle.Regular), Brushes.Black, new PointF(0, 210));
                e.Graphics.DrawString("Tên khách Hàng : " +  cbKH.Text, new Font("Arial", 18, FontStyle.Regular), Brushes.Black, new PointF(0, 270));

                DataTable data_kh = xuly.hd.loadKH();
                DataRow dr = data_kh.Rows.Find(cbKH.SelectedValue);

                e.Graphics.DrawString("Điện thoại : " + dr["SDT"].ToString(), new Font("Arial", 18, FontStyle.Regular), Brushes.Black, new PointF(0, 330));
                e.Graphics.DrawString("CMND : " + dr["SoCMND"].ToString(), new Font("Arial", 18, FontStyle.Regular), Brushes.Black, new PointF(0, 390));
                e.Graphics.DrawString("Chi tiết các mặt hàng", new Font("Arial", 34, FontStyle.Regular), Brushes.Black, new PointF(200,450));
                e.Graphics.DrawString("-----------------------------------------------------------------------------------------------------------------", new Font("Arial", 18, FontStyle.Regular), Brushes.Black, new PointF(0, 510));
                e.Graphics.DrawString("Tên xe" , new Font("Arial", 20, FontStyle.Regular), Brushes.Black, new PointF(0,530));
                e.Graphics.DrawString("Số lượng", new Font("Arial", 20, FontStyle.Regular), Brushes.Black, new PointF(400, 530));
                e.Graphics.DrawString("Thành tiền", new Font("Arial", 20, FontStyle.Regular), Brushes.Black, new PointF(700, 530));
                e.Graphics.DrawString("-----------------------------------------------------------------------------------------------------------------", new Font("Arial", 18, FontStyle.Regular), Brushes.Black, new PointF(0, 550));
                DataTable data_hd = xuly.hd.loadDetailHD(txtMHD.Text);
                int ypost = 580;
                int xpost = 0;
                for (int i = 0; i < data_hd.Rows.Count; i++)
                {
                    e.Graphics.DrawString(data_hd.Rows[i]["TenXe"].ToString(), new Font("Arial", 20, FontStyle.Regular), Brushes.Black, new PointF(xpost,ypost));
                    xpost += 400;
                    e.Graphics.DrawString(data_hd.Rows[i]["soluong"].ToString(), new Font("Arial", 20, FontStyle.Regular), Brushes.Black, new PointF(xpost, ypost));
                    xpost += 300;
                    e.Graphics.DrawString(data_hd.Rows[i]["thanhTien"].ToString(), new Font("Arial", 20, FontStyle.Regular), Brushes.Black, new PointF(xpost, ypost));
                    ypost += 30;
                    xpost = 0;
                }
                ypost += 30;
                e.Graphics.DrawString("-----------------------------------------------------------------------------------------------------------------", new Font("Arial", 18, FontStyle.Regular), Brushes.Black, new PointF(0, ypost));
                ypost += 30;
                e.Graphics.DrawString("Tổng tiền : " + txtThanhTien.Text, new Font("Arial", 20, FontStyle.Regular), Brushes.Black, new PointF(570, ypost));
                ypost += 50;
                e.Graphics.DrawString("Người lập hóa đơn" , new Font("Arial", 20, FontStyle.Regular), Brushes.Black, new PointF(100, ypost));
                e.Graphics.DrawString("Khách hàng", new Font("Arial", 20, FontStyle.Regular), Brushes.Black, new PointF(600, ypost));
                ypost += 80;
                e.Graphics.DrawString("_____________________", new Font("Arial", 20, FontStyle.Regular), Brushes.Black, new PointF(70, ypost));
                e.Graphics.DrawString("__________________", new Font("Arial", 20, FontStyle.Regular), Brushes.Black, new PointF(550, ypost));
                ypost += 50;
                e.Graphics.DrawString("Ngày___Tháng____Năm___", new Font("Arial", 20, FontStyle.Regular), Brushes.Black, new PointF(40, ypost));
                e.Graphics.DrawString("Ngày___Tháng____Năm___", new Font("Arial", 20, FontStyle.Regular), Brushes.Black, new PointF(470, ypost));
                data_hd.Clear();
            }
            catch(Exception ex)
            {
            }
        }

        private void printPreviewDialog1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Form f = sender as Form;
            f.Hide();
            
        }

        private void frmHoaDonBan_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            Program.formNV = new frmNhanVien();
            Program.formNV.Show();
        }
    }
 
}
