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
    public partial class FrmIceMaster : Form
    {
        string ResultSelect = "";

        private static FrmIceMaster pUniqueForm = null;//窗体唯一打开代码

        /// <summary>
        /// 窗体唯一打开代码
        /// </summary>
        /// <param name="Path">是否作为模式窗体打开</param>
        public static void ShowUniqueForm(bool AIfShowModal)
        {
            if (pUniqueForm == null)
            {
                new FrmIceMaster();

                if (AIfShowModal) pUniqueForm.ShowDialog(); else pUniqueForm.Show();
            }
        }

        
        public FrmIceMaster()
        {
            pUniqueForm = this;//窗体唯一打开代码
            InitializeComponent();
        }

        private void FrmIceMaster_FormClosed(object sender, FormClosedEventArgs e)
        {
            pUniqueForm = null;//窗体唯一打开代码
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string pSpdm = "";
            if (tbSpdm.Text.Trim() == "") { pSpdm = ""; } else { pSpdm = " and SIZE_DESC like '%" + tbSpdm.Text + "%'"; }
            string pPm = "";
            if (tbPm.Text.Trim() == "") { pPm = ""; } else { pPm = " and SKU_DESC like '%" + tbPm.Text + "%'"; }
            string pGg = "";
            if (tbGg.Text.Trim() == "") { pGg = ""; } else { pGg = " and SKU_FORMAT like '%" + tbGg.Text + "%'"; }
            string pLx = "";
            if (cbLx.Text.Trim() == "") { pLx = ""; } else { pLx = " and SKU_TYPE ='" + cbLx.Text + "'"; }

            ResultSelect = "select size_desc 商品代码,sku_desc 品名,sku_format 规格,sku_type 类型, season 所属公司,invalid_time 失效时间,valid_time 生效时间,store_condition 存储条件,sku_unit 单位" +
                         " from ice_master where 1=1" + pSpdm + pPm + pGg + pLx + " order by  size_desc";

            OleDbDataAdapter sda = new OleDbDataAdapter(ResultSelect, CommFunction.ConnectString);
            DataSet ds = new DataSet();
            sda.Fill(ds, "chk_con");

            dataGridView1.DataSource = ds.Tables["chk_con"];
            dataGridView1.AutoResizeColumns();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 新增ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            FrmEditIceMaster.falg = "Add";
            FrmEditIceMaster.ShowUniqueForm(true);
            if (FrmEditIceMaster.ifSave)
            {
                string sql = "select size_desc 商品代码,sku_desc 品名,sku_format 规格,sku_type 类型, season 所属公司,invalid_time 失效时间,valid_time 生效时间,store_condition 存储条件,sku_unit 单位"+
                         " from ice_master where size_desc in (" + FrmEditIceMaster.ResultSpdm + "') order by  size_desc";

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
            LYFunctionCs.LYFunctionCsClass.cmd("delete  from ice_master where SIZE_DESC='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["商品代码"].Value.ToString()  + "'", CommFunction.ConnectString);
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
            FrmEditIceMaster.falg = "Edit";
            FrmEditIceMaster.pSpdm = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["商品代码"].Value.ToString();
            FrmEditIceMaster.pPm = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["品名"].Value.ToString();
            FrmEditIceMaster.pGg = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["规格"].Value.ToString();
            FrmEditIceMaster.pLx = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["类型"].Value.ToString();
            FrmEditIceMaster.pDw = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["单位"].Value.ToString();
            FrmEditIceMaster.pSxsj = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["失效时间"].Value.ToString();
            FrmEditIceMaster.pSexsj = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["生效时间"].Value.ToString();
            FrmEditIceMaster.pCctj = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["存储条件"].Value.ToString();
            FrmEditIceMaster.pSsgs = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["所属公司"].Value.ToString();
            FrmEditIceMaster.ShowUniqueForm(true);
            if (FrmEditIceMaster.ifSave)
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
