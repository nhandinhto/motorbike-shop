namespace DoAn_QuanLyXeMay
{
    partial class frmNhanVien
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNhanVien));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ChucNang = new System.Windows.Forms.ToolStripMenuItem();
            this.logOut = new System.Windows.Forms.ToolStripMenuItem();
            this.Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.product = new System.Windows.Forms.ToolStripButton();
            this.showCustom = new System.Windows.Forms.ToolStripButton();
            this.Sell = new System.Windows.Forms.ToolStripButton();
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
            this.menuStrip1.TabIndex = 2;
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
            this.showCustom,
            this.Sell});
            this.toolStrip1.Location = new System.Drawing.Point(0, 33);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(778, 32);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // product
            // 
            this.product.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.product.Image = ((System.Drawing.Image)(resources.GetObject("product.Image")));
            this.product.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.product.Name = "product";
            this.product.Size = new System.Drawing.Size(136, 29);
            this.product.Text = "Xem Sản Phẩm";
            this.product.Click += new System.EventHandler(this.product_Click);
            // 
            // showCustom
            // 
            this.showCustom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.showCustom.Image = ((System.Drawing.Image)(resources.GetObject("showCustom.Image")));
            this.showCustom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showCustom.Name = "showCustom";
            this.showCustom.Size = new System.Drawing.Size(111, 29);
            this.showCustom.Text = "Khách Hàng";
            this.showCustom.Click += new System.EventHandler(this.showCustom_Click);
            // 
            // Sell
            // 
            this.Sell.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Sell.Image = ((System.Drawing.Image)(resources.GetObject("Sell.Image")));
            this.Sell.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Sell.Name = "Sell";
            this.Sell.Size = new System.Drawing.Size(93, 29);
            this.Sell.Text = "Bán Hàng";
            this.Sell.Click += new System.EventHandler(this.Sell_Click);
            // 
            // frmNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 544);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "frmNhanVien";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmNhanVien";
            this.Load += new System.EventHandler(this.frmNhanVien_Load);
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
        private System.Windows.Forms.ToolStripButton Sell;
        private System.Windows.Forms.ToolStripButton showCustom;
    }
}