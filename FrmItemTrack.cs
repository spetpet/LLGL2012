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
    public partial class FrmItemTrack : Form
    {
        private static FrmItemTrack pUniqueForm = null;//窗体唯一打开代码

        public static string ResultSpdm = "'";

        public static string falg = "Edit";
        public static Boolean ifSave = false;

        public static string pUnid = "";
        public static string pSpdm = "";
        public static string pCk = "";
        public static string pHz = "";
        public static string pKhdm = "";
        public static string pFwdj = "";

        /// <summary>
        /// 窗体唯一打开代码
        /// </summary>
        /// <param name="Path">是否作为模式窗体打开</param>
        public static void ShowUniqueForm(bool AIfShowModal)
        {
            if (pUniqueForm == null)
            {
                new FrmItemTrack();

                if (AIfShowModal) pUniqueForm.ShowDialog(); else pUniqueForm.Show();
            }
        }


        public FrmItemTrack()
        {
            pUniqueForm = this;//窗体唯一打开代码
            InitializeComponent();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmItemTrack_FormClosed(object sender, FormClosedEventArgs e)
        {
            pUniqueForm = null;//窗体唯一打开代码
        }

        private void FrmItemTrack_Load(object sender, EventArgs e)
        {
            ifSave = false;
            string strCk = "select WHSE from V_WHSE ";
            string strHz = "select COMPANY from v_company ";

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

            Clean_Frm();
            Load_Frm();

        }

        private void Clean_Frm()
        {
            tbSpdm.Text = "";
            tbKh.Text = "";
            chbCk.Checked = false;
            chbHz.Checked = false;
            lvKh.Items.Clear();
            chbFwdj.Checked = false;
 
        }

        private void Load_Frm()
        {
            tbSpdm.Text = pSpdm;

            if (falg == "Edit")
            {
                if (pFwdj == "附温度计") { chbFwdj.Checked = true; } else { chbFwdj.Checked = false; }
                string[] sArray = pCk.Split( ',');
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
                if (pKhdm.Trim() != "")
                {
                    string[] sArray2 = pKhdm.Split(',');
                    if (sArray2.Length > 0)
                    {
                        for (int i = 0; i < sArray2.Length; i++)
                        {
                            string sql = "select 客户名称 from V_VENDOR_MASTER where MISC_INSTR_CODE_2='" + sArray2[i].ToString() + "'";
                            string sKhmc = LYFunctionCs.LYFunctionCsClass.cmdScalar(sql, CommFunction.ConnectString);
                            ListViewItem lvi = new ListViewItem(sArray2[i].ToString());
                            lvi.SubItems.Add(sKhmc);
                            lvKh.Items.Add(lvi);
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

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvKh.SelectedItems.Count==0) return;
            lvKh.Items.Remove(lvKh.SelectedItems[0]);  
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (tbKh.Text.Trim() == "") { MessageBox.Show("请输入 客户 ！"); return; }

            string OutValue = "";
            int showx, showy;
            showx = Left + tbKh.Left;
            showy = Top + tbKh.Top + tbKh.Height;
            if (CommFunction.ShowGetCodeForm(this.Handle, CommFunction.ConnectString,
                       "select MISC_INSTR_CODE_2 as 货主客户代码,客户名称 from V_VENDOR_MASTER group by MISC_INSTR_CODE_2,客户名称",
                       "MISC_INSTR_CODE_2,客户名称", tbKh.Text, "代码", 0,
                       showx, showy, true,
                      false, true,
                      true, ref OutValue) && (OutValue != ""))
            {
                    string[] sArray = OutValue.Split('|');
                    for (int i = 0; i < lvKh.Items.Count; i++)
                    { if (lvKh.Items[i].SubItems[0].Text.ToString() == sArray[0].ToString()) return; }
                    ListViewItem lvi = new ListViewItem(sArray[0].ToString());
                    lvi.SubItems.Add(sArray[1].ToString());
                    lvKh.Items.Add(lvi);
                    
            }

            tbKh.Focus();
            tbKh.SelectAll();
        }

        private void tbKh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue != 13) return;
            Add_Click(null,null);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (tbSpdm.Text.Trim() == "") { MessageBox.Show("商品代码 不允许为空！"); return; }
            string strCk = "";
            string strHz = "";
            string strKh = "";
            string strFwdj = "";

            for (int i = 0; i < lvCk.Items.Count; i++)
            {
                if (lvCk.Items[i].Checked) strCk = strCk + lvCk.Items[i].Text+","; 
            }

            for (int i = 0; i < lvHz.Items.Count; i++)
            {
                if (lvHz.Items[i].Checked) strHz = strHz + lvHz.Items[i].Text + ",";
            }

            for (int i = 0; i < lvKh.Items.Count; i++)
            {
                strKh = strKh + lvKh.Items[i].Text + ",";
            }

            if (chbFwdj.Checked) { strFwdj = "附温度计"; } else { strFwdj = ""; }

            if (falg == "Add")
            {
                string SqlIns = "insert into cc_item_track(size_desc,whse,season,shipto,ifthermometer,create_date_time,user_name)"
                                + "values('" + tbSpdm.Text.Trim() + "','" + strCk.Trim() + "','"
                                + strHz.Trim() + "','" + strKh.Trim() + "','" + strFwdj.Trim() + "',sysdate,'"
                                + LoginCs.LoginClass.LogName + "')";
                LYFunctionCs.LYFunctionCsClass.cmd(SqlIns, CommFunction.ConnectString);
            }
            else
            {
                string SqlUpdate = "update cc_item_track set whse='" + strCk.Trim() + "'," + "season='" + strHz.Trim() +
                                    "', shipto='" + strKh.Trim() + "',ifthermometer='" + strFwdj.Trim() + "',user_name='" + LoginCs.LoginClass.LogName + "' " +
                                   "where unid='" + pUnid + "'";
                LYFunctionCs.LYFunctionCsClass.cmd(SqlUpdate, CommFunction.ConnectString);
            }

            ResultSpdm = ResultSpdm + "','" + tbSpdm.Text.Trim();

            ifSave = true;

            MessageBox.Show("完成。");

            if (falg == "Add") { Clean_Frm(); } else { Close(); }
        }
        
    }
}
