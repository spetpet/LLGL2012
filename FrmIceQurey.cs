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
    public partial class FrmIceQurey : Form
    {
        string ResultSelect = "";

        private static FrmIceQurey pUniqueForm = null;//窗体唯一打开代码

        /// <summary>
        /// 窗体唯一打开代码
        /// </summary>
        /// <param name="Path">是否作为模式窗体打开</param>
        public static void ShowUniqueForm(bool AIfShowModal)
        {
            if (pUniqueForm == null)
            {
                new FrmIceQurey();

                if (AIfShowModal) pUniqueForm.ShowDialog(); else pUniqueForm.Show();
            }
        }


        public FrmIceQurey()
        {
            pUniqueForm = this;//窗体唯一打开代码
            InitializeComponent();
        }

        private void FrmIceQurey_FormClosed(object sender, FormClosedEventArgs e)
        {
            pUniqueForm = null;//窗体唯一打开代码
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
        string txtLOCN_BRCD;
        if (tbKwdm.Text.Trim() == "")
        {
            txtLOCN_BRCD = "";
        }
        else { txtLOCN_BRCD = " and ld.LOCN_BRCD = '" + tbKwdm.Text + "' "; }

        string txtSIZE_DESC;
        if (tbBpdm.Text.Trim() == "")
        {
            txtSIZE_DESC = "";
        }
        else { txtSIZE_DESC = " and im.SIZE_DESC = '" + tbBpdm.Text + "' "; }

        string txtSKU_DESC;
        if (tbPm.Text.Trim() == "")
        {
            txtSKU_DESC = "";
        }
        else { txtSKU_DESC = " and im.SKU_DESC = '" + tbPm.Text + "' "; }

        string txtSKU_FORMAT;
        if (tbGg.Text.Trim() == "")
        {
            txtSKU_FORMAT = "";
        }
        else { txtSKU_FORMAT = " and im.SKU_FORMAT = '" + tbGg.Text + "' "; }

        string txtSEASON;
        if (cbSsgs.Text.Trim() == "")
        {
            txtSEASON = "";
        }
        else { txtSEASON = " and im.SEASON = '" + cbSsgs.Text + "' "; }

        string txtSKU_TYPE;
        if (cbLx.Text.Trim() == "")
        {
            txtSKU_TYPE = "";
        }
        else { txtSKU_TYPE = " and im.SKU_TYPE = '" + cbLx.Text + "' "; }

        string txtChkEnable;
        if (cbBksx.Text.Trim() == "")
        {
            txtChkEnable = "";
        }
        else if (cbBksx.Text == "可用冰")
        { txtChkEnable = " and round(TO_NUMBER(sysdate - ld.CREATE_DATE_TIME) * 24) >= nvl(im.VALID_TIME,999999) and round(TO_NUMBER(sysdate - ld.CREATE_DATE_TIME) * 24)<= nvl(im.INVALID_TIME,999999) "; }
        else if (cbBksx.Text == "不可用冰")
        { txtChkEnable = " and round(TO_NUMBER(sysdate - ld.CREATE_DATE_TIME) * 24) < nvl(im.VALID_TIME,999999)"; }
        else
        { txtChkEnable = " and  round(TO_NUMBER(sysdate - ld.CREATE_DATE_TIME) * 24)> nvl(im.INVALID_TIME,999999) "; }

            ResultSelect = "select  im.SIZE_DESC 冰排代码,im.SKU_DESC 品名,im.SKU_FORMAT 规格,im.SEASON 所属公司,ld.locn_brcd 库位代码,im.SKU_TYPE 类型,ld.qty_on_hand 数量,im.SKU_UNIT 单位 ," +
                           "round(TO_NUMBER(sysdate - ld.CREATE_DATE_TIME) * 24)  打冰时间,im.valid_time 生效时间,im.invalid_time 失效时间  from ICE_MASTER im,LOCN_DTL ld " +
                           "where im.size_desc=ld.size_desc  " + txtLOCN_BRCD + txtSIZE_DESC + txtSKU_DESC + txtSKU_FORMAT + txtSEASON + txtSKU_TYPE + txtChkEnable;

            OleDbDataAdapter sda = new OleDbDataAdapter(ResultSelect, CommFunction.ConnectString);
            DataSet ds = new DataSet();
            sda.Fill(ds, "chk_con");

            dataGridView1.DataSource = ds.Tables["chk_con"];
            dataGridView1.AutoResizeColumns();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
             CommFunction.LYData2Excel(this.Handle, CommFunction.ConnectString, ResultSelect, "");
        }

        private void FrmIceQurey_Load(object sender, EventArgs e)
        {
            cbSsgs.Items.Clear();
            string strSsgs = "select distinct SEASON from ice_master";
            OleDbDataAdapter sda = new OleDbDataAdapter(strSsgs, CommFunction.ConnectString);
            DataTable SsgsTable = new DataTable();
            sda.Fill(SsgsTable);
            for (int i = 0; i < SsgsTable.Rows.Count; i++)
            {
                cbSsgs.Items.Add(SsgsTable.Rows[i][0]);
            }
        }
    }
}
