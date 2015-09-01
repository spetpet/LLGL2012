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
    public partial class FrmEditVendor : Form
    {
        private static FrmEditVendor pUniqueForm = null;//窗体唯一打开代码
        public static string falg = "Edit";
        public static Boolean ifSave = false;
        public static string pHzkhdm = "";
        public static string pUnid = "";
        public static string pCk = "";
        public static string pHz = "";
        public static string pTh = "";
        public static string pFwdj = "";
        public static string ResultHzkhdm = "'";

        /// <summary>
        /// 窗体唯一打开代码
        /// </summary>
        /// <param name="Path">是否作为模式窗体打开</param>
        public static void ShowUniqueForm(bool AIfShowModal)
        {
            if (pUniqueForm == null)
            {
                new FrmEditVendor();

                if (AIfShowModal) pUniqueForm.ShowDialog(); else pUniqueForm.Show();
            }
        }

        public FrmEditVendor()
        {
            pUniqueForm = this;//窗体唯一打开代码
            InitializeComponent();
        }

        private void FrmEditVendor_FormClosed(object sender, FormClosedEventArgs e)
        {
            pUniqueForm = null;//窗体唯一打开代码
        }

        private void FrmEditVendor_Load(object sender, EventArgs e)
        {
            ifSave = false;
            string strCk = "select WHSE from V_WHSE ";
            string strHz = "select COMPANY from v_company ";
            string strTh = "select distinct t.ysfs therm_type from ysfs t where t.type='温度计字典'";

            OleDbDataAdapter sda = new OleDbDataAdapter(strCk, CommFunction.ConnectString);
            DataTable CKTable = new DataTable();
            sda.Fill(CKTable);
            for (int i = 0; i < CKTable.Rows.Count; i++)
            {
                lvCk.Items.Add(CKTable.Rows[i][0].ToString());
            }

            OleDbDataAdapter sda2 = new OleDbDataAdapter(strHz, CommFunction.ConnectString);
            DataTable HzTable = new DataTable();
            sda2.Fill(HzTable);
            for (int i = 0; i < HzTable.Rows.Count; i++)
            {
                lvHz.Items.Add(HzTable.Rows[i][0].ToString());
            }

            OleDbDataAdapter sda3 = new OleDbDataAdapter(strTh, CommFunction.ConnectString);
            DataTable ThTable = new DataTable();
            sda3.Fill(ThTable);
            for (int i = 0; i < ThTable.Rows.Count; i++)
            {
                therm_combo.Items.Add(ThTable.Rows[i][0].ToString());
            }

            Clean_Frm();
            Load_Frm();
        }

        private void Clean_Frm()
        {
            tbHzKhdm.Text = "";
            therm_combo.Text = "";
            chbCk.Checked = false;
            chbHz.Checked = false;
            chbFwdj.Checked = false;

        }

        private void Load_Frm()
        {
            tbHzKhdm.Text = pHzkhdm;

            if (falg == "Edit")
            {
                therm_combo.Text = pTh;
                if (pFwdj == "附温度计") { chbFwdj.Checked = true; therm_combo.Enabled = true; } else { chbFwdj.Checked = false; therm_combo.Enabled = false; }
                string[] sArray = pCk.Split(',');
                if (sArray.Length > 0)
                {
                    for (int i = 0; i < sArray.Length; i++)
                    {
                        for (int j = 0; j < lvCk.Items.Count; j++)
                        {
                            if (sArray[i].ToString() == lvCk.Items[j].Text.ToString()) { lvCk.Items[j].Checked = true; }
                        }
                    }
                }
                string[] sArray1 = pHz.Split(',');
                if (sArray1.Length > 0)
                {
                    for (int i = 0; i < sArray1.Length; i++)
                    {
                        for (int j = 0; j < lvHz.Items.Count; j++)
                        {
                            if (sArray1[i].ToString() == lvHz.Items[j].Text.ToString()) { lvHz.Items[j].Checked = true; }
                        }
                    }
                }

            }

        }

        private void chbCk_CheckedChanged(object sender, EventArgs e)
        {
            Boolean ch = chbCk.Checked;
            for (int i = 0; i < lvCk.Items.Count; i++)
            {
                lvCk.Items[i].Checked = ch;
            }
        }

        private void chbHz_CheckedChanged(object sender, EventArgs e)
        {
            Boolean ch = chbHz.Checked;
            for (int i = 0; i < lvHz.Items.Count; i++)
            {
                lvHz.Items[i].Checked = ch;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (tbHzKhdm.Text.Trim() == "") { MessageBox.Show("货主客户代码 不允许为空！"); return; }
            string strCk = "";
            string strHz = "";
            string strFwdj = "";
            string strTh = "";

            for (int i = 0; i < lvCk.Items.Count; i++)
            {
                if (lvCk.Items[i].Checked) strCk = strCk + lvCk.Items[i].Text + ",";
            }

            for (int i = 0; i < lvHz.Items.Count; i++)
            {
                if (lvHz.Items[i].Checked) strHz = strHz + lvHz.Items[i].Text + ",";
            }


            if (chbFwdj.Checked) 
            { 
                strFwdj = "附温度计";
                if (therm_combo.Text==""){ MessageBox.Show("选择附温度计后必须选择温度计类型！"); return; }
                strTh = therm_combo.Text;

            } 
            else 
            {
                strFwdj = ""; strTh = "";
            }


            if (falg == "Add")
            {
                string SqlIns = "insert into cc_vendor_track(misc_instr_code_2,whse,season,ifthermometer,create_date_time,user_name,therm_type)"
                                + "values('" + tbHzKhdm.Text.Trim() + "','" + strCk.Trim() + "','"
                                + strHz.Trim() + "','"  + strFwdj.Trim() + "',sysdate,'"
                                + LoginCs.LoginClass.LogName +"','"+strTh.Trim()+ "')";
                LYFunctionCs.LYFunctionCsClass.cmd(SqlIns, CommFunction.ConnectString);
            }
            else
            {
                string SqlUpdate = "update cc_vendor_track set whse='" + strCk.Trim() + "'," + "season='" + strHz.Trim() +
                                    "',ifthermometer='" + strFwdj.Trim() + "',user_name='" + LoginCs.LoginClass.LogName +"',therm_type='"+strTh.Trim()+ "' " +
                                   "where unid='" + pUnid + "'";
                LYFunctionCs.LYFunctionCsClass.cmd(SqlUpdate, CommFunction.ConnectString);
            }

            ResultHzkhdm = ResultHzkhdm + "','" + tbHzKhdm.Text.Trim();

            ifSave = true;

            MessageBox.Show("完成。");

            if (falg == "Add") { Clean_Frm(); } else { Close(); }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void chbFwdj_CheckedChanged(object sender, EventArgs e)
        {
            if (chbFwdj.Checked)
                therm_combo.Enabled = true;
            else
                therm_combo.Enabled = false;
        }




    }
}
