using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Windows.Forms;
using LYFunctionCs;

namespace LoginCs
{
    public partial class frmLOG : Form
    {
        bool ifNotCanClose=true;
        private static frmLOG pUniqueForm = null;//����Ψһ�򿪴���

        private void frmLOG_FormClosed(object sender, FormClosedEventArgs e)
        {
            pUniqueForm = null;//����Ψһ�򿪴���
        }

        /// <summary>
        /// ����Ψһ�򿪴���
        /// </summary>
        /// <param name="Path">�Ƿ���Ϊģʽ�����</param>
        public static void ShowUniqueForm(bool AIfShowModal)
        {
            if (pUniqueForm == null)
            {
                new frmLOG();

                if (AIfShowModal) pUniqueForm.ShowDialog(); else pUniqueForm.Show();
            }
        }

        public frmLOG()
        {
            pUniqueForm = this;//����Ψһ�򿪴���
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ifNotCanClose = false;
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt16(LYFunctionCsClass.cmdScalar("select count(*) from ysfs where type='ְԱ' and UPPER(ysid)='" + textBox1.Text.ToUpper() + "'", LoginClass.ConnectString));
            if (i == 0)
            {
                MessageBox.Show("�û������ڣ�����������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                textBox1.SelectAll();
                return;
            }

            int j = Convert.ToInt16(LYFunctionCsClass.cmdScalar("select count(*) from ysfs where type='ְԱ' and UPPER(ysid)='" + textBox1.Text.ToUpper() + "' and UPPER(nvl(RESERVE1,'!@#$%^&*()'))='" + (textBox2.Text == "" ? "!@#$%^&*()" : textBox2.Text).ToString() + "'", LoginClass.ConnectString));
            if (j == 0)
            {
                MessageBox.Show("���벻��ȷ������������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                textBox2.SelectAll();
                return;
            }
                
            LoginClass.LogID = textBox1.Text;

            ifNotCanClose = false;
            this.Close();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.ToUpper();
            OleDbConnection con = new OleDbConnection(LoginClass.ConnectString);
            con.Open();
            OleDbCommand llogname = new OleDbCommand("select ysfs from ysfs where type='ְԱ' and upper(ysid)='" + textBox1.Text.ToUpper() + "'", con);

            if (llogname.ExecuteScalar() == null)
            {
                textBox3.Text = "";
            }
            else
            {
                LoginClass.LogName = llogname.ExecuteScalar().ToString();
                textBox3.Text = LoginClass.LogName;
            }
        }

        private void frmLOG_KeyPress(object sender, KeyPressEventArgs e)
        {
            //C#��Windowsͨ�õĻس�תTab����
            //��Form��KeyPreView��Ϊtrue��Ȼ����Form��KeyPress���������д��뼴��
            if (e.KeyChar == '\r')
                SelectNextControl(ActiveControl, true, true, true, true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.label5.Visible = true;
            this.label6.Visible = true;
            this.textBox4.Visible = true;
            this.textBox5.Visible = true;
            if (textBox4.CanFocus) textBox4.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt16(LYFunctionCsClass.cmdScalar("select count(*) from ysfs where type='ְԱ' and UPPER(ysid)='" + textBox1.Text.ToUpper() + "'", LoginClass.ConnectString));
            if (i == 0)
            {
                MessageBox.Show("�û������ڣ�����������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                textBox1.SelectAll();
                return;
            }

            int j = Convert.ToInt16(LYFunctionCsClass.cmdScalar("select count(*) from ysfs where type='ְԱ' and UPPER(ysid)='" + textBox1.Text.ToUpper() + "' and UPPER(nvl(RESERVE1,'!@#$%^&*()'))='" + (textBox2.Text == "" ? "!@#$%^&*()" : textBox2.Text).ToString() + "'", LoginClass.ConnectString));
            if (j == 0)
            {
                MessageBox.Show("���벻��ȷ������������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                textBox2.SelectAll();
                return;
            }

            if (textBox4.Text.ToUpper() != textBox5.Text.ToUpper()) {
                MessageBox.Show("\"������\"��\"ȷ��������\"�����","��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            LYFunctionCsClass.cmd("update ysfs set RESERVE1='" + textBox5.Text + "' where type='ְԱ' and upper(ysid)='" + textBox1.Text.ToUpper() + "'", LoginClass.ConnectString);

            label5.Visible = false;
            label6.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;

            MessageBox.Show("�����޸��ڳɹ�,�����������¼!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frmLOG_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = ifNotCanClose;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}