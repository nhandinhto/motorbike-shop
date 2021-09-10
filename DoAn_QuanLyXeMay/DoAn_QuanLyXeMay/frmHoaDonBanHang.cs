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
    public partial class frmHoaDonBanHang : Form
    {
        DataSet ds_HDB = new DataSet();
        XuLy xuly = new XuLy();

        public frmHoaDonBanHang()
        {
            InitializeComponent();
            DataGridViewComboBoxColumn cbColum = (DataGridViewComboBoxColumn)dgvHD.Columns["cbMNV"];
            cbColum.DataSource = xuly.loadMNV();
            cbColum.DisplayMember = "MaNV";
            cbColum.ValueMember = "MaNV";
        }

        private void frmHoaDonBanHang_Load(object sender, EventArgs e)
        {
            DateTime today = DateTime.Now;
            string td = today.ToShortDateString();
            this.dateTimePicker1.Value = DateTime.Parse(td);
            xuly.loadDataGridview_HoaDonBan(dgvHD);
            deActiveControls();
            dataBinding((DataTable)dgvHD.DataSource);
         
        }

        private void dgvHD_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if(e.Exception.Message =="DataGridViewComboBoxCell value is not valid." )
            {
                
                
            }
        }

        private void deActiveControls()
        {
            foreach (Control ctr in this.tablePlayout.Controls)
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
            txtSearch.Enabled = true;
            dateTimePicker1.Enabled = false;
        }

        private void ActiveTextBox()
        {
            foreach (Control ctr in tablePlayout.Controls)
            {
                if (ctr.GetType() == typeof(TextBox))
                {
                    ctr.Enabled = true;
                }
            }
        }

        private void dataBinding(DataTable data)
        {
            txtDG.DataBindings.Clear();
            txtMHD.DataBindings.Clear();
            txtMKH.DataBindings.Clear();
            txtMNV.DataBindings.Clear();
            txtMS.DataBindings.Clear();
            txtMSK.DataBindings.Clear();
            txtMSM.DataBindings.Clear();
            txtMX.DataBindings.Clear();
            dateTimePicker1.DataBindings.Clear();

            txtDG.DataBindings.Add("Text", data, "MauSac");
            txtMHD.DataBindings.Add("Text", data, "MaHD");
            txtMKH.DataBindings.Add("Text", data, "MaKH");
            txtMNV.DataBindings.Add("Text", data, "MaNV");
            txtMS.DataBindings.Add("Text", data, "MauSac");
            txtMSK.DataBindings.Add("Text", data, "SoKhung");
            txtMSM.DataBindings.Add("Text", data, "SoMay");
            txtMX.DataBindings.Add("Text", data, "MaXe");
            dateTimePicker1.DataBindings.Add("Text", data, "NgayLap", true);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có muốn thoát hay không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            dgvHD.ReadOnly = false;
            dgvHD.AllowUserToAddRows = true;
            for (int i = 0; i < dgvHD.Rows.Count - 1; i++)
            {
                dgvHD.Rows[i].ReadOnly = true;
            }
            dgvHD.FirstDisplayedScrollingRowIndex = dgvHD.Rows.Count-1;
        }

        private void dgvHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEdit.Enabled = true;
            btnDel.Enabled = true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            dgvHD.ReadOnly = false;
            for (int i = 0; i < dgvHD.Rows.Count - 1; i++)
            {
                dgvHD.Rows[i].ReadOnly = false;
            }
            dgvHD.Columns[0].ReadOnly = true;
            dgvHD.Columns[1].ReadOnly = true;
            dgvHD.AllowUserToAddRows = false;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có muốn xóa hóa đơn này không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                string mahd = dgvHD.CurrentRow.Cells[0].Value.ToString();
                string maXe = dgvHD.CurrentRow.Cells[1].Value.ToString();
                dgvHD.Rows.RemoveAt(dgvHD.CurrentRow.Index);
            }
            
        }

        
    }
}
