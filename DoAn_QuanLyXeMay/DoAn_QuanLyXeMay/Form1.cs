using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn_QuanLyXeMay
{
    public partial class Form1 : Form
    {
        XuLy xl = new XuLy();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            xl.loadDataGridview_SP(dataGridView1);
        }
    }
}
