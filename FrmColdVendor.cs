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
    public partial class FrmColdVendor : Form
    {
        private static FrmColdVendor pUniqueForm = null;//窗体唯一打开代码

        /// <summary>
        /// 窗体唯一打开代码
        /// </summary>
        /// <param name="Path">是否作为模式窗体打开</param>
        public static void ShowUniqueForm(bool AIfShowModal)
        {
            if (pUniqueForm == null)
            {
                new FrmColdVendor();

                if (AIfShowModal) pUniqueForm.ShowDialog(); else pUniqueForm.Show();
            }
        }


        public FrmColdVendor()
        {
            pUniqueForm = this;//窗体唯一打开代码
            InitializeComponent();
        }

        private void FrmColdVendor_FormClosed(object sender, FormClosedEventArgs e)
        {
            pUniqueForm = null;//窗体唯一打开代码
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string pKhdm = "";
            if (tbKhdm.Text.Trim() == "") { pKhdm = ""; } else { pKhdm = " and 客户代码 like '%"+tbKhdm.Text+"%'"; }
            string pHzKhdm = "";
            if (tbHzkhdm.Text.Trim() == "") { pHzKhdm = ""; } else { pHzKhdm = " and MISC_INSTR_CODE_2 like '%" + tbHzkhdm.Text + "%'"; }
            string pKhmc = "";
            if (tbKhmc.Text.Trim() == "") { pKhmc = ""; } else { pKhmc = " and 客户名称 like '%" + tbKhmc.Text + "%'"; }

            string sql = "select 客户代码,MISC_INSTR_CODE_2 as 货主客户代码,客户名称 from v_vendor_master  where 1=1" + pKhdm + pHzKhdm + pKhmc + " order by 客户代码,MISC_INSTR_CODE_2";

            OleDbDataAdapter sda = new OleDbDataAdapter(sql, CommFunction.ConnectString);
            DataSet ds = new DataSet();
            sda.Fill(ds, "chk_con");

            dataGridView1.DataSource = ds.Tables["chk_con"];
            dataGridView1.AutoResizeColumns();

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if ((sender as DataGridView).CurrentRow == null) return;//如果无此句,则点击dataGridView的列标题时会出错

            string sql = "select unid,misc_instr_code_2 货主客户代码,whse 仓库,season 货主,ifthermometer 是否附温度计,therm_type 温度计类型 " +
                         "from cc_vendor_track where misc_instr_code_2='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["货主客户代码"].Value.ToString() + "'";

            OleDbDataAdapter sda = new OleDbDataAdapter(sql, CommFunction.ConnectString);
            DataSet ds = new DataSet();
            sda.Fill(ds, "chk_con");

            dataGridView2.DataSource = ds.Tables["chk_con"];
            dataGridView2.AutoResizeColumns(); 
        }

        private void 新增ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            if (dataGridView1.Rows.Count <= 0) return;
            FrmEditVendor.falg = "Add";
            FrmEditVendor.pHzkhdm = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["货主客户代码"].Value.ToString();
            FrmEditVendor.ShowUniqueForm(true);
            if (FrmEditVendor.ifSave)
            {
                string sql = "select unid,misc_instr_code_2 货主客户代码,whse 仓库,season 货主,ifthermometer 是否附温度计,therm_type 温度计类型 " +
                         "from cc_vendor_track where misc_instr_code_2 in (" + FrmEditVendor.ResultHzkhdm + "')";

                OleDbDataAdapter sda = new OleDbDataAdapter(sql, CommFunction.ConnectString);
                DataSet ds = new DataSet();
                sda.Fill(ds, "chk_con2");

                dataGridView2.DataSource = ds.Tables["chk_con2"];
                dataGridView2.AutoResizeColumns();
            }
        }

        private void 删除ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            if (dataGridView2.Rows.Count <= 0) return;
            if (MessageBox.Show("确定要删除该条记录吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.No) return;
            LYFunctionCs.LYFunctionCsClass.cmd("delete  from cc_vendor_track where unid='" + dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["unid"].Value.ToString() + "'", CommFunction.ConnectString);
            MessageBox.Show("删除成功！");

            string sql = "select unid,misc_instr_code_2 货主客户代码,whse 仓库,season 货主,ifthermometer 是否附温度计,therm_type 温度计类型 " +
                         "from cc_vendor_track where misc_instr_code_2='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["货主客户代码"].Value.ToString() + "'";

            OleDbDataAdapter sda = new OleDbDataAdapter(sql, CommFunction.ConnectString);
            DataSet ds = new DataSet();
            sda.Fill(ds, "chk_con2");

            dataGridView2.DataSource = ds.Tables["chk_con2"];
            dataGridView2.AutoResizeColumns();
        }

        private void 修改ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            if (dataGridView1.Rows.Count <= 0) return;
            if (dataGridView2.Rows.Count <= 0) return;
            FrmEditVendor.falg = "Edit";
            FrmEditVendor.pUnid = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["unid"].Value.ToString();
            FrmEditVendor.pCk = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["仓库"].Value.ToString();
            FrmEditVendor.pHz = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["货主"].Value.ToString();
            FrmEditVendor.pHzkhdm = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["货主客户代码"].Value.ToString();
            FrmEditVendor.pFwdj = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["是否附温度计"].Value.ToString();
            FrmEditVendor.pTh = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["温度计类型"].Value.ToString();
            FrmEditVendor.ShowUniqueForm(true);
            if (FrmEditVendor.ifSave)
            {
                string sql = "select unid,misc_instr_code_2 货主客户代码,whse 仓库,season 货主,ifthermometer 是否附温度计,therm_type 温度计类型 " +
                          "from cc_vendor_track where misc_instr_code_2='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["货主客户代码"].Value.ToString() + "'";

                OleDbDataAdapter sda = new OleDbDataAdapter(sql, CommFunction.ConnectString);
                DataSet ds = new DataSet();
                sda.Fill(ds, "chk_con2");

                dataGridView2.DataSource = ds.Tables["chk_con2"];
                dataGridView2.AutoResizeColumns();
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            修改ToolStripMenuItem1_Click(null, null);
        }
    }
}
