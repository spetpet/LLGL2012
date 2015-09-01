using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace LLGL2012
{
    /// <summary>
    /// 获取当地的天气状况
    /// </summary>
    public class WeatherGet
    {
        /// <summary>
        /// 获取城市代码
        /// </summary>
        /// <returns></returns>
        private static string GetCityId()
        {
            HttpWebRequest wNetr = (HttpWebRequest)HttpWebRequest.Create("http://61.4.185.48:81/g/");
            HttpWebResponse wNetp = (HttpWebResponse)wNetr.GetResponse();

            wNetr.ContentType = "text/html";
            wNetr.Method = "Get";

            Stream Streams = wNetp.GetResponseStream();
            StreamReader Reads = new StreamReader(Streams, Encoding.UTF8);

            String ReCode = Reads.ReadToEnd();
            //关闭暂时不适用的资源
            Reads.Dispose();
            Streams.Dispose();
            wNetp.Close();
            //分析返回代码
            String[] Temp = ReCode.Split(';');

            ReCode = Temp[1].Replace("var id=", "");

            return ReCode;
        }

        /// <summary>
        /// 获取城市的天气状况
        /// </summary>
        /// <returns></returns>
        public static string GetWeather(string city)
        {
            string wUrl = string.Format("http://m.weather.com.cn/data/{0}.html", city);
            //string wUrl = @"http://m.weather.com.cn/data/101050101.html";
            HttpWebRequest wNetr = (HttpWebRequest)HttpWebRequest.Create(wUrl);
            HttpWebResponse wNetp = (HttpWebResponse)wNetr.GetResponse();

            wNetr.ContentType = "text/html";
            wNetr.Method = "Get";

            Stream Streams = wNetp.GetResponseStream();
            StreamReader Reads = new StreamReader(Streams, Encoding.UTF8);

            String ReCode = Reads.ReadToEnd();
            //关闭暂时不适用的资源
            Reads.Dispose();
            Streams.Dispose();
            wNetp.Close();
            //分析返回代码
            String[] Splits = new String[] { "\"", ",", "\"" };
            String[] Temp = ReCode.Split(Splits, StringSplitOptions.RemoveEmptyEntries);
            string[] Temp1 = Temp[25].Split(new char[] { '~', '℃' });
            string[] Temp2 = Temp[28].Split(new char[] { '~', '℃' });
            string[] Temp3 = Temp[31].Split(new char[] { '~', '℃' });
            string[] Temp4 = Temp[34].Split(new char[] { '~', '℃' });
            string[] Temp5 = Temp[37].Split(new char[] { '~', '℃' });
            string[] Temp6 = Temp[40].Split(new char[] { '~', '℃' });
            ReCode = String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", Temp1[0], Temp1[2], Temp2[0], Temp2[2], Temp3[0], Temp3[2], Temp4[0], Temp4[2], Temp5[0], Temp5[2], Temp6[0], Temp6[2]);
            
            return ReCode;
        }
    }
}