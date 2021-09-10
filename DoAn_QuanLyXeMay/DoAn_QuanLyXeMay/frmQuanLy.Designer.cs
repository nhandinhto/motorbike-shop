namespace DoAn_QuanLyXeMay
{
    partial class frmQuanLy
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuanLy));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ChucNang = new System.Windows.Forms.ToolStripMenuItem();
            this.logOut = new System.Windows.Forms.ToolStripMenuItem();
            this.Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.product = new System.Windows.Forms.ToolStripButton();
            this.account = new System.Windows.Forms.ToolStripButton();
            this.Staff = new System.Windows.Forms.ToolStripButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.NCC = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ChucNang});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(778, 33);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ChucNang
            // 
            this.ChucNang.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logOut,
            this.Exit});
            this.ChucNang.Name = "ChucNang";
            this.ChucNang.Size = new System.Drawing.Size(69, 29);
            this.ChucNang.Text = "Menu";
            // 
            // logOut
            // 
            this.logOut.Name = "logOut";
            this.logOut.Size = new System.Drawing.Size(168, 30);
            this.logOut.Text = "Đăng Xuất";
            this.logOut.Click += new System.EventHandler(this.logOut_Click);
            // 
            // Exit
            // 
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(168, 30);
            this.Exit.Text = "Thoát";
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.product,
            this.account,
            this.Staff,
            this.NCC});
            this.toolStrip1.Location = new System.Drawing.Point(0, 33);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(778, 32);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // product
            // 
            this.product.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.product.Image = ((System.Drawing.Image)(resources.GetObject("product.Image")));
            this.product.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.product.Name = "product";
            this.product.Size = new System.Drawing.Size(160, 29);
            this.product.Text = "Quản lý sản phẩm";
            this.product.Click += new System.EventHandler(this.product_Click);
            // 
            // account
            // 
            this.account.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.account.Image = ((System.Drawing.Image)(resources.GetObject("account.Image")));
            this.account.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.account.Name = "account";
            this.account.Size = new System.Drawing.Size(155, 29);
            this.account.Text = "Quản lý tài khoản";
            this.account.Click += new System.EventHandler(this.account_Click);
            // 
            // Staff
            // 
            this.Staff.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Staff.Image = ((System.Drawing.Image)(resources.GetObject("Staff.Image")));
            this.Staff.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Staff.Name = "Staff";
            this.Staff.Size = new System.Drawing.Size(166, 29);
            this.Staff.Text = "Quản Lý Nhân Viên";
            this.Staff.Click += new System.EventHandler(this.Staff_Click);
            // 
            // NCC
            // 
            this.NCC.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.NCC.Image = ((System.Drawing.Image)(resources.GetObject("NCC.Image")));
            this.NCC.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NCC.Name = "NCC";
            this.NCC.Size = new System.Drawing.Size(200, 29);
            this.NCC.Text = "Quản Lý Nhà Cung Cấp";
            this.NCC.Click += new System.EventHandler(this.NCC_Click);
            // 
            // frmQuanLy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 544);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "frmQuanLy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmQuanLy";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ChucNang;
        private System.Windows.Forms.ToolStripMenuItem logOut;
        private System.Windows.Forms.ToolStripMenuItem Exit;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton product;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripButton account;
        private System.Windows.Forms.ToolStripButton Staff;
        private System.Windows.Forms.ToolStripButton NCC;
    }
}