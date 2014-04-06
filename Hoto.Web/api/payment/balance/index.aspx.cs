using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.api.payment.balance
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //读取站点配置信息
            Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(DTKeys.FILE_SITE_XML_CONFING);

            string order_type = DTRequest.GetFormString("pay_order_type"); //订单类型
            string order_no = DTRequest.GetFormString("pay_order_no");
            decimal order_amount = DTRequest.GetFormDecimal("pay_order_amount", 0);
            string subject = DTRequest.GetFormString("pay_subject");
            if (order_no == "" || order_amount == 0 )
            {
                Response.Redirect(siteConfig.webpath + "error.aspx?msg=" + Utils.UrlEncode("对不起，您提交的参数有误！"));
                return;
            }
            //检查是否已登录
            Model.users userModel = new Web.UI.BasePage().GetUserInfo();
            if (userModel == null)
            {
                Response.Redirect(new Web.UI.BasePage().linkurl("payment", "login")); //尚未登录
                return;
            }
            if (userModel.amount < order_amount)
            {
                Response.Redirect(new Web.UI.BasePage().linkurl("payment", "recharge")); //账户的余额不足
                return;
            }

            if (order_type.ToLower() == DTEnums.AmountTypeEnum.BuyGoods.ToString().ToLower()) //购买商品
            {
                BLL.orders bll = new BLL.orders();
                Model.orders model = bll.GetModel(order_no);
                if (model == null)
                {
                    Response.Redirect(siteConfig.webpath + "error.aspx?msg=" + Utils.UrlEncode("对不起，商品订单号不存在！"));
                    return;
                }
                if (model.payment_status == 1)
                {
                    //执行扣取账户金额
                    int result = new BLL.amount_log().Add(userModel.id, userModel.user_name, DTEnums.AmountTypeEnum.BuyGoods.ToString(), order_no, model.payment_id, -1 * order_amount, subject, 1);
                    if (result > 0)
                    {
                        //更改订单状态
                        bool result1 = bll.UpdateField(order_no, "payment_status=2,payment_time='" + DateTime.Now + "'");
                        if (!result1)
                        {
                            Response.Redirect(new Web.UI.BasePage().linkurl("payment", "error"));
                            return;
                        }
                        //扣除积分
                        if (model.point < 0)
                        {
                            new BLL.point_log().Add(model.user_id, model.user_name, model.point, "换购扣除积分，订单号：" + model.order_no);
                        }
                    }
                    else
                    {
                        Response.Redirect(new Web.UI.BasePage().linkurl("payment", "error"));
                        return;
                    }
                }
                //支付成功
                Response.Redirect(new Web.UI.BasePage().linkurl("payment1", "succeed", order_type, order_no));
                return;
            }
            Response.Redirect(siteConfig.webpath + "error.aspx?msg=" + Utils.UrlEncode("对不起，找不到需要支付的订单类型！"));
            return;
        }
    }
}