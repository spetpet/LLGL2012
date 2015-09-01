namespace LLGL2012
{
    partial class FrmEditTherm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEditTherm));
            this.label4 = new System.Windows.Forms.Label();
            this.tbWdj = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbDxwdj = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.therm_type_textbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "温度计";
            // 
            // tbWdj
            // 
            this.tbWdj.Location = new System.Drawing.Point(83, 89);
            this.tbWdj.Name = "tbWdj";
            this.tbWdj.ReadOnly = true;
            this.tbWdj.Size = new System.Drawing.Size(158, 21);
            this.tbWdj.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "待选温度计";
            // 
            // cbDxwdj
            // 
            this.cbDxwdj.FormattingEnabled = true;
            this.cbDxwdj.Location = new System.Drawing.Point(83, 50);
            this.cbDxwdj.Name = "cbDxwdj";
            this.cbDxwdj.Size = new System.Drawing.Size(158, 20);
            this.cbDxwdj.TabIndex = 10;
            this.cbDxwdj.SelectedIndexChanged += new System.EventHandler(this.cbDxwdj_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(83, 147);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(58, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "保存";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(152, 147);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(61, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "取消";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "温度计类型";
            // 
            // therm_type_textbox
            // 
            this.therm_type_textbox.Location = new System.Drawing.Point(83, 12);
            this.therm_type_textbox.Name = "therm_type_textbox";
            this.therm_type_textbox.ReadOnly = true;
            this.therm_type_textbox.Size = new System.Drawing.Size(158, 21);
            this.therm_type_textbox.TabIndex = 16;
            // 
            // FrmEditTherm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 203);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.therm_type_textbox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbWdj);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbDxwdj);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEditTherm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "编辑温度计";
            this.Load += new System.EventHandler(this.FrmEditTherm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmEditTherm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbWdj;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbDxwdj;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox therm_type_textbox;
    }
}