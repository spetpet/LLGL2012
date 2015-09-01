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
    public partial class FrmEditCode : Form
    {
        public static string ResultCode_type = "'";
        public static string ResultCode_id = "'";

        public static string falg = "Edit";
        public static Boolean ifSave = false;

        public static string pLx = "";
        public static string pDm = "";
        public static string pMc = "";
        public static string pPym = "";
        public static string pBz = "";
        public static string pBl1 = "";
        public static string pBl2 = "";
        public static string pBl3 = "";
        public static string pBl4 = "";
        public static string pBl5 = "";
        public static string pBl6 = "";
        public static string pBl7 = "";
        public static string pBl8 = "";
        public static string pBl9 = "";
        public static string pBl10 = "";
        public static string pTy = "";


        private static FrmEditCode pUniqueForm = null;//窗体唯一打开代码

        /// <summary>
        /// 窗体唯一打开代码
        /// </summary>
        /// <param name="Path">是否作为模式窗体打开</param>
        public static void ShowUniqueForm(bool AIfShowModal)
        {
            if (pUniqueForm == null)
            {
                new FrmEditCode();

                if (AIfShowModal) pUniqueForm.ShowDialog(); else pUniqueForm.Show();
            }
        }

        public FrmEditCode()
        {
            pUniqueForm = this;//窗体唯一打开代码
            InitializeComponent();
        }

        private void FrmEditCode_FormClosed(object sender, FormClosedEventArgs e)
        {
            pUniqueForm = null;//窗体唯一打开代码
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmEditCode_Load(object sender, EventArgs e)
        {
            ifSave = false;
            Clean_Frm();
            string strLx = "select distinct type from ysfs order by type";
            OleDbDataAdapter sda = new OleDbDataAdapter(strLx, CommFunction.ConnectString);
            DataTable LXTable = new DataTable();
            sda.Fill(LXTable);
            for (int i = 0; i < LXTable.Rows.Count; i++)
            {
                cbLx.Items.Add(LXTable.Rows[i][0]);
            }
            Load_Frm();
        }

        private void cbLx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLx.Text.Trim() == "职员")
            {
                lbDm.Text = "工号";
                lbMc.Text = "姓名";
                lbBz.Text = "备注";
                lbBl1.Text = "密码";
                lbBl2.Text = "保留字段2";
                lbBl3.Text = "保留字段3";
                lbBl4.Text = "保留字段4";
                lbBl5.Text = "保留字段5";
                lbBl6.Text = "保留字段6";
                lbBl7.Text = "保留字段7";
                lbBl8.Text = "保留字段8";
                lbBl9.Text = "保留字段9";
                lbBl10.Text = "保留字段10";
                tbBl1.PasswordChar = '*';
            }
            else if (cbLx.Text.Trim() == "温度计所属单位")
            {
                lbDm.Text = "代码";
                lbMc.Text = "公司";
                lbBz.Text = "备注";
                lbBl1.Text = "保留字段1";
                lbBl2.Text = "保留字段2";
                lbBl3.Text = "保留字段3";
                lbBl4.Text = "保留字段4";
                lbBl5.Text = "保留字段5";
                lbBl6.Text = "保留字段6";
                lbBl7.Text = "保留字段7";
                lbBl8.Text = "保留字段8";
                lbBl9.Text = "保留字段9";
                lbBl10.Text = "保留字段10";
                tbBl1.PasswordChar = new char();
            }
            else if (cbLx.Text.Trim() == "温度计字典")
            {
                lbDm.Text = "代码";
                lbMc.Text = "名称";
                lbBz.Text = "备注";
                lbBl1.Text = "在库状态";
                lbBl2.Text = "温度计状态";
                lbBl3.Text = "所属公司";
                lbBl4.Text = "保留字段4";
                lbBl5.Text = "保留字段5";
                lbBl6.Text = "保留字段6";
                lbBl7.Text = "保留字段7";
                lbBl8.Text = "保留字段8";
                lbBl9.Text = "保留字段9";
                lbBl10.Text = "保留字段10";
                tbBl1.PasswordChar = new char();
            }
            else if (cbLx.Text.Trim() == "温度与包装对照表")
            {
                lbDm.Text = "代码";
                lbMc.Text = "类型";
                lbBz.Text = "包装";
                lbBl1.Text = "最高温度(出发)";
                lbBl2.Text = "最低温度(出发)";
                lbBl3.Text = "最高温度(目的)";
                lbBl4.Text = "最低温度(目的)";
                lbBl5.Text = "包装总金额";
                lbBl6.Text = "保留字段6";
                lbBl7.Text = "保留字段7";
                lbBl8.Text = "保留字段8";
                lbBl9.Text = "保留字段9";
                lbBl10.Text = "保留字段10";
                tbBl1.PasswordChar = new char();
            }
            else if (cbLx.Text.Trim() == "冰排所属单位")
            {
                lbDm.Text = "代码";
                lbMc.Text = "名称";
                lbBz.Text = "备注";
                lbBl1.Text = "保留字段1";
                lbBl2.Text = "保留字段2";
                lbBl3.Text = "保留字段3";
                lbBl4.Text = "保留字段4";
                lbBl5.Text = "保留字段5";
                lbBl6.Text = "保留字段6";
                lbBl7.Text = "保留字段7";
                lbBl8.Text = "保留字段8";
                lbBl9.Text = "保留字段9";
                lbBl10.Text = "保留字段10";
                tbBl1.PasswordChar = new char();
            }
            else 
            {
                lbDm.Text = "代码";
                lbMc.Text = "名称";
                lbBz.Text = "备注";
                lbBl1.Text = "保留字段1";
                lbBl2.Text = "保留字段2";
                lbBl3.Text = "保留字段3";
                lbBl4.Text = "保留字段4";
                lbBl5.Text = "保留字段5";
                lbBl6.Text = "保留字段6";
                lbBl7.Text = "保留字段7";
                lbBl8.Text = "保留字段8";
                lbBl9.Text = "保留字段9";
                lbBl10.Text = "保留字段10";
                tbBl1.PasswordChar = new char();
            }
        }

        private void Clean_Frm()
        {
            cbLx.Text = "";
            tbDm.Text = "";
            tbMc.Text = "";
            tbPym.Text = "";
            tbBz.Text = "";
            tbBl1.Text = "";
            tbBl2.Text = "";
            tbBl3.Text = "";
            tbBl4.Text = "";
            tbBl5.Text = "";
            tbBl6.Text = "";
            tbBl7.Text = "";
            tbBl8.Text = "";
            tbBl9.Text = "";
            tbBl10.Text = "";
            chbTy.Checked = false;

        }

        private void Load_Frm()
        {
            if (falg == "Add")
            {
                cbLx.Enabled = true;
                tbDm.Enabled = true;
                cbLx.BackColor = System.Drawing.SystemColors.Window;
                tbDm.BackColor = System.Drawing.SystemColors.Window;
            }
            else
            {
                cbLx.Enabled = false;
                tbDm.Enabled = false;
                cbLx.BackColor = System.Drawing.SystemColors.Control;
                tbDm.BackColor = System.Drawing.SystemColors.Control;
                cbLx.Text = pLx;
                tbDm.Text = pDm;
                tbMc.Text = pMc;
                tbPym.Text = pPym;
                tbBz.Text = pBz;
                tbBl1.Text = pBl1;
                tbBl2.Text = pBl2;
                tbBl3.Text = pBl3;
                tbBl4.Text = pBl4;
                tbBl5.Text = pBl5;
                tbBl6.Text = pBl6;
                tbBl7.Text = pBl7;
                tbBl8.Text = pBl8;
                tbBl9.Text = pBl9;
                tbBl10.Text = pBl10;
                if (pTy == "停用") { chbTy.Checked = true; } else chbTy.Checked = false;
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (cbLx.Text.Trim() == "") { MessageBox.Show("类型 不允许为空！"); return; }
            if (tbDm.Text.Trim() == "") { MessageBox.Show("代码 不允许为空！"); return; }
            string sql = "select count(1) from ysfs where type='" + cbLx.Text.Trim() + "' and ysid='" + tbDm.Text.Trim() + "'";
            if (int.Parse(LYFunctionCs.LYFunctionCsClass.cmdScalar(sql, CommFunction.ConnectString)) == 0)
            {
                string SqlIns = "insert into ysfs(TYPE,ysid,ysfs,PYM,MEMO,RESERVE1,RESERVE2,RESERVE3,RESERVE4,RESERVE5,RESERVE6,"
                                + "RESERVE7,RESERVE8,RESERVE9,RESERVE10,create_date_time,user_name,FLAG) values('" + cbLx.Text.Trim() + "','" + tbDm.Text.Trim() + "','"
                                + tbMc.Text.Trim() + "','" + tbPym.Text.Trim() + "','" + tbBz.Text.Trim() + "','" + tbBl1.Text.Trim() + "','" + tbBl2.Text.Trim()
                                + "','" + tbBl3.Text.Trim() + "','" + tbBl4.Text.Trim() + "','" + tbBl5.Text.Trim() + "','" + tbBl6.Text.Trim() + "','" + tbBl7.Text.Trim()
                                + "','" + tbBl8.Text.Trim() + "','" + tbBl9.Text.Trim() + "','" + tbBl10.Text.Trim() + "',sysdate,'"
                                + LoginCs.LoginClass.LogName + "','" + (chbTy.Checked.ToString().ToUpper()=="true".ToUpper()?"停用":"启用") + "')";
                LYFunctionCs.LYFunctionCsClass.cmd(SqlIns, CommFunction.ConnectString);
            }
            else
            {
                string SqlUpdate = "update ysfs set ysfs='" + tbMc.Text.Trim() + "',PYM='" + tbPym.Text.Trim() + "',MEMO='" + tbBz.Text.Trim() + "',RESERVE1='" + tbBl1.Text.Trim() +
                                   "',RESERVE2='" + tbBl2.Text.Trim() + "',RESERVE3='" + tbBl3.Text.Trim() + "',RESERVE4='" + tbBl4.Text.Trim() + "',RESERVE5='" + tbBl5.Text.Trim() +
                                   "',RESERVE6='" + tbBl6.Text.Trim() + "',RESERVE7='" + tbBl7.Text.Trim() + "',RESERVE8='" + tbBl8.Text.Trim() + "',RESERVE9='" + tbBl9.Text.Trim() +
                                   "',RESERVE10='" + tbBl10.Text.Trim() + "',user_name='" + LoginCs.LoginClass.LogName + "', FLAG='" + (chbTy.Checked.ToString().ToUpper() == "true".ToUpper() ? "停用" : "启用") + "' " +
                                   "where type='" + cbLx.Text.Trim() + "' and ysid='" + tbDm.Text.Trim() + "'";
                LYFunctionCs.LYFunctionCsClass.cmd(SqlUpdate, CommFunction.ConnectString);
            }

            ResultCode_type = ResultCode_type + "','" + cbLx.Text.Trim();
            ResultCode_id = ResultCode_id + "','" + tbDm.Text.Trim();
            
            ifSave = true;

            MessageBox.Show("完成。");

            if (falg == "Add") { Clean_Frm(); } else { Close(); }
        }
    }
}
