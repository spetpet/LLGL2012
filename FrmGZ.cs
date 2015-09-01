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
    public partial class FrmGZ : Form
    {
        private static FrmGZ pUniqueForm = null;//窗体唯一打开代码

        /// <summary>
        /// 窗体唯一打开代码
        /// </summary>
        /// <param name="Path">是否作为模式窗体打开</param>
        public static void ShowUniqueForm(bool AIfShowModal)
        {
            if (pUniqueForm == null)
            {
                new FrmGZ();

                if (AIfShowModal) pUniqueForm.ShowDialog(); else pUniqueForm.Show();
            }
        }


        public FrmGZ()
        {
            pUniqueForm = this;//窗体唯一打开代码
            InitializeComponent();
        }

        private void FrmGZ_FormClosed(object sender, FormClosedEventArgs e)
        {
            pUniqueForm = null;//窗体唯一打开代码
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmGZ_Load(object sender, EventArgs e)
        {
            dtpShri.Value = System.DateTime.Now;
            OleDbConnection cn = new OleDbConnection(CommFunction.ConnectString);
            cn.Open();
            string SqlSeach;
            //SqlSeach = "select z.unid as 跟踪表单号,c.assort_nbr as 发货单号,c.sku_desc as 品名规格," +
            //                    "c.batch_nbr as 商品批号,c.orig_pkt_qty as 商品数量,c.units as 单位,z.season as 货主," +
            //                    "z.shipto_name as　客户名称,z.create_date_time as 发货时间,z.user_name as 发货人,z.cur_temp_src as 发货天气温度 " +
            //                    "from CC_COLD_CHAIN_Z z " +
            //                    "left join CC_COLD_CHAIN_C c on z.unid=c.pkunid " +
            //                    "where z.unid like '%" + tbGzbd.Text + "%' and z.check_date_time is not null  and z.sign_date_time is null " +
            //                    "order by z.create_date_time desc ";
            //要求不复核也可以确认收货
            SqlSeach = "select z.unid as 跟踪表单号,c.assort_nbr as 发货单号,c.sku_desc as 品名规格," +
                                "c.batch_nbr as 商品批号,c.orig_pkt_qty as 商品数量,c.units as 单位,z.season as 货主," +
                                "z.shipto_name as　客户名称,z.create_date_time as 发货时间,z.user_name as 发货人,z.cur_temp_src as 发货天气温度 " +
                                "from CC_COLD_CHAIN_Z z " +
                                "left join CC_COLD_CHAIN_C c on z.unid=c.pkunid " +
                                "where z.unid like '%" + tbGzbd.Text + "%' and z.sign_date_time is null " +
                                "order by z.create_date_time desc ";

            OleDbDataAdapter adapater = new OleDbDataAdapter(SqlSeach, cn);
            DataTable table = new DataTable();
            adapater.Fill(table);
            dataGridView1.DataSource = table.DefaultView;
            dataGridView1.AutoResizeColumns();
         
        }

        private void FrmGZ_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                SelectNextControl(ActiveControl, true, true, true, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection cn = new OleDbConnection(CommFunction.ConnectString);
            cn.Open();
            string SqlSeach;
            //SqlSeach = "select z.unid as 跟踪表单号,c.assort_nbr as 发货单号,c.sku_desc as 品名规格," +
            //                    "c.batch_nbr as 商品批号,c.orig_pkt_qty as 商品数量,c.units as 单位,z.season as 货主," +
            //                    "z.shipto_name as　客户名称,z.create_date_time as 发货时间,z.user_name as 发货人,z.cur_temp_src as 发货天气温度 " +
            //                    "from CC_COLD_CHAIN_Z z " +
            //                    "left join CC_COLD_CHAIN_C c on z.unid=c.pkunid " +
            //                    "where z.unid like '%" + tbGzbd.Text + "%' and z.check_date_time is not null  and z.sign_date_time is null " +
            //                    "order by z.create_date_time desc "; OleDbDataAdapter adapater = new OleDbDataAdapter(SqlSeach, cn);

            //要求不复核也可以确认收货
            if(checkBox1.Checked)
                SqlSeach = "select z.unid as 跟踪表单号,c.assort_nbr as 发货单号,c.sku_desc as 品名规格," +
                                "c.batch_nbr as 商品批号,c.orig_pkt_qty as 商品数量,c.units as 单位,z.season as 货主," +
                                "z.shipto_name as　客户名称,z.create_date_time as 发货时间,z.user_name as 发货人,z.cur_temp_src as 发货天气温度 " +
                                "from CC_COLD_CHAIN_Z z " +
                                "left join CC_COLD_CHAIN_C c on z.unid=c.pkunid " +
                                "where z.unid like '%" + tbGzbd.Text + "%' "+
             //                   "and z.sign_date_time is null " +
                                "order by z.create_date_time desc ";
            else
                SqlSeach = "select z.unid as 跟踪表单号,c.assort_nbr as 发货单号,c.sku_desc as 品名规格," +
                                "c.batch_nbr as 商品批号,c.orig_pkt_qty as 商品数量,c.units as 单位,z.season as 货主," +
                                "z.shipto_name as　客户名称,z.create_date_time as 发货时间,z.user_name as 发货人,z.cur_temp_src as 发货天气温度 " +
                                "from CC_COLD_CHAIN_Z z " +
                                "left join CC_COLD_CHAIN_C c on z.unid=c.pkunid " +
                                "where z.unid like '%" + tbGzbd.Text + "%' " +
                                "and z.sign_date_time is null " +
                                "order by z.create_date_time desc ";

            OleDbDataAdapter adapater = new OleDbDataAdapter(SqlSeach, cn);

            DataTable table = new DataTable();
            adapater.Fill(table);
            dataGridView1.DataSource = table.DefaultView;
            dataGridView1.AutoResizeColumns();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            if ((tbGzbd.Text == "")||(tbShsj.Text.Trim()=="")||(tbShsj2.Text.Trim()==""))//不校验收货时间
                MessageBox.Show("请输完基本信息");
            else
            {
                OleDbConnection cn = new OleDbConnection(CommFunction.ConnectString);
                cn.Open();
                string ff = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string gg = dtpShri.Value.ToString("yyyy-MM-dd");
                gg = gg + " ";
                gg = gg + tbShsj.Text;
                gg = gg + ":";
                gg = gg + tbShsj2.Text;
                gg = gg + ":00";

                string SqlSeach = "update CC_COLD_CHAIN_Z set SIGN_TEMP_ENVI='" + tbHjwd.Text + "',SIGN_TEMP_DRUGS='" + tbYpwd.Text + "',SIGN_DATE_TIME=to_date('" + gg + "','yyyy-mm-dd hh24:mi:ss'),USER_SIGN='" + tbShr.Text + "',SIGN_MEMO='" + tbBz.Text + "' WHERE UNID='" + tbGzbd.Text + "'";
                OleDbCommand command1 = new OleDbCommand(SqlSeach, cn);
                command1.ExecuteNonQuery();
                tbGzbd.Text = null;
                tbHjwd.Text = null;
                tbYpwd.Text = null;
                dtpShri.Text = null;
                tbShsj.Text = null;
                tbShsj2.Text = null;
                tbShr.Text = null;
                tbBz.Text = null;

                //SqlSeach = "select z.unid as 跟踪表单号,c.assort_nbr as 发货单号,c.sku_desc as 品名规格," +
                //                "c.batch_nbr as 商品批号,c.orig_pkt_qty as 商品数量,c.units as 单位,z.season as 货主," +
                //                "z.shipto_name as　客户名称,z.create_date_time as 发货时间,z.user_name as 发货人,z.cur_temp_src as 发货天气温度 " +
                //                "from CC_COLD_CHAIN_Z z " +
                //                "left join CC_COLD_CHAIN_C c on z.unid=c.pkunid " +
                //                "where z.unid like '%" + tbGzbd.Text + "%' and z.check_date_time is not null  and z.sign_date_time is null " +
                //                "order by z.create_date_time desc ";
                //要求不复核也可以确认收货
                SqlSeach = "select z.unid as 跟踪表单号,c.assort_nbr as 发货单号,c.sku_desc as 品名规格," +
                                "c.batch_nbr as 商品批号,c.orig_pkt_qty as 商品数量,c.units as 单位,z.season as 货主," +
                                "z.shipto_name as　客户名称,z.create_date_time as 发货时间,z.user_name as 发货人,z.cur_temp_src as 发货天气温度 " +
                                "from CC_COLD_CHAIN_Z z " +
                                "left join CC_COLD_CHAIN_C c on z.unid=c.pkunid " +
                                "where z.unid like '%" + tbGzbd.Text + "%' and z.sign_date_time is null " +
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

                string SqlSeach = "update CC_COLD_CHAIN_Z set SIGN_TEMP_ENVI='',SIGN_TEMP_DRUGS='',SIGN_DATE_TIME=to_date('" + ff + "','yyyy-mm-dd hh24:mi:ss'),USER_SIGN='',SIGN_MEMO='' WHERE UNID='" + tbGzbd.Text + "'";
                OleDbCommand command1 = new OleDbCommand(SqlSeach, cn);
                command1.ExecuteNonQuery();

                tbGzbd.Text = null;

                //SqlSeach = "select z.unid as 跟踪表单号,c.assort_nbr as 发货单号,c.sku_desc as 品名规格," +
                //                "c.batch_nbr as 商品批号,c.orig_pkt_qty as 商品数量,c.units as 单位,z.season as 货主," +
                //                "z.shipto_name as　客户名称,z.create_date_time as 发货时间,z.user_name as 发货人,z.cur_temp_src as 发货天气温度 " +
                //                "from CC_COLD_CHAIN_Z z " +
                //                "left join CC_COLD_CHAIN_C c on z.unid=c.pkunid " +
                //                "where z.unid like '%" + tbGzbd.Text + "%' and z.check_date_time is not null  and z.sign_date_time is null " +
                //                "order by z.create_date_time desc ";
                //要求不复核也可以确认收货

                SqlSeach = "select z.unid as 跟踪表单号,c.assort_nbr as 发货单号,c.sku_desc as 品名规格," +
                                "c.batch_nbr as 商品批号,c.orig_pkt_qty as 商品数量,c.units as 单位,z.season as 货主," +
                                "z.shipto_name as　客户名称,z.create_date_time as 发货时间,z.user_name as 发货人,z.cur_temp_src as 发货天气温度 " +
                                "from CC_COLD_CHAIN_Z z " +
                                "left join CC_COLD_CHAIN_C c on z.unid=c.pkunid " +
                                "where z.unid like '%" + tbGzbd.Text + "%' and z.sign_date_time is null " +
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
