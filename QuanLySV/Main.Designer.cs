namespace QuanLySV
{
    partial class Main
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.quanLySinhVienToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quanLyLopHocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnl_main = new System.Windows.Forms.Panel();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quanLySinhVienToolStripMenuItem,
            this.quanLyLopHocToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1000, 27);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // quanLySinhVienToolStripMenuItem
            // 
            this.quanLySinhVienToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.quanLySinhVienToolStripMenuItem.Name = "quanLySinhVienToolStripMenuItem";
            this.quanLySinhVienToolStripMenuItem.Size = new System.Drawing.Size(127, 23);
            this.quanLySinhVienToolStripMenuItem.Text = "Quản lý sinh viên";
            this.quanLySinhVienToolStripMenuItem.Click += new System.EventHandler(this.quanLySinhVienToolStripMenuItem_Click);
            // 
            // quanLyLopHocToolStripMenuItem
            // 
            this.quanLyLopHocToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.quanLyLopHocToolStripMenuItem.Name = "quanLyLopHocToolStripMenuItem";
            this.quanLyLopHocToolStripMenuItem.Size = new System.Drawing.Size(118, 23);
            this.quanLyLopHocToolStripMenuItem.Text = "Quản lý lớp học";
            this.quanLyLopHocToolStripMenuItem.Click += new System.EventHandler(this.quanLyLopHocToolStripMenuItem_Click);
            // 
            // pnl_main
            // 
            this.pnl_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_main.Location = new System.Drawing.Point(0, 27);
            this.pnl_main.Name = "pnl_main";
            this.pnl_main.Size = new System.Drawing.Size(1000, 653);
            this.pnl_main.TabIndex = 1;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.toolStripMenuItem1.ForeColor = System.Drawing.Color.Red;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(84, 23);
            this.toolStripMenuItem1.Text = "Đăng xuất";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 680);
            this.Controls.Add(this.pnl_main);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem quanLySinhVienToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quanLyLopHocToolStripMenuItem;
        private System.Windows.Forms.Panel pnl_main;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}
