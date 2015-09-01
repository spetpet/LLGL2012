using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using LoginCs;
using PowerCheckCs;

namespace LLGL2012
{
    public partial class FrmRGBack : Form
    {
        private static FrmRGBack pUniqueForm = null;//窗体唯一打开代码

        /// <summary>
        /// 窗体唯一打开代码
        /// </summary>
        /// <param name="Path">是否作为模式窗体打开</param>
        public static void ShowUniqueForm(bool AIfShowModal)
        {
            if (pUniqueForm == null)
            {
                new FrmRGBack();

                if (AIfShowModal) pUniqueForm.ShowDialog(); else pUniqueForm.Show();
            }
        }

        public FrmRGBack()
        {
            pUniqueForm = this;//窗体唯一打开代码
            InitializeComponent();
        }

        private void FrmRGBack_FormClosed(object sender, FormClosedEventArgs e)
        {
            pUniqueForm = null;//窗体唯一打开代码
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmRGBack_Load(object sender, EventArgs e)
        {
            cbZt.SelectedIndex = 0;
            //tbFhsj.Text = DateTime.Now.Hour.ToString();
            //tbFhsj2.Text = DateTime.Now.Minute.ToString();
            dtpFhrq.Text = DateTime.Now.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            if (cbWdj.Text == "" || cbZt.Text == "")
                MessageBox.Show("请输完基本信息");
            else
            {
                
                 //OleDbConnection isreturn_conn = new OleDbConnection(CommFunction.ConnectString);
                //isreturn_conn.Open();
                string isreturn_sql="select reserve1 from ysfs where type='温度计字典' and ysid='"+cbWdj.Text+"'";
                //OleDbDataAdapter isreturn_ada = new OleDbDataAdapter(isreturn_sql,isreturn_conn);
                string isreturn_str="";
                isreturn_str=LYFunctionCs.LYFunctionCsClass.cmdScalar(isreturn_sql,CommFunction.ConnectString);

                
                if (isreturn_str=="")
                {
                    MessageBox.Show("温度计未在基础数据中登记！");
                    return;
                }
                else{
                    if (isreturn_str == "在库")
                        if (MessageBox.Show("温度计已在库，是否更新数据？", "更新数据", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                            return;
                        
                        if (localfilepath.Text != "" && serverfilepath.Text != "")


                            try
                            {
                                File.Copy(localfilepath.Text, serverfilepath.Text);
                            }
                            catch
                            {
                                MessageBox.Show("拷贝失败");
                                return;
                            }


                        OleDbConnection cn = new OleDbConnection(CommFunction.ConnectString);
                        cn.Open();
                        
                        string gg = dtpFhrq.Value.ToString("yyyy-MM-dd");
                        //gg = gg + " ";
                        //gg = gg + tbFhsj.Text;
                        //gg = gg + ":";
                        //gg = gg + tbFhsj2.Text;
                        //gg = gg + ":00";
                        gg = gg +" "+dtpFhsj.Text;

                        string SqlSeach = "update cc_cold_chain_therm ct set FILE_PATH='" + serverfilepath.Text + "', BACK_TIME=to_date('" + gg + "','yyyy-mm-dd hh24:mi:ss') ,TEMP_MEMO='" + tbWdbz.Text + "',PACK_MEMO='" + tbZlbz.Text + "',IF_BAD='" + cbZt.Text +
                                   "' where ct.pkunid in (select unid from cc_cold_chain_c where pkunid='" + tbGzbd.Text + "') and ct.THERM_ID='" + cbWdj.Text + "'";
                        OleDbCommand command1 = new OleDbCommand(SqlSeach, cn);
                        command1.ExecuteNonQuery();


                        SqlSeach = "update ysfs set RESERVE1='在库', RESERVE2='" + cbZt.Text + "',MEMO='" + tbZlbz.Text + "' where type='温度计字典' and ysid='" + cbWdj.Text + "'";

                        command1.CommandText = SqlSeach;

                        command1.ExecuteNonQuery();

                        command1.Dispose();

                        cn.Close();

                        cbWdj.Text = null;
                        dtpFhrq.Text = null;
                        tbZlbz.Text = null;
                        localfilepath.Text = null;
                        serverfilepath.Text = null;
                        tbWdbz.Text = null;
                        cbZt.SelectedIndex = 0;
                        //tbFhsj.Text = DateTime.Now.Hour.ToString();
                        //tbFhsj2.Text = DateTime.Now.Minute.ToString();
                        dtpFhrq.Text = DateTime.Now.ToString();

                    }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            if (cbWdj.Text == "")
                MessageBox.Show("请输完基本信息");
            else
            {
                OleDbConnection cn = new OleDbConnection(CommFunction.ConnectString);
                cn.Open();

                string gg = null;

                string SqlSeach = "update cc_cold_chain_therm ct set FILE_PATH=null, BACK_TIME=to_date('" + gg + "','yyyy-mm-dd hh24:mi:ss') ,PACK_MEMO=null,TEMP_MEMO=null,IF_BAD=null" +
                          " where ct.pkunid in (select unid from cc_cold_chain_c where pkunid='" + tbGzbd.Text + "') and ct.THERM_ID='" + cbWdj.Text + "'";
                OleDbCommand command1 = new OleDbCommand(SqlSeach, cn);
                command1.ExecuteNonQuery();

                SqlSeach = "update ysfs set RESERVE1='已发',RESERVE2='正常',MEMO='' where type='温度计字典' and ysid='" + cbWdj.Text + "'";

                command1.CommandText = SqlSeach;
                
                command1.ExecuteNonQuery();

                command1.Dispose();

                cn.Close();

                cbWdj.Text = null;
                tbGzbd.Text = null;
                cbZt.SelectedIndex = 0;
                //tbFhsj.Text = DateTime.Now.Hour.ToString();
                //tbFhsj2.Text = DateTime.Now.Minute.ToString();
                dtpFhrq.Text = DateTime.Now.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                localfilepath.Text = open.FileName;
                string temp = "\\\\173.5.28.153\\公司公共文件\\质量管理部\\温度计返还记录文件\\";
                int i = open.FileName.LastIndexOf('\\');
                serverfilepath.Text = temp + open.FileName.Substring(i + 1);

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            OleDbConnection cn = new OleDbConnection(CommFunction.ConnectString);
            cn.Open();

            cbWdj.Items.Clear();
            //string strrg = "select THERM_ID from cc_cold_chain_therm  where  pkunid=(select unid from cc_cold_chain_c where pkunid='" + tbGzbd.Text + "' and rownum=1 )";
            string strrg = "select THERM_ID from cc_cold_chain_therm  where  pkunid in (select unid from cc_cold_chain_c where pkunid='" + tbGzbd.Text + "')";
            OleDbDataAdapter sda = new OleDbDataAdapter(strrg, cn);
            DataSet rTable = new DataSet();
            sda.Fill(rTable);
            for (int i = 0; i < rTable.Tables[0].Rows.Count; i++)
            {
                cbWdj.Items.Add(rTable.Tables[0].Rows[i][0]);
            }
        }
    }
}
