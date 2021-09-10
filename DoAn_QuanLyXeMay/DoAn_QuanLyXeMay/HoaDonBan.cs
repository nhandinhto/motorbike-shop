using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace DoAn_QuanLyXeMay
{
    public class HoaDonBan
    {
        SqlDataProvider provider = new SqlDataProvider();

        public DataTable loadHDB()
        {
            SqlDataAdapter da_hdb = new SqlDataAdapter("Select * from Hoa_Don_Ban_Xe", provider.Conn);
            da_hdb.Fill(provider.ds_xeganmay, "HoaDonBan");
            DataColumn[] primary_key = new DataColumn[1];
            primary_key[0] = provider.ds_xeganmay.Tables["HoaDonBan"].Columns[0];
            provider.ds_xeganmay.Tables["HoaDonBan"].PrimaryKey = primary_key;
            return provider.ds_xeganmay.Tables["HoaDonBan"];
        }

        public DataTable loadNV()
        {
            SqlDataAdapter da_nv = new SqlDataAdapter("select MaNV,HoTenNV from Nhan_Vien", provider.Conn);
            da_nv.Fill(provider.ds_xeganmay, "NhanVien_Hoadon");
            DataColumn[] primary_key = new DataColumn[1];
            primary_key[0] = provider.ds_xeganmay.Tables["NhanVien_HoaDon"].Columns[0];
            provider.ds_xeganmay.Tables["NhanVien_HoaDon"].PrimaryKey = primary_key;
            return provider.ds_xeganmay.Tables["NhanVien_HoaDon"];
        }

        public DataTable loadCTHD()
        {
            SqlDataAdapter da_cthd = new SqlDataAdapter("Select * from ChiTietHD", provider.Conn);
            da_cthd.Fill(provider.ds_xeganmay, "ChiTietHD");
            DataColumn[] primary_key = new DataColumn[2];
            primary_key[0] = provider.ds_xeganmay.Tables["ChiTietHD"].Columns[0];
            primary_key[1] = provider.ds_xeganmay.Tables["ChiTietHD"].Columns[1];
            provider.ds_xeganmay.Tables["ChiTietHD"].PrimaryKey = primary_key;
            return provider.ds_xeganmay.Tables["ChiTietHD"];
        }

        public DataTable loadKH()
        {
            SqlDataAdapter da_nv = new SqlDataAdapter("select MaKH,HoTenKH,SDT,SoCMND from Khach_Hang", provider.Conn);
            da_nv.Fill(provider.ds_xeganmay, "KH_Hoadon");
            DataColumn[] primary_key = new DataColumn[1];
            primary_key[0] = provider.ds_xeganmay.Tables["KH_Hoadon"].Columns[0];
            provider.ds_xeganmay.Tables["KH_Hoadon"].PrimaryKey = primary_key;
            return provider.ds_xeganmay.Tables["KH_Hoadon"];
        }

        public DataTable loadDetailHD(string maHD)
        {
            string jquery = string.Format("Select ChiTietHD.*,Hoa_Don_Ban_Xe.TongTien,TenXe from ChiTietHD join Hoa_Don_Ban_Xe on ChiTietHD.MaHD = Hoa_Don_Ban_Xe.MaHD join Xe on ChiTietHD.MaXe = Xe.MaXe Where ChiTietHD.MaHD=N'{0}'", maHD);
            SqlDataAdapter da_hoadon_cthd = new SqlDataAdapter(jquery, provider.Conn);
            da_hoadon_cthd.Fill(provider.ds_xeganmay, "Details_HD");
            return provider.ds_xeganmay.Tables["Details_HD"];
        }

        public DataTable loadCTHD(string maHD)
        {
            string query = "Select * from ChiTietHD where MaHD = N'" + maHD + "'"; 
            SqlDataAdapter da_cthd = new SqlDataAdapter(query, provider.Conn);
            da_cthd.Fill(provider.ds_xeganmay, "ChiTietHD_filter");
            DataColumn[] primary_key = new DataColumn[2];
            primary_key[0] = provider.ds_xeganmay.Tables["ChiTietHD_filter"].Columns[0];
            primary_key[1] = provider.ds_xeganmay.Tables["ChiTietHD_filter"].Columns[1];
            provider.ds_xeganmay.Tables["ChiTietHD_filter"].PrimaryKey = primary_key;
            return provider.ds_xeganmay.Tables["ChiTietHD_filter"];
        }

        public bool Save(DataSet data)
        {
            try
            {
                SqlDataAdapter da_hd = new SqlDataAdapter("Select * from Hoa_Don_Ban_Xe", provider.Conn);
                SqlCommandBuilder cmd = new SqlCommandBuilder(da_hd);
                da_hd.Update(data.Tables["HoaDonBan"]);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public DataTable loadHDB(string MaHD)
        {
            String query = string.Format("Select * from Hoa_Don_Ban_Xe where MaHD = N'{0}'", MaHD);
            SqlDataAdapter da_hdb = new SqlDataAdapter(query, provider.Conn);
            da_hdb.Fill(provider.ds_xeganmay, "HoaDonBan_filter");
            DataColumn[] primary_key = new DataColumn[1];
            primary_key[0] = provider.ds_xeganmay.Tables["HoaDonBan_filter"].Columns[0];
            provider.ds_xeganmay.Tables["HoaDonBan_filter"].PrimaryKey = primary_key;
            return provider.ds_xeganmay.Tables["HoaDonBan_filter"];
        }

        public bool kiemTraMaHD(string maHD)
        {
            DataTable data = provider.ds_xeganmay.Tables["HoaDonBan"];
            DataRow dr = data.Rows.Find(maHD);
            if( dr != null)
                return true;
            return false;
        }

        public bool themHD(string maHD, string maNV, string maKH, string ngay)
        {
            try{

                if (kiemTraMaHD(maHD))
                {
                    return false;
                }
                else
                {
                    SqlDataAdapter da_hd = new SqlDataAdapter("Select * from Hoa_Don_Ban_Xe", provider.Conn);
                    da_hd.Fill(provider.ds_xeganmay, "Them_HoaDon");
                    DataRow dr = provider.ds_xeganmay.Tables["Them_HoaDon"].NewRow();
                    dr["MaHD"] = maHD;
                    dr["MaKH"] = maKH;
                    dr["MaNV"] = maNV;
                    dr["NgayLap"] = DateTime.Parse(ngay);
                    dr["TongTien"] = 0;
                    provider.ds_xeganmay.Tables["Them_HoaDon"].Rows.Add(dr);
                    SqlCommandBuilder build = new SqlCommandBuilder(da_hd);
                    da_hd.Update(provider.ds_xeganmay, "Them_HoaDon");
                    return true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public bool suaHD(string maHD,string maNV, string maKH, string ngay)
        {
            try
            {
                    SqlDataAdapter da_hd = new SqlDataAdapter("Select * from Hoa_Don_Ban_Xe", provider.Conn);
                    da_hd.Fill(provider.ds_xeganmay, "Sua_HoaDon");
                    DataColumn[] primary_key = new DataColumn[1];
                    primary_key[0] = provider.ds_xeganmay.Tables["Sua_HoaDon"].Columns[0];
                    provider.ds_xeganmay.Tables["Sua_HoaDon"].PrimaryKey = primary_key;
                    DataRow dr = provider.ds_xeganmay.Tables["Sua_HoaDon"].Rows.Find(maHD);
                    dr["MaKH"] = maKH;
                    dr["MaNV"] = maNV;
                    dr["NgayLap"] = DateTime.Parse(ngay);
                    SqlCommandBuilder build = new SqlCommandBuilder(da_hd);
                    da_hd.Update(provider.ds_xeganmay, "Sua_HoaDon");
                    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public bool themCTHD(string maHD,string maXe,int soluong,double price,double money )
        {
            try
            {
                SqlDataAdapter data_cthd = new SqlDataAdapter("Select * from ChiTietHD", provider.Conn);
                data_cthd.Fill(provider.ds_xeganmay, "Them_CTHD");
                DataRow dr = provider.ds_xeganmay.Tables["Them_CTHD"].NewRow();
                dr["MaHD"] = maHD;
                dr["MaXe"] = maXe;
                dr["soluong"] = soluong;
                dr["donGia"] = price;
                dr["thanhTien"] = money;
                provider.ds_xeganmay.Tables["Them_CTHD"].Rows.Add(dr);
                SqlCommandBuilder cmd = new SqlCommandBuilder(data_cthd);
                data_cthd.Update(provider.ds_xeganmay, "Them_CTHD");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool themCTHD2(string maHD, string maXe, int soluong, double price, double money)
        {
            try
            {
                string query = string.Format("Select * from ChiTietHD where MaHD = N'{0}' and MaXe = N'{1}'", maHD, maXe);
                SqlDataAdapter data_cthd = new SqlDataAdapter(query, provider.Conn);
                data_cthd.Fill(provider.ds_xeganmay, "Update_CTHD");
                DataRow dr = provider.ds_xeganmay.Tables["Update_CTHD"].Rows[0];
                dr["soluong"] = soluong;
                dr["donGia"] = price;
                dr["thanhTien"] = money;
                SqlCommandBuilder cmd = new SqlCommandBuilder(data_cthd);
                data_cthd.Update(provider.ds_xeganmay, "Update_CTHD");
                provider.ds_xeganmay.Tables["Update_CTHD"].Clear();
                return true;
            }
            catch (Exception ex)
            {
          
                return false;
            }
        }

        public bool updateCTHD(string maHD, string maXe, int soluong, double price, double money)
        {
            try
            {
                string query = string.Format("Select * from ChiTietHD where MaHD = N'{0}' and MaXe = N'{1}'", maHD, maXe);
                SqlDataAdapter data_cthd = new SqlDataAdapter(query, provider.Conn);
                data_cthd.Fill(provider.ds_xeganmay, "Update_CTHD");
                //DataColumn[] primary_key = new DataColumn[2];
                //primary_key[0] = provider.ds_xeganmay.Tables["Update_CTHD"].Columns[0];
                //primary_key[1] = provider.ds_xeganmay.Tables["Update_CTHD"].Columns[1];                //provider.ds_xeganmay.Tables["Update_CTHD"].PrimaryKey = primary_key;

                DataRow dr = provider.ds_xeganmay.Tables["Update_CTHD"].Rows[0];
                dr["soluong"] = soluong;
                dr["donGia"] = price;
                dr["thanhTien"] = money;
                SqlCommandBuilder cmd = new SqlCommandBuilder(data_cthd);
                data_cthd.Update(provider.ds_xeganmay,"Update_CTHD");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public bool deleteCTHD(string maCTHD, string maXe)
        {
            try
            {
                SqlDataAdapter data_cthd = new SqlDataAdapter("Select * from ChiTietHD", provider.Conn);
                data_cthd.Fill(provider.ds_xeganmay, "Delete_CTHD");
                DataColumn[] primary_key = new DataColumn[2];
                primary_key[0] = provider.ds_xeganmay.Tables["Delete_CTHD"].Columns[0];
                primary_key[1] = provider.ds_xeganmay.Tables["Delete_CTHD"].Columns[1];
                provider.ds_xeganmay.Tables["Delete_CTHD"].PrimaryKey = primary_key;

                string[] keys = { maCTHD, maXe };

                DataRow dr_Delete = provider.ds_xeganmay.Tables["Delete_CTHD"].Rows.Find(keys);
                if (dr_Delete != null)
                {
                    dr_Delete.Delete();
                }
                SqlCommandBuilder cmd = new SqlCommandBuilder(data_cthd);
                data_cthd.Update(provider.ds_xeganmay, "Delete_CTHD");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
