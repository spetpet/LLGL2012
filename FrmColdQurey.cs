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
    public partial class FrmColdQurey : Form
    {
        private static FrmColdQurey pUniqueForm = null;//窗体唯一打开代码

        /// <summary>
        /// 窗体唯一打开代码
        /// </summary>
        /// <param name="Path">是否作为模式窗体打开</param>
        public static void ShowUniqueForm(bool AIfShowModal)
        {
            if (pUniqueForm == null)
            {
                new FrmColdQurey();

                if (AIfShowModal) pUniqueForm.ShowDialog(); else pUniqueForm.Show();
            }
        }

        public FrmColdQurey()
        {
            pUniqueForm = this;//窗体唯一打开代码
            InitializeComponent();
        }

        private void FrmColdQurey_FormClosed(object sender, FormClosedEventArgs e)
        {
            pUniqueForm = null;//窗体唯一打开代码
        }

        private void FrmColdQurey_Load(object sender, EventArgs e)
        {
            OleDbConnection cn = new OleDbConnection(CommFunction.ConnectString);
            cn.Open();
            string strpmgg = "select sku_desc from cc_item_master t where t.cc_flag='冷链' ";
            OleDbDataAdapter sda = new OleDbDataAdapter(strpmgg, cn);
            DataSet pTable = new DataSet();
            sda.Fill(pTable);
            for (int i = 0; i < pTable.Tables[0].Rows.Count; i++)
            {
                PMGG.Items.Add(pTable.Tables[0].Rows[i][0]);
            }

            string SqlSeachHZ = "select company from v_company";
            OleDbDataAdapter adapaterHZ = new OleDbDataAdapter(SqlSeachHZ, cn);
            DataTable HZtable = new DataTable();
            adapaterHZ.Fill(HZtable);
            for (int i = 0; i < HZtable.Rows.Count; i++) HZ.Items.Add(HZtable.Rows[i][0]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            OleDbConnection cn = new OleDbConnection(CommFunction.ConnectString);
            cn.Open();
            string d1 = FHSJ1.Value.ToString("yyyy-MM-dd 00:00:00");
            string d2 = FHSJ2.Value.ToString("yyyy-MM-dd 23:59:59");
            string spzt;
            if ((SPZT.Text.Trim() == "") || (SPZT.Text == null)) { spzt = ""; } 
            else if (SPZT.Text.Trim() == "已发货未复核确认") { spzt = " and (create_date_time is not null and check_date_time is null)"; }
            else if (SPZT.Text.Trim() == "已复核确认未收货") { spzt = " and (check_date_time is not null and sign_date_time is null )"; }
            else if (SPZT.Text.Trim() == "已收货") { spzt = " and sign_date_time is not null"; }
            else spzt="";
            string ywrg;
            if ((YWRG.Text.Trim() == "") || (YWRG.Text == null)) { ywrg = ""; }
            else if (YWRG.Text.Trim() == "有") { ywrg = " AND t.therm_id  is not null "; }
            else if (YWRG.Text.Trim() == "无") { ywrg = " AND t.therm_id  is null "; }
            else ywrg = "";
            string pmgg;
            if ((PMGG.Text.Trim() == "") || (PMGG.Text == null)) { pmgg = ""; } else { pmgg = " and c.sku_desc like'%" + PMGG.Text + "%'"; }
            string khmc;
            if ((KHMC.Text.Trim() == "") || (KHMC.Text == null)) { khmc = ""; } else { khmc = " AND nvl(z.shipto_name,'*') like '%" + KHMC.Text + "%'"; }
            string gzbdh;
            if ((GZBDH.Text.Trim() == "") || (GZBDH.Text == null)) { gzbdh = ""; } else { gzbdh = " AND  z.unid like '%" + GZBDH.Text + "%'"; }
            string wdjbh;
            if ((WDJBH.Text.Trim() == "") || (WDJBH.Text == null)) { wdjbh = ""; } else { wdjbh = " AND  (nvl(t.therm_id ,'!@#$') like'%" + WDJBH.Text + "%' or nvl(wdmxgz.rgid,'!@#$') like '%" + WDJBH.Text + "%') "; }
            string fhdh;
            if ((FHDH.Text.Trim() == "") || (FHDH.Text == null)) { fhdh = ""; } else { fhdh = " AND  nvl(c.assort_nbr,'!@#$') like '%" + FHDH.Text + "%'"; }
            string fhsj;
            fhsj = " and z.create_date_time between (to_date('" + d1 + "','yyyy-mm-dd hh24:mi:ss')) and (to_date('" + d2 + "','yyyy-mm-dd hh24:mi:ss')) ";
            string hz;
            if ((HZ.Text.Trim() == "") || (HZ.Text == null)) { hz = ""; } else { hz = " AND  z.season= '" + HZ.Text + "'"; }

            string SqlSeach = "select z.unid as 跟踪表单号,c.assort_nbr as 发货单号,c.sku_desc as 品名规格,c.batch_nbr as 商品批号," +
                              "c.pack_tips_z as 整件冷链包装提示,c.pack_tips_l as 零散冷链包装提示,c.orig_pkt_qty as 商品数量,c.units as 单位,z.season as 货主,z.shipto_name as 客户名称," +
                              "z.create_date_time as 发货时间,z.check_date_time as 复核时间,z.pack_date_time as 包装时间,z.carr_date_time as 提货时间," +
                              "z.sign_date_time as 收货时间,z.user_name as 发货人,z.user_check as 复核人,z.user_pack as 包装人,z.user_carr as 提货人," +
                              "z.sign_memo as 收货备注,z.user_sign as 收货人,t.therm_id as 温度计编号,z.user_therm as 温度计配附人,z.cur_temp_src as 发货天气温度," +
                              "to_char(z.sign_temp_drugs,'999d9') as 收货时药品温度,to_char(z.sign_temp_envi,'999d9') as 收货时环境温度,z.carr_name as 承运商," +
                              "case " +
                              "when z.create_date_time is not null and check_date_time is null then '已发货未复核确认' " +
                              "when check_date_time is not null and sign_date_time is null then '已复核确认未收货' " +
                              "when sign_date_time is not null then '已收货' " +
                              "else '' end  as 状态," +
                              "z.max_temp_src as 出发地最高温度,z.min_temp_src as 出发地最低温度,z.max_temp_dest as 目的地最高温度,z.min_temp_dest as 目的地最低温度,t.PACK_MEMO as 温度计包装备注,t.TEMP_MEMO as 温度计温度备注 " +
                              "from cc_cold_chain_z z " +
                              "left join cc_cold_chain_c c on z.unid=c.pkunid " +
                              "left join cc_cold_chain_therm t on c.unid=t.pkunid  where 1=1 " + spzt + ywrg + pmgg + khmc + gzbdh + wdjbh + fhdh + fhsj + hz + 
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
            OleDbConnection cn = new OleDbConnection(CommFunction.ConnectString);
            cn.Open();
            string aa = null;
            string d1 = FHSJ1.Value.ToString("yyyy-MM-dd 00:00:00");
            string d2 = FHSJ2.Value.ToString("yyyy-MM-dd 23:59:59");
            string spzt;
            if ((SPZT.Text.Trim() == "") || (SPZT.Text == null)) { spzt = ""; }
            else if (SPZT.Text.Trim() == "已发货未复核确认") { spzt = " and (create_date_time is not null and check_date_time is null)"; }
            else if (SPZT.Text.Trim() == "已复核确认未收货") { spzt = " and (check_date_time is not null and sign_date_time is null )"; }
            else if (SPZT.Text.Trim() == "已收货") { spzt = " and sign_date_time is not null"; }
            else spzt = "";
            string ywrg;
            if ((YWRG.Text.Trim() == "") || (YWRG.Text == null)) { ywrg = ""; }
            else if (YWRG.Text.Trim() == "有") { ywrg = " AND t.therm_id  is not null "; }
            else if (YWRG.Text.Trim() == "无") { ywrg = " AND t.therm_id  is null "; }
            else ywrg = "";
            string pmgg;
            if ((PMGG.Text.Trim() == "") || (PMGG.Text == null)) { pmgg = ""; } else { pmgg = " and c.sku_desc like'%" + PMGG.Text + "%'"; }
            string khmc;
            if ((KHMC.Text.Trim() == "") || (KHMC.Text == null)) { khmc = ""; } else { khmc = " AND nvl(z.shipto_name,'*') like '%" + KHMC.Text + "%'"; }
            string gzbdh;
            if ((GZBDH.Text.Trim() == "") || (GZBDH.Text == null)) { gzbdh = ""; } else { gzbdh = " AND  z.unid like '%" + GZBDH.Text + "%'"; }
            string wdjbh;
            if ((WDJBH.Text.Trim() == "") || (WDJBH.Text == null)) { wdjbh = ""; } else { wdjbh = " AND  (nvl(t.therm_id ,'!@#$') like'%" + WDJBH.Text + "%' or nvl(wdmxgz.rgid,'!@#$') like '%" + WDJBH.Text + "%') "; }
            string fhdh;
            if ((FHDH.Text.Trim() == "") || (FHDH.Text == null)) { fhdh = ""; } else { fhdh = " AND  nvl(c.assort_nbr,'!@#$') like '%" + FHDH.Text + "%'"; }
            string fhsj;
            fhsj = " and z.create_date_time between (to_date('" + d1 + "','yyyy-mm-dd hh24:mi:ss')) and (to_date('" + d2 + "','yyyy-mm-dd hh24:mi:ss')) ";
            string hz;
            if ((HZ.Text.Trim() == "") || (HZ.Text == null)) { hz = ""; } else { hz = " AND  z.season= '" + HZ.Text + "'"; }

            aa = "select z.unid as 跟踪表单号,c.assort_nbr as 发货单号,c.sku_desc as 品名规格,c.batch_nbr as 商品批号," +
                              "c.pack_tips_z as 整件冷链包装提示,c.pack_tips_l as 零散冷链包装提示,c.orig_pkt_qty as 商品数量,c.units as 单位,z.season as 货主,z.shipto_name as 客户名称," +
                              "z.create_date_time as 发货时间,z.check_date_time as 复核时间,z.pack_date_time as 包装时间,z.carr_date_time as 提货时间," +
                              "z.sign_date_time as 收货时间,z.user_name as 发货人,z.user_check as 复核人,z.user_pack as 包装人,z.user_carr as 提货人," +
                              "z.sign_memo as 收货备注,z.user_sign as 收货人,t.therm_id as 温度计编号,z.user_therm as 温度计配附人,z.cur_temp_src as 发货天气温度," +
                              "z.sign_temp_drugs as 收货时药品温度,z.sign_temp_envi as 收货时环境温度,z.carr_name as 承运商," +
                              "case " +
                              "when z.create_date_time is not null and check_date_time is null then '已发货未复核确认' " +
                              "when check_date_time is not null and sign_date_time is null then '已复核确认未收货' " +
                              "when sign_date_time is not null then '已收货' " +
                              "else '' end  as 状态," +
                              "z.max_temp_src as 出发地最高温度,z.min_temp_src as 出发地最低温度,z.max_temp_dest as 目的地最高温度,z.min_temp_dest as 目的地最低温度,t.PACK_MEMO as 温度计包装备注,t.TEMP_MEMO as 温度计温度备注 " +
                              "from cc_cold_chain_z z " +
                              "left join cc_cold_chain_c c on z.unid=c.pkunid " +
                              "left join cc_cold_chain_therm t on c.unid=t.pkunid  where 1=1 " + spzt + ywrg + pmgg + khmc + gzbdh + wdjbh + fhdh + fhsj + hz +
                              "order by z.create_date_time desc ";
            OleDbDataAdapter adapater = new OleDbDataAdapter(aa, cn);
            DataTable table = new DataTable();
            adapater.Fill(table);
            if (table.Rows.Count == 0)
            {
                MessageBox.Show("查询不到记录");
                return;
            }
            CommFunction.LYData2Excel(this.Handle, CommFunction.ConnectString, aa, "");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            OleDbConnection cn = new OleDbConnection(CommFunction.ConnectString);
            cn.Open();
            string d1 = SJ1.Value.ToString("yyyy-MM-dd 00:00:00");
            string d2 = SJ2.Value.ToString("yyyy-MM-dd 23:59:59");


            string SqlSeach = "select c.pkunid as 跟踪表单号,c.sku_desc as 品名规格,c.batch_nbr  as 商品批号,c.orig_pkt_qty  as 商品数量,t.therm_id as 温度计编号,t.BACK_TIME as 温度计返回时间,t.FILE_PATH as 温度跟踪记录文件路径,t.PACK_MEMO as 温度计包装备注,t.TEMP_MEMO as 温度计温度备注,t.IF_BAD 温度计是否损坏 " +
                              "from cc_cold_chain_c c left join cc_cold_chain_therm t on c.unid=t.pkunid " +
                              "where c.pkunid like '%" + gzbdhrg.Text + "%' and t.BACK_TIME between (to_date('" + d1 + "','yyyy-mm-dd hh24:mi:ss')) " +
                              "and (to_date('" + d2 + "','yyyy-mm-dd hh24:mi:ss')) order by c.size_desc ";
            OleDbDataAdapter adapater = new OleDbDataAdapter(SqlSeach, cn);
            DataTable table = new DataTable();
            adapater.Fill(table);
            dataGridView6.DataSource = table.DefaultView;
            dataGridView6.AutoResizeColumns();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;

            OleDbConnection cn = new OleDbConnection(CommFunction.ConnectString);
            cn.Open();
            string aa = null;
            string d1 = SJ1.Value.ToString("yyyy-MM-dd 00:00:00");
            string d2 = SJ2.Value.ToString("yyyy-MM-dd 23:59:59");
            aa = "select c.pkunid as 跟踪表单号,c.sku_desc as 品名规格,c.batch_nbr  as 商品批号,c.orig_pkt_qty  as 商品数量,t.therm_id as 温度计编号,t.BACK_TIME as 温度计返回时间,t.FILE_PATH as 温度跟踪记录文件路径,t.PACK_MEMO as 温度计包装备注,t.TEMP_MEMO as 温度计温度备注,t.IF_BAD 温度计是否损坏 " +
                              "from cc_cold_chain_c c left join cc_cold_chain_therm t on c.unid=t.pkunid " +
                              "where c.pkunid like '%" + gzbdhrg.Text + "%' and c.create_date_time between (to_date('" + d1 + "','yyyy-mm-dd hh24:mi:ss')) " +
                              "and (to_date('" + d2 + "','yyyy-mm-dd hh24:mi:ss')) order by c.size_desc "; 
            OleDbDataAdapter adapater = new OleDbDataAdapter(aa, cn);
            DataTable table = new DataTable();
            adapater.Fill(table);
            if (table.Rows.Count == 0)
            {
                MessageBox.Show("查询不到记录");
                return;
            }
            CommFunction.LYData2Excel(this.Handle, CommFunction.ConnectString, aa, "");
        }
    }
}
