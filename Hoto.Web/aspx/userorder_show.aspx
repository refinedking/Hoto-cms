<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.UI.Page.userorder_show" ValidateRequest="false" %>
<%@ Import namespace="System.Collections.Generic" %>
<%@ Import namespace="System.Text" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="DTcms.Common" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	base.OnInit(e);
	StringBuilder templateBuilder = new StringBuilder(220000);

	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>查看订单详情 - ");
	templateBuilder.Append(Utils.ObjectToStr(config.webname));
	templateBuilder.Append("</title>\r\n<meta content=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webkeyword));
	templateBuilder.Append("\" name=\"keywords\">\r\n<meta content=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webdescription));
	templateBuilder.Append("\" name=\"description\">\r\n<link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/ui/skins/Aqua/css/ligerui-all.css\" />\r\n<link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/pagination.css\" />\r\n<link media=\"screen\" type=\"text/css\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.templateskin));
	templateBuilder.Append("/css/style.css\" rel=\"stylesheet\">\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery-1.3.2.min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/ui/js/ligerBuild.min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.templateskin));
	templateBuilder.Append("/js/base.js\"></");
	templateBuilder.Append("script>\r\n</head>\r\n\r\n<body>\r\n<!--Header-->\r\n");

	templateBuilder.Append("<div class=\"header\">\r\n  <div class=\"header_inner\">\r\n    <h1 class=\"logo\">\r\n      <a title=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webname));
	templateBuilder.Append("\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.weburl));
	templateBuilder.Append("\">");
	templateBuilder.Append(Utils.ObjectToStr(config.webname));
	templateBuilder.Append("</a>\r\n    </h1>\r\n    <ul class=\"nav\">\r\n      <li><a href=\"");
	templateBuilder.Append(linkurl("goods"));

	templateBuilder.Append("\">购物商城</a></li>\r\n      <li><a href=\"");
	templateBuilder.Append(linkurl("photo"));

	templateBuilder.Append("\">今日团购</a></li>\r\n    </ul>\r\n    <div class=\"search\">\r\n      <input id=\"keywords\" name=\"keywords\" class=\"input\" type=\"text\" x-webkit-speech=\"\" autofocus=\"\" placeholder=\"输入回车搜索\" onkeydown=\"if(event.keyCode==13){SiteSearch('");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("search.aspx', '#keywords');return false};\" />\r\n      <input class=\"submit\" type=\"submit\" value=\"搜索\" onclick=\"SiteSearch('");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("search.aspx', '#keywords');\" />\r\n    </div>\r\n    <ul class=\"menu\">\r\n      <li><a href=\"");
	templateBuilder.Append(linkurl("content","contact"));

	templateBuilder.Append("\">联系我们</a></li>\r\n      <li><a href=\"");
	templateBuilder.Append(linkurl("shopping","cart"));

	templateBuilder.Append("\">购物车");
	templateBuilder.Append(get_cart_quantity().ToString());
	

	templateBuilder.Append("件</a></li>\r\n      ");
	if (IsUserLogin())
	{

	templateBuilder.Append("\r\n      <li><a href=\"");
	templateBuilder.Append(linkurl("usercenter","exit"));

	templateBuilder.Append("\">退出</a></li>\r\n      <li><a href=\"");
	templateBuilder.Append(linkurl("usercenter","index"));

	templateBuilder.Append("\">会员中心</a></li>\r\n      ");
	}
	else
	{

	templateBuilder.Append("\r\n      <li><a href=\"");
	templateBuilder.Append(linkurl("register"));

	templateBuilder.Append("\">注册</a></li>\r\n      <li><a href=\"");
	templateBuilder.Append(linkurl("login"));

	templateBuilder.Append("\">登录</a></li>\r\n      ");
	}	//end if


	templateBuilder.Append("\r\n    </ul>\r\n  </div>\r\n</div>");


	templateBuilder.Append("\r\n<!--/Header-->\r\n\r\n<div class=\"boxwrap\">\r\n  <div class=\"left180\">\r\n   <!--Sidebar-->\r\n   ");

	templateBuilder.Append("    <div class=\"sidebar\">\r\n      <h3>交易管理</h3>\r\n      <ul>\r\n        <li><a href=\"");
	templateBuilder.Append(linkurl("userorder","list"));

	templateBuilder.Append("\">交易订单</a></li>\r\n        <li><a href=\"");
	templateBuilder.Append(linkurl("userorder","close"));

	templateBuilder.Append("\">已关闭订单</a></li>\r\n      </ul>\r\n      <h3>账户管理</h3>\r\n      <ul>\r\n        <li><a href=\"");
	templateBuilder.Append(linkurl("useramount","recharge"));

	templateBuilder.Append("\">账户余额</a></li>\r\n        <li><a href=\"");
	templateBuilder.Append(linkurl("userpoint","convert"));

	templateBuilder.Append("\">我的积分</a></li>\r\n        <li><a href=\"");
	templateBuilder.Append(linkurl("usermessage","system"));

	templateBuilder.Append("\">站内短信</a></li>\r\n        <li><a href=\"");
	templateBuilder.Append(linkurl("usercenter","invite"));

	templateBuilder.Append("\">邀请码</a></li>\r\n      </ul>\r\n      <h3>基本资料</h3>\r\n      <ul>\r\n        <li><a href=\"");
	templateBuilder.Append(linkurl("usercenter","proinfo"));

	templateBuilder.Append("\">个人资料</a></li>\r\n        <li><a href=\"");
	templateBuilder.Append(linkurl("usercenter","password"));

	templateBuilder.Append("\">修改密码</a></li>\r\n        <li><a href=\"");
	templateBuilder.Append(linkurl("usercenter","exit"));

	templateBuilder.Append("\">退出登录</a></li>\r\n      </ul>\r\n    </div>");


	templateBuilder.Append("\r\n    <!--/Sidebar-->\r\n  </div>\r\n  \r\n  <div class=\"right757\">\r\n    <h1 class=\"main_tit\">\r\n      <span><a href=\"#\">会员中心</a></span>\r\n      我的订单\r\n      <strong>Order</strong>\r\n    </h1>\r\n    \r\n    <div class=\"order_box\">\r\n      <!--订单状态.开始-->\r\n      <div style=\"text-align:center;\">\r\n      ");
	if (model.status<4)
	{

	templateBuilder.Append("\r\n        <div class=\"order_flow\" style=\"width:460px;\">\r\n          <div class=\"order_flow_left\">\r\n            <a class=\"order_flow_input\">生成</a>\r\n            <span><p class=\"name\">订单已生成</p><p>");
	templateBuilder.Append(Utils.ObjectToStr(model.add_time));
	templateBuilder.Append("</p></span>\r\n          </div>\r\n          ");
	if (payModel.type==1)
	{

	if (model.payment_status==2)
	{

	templateBuilder.Append("\r\n            <div class=\"order_flow_arrive\">\r\n              <a class=\"order_flow_input\">支付</a>\r\n              <span><p class=\"name\">已支付</p><p>");
	templateBuilder.Append(Utils.ObjectToStr(model.payment_time));
	templateBuilder.Append("</p></span>\r\n            </div>\r\n            ");
	}
	else
	{

	templateBuilder.Append("\r\n            <div class=\"order_flow_wait\">\r\n              <a class=\"order_flow_input\">支付</a>\r\n              <span><p class=\"name\">待支付</p></span>\r\n            </div>\r\n            ");
	}	//end if


	}	//end if


	if (model.status<2)
	{

	templateBuilder.Append("\r\n          <div class=\"order_flow_wait\">\r\n            <a class=\"order_flow_input\">确认</a>\r\n            <span><p class=\"name\">待确认</p></span>\r\n          </div>\r\n          ");
	}
	else
	{

	templateBuilder.Append("\r\n          <div class=\"order_flow_arrive\">\r\n            <a class=\"order_flow_input\">确认</a>\r\n            <span><p class=\"name\">订单已确认</p><p>");
	templateBuilder.Append(Utils.ObjectToStr(model.confirm_time));
	templateBuilder.Append("</p></span>\r\n          </div>\r\n          ");
	}	//end if


	if (model.distribution_status==1)
	{

	templateBuilder.Append("\r\n          <div class=\"order_flow_wait\">\r\n            <a class=\"order_flow_input\">发货</a>\r\n            <span><p class=\"name\">待发货</p></span>\r\n          </div>\r\n          ");
	}
	else
	{

	templateBuilder.Append("\r\n          <div class=\"order_flow_arrive\">\r\n            <a class=\"order_flow_input\">发货</a>\r\n            <span><p class=\"name\">已发货</p><p>");
	templateBuilder.Append(Utils.ObjectToStr(model.distribution_time));
	templateBuilder.Append("</p></span>\r\n          </div>\r\n          ");
	}	//end if


	if (model.status>2)
	{

	templateBuilder.Append("\r\n          <div class=\"order_flow_right_arrive\">\r\n            <a class=\"order_flow_input\">完成</a>\r\n            <span><p class=\"name\">订单已完成</p><p>");
	templateBuilder.Append(Utils.ObjectToStr(model.complete_time));
	templateBuilder.Append("</p></span>\r\n          </div>\r\n          ");
	}
	else
	{

	templateBuilder.Append("\r\n          <div class=\"order_flow_right_wait\">\r\n            <a class=\"order_flow_input\">完成</a>\r\n            <span><p class=\"name\">未完成</p></span>\r\n          </div>\r\n          ");
	}	//end if


	templateBuilder.Append("\r\n          <div class=\"clear\"></div>\r\n        </div>\r\n        ");
	}
	else if (model.status==4)
	{

	templateBuilder.Append("\r\n        <div style=\"text-align:center;line-height:50px; font-size:20px; color:Red;\">该订单已取消</div>\r\n        ");
	}
	else if (model.status==5)
	{

	templateBuilder.Append("\r\n        <div style=\"text-align:center;line-height:50px; font-size:20px; color:Red;\">该订单已作废</div>\r\n        ");
	}	//end if


	templateBuilder.Append("\r\n              \r\n      <div class=\"line10\"></div>\r\n      <div class=\"accept_box\">\r\n        <dl>\r\n          <dt>订单号：</dt>\r\n          <dd>");
	templateBuilder.Append(Utils.ObjectToStr(model.order_no));
	templateBuilder.Append("</dd>\r\n        </dl>\r\n        <dl>\r\n          <dt>订单状态：</dt>\r\n          <dd>\r\n            ");
	templateBuilder.Append(get_order_status(model.id).ToString());
	

	if (get_order_payment_status(model.id))
	{

	templateBuilder.Append("\r\n            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a class=\"btn btn-success\" href=\"");
	templateBuilder.Append(linkurl("payment1","confirm","BuyGoods",model.order_no));

	templateBuilder.Append("\">去付款</a>\r\n            ");
	}	//end if


	templateBuilder.Append("\r\n          </dd>\r\n        </dl>\r\n        <dl>\r\n          <dt>顾客姓名：</dt>\r\n          <dd>");
	templateBuilder.Append(Utils.ObjectToStr(model.accept_name));
	templateBuilder.Append("</dd>\r\n        </dl>\r\n        <dl>\r\n          <dt>送货地址：</dt>\r\n          <dd>");
	templateBuilder.Append(Utils.ObjectToStr(model.address));
	templateBuilder.Append("</dd>\r\n        </dl>\r\n        <dl>\r\n          <dt>手机号码：</dt>\r\n          <dd>");
	templateBuilder.Append(Utils.ObjectToStr(model.mobile));
	templateBuilder.Append("</dd>\r\n        </dl>\r\n        <dl>\r\n          <dt>固定电话：</dt>\r\n          <dd>");
	templateBuilder.Append(Utils.ObjectToStr(model.telphone));
	templateBuilder.Append("</dd>\r\n        </dl>\r\n        <dl>\r\n          <dt>备注留言：</dt>\r\n          <dd>");
	templateBuilder.Append(Utils.ObjectToStr(model.message));
	templateBuilder.Append("</dd>\r\n        </dl>\r\n      </div>\r\n              \r\n      <div class=\"line10\"></div>\r\n      <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\" class=\"ftable\">\r\n        <tr>\r\n          <th width=\"64\"></th>\r\n          <th align=\"left\">商品名称</th>\r\n          <th width=\"80\">销售价</td>\r\n          <th width=\"80\">优惠价</th>\r\n          <th width=\"60\">积分</th>\r\n          <th width=\"50\">数量</th>\r\n          <th width=\"100\">金额合计</th>\r\n          <th width=\"80\">积分合计</th>\r\n        </tr>\r\n        ");
	if (model.order_goods!=null)
	{

	int modelt__loop__id=0;
	foreach(DTcms.Model.order_goods modelt in model.order_goods)
	{
		modelt__loop__id++;


	templateBuilder.Append("\r\n        <tr>\r\n          <td><a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("goods_show",modelt.goods_id));

	templateBuilder.Append("\"><img src=\"");
	templateBuilder.Append(get_goods_img_url(modelt.goods_id).ToString());
	

	templateBuilder.Append("\" class=\"img\" /></a></td>\r\n          <td align=\"left\"><a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("goods_show",modelt.goods_id));

	templateBuilder.Append("\">");
	templateBuilder.Append(Utils.ObjectToStr(modelt.goods_name));
	templateBuilder.Append("</a></td>\r\n          <td align=\"center\">￥");
	templateBuilder.Append(Utils.ObjectToStr(modelt.goods_price));
	templateBuilder.Append("</td>\r\n          <td align=\"center\">￥");
	templateBuilder.Append(Utils.ObjectToStr(modelt.real_price));
	templateBuilder.Append("</td>\r\n          <td align=\"center\">\r\n            ");
	if (modelt.point>0)
	{

	templateBuilder.Append("\r\n            +\r\n            ");
	}	//end if


	templateBuilder.Append("\r\n            ");
	templateBuilder.Append(Utils.ObjectToStr(modelt.point));
	templateBuilder.Append("\r\n          </td>\r\n          <td align=\"center\">1</td>\r\n          <td align=\"center\"><font color=\"#FF0000\" size=\"2\">￥");
	templateBuilder.Append((modelt.real_price*modelt.quantity).ToString());
	

	templateBuilder.Append("</font></td>\r\n          <td align=\"center\"><font color=\"#FF0000\" size=\"2\">");
	templateBuilder.Append((modelt.point*modelt.quantity).ToString());
	

	templateBuilder.Append("</font></td>\r\n        </tr>\r\n        ");
	}	//end loop


	}
	else
	{

	templateBuilder.Append("\r\n        <tr><td colspan=\"8\" align=\"center\">暂无记录</td></tr>\r\n        ");
	}	//end if


	templateBuilder.Append("\r\n      </table>\r\n      <div class=\"line10\"></div>\r\n      <div style=\"text-align:right;\">\r\n        商品金额：<span class=\"red\">￥");
	templateBuilder.Append(Utils.ObjectToStr(model.real_amount));
	templateBuilder.Append("</span>&nbsp; &nbsp;+ &nbsp; 运费：<span class=\"red\">￥");
	templateBuilder.Append(Utils.ObjectToStr(model.real_freight));
	templateBuilder.Append("</span>&nbsp; &nbsp;+ &nbsp; 支付手续费：<span class=\"red\">￥");
	templateBuilder.Append(Utils.ObjectToStr(model.payment_fee));
	templateBuilder.Append("</span><br />\r\n        <b>应付总金额：<span class=\"red\">￥");
	templateBuilder.Append(Utils.ObjectToStr(model.order_amount));
	templateBuilder.Append("</span></b>\r\n      </div>\r\n      <div class=\"line10\"></div>\r\n    </div>\r\n\r\n  </div>\r\n</div>\r\n\r\n<div class=\"clear\"></div>\r\n\r\n<!--Footer-->\r\n");

	templateBuilder.Append("<div class=\"footer\">\r\n  <div class=\"footer_inner\">\r\n    <div class=\"clear\"></div>\r\n    <div class=\"copyright\" style=\"text-align:center\">\r\n      <p>版权所有 重庆热图科技有限公司 渝ICP备11064298号 © Copyright 2012 - 2013. rehu.co  All Rights Reserved.\r\n      </p>\r\n    </div>\r\n  </div>\r\n</div>");


	templateBuilder.Append("\r\n<!--/Footer-->\r\n</body>\r\n</html>\r\n");
	Response.Write(templateBuilder.ToString());
}
</script>
