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
    public partial class FrmColdCheck : Form
    {
        private static FrmColdCheck pUniqueForm = null;//窗体唯一打开代码

        /// <summary>
        /// 窗体唯一打开代码
        /// </summary>
        /// <param name="Path">是否作为模式窗体打开</param>
        public static void ShowUniqueForm(bool AIfShowModal)
        {
            if (pUniqueForm == null)
            {
                new FrmColdCheck();

                if (AIfShowModal) pUniqueForm.ShowDialog(); else pUniqueForm.Show();
            }
        }

        public FrmColdCheck()
        {
            pUniqueForm = this;//窗体唯一打开代码
            InitializeComponent();
        }

        private void FrmColdCheck_FormClosed(object sender, FormClosedEventArgs e)
        {
            pUniqueForm = null;//窗体唯一打开代码
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmColdCheck_Load(object sender, EventArgs e)
        {
            dtpFhqi.Value = System.DateTime.Now;
            dtpBzri.Value = System.DateTime.Now;
            dtpThri.Value = System.DateTime.Now;
            
            OleDbConnection cn = new OleDbConnection(CommFunction.ConnectString);
            cn.Open();

            string strR = "select ysfs from ysfs WHERE TYPE='冷链复核人' and nvl(flag,'!@#$%')!='停用'";
            OleDbDataAdapter sda = new OleDbDataAdapter(strR, cn);
            DataSet RYTable = new DataSet();
            sda.Fill(RYTable);
            for (int i = 0; i < RYTable.Tables[0].Rows.Count; i++)
            {
                cbFhr.Items.Add(RYTable.Tables[0].Rows[i][0]);
                cbBzr.Items.Add(RYTable.Tables[0].Rows[i][0]);
            }

            //承运商没有视图
            strR = "select name from V_CARR";
            sda = new OleDbDataAdapter(strR, cn);
            RYTable = new DataSet();
            sda.Fill(RYTable);
            for (int i = 0; i < RYTable.Tables[0].Rows.Count; i++)
            {
                cbCys.Items.Add(RYTable.Tables[0].Rows[i][0]);
            }


            string SqlSeach = "select z.unid as 跟踪表单号,c.assort_nbr as 发货单号,c.sku_desc as 品名规格," +
                                "c.batch_nbr as 商品批号,c.orig_pkt_qty as 商品数量,c.units as 单位,z.season as 货主," +
                                "z.shipto_name as　客户名称,z.create_date_time as 发货时间,z.user_name as 发货人,z.cur_temp_src as 发货天气温度 " +
                                "from CC_COLD_CHAIN_Z z " +
                                "left join CC_COLD_CHAIN_C c on z.unid=c.pkunid " +
                                "where z.unid like '%" + tbGzbd.Text + "%' and z.check_date_time is null " +
                                "order by z.create_date_time desc ";

            OleDbDataAdapter adapater = new OleDbDataAdapter(SqlSeach, cn);
            DataTable table = new DataTable();
            adapater.Fill(table);
            dataGridView1.DataSource = table.DefaultView;
            dataGridView1.AutoResizeColumns();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection cn = new OleDbConnection(CommFunction.ConnectString);
            cn.Open();
            string SqlSeach="";
            if(checkBox1.Checked)
                     SqlSeach = "select z.unid as 跟踪表单号,c.assort_nbr as 发货单号,c.sku_desc as 品名规格," +
                               "c.batch_nbr as 商品批号,c.orig_pkt_qty as 商品数量,c.units as 单位,z.season as 货主," +
                               "z.shipto_name as　客户名称,z.create_date_time as 发货时间,z.user_name as 发货人,z.cur_temp_src as 发货天气温度 " +
                               "from CC_COLD_CHAIN_Z z " +
                               "left join CC_COLD_CHAIN_C c on z.unid=c.pkunid " +
                               "where z.unid like '%" + tbGzbd.Text + "%' "+
 //查询全部才能进行撤销        "and z.check_date_time is null " +
                               "order by z.create_date_time desc ";
            else
                SqlSeach = "select z.unid as 跟踪表单号,c.assort_nbr as 发货单号,c.sku_desc as 品名规格," +
                               "c.batch_nbr as 商品批号,c.orig_pkt_qty as 商品数量,c.units as 单位,z.season as 货主," +
                               "z.shipto_name as　客户名称,z.create_date_time as 发货时间,z.user_name as 发货人,z.cur_temp_src as 发货天气温度 " +
                               "from CC_COLD_CHAIN_Z z " +
                               "left join CC_COLD_CHAIN_C c on z.unid=c.pkunid " +
                               "where z.unid like '%" + tbGzbd.Text + "%' " +
                           "and z.check_date_time is null " +
                               "order by z.create_date_time desc ";

            OleDbDataAdapter adapater = new OleDbDataAdapter(SqlSeach, cn);
            DataTable table = new DataTable();
            adapater.Fill(table);
            dataGridView1.DataSource = table.DefaultView;
            dataGridView1.AutoResizeColumns();
        }

        private void FrmColdCheck_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                SelectNextControl(ActiveControl, true, true, true, true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;

            if ((tbGzbd.Text == "")||
                (dtpFhsj.Text.Trim() == "") || //(tbFhsj2.Text.Trim() == "") ||
                (dtpBzsj.Text.Trim() == "") )//|| (tbBzsj2.Text.Trim() == ""))
                MessageBox.Show("请输完基本信息");
            else
            {

                OleDbConnection cn = new OleDbConnection(CommFunction.ConnectString);
                cn.Open();

                string gg = dtpBzri.Value.ToString("yyyy-MM-dd");

                //gg = gg + " ";
                //gg = gg + tbBzsj.Text;
                //gg = gg + ":";
                //gg = gg + tbBzsj2.Text;
                //gg = gg + ":00";
                gg = gg + " " + dtpBzsj.Text;


                string hh = dtpFhqi.Value.ToString("yyyy-MM-dd");
                //hh = hh + " ";
                //hh = hh + tbFhsj.Text;
                //hh = hh + ":";
                //hh = hh + tbFhsj2.Text;
                //hh = hh + ":00";
                hh = hh + " "+dtpFhsj.Text;


                string II = dtpThri.Value.ToString("yyyy-MM-dd");
                //II = II + " ";
                //II = II + tbThsj.Text;
                //II = II + ":";
                //II = II + tbThsj2.Text;
                //II = II + ":00";
                II = II + " " + dtpThsj.Text;
                string SqlSeach;
                OleDbCommand command1;

                SqlSeach = "update CC_COLD_CHAIN_Z set USER_CHECK='" + cbFhr.Text + "',USER_PACK='" + cbBzr.Text + "',CARR_NAME='" + cbCys.Text + "',PACK_DATE_TIME=to_date('" + gg + "','yyyy-mm-dd hh24:mi:ss'),USER_CARR='" + tbThr.Text + "',CARR_DATE_TIME=to_date('" + II + "','yyyy-mm-dd hh24:mi:ss'), CHECK_DATE_TIME=to_date('" + hh + "','yyyy-mm-dd hh24:mi:ss') WHERE UNID='" + tbGzbd.Text + "'";
                command1 = new OleDbCommand(SqlSeach, cn);
                command1.ExecuteNonQuery();

                tbGzbd.Text = null;
                cbFhr.Text = null;
                cbBzr.Text = null;
                cbCys.Text = null;
                //tbBzsj.Text = null;
                //tbBzsj2.Text = null;
                dtpBzsj.Text = DateTime.Now.ToString();
                tbThr.Text = null;
                //tbThsj.Text = null;
                //tbThsj2.Text = null;
                dtpThsj.Text = DateTime.Now.ToString();
                //tbFhsj.Text = null;
                //tbFhsj2.Text = null;
                dtpFhsj.Text = DateTime.Now.ToString();
                SqlSeach = "select z.unid as 跟踪表单号,c.assort_nbr as 发货单号,c.sku_desc as 品名规格," +
                           "c.batch_nbr as 商品批号,c.orig_pkt_qty as 商品数量,c.units as 单位,z.season as 货主," +
                           "z.shipto_name as　客户名称,z.create_date_time as 发货时间,z.user_name as 发货人,z.cur_temp_src as 发货天气温度 " +
                           "from CC_COLD_CHAIN_Z z " +
                           "left join CC_COLD_CHAIN_C c on z.unid=c.pkunid " +
                           "where z.unid like '%" + tbGzbd.Text + "%' and z.check_date_time is null " +
                           "order by z.create_date_time desc ";
                OleDbDataAdapter adapater = new OleDbDataAdapter(SqlSeach, cn);
                DataTable table = new DataTable();
                adapater.Fill(table);
                dataGridView1.DataSource = table.DefaultView;
                dataGridView1.AutoResizeColumns();


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;

            if (tbGzbd.Text == "")
                MessageBox.Show("请输完基本信息");
            else
            {
                OleDbConnection cn = new OleDbConnection(CommFunction.ConnectString);
                cn.Open();

                string ff = null;

                string SqlSeach = "update CC_COLD_CHAIN_Z set USER_CHECK='',USER_PACK='',CARR_NAME='',PACK_DATE_TIME=to_date('" + ff + "','yyyy-mm-dd hh24:mi:ss'),USER_CARR='',CARR_DATE_TIME=to_date('" + ff + "','yyyy-mm-dd hh24:mi:ss'), CHECK_DATE_TIME=to_date('" + ff + "','yyyy-mm-dd hh24:mi:ss')  WHERE UNID='" + tbGzbd.Text + "'";
                OleDbCommand command1 = new OleDbCommand(SqlSeach, cn);
                command1.ExecuteNonQuery();

                tbGzbd.Text = null;


                SqlSeach = "select z.unid as 跟踪表单号,c.assort_nbr as 发货单号,c.sku_desc as 品名规格," +
                           "c.batch_nbr as 商品批号,c.orig_pkt_qty as 商品数量,c.units as 单位,z.season as 货主," +
                           "z.shipto_name as　客户名称,z.create_date_time as 发货时间,z.user_name as 发货人,z.cur_temp_src as 发货天气温度 " +
                           "from CC_COLD_CHAIN_Z z " +
                           "left join CC_COLD_CHAIN_C c on z.unid=c.pkunid " +
                           "where z.unid like '%" + tbGzbd.Text + "%' and z.check_date_time is null " +
                           "order by z.create_date_time desc ";

                OleDbDataAdapter adapater = new OleDbDataAdapter(SqlSeach, cn);
                DataTable table = new DataTable();
                adapater.Fill(table);
                dataGridView1.DataSource = table.DefaultView;
                dataGridView1.AutoResizeColumns();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            tbGzbd.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["跟踪表单号"].Value.ToString();
        }

    }
}
