using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;//Application//项目的"引用"中也要增加"System.Windows.Forms"


namespace LLGL2012
{
    public partial class CommFunction//加了public partial后其他命名空间的项目才可访问该类
    {
        [DllImport("OptionSetForm.dll")]
        public static extern bool ShowOptionForm(string ACaption, string ATabSheetCaption, string AItemInfo, string AInifile);


        ////声明读写INI文件的API函数
        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);


        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        //读取INI文件所有的节名
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        public static extern int GetPrivateProfileSectionNames(string szBuffer, int size, string filePath);


        [DllImport("ADOLYQueryDll.dll")]
        public static extern bool ShowQueryForm(IntPtr AHandle, string AConnString, string ASelectString, int ADataBaseType, ref string AResultSelect);

        [DllImport("DESCrypt.dll")]
        public static extern string DeCryptStr(string AStr,string AKey);//解密

        [DllImport("ADOLYGetCodeDll.dll")]
        ///<summary>  
        ///取码对话框
        ///</summary>  
        ///<param name="AHandle">应用程序句柄</param>  
        ///<param name="AConnString">连接字符串</param>  
        ///<param name="AOpenStr">select语句</param>  
        ///<param name="AInField">取码字段,多个字段用逗号分隔</param>  
        ///<param name="AInValue">取码值</param>  
        ///<param name="AInFieldLabel">标签</param>  
        ///<param name="AGetCodePos">取码方式.0-全匹配,1--左匹配,2--右匹配</param>  
        ///<param name="AShowX">取码框X坐标</param>  
        ///<param name="AShowY">取码框Y坐标</param>  
        ///<param name="AIfNullGetCode">是否可无值取码,如直接回车</param>  
        ///<param name="AIfShowDialogOneRecord">取出一条记录时是否显示取码框</param>  
        ///<param name="AIfShowDialogZeroRecord">取出零条记录时是否显示取码框</param>  
        ///<param name="AifAbetChineseChar">是否支持中文取码</param>  
        ///<param name="AOutValue">输出字符串</param>  
        ///<returns>返回是否执行了取码操作</returns>   
        public static extern bool ShowGetCodeForm(IntPtr AHandle, string AConnString, string AOpenStr,
                          string AInField, string AInValue, string AInFieldLabel, int AGetCodePos,
                          int AShowX, int AShowY, bool AIfNullGetCode,
                          bool AIfShowDialogOneRecord, bool AIfShowDialogZeroRecord,
                          bool AifAbetChineseChar, ref string AOutValue);

        [DllImport("autowbpy.dll")]
        public static extern string GetBm(string InputStr,int sel);

        [DllImport("LYDataToExcelDLL.dll")]
        public static extern bool LYData2Excel(IntPtr AHandle,string AConnString,string ASelectString,string AExcelTitel);






        public static string ConnectString;
        public static string cfServerName;//要传入水晶报表
        public static string cfUserID;//要传入水晶报表
        public static string cfPassword;//要传入水晶报表

        public const string GSYSNAME = "冷链管理系统";

        public static string WM_ConnString;//WM连接字符串



        public static bool MakeDBConn_temp()
        {
            const string CryptStr = "sp";
            bool result;
            result = false;

            string AppFile = Application.ExecutablePath;//应用程序路径(包括名称)
            AppFile = AppFile.ToLower();
            AppFile = AppFile.Replace(".exe", ".ini");

        labReadIni:
            StringBuilder temp = new StringBuilder(255);
            CommFunction.GetPrivateProfileString("temp连接数据库", "服务名", "", temp, 255, AppFile);
            string datasource = temp.ToString();
            CommFunction.GetPrivateProfileString("temp连接数据库", "用户", "", temp, 255, AppFile);
            string user = temp.ToString();
            CommFunction.GetPrivateProfileString("temp连接数据库", "口令", "", temp, 255, AppFile);
            string pwd = temp.ToString();
            pwd = CommFunction.DeCryptStr(pwd, CryptStr);


            string newconnstr = "";
            newconnstr = newconnstr + "data source=" + datasource + ";";
            newconnstr = newconnstr + "User ID=" + user + ";";
            newconnstr = newconnstr + "Password=" + pwd + ";";
            newconnstr = newconnstr + "Provider=OraOLEDB.Oracle.1";//WIN7下一定要先这样，MSDAORA.1:才连得上，太怪了!
            OleDbConnection odbconn = new OleDbConnection(newconnstr);
            try
            {
                odbconn.Open();
                result = true;
            }
            catch
            {
                result = false;
            }
            if (result == false)
            {
                string ss = "服务名" + Convert.ToChar(0x2) + "Edit" + Convert.ToChar(0x2) + Convert.ToChar(0x2) + "0" + Convert.ToChar(0x2) + Convert.ToChar(0x2) + Convert.ToChar(0x3) +
                            "用户" + Convert.ToChar(0x2) + "Edit" + Convert.ToChar(0x2) + Convert.ToChar(0x2) + "0" + Convert.ToChar(0x2) + Convert.ToChar(0x2) + Convert.ToChar(0x3) +
                            "口令" + Convert.ToChar(0x2) + "Edit" + Convert.ToChar(0x2) + Convert.ToChar(0x2) + "0" + Convert.ToChar(0x2) + Convert.ToChar(0x2) + "1" + CryptStr + Convert.ToChar(0x3);


                if (CommFunction.ShowOptionForm("temp连接数据库", "temp连接数据库", ss, AppFile))
                {
                    goto labReadIni;
                }
                else { Application.Exit(); }
            }

            return true;
        }


