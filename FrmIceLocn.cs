using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LoginCs;
using PowerCheckCs;

namespace LLGL2012
{
    public partial class FrmIceLocn : Form
    {
        string ResultSelect = "";

        private static FrmIceLocn pUniqueForm = null;//窗体唯一打开代码

        /// <summary>
        /// 窗体唯一打开代码
        /// </summary>
        /// <param name="Path">是否作为模式窗体打开</param>
        public static void ShowUniqueForm(bool AIfShowModal)
        {
            if (pUniqueForm == null)
            {
                new FrmIceLocn();

                if (AIfShowModal) pUniqueForm.ShowDialog(); else pUniqueForm.Show();
            }
        }


        public FrmIceLocn()
        {
            pUniqueForm = this;//窗体唯一打开代码
            InitializeComponent();
        }

        private void FrmIceLocn_FormClosed(object sender, FormClosedEventArgs e)
        {
            pUniqueForm = null;//窗体唯一打开代码
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string pKwdm = "";
            if (tbKwdm.Text.Trim() == "") { pKwdm = ""; } else { pKwdm = " and LOCN_BRCD like '%" + tbKwdm.Text + "%'"; }
            string pCctj = "";
            if (cbCctj.Text.Trim() == "") { pCctj = ""; } else { pCctj = " and STORE_CONDITION ='" + cbCctj.Text + "'"; }

            ResultSelect = "select LOCN_BRCD 库位代码,STORE_CONDITION 存储条件 from locn_master where 1=1  " + pKwdm + pCctj + " order by LOCN_BRCD";

            OleDbDataAdapter sda = new OleDbDataAdapter(ResultSelect, CommFunction.ConnectString);
            DataSet ds = new DataSet();
            sda.Fill(ds, "chk_con");

            dataGridView1.DataSource = ds.Tables["chk_con"];
            dataGridView1.AutoResizeColumns();
        }

        private void FrmIceLocn_Load(object sender, EventArgs e)
        {
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

        private void 新增ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            FrmEditIceLocn.falg = "Add";
            FrmEditIceLocn.ShowUniqueForm(true);
            if (FrmEditIceLocn.ifSave)
            {
                string sql = "select LOCN_BRCD 库位代码,STORE_CONDITION 存储条件 from locn_master where  LOCN_BRCD in (" + FrmEditIceLocn.ResultKwdm + "') order by  LOCN_BRCD";

                OleDbDataAdapter sda = new OleDbDataAdapter(sql, CommFunction.ConnectString);
                DataSet ds = new DataSet();
                sda.Fill(ds, "chk_con2");

                dataGridView1.DataSource = ds.Tables["chk_con2"];
                dataGridView1.AutoResizeColumns();
            }
        }

        private void 删除ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            if (dataGridView1.Rows.Count <= 0) return;
            if (MessageBox.Show("确定要删除该条记录吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.No) return;
            LYFunctionCs.LYFunctionCsClass.cmd("delete  from locn_master where LOCN_BRCD='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["库位代码"].Value.ToString() + "'", CommFunction.ConnectString);
            MessageBox.Show("删除成功！");

            OleDbDataAdapter sda = new OleDbDataAdapter(ResultSelect, CommFunction.ConnectString);
            DataSet ds = new DataSet();
            sda.Fill(ds, "chk_con2");

            dataGridView1.DataSource = ds.Tables["chk_con2"];
            dataGridView1.AutoResizeColumns();
        }

        private void 修改ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            if (dataGridView1.Rows.Count <= 0) return;
            FrmEditIceLocn.falg = "Edit";
            FrmEditIceLocn.pKwdm = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["库位代码"].Value.ToString();
            FrmEditIceLocn.pCctj = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["存储条件"].Value.ToString();
            FrmEditIceLocn.ShowUniqueForm(true);
            if (FrmEditIceLocn.ifSave)
            {
                OleDbDataAdapter sda = new OleDbDataAdapter(ResultSelect, CommFunction.ConnectString);
                DataSet ds = new DataSet();
                sda.Fill(ds, "chk_con2");

                dataGridView1.DataSource = ds.Tables["chk_con2"];
                dataGridView1.AutoResizeColumns();
            }
        }
    }
}
