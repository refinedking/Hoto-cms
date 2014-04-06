using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;
using DTcms.API.Payment.Alipay;

namespace DTcms.Web.api.payment.alipay
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //读取站点配置信息
            Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(DTKeys.FILE_SITE_XML_CONFING);

            //获得订单信息
            string order_type = DTRequest.GetFormString("pay_order_type"); //订单类型
            string order_no = DTRequest.GetFormString("pay_order_no");
            decimal order_amount = DTRequest.GetFormDecimal("pay_order_amount", 0);
            string user_name = DTRequest.GetFormString("pay_user_name");
            string subject = DTRequest.GetFormString("pay_subject");
            if (order_type == "" || order_no == "" || order_amount == 0 || user_name == "")
            {
                Response.Redirect(siteConfig.webpath + "error.aspx?msg=" + Utils.UrlEncode("对不起，您提交的参数有误！"));
                return;
            }


            ////////////////////////////////////////////请求参数////////////////////////////////////////////

            //必填参数//

            //请与贵网站订单系统中的唯一订单号匹配
            string out_trade_no = order_no;
            //订单名称，显示在支付宝收银台里的“商品名称”里，显示在支付宝的交易管理的“商品名称”的列表里。
            string _subject = siteConfig.webname + "-" + subject;
            //订单描述、订单详细、订单备注，显示在支付宝收银台里的“商品描述”里
            string body = "支付会员：" + user_name;
            //订单总金额，显示在支付宝收银台里的“应付总额”里
            string total_fee = order_amount.ToString();


            //扩展功能参数——默认支付方式//

            //默认支付方式，代码见“即时到帐接口”技术文档
            string paymethod = "";
            //默认网银代号，代号列表见“即时到帐接口”技术文档“附录”→“银行列表”
            string defaultbank = "";

            //扩展功能参数——防钓鱼//

            //防钓鱼时间戳
            string anti_phishing_key = "";
            //获取客户端的IP地址，建议：编写获取客户端IP地址的程序
            string exter_invoke_ip = "";

            //扩展功能参数——其他//

            //商品展示地址，要用http:// 格式的完整路径，不允许加?id=123这类自定义参数
            string show_url = siteConfig.weburl;
            //自定义参数，可存放任何内容（除=、&等特殊字符外），不会显示在页面上
            string extra_common_param = order_type;
            //默认买家支付宝账号
            string buyer_email = "";
            string royalty_type = "";
            string royalty_parameters = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////

            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("payment_type", "1");
            sParaTemp.Add("show_url", show_url);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("subject", _subject);
            sParaTemp.Add("body", body);
            sParaTemp.Add("total_fee", total_fee);
            sParaTemp.Add("paymethod", paymethod);
            sParaTemp.Add("defaultbank", defaultbank);
            sParaTemp.Add("anti_phishing_key", anti_phishing_key);
            sParaTemp.Add("exter_invoke_ip", exter_invoke_ip);
            sParaTemp.Add("extra_common_param", extra_common_param);
            sParaTemp.Add("buyer_email", buyer_email);
            sParaTemp.Add("royalty_type", royalty_type);
            sParaTemp.Add("royalty_parameters", royalty_parameters);

            //构造即时到帐接口表单提交HTML数据，无需修改
            Service ali = new Service();
            string sHtmlText = ali.Create_direct_pay_by_user(sParaTemp);
            Response.Write(sHtmlText);
        }
    }
}