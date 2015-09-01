namespace LLGL2012
{
    partial class FrmRGBack
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRGBack));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.button4 = new System.Windows.Forms.Button();
            this.tbGzbd = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbWdbz = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.serverfilepath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.localfilepath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tbZlbz = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbZt = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFhrq = new System.Windows.Forms.DateTimePicker();
            this.cbWdj = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpFhsj = new System.Windows.Forms.DateTimePicker();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(26, 26);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(538, 33);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(62, 30);
            this.toolStripButton1.Text = "退出";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 350);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(538, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // button4
            // 
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(251, 31);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(94, 28);
            this.button4.TabIndex = 109;
            this.button4.Text = "查询温度计";
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // tbGzbd
            // 
            this.tbGzbd.Location = new System.Drawing.Point(97, 36);
            this.tbGzbd.Name = "tbGzbd";
            this.tbGzbd.Size = new System.Drawing.Size(148, 21);
            this.tbGzbd.TabIndex = 108;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(26, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 107;
            this.label8.Text = "跟踪表单号";
            // 
            // tbWdbz
            // 
            this.tbWdbz.Location = new System.Drawing.Point(97, 196);
            this.tbWdbz.Name = "tbWdbz";
            this.tbWdbz.Size = new System.Drawing.Size(366, 21);
            this.tbWdbz.TabIndex = 105;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 199);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 106;
            this.label7.Text = "温度计温度备注";
            // 
            // serverfilepath
            // 
            this.serverfilepath.Location = new System.Drawing.Point(112, 272);
            this.serverfilepath.Name = "serverfilepath";
            this.serverfilepath.Size = new System.Drawing.Size(352, 21);
            this.serverfilepath.TabIndex = 104;
            this.serverfilepath.Text = "\\\\173.5.28.153\\质量管理部\\温度计返还记录文件";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 272);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 103;
            this.label6.Text = "服务器文件路径";
            // 
            // button3
            // 
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(470, 229);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(55, 33);
            this.button3.TabIndex = 102;
            this.button3.Text = "浏览";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // localfilepath
            // 
            this.localfilepath.Location = new System.Drawing.Point(112, 236);
            this.localfilepath.Name = "localfilepath";
            this.localfilepath.Size = new System.Drawing.Size(352, 21);
            this.localfilepath.TabIndex = 101;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 239);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 100;
            this.label3.Text = "本地文件路径";
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(303, 301);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 39);
            this.button2.TabIndex = 99;
            this.button2.Text = "撤销确认";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(97, 301);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 39);
            this.button1.TabIndex = 98;
            this.button1.Text = "返回确认";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbZlbz
            // 
            this.tbZlbz.Location = new System.Drawing.Point(97, 169);
            this.tbZlbz.Name = "tbZlbz";
            this.tbZlbz.Size = new System.Drawing.Size(366, 21);
            this.tbZlbz.TabIndex = 96;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 97;
            this.label2.Text = "温度计包装备注";
            // 
            // cbZt
            // 
            this.cbZt.FormattingEnabled = true;
            this.cbZt.Items.AddRange(new object[] {
            "正常",
            "损坏"});
            this.cbZt.Location = new System.Drawing.Point(97, 134);
            this.cbZt.Name = "cbZt";
            this.cbZt.Size = new System.Drawing.Size(67, 20);
            this.cbZt.TabIndex = 95;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 94;
            this.label1.Text = "温度计质量状态";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(286, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 92;
            this.label5.Text = "返回时间";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 89;
            this.label4.Text = "返回日期";
            // 
            // dtpFhrq
            // 
            this.dtpFhrq.Location = new System.Drawing.Point(97, 98);
            this.dtpFhrq.Name = "dtpFhrq";
            this.dtpFhrq.Size = new System.Drawing.Size(148, 21);
            this.dtpFhrq.TabIndex = 88;
            // 
            // cbWdj
            // 
            this.cbWdj.FormattingEnabled = true;
            this.cbWdj.Location = new System.Drawing.Point(97, 66);
            this.cbWdj.Name = "cbWdj";
            this.cbWdj.Size = new System.Drawing.Size(148, 20);
            this.cbWdj.TabIndex = 87;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(26, 69);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 86;
            this.label12.Text = "温度计编号";
            // 
            // dtpFhsj
            // 
            this.dtpFhsj.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpFhsj.Location = new System.Drawing.Point(355, 101);
            this.dtpFhsj.Name = "dtpFhsj";
            this.dtpFhsj.ShowUpDown = true;
            this.dtpFhsj.Size = new System.Drawing.Size(108, 21);
            this.dtpFhsj.TabIndex = 110;
            // 
            // FrmRGBack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 372);
            this.Controls.Add(this.dtpFhsj);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.tbGzbd);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbWdbz);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.serverfilepath);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.localfilepath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbZlbz);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbZt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpFhrq);
            this.Controls.Add(this.cbWdj);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRGBack";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "温度计返还";
            this.Load += new System.EventHandler(this.FrmRGBack_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmRGBack_FormClosed);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox tbGzbd;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbWdbz;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox serverfilepath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox localfilepath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbZlbz;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbZt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFhrq;
        private System.Windows.Forms.ComboBox cbWdj;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtpFhsj;
    }
}