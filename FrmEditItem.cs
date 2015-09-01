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
    public partial class FrmEditItem : Form
    {
        private static FrmEditItem pUniqueForm = null;//窗体唯一打开代码

        public static string ResultSku_id = "'";

        public static string falg = "Edit";
        public static Boolean ifSave = false;

        public static string pSku_id = "";
        public static string pHz = "";
        public static string pSpdm = "";
        public static string pPmgg = "";
        public static string pSccj = "";
        public static string pLlbs = "";
        public static string pCctj = "";
        public static string pJzs = "0";
        public static string pWdj = "";
        public static string pBzlxZ = "";
        public static string pBzlxL = "";
        public static string pRpt = "";

        /// <summary>
        /// 窗体唯一打开代码
        /// </summary>
        /// <param name="Path">是否作为模式窗体打开</param>
        public static void ShowUniqueForm(bool AIfShowModal)
        {
            if (pUniqueForm == null)
            {
                new FrmEditItem();

                if (AIfShowModal) pUniqueForm.ShowDialog(); else pUniqueForm.Show();
            }
        }

        public FrmEditItem()
        {
            pUniqueForm = this;//窗体唯一打开代码
            InitializeComponent();
        }

        private void FrmEditItem_FormClosed(object sender, FormClosedEventArgs e)
        {
            pUniqueForm = null;//窗体唯一打开代码
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tbSku_id_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue != 13) return;
            if (tbSku_id.Text.Trim() == "") { MessageBox.Show("请输入 SKU_ID ！"); return; }
            string OutValue ="";
            int showx, showy;
            showx = Left + (sender as TextBox).Left;
            showy = Top + (sender as TextBox).Top + (sender as TextBox).Height;
            if (CommFunction.ShowGetCodeForm(this.Handle, CommFunction.ConnectString,
                       "select sku_id,season as 货主代码,size_desc as 商品代码,sku_desc as 品名规格, " +
                       " spl_instr_1||spl_instr_2 as 生产厂家,std_pack_qty as 件装数 from item_master@wmrdc " +
                       " where sku_id='"+tbSku_id.Text.Trim()+"'",
                       "sku_id,size_desc,sku_desc", (sender as TextBox).Text, "代码", 0,
                       showx, showy, true,
                      false, true,
                      false, ref OutValue) && (OutValue != ""))
            {
                string[] sArray = OutValue.Split('|');
                (sender as TextBox).Text = sArray[0];
                cbHz.Text = sArray[1];
                tbSpdm.Text = sArray[2];
                tbPmgg.Text = sArray[3];
                tbSccj.Text = sArray[4];
                tbJzs.Text = sArray[5];
            }
        }

        private void tbSpdm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue != 13) return;
            if (tbSpdm.Text.Trim() == "") { MessageBox.Show("请输入 商品代码 ！"); return; }
            string strHz = "";
            if (cbHz.Text.Trim() == "") { strHz = ""; } else { strHz = " and season='"+cbHz.Text.Trim()+"'";}
            string strSpdm = "";
            if (tbSpdm.Text.Trim() == "") { strSpdm = ""; } else { strSpdm = " and size_desc='" + tbSpdm.Text.Trim()+"'"; }
            string OutValue = "";
            int showx, showy;
            showx = Left + (sender as TextBox).Left;
            showy = Top + (sender as TextBox).Top + (sender as TextBox).Height;
            if (CommFunction.ShowGetCodeForm(this.Handle, CommFunction.ConnectString,
                       "select sku_id,season as 货主代码,size_desc as 商品代码,sku_desc as 品名规格, " +
                       " spl_instr_1||spl_instr_2 as 生产厂家,std_pack_qty as 件装数 from item_master@wmrdc " +
                       " where 1=1 " + strHz + strSpdm,
                       "sku_id,size_desc,sku_desc", (sender as TextBox).Text, "代码", 0,
                       showx, showy, true,
                      false, true,
                      false, ref OutValue) && (OutValue != ""))
            {
                string[] sArray = OutValue.Split('|');
                (sender as TextBox).Text = sArray[2];
                cbHz.Text = sArray[1];
                tbSku_id.Text = sArray[0];
                tbPmgg.Text = sArray[3];
                tbSccj.Text = sArray[4];
                tbJzs.Text = sArray[5];
            }

        }

        private void FrmEditItem_Load(object sender, EventArgs e)
        {
            ifSave = false;
            Clean_Frm();            
            string strHZ = "select COMPANY from v_company ";
            OleDbDataAdapter sda = new OleDbDataAdapter(strHZ, CommFunction.ConnectString);
            DataTable HZTable = new DataTable();
            sda.Fill(HZTable);
            for (int i = 0; i < HZTable.Rows.Count; i++)
            {
                cbHz.Items.Add(HZTable.Rows[i][0]);
            }

            string strWdj = "select ysfs from ysfs where type='温度计所属' and nvl(flag,'!@#$%')!='停用' ";
            OleDbDataAdapter sda1 = new OleDbDataAdapter(strWdj, CommFunction.ConnectString);
            DataTable WdjTable = new DataTable();
            sda1.Fill(WdjTable);
            for (int i = 0; i < WdjTable.Rows.Count; i++)
            {
                cbWdj.Items.Add(WdjTable.Rows[i][0]);
            }

            string strBzlxZ = "select distinct ysfs from ysfs where type='温度与包装对照表' and nvl(flag,'!@#$%')!='停用'";
            OleDbDataAdapter sda2 = new OleDbDataAdapter(strBzlxZ, CommFunction.ConnectString);
            DataTable BzlxTable = new DataTable();
            sda2.Fill(BzlxTable);
            for (int i = 0; i < BzlxTable.Rows.Count; i++)
            {
                cbBzlxZ.Items.Add(BzlxTable.Rows[i][0]);
            }

            string strBzlxL = "select distinct ysfs from ysfs where type='温度与包装对照表' and nvl(flag,'!@#$%')!='停用'";
            OleDbDataAdapter sda3 = new OleDbDataAdapter(strBzlxL, CommFunction.ConnectString);
            DataTable BzlxTable1 = new DataTable();
            sda3.Fill(BzlxTable1);
            for (int i = 0; i < BzlxTable1.Rows.Count; i++)
            {
                cbBzlxL.Items.Add(BzlxTable1.Rows[i][0]);
            }

            Load_Frm();
        }

        private void tbJzs_KeyPress(object sender, KeyPressEventArgs e)
        {
            //只允许输整数   件装数自动带出 不允修改 此行无用       
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8)
                e.Handled = true;
        }


        private void Clean_Frm()
        {
            tbSku_id.Text = "";
            cbHz.Text = "";
            tbSpdm.Text = "";
            tbPmgg.Text = "";
            tbSccj.Text = "";
            chbLL.Checked = false;
            cbCctj.Text = "";
            tbJzs.Text = "0";
            cbWdj.Text = "";
            cbBzlxZ.Text = "";
            cbBzlxL.Text = "";
            tbRpt.Text = "";
        }

        private void Load_Frm()
        {
            if (falg == "Edit")
            {
                tbSku_id.Enabled = false;
                cbHz.Enabled = false;
                tbSpdm.Enabled = false;
                tbSku_id.Text = pSku_id;
                cbHz.Text = pHz;
                tbSpdm.Text = pSpdm;
                tbPmgg.Text = pPmgg;
                tbSccj.Text = pSccj;
                if (pLlbs == "冷链") { chbLL.Checked = true; } else { chbLL.Checked = false; };
                cbCctj.Text = pCctj;
                tbJzs.Text = pJzs;
                cbWdj.Text = pWdj;
                cbBzlxZ.Text = pBzlxZ;
                cbBzlxL.Text = pBzlxL;
                tbRpt.Text = pRpt;

            }
            else 
            {
                tbSku_id.Enabled = true;
                cbHz.Enabled = true;
                tbSpdm.Enabled = true;
            }
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (tbSku_id.Text.Trim() == "") { MessageBox.Show("SKU_ID 不允许为空！"); return; }
            if (cbHz.Text.Trim() == "") { MessageBox.Show("货主代码 不允许为空！"); return; }
            if (tbSpdm.Text.Trim() == "") { MessageBox.Show("商品代码 不允许为空！"); return; }
            string pLLbs;
            if (chbLL.Checked) { pLLbs = "冷链"; } else { pLLbs = ""; }
            string sql = "select count(1) from cc_item_master where sku_id='"+tbSku_id.Text.Trim()+"'";
            if (int.Parse(LYFunctionCs.LYFunctionCsClass.cmdScalar(sql, CommFunction.ConnectString)) == 0)
            {                
                string SqlIns = "insert into cc_item_master(sku_id,season,size_desc,sku_desc,cc_flag,manufacturer,storage_conditions,therm_company,pym,"
                                + "create_date_time,user_name,std_pack_qty,pack_type_z,pack_type_l,REPORT_FILE) values('" + tbSku_id.Text.Trim() + "','" + cbHz.Text.Trim() + "','"
                                + tbSpdm.Text.Trim() + "','" + tbPmgg.Text.Trim() + "','" + pLLbs + "','" + tbSccj.Text.Trim() + "','" + cbCctj.Text.Trim() 
                                + "','" + cbWdj.Text.Trim() + "','"+" " /*CommFunction.Pym(tbPmgg.Text.Trim()) 品名规格中带数字和空格没办法用这个函数*/ + "',sysdate,'" 
                                + LoginCs.LoginClass.LogName + "','"+ tbJzs.Text.Trim() + "','"+ cbBzlxZ.Text.Trim() +"','"+ cbBzlxL.Text.Trim()+"','"+ tbRpt.Text.Trim() + "')";
                LYFunctionCs.LYFunctionCsClass.cmd(SqlIns, CommFunction.ConnectString);
            }
            else 
            {
                string SqlUpdate = "update cc_item_master set CC_FLAG='" + pLLbs + "'," + "STORAGE_CONDITIONS='" + cbCctj.Text.Trim() +
                                   "', THERM_COMPANY='" + cbWdj.Text.Trim() + "', user_name='" + LoginCs.LoginClass.LogName + "',PACK_TYPE_Z='" + cbBzlxZ.Text.Trim() + "',PACK_TYPE_L='" + cbBzlxL.Text.Trim() + "',REPORT_FILE='" + tbRpt.Text.Trim() +
                                   "'where sku_id='"+tbSku_id.Text.Trim()+"'";
                LYFunctionCs.LYFunctionCsClass.cmd(SqlUpdate, CommFunction.ConnectString);
            }

            ResultSku_id = ResultSku_id + "','" + tbSku_id.Text.Trim();

            ifSave = true;

            MessageBox.Show("完成。");

            if (falg == "Add") { Clean_Frm(); } else { Close(); }

        }

        private void tbSku_id_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
