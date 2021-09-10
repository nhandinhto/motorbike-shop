using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace DoAn_QuanLyXeMay
{
    public class XuLy
    {
        public SqlDataProvider provider = new SqlDataProvider();
        public SanPham sp = new SanPham();
        public NhaCungCap ncc = new NhaCungCap();
        public TaiKhoan tk = new TaiKhoan();
        public HoaDonBan hd = new HoaDonBan();
        public DataSet ds_QuanLyXeMay;
        public NhanVien nv = new NhanVien();
        

        public void loadDataGridview_SP(DataGridView dgv)
        {
            DataTable data = sp.loadSanPham();
            dgv.DataSource = data;
        }

        public void loadComboBox_NCC(ComboBox cb)
        {
            DataTable data = ncc.loadNCC();
            DataRow dr = data.NewRow();
            dr["MaNCC"] = "all";
            dr["TenNCC"] = "Tất cả nhà cung cấp";
            data.Rows.InsertAt(dr, 0);
            cb.DataSource = data;
            cb.DisplayMember = "TenNCC";
            cb.ValueMember = "MaNCC";
        }

        public void loadDataGridview_TK(DataGridView dgv)
        {
            DataTable data = tk.loadTK();
            dgv.DataSource = data;
        }

        public void loadComboBox_LTK(ComboBox cb)
        {
            DataTable data = tk.loadLoai();
            DataRow dr = data.NewRow();
            dr["MaCV"] = "all";
            dr["TenCV"] = "Tất cả";
            data.Rows.InsertAt(dr, 0);
            cb.DataSource = data;
            cb.DisplayMember = "TenCV";
            cb.ValueMember = "MaCV";

        }

        public void loadDataGridview_CTHD(DataGridView dgv)
        {
            DataTable data = hd.loadCTHD();
            dgv.DataSource = data;
        }

        public void loadComBoBox_Mnv_hd(ComboBox cb)
        {
            DataTable data = hd.loadNV();
            DataRow dr = data.NewRow();
            dr["MaNV"] = "all";
            dr["HoTenNV"] = "Tất cả nhân viện";
            data.Rows.InsertAt(dr, 0);
            cb.DataSource = data;
            cb.DisplayMember = "HoTenNV";
            cb.ValueMember = "MaNV";
        }

        public void loadComBoBox_HD(ComboBox cb)
        {
            DataTable data = hd.loadHDB();
            DataRow dr = data.NewRow();
            dr["MaHD"] = "Danh Sách";
            data.Rows.InsertAt(dr, 0);
            cb.DataSource = data;
            cb.DisplayMember = "MaHD";
            cb.ValueMember = "MaHD";
        }

        public void loadComBoBox_KH_HD(ComboBox cb)
        {
            DataTable data = hd.loadKH();
            DataRow dr = data.NewRow();
            dr["MaKH"] = "all";
            dr["HoTenKH"] = "Tất cả khách hàng";
            data.Rows.InsertAt(dr, 0);
            cb.DataSource = data;
            cb.DisplayMember = "HoTenKH";
            cb.ValueMember = "MaKH";
        }

     

        public void loadComBoBox_HD_Xe(ComboBox cb)
        {
            DataTable data = sp.loadSanPham();
            cb.DataSource = data;
            cb.DisplayMember = "TenXe";
            cb.ValueMember = "MaXe";
        }

        public bool save_hd()
        {
            if(hd.Save(ds_QuanLyXeMay))
            {
                return true;
            }
            return false;
        }

        public string ComboBox_SelectedChanged(ComboBox cb,string mcv)
        {
            DataTable data = cb.DataSource as DataTable;
            DataRow dr_find = data.Rows.Find(mcv);
            if (dr_find != null)
            {
                return dr_find["TenCV"].ToString();
            }
            return null;
        }

        public DataRow findNhaCC(string mncc)
        {
            return ncc.findNCC(mncc);
        }

        public void loadDataGridview_SP_NCC(DataGridView dgv,string mncc)
        {
            DataTable data_ncc = sp.loadSanPham_filter(mncc);
            if (data_ncc == null)
                return;
            dgv.DataSource = data_ncc;

        }

        public bool Them(string maXe, string mncc, string sk, string sm, string tenxe, string mausac, string gia, string hinh,int soluong)
        {
            if (sp.Them(maXe, mncc, sk, sm, tenxe, mausac, gia, hinh,soluong))
                return true;
            return false;
        }

        public bool Sua(DataTable data,string maXe, string mncc, string sk, string sm, string tenxe, string mausac, string gia, string hinh, int soluong)
        {
            if (sp.Sua(data,maXe, mncc, sk, sm, tenxe, mausac, gia, hinh, soluong))
                return true;
            return false;
        }
       
        public bool Xoa(string maxe)
        {
            if (sp.kiemTraBaoHanh(maxe))
            {
                MessageBox.Show("Mã xe đang trong thời gian bảo hành");
                return false;
            }
            else
            {
                if (sp.Xoa(sp.loadSanPham(), maxe))
                    return true;
                return false;
            }
        }

        public DataTable SanPham_Search(string info)
        {
            return sp.search(info);
        }

        public bool ThemHD(string maHD, string maNV, string maKH, string date)
        {
            try
            {
                if (hd.themHD(maHD, maNV, maKH, date))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool EditHD(string maHD, string maNV, string maKH, string date)
        {
            try
            {
                if (hd.suaHD(maHD, maNV, maKH, date))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ThemCTHD(string maHD, string maXe, int soluong, double price, double money)
        {
            try
            {
                if (hd.themCTHD(maHD, maXe, soluong, price, money))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ThemCTHD2(string maHD, string maXe, int soluong, double price, double money)
        {
            try
            {
                if (hd.themCTHD2(maHD, maXe, soluong, price, money))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateCTHD(string maHD, string maXe, int soluong, double price, double money)
        {
            try
            {
                if (hd.updateCTHD(maHD, maXe, soluong, price, money))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteCTHD(string maCTHD, string maXe)
        {
            try
            {
                if (hd.deleteCTHD(maCTHD, maXe))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
