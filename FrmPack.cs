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
    public partial class FrmPack : Form
    {
        public static Boolean Ifsave = false;
        public static Boolean Ifwdj = false;
        public static string pWdjgs = "";
        public static string pName = "";
        public static string pLx = "";
        public static string pBzts = "";
        public static string pBzjs = "0";
        public static string pWdj="";

        public struct Bp_dtl
        {
            public string pIce_Id;
            public string pIce_Name;
            public string pIce_Qty;
        }

        public static Bp_dtl[] pBd;


        private static FrmPack pUniqueForm = null;//窗体唯一打开代码

        /// <summary>
        /// 窗体唯一打开代码
        /// </summary>
        /// <param name="Path">是否作为模式窗体打开</param>
        public static void ShowUniqueForm(bool AIfShowModal)
        {
            if (pUniqueForm == null)
            {
                new FrmPack();

                if (AIfShowModal) pUniqueForm.ShowDialog(); else pUniqueForm.Show();
            }
        }

        public FrmPack()
        {
            pUniqueForm = this;//窗体唯一打开代码
            InitializeComponent();
        }

        private void FrmPack_FormClosed(object sender, FormClosedEventArgs e)
        {
            pUniqueForm = null;//窗体唯一打开代码
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Ifsave = true;
            pBzts = cbBzlx.Text.Trim();
            pBzjs = tbBjs.Text.Trim();
            pWdj = tbWdj.Text.Trim();
            pBd = new Bp_dtl[lvBp.Items.Count];
            for (int i = 0; i < lvBp.Items.Count; i++)
            {
                pBd[i].pIce_Id = lvBp.Items[i].SubItems[0].Text;
                pBd[i].pIce_Name = lvBp.Items[i].SubItems[1].Text;
                pBd[i].pIce_Qty = lvBp.Items[i].SubItems[2].Text;
            }

            Close();
        }

        private void FrmPack_Load(object sender, EventArgs e)
        {
            string sBzlx = "select memo from ysfs where type='温度与包装对照表' and ysfs='" + pName + "'" + pLx;
            string stra = LYFunctionCs.LYFunctionCsClass.cmdScalar(sBzlx, CommFunction.ConnectString);
            string[] strb;
            strb = stra.Split(';');
            for (int mxsl = 0; mxsl < strb.Length; mxsl++)
            {
                cbBzlx.Items.Add(strb[mxsl].ToString());
            }

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

            if (pBd != null)
            {
                for (int ii = 0; ii < pBd.Length; ii++)
                {
                    ListViewItem lvi = new ListViewItem(pBd[ii].pIce_Id);
                    lvi.SubItems.Add(pBd[ii].pIce_Name);
                    lvi.SubItems.Add(pBd[ii].pIce_Qty);
                    lvBp.Items.Add(lvi);
                }

            }
            else { lvBp.Items.Clear(); }
            
            if (Ifwdj)
            {
                cbDxwdj.Enabled = true;
                tbWdj.Enabled = true;
                string strrg = "select ysid from ysfs where type='温度计字典' and RESERVE2='正常' and RESERVE1='在库' and RESERVE3='" + pWdjgs + "' and nvl(flag,'!@#$%')!='停用'";
                OleDbConnection con2 = new OleDbConnection(CommFunction.ConnectString);
                OleDbDataAdapter sda2 = new OleDbDataAdapter(strrg, con2);
                DataSet rTable = new DataSet();
                sda2.Fill(rTable);
                cbDxwdj.Items.Clear();
                cbDxwdj.Items.Add("");
                for (int i = 0; i < rTable.Tables[0].Rows.Count; i++)
                {
                    cbDxwdj.Items.Add(rTable.Tables[0].Rows[i][0]);
                }
                con2.Close();
                con2.Dispose();
                cbBzlx.Text = pBzts;
                tbBjs.Text = pBzjs;
                tbWdj.Text = pWdj;

            }
            else
            {
                cbDxwdj.Enabled = false;
                tbWdj.Enabled = false;
                cbDxwdj.Items.Clear();
                cbBzlx.Text = pBzts;
                tbBjs.Text = pBzjs;
                tbWdj.Text = "";
            }
        }

        private void cbDxwdj_SelectedIndexChanged(object sender, EventArgs e)
        {
            string temp;
            if (Ifwdj)
            {
                if (cbDxwdj.Text.Trim() != "")
                {
                    temp = tbWdj.Text;
                    temp = cbDxwdj.Text + ";" + temp;
                }
                else
                    temp = cbDxwdj.Text + ";";
                tbWdj.Text = temp;
            }
        }

        private void tbBjs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8)
                e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string bPssgs;
            if (cbBpssgs.Text.Trim() == "") { bPssgs = ""; } else { bPssgs = " and im.SEASON ='" + cbBpssgs.Text +"'"; }
            string bPlx;
            if (cbBplx.Text.Trim() == "") { bPlx = ""; } else { bPlx = " and im.SKU_TYPE='" + cbBplx.Text + "'"; }
            OleDbConnection cn = new OleDbConnection(CommFunction.ConnectString);
            cn.Open();
            string SqlSeach = "select  im.SIZE_DESC 冰排代码,im.SKU_DESC 品名,im.SKU_FORMAT 规格,sum(ld.qty_on_hand) 数量," +
                              "nvl((select sum(c.ice_qty) from CC_COLD_CHAIN_ICE c where c.ice_id=im.size_desc and c.if_pick='待拣'),0) 已分配数量," +
                              "sum(ld.qty_on_hand)-nvl((select sum(c.ice_qty) from CC_COLD_CHAIN_ICE c where c.ice_id=im.size_desc and c.if_pick='待拣'),0) 可用数量 " +
                              "from ICE_MASTER im,LOCN_DTL ld  " +
                              "where round(TO_NUMBER(sysdate - ld.CREATE_DATE_TIME) * 24) >= nvl(im.VALID_TIME,999999) " +
                              "and round(TO_NUMBER(sysdate - ld.CREATE_DATE_TIME) * 24)<= nvl(im.INVALID_TIME,999999) " +  bPssgs + bPlx  +
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
            tbSl.Focus();
            tbSl.SelectAll();
        }

        private void tbSl_KeyPress(object sender, KeyPressEventArgs e)
        {
            //只允许输整数   件装数自动带出 不允修改 此行无用       
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8)
                e.Handled = true;
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (tbPm.Text.Trim() == "") { MessageBox.Show("请输入完整信息 ！"); return; }
            if (int.Parse(tbSl.Text) > int.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["可用数量"].Value.ToString()))
            {
                MessageBox.Show("请输入数量大于实际库存可用数量 ！"); return;
            }

                for (int i = 0; i < lvBp.Items.Count; i++)
                { if (lvBp.Items[i].SubItems[0].Text.ToString() == dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["冰排代码"].Value.ToString()) return; }
                ListViewItem lvi = new ListViewItem(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["冰排代码"].Value.ToString());
                lvi.SubItems.Add(tbPm.Text + tbGg.Text);
               // lvi.SubItems.Add(tbGg.Text);
                lvi.SubItems.Add(tbSl.Text);
                lvBp.Items.Add(lvi);
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvBp.SelectedItems.Count == 0) return;
            lvBp.Items.Remove(lvBp.SelectedItems[0]);
        }
    }
}
