using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.API.Payment.Tenpay;
using DTcms.Common;

namespace DTcms.Web.api.payment.tenpay
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //读取站点配置信息
            Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(DTKeys.FILE_SITE_XML_CONFING);
            //获得订单信息
            string order_type = DTRequest.GetFormString("pay_order_type"); //订单类型
            string order_no = DTRequest.GetFormString("pay_order_no"); //订单号
            decimal order_amount = DTRequest.GetFormDecimal("pay_order_amount", 0); //订单金额
            string user_name = DTRequest.GetFormString("pay_user_name"); //支付用户名
            string subject = DTRequest.GetFormString("pay_subject"); //备注说明

            if (order_type == "" || order_no == "" || order_amount == 0 || user_name == "")
            {
                Response.Redirect(siteConfig.webpath + "error.aspx?msg=" + Utils.UrlEncode("对不起，您提交的参数有误！"));
                return;
            }

            ////////////////////////////////////////////请求参数////////////////////////////////////////////
            //创建RequestHandler实例
            RequestHandler reqHandler = new RequestHandler(Context);
            //初始化
            reqHandler.init();
            //设置密钥
            reqHandler.setKey(TenpayUtil.tenpay_key);
            reqHandler.setGateUrl("https://gw.tenpay.com/gateway/pay.htm");
            //-----------------------------
            //设置支付参数
            //-----------------------------
            reqHandler.setParameter("partner", TenpayUtil.bargainor_id);		        //商户号
            reqHandler.setParameter("out_trade_no", order_no);		//商家订单号
            reqHandler.setParameter("total_fee", (Convert.ToDouble(order_amount) * 100).ToString());			        //商品金额,以分为单位
            reqHandler.setParameter("return_url", TenpayUtil.tenpay_return);		    //交易完成后跳转的URL
            reqHandler.setParameter("notify_url", TenpayUtil.tenpay_notify);		    //接收财付通通知的URL
            reqHandler.setParameter("body", "支付会员：" + user_name);	                    //商品描述
            reqHandler.setParameter("bank_type", "DEFAULT");		    //银行类型(中介担保时此参数无效)
            reqHandler.setParameter("spbill_create_ip", Page.Request.UserHostAddress);   //用户的公网ip，不是商户服务器IP
            reqHandler.setParameter("fee_type", "1");                    //币种，1人民币
            reqHandler.setParameter("subject", siteConfig.webname + "-" + subject);              //商品名称(中介交易时必填)


            //系统可选参数
            reqHandler.setParameter("sign_type", "MD5");
            reqHandler.setParameter("service_version", "1.0");
            reqHandler.setParameter("input_charset", "UTF-8");
            reqHandler.setParameter("sign_key_index", "1");

            //业务可选参数

            reqHandler.setParameter("attach", order_type);               //附加数据，原样返回
            reqHandler.setParameter("product_fee", "0");                 //商品费用，必须保证transport_fee + product_fee=total_fee
            reqHandler.setParameter("transport_fee", "0");               //物流费用，必须保证transport_fee + product_fee=total_fee
            reqHandler.setParameter("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));            //订单生成时间，格式为yyyymmddhhmmss
            reqHandler.setParameter("time_expire", "");                 //订单失效时间，格式为yyyymmddhhmmss
            reqHandler.setParameter("buyer_id", "");                    //买方财付通账号
            reqHandler.setParameter("goods_tag", "");                   //商品标记
            reqHandler.setParameter("trade_mode", "1");                 //交易模式，1即时到账(默认)，2中介担保，3后台选择（买家进支付中心列表选择）
            reqHandler.setParameter("transport_desc", "");              //物流说明
            reqHandler.setParameter("trans_type", "1");                  //交易类型，1实物交易，2虚拟交易
            reqHandler.setParameter("agentid", "");                     //平台ID
            reqHandler.setParameter("agent_type", "");                  //代理模式，0无代理(默认)，1表示卡易售模式，2表示网店模式
            reqHandler.setParameter("seller_id", "");                   //卖家商户号，为空则等同于partner

            //获取请求带参数的url
            string requestUrl = reqHandler.getRequestURL();

            /*Get的实现方式
            string a_link = "<a target=\"_blank\" href=\"" + requestUrl + "\">" + "财付通支付" + "</a>";
            Response.Write(a_link);*/

            //实现自动跳转===============================
            StringBuilder sbHtml = new StringBuilder();
            sbHtml.Append("<form id='tenpaysubmit' name='tenpaysubmit' action='" + reqHandler.getGateUrl() + "' method='get'>");
            Hashtable ht = reqHandler.getAllParameters();
            foreach (DictionaryEntry de in ht)
            {
                sbHtml.Append("<input type=\"hidden\" name=\"" + de.Key + "\" value=\"" + de.Value + "\" >\n");
            }
            //submit按钮控件请不要含有name属性
            sbHtml.Append("<input type='submit' value='确认' style='display:none;'></form>");
            sbHtml.Append("<script>document.forms['tenpaysubmit'].submit();</script>");

            Response.Write(sbHtml.ToString());


        }
    }
}