using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace DoAn_QuanLyXeMay
{
    public class TaiKhoan
    {
        SqlDataProvider provider = new SqlDataProvider();
        public DataTable loadTK()
        {
            SqlDataAdapter da_tk = new SqlDataAdapter("Select * from TaiKhoan", provider.Conn);
            da_tk.Fill(provider.ds_xeganmay, "TaiKhoan");
            DataColumn[] primarykey = new DataColumn[1];
            primarykey[0] = provider.ds_xeganmay.Tables["TaiKhoan"].Columns[0];
            provider.ds_xeganmay.Tables["TaiKhoan"].PrimaryKey = primarykey;
            return provider.ds_xeganmay.Tables["TaiKhoan"];
        }

        public DataTable loadLoai()
        {
            SqlDataAdapter da_tk = new SqlDataAdapter("Select LoaiTK.* from LoaiTK", provider.Conn);
            da_tk.Fill(provider.ds_xeganmay,"Loai");
            DataColumn[] primarykey = new DataColumn[1];
            primarykey[0] = provider.ds_xeganmay.Tables["Loai"].Columns[0];
            provider.ds_xeganmay.Tables["Loai"].PrimaryKey = primarykey;
            return provider.ds_xeganmay.Tables["Loai"];
        }

        public DataTable loadTK_Filter(string mcv)
        {
            SqlDataAdapter da_filter = new SqlDataAdapter(string.Format("Select * from TaiKhoan where MaCV = N'{0}'", mcv), provider.Conn);
            da_filter.Fill(provider.ds_xeganmay, "TaiKhoan_LoaiTK");
            DataColumn[] primary_key = new DataColumn[1];
            primary_key[0] = provider.ds_xeganmay.Tables["TaiKhoan_LoaiTK"].Columns[0];
            provider.ds_xeganmay.Tables["TaiKhoan_LoaiTK"].PrimaryKey = primary_key;
            return provider.ds_xeganmay.Tables["TaiKhoan_LoaiTK"];
        }

    }
}
