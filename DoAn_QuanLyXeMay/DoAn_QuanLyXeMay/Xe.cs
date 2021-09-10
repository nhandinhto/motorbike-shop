using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace DoAn_QuanLyXeMay
{
    public partial class Xe : Form
    {
        XuLy xuly = new XuLy();
        DataTable sp = new DataTable();
        DataTable ncc = new DataTable();
        public Xe()
        {
            InitializeComponent();
        }

        private void Xe_Load(object sender, EventArgs e)
        {
            xuly.loadDataGridview_SP(dgvSP);
            xuly.loadComboBox_NCC(cbNCC);
            DataTable sp = xuly.sp.loadSanPham();
            DataTable ncc = xuly.ncc.loadNCC();
            Databinding(sp, ncc);
            deActiveControls();
        }

        private void Databinding(DataTable sanpham, DataTable ncc)
        {
            txtMX.DataBindings.Clear();
            cbNCC.DataBindings.Clear();
            txtSK.DataBindings.Clear();
            txtSM.DataBindings.Clear();
            txtName.DataBindings.Clear();
            txtColor.DataBindings.Clear();
            txtPrice.DataBindings.Clear();
            txtImg.DataBindings.Clear();
            txtSL.DataBindings.Clear();

            txtMX.DataBindings.Add("Text", sanpham, "MaXe");
            txtSK.DataBindings.Add("Text", sanpham, "SoKhung");
            txtSM.DataBindings.Add("Text", sanpham, "SoMay");
            txtName.DataBindings.Add("Text", sanpham, "TenXe");
            txtColor.DataBindings.Add("Text", sanpham, "MauSac");
            txtPrice.DataBindings.Add("Text", sanpham, "Gia");
            txtSL.DataBindings.Add("Text", sanpham, "soluong");
            try
            {
                txtImg.DataBindings.Add("Text", sanpham, "hinh");
                imgXe.Image = new Bitmap(Application.StartupPath + "\\Hinh\\" + txtImg.Text);
            }
            catch
            {
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có muốn đóng chương trình không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                this.Close();
  
            }
        }

        private void deActiveControls()
        {
            foreach (Control ctr in this.tableLayoutPanel1.Controls)
            {
                if (ctr.GetType() == typeof(TextBox) || ctr.GetType() == typeof(ComboBox))
                {
                    ctr.Enabled = false;
                }
            }
            foreach (ToolStripButton btn in toolStrip1.Items)
            {
                if (btn.Name == "btnAdd" || btn.Name == "btnClose")
                {
                    btn.Enabled = true;
                }
                else
                {
                    btn.Enabled = false;
                }
            }
            txtTimKiem.Enabled = true;
        }

        private void ActiveTextBox()
        {
            foreach (Control ctr in this.tableLayoutPanel1.Controls)
            {
                if (ctr.GetType() == typeof(TextBox) || ctr.GetType()== typeof(ComboBox))
                {
                    ctr.Enabled = true;
                }
            }
        }

        private void Xe_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            Program.formQL.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ActiveTextBox();
            txtMX.Focus();
            btnSave.Enabled = true;
        }

        private void cbNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbNCC.SelectedIndex == 0)
            {
                DataTable data = (DataTable)dgvSP.DataSource;
                if (data != null)
                    data.Clear();
                xuly.loadDataGridview_SP(dgvSP);
                Databinding((DataTable)dgvSP.DataSource, ncc);
            }
            else
            {
                DataTable data = (DataTable)dgvSP.DataSource;
                if (data != null)
                    data.Clear();
                data = xuly.sp.loadSanPham_filter(cbNCC.SelectedValue.ToString());
                dgvSP.DataSource = data;
                Databinding(data, ncc);
            }
        }

        /// <summary>
        /// Tải 1 file ảnh từ 1 ổ đĩa bất kì lên picture box 
        /// File muốn tải phải có dạng JPG hoặc Dạng GIF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpload_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image Files|*.jpg;*.png;*.gif";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtImg.Text = openFileDialog1.FileName;
                Image img = Image.FromFile(txtImg.Text);
                imgXe.Image = img;
            }
        }

        /// <summary>
        /// Lưu ảnh từ PictureBox vào file Hinh trong thư mục debug 
        /// sử dụng đường dẫn mặc định
        /// Lưu file dưới dạng jpg hoặc gif
        /// Gán đường dẫn tới hình vào textbox img để thực hiện việc lưu trữ định dạng hình vào dtb
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveImg_Click(object sender, EventArgs e)
        {
            saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = Application.StartupPath + "\\Hinh\\";
            saveFileDialog1.Filter = "Image FILES|*.jpg;*.png;*.gif";
            ImageFormat format = ImageFormat.Png;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                imgXe.Image.Save(saveFileDialog1.FileName, format);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtMX.Enabled == true)
            {
                try
                {
                    if (xuly.sp.kiemTraTrung(txtMX.Text))
                    {
                        MessageBox.Show("Trùng Mã Xe", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        string[] anh = txtImg.Text.Split('\\');
                        string hinh = anh[anh.Length - 1];
                        if (xuly.Them(txtMX.Text, cbNCC.SelectedValue.ToString(), txtSK.Text, txtSM.Text, txtName.Text, txtColor.Text, txtPrice.Text, hinh, int.Parse(txtSL.Text)))
                        {
                            DataTable data = (DataTable)dgvSP.DataSource;
                            if (data != null)
                                data.Clear();
                            data = xuly.sp.loadSanPham_filter(cbNCC.SelectedValue.ToString());
                            dgvSP.DataSource = data;
                            Databinding(data, ncc);
                            MessageBox.Show("Thêm thành công");
                            btnSave.Enabled = false;
                            cbNCC.SelectedIndex = 0;
                            deActiveControls();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Thêm không thành công");
                    return;
                }
            }
            else
            {
                try
                {
                    string[] anh = txtImg.Text.Split('\\');
                    string hinh = anh[anh.Length - 1];
                    DataTable dgvdata = xuly.sp.loadSanPham();
                    if (xuly.Sua(dgvdata, txtMX.Text, cbNCC.SelectedValue.ToString(), txtSK.Text, txtSM.Text, txtName.Text, txtColor.Text, txtPrice.Text, hinh, int.Parse(txtSL.Text)))
                    {
                        DataTable data = dgvSP.DataSource as DataTable;
                        if (data != null)
                            data = null;
                        data = xuly.sp.loadSanPham_filter(txtMX.Text);
                        dgvSP.DataSource = data;
                        Databinding(data, ncc);
                        MessageBox.Show("Sửa thông tin thành công");
                        btnSave.Enabled = false;
                        cbNCC.SelectedIndex = 0;
                        deActiveControls();

                    }
                    else
                    {
                        MessageBox.Show("Sửa không thành công");
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Sửa không thành công");
                    return;
                }
            }
         
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ActiveTextBox();
            txtMX.Enabled = false;
            btnSave.Enabled = true;
        }

        private void dgvSP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvSP.CurrentRow != null && dgvSP.CurrentRow.Index >= 0)
                {

                    string mncc = dgvSP.CurrentRow.Cells[1].Value.ToString();
                    if (xuly.findNhaCC(mncc) != null)
                    {
                        DataRow dr = xuly.findNhaCC(mncc);
                        cbNCC.Text = dr["TenNCC"].ToString();
                        string imgURL = txtImg.Text;
                        imgXe.Image = new Bitmap(Application.StartupPath + "\\Hinh\\" + imgURL);
                    }
                    btnDel.Enabled = true;
                    btnEdit.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                return;
            }

        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có muốn xóa dữ liệu này không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                try
                {
                    if (dgvSP.CurrentRow != null && dgvSP.CurrentRow.Index >= 0)
                    {
                        if (xuly.Xoa(txtMX.Text))
                        {
                            DataTable data = (DataTable)dgvSP.DataSource;
                            if (data != null)
                                data.Clear();
                            data = xuly.sp.loadSanPham_filter(cbNCC.SelectedValue.ToString());
                            dgvSP.DataSource = data;
                            Databinding(data, ncc);
                            MessageBox.Show("Xóa thành công");
                            deActiveControls();
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Xóa không thành công");
                            return;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Xoa khong thanh cong");
                    return;
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtTimKiem.Text.Trim().Length > 0)
            {
                DataTable data_search = xuly.SanPham_Search(txtTimKiem.Text);
                if (data_search.Rows.Count > 0)
                {
                    DataTable data = dgvSP.DataSource as DataTable;
                    if (data != null)
                        data.Clear();
                    data = data_search;
                    dgvSP.DataSource = data;
                    Databinding(data, ncc);
                }
                else
                {
                    xuly.loadDataGridview_SP(dgvSP);
                    Databinding((DataTable)dgvSP.DataSource, ncc);
                }
            }
        }

    }
}
