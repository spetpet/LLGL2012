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
    public partial class FrmEditIce : Form
    {
        private static FrmEditIce pUniqueForm = null;//窗体唯一打开代码
        public static string pBpdm = "";
        public static string pBppm = "";
        public static string pBpgg = "";
        public static string pBpsl = "";
        public static string pIfsave = "";
        public static string pPkunid = "";

        /// <summary>
        /// 窗体唯一打开代码
        /// </summary>
        /// <param name="Path">是否作为模式窗体打开</param>
        public static void ShowUniqueForm(bool AIfShowModal)
        {
            if (pUniqueForm == null)
            {
                new FrmEditIce();

                if (AIfShowModal) pUniqueForm.ShowDialog(); else pUniqueForm.Show();
            }
        }

        public FrmEditIce()
        {
            pUniqueForm = this;//窗体唯一打开代码
            InitializeComponent();
        }

        private void FrmEditIce_FormClosed(object sender, FormClosedEventArgs e)
        {
            pUniqueForm = null;//窗体唯一打开代码
        }

        private void FrmEditIce_Load(object sender, EventArgs e)
        {
            pIfsave = "False";
            string strBp = "select distinct season from ice_master";
            OleDbConnection con = new OleDbConnection(CommFunction.ConnectString);
            OleDbDataAdapter sda = new OleDbDataAdapter(strBp, con);
            DataSet bTable = new DataSet();
            sda.Fill(bTable);
            cbBpssgs.Items.Clear();
            cbBpssgs.Items.Add("");
            for (int i = 0; i < bTable.Tables[0].Rows.Count; i++)
            {
                cbBpssgs.Items.Add(bTable.Tables[0].Rows[i][0]);
            }
            con.Close();
            con.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string bPssgs;
            if (cbBpssgs.Text.Trim() == "") { bPssgs = ""; } else { bPssgs = " and im.SEASON ='" + cbBpssgs.Text + "'"; }
            string bPlx;
            if (cbBplx.Text.Trim() == "") { bPlx = ""; } else { bPlx = " and im.SKU_TYPE='" + cbBplx.Text + "'"; }
            OleDbConnection cn = new OleDbConnection(CommFunction.ConnectString);
            cn.Open();
            string SqlSeach = "select  im.SIZE_DESC 冰排代码,im.SKU_DESC 品名,im.SKU_FORMAT 规格,sum(ld.qty_on_hand) 数量," +
                              "nvl((select sum(c.ice_qty) from CC_COLD_CHAIN_ICE c where c.ice_id=im.size_desc and c.if_pick='待拣'),0) 已分配数量," +
                              "sum(ld.qty_on_hand)-nvl((select sum(c.ice_qty) from CC_COLD_CHAIN_ICE c where c.ice_id=im.size_desc and c.if_pick='待拣'),0) 可用数量 " +
                              "from ICE_MASTER im,LOCN_DTL ld  " +
                              "where round(TO_NUMBER(sysdate - ld.CREATE_DATE_TIME) * 24) >= nvl(im.VALID_TIME,999999) " +
                              "and round(TO_NUMBER(sysdate - ld.CREATE_DATE_TIME) * 24)<= nvl(im.INVALID_TIME,999999) and ld.size_desc=im.size_desc " + bPssgs + bPlx +
                              
                              "group by im.SIZE_DESC,im.SKU_DESC,im.SKU_FORMAT " +
                              "order by  im.SIZE_DESC ";
            OleDbDataAdapter sda = new OleDbDataAdapter(SqlSeach, CommFunction.ConnectString);
            DataSet ds = new DataSet();
            sda.Fill(ds, "chk_con");
            dataGridView1.DataSource = ds.Tables["chk_con"];
            dataGridView1.AutoResizeColumns();
            cn.Close();
            cn.Dispose();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if ((sender as DataGridView).CurrentRow == null) return;//如果无此句,则点击dataGridView的列标题时会出错
            tbPm.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["品名"].Value.ToString();
            tbGg.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["规格"].Value.ToString();
            tbSl.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["可用数量"].Value.ToString();
            pBpdm = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["冰排代码"].Value.ToString();
            pBppm = tbPm.Text;
            pBpgg = tbGg.Text;
            tbSl.Focus();
            tbSl.SelectAll();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (tbPm.Text.Trim() == "") { MessageBox.Show("请输入完整信息 ！"); return; }
            if (int.Parse(tbSl.Text) > int.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["可用数量"].Value.ToString()))
            {
                MessageBox.Show("请输入数量大于实际库存可用数量 ！"); return;
            }

            if (int.Parse(LYFunctionCs.LYFunctionCsClass.cmdScalar("select count(1) from CC_COLD_CHAIN_ICE where pkunid='" + pPkunid + "' and ice_id='"+ pBpdm +"'", CommFunction.ConnectString)) > 0)
            { MessageBox.Show("跟踪细单号 "+ pPkunid +" 已经分配了该冰块，请选择其他冰块"); return; }

            pBpsl = tbSl.Text;
            pIfsave = "True";
            Close();
           
        }

        private void tbSl_KeyPress(object sender, KeyPressEventArgs e)
        {
            //只允许输整数   件装数自动带出 不允修改 此行无用       
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8)
                e.Handled = true;
        }
    }
}
