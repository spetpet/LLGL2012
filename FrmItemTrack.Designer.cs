namespace LLGL2012
{
    partial class FrmItemTrack
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

        #region CopyRight
        /*A4EB150879F11D62AFA6*/
        #endregion

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmItemTrack));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSpdm = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chbCk = new System.Windows.Forms.CheckBox();
            this.chbHz = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lvHz = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.label4 = new System.Windows.Forms.Label();
            this.tbKh = new System.Windows.Forms.TextBox();
            this.Add = new System.Windows.Forms.Button();
            this.lvKh = new System.Windows.Forms.ListView();
            this.colhKhdm = new System.Windows.Forms.ColumnHeader();
            this.colhKhmc = new System.Windows.Forms.ColumnHeader();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lvCk = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.chbFwdj = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(26, 26);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(451, 33);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(61, 30);
            this.toolStripButton1.Text = "保存";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(61, 30);
            this.toolStripButton2.Text = "退出";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 347);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(451, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "商品代码";
            // 
            // tbSpdm
            // 
            this.tbSpdm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.tbSpdm.Location = new System.Drawing.Point(69, 42);
            this.tbSpdm.Name = "tbSpdm";
            this.tbSpdm.Size = new System.Drawing.Size(100, 21);
            this.tbSpdm.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "仓库";
            // 
            // chbCk
            // 
            this.chbCk.AutoSize = true;
            this.chbCk.Location = new System.Drawing.Point(12, 328);
            this.chbCk.Name = "chbCk";
            this.chbCk.Size = new System.Drawing.Size(48, 16);
            this.chbCk.TabIndex = 7;
            this.chbCk.Text = "全选";
            this.chbCk.UseVisualStyleBackColor = true;
            this.chbCk.CheckedChanged += new System.EventHandler(this.chbCk_CheckedChanged);
            // 
            // chbHz
            // 
            this.chbHz.AutoSize = true;
            this.chbHz.Location = new System.Drawing.Point(89, 328);
            this.chbHz.Name = "chbHz";
            this.chbHz.Size = new System.Drawing.Size(48, 16);
            this.chbHz.TabIndex = 10;
            this.chbHz.Text = "全选";
            this.chbHz.UseVisualStyleBackColor = true;
            this.chbHz.CheckedChanged += new System.EventHandler(this.chbHz_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(87, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "货主";
            // 
            // lvHz
            // 
            this.lvHz.CheckBoxes = true;
            this.lvHz.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.lvHz.Location = new System.Drawing.Point(89, 90);
            this.lvHz.Name = "lvHz";
            this.lvHz.Size = new System.Drawing.Size(63, 238);
            this.lvHz.TabIndex = 8;
            this.lvHz.UseCompatibleStateImageBehavior = false;
            this.lvHz.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "";
            this.columnHeader2.Width = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(172, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "客户";
            // 
            // tbKh
            // 
            this.tbKh.Location = new System.Drawing.Point(207, 90);
            this.tbKh.Name = "tbKh";
            this.tbKh.Size = new System.Drawing.Size(151, 21);
            this.tbKh.TabIndex = 12;
            this.tbKh.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbKh_KeyDown);
            // 
            // Add
            // 
            this.Add.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.Add.Image = ((System.Drawing.Image)(resources.GetObject("Add.Image")));
            this.Add.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Add.Location = new System.Drawing.Point(364, 88);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(66, 23);
            this.Add.TabIndex = 13;
            this.Add.Text = "添加";
            this.Add.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Add.UseVisualStyleBackColor = false;
            this.Add.Visible = false;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // lvKh
            // 
            this.lvKh.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colhKhdm,
            this.colhKhmc});
            this.lvKh.ContextMenuStrip = this.contextMenuStrip1;
            this.lvKh.FullRowSelect = true;
            this.lvKh.GridLines = true;
            this.lvKh.Location = new System.Drawing.Point(174, 126);
            this.lvKh.Name = "lvKh";
            this.lvKh.Size = new System.Drawing.Size(265, 202);
            this.lvKh.TabIndex = 14;
            this.lvKh.UseCompatibleStateImageBehavior = false;
            this.lvKh.View = System.Windows.Forms.View.Details;
            // 
            // colhKhdm
            // 
            this.colhKhdm.Text = "客户代码";
            this.colhKhdm.Width = 77;
            // 
            // colhKhmc
            // 
            this.colhKhmc.Text = "客户名称";
            this.colhKhmc.Width = 174;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(123, 26);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.删除ToolStripMenuItem.Text = "删除客户";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // lvCk
            // 
            this.lvCk.CheckBoxes = true;
            this.lvCk.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvCk.Location = new System.Drawing.Point(12, 90);
            this.lvCk.Name = "lvCk";
            this.lvCk.Size = new System.Drawing.Size(63, 238);
            this.lvCk.TabIndex = 15;
            this.lvCk.UseCompatibleStateImageBehavior = false;
            this.lvCk.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 47;
            // 
            // chbFwdj
            // 
            this.chbFwdj.AutoSize = true;
            this.chbFwdj.Location = new System.Drawing.Point(174, 45);
            this.chbFwdj.Name = "chbFwdj";
            this.chbFwdj.Size = new System.Drawing.Size(72, 16);
            this.chbFwdj.TabIndex = 16;
            this.chbFwdj.Text = "附温度计";
            this.chbFwdj.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(364, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 17;
            this.label5.Text = "（回车）";
            // 
            // FrmItemTrack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 369);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chbFwdj);
            this.Controls.Add(this.lvCk);
            this.Controls.Add(this.lvKh);
            this.Controls.Add(this.tbKh);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chbHz);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lvHz);
            this.Controls.Add(this.chbCk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbSpdm);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmItemTrack";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "冷链跟踪";
            this.Load += new System.EventHandler(this.FrmItemTrack_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmItemTrack_FormClosed);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSpdm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chbCk;
        private System.Windows.Forms.CheckBox chbHz;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lvHz;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbKh;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.ListView lvKh;
        private System.Windows.Forms.ListView lvCk;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader colhKhdm;
        private System.Windows.Forms.ColumnHeader colhKhmc;
        private System.Windows.Forms.CheckBox chbFwdj;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.Label label5;
    }
}