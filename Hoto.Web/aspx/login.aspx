<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.UI.Page.login" ValidateRequest="false" %>
<%@ Import namespace="System.Collections.Generic" %>
<%@ Import namespace="System.Text" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="DTcms.Common" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	base.OnInit(e);
	StringBuilder templateBuilder = new StringBuilder(220000);

	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>会员登录 - ");
	templateBuilder.Append(Utils.ObjectToStr(config.webname));
	templateBuilder.Append("</title>\r\n<meta content=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webkeyword));
	templateBuilder.Append("\" name=\"keywords\">\r\n<meta content=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webdescription));
	templateBuilder.Append("\" name=\"description\">\r\n<link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/ui/skins/Aqua/css/ligerui-all.css\" />\r\n<link media=\"screen\" type=\"text/css\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.templateskin));
	templateBuilder.Append("/css/style.css\" rel=\"stylesheet\">\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery-1.3.2.min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/ui/js/ligerBuild.min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.templateskin));
	templateBuilder.Append("/js/login_validate.js\"></");
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


	templateBuilder.Append("\r\n<!--/Header-->\r\n\r\n<div class=\"boxwrap\">\r\n  <div class=\"main_box\">\r\n    <h1 class=\"main_tit\">\r\n      会员登录<strong>Login</strong>\r\n    </h1>\r\n\r\n    <div class=\"login-box\">\r\n    <div class=\"left-box\">\r\n      <form id=\"loginform\" name=\"loginform\" url=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=user_login\">\r\n      <dl>\r\n        <dt>用户名：</dt>\r\n        <dd><input id=\"txtUserName\" name=\"txtUserName\" class=\"input txt\" type=\"text\" maxlength=\"50\" /></dd>\r\n      </dl>\r\n      <dl>\r\n        <dt>密　码：</dt>\r\n        <dd><input id=\"txtPassword\" name=\"txtPassword\" class=\"input txt required\" type=\"password\" maxlength=\"100\" /></dd>\r\n      </dl>\r\n      <dl>\r\n        <dt></dt>\r\n        <dd>\r\n          <input id=\"chkRemember\" name=\"chkRemember\" class=\"checkbox\" type=\"checkbox\" style=\"vertical-align:middle\">\r\n          <label class=\"red\" for=\"chkRemember\">一周内自动登录</label>\r\n        </dd>\r\n      </dl>\r\n      <dl>\r\n        <dt></dt>\r\n        <dd>\r\n          <input id=\"btnSubmit\" name=\"btnSubmit\" type=\"submit\" class=\"btn_login\" value=\"登 录\" />\r\n          <a href=\"");
	templateBuilder.Append(linkurl("repassword","confirm"));

	templateBuilder.Append("\">忘记密码？</a>\r\n        </dd>\r\n      </dl>\r\n      <dl id=\"msgtips\">\r\n        <dt></dt>\r\n        <dd></dd>\r\n      </dl>\r\n      <input id=\"turl\" name=\"turl\" type=\"hidden\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(turl));
	templateBuilder.Append("\" />\r\n      </form>\r\n      <!--第三方登录-->\r\n      <div class=\"oauth_inner\">\r\n        <label>其它登录方式</label>\r\n        <div class=\"oauth_list\">\r\n          ");
	DataTable oauth_list = get_app_oauth_list(0, "");
	

	templateBuilder.Append(" <!--取得一个DataTable-->\r\n          ");
	int dr__loop__id=0;
	foreach(DataRow dr in oauth_list.Rows)
	{
		dr__loop__id++;


	templateBuilder.Append("\r\n          <a title=\"" + Utils.ObjectToStr(dr["title"]) + "\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("api/oauth/" + Utils.ObjectToStr(dr["api_path"]) + "/index.aspx\"><img src=\"" + Utils.ObjectToStr(dr["img_url"]) + "\" /></a>\r\n          ");
	}	//end loop


	templateBuilder.Append("\r\n        </div>\r\n      </div>\r\n      <!--/第三方登录-->\r\n    </div>\r\n    <div class=\"center-box\"></div>\r\n    <div class=\"right-box\">\r\n      <p class=\"note\">我们诚挚地邀请您加入<br />DTcms—100%开源、永久免费</p>\r\n      <p>如果您还没有会员账户</p>\r\n      <p class=\"button\">\r\n        <a href=\"");
	templateBuilder.Append(linkurl("register"));

	templateBuilder.Append("\">立即注册</a>\r\n      </p>\r\n    </div>\r\n    <div class=\"clear\"></div>\r\n  </div>\r\n\r\n    <div class=\"clear\"></div>\r\n  </div>\r\n</div>\r\n\r\n<div class=\"clear\"></div>\r\n\r\n<!--Footer-->\r\n");

	templateBuilder.Append("<div class=\"footer\">\r\n  <div class=\"footer_inner\">\r\n    <div class=\"clear\"></div>\r\n    <div class=\"copyright\" style=\"text-align:center\">\r\n      <p>版权所有 重庆热图科技有限公司 渝ICP备11064298号 © Copyright 2012 - 2013. rehu.co  All Rights Reserved.\r\n      </p>\r\n    </div>\r\n  </div>\r\n</div>");


	templateBuilder.Append("\r\n<!--/Footer-->\r\n</body>\r\n</html>\r\n");
	Response.Write(templateBuilder.ToString());
}
</script>
