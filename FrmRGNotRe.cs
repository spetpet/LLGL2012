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
    public partial class FrmRGNotRe : Form
    {
        OleDbConnection cn;

        private static FrmRGNotRe pUniqueForm = null;//窗体唯一打开代码

        /// <summary>
        /// 窗体唯一打开代码
        /// </summary>
        /// <param name="Path">是否作为模式窗体打开</param>
        public static void ShowUniqueForm(bool AIfShowModal)
        {
            if (pUniqueForm == null)
            {
                new FrmRGNotRe();

                if (AIfShowModal) pUniqueForm.ShowDialog(); else pUniqueForm.Show();
            }
        }

        public FrmRGNotRe()
        {
            pUniqueForm = this;//窗体唯一打开代码
            InitializeComponent();
        }

        private void FrmRGNotRe_FormClosed(object sender, FormClosedEventArgs e)
        {
            pUniqueForm = null;//窗体唯一打开代码
            cn.Close();
            cn.Dispose();
        }

        private void FrmRGNotRe_Load(object sender, EventArgs e)
        {
            cn = new OleDbConnection(CommFunction.ConnectString);
            cn.Open();

            string searchtemp = "select unid as 跟踪表单号,shipto_name as 客户名称,create_date_time as 发货时间,season  as 货主 from (select row_number() over(partition by t.therm_id order by z.unid desc) rn,z.unid,z.shipto_name,z.create_date_time,z.season " +
                                "from cc_cold_chain_z z "+
                                "left join cc_cold_chain_c cc on cc.pkunid=z.unid " +
                                "left join cc_cold_chain_therm t on t.pkunid=cc.unid "+
                                "left join ysfs c on c.ysid=t.therm_id "+
                                // "where z.sign_date_time is not null and sysdate-z.create_date_time>=7 and "+

                                "where c.reserve1='已发' and c.type='温度计字典') where rn=1 and sysdate-create_date_time>=7 " +
                                //"and rownum=1 "+
                                "order by unid ";


            OleDbDataAdapter adapater = new OleDbDataAdapter(searchtemp, cn);
            DataTable table = new DataTable();
            adapater.Fill(table);
            dataGridView1.DataSource = table.DefaultView;
            dataGridView1.AutoResizeColumns();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            if ((sender as DataGridView).CurrentRow == null) return;//如果无此句,则点击dataGridView的列标题时会出错

            string select_gzbdh = (sender as DataGridView).CurrentRow.Cells[0].Value.ToString();
            string searchtemp = "select t.therm_id as 温度计编号,c.sku_desc as 品名规格,c.batch_nbr as 批号,c.orig_pkt_qty as 数量,c.units as 单位 " +
                                "from cc_cold_chain_c c " +
                                "left join cc_cold_chain_therm t on c.unid=t.pkunid " +
                                "left join ysfs cc on cc.ysid=t.therm_id " +
                                "where cc.RESERVE1='已发' and cc.type='温度计字典' and c.pkunid='" + select_gzbdh + "'";

            OleDbDataAdapter adapater = new OleDbDataAdapter(searchtemp, cn);
            DataTable table = new DataTable();
            adapater.Fill(table);
            dataGridView2.DataSource = table.DefaultView;
            dataGridView2.AutoResizeColumns();
        }
    }
}
