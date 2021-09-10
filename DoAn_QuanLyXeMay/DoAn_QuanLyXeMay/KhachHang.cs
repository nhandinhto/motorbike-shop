using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DoAn_QuanLyXeMay
{
    class KhachHang
    {
        SqlDataProvider provider = new SqlDataProvider();

        DataSet ds = new DataSet();
        SqlDataAdapter da_KH;
        public DataTable LoadKH() //Load bảng khách hàng lên datagridview
        {
            string caulenh = "select * from Khach_Hang";
            da_KH = new SqlDataAdapter(caulenh, provider.Conn);
            da_KH.Fill(ds, "Khach_Hang");
            DataColumn[] kc = new DataColumn[1]; //1 ở đây là 1 trường làm khoá chính: MaKH
            kc[0] = ds.Tables["Khach_Hang"].Columns[0];
            ds.Tables["Khach_Hang"].PrimaryKey = kc;
            return ds.Tables["Khach_Hang"];
        }
        public bool ktKhoaChinh(string MaKH)
        {
            try
            {
                string[] mang = new string[1];
                mang[0] = MaKH;
                DataRow dr = ds.Tables["Khach_Hang"].Rows.Find(mang);
                if (dr != null)
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool luuVaoCSDL()
        {
            try
            {
                SqlCommandBuilder build = new SqlCommandBuilder(da_KH);
                da_KH.Update(ds, "Khach_Hang");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool themKH(string MaKH, string HoTenKH, string GioiTinh, string DiaChi, string SDT,string SoCMND)
        {
            try
            {
                DataRow dr = ds.Tables["Khach_Hang"].NewRow();
                dr["MaKH"] = MaKH;
                dr["HoTenKH"] = HoTenKH;
                dr["GioiTinh"] = GioiTinh;
                dr["DiaChi"] = DiaChi;
                dr["SDT"] = SDT;
                dr["SoCMND"] = SoCMND;
                ds.Tables["Khach_Hang"].Rows.Add(dr);
                if (luuVaoCSDL())
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool xoaKH(string MaKH)
        {
            try
            {
                string[] mang = new string[1];
                mang[0] = MaKH;              
                DataRow dr = ds.Tables["Khach_Hang"].Rows.Find(mang);
                if (dr != null)
                {
                    dr.Delete();
                }
                if (luuVaoCSDL())
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool suaKH(string MaKH, string HoTenKH, string GioiTinh, string DiaChi, string SDT, string SoCMND)
        {
            try
            {
                
                DataRow dr = ds.Tables["Khach_Hang"].Rows.Find(MaKH);
                if (dr != null)
                {
                    dr["MaKH"] = MaKH;
                    dr["HoTenKH"] = HoTenKH;
                    dr["GioiTinh"] = GioiTinh;
                    dr["DiaChi"] = DiaChi;
                    dr["SDT"] = SDT;
                    dr["SoCMND"] = SoCMND;
                }
                if (luuVaoCSDL())
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public DataTable TimKiem(string info)
        {
            //string caulenh = string.Format("Select Xe.* from Xe join Nha_Cung_Cap on Xe.MaNCC = Nha_Cung_Cap.MaNCC Where MaXe = '{0}' OR TenNCC Like N'%{0}%' or TenXe = N'{0}'", info);
            string cauLenh = "select Khach_Hang.* from Khach_Hang where (MaKH Like N'%" + info + "%' or HoTenKH Like N'%" + info + "%' or DiaChi like N'%" + info + "%')";
            SqlDataAdapter da_TimKH = new SqlDataAdapter(cauLenh, provider.Conn);
            da_TimKH.Fill(ds, "KHTK");
            DataColumn[] kc = new DataColumn[1];
            kc[0] = ds.Tables["KHTK"].Columns[0];
            ds.Tables["KHTK"].PrimaryKey = kc;
            return ds.Tables["KHTK"];
        }
    }
    
}
