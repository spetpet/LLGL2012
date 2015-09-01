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
    public partial class FrmItemMaster : Form
    {
        string ResultSelect = "";

        private static FrmItemMaster pUniqueForm = null;//窗体唯一打开代码

        /// <summary>
        /// 窗体唯一打开代码
        /// </summary>
        /// <param name="Path">是否作为模式窗体打开</param>
        public static void ShowUniqueForm(bool AIfShowModal)
        {
            if (pUniqueForm == null)
            {
                new FrmItemMaster();

                if (AIfShowModal) pUniqueForm.ShowDialog(); else pUniqueForm.Show();
            }
        }

        public FrmItemMaster()
        {
            pUniqueForm = this;//窗体唯一打开代码
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            
            ResultSelect = "";

            string sql = "select sku_id,season as 货主,size_desc as 商品代码,sku_desc as 品名规格,cc_flag as 冷链标示,manufacturer as 生产厂家," +
                         "storage_conditions as 存储条件,therm_company as 温度计所属单位,pym as 拼音码,create_date_time as 创建时间,user_name as 操作人员," +
                         "std_pack_qty as 件装数,pack_type_z as 整件包装类型,pack_type_l as 零散包装类型,REPORT_FILE 跟踪报表名称 from cc_item_master ";

            if (CommFunction.ShowQueryForm(this.Handle, CommFunction.ConnectString, sql, 3, ref ResultSelect) == false) { return; }

            OleDbDataAdapter sda = new OleDbDataAdapter(ResultSelect, CommFunction.ConnectString);
            DataSet ds = new DataSet();
            sda.Fill(ds, "chk_con2");

            dataGridView1.DataSource = ds.Tables["chk_con2"];
            dataGridView1.AutoResizeColumns();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmItemMaster_FormClosed(object sender, FormClosedEventArgs e)
        {
            pUniqueForm = null;//窗体唯一打开代码
        }

        private void 新增ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            FrmEditItem.falg = "Add";
            FrmEditItem.ShowUniqueForm(true);
            if (FrmEditItem.ifSave)
            {
                string sql = "select sku_id,season 货主,size_desc 商品代码,sku_desc 品名规格,cc_flag 冷链标示,manufacturer 生产厂家," +
                         "storage_conditions 存储条件,therm_company 温度计所属单位,pym 拼音码,create_date_time 创建时间,user_name 操作人员," +
                         "std_pack_qty 件装数,pack_type_z 整件包装类型,pack_type_l 零散包装类型,REPORT_FILE 跟踪报表名称 from cc_item_master where 1=1 and sku_id in (" + FrmEditItem.ResultSku_id + "')";

                OleDbDataAdapter sda = new OleDbDataAdapter(sql, CommFunction.ConnectString);
                DataSet ds = new DataSet();
                sda.Fill(ds, "chk_con2");

                dataGridView1.DataSource = ds.Tables["chk_con2"];
                dataGridView1.AutoResizeColumns();
            }
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            if (dataGridView1.Rows.Count <= 0) return;
            FrmEditItem.falg = "Edit";
            FrmEditItem.pSku_id = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
            FrmEditItem.pHz = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString();
            FrmEditItem.pSpdm = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString();
            FrmEditItem.pPmgg = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString();
            FrmEditItem.pSccj = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[5].Value.ToString();
            FrmEditItem.pLlbs = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value.ToString();
            FrmEditItem.pCctj = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[6].Value.ToString();
            FrmEditItem.pJzs = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[11].Value.ToString();
            FrmEditItem.pWdj = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[7].Value.ToString();
            FrmEditItem.pBzlxZ = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[12].Value.ToString();
            FrmEditItem.pBzlxL = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[13].Value.ToString();
            FrmEditItem.pRpt = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[14].Value.ToString();
            FrmEditItem.ShowUniqueForm(true);
            if (FrmEditItem.ifSave)
            {

                OleDbDataAdapter sda = new OleDbDataAdapter(ResultSelect, CommFunction.ConnectString);
                DataSet ds = new DataSet();
                sda.Fill(ds, "chk_con2");

                dataGridView1.DataSource = ds.Tables["chk_con2"];
                dataGridView1.AutoResizeColumns(); 
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            if (dataGridView1.Rows.Count <= 0) return;
            if (MessageBox.Show("确定要删除该条记录吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.No) return;
            LYFunctionCs.LYFunctionCsClass.cmd("delete  from cc_item_master where sku_id='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString()+"'" ,CommFunction.ConnectString);
            string pNum = LYFunctionCs.LYFunctionCsClass.cmdScalar("select count(1) from cc_item_master c where c.size_desc='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString() + "' and c.cc_flag='冷链' ", CommFunction.ConnectString);
            if (int.Parse(pNum) <= 0) { LYFunctionCs.LYFunctionCsClass.cmd("delete  from cc_item_track where size_desc='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString() + "'", CommFunction.ConnectString); }
            MessageBox.Show("删除成功！");

            OleDbDataAdapter sda = new OleDbDataAdapter(ResultSelect, CommFunction.ConnectString);
            DataSet ds = new DataSet();
            sda.Fill(ds, "chk_con2");

            dataGridView1.DataSource = ds.Tables["chk_con2"];
            dataGridView1.AutoResizeColumns();

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            修改ToolStripMenuItem_Click(null,null);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
        }


        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if ((sender as DataGridView).CurrentRow == null) return;//如果无此句,则点击dataGridView的列标题时会出错

            if (dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["冷链标示"].Value.ToString() != "冷链")
            { dataGridView1.AutoGenerateColumns = false; dataGridView2.DataSource = null; return; }

            string sql = "select unid,size_desc 商品代码,whse 仓库,season 货主,shipto 客户,ifthermometer 是否附温度计,create_date_time 时间,user_name 操作人 "+
                         "from cc_item_track where size_desc='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString() + "'";

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
            if (dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value.ToString() != "冷链") { MessageBox.Show("非冷链品种，请核对！"); return; }
            FrmItemTrack.falg = "Add";
            FrmItemTrack.pSpdm = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString();
            FrmItemTrack.ShowUniqueForm(true);           
            if (FrmItemTrack.ifSave)
            {
                string sql = "select unid,size_desc 商品代码,whse 仓库,season 货主,shipto 客户,ifthermometer 是否附温度计,create_date_time 时间,user_name 操作人 " +
                             "from cc_item_track where 1=1 and size_desc in (" + FrmItemTrack.ResultSpdm + "')";

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
            LYFunctionCs.LYFunctionCsClass.cmd("delete  from cc_item_track where unid='" + dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", CommFunction.ConnectString);
            MessageBox.Show("删除成功！");

            string sql = "select unid,size_desc 商品代码,whse 仓库,season 货主,shipto 客户,ifthermometer 是否附温度计,create_date_time 时间,user_name 操作人 " +
                         "from cc_item_track where size_desc='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString() + "'";

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
            if (dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value.ToString() != "冷链") { MessageBox.Show("非冷链品种，请核对！"); return; }
            if (dataGridView2.Rows.Count <= 0) return;
            FrmItemTrack.falg = "Edit";
            FrmItemTrack.pUnid = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[0].Value.ToString();
            FrmItemTrack.pSpdm = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[1].Value.ToString();
            FrmItemTrack.pCk = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[2].Value.ToString();
            FrmItemTrack.pHz = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[3].Value.ToString();
            FrmItemTrack.pKhdm = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[4].Value.ToString();
            FrmItemTrack.pFwdj = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[5].Value.ToString();
            FrmItemTrack.ShowUniqueForm(true);
            if (FrmItemTrack.ifSave)
            {
                string sql = "select unid,size_desc 商品代码,whse 仓库,season 货主,shipto 客户,ifthermometer 是否附温度计,create_date_time 时间,user_name 操作人 " +
                         "from cc_item_track where size_desc='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString() + "'";

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