        ///<summary>
        ///程序启动必需的WMS数据库连接设置
        ///</summary>
        ///<param name=""></param>
        ///<returns></returns>
        public static bool MakeDBConn_WMS()
        {
            const string CryptStr = "sp";
            bool result;
            result = false;

            string AppFile = Application.ExecutablePath;//应用程序路径(包括名称)
            AppFile = AppFile.ToLower();
            AppFile = AppFile.Replace(".exe", ".ini");

        labReadIni:
            StringBuilder temp = new StringBuilder(255);
            CommFunction.GetPrivateProfileString("WMS连接数据库", "服务名", "", temp, 255, AppFile);
            string datasource = temp.ToString();
            CommFunction.GetPrivateProfileString("WMS连接数据库", "用户", "", temp, 255, AppFile);
            string user = temp.ToString();
            CommFunction.GetPrivateProfileString("WMS连接数据库", "口令", "", temp, 255, AppFile);
            string pwd = temp.ToString();
            pwd = CommFunction.DeCryptStr(pwd, CryptStr);

            string newconnstr = "";
            newconnstr = newconnstr + "data source=" + datasource + ";";
            newconnstr = newconnstr + "User ID=" + user + ";";
            newconnstr = newconnstr + "Password=" + pwd + ";";
            newconnstr = newconnstr + "Provider=MSDAORA.1:";//Unicode=True;//OraOleDb.Oracle
            OleDbConnection odbconn = new OleDbConnection(newconnstr);
            try
            {
                odbconn.Open();
                result = true;
                //CommFunction.cfServerName = datasource;
                //CommFunction.cfUserID = user;
                //CommFunction.cfPassword = pwd;
                //LoginCs.LoginClass.ConnectString = newconnstr;//传递给登录模块以便登录

                CommFunction.WM_ConnString = newconnstr;
            }
            catch
            {
                result = false;
            }
            if (result == false)
            {
                string ss = "服务名" + Convert.ToChar(0x2) + "Edit" + Convert.ToChar(0x2) + Convert.ToChar(0x2) + "0" + Convert.ToChar(0x2) + Convert.ToChar(0x2) + Convert.ToChar(0x3) +
                            "用户" + Convert.ToChar(0x2) + "Edit" + Convert.ToChar(0x2) + Convert.ToChar(0x2) + "0" + Convert.ToChar(0x2) + Convert.ToChar(0x2) + Convert.ToChar(0x3) +
                            "口令" + Convert.ToChar(0x2) + "Edit" + Convert.ToChar(0x2) + Convert.ToChar(0x2) + "0" + Convert.ToChar(0x2) + Convert.ToChar(0x2) + "1" + CryptStr + Convert.ToChar(0x3);


                if (CommFunction.ShowOptionForm("WMS连接数据库", "WMS连接数据库", ss, AppFile))
                {
                    goto labReadIni;
                }
                else { Application.Exit(); }
            }

            return true;

        }

