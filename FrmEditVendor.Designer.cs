namespace LLGL2012
{
    partial class FrmEditVendor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEditVendor));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.lvCk = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.label3 = new System.Windows.Forms.Label();
            this.lvHz = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.chbCk = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.chbHz = new System.Windows.Forms.CheckBox();
            this.chbFwdj = new System.Windows.Forms.CheckBox();
            this.tbHzKhdm = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.therm_combo = new System.Windows.Forms.ComboBox();
            this.toolStrip1.SuspendLayout();
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
            this.toolStrip1.Size = new System.Drawing.Size(338, 33);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(62, 30);
            this.toolStripButton1.Text = "保存";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(62, 30);
            this.toolStripButton2.Text = "退出";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // lvCk
            // 
            this.lvCk.CheckBoxes = true;
            this.lvCk.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvCk.Location = new System.Drawing.Point(36, 128);
            this.lvCk.Name = "lvCk";
            this.lvCk.Size = new System.Drawing.Size(89, 238);
            this.lvCk.TabIndex = 21;
            this.lvCk.UseCompatibleStateImageBehavior = false;
            this.lvCk.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 47;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(155, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "货主";
            // 
            // lvHz
            // 
            this.lvHz.CheckBoxes = true;
            this.lvHz.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.lvHz.Location = new System.Drawing.Point(157, 128);
            this.lvHz.Name = "lvHz";
            this.lvHz.Size = new System.Drawing.Size(94, 238);
            this.lvHz.TabIndex = 19;
            this.lvHz.UseCompatibleStateImageBehavior = false;
            this.lvHz.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "";
            this.columnHeader2.Width = 40;
            // 
            // chbCk
            // 
            this.chbCk.AutoSize = true;
            this.chbCk.Location = new System.Drawing.Point(36, 366);
            this.chbCk.Name = "chbCk";
            this.chbCk.Size = new System.Drawing.Size(48, 16);
            this.chbCk.TabIndex = 18;
            this.chbCk.Text = "全选";
            this.chbCk.UseVisualStyleBackColor = true;
            this.chbCk.CheckedChanged += new System.EventHandler(this.chbCk_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "仓库";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 399);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(338, 22);
            this.statusStrip1.TabIndex = 16;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // chbHz
            // 
            this.chbHz.AutoSize = true;
            this.chbHz.Location = new System.Drawing.Point(157, 366);
            this.chbHz.Name = "chbHz";
            this.chbHz.Size = new System.Drawing.Size(48, 16);
            this.chbHz.TabIndex = 22;
            this.chbHz.Text = "全选";
            this.chbHz.UseVisualStyleBackColor = true;
            this.chbHz.CheckedChanged += new System.EventHandler(this.chbHz_CheckedChanged);
            // 
            // chbFwdj
            // 
            this.chbFwdj.AutoSize = true;
            this.chbFwdj.Location = new System.Drawing.Point(18, 70);
            this.chbFwdj.Name = "chbFwdj";
            this.chbFwdj.Size = new System.Drawing.Size(72, 16);
            this.chbFwdj.TabIndex = 25;
            this.chbFwdj.Text = "附温度计";
            this.chbFwdj.UseVisualStyleBackColor = true;
            this.chbFwdj.CheckedChanged += new System.EventHandler(this.chbFwdj_CheckedChanged);
            // 
            // tbHzKhdm
            // 
            this.tbHzKhdm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.tbHzKhdm.Location = new System.Drawing.Point(97, 36);
            this.tbHzKhdm.Name = "tbHzKhdm";
            this.tbHzKhdm.Size = new System.Drawing.Size(100, 21);
            this.tbHzKhdm.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "货主客户代码";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(107, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 26;
            this.label4.Text = "选择温度计类型";
            // 
            // therm_combo
            // 
            this.therm_combo.Enabled = false;
            this.therm_combo.FormattingEnabled = true;
            this.therm_combo.Location = new System.Drawing.Point(202, 69);
            this.therm_combo.Name = "therm_combo";
            this.therm_combo.Size = new System.Drawing.Size(121, 20);
            this.therm_combo.TabIndex = 27;
            // 
            // FrmEditVendor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 421);
            this.Controls.Add(this.therm_combo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chbFwdj);
            this.Controls.Add(this.tbHzKhdm);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chbHz);
            this.Controls.Add(this.lvCk);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lvHz);
            this.Controls.Add(this.chbCk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEditVendor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "冷链客户";
            this.Load += new System.EventHandler(this.FrmEditVendor_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmEditVendor_FormClosed);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ListView lvCk;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lvHz;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.CheckBox chbCk;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.CheckBox chbHz;
        private System.Windows.Forms.CheckBox chbFwdj;
        private System.Windows.Forms.TextBox tbHzKhdm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox therm_combo;
    }
}