using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DoAn_QuanLyXeMay
{
   public class xlDangKy
    {
       SqlDataProvider provider = new SqlDataProvider();
       //SqlConnection cnn = new SqlConnection("Data Source=DESKTOP-T32VP39\\SQLEXPRESS;Initial Catalog=QLBanXeGanMay;Integrated Security=True");
       DataSet ds = new DataSet();
       SqlDataAdapter da_DK;
       public DataTable LoadTK() //Load bảng tài khoản lên datagridview
       {
           string caulenh = "select * from TaiKhoan";
           da_DK = new SqlDataAdapter(caulenh, provider.Conn);
           da_DK.Fill(ds, "TaiKhoan");
           DataColumn[] kc = new DataColumn[1]; //1 ở đây là 1 trường làm khoá chính: MaKH
           kc[0] = ds.Tables["TaiKhoan"].Columns[0];
           ds.Tables["TaiKhoan"].PrimaryKey = kc;
           return ds.Tables["TaiKhoan"];
       }
       public bool ktKhoaChinh(string Username)
       {
           try
           {
               string[] mang = new string[1];
               mang[0] = Username;
               DataRow dr = ds.Tables["TaiKhoan"].Rows.Find(mang);
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
               SqlCommandBuilder build = new SqlCommandBuilder(da_DK);
               da_DK.Update(ds, "TaiKhoan");
               return true;
           }
           catch
           {
               return false;
           }
       }
       public bool DangKyTK(string Username, string MK, string MaNV, string MaCV)
       {
           try
           {
               DataRow dr = ds.Tables["TaiKhoan"].NewRow();
               dr["Username"] = Username;
               dr["MK"] = MK;
               dr["MaNV"] = MaNV;             
               dr["MaCV"] = MaCV;
               ds.Tables["TaiKhoan"].Rows.Add(dr);
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
       public bool xoaTK(string Username)
       {
           try
           {
               string[] mang = new string[1];
               mang[0] = Username;
               DataRow dr = ds.Tables["TaiKhoan"].Rows.Find(mang);
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
       public bool suaTK(string Username, string Password, string CFPassword, string MaNV, string maCV)
       {
           try
           {

               DataRow dr = ds.Tables["TaiKhoan"].Rows.Find(Username);
               if (dr != null)
               {
                   dr["Username"] = Username;
                   dr["MK"] = Password;
                   dr["MK"] = CFPassword;
                   dr["MaNV"] = MaNV;
                   dr["MaCV"] = maCV;
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
    }
}