        ///<summary>
        ///程序启动必需的WMS数据库连接设置
        ///</summary>
        ///<param name=""></param>
        ///<returns></returns>
        public static bool MakeDBConn_HCWL()
        {
            const string CryptStr = "sp";
            bool result;
            result = false;

            string AppFile = Application.ExecutablePath;//应用程序路径(包括名称)
            AppFile = AppFile.ToLower();
            AppFile = AppFile.Replace(".exe", ".ini");

        labReadIni:
            StringBuilder temp = new StringBuilder(255);
            CommFunction.GetPrivateProfileString("HCWL连接数据库", "服务名", "", temp, 255, AppFile);
            string datasource = temp.ToString();
            CommFunction.GetPrivateProfileString("HCWL连接数据库", "用户", "", temp, 255, AppFile);
            string user = temp.ToString();
            CommFunction.GetPrivateProfileString("HCWL连接数据库", "口令", "", temp, 255, AppFile);
            string pwd = temp.ToString();
            pwd = CommFunction.DeCryptStr(pwd, CryptStr);


            string newconnstr = "";
            newconnstr = newconnstr + "data source=" + datasource + ";";
            newconnstr = newconnstr + "User ID=" + user + ";";
            newconnstr = newconnstr + "Password=" + pwd + ";";
            newconnstr = newconnstr + "Provider=MSDAORA.1:";//Unicode=True;//OraOleDb.Oracle
            OleDbConnection odbconn = new OleDbConnection(newconnstr);
            try
            {
                odbconn.Open();
                result = true;
                CommFunction.cfServerName = datasource;
                CommFunction.cfUserID = user;
                CommFunction.cfPassword = pwd;
                LoginCs.LoginClass.ConnectString = newconnstr;//传递给登录模块以便登录

                CommFunction.ConnectString = newconnstr;
            }
            catch
            {
                result = false;
            }
            if (result == false)
            {
                string ss = "服务名" + Convert.ToChar(0x2) + "Edit" + Convert.ToChar(0x2) + Convert.ToChar(0x2) + "0" + Convert.ToChar(0x2) + Convert.ToChar(0x2) + Convert.ToChar(0x3) +
                            "用户" + Convert.ToChar(0x2) + "Edit" + Convert.ToChar(0x2) + Convert.ToChar(0x2) + "0" + Convert.ToChar(0x2) + Convert.ToChar(0x2) + Convert.ToChar(0x3) +
                            "口令" + Convert.ToChar(0x2) + "Edit" + Convert.ToChar(0x2) + Convert.ToChar(0x2) + "0" + Convert.ToChar(0x2) + Convert.ToChar(0x2) + "1" + CryptStr + Convert.ToChar(0x3);


                if (CommFunction.ShowOptionForm("HCWL连接数据库", "HCWL连接数据库", ss, AppFile))
                {
                    goto labReadIni;
                }
                else { Application.Exit(); }
            }

            return true;

        }


        /// <summary>
        /// 获取一串汉字的拼音声母
        /// </summary>
        /// <param name="chinese">Unicode格式的汉字字符串</param>
        /// <returns>拼音声母字符串</returns>
        /// <example>
        /// “旺旺软件工作室”转换为“wwrjgzs”
        /// </example>
        public static String Pym(String chinese)
        {
            char[] buffer = new char[chinese.Length];
            for (int i = 0; i < chinese.Length; i++)
            {
                buffer[i] = Pym(chinese[i]);
            }
            return new String(buffer);
        }

        /// <summary>
        /// 获取一个汉字的拼音声母
        /// </summary>
        /// <param name="chinese">Unicode格式的一个汉字</param>
        /// <returns>汉字的声母</returns>
        public static char Pym(Char chinese)
        {
            Encoding gb2312 = Encoding.GetEncoding("GB2312");
            Encoding unicode = Encoding.Unicode;

            // Pym the string into a byte[].
            byte[] unicodeBytes = unicode.GetBytes(new Char[] { chinese });
            // Perform the conversion from one encoding to the other.
            byte[] asciiBytes = Encoding.Convert(unicode, gb2312, unicodeBytes);

            // 计算该汉字的GB-2312编码
            int n = (int)asciiBytes[0] << 8;
            n += (int)asciiBytes[1];

            // 根据汉字区域码获取拼音声母
            if (In(0xB0A1, 0xB0C4, n)) return 'a';
            if (In(0XB0C5, 0XB2C0, n)) return 'b';
            if (In(0xB2C1, 0xB4ED, n)) return 'c';
            if (In(0xB4EE, 0xB6E9, n)) return 'd';
            if (In(0xB6EA, 0xB7A1, n)) return 'e';
            if (In(0xB7A2, 0xB8c0, n)) return 'f';
            if (In(0xB8C1, 0xB9FD, n)) return 'g';
            if (In(0xB9FE, 0xBBF6, n)) return 'h';
            if (In(0xBBF7, 0xBFA5, n)) return 'j';
            if (In(0xBFA6, 0xC0AB, n)) return 'k';
            if (In(0xC0AC, 0xC2E7, n)) return 'l';
            if (In(0xC2E8, 0xC4C2, n)) return 'm';
            if (In(0xC4C3, 0xC5B5, n)) return 'n';
            if (In(0xC5B6, 0xC5BD, n)) return 'o';
            if (In(0xC5BE, 0xC6D9, n)) return 'p';
            if (In(0xC6DA, 0xC8BA, n)) return 'q';
            if (In(0xC8BB, 0xC8F5, n)) return 'r';
            if (In(0xC8F6, 0xCBF0, n)) return 's';
            if (In(0xCBFA, 0xCDD9, n)) return 't';
            if (In(0xCDDA, 0xCEF3, n)) return 'w';
            if (In(0xCEF4, 0xD188, n)) return 'x';
            if (In(0xD1B9, 0xD4D0, n)) return 'y';
            if (In(0xD4D1, 0xD7F9, n)) return 'z';
            return '\0';
        }

        private static bool In(int Lp, int Hp, int Value)
        {
            return ((Value <= Hp) && (Value >= Lp));
        }

    }
}
