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
    public partial class FrmSearchCcPkt : Form
    {
        public static string pPkt = "";
        public static string pifsave = "";

        private static FrmSearchCcPkt pUniqueForm = null;//窗体唯一打开代码

        /// <summary>
        /// 窗体唯一打开代码
        /// </summary>
        /// <param name="Path">是否作为模式窗体打开</param>
        public static void ShowUniqueForm(bool AIfShowModal)
        {
            if (pUniqueForm == null)
            {
                new FrmSearchCcPkt();

                if (AIfShowModal) pUniqueForm.ShowDialog(); else pUniqueForm.Show();
            }
        }


        public FrmSearchCcPkt()
        {
            pUniqueForm = this;//窗体唯一打开代码
            InitializeComponent();
        }

        private void FrmSearchCcPkt_FormClosed(object sender, FormClosedEventArgs e)
        {
            pUniqueForm = null;//窗体唯一打开代码
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmSearchCcPkt_Load(object sender, EventArgs e)
        {
            pPkt = "";
            pifsave = "False";
            string strCk = "select whse from v_whse ";
            OleDbDataAdapter sda = new OleDbDataAdapter(strCk, CommFunction.ConnectString);
            DataTable CKTable = new DataTable();
            sda.Fill(CKTable);
            for (int i = 0; i < CKTable.Rows.Count; i++)
            {
                cbCk.Items.Add(CKTable.Rows[i][0]);
            }
            
            string strHZ = "select COMPANY from v_company ";
            OleDbDataAdapter sda1 = new OleDbDataAdapter(strHZ, CommFunction.ConnectString);
            DataTable HZTable = new DataTable();
            sda1.Fill(HZTable);
            for (int i = 0; i < HZTable.Rows.Count; i++)
            {
                cbHz.Items.Add(HZTable.Rows[i][0]);
            }

            dtpFrom.Value = System.DateTime.Now;
            dtpTo.Value = DateTime.Now.AddDays(1);
            tbPkt.Text = "";
            tbHzqd.Text = "";
            tbBch.Text = "";
            tbKh.Text = "";
            tbYwbh.Text = "";
            tbSpdm.Text = "";
            tbPh.Text = "";
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string strZtFrom = "";
            if (cbZtFrom.Text == "未选择") { strZtFrom = "10"; }
            else if (cbZtFrom.Text == "打印") { strZtFrom = "20"; }
            else if (cbZtFrom.Text == "包装中") { strZtFrom = "35"; }
            else if (cbZtFrom.Text == "包装完成") { strZtFrom = "40"; }
            else { strZtFrom = ""; }
            
            string strZtTo = "";
            if (cbZtTo.Text == "未选择") { strZtTo = "10"; }
            else if (cbZtTo.Text == "打印") { strZtTo = "20"; }
            else if (cbZtTo.Text == "包装中") { strZtTo = "35"; }
            else if (cbZtTo.Text == "包装完成") { strZtTo = "40"; }
            else { strZtTo = ""; }
      
            string strZt = "";
            if ((strZtFrom != "") && (strZtTo != "")) { strZt = " and stat_code>=" + strZtFrom + " and stat_code<=" + strZtTo; }
            else if ((strZtFrom != "") && (strZtTo == "")) { strZt = " and stat_code>="+strZtFrom; }
            else if ((strZtFrom == "") && (strZtTo != "")) { strZt = " and stat_code<=" + strZtTo; }
            else { strZt = ""; }
            

            string strSj = "";
            if ((dtpFrom.Value == null) || (dtpTo.Value == null)) { MessageBox.Show("请选择日期！"); return; } else { strSj = " and to_char(create_date_time,'YYYY-MM-DD')>='" + dtpFrom.Value.ToString("yyyy-MM-dd") + "' and to_char(create_date_time,'YYYY-MM-DD')<='" + dtpTo.Value.ToString("yyyy-MM-dd") + "'"; }
            string strCk = "";
            if (cbCk.Text.Trim() == "") { strCk = ""; } else { strCk = " and whse='" + cbCk.Text.Trim() + "'"; }
            string strHz = "";
            if (cbHz.Text.Trim() == "") { strHz = ""; } else { strHz = " and season='" + cbHz.Text.Trim() + "'"; }
            string strPkt = "";
            if (tbPkt.Text.Trim() == "") { strPkt = ""; } else { strPkt = " and pkt_ctrl_Nbr='" + tbPkt.Text.Trim() + "'"; }
            string strHzqd = "";
            if (tbHzqd.Text.Trim() == "") { strHzqd = ""; } else { strHzqd = " and major_pkt_grp_attr='" + tbHzqd.Text.Trim() + "'"; }
            string strBch = "";
            if (tbBch.Text.Trim() == "") { strBch = ""; } else { strBch = " and pick_wave_nbr='" + tbBch.Text.Trim() + "'"; }
            string strKh = "";
            if (tbKh.Text.Trim() == "") { strKh = ""; } else { strKh = " and shipto_name like '%" + tbKh.Text.Trim() + "%'"; }
            string strYwbh = "";
            if (tbYwbh.Text.Trim() == "") { strYwbh = ""; } else { strYwbh = " and assort_nbr like '%" + tbYwbh.Text.Trim() + "%'"; }
            string strSpdm = "";
            if (tbSpdm.Text.Trim() == "") { strSpdm = ""; } else { strSpdm = " and size_desc='" + tbSpdm.Text.Trim() + "'"; }
            string strPh = "";
            if (tbPh.Text.Trim() == "") { strPh = ""; } else { strPh = " and batch_nbr='" + tbPh.Text.Trim() + "'"; }

            string sql = "select whse as 仓库,season as 货主,pkt_ctrl_nbr as 物流PKT号,major_pkt_grp_attr as 货主清单号,create_date_time as 下单时间," +
                              "stat_code as 单据状态,pick_wave_nbr as 波次号,shipto as 客户代码,shipto_name as 客户名称,shipto_addr as 送货地址,pkt_seq_nbr as 物流PKT序号," +
                              "assort_nbr as 货主业务编号,size_desc as 商品代码,sku_desc as 品名规格,orig_pkt_qty as 数量,batch_nbr as 批号,units as 单位," +
                              "vendor_name as 供应商,manufacturer as 生产厂家,ifthermometer as 是否需附温度计,therm_company 温度计所属公司,pack_type_z 整件包装类型,pack_type_l 零散包装类型 from v_cc_pkt where 1=1 " +
                              strSj + strCk + strHz + strPkt + strHzqd + strBch + strKh + strYwbh + strSpdm + strPh + strZt +" order by pkt_ctrl_nbr,pkt_seq_nbr";

            OleDbDataAdapter sda = new OleDbDataAdapter(sql, CommFunction.ConnectString);
            DataSet ds = new DataSet();
            sda.Fill(ds, "chk_con");

            dataGridView1.DataSource = ds.Tables["chk_con"];
            dataGridView1.AutoResizeColumns();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            pPkt = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["物流PKT号"].Value.ToString();
            pifsave = "True";
            Close();
        }
    }
}
