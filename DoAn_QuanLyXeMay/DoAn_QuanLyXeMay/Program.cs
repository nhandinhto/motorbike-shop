using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn_QuanLyXeMay
{
    static class Program
    {
        public static Login formLogin =null;
        public static frmNhanVien formNV = null;
        public static frmQuanLy formQL = null;
        public static Xe frmXe = null;
        public static frmHoaDonBan hoaDonBan = null;
        public static DangKy formDK = null;
        public static FrKhachHang formKH = null;
        public static FrNhaCC formNCC = null;
        public static FrNhanVien formNV_QL = null;
        public static FrSanPham_NV_ formSP = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            formLogin = new Login();
            Application.Run(formLogin);
        }
    }
}
