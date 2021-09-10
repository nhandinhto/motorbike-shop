using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DoAn_QuanLyXeMay
{
    public class NhaCungCap
    {
        SqlDataProvider provider = new SqlDataProvider();
        SqlDataAdapter da_NCC = new SqlDataAdapter();
        DataSet ds = new DataSet();

        public DataTable loadNCC()
        {
            da_NCC = new SqlDataAdapter("Select * from Nha_Cung_Cap", provider.Conn);
            da_NCC.Fill(provider.ds_xeganmay, "NhaCC");
            DataColumn[] primary_key = new DataColumn[1];
            primary_key[0] = provider.ds_xeganmay.Tables["NhaCC"].Columns[0];
            provider.ds_xeganmay.Tables["NhaCC"].PrimaryKey = primary_key;
            return provider.ds_xeganmay.Tables["NhaCC"];
        }
        public DataRow findNCC(string mncc)
        {
            return provider.ds_xeganmay.Tables["NhaCC"].Rows.Find(mncc);
        }
        public DataTable loadNhaCC()
        {
            string caulenh = "select * from Nha_Cung_Cap"; // câu truy vấn để load dữ liệu bảng NCC 
            da_NCC = new SqlDataAdapter(caulenh, provider.Conn);
            da_NCC.Fill(ds, "Nha_Cung_Cap");
            DataColumn[] kc = new DataColumn[1]; //ở đây 1 là do bảng có 1 khoá chính
            kc[0] = ds.Tables["Nha_Cung_Cap"].Columns[0];
            ds.Tables["Nha_Cung_Cap"].PrimaryKey = kc;
            return ds.Tables["Nha_Cung_Cap"];
        }
        public bool ktKhoaChinh(string MaNCC)
        {
            try
            {
                string[] mang = new string[1];
                mang[0] = MaNCC;
                DataRow dr = ds.Tables["Nha_Cung_Cap"].Rows.Find(mang);
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
                SqlCommandBuilder build = new SqlCommandBuilder(da_NCC);
                da_NCC.Update(ds, "Nha_Cung_Cap");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool themNCC(string MaNCC, string TenNCC, string DiaChi, string SDT, string Gmail)
        {
            try
            {
                DataRow dr = ds.Tables["Nha_Cung_Cap"].NewRow();
                dr["MaNCC"] = MaNCC;
                dr["TenNCC"] = TenNCC;
                dr["DiaChi"] = DiaChi;
                dr["SDT"] = SDT;
                dr["Gmail"] = Gmail;
                ds.Tables["Nha_Cung_Cap"].Rows.Add(dr);
                if (luuVaoCSDL())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool xoaNCC(string MaNCC)
        {
            try
            {
                DataRow dr = ds.Tables["Nha_Cung_Cap"].Rows.Find(MaNCC);
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
        public bool suaNCC(string MaNCC, string TenNCC, string DiaChi, string SDT, string Gmail)
        {
            try
            {

                DataRow dr = ds.Tables["Nha_Cung_Cap"].Rows.Find(MaNCC);
                if (dr != null)
                {
                    dr["MaNCC"] = MaNCC;
                    dr["TenNCC"] = TenNCC;
                    dr["DiaChi"] = DiaChi;
                    dr["SDT"] = SDT;
                    dr["Gmail"] = Gmail;
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
            string cauLenh = "select Nha_Cung_Cap.* from Nha_Cung_Cap where (TenNCC Like N'%" + info + "%' or TenNCC Like N'%" + info + "%' or DiaChi like '%" + info + "%')";
            SqlDataAdapter da_TimNCC = new SqlDataAdapter(cauLenh, provider.Conn);
            da_TimNCC.Fill(ds, "NCCTK");
            DataColumn[] kc = new DataColumn[1];
            kc[0] = ds.Tables["NCCTK"].Columns[0];
            ds.Tables["NCCTK"].PrimaryKey = kc;
            return ds.Tables["NCCTK"];
        }
    }
}
