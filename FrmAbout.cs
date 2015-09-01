using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace LLGL2012
{
    partial class FrmAbout : Form
    {
        //public FrmAbout()
        //{
        //    InitializeComponent();
        //    this.Text = String.Format("关于 {0} {0}", AssemblyTitle);
        //    this.labelProductName.Text = AssemblyProduct;
        //    this.labelVersion.Text = String.Format("版本 {0} {0}", AssemblyVersion);
        //    this.labelCopyright.Text = AssemblyCopyright;
        //    this.labelCompanyName.Text = AssemblyCompany;
        //    this.textBoxDescription.Text = AssemblyDescription;
        //}

        //#region 程序集属性访问器

        //public string AssemblyTitle
        //{
        //    get
        //    {
        //        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
        //        if (attributes.Length > 0)
        //        {
        //            AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
        //            if (titleAttribute.Title != "")
        //            {
        //                return titleAttribute.Title;
        //            }
        //        }
        //        return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
        //    }
        //}

        //public string AssemblyVersion
        //{
        //    get
        //    {
        //        return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        //    }
        //}

        //public string AssemblyDescription
        //{
        //    get
        //    {
        //        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
        //        if (attributes.Length == 0)
        //        {
        //            return "";
        //        }
        //        return ((AssemblyDescriptionAttribute)attributes[0]).Description;
        //    }
        //}

        //public string AssemblyProduct
        //{
        //    get
        //    {
        //        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
        //        if (attributes.Length == 0)
        //        {
        //            return "";
        //        }
        //        return ((AssemblyProductAttribute)attributes[0]).Product;
        //    }
        //}

        //public string AssemblyCopyright
        //{
        //    get
        //    {
        //        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
        //        if (attributes.Length == 0)
        //        {
        //            return "";
        //        }
        //        return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
        //    }
        //}

        //public string AssemblyCompany
        //{
        //    get
        //    {
        //        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
        //        if (attributes.Length == 0)
        //        {
        //            return "";
        //        }
        //        return ((AssemblyCompanyAttribute)attributes[0]).Company;
        //    }
        //}
        //#endregion

        private void labelProductName_Click(object sender, EventArgs e)
        {

        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }

                private static FrmAbout pUniqueForm = null;//窗体唯一打开代码

        /// <summary>
        /// 窗体唯一打开代码
        /// </summary>
        /// <param name="Path">是否作为模式窗体打开</param>
        public static void ShowUniqueForm(bool AIfShowModal)
        {
            if (pUniqueForm == null)
            {
                new FrmAbout();

                if (AIfShowModal) pUniqueForm.ShowDialog(); else pUniqueForm.Show();
            }
        }

        public FrmAbout()
        {
            pUniqueForm = this;//窗体唯一打开代码
            InitializeComponent();
        }

        private void FrmAbout_FormClosed(object sender, FormClosedEventArgs e)
        {
            pUniqueForm = null;//窗体唯一打开代码
        }

        private void textBoxDescription_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
