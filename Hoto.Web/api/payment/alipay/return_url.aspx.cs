using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;
using DTcms.API.Payment.Alipay;

namespace DTcms.Web.api.payment.alipay
{
    public partial class return_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //读取站点配置信息
            Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(DTKeys.FILE_SITE_XML_CONFING);
            SortedDictionary<string, string> sPara = GetRequestGet();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, DTRequest.GetString("notify_id"), DTRequest.GetString("sign"));

                if (verifyResult)//验证成功
                {
                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表
                    string trade_no = DTRequest.GetString("trade_no");              //支付宝交易号
                    string order_no = DTRequest.GetString("out_trade_no");	        //获取订单号
                    string total_fee = DTRequest.GetString("total_fee");            //获取总金额
                    string subject = DTRequest.GetString("subject");                //商品名称、订单名称
                    string body = DTRequest.GetString("body");                      //商品描述、订单备注、描述
                    string buyer_email = DTRequest.GetString("buyer_email");        //买家支付宝账号
                    string trade_status = DTRequest.GetString("trade_status");      //交易状态
                    string order_type = DTRequest.GetString("extra_common_param");  //订单交易类别

                    if (DTRequest.GetString("trade_status") == "TRADE_FINISHED" || DTRequest.GetString("trade_status") == "TRADE_SUCCESS")
                    {
                        //成功状态
                        Response.Redirect(new Web.UI.BasePage().linkurl("payment1", "succeed", order_type, order_no));
                        return;
                    }
                }
            }
            //失败状态
            Response.Redirect(new Web.UI.BasePage().linkurl("payment", "error"));
            return;
        }

        /// <summary>
        /// 获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestGet()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            coll = Request.QueryString;

            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.QueryString[requestItem[i]]);
            }

            return sArray;
        }

    }
}