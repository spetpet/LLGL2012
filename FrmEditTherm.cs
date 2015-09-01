using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LLGL2012
{
    public partial class FrmEditTherm : Form
    {
        private static FrmEditTherm pUniqueForm = null;//窗体唯一打开代码
        public static string pWdjgs = "";
        public static string pTherm = "";
        public static string pIfsave = "";
        public static string pVendor_nbr = "";

        /// <summary>
        /// 窗体唯一打开代码
        /// </summary>
        /// <param name="Path">是否作为模式窗体打开</param>
        public static void ShowUniqueForm(bool AIfShowModal)
        {
            if (pUniqueForm == null)
            {
                new FrmEditTherm();

                if (AIfShowModal) pUniqueForm.ShowDialog(); else pUniqueForm.Show();
            }
        }

        
        public FrmEditTherm()
        {
            pUniqueForm = this;//窗体唯一打开代码
            InitializeComponent();
        }

        private void FrmEditTherm_FormClosed(object sender, FormClosedEventArgs e)
        {
            pUniqueForm = null;//窗体唯一打开代码
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmEditTherm_Load(object sender, EventArgs e)
        {
            string strrg = "";
            string strTherm_type_sql = "select ct.therm_type from cc_vendor_track ct left join vendor_master vm on vm.misc_instr_code_2=ct.misc_instr_code_2 where vm.vendor_id='"+pVendor_nbr+"'";
            OleDbConnection con1 = new OleDbConnection(CommFunction.ConnectString);
            OleDbDataAdapter sda1 = new OleDbDataAdapter(strTherm_type_sql, con1);
            DataSet ThermTable = new DataSet();
            sda1.Fill(ThermTable);
            if (ThermTable.Tables[0].Rows.Count > 0) 
            { 
                therm_type_textbox.Text = ThermTable.Tables[0].Rows[0][0].ToString();
                strrg = "select ysid from ysfs where type='温度计字典' and ysfs='"+therm_type_textbox.Text+"' and RESERVE2='正常' and RESERVE1='在库' and RESERVE3='" + pWdjgs + "' and nvl(flag,'!@#$%')!='停用'";
            } 
            else 
            { 
                therm_type_textbox.Text = "无限制";
                strrg = "select ysid from ysfs where type='温度计字典' and RESERVE2='正常' and RESERVE1='在库' and RESERVE3='" + pWdjgs + "' and nvl(flag,'!@#$%')!='停用'";
            }
            con1.Close();
            con1.Dispose();
            OleDbConnection con2 = new OleDbConnection(CommFunction.ConnectString);
            OleDbDataAdapter sda2 = new OleDbDataAdapter(strrg, con2);
            DataSet rTable = new DataSet();
            sda2.Fill(rTable);
            cbDxwdj.Items.Clear();
            cbDxwdj.Items.Add("");
            for (int i = 0; i < rTable.Tables[0].Rows.Count; i++)
            {
                cbDxwdj.Items.Add(rTable.Tables[0].Rows[i][0]);
            }
            con2.Close();
            con2.Dispose();
        }

        private void cbDxwdj_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbWdj.Text = cbDxwdj.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbWdj.Text == "") { MessageBox.Show("请先选择需要添加的温度计的编号！"); return; }
            pTherm = tbWdj.Text;
            pIfsave = "True";
            Close();
        }
    }
}
