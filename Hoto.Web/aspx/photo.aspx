<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.UI.Page.news" ValidateRequest="false" %>
<%@ Import namespace="System.Collections.Generic" %>
<%@ Import namespace="System.Text" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="DTcms.Common" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	base.OnInit(e);
	StringBuilder templateBuilder = new StringBuilder(220000);
	const int channel_id = 3;

	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>图片分享 - ");
	templateBuilder.Append(Utils.ObjectToStr(config.webname));
	templateBuilder.Append("</title>\r\n<link media=\"screen\" type=\"text/css\" href=\"");
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


	templateBuilder.Append("\r\n<!--/Header-->\r\n\r\n<div class=\"boxwrap\">\r\n  <div class=\"left710\">\r\n   <!--Content-->\r\n    <div class=\"main_box\">\r\n      <dl class=\"head green\">\r\n        <dt>图片分享</dt>\r\n        <dd>\r\n          <ul class=\"sub_nav\">\r\n            ");
	DataTable category_list1 = get_category_child_list(channel_id, 0);
	

	int cdr1__loop__id=0;
	foreach(DataRow cdr1 in category_list1.Rows)
	{
		cdr1__loop__id++;


	templateBuilder.Append("\r\n            <li class=\"n");
	templateBuilder.Append(cdr1__loop__id.ToString());
	

	templateBuilder.Append("\"><a href=\"");
	templateBuilder.Append(linkurl("photo_list",Utils.ObjectToStr(cdr1["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(cdr1["title"]) + "</a></li>\r\n            ");
	}	//end loop


	templateBuilder.Append("\r\n          </ul>\r\n        </dd>\r\n      </dl>\r\n      <div class=\"clear\"></div>\r\n      <div class=\"photo_list\">\r\n        <ul>\r\n          ");
	DataTable photo_list = get_news_list(channel_id, 0, 12, "is_red=1");
	

	int listdr__loop__id=0;
	foreach(DataRow listdr in photo_list.Rows)
	{
		listdr__loop__id++;


	templateBuilder.Append("\r\n          <li>\r\n            <a class=\"img\" href=\"");
	templateBuilder.Append(linkurl("photo_show",Utils.ObjectToStr(listdr["id"])));

	templateBuilder.Append("\" title=\"" + Utils.ObjectToStr(listdr["title"]) + "\">\r\n              <img src=\"" + Utils.ObjectToStr(listdr["img_url"]) + "\" alt=\"" + Utils.ObjectToStr(listdr["title"]) + "\" />\r\n              <div class=\"info\">\r\n                <h4>" + Utils.ObjectToStr(listdr["title"]) + "</h4>\r\n                <p>" + Utils.ObjectToStr(listdr["zhaiyao"]) + "</p>\r\n              </div>\r\n            </a>\r\n          </li>\r\n          ");
	}	//end loop


	templateBuilder.Append("\r\n          \r\n        </ul>\r\n      </div>\r\n      \r\n    </div>\r\n    <!--/Content-->\r\n  </div>\r\n  \r\n  <div class=\"left264\">\r\n    <!--Sidebar-->\r\n    <div class=\"sidebar\">\r\n      <h3>资源类别</h3>\r\n      <ul>\r\n        ");
	DataTable category_list = get_category_list(channel_id, 0);
	

	int cdr__loop__id=0;
	foreach(DataRow cdr in category_list.Rows)
	{
		cdr__loop__id++;


	templateBuilder.Append("\r\n        <li><a title=\"" + Utils.ObjectToStr(cdr["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("photo_list",Utils.ObjectToStr(cdr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(cdr["title"]) + "</a></li>\r\n        ");
	}	//end loop


	templateBuilder.Append("\r\n      </ul>\r\n      <h3>推荐图片</h3>\r\n      <div class=\"focus_list\">\r\n        <ul>\r\n          ");
	DataTable redphoto = get_news_list(channel_id, 0, 6, "is_red=1 and img_url<>''");
	

	int reddr__loop__id=0;
	foreach(DataRow reddr in redphoto.Rows)
	{
		reddr__loop__id++;


	templateBuilder.Append("\r\n          <li>\r\n            <a title=\"" + Utils.ObjectToStr(reddr["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("photo_show",Utils.ObjectToStr(reddr["id"])));

	templateBuilder.Append("\">\r\n              <img src=\"" + Utils.ObjectToStr(reddr["img_url"]) + "\" width=\"100\" height=\"100\" alt=\"" + Utils.ObjectToStr(reddr["title"]) + "\" />\r\n              <span>" + Utils.ObjectToStr(reddr["title"]) + "</span>\r\n            </a>\r\n          </li>\r\n          ");
	}	//end loop


	templateBuilder.Append("\r\n        </ul>\r\n        <div class=\"clear\"></div>\r\n      </div>\r\n      <h3>人气排行</h3>\r\n      <ul class=\"rank_list\">\r\n        ");
	DataTable hotphoto = get_news_list(channel_id, 0, 10, "", "click desc,id desc");
	

	int hotdr__loop__id=0;
	foreach(DataRow hotdr in hotphoto.Rows)
	{
		hotdr__loop__id++;


	if (hotdr__loop__id==1)
	{

	templateBuilder.Append("\r\n         <li class=\"active\">\r\n         ");
	}
	else
	{

	templateBuilder.Append("\r\n         <li>\r\n         ");
	}	//end if


	templateBuilder.Append("\r\n          <span>");	templateBuilder.Append(Utils.ObjectToDateTime(Utils.ObjectToStr(hotdr["add_time"])).ToString("MM-dd"));

	templateBuilder.Append("</span>\r\n          <i class=\"num\">");
	templateBuilder.Append(hotdr__loop__id.ToString());
	

	templateBuilder.Append("</i><a href=\"");
	templateBuilder.Append(linkurl("photo_show",Utils.ObjectToStr(hotdr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(hotdr["title"]) + "</a>\r\n        </li>\r\n        ");
	}	//end loop


	templateBuilder.Append("\r\n      </ul>\r\n    </div>\r\n    <!--/Sidebar-->\r\n  </div>\r\n</div>\r\n\r\n<div class=\"clear\"></div>\r\n\r\n<!--Footer-->\r\n");

	templateBuilder.Append("<div class=\"footer\">\r\n  <div class=\"footer_inner\">\r\n    <div class=\"clear\"></div>\r\n    <div class=\"copyright\" style=\"text-align:center\">\r\n      <p>版权所有 重庆热图科技有限公司 渝ICP备11064298号 © Copyright 2012 - 2013. rehu.co  All Rights Reserved.\r\n      </p>\r\n    </div>\r\n  </div>\r\n</div>");


	templateBuilder.Append("\r\n<!--/Footer-->\r\n</body>\r\n</html>\r\n");
	Response.Write(templateBuilder.ToString());
}
</script>
