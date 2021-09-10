using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;  
namespace DoAn_QuanLyXeMay
{
    public class SqlDataProvider
    {
        //SqlConnection conn = new SqlConnection("Data Source=DESKTOP-8O63SGA\\SQLEXPRESS;Initial Catalog=QLBanXeGanMay;User ID=sa;Password=sa123");
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-8O63SGA\\SQLEXPRESS;Initial Catalog=QLBanXeGanMay;User ID=sa;Password=sa123");
        public SqlConnection Conn
        {
            get { return conn; }
        }

        public DataSet ds_xeganmay = new DataSet();
        
    }
}
