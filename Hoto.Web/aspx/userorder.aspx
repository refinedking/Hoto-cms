<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.UI.Page.userorder" ValidateRequest="false" %>
<%@ Import namespace="System.Collections.Generic" %>
<%@ Import namespace="System.Text" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="DTcms.Common" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	base.OnInit(e);
	StringBuilder templateBuilder = new StringBuilder(220000);

	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>我的订单 - ");
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


	templateBuilder.Append("\r\n<!--/Header-->\r\n\r\n<div class=\"boxwrap\">\r\n  <div class=\"left180\">\r\n    <!--Sidebar-->\r\n    ");

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


	templateBuilder.Append("\r\n    <!--/Sidebar-->\r\n  </div>\r\n  \r\n  <div class=\"right757\">\r\n    <h1 class=\"main_tit\">\r\n      <span><a href=\"");
	templateBuilder.Append(linkurl("usercenter","index"));

	templateBuilder.Append("\">管理首页</a></span>\r\n      我的订单\r\n      <strong>Order</strong>\r\n    </h1>\r\n    ");
	if (action=="list")
	{

	templateBuilder.Append("\r\n    <!--交易订单-->\r\n    <div class=\"tab_head\">\r\n      <ul class=\"tabs\">\r\n        <li class=\"selected\"><a href=\"");
	templateBuilder.Append(linkurl("userorder","list"));

	templateBuilder.Append("\">交易订单</a></li>\r\n        <li><a href=\"");
	templateBuilder.Append(linkurl("userorder","close"));

	templateBuilder.Append("\">已关闭订单</a></li>\r\n      </ul>\r\n    </div>\r\n    \r\n    <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"ftable\">\r\n      <tr>\r\n        <th width=\"16%\" align=\"left\">订单号</th>\r\n        <th align=\"left\">订单商品</th>\r\n        <th width=\"10%\">收货人</th>\r\n        <th width=\"12%\">订单金额</th>\r\n        <th width=\"11%\">下单时间</th>\r\n        <th width=\"10%\">订单状态</th>\r\n        <th width=\"12%\">操作</th>\r\n      </tr>\r\n      ");
	DataTable list1 = get_order_list(10, page, "user_id='"+userModel.id+"' and status<=3", out totalcount);
	

	templateBuilder.Append(" <!--取得一个DataTable-->\r\n      ");
	string pagelist1 = get_page_link(10, page, totalcount, "userorder1", action, "__id__");
	

	templateBuilder.Append(" <!--取得分页页码列表-->\r\n      ");
	int dr__loop__id=0;
	foreach(DataRow dr in list1.Rows)
	{
		dr__loop__id++;


	templateBuilder.Append("\r\n      <tr>\r\n        <td>" + Utils.ObjectToStr(dr["order_no"]) + "</td>\r\n        <td>\r\n          ");
	List<DTcms.Model.article_goods> ls = get_order_goods_list(Utils.StrToInt(Utils.ObjectToStr(dr["id"]), 0));
	

	if (ls!=null)
	{

	int modelt__loop__id=0;
	foreach(DTcms.Model.article_goods modelt in ls)
	{
		modelt__loop__id++;


	templateBuilder.Append("\r\n          <a target=\"_blank\" title=\"");
	templateBuilder.Append(Utils.ObjectToStr(modelt.title));
	templateBuilder.Append("\" href=\"");
	templateBuilder.Append(linkurl("goods_show",modelt.id));

	templateBuilder.Append("\"><img width=\"60\" height=\"60\" alt=\"");
	templateBuilder.Append(Utils.ObjectToStr(modelt.title));
	templateBuilder.Append("\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(modelt.img_url));
	templateBuilder.Append("\"></a>\r\n          ");
	}	//end loop


	}	//end if


	templateBuilder.Append("\r\n        </td>\r\n        <td align=\"center\">" + Utils.ObjectToStr(dr["accept_name"]) + "</td>\r\n        <td align=\"center\"><strong>￥" + Utils.ObjectToStr(dr["order_amount"]) + "</strong><br />");
	templateBuilder.Append(get_payment_title(Utils.StrToInt(Utils.ObjectToStr(dr["payment_id"]), 0)).ToString());
	

	templateBuilder.Append("</td>\r\n        <td align=\"center\">" + Utils.ObjectToStr(dr["add_time"]) + "</td>\r\n        <td align=\"center\">\r\n          ");
	templateBuilder.Append(get_order_status(Utils.StrToInt(Utils.ObjectToStr(dr["id"]), 0)).ToString());
	

	templateBuilder.Append("\r\n        </td>\r\n        <td align=\"center\">\r\n          <a href=\"");
	templateBuilder.Append(linkurl("userorder_show",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">查看订单</a><br />\r\n          ");
	if (get_order_payment_status(Utils.StrToInt(Utils.ObjectToStr(dr["id"]), 0)))
	{

	templateBuilder.Append("\r\n          <a href=\"");
	templateBuilder.Append(linkurl("payment1","confirm","BuyGoods",Utils.ObjectToStr(dr["order_no"])));

	templateBuilder.Append("\">去付款</a> |\r\n          ");
	}	//end if


	if (Utils.StrToInt(Utils.ObjectToStr(dr["status"]), 0)<2)
	{

	templateBuilder.Append("\r\n          <a href=\"javascript:;\" onclick=\"clickSubmit('");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=order_cancel&order_no=" + Utils.ObjectToStr(dr["order_no"]) + "');\">取消</a>\r\n          ");
	}	//end if


	templateBuilder.Append("\r\n        </td>\r\n      </tr>\r\n      ");
	}	//end loop


	if (totalcount<1)
	{

	templateBuilder.Append("\r\n      <tr><td colspan=\"8\" align=\"center\">暂无记录</td></tr>\r\n      ");
	}	//end if


	templateBuilder.Append("\r\n    </table>\r\n    \r\n    <div class=\"page_foot\">\r\n      <div class=\"flickr right\"></div><!--放置页码列表-->\r\n    </div>\r\n    <!--交易订单-->\r\n    \r\n    ");
	}
	else if (action=="close")
	{

	templateBuilder.Append("\r\n    <div class=\"tab_head\">\r\n      <ul class=\"tabs\">\r\n        <li><a href=\"");
	templateBuilder.Append(linkurl("userorder","list"));

	templateBuilder.Append("\">交易订单</a></li>\r\n        <li class=\"selected\"><a href=\"");
	templateBuilder.Append(linkurl("userorder","close"));

	templateBuilder.Append("\">已关闭订单</a></li>\r\n      </ul>\r\n    </div>\r\n    \r\n    <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"ftable\">\r\n      <tr>\r\n        <th width=\"16%\" align=\"left\">订单号</th>\r\n        <th align=\"left\">订单商品</th>\r\n        <th width=\"10%\">收货人</th>\r\n        <th width=\"12%\">订单金额</th>\r\n        <th width=\"11%\">下单时间</th>\r\n        <th width=\"10%\">订单状态</th>\r\n        <th width=\"12%\">操作</th>\r\n      </tr>\r\n      ");
	DataTable list2 = get_order_list(10, page, "user_id='"+userModel.id+"' and status>3", out totalcount);
	

	templateBuilder.Append(" <!--取得一个DataTable-->\r\n      ");
	string pagelist2 = get_page_link(10, page, totalcount, "userorder1", action, "__id__");
	

	templateBuilder.Append(" <!--取得分页页码列表-->\r\n      ");
	int dr__loop__id=0;
	foreach(DataRow dr in list2.Rows)
	{
		dr__loop__id++;


	templateBuilder.Append("\r\n      <tr>\r\n        <td>" + Utils.ObjectToStr(dr["order_no"]) + "</td>\r\n        <td>\r\n          ");
	List<DTcms.Model.article_goods> ls = get_order_goods_list(Utils.StrToInt(Utils.ObjectToStr(dr["id"]), 0));
	

	if (ls!=null)
	{

	int modelt__loop__id=0;
	foreach(DTcms.Model.article_goods modelt in ls)
	{
		modelt__loop__id++;


	templateBuilder.Append("\r\n          <a target=\"_blank\" title=\"");
	templateBuilder.Append(Utils.ObjectToStr(modelt.title));
	templateBuilder.Append("\" href=\"");
	templateBuilder.Append(linkurl("goods_show",modelt.id));

	templateBuilder.Append("\"><img width=\"60\" height=\"60\" alt=\"");
	templateBuilder.Append(Utils.ObjectToStr(modelt.title));
	templateBuilder.Append("\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(modelt.img_url));
	templateBuilder.Append("\"></a>\r\n          ");
	}	//end loop


	}	//end if


	templateBuilder.Append("\r\n        </td>\r\n        <td align=\"center\">" + Utils.ObjectToStr(dr["accept_name"]) + "</td>\r\n        <td align=\"center\"><strong>￥" + Utils.ObjectToStr(dr["order_amount"]) + "</strong><br />");
	templateBuilder.Append(get_payment_title(Utils.StrToInt(Utils.ObjectToStr(dr["payment_id"]), 0)).ToString());
	

	templateBuilder.Append("</td>\r\n        <td align=\"center\">" + Utils.ObjectToStr(dr["add_time"]) + "</td>\r\n        <td align=\"center\">");
	templateBuilder.Append(get_order_status(Utils.StrToInt(Utils.ObjectToStr(dr["id"]), 0)).ToString());
	

	templateBuilder.Append("</td>\r\n        <td align=\"center\"><a href=\"");
	templateBuilder.Append(linkurl("userorder_show",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">查看订单</a></td>\r\n      </tr>\r\n      ");
	}	//end loop


	if (totalcount<1)
	{

	templateBuilder.Append("\r\n      <tr><td colspan=\"8\" align=\"center\">暂无记录</td></tr>\r\n      ");
	}	//end if


	templateBuilder.Append("\r\n    </table>\r\n    \r\n    <div class=\"page_foot\">\r\n      <div class=\"flickr right\">");
	templateBuilder.Append(Utils.ObjectToStr(pagelist2));
	templateBuilder.Append("</div><!--放置页码列表-->\r\n    </div>\r\n    ");
	}	//end if


	templateBuilder.Append("\r\n  </div>\r\n</div>\r\n\r\n<div class=\"clear\"></div>\r\n\r\n<!--Footer-->\r\n");

	templateBuilder.Append("<div class=\"footer\">\r\n  <div class=\"footer_inner\">\r\n    <div class=\"clear\"></div>\r\n    <div class=\"copyright\" style=\"text-align:center\">\r\n      <p>版权所有 重庆热图科技有限公司 渝ICP备11064298号 © Copyright 2012 - 2013. rehu.co  All Rights Reserved.\r\n      </p>\r\n    </div>\r\n  </div>\r\n</div>");


	templateBuilder.Append("\r\n<!--/Footer-->\r\n</body>\r\n</html>\r\n");
	Response.Write(templateBuilder.ToString());
}
</script>
