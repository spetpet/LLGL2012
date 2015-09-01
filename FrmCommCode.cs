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
    public partial class FrmCommCode : Form
    {
        string ResultSelect = "";

        private static FrmCommCode pUniqueForm = null;//窗体唯一打开代码

        /// <summary>
        /// 窗体唯一打开代码
        /// </summary>
        /// <param name="Path">是否作为模式窗体打开</param>
        public static void ShowUniqueForm(bool AIfShowModal)
        {
            if (pUniqueForm == null)
            {
                new FrmCommCode();

                if (AIfShowModal) pUniqueForm.ShowDialog(); else pUniqueForm.Show();
            }
        }

        public FrmCommCode()
        {
            pUniqueForm = this;//窗体唯一打开代码
            InitializeComponent();
        }

        private void FrmCommCode_FormClosed(object sender, FormClosedEventArgs e)
        {
            pUniqueForm = null;//窗体唯一打开代码
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            
            ResultSelect = "";

            string sql = "select type as 类型,ysid as 代码,ysfs as 名称,pym as 拼音码,memo as 备注,reserve1 as 保留字段1,reserve2 as 保留字段2,reserve3 as 保留字段3," +
                         "reserve4 as 保留字段4,reserve5 as 保留字段5,reserve6 as 保留字段6,reserve7 as 保留字段7,reserve8 as 保留字段8," +
                         "reserve9 as 保留字段9,reserve10 as 保留字段10,create_date_time as 创建时间,user_name as 操作员,flag as 是否停用 " +
                         "from ysfs order by type,ysid ";

            if (CommFunction.ShowQueryForm(this.Handle, CommFunction.ConnectString, sql, 3, ref ResultSelect) == false) { return; }

            OleDbDataAdapter sda = new OleDbDataAdapter(ResultSelect, CommFunction.ConnectString);
            DataSet ds = new DataSet();
            sda.Fill(ds, "chk_con2");

            dataGridView1.DataSource = ds.Tables["chk_con2"];
            dataGridView1.AutoResizeColumns();
        }

        private void 新增ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            FrmEditCode.falg = "Add";
            FrmEditCode.ShowUniqueForm(true);
            if (FrmEditCode.ifSave)
            {
                string sql = "select type as 类型,ysid as 代码,ysfs as 名称,pym as 拼音码,memo as 备注,reserve1 as 保留字段1,reserve2 as 保留字段2,reserve3 as 保留字段3," +
                             "reserve4 as 保留字段4,reserve5 as 保留字段5,reserve6 as 保留字段6,reserve7 as 保留字段7,reserve8 as 保留字段8," +
                             "reserve9 as 保留字段9,reserve10 as 保留字段10,create_date_time as 创建时间,user_name as 操作员,flag as 是否停用 " +
                            "from ysfs where 1=1 and type in (" + FrmEditCode.ResultCode_type + "') and ysid in (" + FrmEditCode.ResultCode_id + "') order by type,ysid";

                OleDbDataAdapter sda = new OleDbDataAdapter(sql, CommFunction.ConnectString);
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
            LYFunctionCs.LYFunctionCsClass.cmd("delete  from ysfs where type='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() +
                                               "' and ysid='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString() + "'", CommFunction.ConnectString);
            MessageBox.Show("删除成功！");

            OleDbDataAdapter sda = new OleDbDataAdapter(ResultSelect, CommFunction.ConnectString);
            DataSet ds = new DataSet();
            sda.Fill(ds, "chk_con2");

            dataGridView1.DataSource = ds.Tables["chk_con2"];
            dataGridView1.AutoResizeColumns();
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            if (dataGridView1.Rows.Count <= 0) return;
            FrmEditCode.falg = "Edit";
            FrmEditCode.pLx = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
            FrmEditCode.pDm = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString();
            FrmEditCode.pMc = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString();
            FrmEditCode.pPym = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString();
            FrmEditCode.pBz = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value.ToString();
            FrmEditCode.pBl1 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[5].Value.ToString();
            FrmEditCode.pBl2 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[6].Value.ToString();
            FrmEditCode.pBl3 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[7].Value.ToString();
            FrmEditCode.pBl4 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[8].Value.ToString();
            FrmEditCode.pBl5 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[9].Value.ToString();
            FrmEditCode.pBl6 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[10].Value.ToString();
            FrmEditCode.pBl7 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[11].Value.ToString();
            FrmEditCode.pBl8 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[12].Value.ToString();
            FrmEditCode.pBl9 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[13].Value.ToString();
            FrmEditCode.pBl10 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[14].Value.ToString();
            FrmEditCode.pTy = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[17].Value.ToString();
            FrmEditCode.ShowUniqueForm(true);
            if (FrmEditCode.ifSave)
            {

                OleDbDataAdapter sda = new OleDbDataAdapter(ResultSelect, CommFunction.ConnectString);
                DataSet ds = new DataSet();
                sda.Fill(ds, "chk_con2");

                dataGridView1.DataSource = ds.Tables["chk_con2"];
                dataGridView1.AutoResizeColumns();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            修改ToolStripMenuItem_Click(null,null);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
           
            if (dataGridView1.Rows.Count <= 0) return;
            FrmEditCode.falg = "Edit";
            FrmEditCode.pLx = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
            FrmEditCode.pDm = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString();
            FrmEditCode.pMc = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString();
            FrmEditCode.pPym = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString();
            FrmEditCode.pBz = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value.ToString();
            FrmEditCode.pBl1 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[5].Value.ToString();
            FrmEditCode.pBl2 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[6].Value.ToString();
            FrmEditCode.pBl3 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[7].Value.ToString();
            FrmEditCode.pBl4 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[8].Value.ToString();
            FrmEditCode.pBl5 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[9].Value.ToString();
            FrmEditCode.pBl6 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[10].Value.ToString();
            FrmEditCode.pBl7 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[11].Value.ToString();
            FrmEditCode.pBl8 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[12].Value.ToString();
            FrmEditCode.pBl9 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[13].Value.ToString();
            FrmEditCode.pBl10 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[14].Value.ToString();
            FrmEditCode.pTy = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[17].Value.ToString();
            FrmEditCode.ShowUniqueForm(true);
            if (FrmEditCode.ifSave)
            {

                OleDbDataAdapter sda = new OleDbDataAdapter(ResultSelect, CommFunction.ConnectString);
                DataSet ds = new DataSet();
                sda.Fill(ds, "chk_con2");

                dataGridView1.DataSource = ds.Tables["chk_con2"];
                dataGridView1.AutoResizeColumns();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            if (dataGridView1.Rows.Count <= 0) return;
            if (MessageBox.Show("确定要删除该条记录吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.No) return;
            LYFunctionCs.LYFunctionCsClass.cmd("delete  from ysfs where type='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() +
                                               "' and ysid='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString() + "'", CommFunction.ConnectString);
            MessageBox.Show("删除成功！");

            OleDbDataAdapter sda = new OleDbDataAdapter(ResultSelect, CommFunction.ConnectString);
            DataSet ds = new DataSet();
            sda.Fill(ds, "chk_con2");

            dataGridView1.DataSource = ds.Tables["chk_con2"];
            dataGridView1.AutoResizeColumns();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            FrmEditCode.falg = "Add";
            FrmEditCode.ShowUniqueForm(true);
            if (FrmEditCode.ifSave)
            {
                string sql = "select type as 类型,ysid as 代码,ysfs as 名称,pym as 拼音码,memo as 备注,reserve1 as 保留字段1,reserve2 as 保留字段2,reserve3 as 保留字段3," +
                             "reserve4 as 保留字段4,reserve5 as 保留字段5,reserve6 as 保留字段6,reserve7 as 保留字段7,reserve8 as 保留字段8," +
                             "reserve9 as 保留字段9,reserve10 as 保留字段10,create_date_time as 创建时间,user_name as 操作员,flag as 是否停用 " +
                            "from ysfs where 1=1 and type in (" + FrmEditCode.ResultCode_type + "') and ysid in (" + FrmEditCode.ResultCode_id + "') order by type,ysid";

                OleDbDataAdapter sda = new OleDbDataAdapter(sql, CommFunction.ConnectString);
                DataSet ds = new DataSet();
                sda.Fill(ds, "chk_con2");

                dataGridView1.DataSource = ds.Tables["chk_con2"];
                dataGridView1.AutoResizeColumns();
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() == "职员")
            {
                // 把第6列显示*号，*号的个数和实际数据的长度相同 
                if ((e.ColumnIndex == 5))
                {
                    if (e.Value != null && e.Value.ToString().Length > 0)
                    {
                        e.Value = new string('*', e.Value.ToString().Length);
                    }
                }
            }
        }

        private void FrmCommCode_Load(object sender, EventArgs e)
        {
            string strLx = "select distinct type from ysfs where nvl(flag,'!@#$%')!='停用'";
            OleDbDataAdapter sda = new OleDbDataAdapter(strLx, CommFunction.ConnectString);
            DataTable LxTable = new DataTable();
            sda.Fill(LxTable);
            for (int i = 0; i < LxTable.Rows.Count; i++)
            {
                toolStripComboBox1.Items.Add(LxTable.Rows[i][0]);
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            string sql = "select type as 类型,ysid as 代码,ysfs as 名称,pym as 拼音码,memo as 备注,reserve1 as 保留字段1,reserve2 as 保留字段2,reserve3 as 保留字段3," +
                             "reserve4 as 保留字段4,reserve5 as 保留字段5,reserve6 as 保留字段6,reserve7 as 保留字段7,reserve8 as 保留字段8," +
                             "reserve9 as 保留字段9,reserve10 as 保留字段10,create_date_time as 创建时间,user_name as 操作员,flag as 是否停用 " +
                            "from ysfs where 1=1 and type like '%" + toolStripComboBox1.Text + "%' order by type,ysid";

            OleDbDataAdapter sda = new OleDbDataAdapter(sql, CommFunction.ConnectString);
            DataSet ds = new DataSet();
            sda.Fill(ds, "chk_con2");

            dataGridView1.DataSource = ds.Tables["chk_con2"];
            dataGridView1.AutoResizeColumns();
        }
    }
}
