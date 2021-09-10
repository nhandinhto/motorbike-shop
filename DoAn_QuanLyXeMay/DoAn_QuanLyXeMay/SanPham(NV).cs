using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DoAn_QuanLyXeMay
{
    public class SanPham_NV_
    {
        SqlDataProvider provider = new SqlDataProvider();
        //SqlConnection cnn = new SqlConnection();
        DataSet ds = new DataSet();
        SqlDataAdapter da_Xe;
        public DataTable LoadXe() //Load bảng khách hàng lên datagridview
        {
            string caulenh = "select * from Xe";
            da_Xe = new SqlDataAdapter(caulenh, provider.Conn);
            da_Xe.Fill(ds, "Xe");
            DataColumn[] kc = new DataColumn[1]; //1 ở đây là 1 trường làm khoá chính: MaXe
            kc[0] = ds.Tables["Xe"].Columns[0];
            ds.Tables["Xe"].PrimaryKey = kc;
            return ds.Tables["Xe"];
        }
        public DataTable TimKiem(string info) 
        {
            //string caulenh = string.Format("Select Xe.* from Xe join Nha_Cung_Cap on Xe.MaNCC = Nha_Cung_Cap.MaNCC Where MaXe = '{0}' OR TenNCC Like N'%{0}%' or TenXe = N'{0}'", info);
            string cauLenh = "select Xe.* from Xe, Nha_Cung_Cap where Xe.MaNCC = Nha_Cung_Cap.MaNCC and (TenXe Like N'%" + info + "%' or TenNCC Like N'%" + info + "%' or Gia like '%" + info + "%')";
            SqlDataAdapter da_TimXe = new SqlDataAdapter(cauLenh, provider.Conn);
            da_TimXe.Fill(ds,"XeTK");
            DataColumn[] kc = new DataColumn[1];
            kc[0] = ds.Tables["XeTK"].Columns[0];
            ds.Tables["XeTK"].PrimaryKey = kc;
            return ds.Tables["XeTK"];
        }
        public bool ktKhoaChinh(string MaXe)
        {
            try
            {
                string[] mang = new string[1];
                mang[0] = MaXe;
                DataRow dr = ds.Tables["Xe"].Rows.Find(mang);
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
                SqlCommandBuilder build = new SqlCommandBuilder(da_Xe);
                da_Xe.Update(ds, "Xe");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
