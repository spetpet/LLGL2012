namespace LLGL2012
{
    partial class FrmEditIce
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEditIce));
            this.Add = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.tbSl = new System.Windows.Forms.TextBox();
            this.tbGg = new System.Windows.Forms.TextBox();
            this.tbPm = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbBplx = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbBpssgs = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // Add
            // 
            this.Add.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.Add.Image = ((System.Drawing.Image)(resources.GetObject("Add.Image")));
            this.Add.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Add.Location = new System.Drawing.Point(603, 83);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(66, 23);
            this.Add.TabIndex = 35;
            this.Add.Text = "添加";
            this.Add.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Add.UseVisualStyleBackColor = false;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 112);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(657, 259);
            this.dataGridView1.TabIndex = 34;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(246, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 23);
            this.button1.TabIndex = 33;
            this.button1.Text = "查询库存";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbSl
            // 
            this.tbSl.Location = new System.Drawing.Point(407, 83);
            this.tbSl.Name = "tbSl";
            this.tbSl.Size = new System.Drawing.Size(73, 21);
            this.tbSl.TabIndex = 32;
            this.tbSl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSl_KeyPress);
            // 
            // tbGg
            // 
            this.tbGg.BackColor = System.Drawing.SystemColors.Control;
            this.tbGg.Enabled = false;
            this.tbGg.Location = new System.Drawing.Point(407, 47);
            this.tbGg.Name = "tbGg";
            this.tbGg.Size = new System.Drawing.Size(262, 21);
            this.tbGg.TabIndex = 31;
            // 
            // tbPm
            // 
            this.tbPm.BackColor = System.Drawing.SystemColors.Control;
            this.tbPm.Enabled = false;
            this.tbPm.Location = new System.Drawing.Point(407, 12);
            this.tbPm.Name = "tbPm";
            this.tbPm.Size = new System.Drawing.Size(262, 21);
            this.tbPm.TabIndex = 30;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(355, 83);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 29;
            this.label9.Text = "数量";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(353, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 28;
            this.label8.Text = "规格";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(353, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 27;
            this.label7.Text = "品名";
            // 
            // cbBplx
            // 
            this.cbBplx.FormattingEnabled = true;
            this.cbBplx.Items.AddRange(new object[] {
            "",
            "硬冰",
            "软冰"});
            this.cbBplx.Location = new System.Drawing.Point(90, 48);
            this.cbBplx.Name = "cbBplx";
            this.cbBplx.Size = new System.Drawing.Size(73, 20);
            this.cbBplx.TabIndex = 26;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 25;
            this.label6.Text = "冰排类型";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 24;
            this.label5.Text = "冰排所属公司";
            // 
            // cbBpssgs
            // 
            this.cbBpssgs.FormattingEnabled = true;
            this.cbBpssgs.Location = new System.Drawing.Point(90, 12);
            this.cbBpssgs.Name = "cbBpssgs";
            this.cbBpssgs.Size = new System.Drawing.Size(240, 20);
            this.cbBpssgs.TabIndex = 23;
            // 
            // FrmEditIce
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 383);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbSl);
            this.Controls.Add(this.tbGg);
            this.Controls.Add(this.tbPm);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbBplx);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbBpssgs);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEditIce";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "编辑冰排";
            this.Load += new System.EventHandler(this.FrmEditIce_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmEditIce_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbSl;
        private System.Windows.Forms.TextBox tbGg;
        private System.Windows.Forms.TextBox tbPm;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbBplx;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbBpssgs;
    }
}