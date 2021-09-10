using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace DoAn_QuanLyXeMay
{
    public class NhanVien
    {
        SqlDataProvider provider = new SqlDataProvider();
        DataSet ds = new DataSet();
        SqlDataAdapter da_NV;
        /// <summary>
        /// Phương thức khởi tạo khi khởi tạo đối tượgn nhân viên đồng thời load 
        /// dữ liệu nhân viên từ cơ sở dữ liệu lên 
        /// </summary>
        public NhanVien()
        {
            SqlDataAdapter da_nhanvien = new SqlDataAdapter("Select * from TaiKhoan", provider.Conn);
            da_nhanvien.Fill(provider.ds_xeganmay, "TK");
            DataColumn[] primary_key = new DataColumn[1];
            primary_key[0] = provider.ds_xeganmay.Tables["TK"].Columns[0];
            provider.ds_xeganmay.Tables["TK"].PrimaryKey = primary_key;
        }

        public DataTable loadNV()
        {
            SqlDataAdapter da_nhanvien = new SqlDataAdapter("Select * from Nhan_Vien", provider.Conn);
            da_nhanvien.Fill(provider.ds_xeganmay, "NhanVien");
            DataColumn[] primary_key = new DataColumn[1];
            primary_key[0] = provider.ds_xeganmay.Tables["NhanVien"].Columns[0];
            provider.ds_xeganmay.Tables["NhanVien"].PrimaryKey = primary_key;
            return provider.ds_xeganmay.Tables["NhanVien"];
        }

        public bool login(string id,string pw,string cv)
        {
            try
            {
                NhanVien a = new NhanVien();
                DataTable data_login = new DataTable();
                SqlDataAdapter data = new SqlDataAdapter("select TaiKhoan.*,TenCV from TaiKhoan left join LoaiTK on TaiKhoan.MaCV = LoaiTK.MaCV", provider.Conn);
                data.Fill(data_login);
                DataColumn[] primarykey = new DataColumn[1];
                primarykey[0] = data_login.Columns[0];
                data_login.PrimaryKey = primarykey;
                DataRow data_find = data_login.NewRow();
                data_find = data_login.Rows.Find(id);
                if (data_find.Table.Rows.Count <=0)
                {
                    return false;
                }
                else
                {
                    if (data_find[1].ToString().Trim().Equals(pw))
                    {
                        if(data_find[4].ToString().Trim().Equals(cv))
                            return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }


        public DataTable LoadNV() //Load bảng khách hàng lên datagridview
        {
            string caulenh = "select * from Nhan_Vien";
            da_NV = new SqlDataAdapter(caulenh, provider.Conn);
            da_NV.Fill(ds, "Nhan_Vien");
            DataColumn[] kc = new DataColumn[1]; //1 ở đây là 1 trường làm khoá chính: MaKH
            kc[0] = ds.Tables["Nhan_Vien"].Columns[0];
            ds.Tables["Nhan_Vien"].PrimaryKey = kc;
            return ds.Tables["Nhan_Vien"];
        }
        public bool ktKhoaChinh(string MaNV)
        {
            try
            {
                string[] mang = new string[1];
                mang[0] = MaNV;
                DataRow dr = ds.Tables["Nhan_Vien"].Rows.Find(mang);
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
                SqlCommandBuilder build = new SqlCommandBuilder(da_NV);
                da_NV.Update(ds, "Nhan_Vien");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool themKH(string MaNV, string HoTenNV, string GioiTinh, string SoCMND, string DiaChi, string Luong)
        {
            try
            {
                DataRow dr = ds.Tables["Nhan_Vien"].NewRow();
                dr["MaNV"] = MaNV;
                dr["HoTenNV"] = HoTenNV;
                dr["GioiTinh"] = GioiTinh;
                dr["SoCMND"] = SoCMND;
                dr["DiaChi"] = DiaChi;
                dr["luong"] = Luong;
                ds.Tables["Nhan_Vien"].Rows.Add(dr);
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
        public bool xoaNV(string MaNV)
        {
            try
            {
                string[] mang = new string[1];
                mang[0] = MaNV;
                DataRow dr = ds.Tables["Nhan_Vien"].Rows.Find(mang);
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
        public bool suaKH(string MaNV, string HoTenNV, string GioiTinh, string SoCMND, string DiaChi, string Luong)
        {
            try
            {

                DataRow dr = ds.Tables["Nhan_Vien"].Rows.Find(MaNV);
                if (dr != null)
                {
                    dr["MaNV"] = MaNV;
                    dr["HoTenNV"] = HoTenNV;
                    dr["GioiTinh"] = GioiTinh;
                    dr["SoCMND"] = SoCMND;
                    dr["DiaChi"] = DiaChi;
                    dr["luong"] = Luong;
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
            string cauLenh = "select Nhan_Vien.* from Nhan_Vien where (MaNV Like N'%" + info + "%' or HoTenNV Like N'%" + info + "%' or DiaChi like N'%" + info + "%')";
            SqlDataAdapter da_TimKH = new SqlDataAdapter(cauLenh, provider.Conn);
            da_TimKH.Fill(ds, "NVTK");
            DataColumn[] kc = new DataColumn[1];
            kc[0] = ds.Tables["NVTK"].Columns[0];
            ds.Tables["NVTK"].PrimaryKey = kc;
            return ds.Tables["NVTK"];
        }

    }
}
