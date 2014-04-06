using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.API.Payment.Tenpay;
using DTcms.Common;

namespace DTcms.Web.api.payment.tenpay
{
    public partial class notify_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //创建ResponseHandler实例
            ResponseHandler resHandler = new ResponseHandler(Context);
            resHandler.setKey(TenpayUtil.tenpay_key);

            //判断签名
            if (resHandler.isTenpaySign())
            {
                ///通知id
                string notify_id = resHandler.getParameter("notify_id");
                //通过通知ID查询，确保通知来至财付通
                //创建查询请求
                RequestHandler queryReq = new RequestHandler(Context);
                queryReq.init();
                queryReq.setKey(TenpayUtil.tenpay_key);
                queryReq.setGateUrl("https://gw.tenpay.com/gateway/simpleverifynotifyid.xml");
                queryReq.setParameter("partner", TenpayUtil.bargainor_id);
                queryReq.setParameter("notify_id", notify_id);

                //通信对象
                TenpayHttpClient httpClient = new TenpayHttpClient();
                httpClient.setTimeOut(5);
                //设置请求内容
                httpClient.setReqContent(queryReq.getRequestURL());
                //后台调用
                if (httpClient.call())
                {
                    //设置结果参数
                    ClientResponseHandler queryRes = new ClientResponseHandler();
                    queryRes.setContent(httpClient.getResContent());
                    queryRes.setKey(TenpayUtil.tenpay_key);
                    //判断签名及结果
                    //只有签名正确,retcode为0，trade_state为0才是支付成功
                    if (queryRes.isTenpaySign())
                    {
                        //取结果参数做业务处理
                        string out_trade_no = resHandler.getParameter("out_trade_no");
                        //财付通订单号
                        string transaction_id = resHandler.getParameter("transaction_id");
                        //金额,以分为单位
                        string total_fee = resHandler.getParameter("total_fee");
                        //如果有使用折扣券，discount有值，total_fee+discount=原请求的total_fee
                        string discount = resHandler.getParameter("discount");
                        //订单类型
                        string order_type = resHandler.getParameter("attach");
                        //支付结果
                        string trade_state = resHandler.getParameter("trade_state");
                        //交易模式，1即时到帐 2中介担保
                        string trade_mode = resHandler.getParameter("trade_mode");
                        #region
                        //判断签名及结果
                        if ("0".Equals(queryRes.getParameter("retcode")))
                        {
                            //Response.Write("id验证成功");

                            if ("1".Equals(trade_mode))
                            {       //即时到账 
                                if ("0".Equals(trade_state))
                                {
                                    //------------------------------
                                    //即时到账处理业务开始
                                    //------------------------------
                                    //处理数据库逻辑
                                    //注意交易单不要重复处理
                                    //注意判断返回金额

                                    //修改支付状态、时间
                                    if (order_type.ToLower() == DTEnums.AmountTypeEnum.Recharge.ToString().ToLower()) //在线充值
                                    {
                                        BLL.amount_log bll = new BLL.amount_log();
                                        Model.amount_log model = bll.GetModel(out_trade_no);
                                        if (model == null)
                                        {
                                            Response.Write("该订单号不存在");
                                            return;
                                        }
                                        if (model.value != (decimal.Parse(total_fee) / 100))
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
                                        Model.orders model = bll.GetModel(out_trade_no);
                                        if (model == null)
                                        {
                                            Response.Write("该订单号不存在");
                                            return;
                                        }
                                        if (model.order_amount != (decimal.Parse(total_fee) / 100))
                                        {
                                            Response.Write("订单金额和支付金额不相符");
                                            return;
                                        }
                                        bool result = bll.UpdateField(out_trade_no, "payment_status=2,payment_time='" + DateTime.Now + "'");
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

                                    //------------------------------
                                    //即时到账处理业务完毕
                                    //------------------------------

                                    //给财付通系统发送成功信息，财付通系统收到此结果后不再进行后续通知
                                    Response.Write("success");
                                }
                                else
                                {
                                    Response.Write("即时到账支付失败");
                                }
                            }
                        }
                        else
                        {
                            //错误时，返回结果可能没有签名，写日志trade_state、retcode、retmsg看失败详情。
                            //通知财付通处理失败，需要重新通知
                            Response.Write("查询验证签名失败或id验证失败");
                            Response.Write("retcode:" + queryRes.getParameter("retcode"));
                        }
                        #endregion
                    }
                    else
                    {
                        Response.Write("通知ID查询签名验证失败");
                    }
                }
                else
                {
                    //通知财付通处理失败，需要重新通知
                    Response.Write("后台调用通信失败");
                    //写错误日志
                    Response.Write("call err:" + httpClient.getErrInfo() + "<br>" + httpClient.getResponseCode() + "<br>");

                }
            }
            else
            {
                Response.Write("签名验证失败");
            }
            Response.End();
        }
    }
}