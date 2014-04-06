<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.UI.Page.goods_list" ValidateRequest="false" %>
<%@ Import namespace="System.Collections.Generic" %>
<%@ Import namespace="System.Text" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="DTcms.Common" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	base.OnInit(e);
	StringBuilder templateBuilder = new StringBuilder(220000);
	const int channel_id = 2;

	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n");
	string category_title = get_category_title(category_id,"购物商城");
	

	templateBuilder.Append("\r\n<title>");
	templateBuilder.Append(Utils.ObjectToStr(category_title));
	templateBuilder.Append(" - ");
	templateBuilder.Append(Utils.ObjectToStr(config.webname));
	templateBuilder.Append("</title>\r\n<link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/pagination.css\" />\r\n<link media=\"screen\" type=\"text/css\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.templateskin));
	templateBuilder.Append("/css/style.css\" rel=\"stylesheet\">\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery-1.3.2.min.js\"></");
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


	templateBuilder.Append("\r\n<!--/Header-->\r\n\r\n<div class=\"boxwrap\">\r\n  <div class=\"left710\">\r\n   <!--Content-->\r\n    <div class=\"main_box\">\r\n      ");
	string category_nav = get_category_menu("goods_list1", category_id);
	

	templateBuilder.Append("\r\n      <dl class=\"head green\">\r\n        <dt>购物商城</dt>\r\n        <dd>\r\n          <span>当前位置：<a href=\"");
	templateBuilder.Append(linkurl("index",""));

	templateBuilder.Append("\">首页 </a>&gt;<a href=\"");
	templateBuilder.Append(linkurl("goods",""));

	templateBuilder.Append("\">购物商城</a>");
	templateBuilder.Append(Utils.ObjectToStr(category_nav));
	templateBuilder.Append("</span>\r\n        </dd>\r\n      </dl>\r\n      <div class=\"clear\"></div>\r\n      <h1 class=\"base_tit\">分类“");
	templateBuilder.Append(Utils.ObjectToStr(category_title));
	templateBuilder.Append("”的内容</h1>\r\n      <div class=\"pro_list\">\r\n        <ul>\r\n          ");
	DataTable goods_list = get_goods_list(channel_id, category_id, 16, page, "", out totalcount);
	

	templateBuilder.Append(" <!--取得一个DataTable-->\r\n          ");
	string pagelist = get_page_link(16, page, totalcount, "goods_list1", category_id, "__id__");
	

	templateBuilder.Append(" <!--取得分页页码列表-->\r\n          ");
	int listdr__loop__id=0;
	foreach(DataRow listdr in goods_list.Rows)
	{
		listdr__loop__id++;


	templateBuilder.Append("\r\n          <li>\r\n            <a class=\"pic\" href=\"");
	templateBuilder.Append(linkurl("goods_show",Utils.ObjectToStr(listdr["id"])));

	templateBuilder.Append("\" title=\"" + Utils.ObjectToStr(listdr["title"]) + "\"><img src=\"" + Utils.ObjectToStr(listdr["img_url"]) + "\" alt=\"" + Utils.ObjectToStr(listdr["title"]) + "\" /></a>\r\n            <div class=\"info\">\r\n              <a class=\"name\" href=\"");
	templateBuilder.Append(linkurl("goods_show",Utils.ObjectToStr(listdr["id"])));

	templateBuilder.Append("\" title=\"" + Utils.ObjectToStr(listdr["title"]) + "\">" + Utils.ObjectToStr(listdr["title"]) + "</a>\r\n              <div class=\"price\">\r\n                <span>价格：</span>\r\n                <strong>￥" + Utils.ObjectToStr(listdr["sell_price"]) + "</strong>\r\n              </div>\r\n            </div>\r\n          </li>\r\n          ");
	}	//end loop


	templateBuilder.Append("\r\n        </ul>\r\n      </div>\r\n      <div class=\"line20\"></div>\r\n      <div class=\"flickr\">");
	templateBuilder.Append(Utils.ObjectToStr(pagelist));
	templateBuilder.Append("</div> <!--放置页码列表-->\r\n    </div>\r\n    <!--/Content-->\r\n  </div>\r\n  \r\n  <div class=\"left264\">\r\n    <!--Sidebar-->\r\n    <div class=\"sidebar\">\r\n      <h3>商品类别</h3>\r\n      <ul class=\"navbar\">\r\n        ");
	DataTable category_list2 = get_category_child_list(channel_id, 0);
	

	int cdr2__loop__id=0;
	foreach(DataRow cdr2 in category_list2.Rows)
	{
		cdr2__loop__id++;


	templateBuilder.Append("\r\n        <li>\r\n          <h4><a href=\"");
	templateBuilder.Append(linkurl("goods_list",Utils.ObjectToStr(cdr2["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(cdr2["title"]) + "</a></h4>\r\n          <div class=\"list\">\r\n            ");
	DataTable category_list21 = get_category_child_list(channel_id, Utils.StrToInt(Utils.ObjectToStr(cdr2["id"]), 0));
	

	int cdr21__loop__id=0;
	foreach(DataRow cdr21 in category_list21.Rows)
	{
		cdr21__loop__id++;


	if (Utils.StrToInt(Utils.ObjectToStr(cdr21["id"]), 0)==category_id)
	{

	templateBuilder.Append("\r\n            <a href=\"");
	templateBuilder.Append(linkurl("goods_list",Utils.ObjectToStr(cdr21["id"])));

	templateBuilder.Append("\" class=\"current\">" + Utils.ObjectToStr(cdr21["title"]) + "</a>\r\n            ");
	}
	else
	{

	templateBuilder.Append("\r\n            <a href=\"");
	templateBuilder.Append(linkurl("goods_list",Utils.ObjectToStr(cdr21["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(cdr21["title"]) + "</a>\r\n            ");
	}	//end if


	}	//end loop


	templateBuilder.Append("\r\n          </div>\r\n        </li>\r\n        ");
	}	//end loop


	templateBuilder.Append("\r\n      </ul>\r\n      <div class=\"clear\"></div>\r\n      <h3><a class=\"arrow\" href=\"");
	templateBuilder.Append(linkurl("goods"));

	templateBuilder.Append("\" title=\"查看更多\">&gt;</a>推荐商品</h3>\r\n      <div class=\"focus_list\">\r\n        <ul>\r\n          ");
	DataTable redgoods = get_goods_list(channel_id, 0, 6, "is_red=1 and img_url<>''");
	

	int reddr__loop__id=0;
	foreach(DataRow reddr in redgoods.Rows)
	{
		reddr__loop__id++;


	templateBuilder.Append("\r\n          <li>\r\n            <a title=\"" + Utils.ObjectToStr(reddr["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("goods_show",Utils.ObjectToStr(reddr["id"])));

	templateBuilder.Append("\">\r\n              <img src=\"" + Utils.ObjectToStr(reddr["img_url"]) + "\" width=\"100\" height=\"100\" alt=\"" + Utils.ObjectToStr(reddr["title"]) + "\" />\r\n              <span>" + Utils.ObjectToStr(reddr["title"]) + "</span>\r\n            </a>\r\n          </li>\r\n          ");
	}	//end loop


	templateBuilder.Append("\r\n        </ul>\r\n        <div class=\"clear\"></div>\r\n      </div>\r\n      <h3><a class=\"arrow\" href=\"");
	templateBuilder.Append(linkurl("goods"));

	templateBuilder.Append("\" title=\"查看更多\">&gt;</a>人气排行</h3>\r\n      <ul class=\"rank_list\">\r\n        ");
	DataTable hotgoods = get_goods_list(channel_id, 0, 10, "", "click desc,id desc");
	

	int hotdr__loop__id=0;
	foreach(DataRow hotdr in hotgoods.Rows)
	{
		hotdr__loop__id++;


	if (hotdr__loop__id==1)
	{

	templateBuilder.Append("\r\n        <li class=\"active\">\r\n        ");
	}
	else
	{

	templateBuilder.Append("\r\n        <li>\r\n        ");
	}	//end if


	templateBuilder.Append("\r\n          <span>");	templateBuilder.Append(Utils.ObjectToDateTime(Utils.ObjectToStr(hotdr["add_time"])).ToString("MM-dd"));

	templateBuilder.Append("</span>\r\n          <i class=\"num\">");
	templateBuilder.Append(hotdr__loop__id.ToString());
	

	templateBuilder.Append("</i><a href=\"");
	templateBuilder.Append(linkurl("goods_show",Utils.ObjectToStr(hotdr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(hotdr["title"]) + "</a>\r\n        </li>\r\n        ");
	}	//end loop


	templateBuilder.Append("\r\n      </ul>\r\n    </div>\r\n    <!--/Sidebar-->\r\n  </div>\r\n</div>\r\n\r\n<div class=\"clear\"></div>\r\n\r\n<!--Footer-->\r\n");

	templateBuilder.Append("<div class=\"footer\">\r\n  <div class=\"footer_inner\">\r\n    <div class=\"clear\"></div>\r\n    <div class=\"copyright\" style=\"text-align:center\">\r\n      <p>版权所有 重庆热图科技有限公司 渝ICP备11064298号 © Copyright 2012 - 2013. rehu.co  All Rights Reserved.\r\n      </p>\r\n    </div>\r\n  </div>\r\n</div>");


	templateBuilder.Append("\r\n<!--/Footer-->\r\n</body>\r\n</html>\r\n");
	Response.Write(templateBuilder.ToString());
}
</script>
