using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DoAn_QuanLyXeMay
{
    public class SanPham
    {
        SqlDataProvider provider = new SqlDataProvider();

        SqlDataAdapter da_sanpham;

        public DataTable loadSanPham()
        {
            da_sanpham = new SqlDataAdapter("Select * from Xe", provider.Conn);
            da_sanpham.Fill(provider.ds_xeganmay, "SanPham");
            DataColumn[] primaryKey = new DataColumn[1];
            primaryKey[0] = provider.ds_xeganmay.Tables["SanPham"].Columns[0];
            provider.ds_xeganmay.Tables["SanPham"].PrimaryKey = primaryKey;
            return provider.ds_xeganmay.Tables["SanPham"];
        }

        public DataTable loadSanPham_filter(string mancc)
        {
            da_sanpham = new SqlDataAdapter(string.Format("Select * from Xe Where MaNCC = N'{0}'",mancc), provider.Conn);
            da_sanpham.Fill(provider.ds_xeganmay, "SanPham_NCC");
            DataColumn[] primaryKey = new DataColumn[1];
            primaryKey[0] = provider.ds_xeganmay.Tables["SanPham_NCC"].Columns[0];
            provider.ds_xeganmay.Tables["SanPham_NCC"].PrimaryKey = primaryKey;
            return provider.ds_xeganmay.Tables["SanPham_NCC"];
        }

        public bool kiemTraTrung(string maXe)
        {
            DataRow dr_find = provider.ds_xeganmay.Tables["SanPham"].Rows.Find(maXe);
            if (dr_find != null)
                return true;
            return false;
        }

        public bool Them(string maXe, string mncc, string sk, string sm, string tenxe, string mausac, string gia, string hinh,int soluong)
        {
            try
            {
                da_sanpham = new SqlDataAdapter("Select * from Xe", provider.Conn);
                DataRow dr_add = provider.ds_xeganmay.Tables["SanPham"].NewRow();
                dr_add["MaXe"] = maXe;
                dr_add["MaNCC"] = mncc;
                dr_add["SoKhung"] = sk;
                dr_add["SoMay"] = sm;
                dr_add["TenXe"] = tenxe;
                dr_add["MauSac"] = mausac;
                dr_add["Gia"] = gia;
                dr_add["hinh"] = hinh;
                dr_add["soluong"] = soluong;
                provider.ds_xeganmay.Tables["SanPham"].Rows.Add(dr_add);
                SqlCommandBuilder cmb = new SqlCommandBuilder(da_sanpham);
                da_sanpham.Update(provider.ds_xeganmay, "SanPham");
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool Sua(DataTable data,string maXe,string mncc, string sk, string sm, string tenxe, string mausac, string gia, string hinh,int soluong)
        {
            try
            {
                da_sanpham = new SqlDataAdapter("Select * from Xe", provider.Conn);
                DataRow dr_find = data.Rows.Find(maXe);
                dr_find["MaNCC"] = mncc;
                dr_find["SoKhung"] = sk;
                dr_find["SoMay"] = sm;
                dr_find["TenXe"] = tenxe;
                dr_find["MauSac"] = mausac;
                dr_find["Gia"] = gia;
                dr_find["hinh"] = hinh;
                dr_find["soluong"] = soluong;
                SqlCommandBuilder cmd = new SqlCommandBuilder(da_sanpham);
                da_sanpham.Update(provider.ds_xeganmay,"SanPham");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public DataTable loadBaoHanh()
        {
            SqlDataAdapter da_baohanh = new SqlDataAdapter("Select * from Bao_Hanh", provider.Conn);
            da_baohanh.Fill(provider.ds_xeganmay, "BaoHanh");
            DataColumn[] primarykey = new DataColumn[2];
            primarykey[0] = provider.ds_xeganmay.Tables["BaoHanh"].Columns[0];
            primarykey[1] = provider.ds_xeganmay.Tables["BaoHanh"].Columns[1];
            provider.ds_xeganmay.Tables["BaoHanh"].PrimaryKey = primarykey;
            return provider.ds_xeganmay.Tables["BaoHanh"];
        }

        public bool kiemTraBaoHanh(string maxe)
        {
            SqlDataAdapter da_baohanh_xe = new SqlDataAdapter("Select MaBH,MaXe from Bao_Hanh Where MaXe = N'" + maxe + "'", provider.Conn);
            da_baohanh_xe.Fill(provider.ds_xeganmay, "BaoHanh_Xe");
            if(provider.ds_xeganmay.Tables["BaoHanh_Xe"].Rows.Count>0)
                return true;
            return false;
        }

        public bool Xoa(DataTable data,string maxe)
        {
            try
            {
                da_sanpham = new SqlDataAdapter("Select * from Xe", provider.Conn);
                DataRow dr_del = data.Rows.Find(maxe);
                dr_del.Delete();
                SqlCommandBuilder cmd = new SqlCommandBuilder(da_sanpham);
                da_sanpham.Update(provider.ds_xeganmay, "SanPham");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public DataTable search(string info)
        {
            string squery = string.Format("Select Xe.* from Xe join Nha_Cung_Cap on Xe.MaNCC = Nha_Cung_Cap.MaNCC Where MaXe = '{0}' OR TenNCC Like N'%{0}%' or TenXe Like N'%{0}%'", info);
            SqlDataAdapter da_find_sp = new SqlDataAdapter(squery, provider.Conn);
            DataTable data_search = new DataTable();
            da_find_sp.Fill(data_search);
            DataColumn[] primaryKey = new DataColumn[1];
            primaryKey[0] = data_search.Columns[0];
            data_search.PrimaryKey = primaryKey;
            return data_search;
        }

    }
}
