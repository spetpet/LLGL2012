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
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 商品资料ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            FrmItemMaster.ShowUniqueForm(true);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            CommFunction.MakeDBConn_HCWL();
            LoginCs.frmLOG.ShowUniqueForm(true);
            tSLabel1.Text = " 当前用户："+LoginCs.LoginClass.LogName;
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void 通用配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            FrmCommCode.ShowUniqueForm(true);
        }

        private void 发货录入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            FrmColdChainNew.ShowUniqueForm(true);
        }

        private void 复核确认ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            FrmColdCheck.ShowUniqueForm(true);
        }

        private void 跟踪记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            FrmGZ.ShowUniqueForm(true);
        }

        private void 温度计返还ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            FrmRGBack.ShowUniqueForm(true);
        }

        private void 综合查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            FrmColdQurey.ShowUniqueForm(true);
        }

        private void 天未返还温度计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            FrmRGNotRe.ShowUniqueForm(true);
        }

        private void 客户资料ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            FrmColdVendor.ShowUniqueForm(true);
        }

        private void 冰排字典ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            FrmIceMaster.ShowUniqueForm(true);
        }

        private void 冰排库位ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            FrmIceLocn.ShowUniqueForm(true);
        }

        private void 冰排库存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            FrmIceQurey.ShowUniqueForm(true);
        }

        private void 冰排操作ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!PowerCheckCsClass.IfHasPower(LoginClass.LogID, CommFunction.GSYSNAME, sender, CommFunction.ConnectString)) return;
            FrmIceOperate.ShowUniqueForm(true);
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAbout.ShowUniqueForm(true);
        }
    }
}
