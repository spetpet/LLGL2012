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
using ss_getcitycode;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using Newtonsoft.Json;



namespace LLGL2012
{
    public partial class FrmColdChainNew : Form
    {
        private static FrmColdChainNew pUniqueForm = null;//窗体唯一打开代码
        private static string pShowday = "3";
        private static string pYpwd = "0";
        private static string pPrint = "";
        private static string pSave = "";
        private static string pSaveWdj = "";

        static ss_getcitycode.getname gname = new ss_getcitycode.getname();
        static string json_str = gname.getcity("city.json");
        List<ss_getcitycode.province> province1 = JsonConvert.DeserializeObject<List<ss_getcitycode.province>>(json_str);

        /// <summary>
        /// 窗体唯一打开代码
        /// </summary>
        /// <param name="Path">是否作为模式窗体打开</param>
        public static void ShowUniqueForm(bool AIfShowModal)
        {
            if (pUniqueForm == null)
            {
                new FrmColdChainNew();

                if (AIfShowModal) pUniqueForm.ShowDialog(); else pUniqueForm.Show();
            }
        }


        public FrmColdChainNew()
        {
            pUniqueForm = this;//窗体唯一打开代码
            InitializeComponent();
        }

        private void FrmColdChainNew_FormClosed(object sender, FormClosedEventArgs e)
        {
            pUniqueForm = null;//窗体唯一打开代码
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

           // if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;

            FrmSearchCcPkt.ShowUniqueForm(true);

            if (FrmSearchCcPkt.pifsave == "True")
            {

                OleDbDataAdapter sda;
                string strR2 = "select retail_price as 货值,whse as 仓库,season as 货主,pkt_ctrl_nbr as 物流PKT号,major_pkt_grp_attr as 货主清单号,create_date_time as 下单时间," +
                                  "stat_code as 单据状态,pick_wave_nbr as 波次号,shipto as 客户代码,shipto_name as 客户名称,shipto_addr as 送货地址,pkt_seq_nbr as 物流PKT序号," +
                                  "assort_nbr as 货主业务编号,size_desc as 商品代码,sku_desc as 品名规格,orig_pkt_qty as 数量,batch_nbr as 批号,units as 单位," +
                                  "vendor_name as 供应商,manufacturer as 生产厂家,ifthermometer as 是否需附温度计,therm_company 温度计所属公司,pack_type_z 整件包装类型,pack_type_l 零散包装类型,zxs 整箱数,ls 零数 " +
                                  " from  v_cc_pkt where pkt_ctrl_nbr='" + FrmSearchCcPkt.pPkt + "' order by pkt_ctrl_nbr,pkt_seq_nbr";
                sda = new OleDbDataAdapter(strR2, CommFunction.ConnectString);
                DataSet LLTable = new DataSet();
                sda.Fill(LLTable);
                if (LLTable.Tables[0].Rows.Count > 0)
                {

                    //保存操作
                    if (MessageBox.Show("确实要新增一张跟踪单吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        pSave = "False";
                        tbUnid.Text = "";
                        tbPKT.Text = FrmSearchCcPkt.pPkt;
                        tbCk.Text = LLTable.Tables[0].Rows[0]["仓库"].ToString();
                        tbHz.Text = LLTable.Tables[0].Rows[0]["货主"].ToString();
                        tbHzqd.Text = LLTable.Tables[0].Rows[0]["货主清单号"].ToString();
                        tbXdsj.Text = LLTable.Tables[0].Rows[0]["下单时间"].ToString();
                        tbDjzt.Text = LLTable.Tables[0].Rows[0]["单据状态"].ToString();
                        tbBch.Text = LLTable.Tables[0].Rows[0]["波次号"].ToString();
                        tbKhmc.Text = LLTable.Tables[0].Rows[0]["客户名称"].ToString();
                        tbShdz.Text = LLTable.Tables[0].Rows[0]["送货地址"].ToString();

                        OleDbConnection con = new OleDbConnection(CommFunction.ConnectString);
                        con.Open();

                        OleDbCommand ocmd = new OleDbCommand("SP_INSERT_LLGZ", con);
                        ocmd.CommandType = CommandType.StoredProcedure;

                        //此处sourceColumn参数没什么用(不用该参数也可以).参数值必须按该存储过程中的参数顺序进行传递

                        ocmd.Parameters.Add("A", OleDbType.VarChar, 50, "pWHSE").Value = LLTable.Tables[0].Rows[0]["仓库"];
                        //Parameters.Add的第一个参数parameterName无实际意义，可随意输入

                        ocmd.Parameters.Add("B", OleDbType.VarChar, 100, "pSEASON").Value = LLTable.Tables[0].Rows[0]["货主"];
                        ocmd.Parameters.Add("C", OleDbType.VarChar, 100, "pSHIPTO").Value = LLTable.Tables[0].Rows[0]["客户代码"];
                        ocmd.Parameters.Add("D", OleDbType.VarChar, 100, "pSHIPTO_NAME").Value = LLTable.Tables[0].Rows[0]["客户名称"];
                        ocmd.Parameters.Add("E", OleDbType.VarChar, 100, "pSHIPTO_ADDR").Value = LLTable.Tables[0].Rows[0]["送货地址"];
                        ocmd.Parameters.Add("F", OleDbType.VarChar, 100, "pPKT_CTRL_NBR").Value = LLTable.Tables[0].Rows[0]["物流PKT号"];
                        ocmd.Parameters.Add("G", OleDbType.VarChar, 100, "pMAJOR_PKT_GRP_ATTR").Value = LLTable.Tables[0].Rows[0]["货主清单号"];
                        ocmd.Parameters.Add("H", OleDbType.VarChar, 100, "pUSER_NAME").Value = LoginCs.LoginClass.LogName;
                        ocmd.Parameters.Add("I", OleDbType.VarChar, 100, "pUSER_THERM").Value = "";//cbWdjpfr.Text.Trim();
                        ocmd.Parameters.Add("J", OleDbType.VarChar, 100, "pUSER_CHECK").Value = "";
                        ocmd.Parameters.Add("K", OleDbType.VarChar, 100, "pCHECK_DATE_TIME").Value = "";//未知
                        ocmd.Parameters.Add("L", OleDbType.VarChar, 100, "pMIN_TEMP_SRC").Value = "";// nuCfmin.Value;
                        ocmd.Parameters.Add("M", OleDbType.VarChar, 100, "pMAX_TEMP_SRC").Value = "";// nuCfmax.Value;
                        ocmd.Parameters.Add("N", OleDbType.VarChar, 100, "pMIN_TEMP_DEST").Value = "";// nuMdmin.Value;
                        ocmd.Parameters.Add("O", OleDbType.VarChar, 100, "pMAX_TEMP_DEST").Value = "";// nuMdmax.Value;
                        ocmd.Parameters.Add("P", OleDbType.VarChar, 100, "pCUR_TEMP_SRC").Value = "";// nuFhwd.Value;
                        ocmd.Parameters.Add("Q", OleDbType.VarChar, 100, "pFULL_COLD_CAR").Value = "";// chbLcc.Checked ? "1" : "0";
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
                        //ocmd.Parameters.Add("ZA", OleDbType.VarChar, 100, "pRETAIL_PRICE").Value = "";//未知;

                        OleDbParameter pUNID = ocmd.Parameters.Add("GZDH", OleDbType.VarChar, 100, "pUNID");//参数类型的变量，取名时也可随意

                        pUNID.Direction = ParameterDirection.Output;

                        ocmd.ExecuteNonQuery();

                        string UNID = ocmd.Parameters["GZDH"].Value.ToString().Trim(Convert.ToChar(0x0));//此处需用到Parameters.Add的第一个参数parameterName

                        con.Close();
                        con.Dispose();



                        for (int i = 0; i <= LLTable.Tables[0].Rows.Count - 1; i++)
                        {
                            OleDbConnection con2 = new OleDbConnection(CommFunction.ConnectString);
                            con2.Open();

                            OleDbCommand ocmd2 = new OleDbCommand("SP_INSERT_LLGZ_C", con2);
                            ocmd2.CommandType = CommandType.StoredProcedure;

                            //此处sourceColumn参数没什么用(不用该参数也可以).参数值必须按该存储过程中的参数顺序进行传递

                            ocmd2.Parameters.Add("A", OleDbType.VarChar, 50, "pPKUNID").Value = int.Parse(UNID);
                            //Parameters.Add的第一个参数parameterName无实际意义，可随意输入

                            ocmd2.Parameters.Add("B", OleDbType.VarChar, 100, "pPKT_SEQ_NBR").Value = LLTable.Tables[0].Rows[i]["物流PKT序号"].ToString();
                            ocmd2.Parameters.Add("C", OleDbType.VarChar, 100, "pASSORT_NBR").Value = LLTable.Tables[0].Rows[i]["货主业务编号"].ToString();
                            ocmd2.Parameters.Add("D", OleDbType.VarChar, 100, "pSIZE_DESC").Value = LLTable.Tables[0].Rows[i]["商品代码"].ToString();
                            ocmd2.Parameters.Add("E", OleDbType.VarChar, 100, "pSKU_DESC").Value = LLTable.Tables[0].Rows[i]["品名规格"].ToString();
                            ocmd2.Parameters.Add("F", OleDbType.VarChar, 100, "pBATCH_NBR").Value = LLTable.Tables[0].Rows[i]["批号"].ToString();
                            ocmd2.Parameters.Add("G", OleDbType.VarChar, 100, "pORIG_PKT_QTY").Value = LLTable.Tables[0].Rows[i]["数量"].ToString();
                            ocmd2.Parameters.Add("H", OleDbType.VarChar, 100, "pUNITS").Value = LLTable.Tables[0].Rows[i]["单位"].ToString(); ;
                            ocmd2.Parameters.Add("I", OleDbType.VarChar, 100, "pUSER_NAME").Value = LoginCs.LoginClass.LogName;
                            ocmd2.Parameters.Add("J", OleDbType.VarChar, 100, "pVENDOR_NAME").Value = LLTable.Tables[0].Rows[i]["供应商"].ToString();
                            ocmd2.Parameters.Add("K", OleDbType.VarChar, 100, "pPACK_TIPS_Z").Value = "";//cd[i].pBzts == null ? "" : cd[i].pBzts;
                            ocmd2.Parameters.Add("L", OleDbType.VarChar, 100, "pPACK_NUM_Z").Value = "";//cd[i].pBzjs == null ? "0" : cd[i].pBzjs;
                            ocmd2.Parameters.Add("M", OleDbType.VarChar, 100, "pPACK_TIPS_L").Value = "";//cd[i].pBzts == null ? "" : cd[i].pBzts;
                            ocmd2.Parameters.Add("N", OleDbType.VarChar, 100, "pPACK_NUM_L").Value = "";//cd[i].pBzjs == null ? "0" : cd[i].pBzjs;
                            ocmd2.Parameters.Add("O", OleDbType.VarChar, 100, "pIFTHERMOMETER").Value = LLTable.Tables[0].Rows[i]["是否需附温度计"].ToString();
                            ocmd2.Parameters.Add("P", OleDbType.VarChar, 100, "pTHERM_COMPANY").Value = LLTable.Tables[0].Rows[i]["温度计所属公司"].ToString();
                            ocmd2.Parameters.Add("Q", OleDbType.VarChar, 100, "pPACK_TYPE_Z").Value = LLTable.Tables[0].Rows[i]["整件包装类型"].ToString();
                            ocmd2.Parameters.Add("R", OleDbType.VarChar, 100, "pPACK_TYPE_L").Value = LLTable.Tables[0].Rows[i]["零散包装类型"].ToString();
                            ocmd2.Parameters.Add("S", OleDbType.VarChar, 100, "pZXS").Value = LLTable.Tables[0].Rows[i]["整箱数"].ToString();
                            ocmd2.Parameters.Add("T", OleDbType.VarChar, 100, "pLS").Value = LLTable.Tables[0].Rows[i]["零数"].ToString();
                            ocmd2.Parameters.Add("U", OleDbType.VarChar, 100, "pRETAIL_PRICE").Value = LLTable.Tables[0].Rows[i]["货值"].ToString();


                            OleDbParameter pUNID_C = ocmd2.Parameters.Add("GZDH_C", OleDbType.VarChar, 100, "pUNID_C");//参数类型的变量，取名时也可随意

                            pUNID_C.Direction = ParameterDirection.Output;

                            ocmd2.ExecuteNonQuery();

                            string UNID_C = ocmd2.Parameters["GZDH_C"].Value.ToString().Trim(Convert.ToChar(0x0));//此处需用到Parameters.Add的第一个参数parameterName

                            con2.Close();
                            con2.Dispose();

                        }

                        OleDbConnection con3 = new OleDbConnection(CommFunction.ConnectString);
                        con3.Open();
                        string SqlSeach = "select  unid as 跟踪单号,whse 仓库,season 货主,shipto 客户代码,shipto_name 客户名称,shipto_addr 送货地址, " +
                                          "pkt_ctrl_nbr PKT号,major_pkt_grp_attr 货主清单号,create_date_time 创建时间,user_name 用户名称,user_therm 温度计配附人, " +
                                          "user_check 复核人,check_date_time 复核时间,nvl(min_temp_src,0) 出发地最低温度,nvl(max_temp_src,0) 出发地最高温度,nvl(min_temp_dest,0) 目的地最低温度, " +
                                          "nvl(max_temp_dest,0) 目的地最高温度,nvl(cur_temp_src,0) 发货时出发地温度,full_cold_car 是否全程冷藏车,user_pack 包装人,pack_date_time 包装时间, " +
                                          "carr_name 承运商名称,user_carr 承运商提货人,carr_date_time 承运商提货时间,user_sign 签收人,sign_date_time 签收时间, " +
                                          "sign_temp_envi 签收时环境温度,sign_temp_drugs 签收时药品温度,sign_memo 签收的备注,ifprint 打印标志 " +
                                          "from cc_cold_chain_z where  create_date_time>= sysdate-" + pShowday + pPrint + " order by create_date_time desc";//where unid='" + UNID + "'";
                        OleDbDataAdapter sda3 = new OleDbDataAdapter(SqlSeach, CommFunction.ConnectString);
                        DataSet ds3 = new DataSet();
                        sda3.Fill(ds3, "ccz_con");
                        dataGridView1.DataSource = ds3.Tables["ccz_con"];
                        dataGridView1.AutoResizeColumns();
                        con3.Close();
                        con3.Dispose();

                        tbUnid.Text = UNID;
                        
                    }

                }
            }

        }

        private void FrmColdChainNew_Load(object sender, EventArgs e)
        {
            day_comboBox.SelectedIndex = 0;
            day_comboBox.Visible = false;
            dateTimePicker1.MinDate = DateTime.Now;
            DateTime dt =DateTime.Now;
            dt=dt.AddDays(5.0);

            dateTimePicker1.MaxDate = dt;
            ss_getcitycode.getname gname = new ss_getcitycode.getname();
            string json_str = gname.getcity("city.json");
            List<ss_getcitycode.province> province1 = JsonConvert.DeserializeObject<List<ss_getcitycode.province>>(json_str);

            label25.Text = LoginClass.LogID;
            
            OleDbDataAdapter sda;
            string strR2 = "select ysfs from ysfs WHERE TYPE='冷链复核人' and nvl(flag,'!@#$%')!='停用'  ";
            sda = new OleDbDataAdapter(strR2, CommFunction.ConnectString);
            DataSet RYTable2 = new DataSet();
            sda.Fill(RYTable2);
            for (int i = 0; i < RYTable2.Tables[0].Rows.Count; i++)
            {
                cbWdjpfr.Items.Add(RYTable2.Tables[0].Rows[i][0]);

            }
            Readini();
            nuFhwd.Value = decimal.Parse(pYpwd);
            OleDbConnection con3 = new OleDbConnection(CommFunction.ConnectString);
            con3.Open();
            string SqlSeach = "select  unid as 跟踪单号,whse 仓库,season 货主,shipto 客户代码,shipto_name 客户名称,shipto_addr 送货地址, " +
                              "pkt_ctrl_nbr PKT号,major_pkt_grp_attr 货主清单号,create_date_time 创建时间,user_name 用户名称,user_therm 温度计配附人, " +
                              "user_check 复核人,check_date_time 复核时间,nvl(min_temp_src,0) 出发地最低温度,nvl(max_temp_src,0) 出发地最高温度,nvl(min_temp_dest,0) 目的地最低温度, " +
                              "nvl(max_temp_dest,0) 目的地最高温度,nvl(cur_temp_src,0) 发货时出发地温度,full_cold_car 是否全程冷藏车,user_pack 包装人,pack_date_time 包装时间, " +
                              "carr_name 承运商名称,user_carr 承运商提货人,carr_date_time 承运商提货时间,user_sign 签收人,sign_date_time 签收时间, " +
                              "sign_temp_envi 签收时环境温度,sign_temp_drugs 签收时药品温度,sign_memo 签收的备注,ifprint 打印标志 " +
                              "from cc_cold_chain_z where create_date_time>= sysdate-"+pShowday+pPrint+" order by create_date_time desc";
            OleDbDataAdapter sda3 = new OleDbDataAdapter(SqlSeach, CommFunction.ConnectString);
            DataSet ds3 = new DataSet();
            sda3.Fill(ds3, "ccz_con");
            dataGridView1.DataSource = ds3.Tables["ccz_con"];
            dataGridView1.AutoResizeColumns();
            if (dataGridView1.RowCount > 0)
            {
                tbPKT.Text = dataGridView1.Rows[0].Cells["PKT号"].Value.ToString();
                tbCk.Text = dataGridView1.Rows[0].Cells["仓库"].Value.ToString();
                tbHz.Text = dataGridView1.Rows[0].Cells["货主"].Value.ToString();
                tbHzqd.Text = dataGridView1.Rows[0].Cells["货主清单号"].Value.ToString();
                //tbXdsj.Text = dataGridView1.Rows[0].Cells["下单时间"].Value.ToString();
                //tbDjzt.Text = dataGridView1.Rows[0].Cells["单据状态"].Value.ToString();
                //tbBch.Text = dataGridView1.Rows[0].Cells["波次号"].Value.ToString();
                tbKhmc.Text = dataGridView1.Rows[0].Cells["客户名称"].Value.ToString();
                tbShdz.Text = dataGridView1.Rows[0].Cells["送货地址"].Value.ToString();
            }

            con3.Close();
            con3.Dispose();
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView2.Rows[e.RowIndex].Cells["是否需附温度计"].Value.ToString() == "附温度计")
            {
                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;
                pSaveWdj = "True";
            }
            else { pSaveWdj = ""; }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if ((sender as DataGridView).CurrentRow == null) return;//如果无此句,则点击dataGridView的列标题时会出错
            tbBzlxZ.Text = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["整件冷链包装提示"].Value.ToString();
            tbBjsZ.Text = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["整件冷链包装件数"].Value.ToString();
            tbBzlxL.Text = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["零散冷链包装提示"].Value.ToString();
            tbBjsL.Text = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["零散冷链包装件数"].Value.ToString();
            if (dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["是否需附温度计"].Value.ToString() == "附温度计")
            {
                cbWdjpfr.Enabled = true;
                新增温度计ToolStripMenuItem.Visible = true;
                删除温度计ToolStripMenuItem.Visible = true;             
            }
            else
            {
                cbWdjpfr.Enabled = false;
                新增温度计ToolStripMenuItem.Visible = false;
                删除温度计ToolStripMenuItem.Visible = false;
            }

            OleDbConnection con = new OleDbConnection(CommFunction.ConnectString);
            con.Open();
            string SqlSeach = "select therm_id 温度计编号,back_time 温度计返回时间,file_path 温度文件路径,memo 温度计返回备注,if_bad 温度计是否损坏 " +
                              "from cc_cold_chain_therm where pkunid='"
                              + dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["跟踪细单号"].Value.ToString() + "'";
            OleDbDataAdapter sda = new OleDbDataAdapter(SqlSeach, CommFunction.ConnectString);
            DataSet ds = new DataSet();
            sda.Fill(ds, "Therm_con");
            dataGridView4.DataSource = ds.Tables["Therm_con"];
            dataGridView4.AutoResizeColumns();
            con.Close();
            con.Dispose();

            OleDbConnection con1 = new OleDbConnection(CommFunction.ConnectString);
            con1.Open();
            SqlSeach = "select ice_id 冰排代码,ice_name 冰排名称,ice_qty 冰排数量,if_pick 冰排是否已拣,pick_user_name 拣冰人,pick_date_time 拣冰时间 " +
                              "from cc_cold_chain_ice where pkunid='"
                              + dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["跟踪细单号"].Value.ToString() + "'";
            OleDbDataAdapter sda1 = new OleDbDataAdapter(SqlSeach, CommFunction.ConnectString);
            DataSet ds1 = new DataSet();
            sda1.Fill(ds1, "Ice_con");
            dataGridView3.DataSource = ds1.Tables["Ice_con"];
            dataGridView3.AutoResizeColumns();
            con1.Close();
            con1.Dispose();
            
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

        private void cbBzlxZ_Click(object sender, EventArgs e)
        {
            cbBzlxZ.Items.Clear();
            if (dataGridView2.RowCount > 0)
            {
                string sBzlx = "select memo from ysfs where type='温度与包装对照表' and ysfs='"
                               + dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["整件包装类型"].Value.ToString() + "'"
                               + bzlx(dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["整件包装类型"].Value.ToString());
                string stra = LYFunctionCs.LYFunctionCsClass.cmdScalar(sBzlx, CommFunction.ConnectString);
                string[] strb;
                strb = stra.Split(';');
                for (int mxsl = 0; mxsl < strb.Length; mxsl++)
                {
                    cbBzlxZ.Items.Add(strb[mxsl].ToString());
                }
            }
        }


        private void tbUnid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue != 13) return;
            OleDbConnection con3 = new OleDbConnection(CommFunction.ConnectString);
            con3.Open();
            string SqlSeach = "select  unid as 跟踪单号,whse 仓库,season 货主,shipto 客户代码,shipto_name 客户名称,shipto_addr 送货地址, " +
                              "pkt_ctrl_nbr PKT号,major_pkt_grp_attr 货主清单号,create_date_time 创建时间,user_name 用户名称,user_therm 温度计配附人, " +
                              "user_check 复核人,check_date_time 复核时间,nvl(min_temp_src,0) 出发地最低温度,nvl(max_temp_src,0) 出发地最高温度,nvl(min_temp_dest,0) 目的地最低温度, " +
                              "nvl(max_temp_dest,0) 目的地最高温度,nvl(cur_temp_src,0) 发货时出发地温度,full_cold_car 是否全程冷藏车,user_pack 包装人,pack_date_time 包装时间, " +
                              "carr_name 承运商名称,user_carr 承运商提货人,carr_date_time 承运商提货时间,user_sign 签收人,sign_date_time 签收时间, " +
                              "sign_temp_envi 签收时环境温度,sign_temp_drugs 签收时药品温度,sign_memo 签收的备注,ifprint 打印标志 " +
                              "from cc_cold_chain_z where unid='" + tbUnid.Text + "'";
            OleDbDataAdapter sda3 = new OleDbDataAdapter(SqlSeach, CommFunction.ConnectString);
            DataSet ds3 = new DataSet();
            sda3.Fill(ds3, "ccz_con");
            dataGridView1.DataSource = ds3.Tables["ccz_con"];
            dataGridView1.AutoResizeColumns();
            if (dataGridView1.RowCount>0)
            {
                tbPKT.Text = dataGridView1.Rows[0].Cells["PKT号"].Value.ToString();
                tbCk.Text = dataGridView1.Rows[0].Cells["仓库"].Value.ToString();
                tbHz.Text = dataGridView1.Rows[0].Cells["货主"].Value.ToString();
                tbHzqd.Text = dataGridView1.Rows[0].Cells["货主清单号"].Value.ToString();
                //tbXdsj.Text = dataGridView1.Rows[0].Cells["下单时间"].Value.ToString();
                //tbDjzt.Text = dataGridView1.Rows[0].Cells["单据状态"].Value.ToString();
                //tbBch.Text = dataGridView1.Rows[0].Cells["波次号"].Value.ToString();
                tbKhmc.Text = dataGridView1.Rows[0].Cells["客户名称"].Value.ToString();
                tbShdz.Text = dataGridView1.Rows[0].Cells["送货地址"].Value.ToString();
                tbKhmc.Text = dataGridView1.Rows[0].Cells["客户名称"].Value.ToString();
                tbShdz.Text = dataGridView1.Rows[0].Cells["送货地址"].Value.ToString();
                nuCfmax.Value = decimal.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["出发地最高温度"].Value.ToString());
                nuCfmin.Value = decimal.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["出发地最低温度"].Value.ToString());
                nuMdmax.Value = decimal.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["目的地最高温度"].Value.ToString());
                nuMdmin.Value = decimal.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["目的地最低温度"].Value.ToString());
                nuFhwd.Value = decimal.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["发货时出发地温度"].Value.ToString());
                chbLcc.Checked = dataGridView1.Rows[0].Cells["是否全程冷藏车"].Value.ToString() == "1" ? true : false;
                cbWdjpfr.Text = dataGridView1.Rows[0].Cells["温度计配附人"].Value.ToString();
            }

            con3.Close();
            con3.Dispose();

            OleDbConnection con4 = new OleDbConnection(CommFunction.ConnectString);
            con4.Open();
            SqlSeach = "select retail_price as 货值,unid 跟踪细单号,pkt_seq_nbr PKT序号,assort_nbr 货主业务编号," +
                       "size_desc 商品代码,sku_desc 品名规格,batch_nbr 批号,orig_pkt_qty 数量,units 单位,vendor_name 供应商名称,pack_tips_z 整件冷链包装提示," +
                       "pack_num_z 整件冷链包装件数,pack_tips_l 零散冷链包装提示,pack_num_l 零散冷链包装件数,ifthermometer as 是否需附温度计,therm_company 温度计所属公司,pack_type_z 整件包装类型,pack_type_l 零散包装类型,zxs 整箱数,ls 零数 " +
                       "from cc_cold_chain_c where pkunid='" + tbUnid.Text + "' order by pkt_seq_nbr";

            OleDbDataAdapter sda4 = new OleDbDataAdapter(SqlSeach, CommFunction.ConnectString);
            DataSet ds4 = new DataSet();
            sda4.Fill(ds4, "ccc_con");
            dataGridView2.DataSource = ds4.Tables["ccc_con"];
            dataGridView2.AutoResizeColumns();
            con4.Close();
            con4.Dispose();
 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount <= 0) return;
            if ((tbBzlxZ.Text.Trim() == "") && (tbBzlxL.Text.Trim() == "")) { MessageBox.Show("请选择包装类型和包装件数！"); return; }
            if ((tbBzlxZ.Text.Trim() != "") && (tbBjsZ.Text.Trim() == "")) { MessageBox.Show("请填入包装件数！"); return; }
            if ((tbBzlxZ.Text.Trim() == "") && (tbBjsZ.Text.Trim() != "")) { MessageBox.Show("请选择包装类型！"); return; }
            if ((tbBzlxL.Text.Trim() != "") && (tbBjsL.Text.Trim() == "")) { MessageBox.Show("请填入包装件数！"); return; }
            if ((tbBzlxL.Text.Trim() == "") && (tbBjsL.Text.Trim() != "")) { MessageBox.Show("请选择包装类型！"); return; }
            
            string SqlUpt = "update cc_cold_chain_c set pack_tips_z='"+ tbBzlxZ.Text +"',pack_num_z='"+ tbBjsZ.Text +"',pack_tips_l='"+ tbBzlxL.Text +"',pack_num_l='"+ tbBjsL.Text +"' where unid='" + dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["跟踪细单号"].Value.ToString() + "' ";
            LYFunctionCs.LYFunctionCsClass.cmd(SqlUpt, CommFunction.ConnectString);

            OleDbConnection con4 = new OleDbConnection(CommFunction.ConnectString);
            con4.Open();
            string SqlSeach = "select retail_price as 货值,unid 跟踪细单号,pkt_seq_nbr PKT序号,assort_nbr 货主业务编号," +
                       "size_desc 商品代码,sku_desc 品名规格,batch_nbr 批号,orig_pkt_qty 数量,units 单位,vendor_name 供应商名称,pack_tips_z 整件冷链包装提示," +
                       "pack_num_z 整件冷链包装件数,pack_tips_l 零散冷链包装提示,pack_num_l 零散冷链包装件数,ifthermometer as 是否需附温度计,therm_company 温度计所属公司,pack_type_z 整件包装类型,pack_type_l 零散包装类型,zxs 整箱数,ls 零数 " +
                       "from cc_cold_chain_c where pkunid='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["跟踪单号"].Value.ToString() + "' order by pkt_seq_nbr";

            OleDbDataAdapter sda4 = new OleDbDataAdapter(SqlSeach, CommFunction.ConnectString);
            DataSet ds4 = new DataSet();
            sda4.Fill(ds4, "ccc_con");
            dataGridView2.DataSource = ds4.Tables["ccc_con"];
            dataGridView2.AutoResizeColumns();
            con4.Close();
            con4.Dispose();
            MessageBox.Show("保存类型成功！");
        }

        private void cbBzlx_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            if (MessageBox.Show("确实删除跟踪单  " + tbUnid.Text + "  吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                OleDbConnection cn = new OleDbConnection(CommFunction.ConnectString);
                cn.Open();
                //更新温度计状态表


                string Sqlrg = "select ct.therm_id from cc_cold_chain_therm ct where ct.pkunid in " +
                               "(select unid from cc_cold_chain_c where pkunid='" + tbUnid.Text + "')";

                OleDbDataAdapter sda;
                sda = new OleDbDataAdapter(Sqlrg, cn);
                DataTable rgTable = new DataTable();
                sda.Fill(rgTable);

                for (int rgsltemp = 0; rgsltemp < rgTable.Rows.Count; rgsltemp++)
                {
                    LYFunctionCs.LYFunctionCsClass.cmd("update ysfs set RESERVE1='在库' where type='温度计字典' and ysid='" + rgTable.Rows[rgsltemp][0] + "' ", CommFunction.ConnectString);
                }

                LYFunctionCs.LYFunctionCsClass.cmd("delete from cc_cold_chain_therm ct where ct.pkunid in (select unid from cc_cold_chain_c where pkunid='" + tbUnid.Text + "')", CommFunction.ConnectString);



                // 删除主表的记录



                LYFunctionCs.LYFunctionCsClass.cmd("delete from cc_cold_chain_ice ci where ci.pkunid in (select unid from cc_cold_chain_c where pkunid='" + tbUnid.Text + "' )", CommFunction.ConnectString);

                LYFunctionCs.LYFunctionCsClass.cmd("delete from cc_cold_chain_Z where unid='" + tbUnid.Text + "'", CommFunction.ConnectString);

                pSave = "";

                //清空数据
                //dataGridView1.AutoGenerateColumns = false;
                //dataGridView1.DataSource = null;
                //dataGridView2.AutoGenerateColumns = false;
                //dataGridView2.DataSource = null;
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
                nuFhwd.Value = decimal.Parse(pYpwd);
                chbLcc.Checked = false;
                cbWdjpfr.Text = null;

                tbUnid.Text = null;

                OleDbConnection con3 = new OleDbConnection(CommFunction.ConnectString);
                con3.Open();
                string SqlSeach = "select  unid as 跟踪单号,whse 仓库,season 货主,shipto 客户代码,shipto_name 客户名称,shipto_addr 送货地址, " +
                                  "pkt_ctrl_nbr PKT号,major_pkt_grp_attr 货主清单号,create_date_time 创建时间,user_name 用户名称,user_therm 温度计配附人, " +
                                  "user_check 复核人,check_date_time 复核时间,nvl(min_temp_src,0) 出发地最低温度,nvl(max_temp_src,0) 出发地最高温度,nvl(min_temp_dest,0) 目的地最低温度, " +
                                  "nvl(max_temp_dest,0) 目的地最高温度,nvl(cur_temp_src,0) 发货时出发地温度,full_cold_car 是否全程冷藏车,user_pack 包装人,pack_date_time 包装时间, " +
                                  "carr_name 承运商名称,user_carr 承运商提货人,carr_date_time 承运商提货时间,user_sign 签收人,sign_date_time 签收时间, " +
                                  "sign_temp_envi 签收时环境温度,sign_temp_drugs 签收时药品温度,sign_memo 签收的备注,ifprint 打印标志 " +
                                  "from cc_cold_chain_z where create_date_time>= sysdate-" + pShowday + pPrint +" order by create_date_time desc";
                OleDbDataAdapter sda3 = new OleDbDataAdapter(SqlSeach, CommFunction.ConnectString);
                DataSet ds3 = new DataSet();
                sda3.Fill(ds3, "ccz_con");
                dataGridView1.DataSource = ds3.Tables["ccz_con"];
                dataGridView1.AutoResizeColumns();
                if (dataGridView1.RowCount > 0)
                {
                    tbPKT.Text = dataGridView1.Rows[0].Cells["PKT号"].Value.ToString();
                    tbCk.Text = dataGridView1.Rows[0].Cells["仓库"].Value.ToString();
                    tbHz.Text = dataGridView1.Rows[0].Cells["货主"].Value.ToString();
                    tbHzqd.Text = dataGridView1.Rows[0].Cells["货主清单号"].Value.ToString();
                    //tbXdsj.Text = dataGridView1.Rows[0].Cells["下单时间"].Value.ToString();
                    //tbDjzt.Text = dataGridView1.Rows[0].Cells["单据状态"].Value.ToString();
                    //tbBch.Text = dataGridView1.Rows[0].Cells["波次号"].Value.ToString();
                    tbKhmc.Text = dataGridView1.Rows[0].Cells["客户名称"].Value.ToString();
                    tbShdz.Text = dataGridView1.Rows[0].Cells["送货地址"].Value.ToString();
                }

                con3.Close();
                con3.Dispose();
               
            }
        }

        private void tbBjs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8)
                e.Handled = true;
        }

        private void 新增温度计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView2.RowCount > 0)
            {
                if (cbWdjpfr.Text.Trim() == "") { MessageBox.Show("请选择 温度计配附人！"); return; }
                FrmEditTherm.pWdjgs = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["温度计所属公司"].Value.ToString();
                FrmEditTherm.pVendor_nbr = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["客户代码"].Value.ToString();
                FrmEditTherm.ShowUniqueForm(true);

                if ((FrmEditTherm.pTherm.Trim() != "") && (FrmEditTherm.pIfsave == "True"))
                {
                    string SqlUpt = "update ysfs set RESERVE1='已发' where type='温度计字典' and ysid='" + FrmEditTherm.pTherm + "' ";
                    LYFunctionCs.LYFunctionCsClass.cmd(SqlUpt, CommFunction.ConnectString);

                    string strwdrggz = "select * from CC_COLD_CHAIN_THERM t where t.pkunid='" 
                                       + dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["跟踪细单号"].Value.ToString()
                                       + "' and t.therm_id='" + FrmEditTherm.pTherm + "'";

                    OleDbDataAdapter sda1;
                    sda1 = new OleDbDataAdapter(strwdrggz, CommFunction.ConnectString);
                    DataSet rgTable = new DataSet();
                    sda1.Fill(rgTable);

                    if (rgTable.Tables[0].Rows.Count == 0)
                    {
                        string SqlIns = "insert into　CC_COLD_CHAIN_THERM (pkunid,therm_id,user_name) values('" + dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["跟踪细单号"].Value.ToString()
                                        + "','" + FrmEditTherm.pTherm + "','" + LoginCs.LoginClass.LogName  + "')";
                        LYFunctionCs.LYFunctionCsClass.cmd(SqlIns, CommFunction.ConnectString);

                    }

                    OleDbConnection con = new OleDbConnection(CommFunction.ConnectString);
                    con.Open();
                    string SqlSeach = "select therm_id 温度计编号,back_time 温度计返回时间,file_path 温度文件路径,memo 温度计返回备注,if_bad 温度计是否损坏 " +
                                      "from cc_cold_chain_therm where pkunid='"
                                      + dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["跟踪细单号"].Value.ToString() + "'";
                    OleDbDataAdapter sda = new OleDbDataAdapter(SqlSeach, CommFunction.ConnectString);
                    DataSet ds = new DataSet();
                    sda.Fill(ds, "Therm_con");
                    dataGridView4.DataSource = ds.Tables["Therm_con"];
                    dataGridView4.AutoResizeColumns();
                    con.Close();
                    con.Dispose();
                }
            }
            else { dataGridView4.AutoGenerateColumns = false; dataGridView4.DataSource = null; return; }
        }

        private void 删除温度计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView4.Rows.Count <= 0) return;

            LYFunctionCs.LYFunctionCsClass.cmd("update ysfs set RESERVE1='在库' where type='温度计字典' and ysid='" + dataGridView4.Rows[dataGridView4.CurrentCell.RowIndex].Cells["温度计编号"].Value.ToString() + "' ", CommFunction.ConnectString);

            LYFunctionCs.LYFunctionCsClass.cmd("delete from cc_cold_chain_therm ct where ct.pkunid ='" + dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["跟踪细单号"].Value.ToString()
                                                + "' and therm_id='" + dataGridView4.Rows[dataGridView4.CurrentCell.RowIndex].Cells["温度计编号"].Value.ToString() + "'", CommFunction.ConnectString);
            
            OleDbConnection con = new OleDbConnection(CommFunction.ConnectString);
            con.Open();
            string SqlSeach = "select therm_id 温度计编号,back_time 温度计返回时间,file_path 温度文件路径,memo 温度计返回备注,if_bad 温度计是否损坏 " +
                              "from cc_cold_chain_therm where pkunid='"
                              + dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["跟踪细单号"].Value.ToString() + "'";
            OleDbDataAdapter sda = new OleDbDataAdapter(SqlSeach, CommFunction.ConnectString);
            DataSet ds = new DataSet();
            sda.Fill(ds, "Therm_con");
            dataGridView4.DataSource = ds.Tables["Therm_con"];
            dataGridView4.AutoResizeColumns();
            con.Close();
            con.Dispose();

        }

        private void 新增冰排ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView2.RowCount > 0)
            {
                FrmEditIce.pPkunid = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["跟踪细单号"].Value.ToString();
                FrmEditIce.ShowUniqueForm(true);

                if (FrmEditIce.pIfsave == "True")
                {
                    string SqlIns = "insert into　cc_cold_chain_ice (PKUNID,ICE_ID,ICE_NAME,ICE_QTY,USER_NAME,IF_PICK) values('" +
                                                    dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["跟踪细单号"].Value.ToString() + "','" +
                                                    FrmEditIce.pBpdm + "','" + FrmEditIce.pBppm + FrmEditIce.pBpgg + "','" + FrmEditIce.pBpsl + "','"
                                                    + LoginCs.LoginClass.LogName + "','待拣')";
                    LYFunctionCs.LYFunctionCsClass.cmd(SqlIns, CommFunction.ConnectString);


                    OleDbConnection con1 = new OleDbConnection(CommFunction.ConnectString);
                    con1.Open();
                    string SqlSeach = "select ice_id 冰排代码,ice_name 冰排名称,ice_qty 冰排数量,if_pick 冰排是否已拣,pick_user_name 拣冰人,pick_date_time 拣冰时间 " +
                                      "from cc_cold_chain_ice where pkunid='"
                                      + dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["跟踪细单号"].Value.ToString() + "'";
                    OleDbDataAdapter sda1 = new OleDbDataAdapter(SqlSeach, CommFunction.ConnectString);
                    DataSet ds1 = new DataSet();
                    sda1.Fill(ds1, "Ice_con");
                    dataGridView3.DataSource = ds1.Tables["Ice_con"];
                    dataGridView3.AutoResizeColumns();
                    con1.Close();
                    con1.Dispose();
                }

            }
            else { dataGridView3.AutoGenerateColumns = false; dataGridView3.DataSource = null; return; }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            
            if (dataGridView1.RowCount <= 0) return;
            OleDbConnection con4 = new OleDbConnection(CommFunction.ConnectString);
            con4.Open();
            string SqlSeach = "select retail_price as 货值,unid 跟踪细单号,pkt_seq_nbr PKT序号,assort_nbr 货主业务编号," +
                       "size_desc 商品代码,sku_desc 品名规格,batch_nbr 批号,orig_pkt_qty 数量,units 单位,vendor_name 供应商名称,pack_tips_z 整件冷链包装提示," +
                       "pack_num_z 整件冷链包装件数,pack_tips_l 零散冷链包装提示,pack_num_l 零散冷链包装件数,ifthermometer as 是否需附温度计,therm_company 温度计所属公司,pack_type_z 整件包装类型,pack_type_l 零散包装类型,zxs 整箱数,ls 零数 " +
                       "from cc_cold_chain_c where pkunid='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["跟踪单号"].Value.ToString() + "' order by pkt_seq_nbr";

            OleDbDataAdapter sda4 = new OleDbDataAdapter(SqlSeach, CommFunction.ConnectString);
            DataSet ds4 = new DataSet();
            sda4.Fill(ds4, "ccc_con");
            dataGridView2.DataSource = ds4.Tables["ccc_con"];
            dataGridView2.AutoResizeColumns();
            con4.Close();
            con4.Dispose();

            if (dataGridView1.RowCount > 0)
            {
                tbPKT.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["PKT号"].Value.ToString();
                tbCk.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["仓库"].Value.ToString();
                tbHz.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["货主"].Value.ToString();
                tbHzqd.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["货主清单号"].Value.ToString();
                //tbXdsj.Text = dataGridView1.Rows[0].Cells["下单时间"].Value.ToString();
                //tbDjzt.Text = dataGridView1.Rows[0].Cells["单据状态"].Value.ToString();
                //tbBch.Text = dataGridView1.Rows[0].Cells["波次号"].Value.ToString();
                tbKhmc.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["客户名称"].Value.ToString();
                tbShdz.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["送货地址"].Value.ToString();
                nuCfmax.Value = decimal.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["出发地最高温度"].Value.ToString());
                nuCfmin.Value = decimal.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["出发地最低温度"].Value.ToString());
                nuMdmax.Value = decimal.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["目的地最高温度"].Value.ToString());
                nuMdmin.Value = decimal.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["目的地最低温度"].Value.ToString());
                nuFhwd.Value = decimal.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["发货时出发地温度"].Value.ToString());
                chbLcc.Checked = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["是否全程冷藏车"].Value.ToString() == "1" ? true : false;
                cbWdjpfr.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["温度计配附人"].Value.ToString();


                tbUnid.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["跟踪单号"].Value.ToString();
                if (pSave == "False") { dataGridView1.Enabled = false; return; } else { dataGridView1.Enabled = true; }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            if ((nuCfmax.Value == nuCfmin.Value) || (nuMdmax.Value == nuMdmin.Value))
             { MessageBox.Show("请正确填写温度！"); return; }
            
            for (int i = 0; i < dataGridView2.RowCount; i++) 
            {
                if ((dataGridView2.Rows[i].Cells["整件冷链包装提示"].ToString() == "") ||
                    (dataGridView2.Rows[i].Cells["整件冷链包装件数"].ToString() == "") ||
                        (dataGridView2.Rows[i].Cells["零散冷链包装件数"].ToString() == "") ||
                            (dataGridView2.Rows[i].Cells["零散冷链包装件数"].ToString() == ""))
                { MessageBox.Show("请确保为每个品种明细选择了包装类型和件数！"); return; }
            }

            if (dataGridView1.RowCount <= 0) return;
            //if ((cbWdjpfr.Enabled == true) && (cbWdjpfr.Text.Trim() == "")) { MessageBox.Show("请选择 温度计配附人！"); return; }
            if ((pSaveWdj == "True") && (dataGridView4.RowCount <= 0)) { MessageBox.Show("请先附温度计！"); return; }

            LYFunctionCs.LYFunctionCsClass.cmd("update cc_cold_chain_z set USER_THERM='" + cbWdjpfr.Text + "',MAX_TEMP_SRC= '" + nuCfmax.Value.ToString() +
                                               "', MIN_TEMP_SRC='" + nuCfmin.Value.ToString() + "',MAX_TEMP_DEST='" + nuMdmax.Value.ToString() +
                                               "', MIN_TEMP_DEST='" + nuMdmin.Value.ToString() + "', CUR_TEMP_SRC='" + nuFhwd.Value.ToString() + "',FULL_COLD_CAR='" + (chbLcc.Checked == true ? "1" : "0") +
                                               "' where unid='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["跟踪单号"].Value.ToString() + "'", CommFunction.ConnectString);
            MessageBox.Show("保存成功！");

            pSave = "";

            OleDbConnection con3 = new OleDbConnection(CommFunction.ConnectString);
            con3.Open();
            string SqlSeach = "select  unid as 跟踪单号,whse 仓库,season 货主,shipto 客户代码,shipto_name 客户名称,shipto_addr 送货地址, " +
                              "pkt_ctrl_nbr PKT号,major_pkt_grp_attr 货主清单号,create_date_time 创建时间,user_name 用户名称,user_therm 温度计配附人, " +
                              "user_check 复核人,check_date_time 复核时间,nvl(min_temp_src,0) 出发地最低温度,nvl(max_temp_src,0) 出发地最高温度,nvl(min_temp_dest,0) 目的地最低温度, " +
                              "nvl(max_temp_dest,0) 目的地最高温度,nvl(cur_temp_src,0) 发货时出发地温度,full_cold_car 是否全程冷藏车,user_pack 包装人,pack_date_time 包装时间, " +
                              "carr_name 承运商名称,user_carr 承运商提货人,carr_date_time 承运商提货时间,user_sign 签收人,sign_date_time 签收时间, " +
                              "sign_temp_envi 签收时环境温度,sign_temp_drugs 签收时药品温度,sign_memo 签收的备注,ifprint 打印标志 " +
                              "from cc_cold_chain_z where create_date_time>= sysdate-"+pShowday+pPrint+" order by create_date_time desc";
            OleDbDataAdapter sda3 = new OleDbDataAdapter(SqlSeach, CommFunction.ConnectString);
            DataSet ds3 = new DataSet();
            sda3.Fill(ds3, "ccz_con");
            dataGridView1.DataSource = ds3.Tables["ccz_con"];
            dataGridView1.AutoResizeColumns();
            if (dataGridView1.RowCount > 0)
            {
                tbPKT.Text = dataGridView1.Rows[0].Cells["PKT号"].Value.ToString();
                tbCk.Text = dataGridView1.Rows[0].Cells["仓库"].Value.ToString();
                tbHz.Text = dataGridView1.Rows[0].Cells["货主"].Value.ToString();
                tbHzqd.Text = dataGridView1.Rows[0].Cells["货主清单号"].Value.ToString();
                //tbXdsj.Text = dataGridView1.Rows[0].Cells["下单时间"].Value.ToString();
                //tbDjzt.Text = dataGridView1.Rows[0].Cells["单据状态"].Value.ToString();
                //tbBch.Text = dataGridView1.Rows[0].Cells["波次号"].Value.ToString();
                tbKhmc.Text = dataGridView1.Rows[0].Cells["客户名称"].Value.ToString();
                tbShdz.Text = dataGridView1.Rows[0].Cells["送货地址"].Value.ToString();
            }

            con3.Close();
            con3.Dispose();

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;

            if ((tbUnid.Text.Trim() == "") || tbUnid.Text == null) { return; }

            LYFunctionCs.frmReportView frv = new LYFunctionCs.frmReportView();

            ReportDocument Rpt = new ReportDocument();

            string pRptNmae = "";
            pRptNmae = LYFunctionCs.LYFunctionCsClass.cmdScalar("select cm.report_file from cc_cold_chain_c cc left join cc_cold_chain_z cz on cc.pkunid=cz.unid left join cc_item_master cm on cc.size_desc=cm.size_desc and cm.season=cz.season where pkunid='" + tbUnid.Text + "' and  cm.report_file is not null and rownum=1 ", CommFunction.ConnectString);
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
            mypaDVal.Value = tbUnid.Text;

            mypaVal.Add(mypaDVal);
            Rpt.DataDefinition.ParameterFields["GZBDH"].ApplyCurrentValues(mypaVal);

            frv.ShowDialog();

            LYFunctionCs.LYFunctionCsClass.cmd("update cc_cold_chain_z set ifprint='1' where UNID='"+tbUnid.Text+"'", CommFunction.ConnectString);
        }

        private void 删除冰排ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count <= 0) return;
            LYFunctionCs.LYFunctionCsClass.cmd("delete from cc_cold_chain_ice ci where ci.pkunid='" + dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["跟踪细单号"].Value.ToString() +
                                               "' and ice_id='" + dataGridView3.Rows[dataGridView3.CurrentCell.RowIndex].Cells["冰排代码"].Value.ToString() + "'", CommFunction.ConnectString);

            OleDbConnection con1 = new OleDbConnection(CommFunction.ConnectString);
            con1.Open();
            string SqlSeach = "select ice_id 冰排代码,ice_name 冰排名称,ice_qty 冰排数量,if_pick 冰排是否已拣,pick_user_name 拣冰人,pick_date_time 拣冰时间 " +
                              "from cc_cold_chain_ice where pkunid='"
                              + dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["跟踪细单号"].Value.ToString() + "'";
            OleDbDataAdapter sda1 = new OleDbDataAdapter(SqlSeach, CommFunction.ConnectString);
            DataSet ds1 = new DataSet();
            sda1.Fill(ds1, "Ice_con");
            dataGridView3.DataSource = ds1.Tables["Ice_con"];
            dataGridView3.AutoResizeColumns();
            con1.Close();
            con1.Dispose();

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (pSave == "False") { MessageBox.Show("请保存跟踪单后再退出！");return; }
            Close();
        }

        private void 显示记录设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmShowDay.ShowUniqueForm(true);
        }

        private void Readini()
        {
            string AppFile = Application.StartupPath + "\\";
            StringBuilder temp = new StringBuilder(255);
            CommFunction.GetPrivateProfileString("显示天数设置", "天数", "", temp, 255, AppFile + "ShowDay.ini");
            pShowday = temp.ToString();
            CommFunction.GetPrivateProfileString("默认药品温度设置", "药品温度", "", temp, 255, AppFile + "ShowDay.ini");
            pYpwd = temp.ToString();
            CommFunction.GetPrivateProfileString("打印设置", "是否打印", "", temp, 255, AppFile + "ShowDay.ini");
            if (temp.ToString().ToUpper()=="TRUE"){pPrint=" and ifprint='1'";}else{ pPrint="";}
            if ((pShowday.Trim()=="")||(pShowday==null)) {pShowday = "3";}
            if ((pYpwd.Trim() == "") || (pYpwd == null)) { pYpwd = "0"; }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbBzlxZ_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbBzlxZ.Text = cbBzlxZ.Text;
        }

        private void cbBzlxL_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbBzlxL.Text = cbBzlxL.Text;
        }

        private void cbBzlxL_Click(object sender, EventArgs e)
        {
            cbBzlxL.Items.Clear();
            if (dataGridView2.RowCount > 0)
            {
                string sBzlx = "select memo from ysfs where type='温度与包装对照表' and ysfs='"
                               + dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["零散包装类型"].Value.ToString() + "'"
                               + bzlx(dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["零散包装类型"].Value.ToString());
                string stra = LYFunctionCs.LYFunctionCsClass.cmdScalar(sBzlx, CommFunction.ConnectString);
                string[] strb;
                strb = stra.Split(';');
                for (int mxsl = 0; mxsl < strb.Length; mxsl++)
                {
                    cbBzlxL.Items.Add(strb[mxsl].ToString());
                }
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells["打印标志"].Value.ToString() == "1")
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
            }
        }

        private void FrmColdChainNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (pSave == "False") { MessageBox.Show("请保存跟踪单后再退出！"); e.Cancel = true; }
        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void province_comboBox_Click(object sender, EventArgs e)
        {
            province_comboBox.Items.Clear();
            
            foreach (ss_getcitycode.province pro_temp in province1)
            {
                province_comboBox.Items.Add(pro_temp.省.ToString());
            }
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void province_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            city_comboBox.Items.Clear();
            foreach (ss_getcitycode.city_instra city_temp in province1[province_comboBox.SelectedIndex].市)
            {
                city_comboBox.Items.Add(city_temp.市名.ToString());
            }
            city_comboBox.Text = province1[province_comboBox.SelectedIndex].市[0].市名.ToString();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (province_comboBox.Text == "") return;
            string temp_city = province1[province_comboBox.SelectedIndex].市[city_comboBox.SelectedIndex].编码.ToString();
            string temp_str = WeatherGet.GetWeather(temp_city);
            string[] temp_str1 =temp_str.Split(new char[] { ',' });
            int dt_cal = (dateTimePicker1.Value.AddDays(1.0) - DateTime.Now).Days;
            if (dt_cal == 0)
            {
                nuCfmax.Value = int.Parse(temp_str1[dt_cal]);
                nuCfmin.Value = int.Parse(temp_str1[dt_cal+1]);

            }
            else
            {
                nuCfmax.Value = int.Parse(temp_str1[dt_cal * 2]);
                nuCfmin.Value = int.Parse(temp_str1[dt_cal * 2+1]);
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (province_comboBox.Text == "") return;
            string temp_city = province1[province_comboBox.SelectedIndex].市[city_comboBox.SelectedIndex].编码.ToString();
            string temp_str = WeatherGet.GetWeather(temp_city);
            string[] temp_str1 = temp_str.Split(new char[] { ',' });
            int dt_cal = (dateTimePicker1.Value.AddDays(1.0) - DateTime.Now).Days;
            if (dt_cal == 0)
            {
                nuMdmax.Value = int.Parse(temp_str1[dt_cal ]);
                nuMdmin.Value = int.Parse(temp_str1[dt_cal+1]);

            }
            else
            {
                nuMdmax.Value = int.Parse(temp_str1[dt_cal * 2 ]);
                nuMdmin.Value = int.Parse(temp_str1[dt_cal * 2+1]);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (province_comboBox.Text == "") return;
            string temp_city = province1[province_comboBox.SelectedIndex].市[city_comboBox.SelectedIndex].编码.ToString();
            string temp_str = WeatherGet.GetWeather(temp_city);
            string[] temp_str1 = temp_str.Split(new char[] { ',' });
            if (day_comboBox.SelectedIndex == 0)
            {
                nuCfmax.Value = int.Parse(temp_str1[day_comboBox.SelectedIndex+1]);
                nuCfmin.Value = int.Parse(temp_str1[day_comboBox.SelectedIndex]);

            }
            else
            {
                nuCfmax.Value = int.Parse(temp_str1[day_comboBox.SelectedIndex * 2+1]);
                nuCfmin.Value = int.Parse(temp_str1[day_comboBox.SelectedIndex * 2]);
            }
        }

    }
}
