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

using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace LLGL2012
{
    public partial class FrmColdChain : Form
    {
        private static FrmColdChain pUniqueForm = null;//窗体唯一打开代码

        public struct Bp_dtl
        {
            public string pIce_Id;
            public string pIce_Name;
            public string pIce_Qty; 
        }

        public struct ColdChain_dtl
        {
            public string pBzts;
            public string pBzjs;
            public string pWdj;
            public Bp_dtl[] bd;
        };

        public static ColdChain_dtl[] cd;

        /// <summary>
        /// 窗体唯一打开代码
        /// </summary>
        /// <param name="Path">是否作为模式窗体打开</param>
        public static void ShowUniqueForm(bool AIfShowModal)
        {
            if (pUniqueForm == null)
            {
                new FrmColdChain();

                if (AIfShowModal) pUniqueForm.ShowDialog(); else pUniqueForm.Show();
            }
        }

        public FrmColdChain()
        {
            pUniqueForm = this;//窗体唯一打开代码
            InitializeComponent();
        }

        private void FrmColdChain_FormClosed(object sender, FormClosedEventArgs e)
        {
            pUniqueForm = null;//窗体唯一打开代码
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmSearchCcPkt.ShowUniqueForm(true);
            tbPKT.Text = FrmSearchCcPkt.pPkt;
            if (FrmSearchCcPkt.pPkt.ToString().Trim() == "") return;
            getInf();
        }


        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if ((sender as DataGridView).CurrentRow == null) return;//如果无此句,则点击dataGridView的列标题时会出错
            tbPKT.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["物流PKT号"].Value.ToString();
            tbCk.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["仓库"].Value.ToString();
            tbHz.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["货主"].Value.ToString();
            tbHzqd.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["货主清单号"].Value.ToString();
            tbXdsj.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["下单时间"].Value.ToString();
            tbDjzt.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["单据状态"].Value.ToString();
            tbBch.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["波次号"].Value.ToString();
            tbKhmc.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["客户名称"].Value.ToString();
            tbShdz.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["送货地址"].Value.ToString();
            if (dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["是否需附温度计"].Value.ToString() == "附温度计")
            {
                cbWdjpfr.Enabled = true;
            }
            else 
            {
                cbWdjpfr.Enabled = false;
            }
        }

        private void tbPKT_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyValue != 13) return;
            //getInf();
        }

        private void getInf()
        {
            //if (tbPKT.Text.Trim() == "") { MessageBox.Show("请输入 PKT 号！"); return; }

            OleDbConnection cn = new OleDbConnection(CommFunction.ConnectString);
            cn.Open();
            string SqlSeach = "select whse as 仓库,season as 货主,pkt_ctrl_nbr as 物流PKT号,major_pkt_grp_attr as 货主清单号,create_date_time as 下单时间," +
                              "stat_code as 单据状态,pick_wave_nbr as 波次号,shipto as 客户代码,shipto_name as 客户名称,shipto_addr as 送货地址,pkt_seq_nbr as 物流PKT序号," +
                              "assort_nbr as 货主业务编号,size_desc as 商品代码,sku_desc as 品名规格,orig_pkt_qty as 数量,batch_nbr as 批号,units as 单位," +
                              "vendor_name as 供应商,manufacturer as 生产厂家,ifthermometer as 是否需附温度计,therm_company 温度计所属公司,pack_type 包装类型,'' as 跟踪单号" +
                              " from  v_cc_pkt where pkt_ctrl_nbr='" + tbPKT.Text.Trim() + "' order by pkt_ctrl_nbr,pkt_seq_nbr";
            OleDbDataAdapter sda = new OleDbDataAdapter(SqlSeach, CommFunction.ConnectString);
            DataSet ds = new DataSet();
            sda.Fill(ds, "chk_con");
            dataGridView1.DataSource = ds.Tables["chk_con"];
            dataGridView1.AutoResizeColumns();
            tbPKT.Focus();
            tbPKT.SelectAll();

            cd = new ColdChain_dtl[dataGridView1.Rows.Count];
            for (int i=0;i<dataGridView1.Rows.Count;i++)
            {
                cd[i].bd = null;
            }
 
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells["是否需附温度计"].Value.ToString() == "附温度计")
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            if ((cbWdjpfr.Enabled == true) && cbWdjpfr.Text.Trim() == "") { MessageBox.Show("请选择 温度计配附人 ！"); return; }
            if (int.Parse(LYFunctionCs.LYFunctionCsClass.cmdScalar("select count(1) from cc_cold_chain_z where pkt_ctrl_nbr='" + tbPKT.Text + "'", CommFunction.ConnectString)) == 0)
            {
                if (MessageBox.Show("确实要新增一张跟踪单吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    OleDbConnection con = new OleDbConnection(CommFunction.ConnectString);
                    con.Open();

                    OleDbCommand ocmd = new OleDbCommand("SP_INSERT_LLGZ", con);
                    ocmd.CommandType = CommandType.StoredProcedure;

                    //此处sourceColumn参数没什么用(不用该参数也可以).参数值必须按该存储过程中的参数顺序进行传递

                    ocmd.Parameters.Add("A", OleDbType.VarChar, 50, "pWHSE").Value = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["仓库"].Value.ToString();
                    //Parameters.Add的第一个参数parameterName无实际意义，可随意输入

                    ocmd.Parameters.Add("B", OleDbType.VarChar, 100, "pSEASON").Value = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["货主"].Value.ToString();
                    ocmd.Parameters.Add("C", OleDbType.VarChar, 100, "pSHIPTO").Value = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["客户代码"].Value.ToString();
                    ocmd.Parameters.Add("D", OleDbType.VarChar, 100, "pSHIPTO_NAME").Value = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["客户名称"].Value.ToString();
                    ocmd.Parameters.Add("E", OleDbType.VarChar, 100, "pSHIPTO_ADDR").Value = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["送货地址"].Value.ToString();
                    ocmd.Parameters.Add("F", OleDbType.VarChar, 100, "pPKT_CTRL_NBR").Value = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["物流PKT号"].Value.ToString();
                    ocmd.Parameters.Add("G", OleDbType.VarChar, 100, "pMAJOR_PKT_GRP_ATTR").Value = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["货主清单号"].Value.ToString();
                    ocmd.Parameters.Add("H", OleDbType.VarChar, 100, "pUSER_NAME").Value = LoginCs.LoginClass.LogName;
                    ocmd.Parameters.Add("I", OleDbType.VarChar, 100, "pUSER_THERM").Value = cbWdjpfr.Text.Trim();
                    ocmd.Parameters.Add("J", OleDbType.VarChar, 100, "pUSER_CHECK").Value = "";
                    ocmd.Parameters.Add("K", OleDbType.VarChar, 100, "pCHECK_DATE_TIME").Value = "";//未知
                    ocmd.Parameters.Add("L", OleDbType.VarChar, 100, "pMIN_TEMP_SRC").Value = nuCfmin.Value;
                    ocmd.Parameters.Add("M", OleDbType.VarChar, 100, "pMAX_TEMP_SRC").Value = nuCfmax.Value;
                    ocmd.Parameters.Add("N", OleDbType.VarChar, 100, "pMIN_TEMP_DEST").Value = nuMdmin.Value;
                    ocmd.Parameters.Add("O", OleDbType.VarChar, 100, "pMAX_TEMP_DEST").Value = nuMdmax.Value;
                    ocmd.Parameters.Add("P", OleDbType.VarChar, 100, "pCUR_TEMP_SRC").Value = nuFhwd.Value;
                    ocmd.Parameters.Add("Q", OleDbType.VarChar, 100, "pFULL_COLD_CAR").Value = chbLcc.Checked ? "1" : "0";
                    ocmd.Parameters.Add("R", OleDbType.VarChar, 100, "pUSER_PACK").Value = "";
                    ocmd.Parameters.Add("S", OleDbType.VarChar, 100, "pPACK_DATE_TIME").Value = "";//未知;
                    ocmd.Parameters.Add("T", OleDbType.VarChar, 100, "pCARR_NAME").Value = "";//未知;
                    ocmd.Parameters.Add("U", OleDbType.VarChar, 100, "pUSER_CARR").Value = "";//未知;
                    ocmd.Parameters.Add("V", OleDbType.VarChar, 100, "pCARR_DATE_TIME").Value = "";//未知;
                    ocmd.Parameters.Add("W", OleDbType.VarChar, 100, "pUSER_SIGN").Value = "";//未知;
                    ocmd.Parameters.Add("X", OleDbType.VarChar, 100, "pSIGN_DATE_TIME").Value = "";//未知;
                    ocmd.Parameters.Add("Y", OleDbType.VarChar, 100, "pSIGN_TEMP_ENVI").Value = "";//未知;
                    ocmd.Parameters.Add("Z", OleDbType.VarChar, 100, "pSIGN_TEMP_DRUGS").Value = "";//未知;
                    ocmd.Parameters.Add("A", OleDbType.VarChar, 100, "pSIGN_MEMO").Value = "";//未知;

                    OleDbParameter pUNID = ocmd.Parameters.Add("GZDH", OleDbType.VarChar, 100, "pUNID");//参数类型的变量，取名时也可随意

                    pUNID.Direction = ParameterDirection.Output;

                    ocmd.ExecuteNonQuery();

                    string UNID = ocmd.Parameters["GZDH"].Value.ToString().Trim(Convert.ToChar(0x0));//此处需用到Parameters.Add的第一个参数parameterName

                    con.Close();
                    con.Dispose();

                    for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
                    {
                        OleDbConnection con2 = new OleDbConnection(CommFunction.ConnectString);
                        con2.Open();

                        OleDbCommand ocmd2 = new OleDbCommand("SP_INSERT_LLGZ_C", con2);
                        ocmd2.CommandType = CommandType.StoredProcedure;

                        //此处sourceColumn参数没什么用(不用该参数也可以).参数值必须按该存储过程中的参数顺序进行传递

                        ocmd2.Parameters.Add("A", OleDbType.VarChar, 50, "pPKUNID").Value = int.Parse(UNID);
                        //Parameters.Add的第一个参数parameterName无实际意义，可随意输入

                        ocmd2.Parameters.Add("B", OleDbType.VarChar, 100, "pPKT_SEQ_NBR").Value = dataGridView1.Rows[i].Cells["物流PKT序号"].Value;
                        ocmd2.Parameters.Add("C", OleDbType.VarChar, 100, "pASSORT_NBR").Value = dataGridView1.Rows[i].Cells["货主业务编号"].Value.ToString();
                        ocmd2.Parameters.Add("D", OleDbType.VarChar, 100, "pSIZE_DESC").Value = dataGridView1.Rows[i].Cells["商品代码"].Value.ToString();
                        ocmd2.Parameters.Add("E", OleDbType.VarChar, 100, "pSKU_DESC").Value = dataGridView1.Rows[i].Cells["品名规格"].Value.ToString();
                        ocmd2.Parameters.Add("F", OleDbType.VarChar, 100, "pBATCH_NBR").Value = dataGridView1.Rows[i].Cells["批号"].Value.ToString();
                        ocmd2.Parameters.Add("G", OleDbType.VarChar, 100, "pORIG_PKT_QTY").Value = dataGridView1.Rows[i].Cells["数量"].Value;
                        ocmd2.Parameters.Add("H", OleDbType.VarChar, 100, "pUNITS").Value = dataGridView1.Rows[i].Cells["单位"].Value.ToString(); ;
                        ocmd2.Parameters.Add("I", OleDbType.VarChar, 100, "pUSER_NAME").Value = LoginCs.LoginClass.LogName;
                        ocmd2.Parameters.Add("J", OleDbType.VarChar, 100, "pVENDOR_NAME").Value = dataGridView1.Rows[i].Cells["供应商"].Value.ToString();
                        ocmd2.Parameters.Add("K", OleDbType.VarChar, 100, "pPACK_TIPS").Value = cd[i].pBzts == null ? "" : cd[i].pBzts;
                        ocmd2.Parameters.Add("L", OleDbType.VarChar, 100, "pPACK_NUM").Value = cd[i].pBzjs == null ? "0" : cd[i].pBzjs;


                        OleDbParameter pUNID_C = ocmd2.Parameters.Add("GZDH_C", OleDbType.VarChar, 100, "pUNID_C");//参数类型的变量，取名时也可随意

                        pUNID_C.Direction = ParameterDirection.Output;

                        ocmd2.ExecuteNonQuery();

                        string UNID_C = ocmd2.Parameters["GZDH_C"].Value.ToString().Trim(Convert.ToChar(0x0));//此处需用到Parameters.Add的第一个参数parameterName

                        con2.Close();
                        con2.Dispose();

                        //冰排信息

                        if (cd[i].bd != null)
                        {
                            for (int iBd = 0; iBd < cd[i].bd.Length; iBd++)
                            {
                                string SqlIns = "insert into　cc_cold_chain_ice (PKUNID,ICE_ID,ICE_NAME,ICE_QTY,USER_NAME,IF_PICK,PKT_SEQ_NBR) values('" +
                                                UNID_C + "','" + cd[i].bd[iBd].pIce_Id + "','" + cd[i].bd[iBd].pIce_Name + "','" + cd[i].bd[iBd].pIce_Qty + "','"
                                                + LoginCs.LoginClass.LogName + "','待拣','" + dataGridView1.Rows[i].Cells["物流PKT序号"].Value.ToString() + "')";
                                LYFunctionCs.LYFunctionCsClass.cmd(SqlIns, CommFunction.ConnectString);
                            }
                        }


                        //温度计已发 更新处理。。。
                        string stra = cd[i].pWdj == null ? "" : cd[i].pWdj;
                        char[] sep = { ';' };
                        string[] strb = new string[10];
                        strb = stra.Split(sep);
                        int temprgsl = strb.Length - 1;
                        for (int temp1 = 0; temp1 < temprgsl; temp1++)
                        {//同一个明细的温度计循环
                            string SqlUpt = "update ysfs set RESERVE1='已发' where type='温度计字典' and ysid='" + strb[temp1].ToString() + "' ";
                            LYFunctionCs.LYFunctionCsClass.cmd(SqlUpt, CommFunction.ConnectString);

                            string strwdrggz = "select * from CC_COLD_CHAIN_THERM t where t.pkunid='" + UNID_C + "' and t.therm_id='" + strb[temp1].ToString() + "'";

                            OleDbDataAdapter sda;
                            sda = new OleDbDataAdapter(strwdrggz, CommFunction.ConnectString);
                            DataSet rgTable = new DataSet();
                            sda.Fill(rgTable);

                            if (rgTable.Tables[0].Rows.Count == 0)
                            {
                                string SqlIns = "insert into　CC_COLD_CHAIN_THERM (pkunid,therm_id,user_name,PKT_SEQ_NBR) values('" + UNID_C + "','" + strb[temp1].ToString() + "','" + LoginCs.LoginClass.LogName + "','" + dataGridView1.Rows[i].Cells["物流PKT序号"].Value.ToString() + "')";
                                LYFunctionCs.LYFunctionCsClass.cmd(SqlIns, CommFunction.ConnectString);

                            }

                        }//同一个明细的温度计循环     

                    }


                    //清空数据
                    dataGridView1.AutoGenerateColumns = false;
                    dataGridView1.DataSource = null;
                    tbPKT.Text = null;
                    tbCk.Text = null;
                    tbHz.Text = null;
                    tbHzqd.Text = null;
                    tbXdsj.Text = null;
                    tbDjzt.Text = null;
                    tbBch.Text = null;
                    tbKhmc.Text = null;
                    tbShdz.Text = null;
                    //nuCfmax.Value = 0;
                    //nuMdmax.Value = 0;
                    //nuFhwd.Value = 0;
                    //nuCfmin.Value = 0;
                    //nuMdmin.Value = 0;
                    chbLcc.Checked = false;
                    cbWdjpfr.Text = null;


                    tbGzbdh.Text = UNID;
                }
            }
            else 
            {
                if (MessageBox.Show("确实要修改该跟踪单吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    string SqlUpt = "update cc_cold_chain_z set USER_THERM='"+ cbWdjpfr.Text.Trim() +"',MIN_TEMP_SRC='"+ nuCfmin.Value +"',MAX_TEMP_SRC='"+ nuCfmax.Value +"',MIN_TEMP_DEST='"+ nuMdmin.Value +"',MAX_TEMP_DEST='"+ nuMdmax.Value
                                   + "',CUR_TEMP_SRC='" + nuFhwd.Value + "',FULL_COLD_CAR='" + (chbLcc.Checked ? "1" : "0") + "' where unid='" + dataGridView1.Rows[0].Cells["跟踪单号"].Value + "'";
                    LYFunctionCs.LYFunctionCsClass.cmd(SqlUpt, CommFunction.ConnectString);

                    for (int ii = 0; ii <= dataGridView1.RowCount - 1; ii++)
                    {
                        SqlUpt = "update cc_cold_chain_c set PACK_TIPS='" + (cd[ii].pBzts == null ? "" : cd[ii].pBzts) + "',PACK_NUM='" + (cd[ii].pBzjs == null ? "0" : cd[ii].pBzjs) + "' where pkunid='" + dataGridView1.Rows[0].Cells["跟踪单号"].Value + "' and PKT_SEQ_NBR='" + dataGridView1.Rows[ii].Cells["物流PKT序号"].Value + "'";
                        LYFunctionCs.LYFunctionCsClass.cmd(SqlUpt, CommFunction.ConnectString);

                        string Sqlrg = "select ct.therm_id from cc_cold_chain_therm ct where ct.pkunid=" +
                          "(select unid from cc_cold_chain_c where pkunid='" + dataGridView1.Rows[0].Cells["跟踪单号"].Value + "' and ct.pkt_seq_nbr=cc_cold_chain_c.pkt_seq_nbr  )";

                        OleDbDataAdapter sda;
                        sda = new OleDbDataAdapter(Sqlrg, CommFunction.ConnectString);
                        DataTable rgTable = new DataTable();
                        sda.Fill(rgTable);

                        for (int rgsltemp = 0; rgsltemp < rgTable.Rows.Count; rgsltemp++)
                        {
                            LYFunctionCs.LYFunctionCsClass.cmd("update ysfs set RESERVE1='在库' where type='温度计字典' and ysid='" + rgTable.Rows[rgsltemp][0] + "' ", CommFunction.ConnectString);
                        }

                        LYFunctionCs.LYFunctionCsClass.cmd("delete from cc_cold_chain_therm ct where ct.pkunid in (select unid from cc_cold_chain_c where pkunid='" + dataGridView1.Rows[0].Cells["跟踪单号"].Value + "')", CommFunction.ConnectString);

                        LYFunctionCs.LYFunctionCsClass.cmd("delete from cc_cold_chain_ice ci where ci.pkunid in (select unid from cc_cold_chain_c where pkunid='" + dataGridView1.Rows[0].Cells["跟踪单号"].Value + "')", CommFunction.ConnectString);


                        //冰排信息

                        if (cd[ii].bd != null)
                        {
                            for (int iBd = 0; iBd < cd[ii].bd.Length; iBd++)
                            {
                                string SqlIns = "insert into　cc_cold_chain_ice (PKUNID,ICE_ID,ICE_NAME,ICE_QTY,USER_NAME,IF_PICK,PKT_SEQ_NBR) values("+
                                                "(select unid from cc_cold_chain_c where pkunid='" + dataGridView1.Rows[0].Cells["跟踪单号"].Value + "' and pkt_seq_nbr='" + dataGridView1.Rows[ii].Cells["物流PKT序号"].Value.ToString() + "'),'" + cd[ii].bd[iBd].pIce_Id + "','" + cd[ii].bd[iBd].pIce_Name + "','" + cd[ii].bd[iBd].pIce_Qty + "','"
                                                + LoginCs.LoginClass.LogName + "','待拣','" + dataGridView1.Rows[ii].Cells["物流PKT序号"].Value.ToString() + "')";
                                LYFunctionCs.LYFunctionCsClass.cmd(SqlIns, CommFunction.ConnectString);
                            }
                        }


                        //温度计已发 更新处理。。。
                        string stra = cd[ii].pWdj == null ? "" : cd[ii].pWdj;
                        char[] sep = { ';' };
                        string[] strb = new string[10];
                        strb = stra.Split(sep);
                        int temprgsl = strb.Length - 1;
                        for (int temp1 = 0; temp1 < temprgsl; temp1++)
                        {//同一个明细的温度计循环
                            SqlUpt = "update ysfs set RESERVE1='已发' where type='温度计字典' and ysid='" + strb[temp1].ToString() + "' ";
                            LYFunctionCs.LYFunctionCsClass.cmd(SqlUpt, CommFunction.ConnectString);

                            string strwdrggz = "select * from CC_COLD_CHAIN_THERM t where t.pkunid in (select unid from cc_cold_chain_c where pkunid='" + dataGridView1.Rows[0].Cells["跟踪单号"].Value + "') and t.therm_id='" + strb[temp1].ToString() + "'";

                            OleDbDataAdapter sda1;
                            sda1 = new OleDbDataAdapter(strwdrggz, CommFunction.ConnectString);
                            DataSet rgTable1 = new DataSet();
                            sda1.Fill(rgTable1);

                            if (rgTable1.Tables[0].Rows.Count == 0)
                            {
                                string SqlIns = "insert into　CC_COLD_CHAIN_THERM (pkunid,therm_id,user_name,PKT_SEQ_NBR) values((select unid from cc_cold_chain_c where pkunid='" + dataGridView1.Rows[0].Cells["跟踪单号"].Value + "' and pkt_seq_nbr='" + dataGridView1.Rows[ii].Cells["物流PKT序号"].Value.ToString() + "'),'" + strb[temp1].ToString() + "','" + LoginCs.LoginClass.LogName + "','" + dataGridView1.Rows[ii].Cells["物流PKT序号"].Value.ToString() + "')";
                                LYFunctionCs.LYFunctionCsClass.cmd(SqlIns, CommFunction.ConnectString);

                            }
                        }

                    }


                    //清空数据
                    dataGridView1.AutoGenerateColumns = false;
                    dataGridView1.DataSource = null;
                    tbPKT.Text = null;
                    tbCk.Text = null;
                    tbHz.Text = null;
                    tbHzqd.Text = null;
                    tbXdsj.Text = null;
                    tbDjzt.Text = null;
                    tbBch.Text = null;
                    tbKhmc.Text = null;
                    tbShdz.Text = null;
                    //nuCfmax.Value = 0;
                    //nuMdmax.Value = 0;
                    //nuFhwd.Value = 0;
                    //nuCfmin.Value = 0;
                    //nuMdmin.Value = 0;
                    chbLcc.Checked = false;
                    cbWdjpfr.Text = null;

                    tbGzbdh.Text = null;
 
                }
            }
        }

        private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            if (dataGridView1.Rows.Count <= 0) return;
            if (dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["是否需附温度计"].Value.ToString() == "附温度计") FrmPack.Ifwdj = true; else FrmPack.Ifwdj = false;
            FrmPack.pWdjgs = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["温度计所属公司"].Value.ToString();
            FrmPack.pName = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["包装类型"].Value.ToString();
            FrmPack.pLx = bzlx(FrmPack.pName);
            FrmPack.pBzts = cd[dataGridView1.CurrentCell.RowIndex].pBzts;
            FrmPack.pBzjs = cd[dataGridView1.CurrentCell.RowIndex].pBzjs;
            FrmPack.pWdj = cd[dataGridView1.CurrentCell.RowIndex].pWdj;

            if (cd[dataGridView1.CurrentCell.RowIndex].bd != null)
            {
                FrmPack.pBd = new FrmPack.Bp_dtl[cd[dataGridView1.CurrentCell.RowIndex].bd.Length];//恶心调用 木有办法
                for (int ii = 0; ii < cd[dataGridView1.CurrentCell.RowIndex].bd.Length; ii++)
                {
                    FrmPack.pBd[ii].pIce_Id = cd[dataGridView1.CurrentCell.RowIndex].bd[ii].pIce_Id;
                    FrmPack.pBd[ii].pIce_Name = cd[dataGridView1.CurrentCell.RowIndex].bd[ii].pIce_Name;
                    FrmPack.pBd[ii].pIce_Qty = cd[dataGridView1.CurrentCell.RowIndex].bd[ii].pIce_Qty;
                }
            }
            else if (cd[dataGridView1.CurrentCell.RowIndex].bd == null)  { FrmPack.pBd = null; }//防止查询跟踪单赋值的时候 listview没有清除

            FrmPack.ShowUniqueForm(true);
            if (FrmPack.Ifsave)
            {
                cd[dataGridView1.CurrentCell.RowIndex].pBzts = FrmPack.pBzts;
                cd[dataGridView1.CurrentCell.RowIndex].pBzjs = FrmPack.pBzjs;
                cd[dataGridView1.CurrentCell.RowIndex].pWdj = FrmPack.pWdj;
                if (FrmPack.pBd != null)
                {
                    cd[dataGridView1.CurrentCell.RowIndex].bd = new Bp_dtl[FrmPack.pBd.Length];
                    for (int i = 0; i < FrmPack.pBd.Length; i++)
                    {
                        cd[dataGridView1.CurrentCell.RowIndex].bd[i].pIce_Id = FrmPack.pBd[i].pIce_Id;
                        cd[dataGridView1.CurrentCell.RowIndex].bd[i].pIce_Name = FrmPack.pBd[i].pIce_Name;
                        cd[dataGridView1.CurrentCell.RowIndex].bd[i].pIce_Qty = FrmPack.pBd[i].pIce_Qty;
                    }
                }
            }
        }

        private string bzlx(string lx)
        {
            if (chbLcc.Checked) { return " and reserve1='" + "自运全程冷藏车配送" + "'"; }
            OleDbConnection cn = new OleDbConnection(CommFunction.ConnectString);
            cn.Open();
            OleDbDataAdapter sda;
            string strR1, strR2, strR3, strR4;
            string strWD = "select distinct reserve1,reserve2,reserve3,reserve4 from ysfs where type='温度与包装对照表' and ysfs='" + lx + "' and reserve1<>'自运全程冷藏车配送' ";
            sda = new OleDbDataAdapter(strWD, cn);
            DataSet WDTable = new DataSet();
            sda.Fill(WDTable);
            for (int i = 0; i < WDTable.Tables[0].Rows.Count; i++)
            {
                strR1 = WDTable.Tables[0].Rows[i][0].ToString();
                strR2 = WDTable.Tables[0].Rows[i][1].ToString();
                strR3 = WDTable.Tables[0].Rows[i][2].ToString();
                strR4 = WDTable.Tables[0].Rows[i][3].ToString();
                if ((int.Parse(strR1.Split('~')[1]) <= nuCfmax.Value) && (nuCfmax.Value <= int.Parse(strR1.Split('~')[0]))
                    && (int.Parse(strR2.Split('~')[1]) <= nuCfmin.Value) && (nuCfmin.Value <= int.Parse(strR2.Split('~')[0]))
                    && (int.Parse(strR3.Split('~')[1]) <= nuMdmax.Value) && (nuMdmax.Value <= int.Parse(strR3.Split('~')[0]))
                    && (int.Parse(strR4.Split('~')[1]) <= nuMdmin.Value) && (nuMdmin.Value <= int.Parse(strR4.Split('~')[0])))
                {
                    return " and reserve1='" + strR1 + "' and reserve2='" + strR2 + "'and reserve3='" + strR3 + "' and reserve4='" + strR4 + "'";
                }

            }
            return " and 1=2 ";
        }

        private void FrmColdChain_Load(object sender, EventArgs e)
        {
            OleDbDataAdapter sda;
            string strR2 = "select ysfs from ysfs WHERE TYPE='冷链复核人' and nvl(flag,'!@#$%')!='停用'  ";
            sda = new OleDbDataAdapter(strR2, CommFunction.ConnectString);
            DataSet RYTable2 = new DataSet();
            sda.Fill(RYTable2);
            for (int i = 0; i < RYTable2.Tables[0].Rows.Count; i++)
            {
                cbWdjpfr.Items.Add(RYTable2.Tables[0].Rows[i][0]);

            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            button1_Click(null,null);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;

            if (MessageBox.Show("确实要删除跟踪单" + tbGzbdh.Text + "吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel) return;

            OleDbConnection cn = new OleDbConnection(CommFunction.ConnectString);
            cn.Open();
            //更新温度计状态表

        
            string Sqlrg = "select ct.therm_id from cc_cold_chain_therm ct where ct.pkunid in "+
                           "(select unid from cc_cold_chain_c where pkunid='" + tbGzbdh.Text + "')";

            OleDbDataAdapter sda;
            sda = new OleDbDataAdapter(Sqlrg, cn);
            DataTable rgTable = new DataTable();
            sda.Fill(rgTable);

            for (int rgsltemp = 0; rgsltemp < rgTable.Rows.Count; rgsltemp++)
            {
                LYFunctionCs.LYFunctionCsClass.cmd("update ysfs set RESERVE1='在库' where type='温度计字典' and ysid='" + rgTable.Rows[rgsltemp][0] + "' ", CommFunction.ConnectString);
            }

            LYFunctionCs.LYFunctionCsClass.cmd("delete from cc_cold_chain_therm ct where ct.pkunid in (select unid from cc_cold_chain_c where pkunid='" + tbGzbdh.Text + "')", CommFunction.ConnectString);



            // 删除主表的记录



            LYFunctionCs.LYFunctionCsClass.cmd("delete from cc_cold_chain_ice ci where ci.pkunid in (select unid from cc_cold_chain_c where pkunid='" + tbGzbdh.Text + "' )", CommFunction.ConnectString);

            LYFunctionCs.LYFunctionCsClass.cmd("delete from cc_cold_chain_c where pkunid='" + tbGzbdh.Text + "'", CommFunction.ConnectString);

            LYFunctionCs.LYFunctionCsClass.cmd("delete from cc_cold_chain_Z where unid='" + tbGzbdh.Text + "'", CommFunction.ConnectString);

            //清空数据
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = null;
            tbPKT.Text = null;
            tbCk.Text = null;
            tbHz.Text = null;
            tbHzqd.Text = null;
            tbXdsj.Text = null;
            tbDjzt.Text = null;
            tbBch.Text = null;
            tbKhmc.Text = null;
            tbShdz.Text = null;
            //nuCfmax.Value = 0;
            //nuMdmax.Value = 0;
            //nuFhwd.Value = 0;
            //nuCfmin.Value = 0;
            //nuMdmin.Value = 0;
            chbLcc.Checked = false;
            cbWdjpfr.Text = null;

            tbGzbdh.Text = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;

            if ((tbGzbdh.Text.Trim() == "") || tbGzbdh.Text == null) { return; }

            LYFunctionCs.frmReportView frv = new LYFunctionCs.frmReportView();

            ReportDocument Rpt = new ReportDocument();

            string pRptNmae = "";
            pRptNmae = LYFunctionCs.LYFunctionCsClass.cmdScalar("select cm.report_file from cc_cold_chain_c cc left join cc_cold_chain_z cz on cc.pkunid=cz.unid left join cc_item_master cm on cc.size_desc=cm.size_desc and cm.season=cz.season where pkunid='" + tbGzbdh.Text + "' and  cm.report_file is not null and rownum=1 ", CommFunction.ConnectString);
            if ((pRptNmae.Trim() == "") || (pRptNmae == null))
                pRptNmae = "Rep_CC_ITEM_TRACK.rpt";

            Rpt.Load(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + pRptNmae);

            TableLogOnInfo logOnInfo = new TableLogOnInfo();
            logOnInfo.ConnectionInfo.ServerName = CommFunction.cfServerName;
            logOnInfo.ConnectionInfo.UserID = CommFunction.cfUserID;
            logOnInfo.ConnectionInfo.Password = CommFunction.cfPassword;

            Rpt.Database.Tables[0].ApplyLogOnInfo(logOnInfo);
            //Rpt.Refresh();

            frv.crystalReportViewer1.ReportSource = Rpt;


            CrystalDecisions.Shared.ParameterValues mypaVal = new CrystalDecisions.Shared.ParameterValues();
            CrystalDecisions.Shared.ParameterDiscreteValue mypaDVal = new CrystalDecisions.Shared.ParameterDiscreteValue();
            mypaDVal.Value = tbGzbdh.Text;

            mypaVal.Add(mypaDVal);
            Rpt.DataDefinition.ParameterFields["GZBDH"].ApplyCurrentValues(mypaVal);

            frv.ShowDialog();

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            //string ResultSelect = "";
            string SqlSeach = "select cz.unid as 跟踪单号, whse as 仓库,season as 货主,pkt_ctrl_nbr as 物流PKT号,major_pkt_grp_attr as 货主清单号,create_date_time as 下单时间,"+
                               "'' as 单据状态,'' as 波次号,shipto as 客户代码,shipto_name as 客户名称,shipto_addr as 送货地址,pkt_seq_nbr as 物流PKT序号,"+
                               "assort_nbr as 货主业务编号,size_desc as 商品代码,sku_desc as 品名规格,orig_pkt_qty as 数量,batch_nbr as 批号,units as 单位,"+
                               "vendor_name as 供应商,manufacturer as 生产厂家,case when cz.user_therm is not null then '附温度计' else '' end as 是否需附温度计,therm_company 温度计所属公司,pack_type 包装类型 "+ 
                               "from cc_cold_chain_c cc "+
                               "left join cc_cold_chain_z cz on cz.unid=cc.pkunid "+
                               "left join cc_item_master cm on cm.size_desc=cc.size_desc and cm.season=cz.season where cz.unid='" + toolStripTextBox1.Text +
                               "' order by pkt_ctrl_nbr,pkt_seq_nbr ";
            //if (CommFunction.ShowQueryForm(this.Handle, CommFunction.ConnectString, SqlSeach, 3, ref ResultSelect) == false) { return; }

            OleDbDataAdapter sda = new OleDbDataAdapter(SqlSeach, CommFunction.ConnectString);
            DataSet ds = new DataSet();
            sda.Fill(ds, "chk_con");
            dataGridView1.DataSource = ds.Tables["chk_con"];
            dataGridView1.AutoResizeColumns();
            tbPKT.Focus();
            tbPKT.SelectAll();

            if (dataGridView1.Rows.Count <= 0) return;
            string Sqlcha1 = "select MIN_TEMP_SRC,MAX_TEMP_SRC,MIN_TEMP_DEST,MAX_TEMP_DEST,CUR_TEMP_SRC,FULL_COLD_CAR,USER_THERM from cc_cold_chain_z where unid='" + dataGridView1.Rows[0].Cells["跟踪单号"].Value.ToString() + "'";
            OleDbDataAdapter sda1;
            sda1 = new OleDbDataAdapter(Sqlcha1, CommFunction.ConnectString);
            DataTable ATable = new DataTable();
            sda1.Fill(ATable);

            nuCfmin.Value = int.Parse(ATable.Rows[0][0].ToString());
            nuCfmax.Value = int.Parse(ATable.Rows[0][1].ToString());
            nuMdmin.Value = int.Parse(ATable.Rows[0][2].ToString());
            nuMdmax.Value = int.Parse(ATable.Rows[0][3].ToString());
            nuFhwd.Value = int.Parse(ATable.Rows[0][4].ToString());
            if (ATable.Rows[0][5].ToString() == "1") { chbLcc.Checked = true; } else { chbLcc.Checked = false; }
            cbWdjpfr.Text = ATable.Rows[0][6].ToString();


            cd = new ColdChain_dtl[dataGridView1.Rows.Count];//查询跟踪单 和 待跟踪单 共享一个全局变量结构体数组

            for (int i = 0; i < cd.Length; i++)
            {
                string Sqlcha2 = "select PACK_TIPS,PACK_NUM from cc_cold_chain_c where ASSORT_NBR='" + dataGridView1.Rows[i].Cells["货主业务编号"].Value.ToString() + "' and PKT_SEQ_NBR='" + dataGridView1.Rows[i].Cells["物流PKT序号"].Value.ToString() + "'";
                OleDbDataAdapter sda2;
                sda2 = new OleDbDataAdapter(Sqlcha2, CommFunction.ConnectString);
                DataTable ETable = new DataTable();
                sda2.Fill(ETable);

                cd[i].pBzts = ETable.Rows[0][0].ToString();
                cd[i].pBzjs = ETable.Rows[0][1].ToString();

                OleDbDataAdapter sda3;
                string strWDJ = "select THERM_ID from cc_cold_chain_therm WHERE PKT_SEQ_NBR='" + dataGridView1.Rows[i].Cells["物流PKT序号"].Value.ToString() + "' and PKUNID=(select unid from cc_cold_chain_c where PKUNID='" + dataGridView1.Rows[0].Cells["跟踪单号"].Value.ToString() + "' and cc_cold_chain_therm.pkt_seq_nbr=cc_cold_chain_c.pkt_seq_nbr)";
                sda3 = new OleDbDataAdapter(strWDJ, CommFunction.ConnectString);
                DataSet WDJTable = new DataSet();
                sda3.Fill(WDJTable);
                string temp = "";
                for (int wdjNum = 0; wdjNum < WDJTable.Tables[0].Rows.Count; wdjNum++)
                {
                    temp = WDJTable.Tables[0].Rows[wdjNum][0] + ";" + temp;

                }
                cd[i].pWdj = temp;

                OleDbDataAdapter sda4;
                string strBP = "select ICE_ID,ICE_NAME,ICE_QTY from cc_cold_chain_ice WHERE PKT_SEQ_NBR='" + dataGridView1.Rows[i].Cells["物流PKT序号"].Value.ToString() + "' and PKUNID=(select unid from cc_cold_chain_c where PKUNID='" + dataGridView1.Rows[0].Cells["跟踪单号"].Value.ToString() + "' and  cc_cold_chain_ice.pkt_seq_nbr=cc_cold_chain_c.pkt_seq_nbr)";
                sda4 = new OleDbDataAdapter(strBP, CommFunction.ConnectString);
                DataSet BPTable = new DataSet();
                sda4.Fill(BPTable);
                FrmPack.pBd = new FrmPack.Bp_dtl[BPTable.Tables[0].Rows.Count];
                cd[i].bd = new Bp_dtl[BPTable.Tables[0].Rows.Count];
                if (BPTable.Tables[0].Rows.Count == 0) { cd[i].bd = null; }
                else
                {
                    for (int bpNum = 0; bpNum < cd[i].bd.Length; bpNum++)
                    {
                        cd[i].bd[bpNum].pIce_Id = BPTable.Tables[0].Rows[bpNum][0].ToString();
                        cd[i].bd[bpNum].pIce_Name = BPTable.Tables[0].Rows[bpNum][1].ToString();
                        cd[i].bd[bpNum].pIce_Qty = BPTable.Tables[0].Rows[bpNum][2].ToString();
                    }
                }
            }
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue != 13) return;
            toolStripButton3_Click(null,null);
            toolStripTextBox1.Focus();
            toolStripTextBox1.SelectAll();
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //右键选中行
            if (e.Button == MouseButtons.Right && e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                dataGridView1.CurrentRow.Selected = false;
                dataGridView1.Rows[e.RowIndex].Selected = true;

            }

        }

    }
}

