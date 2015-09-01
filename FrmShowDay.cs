using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LLGL2012
{
    public partial class FrmShowDay : Form
    {
        private static FrmShowDay pUniqueForm = null;//窗体唯一打开代码

        /// <summary>
        /// 窗体唯一打开代码
        /// </summary>
        /// <param name="Path">是否作为模式窗体打开</param>
        public static void ShowUniqueForm(bool AIfShowModal)
        {
            if (pUniqueForm == null)
            {
                new FrmShowDay();

                if (AIfShowModal) pUniqueForm.ShowDialog(); else pUniqueForm.Show();
            }
        }

        public FrmShowDay()
        {
            pUniqueForm = this;//窗体唯一打开代码
            InitializeComponent();
        }

        private void FrmShowDay_FormClosed(object sender, FormClosedEventArgs e)
        {
            pUniqueForm = null;//窗体唯一打开代码
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string AppFile = Application.StartupPath + "\\";
            CommFunction.WritePrivateProfileString("显示天数设置", "天数",textBox1.Text.Trim(),AppFile+"ShowDay.ini");
            CommFunction.WritePrivateProfileString("打印设置", "是否打印", checkBox1.Checked.ToString(), AppFile + "ShowDay.ini");
            CommFunction.WritePrivateProfileString("默认药品温度设置", "药品温度", textBox2.Text.Trim(), AppFile + "ShowDay.ini");
            Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //只允许输整数   件装数自动带出 不允修改 此行无用       
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8)
                e.Handled = true;
        }

        private void FrmShowDay_Load(object sender, EventArgs e)
        {
            string AppFile = Application.StartupPath + "\\";
            StringBuilder temp = new StringBuilder(255);
            CommFunction.GetPrivateProfileString("显示天数设置", "天数", "", temp, 255, AppFile + "ShowDay.ini");
            textBox1.Text = temp.ToString();
            CommFunction.GetPrivateProfileString("默认药品温度设置", "药品温度", "", temp, 255, AppFile + "ShowDay.ini");
            textBox2.Text = temp.ToString();
            CommFunction.GetPrivateProfileString("打印设置", "是否打印", "", temp, 255, AppFile + "ShowDay.ini");
            string print = temp.ToString();
            if (print == "") { print = "FALSE"; }
            checkBox1.Checked = Boolean.Parse(print.ToString());
            if ((textBox1.Text.Trim() == "") || (textBox1.Text == null)) { textBox1.Text = "3"; }
            if ((textBox2.Text.Trim() == "") || (textBox2.Text == null)) { textBox2.Text = "0"; }
        }
    }
}
