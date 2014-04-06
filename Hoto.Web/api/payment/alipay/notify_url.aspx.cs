using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.API.Payment.Alipay;
using DTcms.Common;

namespace DTcms.Web.api.payment.alipay
{
    public partial class notify_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SortedDictionary<string, string> sPara = GetRequestPost();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, DTRequest.GetString("notify_id"), DTRequest.GetString("sign"));

                if (verifyResult)//验证成功
                {
                    string trade_no = DTRequest.GetString("trade_no");         //支付宝交易号
                    string order_no = DTRequest.GetString("out_trade_no");     //获取订单号
                    string total_fee = DTRequest.GetString("total_fee");       //获取总金额
                    string subject = DTRequest.GetString("subject");           //商品名称、订单名称
                    string body = DTRequest.GetString("body");                 //商品描述、订单备注、描述
                    string buyer_email = DTRequest.GetString("buyer_email");   //买家支付宝账号
                    string trade_status = DTRequest.GetString("trade_status"); //交易状态
                    string order_type = DTRequest.GetString("extra_common_param"); //订单交易类别

                    if (DTRequest.GetString("trade_status") == "TRADE_FINISHED" || DTRequest.GetString("trade_status") == "TRADE_SUCCESS")
                    {
                        //修改支付状态、时间
                        if (order_type.ToLower() == DTEnums.AmountTypeEnum.Recharge.ToString().ToLower()) //在线充值
                        {
                            BLL.amount_log bll = new BLL.amount_log();
                            Model.amount_log model = bll.GetModel(order_no);
                            if (model == null)
                            {
                                Response.Write("该订单号不存在");
                                return;
                            }
                            if (model.value != decimal.Parse(total_fee))
                            {
                                Response.Write("订单金额和支付金额不相符");
                                return;
                            }
                            model.status = 1;
                            model.complete_time = DateTime.Now;
                            bool result = bll.Update(model);
                            if (!result)
                            {
                                Response.Write("修改订单状态失败");
                                return;
                            }
                        }
                        else if (order_type.ToLower() == DTEnums.AmountTypeEnum.BuyGoods.ToString().ToLower()) //购买商品
                        {
                            BLL.orders bll = new BLL.orders();
                            Model.orders model = bll.GetModel(order_no);
                            if (model == null)
                            {
                                Response.Write("该订单号不存在");
                                return;
                            }
                            if (model.order_amount != decimal.Parse(total_fee))
                            {
                                Response.Write("订单金额和支付金额不相符");
                                return;
                            }
                            bool result = bll.UpdateField(order_no, "payment_status=2,payment_time='" + DateTime.Now + "'");
                            if (!result)
                            {
                                Response.Write("修改订单状态失败");
                                return;
                            }
                            //扣除积分
                            if (model.point < 0)
                            {
                                new BLL.point_log().Add(model.user_id, model.user_name, model.point, "换购扣除积分，订单号：" + model.order_no);
                            }
                        }
                    }

                    Response.Write("success");  //请不要修改或删除
                }
                else//验证失败
                {
                    Response.Write("fail");
                }
            }
            else
            {
                Response.Write("无通知参数");
            }
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            coll = Request.Form;

            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }

            return sArray;
        }

    }
}