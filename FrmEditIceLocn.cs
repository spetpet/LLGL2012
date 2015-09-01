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
    public partial class FrmEditIceLocn : Form
    {
        public static string ResultKwdm = "'";
        public static string falg = "Edit";
        public static Boolean ifSave = false;

        public static string pKwdm = "";
        public static string pCctj = "";

        private static FrmEditIceLocn pUniqueForm = null;//窗体唯一打开代码

        /// <summary>
        /// 窗体唯一打开代码
        /// </summary>
        /// <param name="Path">是否作为模式窗体打开</param>
        public static void ShowUniqueForm(bool AIfShowModal)
        {
            if (pUniqueForm == null)
            {
                new FrmEditIceLocn();

                if (AIfShowModal) pUniqueForm.ShowDialog(); else pUniqueForm.Show();
            }
        }

        public FrmEditIceLocn()
        {
            pUniqueForm = this;//窗体唯一打开代码 
            InitializeComponent();
        }

        private void FrmEditIceLocn_FormClosed(object sender, FormClosedEventArgs e)
        {
            pUniqueForm = null;//窗体唯一打开代码
        }

        private void FrmEditIceLocn_Load(object sender, EventArgs e)
        {
            ifSave = false;
            Clean_Frm();
            Load_Frm();
            cbCctj.Items.Clear();
            string strCctj = "select Distinct STORE_CONDITION from ICE_master";
            OleDbDataAdapter sda = new OleDbDataAdapter(strCctj, CommFunction.ConnectString);
            DataTable CctjTable = new DataTable();
            sda.Fill(CctjTable);
            for (int i = 0; i < CctjTable.Rows.Count; i++)
            {
                cbCctj.Items.Add(CctjTable.Rows[i][0]);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Clean_Frm()
        {
            tbKwdm.Text = "";
            cbCctj.Text = "";

        }

        private void Load_Frm()
        {
            if (falg == "Add")
            {
                tbKwdm.Enabled = true;
                tbKwdm.BackColor = System.Drawing.SystemColors.Window;
            }
            else
            {
                tbKwdm.Enabled = false;
                tbKwdm.BackColor = System.Drawing.SystemColors.Control;
                tbKwdm.Text = pKwdm;
                cbCctj.Text = pCctj;
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (tbKwdm.Text.Trim() == "") { MessageBox.Show("库位代码 不允许为空！"); return; }
            string sql = "select count(1) from locn_master where LOCN_BRCD='" + tbKwdm.Text.Trim() + "'";
            if (int.Parse(LYFunctionCs.LYFunctionCsClass.cmdScalar(sql, CommFunction.ConnectString)) == 0)
            {
                string SqlIns = "insert into locn_master (LOCN_BRCD,STORE_CONDITION) values ('" + tbKwdm.Text.Trim() + "','" + cbCctj.Text.Trim() + "')";
                LYFunctionCs.LYFunctionCsClass.cmd(SqlIns, CommFunction.ConnectString);
            }
            else
            {
                string SqlUpdate = "update locn_master set LOCN_BRCD='" + tbKwdm.Text.Trim() + "',STORE_CONDITION='" + cbCctj.Text.Trim() +
                                   "' where LOCN_BRCD='" + tbKwdm.Text.Trim() + "'";
                LYFunctionCs.LYFunctionCsClass.cmd(SqlUpdate, CommFunction.ConnectString);
            }

            ResultKwdm = ResultKwdm + "','" + tbKwdm.Text.Trim();

            ifSave = true;

            MessageBox.Show("完成。");

            if (falg == "Add") { Clean_Frm(); } else { Close(); }
        }
    }
}
