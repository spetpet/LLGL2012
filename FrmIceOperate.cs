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
    public partial class FrmIceOperate : Form
    {
        private static FrmIceOperate pUniqueForm = null;//窗体唯一打开代码

        /// <summary>
        /// 窗体唯一打开代码
        /// </summary>
        /// <param name="Path">是否作为模式窗体打开</param>
        public static void ShowUniqueForm(bool AIfShowModal)
        {
            if (pUniqueForm == null)
            {
                new FrmIceOperate();

                if (AIfShowModal) pUniqueForm.ShowDialog(); else pUniqueForm.Show();
            }
        }


        public FrmIceOperate()
        {
            pUniqueForm = this;//窗体唯一打开代码
            InitializeComponent();
        }

        private void FrmIceOperate_FormClosed(object sender, FormClosedEventArgs e)
        {
            pUniqueForm = null;//窗体唯一打开代码
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmIceOperate_Load(object sender, EventArgs e)
        {
            dtpFrom.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            dtpTo.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string txtCZSJ = "";
            txtCZSJ = " and to_char(create_date_time,'YYYY-MM-DD')>='" + dtpFrom.Value.ToString("yyyy-MM-dd") + "' and to_char(create_date_time,'YYYY-MM-DD')<='" + dtpTo.Value.ToString("yyyy-MM-dd") + "' ";
            string txtOPERATE_TYPE;
            if (cbCzlx.Text.Trim() == "")
            {
                txtOPERATE_TYPE = "";
            }
            else
            {
                txtOPERATE_TYPE = " and operate_type='" + cbCzlx.Text.Trim() + "' ";
            }
            string txtSKU_DESC;
            if (tbPm.Text.Trim() == "")
            {
                txtSKU_DESC = "";
            }
            else
            {
                txtSKU_DESC = " and sku_desc='" + tbPm.Text.Trim() + "' ";
            }
            string txtOPERATER;
            if (tbCzry.Text.Trim() == "")
            {
                txtOPERATER = "";
            }
            else
            {
                txtOPERATER = " and operater='" + tbCzry.Text.Trim() + "'";
            }

            string ResultSelect = "select operate_type 操作类型,sku_desc 品名,qty 数量,create_date_time 操作时间,operater 操作人员 from ice_operate where 1=1 " + txtCZSJ + txtOPERATE_TYPE + txtSKU_DESC + txtOPERATER + " order by create_date_time desc";

            OleDbDataAdapter sda = new OleDbDataAdapter(ResultSelect, CommFunction.ConnectString);
            DataSet ds = new DataSet();
            sda.Fill(ds, "chk_con");

            dataGridView1.DataSource = ds.Tables["chk_con"];
            dataGridView1.AutoResizeColumns();
        }
    }
}
