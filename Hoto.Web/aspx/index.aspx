<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.UI.Page.index" ValidateRequest="false" %>
<%@ Import namespace="System.Collections.Generic" %>
<%@ Import namespace="System.Text" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="DTcms.Common" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	base.OnInit(e);
	StringBuilder templateBuilder = new StringBuilder(220000);

	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>");
	templateBuilder.Append(Utils.ObjectToStr(config.webtitle));
	templateBuilder.Append("</title>\r\n<meta content=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webkeyword));
	templateBuilder.Append("\" name=\"keywords\">\r\n<meta content=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webdescription));
	templateBuilder.Append("\" name=\"description\">\r\n<link media=\"screen\" type=\"text/css\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.templateskin));
	templateBuilder.Append("/css/style.css\" rel=\"stylesheet\">\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery-1.3.2.min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery.KinSlideshow-1.2.1.min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.templateskin));
	templateBuilder.Append("/js/base.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\">\r\n$(function(){\r\n	$(\"#focusNews\").KinSlideshow();\r\n})\r\n</");
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


	templateBuilder.Append("\r\n<!--/Header-->\r\n\r\n<div class=\"boxwrap\">\r\n  <div class=\"left710\">\r\n    <div class=\"pad20\">\r\n      <!--Focus-->\r\n      <div class=\"left300\">\r\n        <div id=\"focusNews\" class=\"ifocus\">\r\n          ");
	DataTable focusNews = get_news_list(1, 0, 8, "is_lock=0 and is_slide=1");
	

	int newsdr__loop__id=0;
	foreach(DataRow newsdr in focusNews.Rows)
	{
		newsdr__loop__id++;


	templateBuilder.Append("\r\n          <a title=\"" + Utils.ObjectToStr(newsdr["title"]) + "\" target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("news_show",Utils.ObjectToStr(newsdr["id"])));

	templateBuilder.Append("\"><img src=\"" + Utils.ObjectToStr(newsdr["img_url"]) + "\" alt=\"" + Utils.ObjectToStr(newsdr["title"]) + "\" /></a>\r\n          ");
	}	//end loop


	templateBuilder.Append("\r\n        </div>\r\n      </div>\r\n      <!--/Focus-->\r\n      <!--Top News-->\r\n      <div class=\"right350 icnts\">\r\n        ");
	DataTable topnews = get_news_list(1, 0, 2, "is_lock=0 and is_top=1");
	

	int newsdr1__loop__id=0;
	foreach(DataRow newsdr1 in topnews.Rows)
	{
		newsdr1__loop__id++;


	if (newsdr1__loop__id==1)
	{

	templateBuilder.Append("\r\n        <h3 class=\"tit\"><a title=\"" + Utils.ObjectToStr(newsdr1["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("news_show",Utils.ObjectToStr(newsdr1["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(newsdr1["title"]) + "</a></h3>\r\n        ");
	}	//end if


	}	//end loop


	templateBuilder.Append("\r\n        <ul class=\"list\">\r\n          ");
	DataTable rednews = get_news_list(1, 0, 8, "is_lock=0 and is_red=1");
	

	int newsdr11__loop__id=0;
	foreach(DataRow newsdr11 in rednews.Rows)
	{
		newsdr11__loop__id++;


	if (newsdr11__loop__id<=4)
	{

	templateBuilder.Append("\r\n          <li>\r\n            <span>[");
	templateBuilder.Append(get_category_title(Utils.StrToInt(Utils.ObjectToStr(newsdr11["category_id"]), 0), "").ToString());
	

	templateBuilder.Append("]</span>\r\n            <a href=\"");
	templateBuilder.Append(linkurl("news_show",Utils.ObjectToStr(newsdr11["id"])));

	templateBuilder.Append("\" title=\"" + Utils.ObjectToStr(newsdr11["title"]) + "\">" + Utils.ObjectToStr(newsdr11["title"]) + "</a>\r\n          </li>\r\n          ");
	}	//end if


	}	//end loop


	templateBuilder.Append("\r\n        </ul>\r\n        ");
	int newsdr2__loop__id=0;
	foreach(DataRow newsdr2 in topnews.Rows)
	{
		newsdr2__loop__id++;


	if (newsdr2__loop__id==2)
	{

	templateBuilder.Append("\r\n        <h3 class=\"tit\"><a href=\"");
	templateBuilder.Append(linkurl("news_show",Utils.ObjectToStr(newsdr2["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(newsdr2["title"]) + "</a></h3>\r\n        ");
	}	//end if


	}	//end loop


	templateBuilder.Append("\r\n        <ul class=\"list last\">\r\n          ");
	int newsdr12__loop__id=0;
	foreach(DataRow newsdr12 in rednews.Rows)
	{
		newsdr12__loop__id++;


	if (newsdr12__loop__id>4)
	{

	templateBuilder.Append("\r\n          <li>\r\n            <span>[");
	templateBuilder.Append(get_category_title(Utils.StrToInt(Utils.ObjectToStr(newsdr12["category_id"]), 0), "").ToString());
	

	templateBuilder.Append("]</span>\r\n            <a href=\"");
	templateBuilder.Append(linkurl("news_show",Utils.ObjectToStr(newsdr12["id"])));

	templateBuilder.Append("\" title=\"" + Utils.ObjectToStr(newsdr12["title"]) + "\">" + Utils.ObjectToStr(newsdr12["title"]) + "</a>\r\n          </li>\r\n          ");
	}	//end if


	}	//end loop


	templateBuilder.Append("\r\n        </ul>\r\n      </div>\r\n      <!--/Top News-->\r\n      <div class=\"clear\"></div>\r\n    </div>\r\n    <!--Hot Goods-->\r\n    <div class=\"clear\"></div>\r\n    <div class=\"igoods_box\">\r\n      <div class=\"igoods_list\">\r\n        <dl class=\"head\">\r\n          <dt>购物商城</dt>\r\n          <dd>\r\n            <ul class=\"sub_nav\">\r\n              ");
	DataTable category_list1 = get_category_child_list(2, 0);
	

	int cdr1__loop__id=0;
	foreach(DataRow cdr1 in category_list1.Rows)
	{
		cdr1__loop__id++;


	templateBuilder.Append("\r\n              <li class=\"n");
	templateBuilder.Append(cdr1__loop__id.ToString());
	

	templateBuilder.Append("\"><a href=\"");
	templateBuilder.Append(linkurl("goods_list",Utils.ObjectToStr(cdr1["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(cdr1["title"]) + "</a></li>\r\n              ");
	}	//end loop


	templateBuilder.Append("\r\n            </ul>\r\n          </dd>\r\n        </dl>\r\n        \r\n        <ul class=\"list\">\r\n          ");
	DataTable redgoods = get_goods_list(2, 0, 8, "is_lock=0 and is_red=1");
	

	int goodsdr1__loop__id=0;
	foreach(DataRow goodsdr1 in redgoods.Rows)
	{
		goodsdr1__loop__id++;


	templateBuilder.Append("\r\n          <li>\r\n            <a class=\"pic\" href=\"");
	templateBuilder.Append(linkurl("goods_show",Utils.ObjectToStr(goodsdr1["id"])));

	templateBuilder.Append("\"><img src=\"" + Utils.ObjectToStr(goodsdr1["img_url"]) + "\" alt=\"" + Utils.ObjectToStr(goodsdr1["title"]) + "\" /></a>\r\n            <div class=\"info\">\r\n              <a class=\"name\" href=\"");
	templateBuilder.Append(linkurl("goods_show",Utils.ObjectToStr(goodsdr1["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(goodsdr1["title"]) + "</a>\r\n              <div class=\"price\">\r\n                <span>价格：</span>\r\n                <strong>￥" + Utils.ObjectToStr(goodsdr1["sell_price"]) + "</strong>\r\n              </div>\r\n            </div>\r\n          </li>\r\n          ");
	}	//end loop


	templateBuilder.Append("\r\n        </ul>\r\n      </div>\r\n    </div>\r\n    <!--/Hot Goods-->\r\n  </div>\r\n  \r\n  <div class=\"left264\">\r\n    <!--Red Download-->\r\n    <div class=\"idown_list\">\r\n      <h3>推荐资源</h3>\r\n      <ul>\r\n        ");
	DataTable reddown = get_download_list(4, 0, 5, "is_red=1 and img_url<>''");
	

	int downdr__loop__id=0;
	foreach(DataRow downdr in reddown.Rows)
	{
		downdr__loop__id++;


	templateBuilder.Append("\r\n        <li><a href=\"");
	templateBuilder.Append(linkurl("down_show",Utils.ObjectToStr(downdr["id"])));

	templateBuilder.Append("\"> <img border=\"0\" alt=\"" + Utils.ObjectToStr(downdr["title"]) + "\" src=\"" + Utils.ObjectToStr(downdr["img_url"]) + "\" /> </a> <a href=\"");
	templateBuilder.Append(linkurl("down_show",Utils.ObjectToStr(downdr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(downdr["title"]) + "</a> <br />\r\n          <span class=\"date\">更新：");	templateBuilder.Append(Utils.ObjectToDateTime(Utils.ObjectToStr(downdr["add_time"])).ToString("yyyy-MM-dd"));

	templateBuilder.Append("</span> <br />\r\n          <a class=\"down\" href=\"");
	templateBuilder.Append(linkurl("down_show",Utils.ObjectToStr(downdr["id"])));

	templateBuilder.Append("\">下载</a>\r\n        </li>\r\n        ");
	}	//end loop


	templateBuilder.Append("\r\n      </ul>\r\n    </div>\r\n    <!--/Red Download-->\r\n    <!--Hot Goods-->\r\n    <div class=\"isidebar\">\r\n      <h3>热门商品</h3>\r\n      <div class=\"list\">\r\n        <ol> \r\n          ");
	DataTable hotgoods = get_goods_list(2, 0, 10, "is_lock=0 and is_hot=1", "click desc,id desc");
	

	int goodsdr2__loop__id=0;
	foreach(DataRow goodsdr2 in hotgoods.Rows)
	{
		goodsdr2__loop__id++;


	if (goodsdr2__loop__id==1)
	{

	templateBuilder.Append("\r\n          <li class=\"active\" onmouseover=\"ToggleProps(this, 'active');\">\r\n          ");
	}
	else
	{

	templateBuilder.Append("\r\n          <li onmousemove=\"ToggleProps(this, 'active');\">\r\n          ");
	}	//end if


	templateBuilder.Append("\r\n            <i class=\"num\">");
	templateBuilder.Append(goodsdr2__loop__id.ToString());
	

	templateBuilder.Append("</i>\r\n            <div class=\"photo\">\r\n              <a title=\"" + Utils.ObjectToStr(goodsdr2["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("goods_show",Utils.ObjectToStr(goodsdr2["id"])));

	templateBuilder.Append("\"><img src=\"" + Utils.ObjectToStr(goodsdr2["img_url"]) + "\" alt=\"" + Utils.ObjectToStr(goodsdr2["title"]) + "\"></a>\r\n            </div>\r\n            <div class=\"props\">\r\n              <p class=\"name\"><a title=\"" + Utils.ObjectToStr(goodsdr2["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("goods_show",Utils.ObjectToStr(goodsdr2["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(goodsdr2["title"]) + "</a></p>\r\n              <p class=\"price\">¥" + Utils.ObjectToStr(goodsdr2["sell_price"]) + "</p>\r\n              <p class=\"button\"><a class=\"ibtn\" href=\"");
	templateBuilder.Append(linkurl("goods_show",Utils.ObjectToStr(goodsdr2["id"])));

	templateBuilder.Append("\">立即购买</a></p>\r\n            </div>\r\n          </li>\r\n          ");
	}	//end loop


	templateBuilder.Append("\r\n          </ol>\r\n      </div>\r\n    </div>\r\n    <!--Hot Goods-->\r\n  </div>\r\n</div>\r\n\r\n<div class=\"clear\"></div>\r\n<div class=\"boxwrap\" style=\"border-top:0;\">\r\n  <!--Red Photo-->\r\n  <div class=\"iphoto_list\">\r\n    <dl class=\"head\">\r\n      <dt>图片分享</dt>\r\n      <dd>\r\n        <ul class=\"sub_nav\">\r\n          ");
	DataTable category_list2 = get_category_child_list(3, 0);
	

	int cdr2__loop__id=0;
	foreach(DataRow cdr2 in category_list2.Rows)
	{
		cdr2__loop__id++;


	templateBuilder.Append("\r\n          <li class=\"n");
	templateBuilder.Append(cdr2__loop__id.ToString());
	

	templateBuilder.Append("\"><a href=\"");
	templateBuilder.Append(linkurl("photo_list",Utils.ObjectToStr(cdr2["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(cdr2["title"]) + "</a></li>\r\n          ");
	}	//end loop


	templateBuilder.Append("\r\n        </ul>\r\n      </dd>\r\n    </dl>\r\n    <div class=\"clear\"></div>\r\n    <div class=\"list\">\r\n      <ul>\r\n        ");
	DataTable redphoto = get_news_list(3, 0, 12, "is_lock=0 and is_red=1");
	

	int photodr1__loop__id=0;
	foreach(DataRow photodr1 in redphoto.Rows)
	{
		photodr1__loop__id++;


	templateBuilder.Append("\r\n        <li class=\"a");
	templateBuilder.Append(photodr1__loop__id.ToString());
	

	templateBuilder.Append("\">\r\n          <img src=\"" + Utils.ObjectToStr(photodr1["img_url"]) + "\" alt=\"" + Utils.ObjectToStr(photodr1["title"]) + "\" />\r\n          <a title=\"" + Utils.ObjectToStr(photodr1["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("photo_show",Utils.ObjectToStr(photodr1["id"])));

	templateBuilder.Append("\"><strong>" + Utils.ObjectToStr(photodr1["title"]) + "</strong><br><span>");
	templateBuilder.Append(Utils.DropHTML(Utils.ObjectToStr(photodr1["zhaiyao"]),28));

	templateBuilder.Append("</span><br>查看详情</a><i class=\"absbg\"></i>\r\n        </li>\r\n        ");
	}	//end loop


	templateBuilder.Append("\r\n      </ul>\r\n    </div>\r\n  </div>\r\n  <!--/Red Photo-->\r\n  <!--Links-->\r\n  <div class=\"ilink_list\">\r\n    <h3><span class=\"graylink\"><a href=\"");
	templateBuilder.Append(linkurl("link",""));

	templateBuilder.Append("\">更多...</a></span>友情链接</h3>\r\n    <p class=\"txt\">\r\n      ");
	DataTable link_list1 = get_plugin_method("DTcms.Web.Plugin.Link", "link", "get_link_list", 0, "is_lock=0 and is_image=0 and is_red=1");
	

	int ldr1__loop__id=0;
	foreach(DataRow ldr1 in link_list1.Rows)
	{
		ldr1__loop__id++;


	templateBuilder.Append("\r\n      <a target=\"_blank\" href=\"" + Utils.ObjectToStr(ldr1["site_url"]) + "\">" + Utils.ObjectToStr(ldr1["title"]) + "</a> | \r\n      ");
	}	//end loop


	templateBuilder.Append("\r\n    </p>\r\n    <div class=\"img\">\r\n      <ul>\r\n        ");
	DataTable link_list2 = get_plugin_method("DTcms.Web.Plugin.Link", "link", "get_link_list", 9, "is_lock=0 and is_image=1 and is_red=1");
	

	int ldr2__loop__id=0;
	foreach(DataRow ldr2 in link_list2.Rows)
	{
		ldr2__loop__id++;


	templateBuilder.Append("\r\n        <li><a target=\"_blank\" href=\"" + Utils.ObjectToStr(ldr2["site_url"]) + "\" title=\"" + Utils.ObjectToStr(ldr2["title"]) + "\"><img src=\"" + Utils.ObjectToStr(ldr2["img_url"]) + "\" width=\"88\" height=\"31\" /></a></li>\r\n        ");
	}	//end loop


	templateBuilder.Append("\r\n      </ul>\r\n      <div class=\"clear\"></div>\r\n    </div>\r\n  </div>\r\n  <!--/Links-->\r\n</div>\r\n\r\n<!--Footer-->\r\n");

	templateBuilder.Append("<div class=\"footer\">\r\n  <div class=\"footer_inner\">\r\n    <div class=\"clear\"></div>\r\n    <div class=\"copyright\" style=\"text-align:center\">\r\n      <p>版权所有 重庆热图科技有限公司 渝ICP备11064298号 © Copyright 2012 - 2013. rehu.co  All Rights Reserved.\r\n      </p>\r\n    </div>\r\n  </div>\r\n</div>");


	templateBuilder.Append("\r\n<!--/Footer-->\r\n</body>\r\n</html>\r\n");
	Response.Write(templateBuilder.ToString());
}
</script>
