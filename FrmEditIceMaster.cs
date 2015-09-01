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
    public partial class FrmEditIceMaster : Form
    {
        public static string ResultSpdm = "'";
        public static string falg = "Edit";
        public static Boolean ifSave = false;

        public static string pSpdm = "";
        public static string pPm = "";
        public static string pGg = "";
        public static string pLx = "";
        public static string pDw = "";
        public static string pSxsj = "";
        public static string pSexsj = "";
        public static string pCctj = "";
        public static string pSsgs = "";

        private static FrmEditIceMaster pUniqueForm = null;//窗体唯一打开代码

        /// <summary>
        /// 窗体唯一打开代码
        /// </summary>
        /// <param name="Path">是否作为模式窗体打开</param>
        public static void ShowUniqueForm(bool AIfShowModal)
        {
            if (pUniqueForm == null)
            {
                new FrmEditIceMaster();

                if (AIfShowModal) pUniqueForm.ShowDialog(); else pUniqueForm.Show();
            }
        }

        
        public FrmEditIceMaster()
        {
            pUniqueForm = this;//窗体唯一打开代码
            InitializeComponent();
        }

        private void FrmEditIceMaster_FormClosed(object sender, FormClosedEventArgs e)
        {
            pUniqueForm = null;//窗体唯一打开代码
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmEditIceMaster_Load(object sender, EventArgs e)
        {
            ifSave = false;
            Clean_Frm();
            Load_Frm();
        }

        private void Clean_Frm()
        {
            tbSpdm.Text = "";
            tbPm.Text = "";
            tbGg.Text = "";
            cbLx.Text = "";
            tbDw.Text = "";
            tbSxsj.Text = "";
            tbSexsj.Text = "";
            tbCctj.Text = "";
            tbSsgs.Text = "";

        }

        private void Load_Frm()
        {
            if (falg == "Add")
            {
                tbSpdm.Enabled = true;
                tbSpdm.BackColor = System.Drawing.SystemColors.Window;
            }
            else
            {
                tbSpdm.Enabled = false;
                tbSpdm.BackColor = System.Drawing.SystemColors.Control;
                tbSpdm.Text = pSpdm;
                tbPm.Text = pPm;
                tbGg.Text = pGg;
                cbLx.Text = pLx;
                tbDw.Text = pDw;
                tbSxsj.Text = pSxsj;
                tbSexsj.Text = pSexsj;
                tbCctj.Text = pCctj;
                tbSsgs.Text = pSsgs;
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (tbSpdm.Text.Trim() == "") { MessageBox.Show("商品代码 不允许为空！"); return; }
            if (tbSsgs.Text.Trim() == "") { MessageBox.Show("所属公司 不允许为空！"); return; }
            string sql = "select count(1) from ice_master where SIZE_DESC='" + tbSpdm.Text.Trim() + "'";
            if (int.Parse(LYFunctionCs.LYFunctionCsClass.cmdScalar(sql, CommFunction.ConnectString)) == 0)
            {
                string SqlIns = "insert into ice_master(size_desc,sku_desc,sku_format,sku_type,season,invalid_time,valid_time,store_condition,sku_unit) values('" + tbSpdm.Text.Trim() + "','" + tbPm.Text.Trim() + "','"
                                + tbGg.Text.Trim() + "','" + cbLx.Text.Trim() + "','" + tbSsgs.Text.Trim() + "','" + tbSxsj.Text.Trim() + "','" + tbSexsj.Text.Trim()
                                + "','" + tbCctj.Text.Trim() + "','" + tbDw.Text.Trim()  + "')";
                LYFunctionCs.LYFunctionCsClass.cmd(SqlIns, CommFunction.ConnectString);
            }
            else
            {
                string SqlUpdate = "update ice_master set size_desc='" + tbSpdm.Text.Trim() + "',sku_desc='" + tbPm.Text.Trim() + "',sku_format='" + tbGg.Text.Trim() + "',sku_type='" + cbLx.Text.Trim() +
                                   "',season='" + tbSsgs.Text.Trim() + "',invalid_time='" + tbSxsj.Text.Trim() + "',valid_time='" + tbSexsj.Text.Trim() + "',store_condition='" + tbCctj.Text.Trim() +
                                   "',sku_unit='" + tbDw.Text.Trim() + "' " +
                                   "where SIZE_DESC='" + tbSpdm.Text.Trim() + "'";
                LYFunctionCs.LYFunctionCsClass.cmd(SqlUpdate, CommFunction.ConnectString);
            }

            ResultSpdm = ResultSpdm + "','" + tbSpdm.Text.Trim();

            ifSave = true;

            MessageBox.Show("完成。");

            if (falg == "Add") { Clean_Frm(); } else { Close(); }
        }

    }
}
