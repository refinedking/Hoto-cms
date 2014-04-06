using System;
using System.Xml;
using System.Text;
using System.Web;
using Hoto.Common;

namespace Hoto.API.Payment.Tenpay
{
	/// <summary>
	/// TenpayUtil 的摘要说明。
	/// </summary>
	public class TenpayUtil
	{
        public static string tenpay         ="1";
        public static string bargainor_id   = "";                   //财付通商户号
        public static string tenpay_key     = "";  					//财付通密钥;
        public static string tenpay_return  = "";//显示支付通知页面;
        public static string tenpay_notify  =""; //支付完成后的回调处理页面;

        static TenpayUtil()
        {
            //读取XML配置信息
            string fullPath = HotoUtils.GetMapPath("~/xmlconfig/tenpay.config");
            XmlDocument doc = new XmlDocument();
            doc.Load(fullPath);
            XmlNode _bargainor_id = doc.SelectSingleNode(@"Root/bargainor_id");
            XmlNode _tenpay_key = doc.SelectSingleNode(@"Root/tenpay_key");
            XmlNode _tenpay_return = doc.SelectSingleNode(@"Root/tenpay_return");
            XmlNode _tenpay_notify = doc.SelectSingleNode(@"Root/tenpay_notify");
            //读取站点配置信息
            Model.siteconfig model = new BLL.siteconfig().loadConfig(HotoKeys.FILE_SITE_XML_CONFING);

            bargainor_id = _bargainor_id.InnerText;
            tenpay_key = _tenpay_key.InnerText;
            tenpay_return = HotoUtils.DelLastChar(model.weburl, "/") + _tenpay_return.InnerText;
            tenpay_notify = HotoUtils.DelLastChar(model.weburl, "/") + _tenpay_notify.InnerText;
		}
		/** 对字符串进行URL编码 */
		public static string UrlEncode(string instr, string charset)
		{
			//return instr;
			if(instr == null || instr.Trim() == "")
				return "";
			else
			{
				string res;
				
				try
				{
					res = HttpUtility.UrlEncode(instr,Encoding.GetEncoding(charset));
				}
				catch
				{
					res = HttpUtility.UrlEncode(instr,Encoding.GetEncoding("GB2312"));
				}
				return res;
			}
		}

		/** 对字符串进行URL解码 */
		public static string UrlDecode(string instr, string charset)
		{
			if(instr == null || instr.Trim() == "")
				return "";
			else
			{
				string res;
				
				try
				{
					res = HttpUtility.UrlDecode(instr,Encoding.GetEncoding(charset));
				}
				catch
				{
					res = HttpUtility.UrlDecode(instr,Encoding.GetEncoding("GB2312"));
				}
				return res;
			}
		}

		/** 取时间戳生成随即数,替换交易单号中的后10位流水号 */
		public static UInt32 UnixStamp()
		{
			TimeSpan ts = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			return Convert.ToUInt32(ts.TotalSeconds);
		}
		/** 取随机数 */
		public static string BuildRandomStr(int length) 
		{
			Random rand = new Random();

			int num = rand.Next();

			string str = num.ToString();

			if(str.Length > length)
			{
				str = str.Substring(0,length);
			}
			else if(str.Length < length)
			{
				int n = length - str.Length;
				while(n > 0)
				{
					str.Insert(0, "0");
					n--;
				}
			}
			
			return str;
		}
	}
}