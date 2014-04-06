<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.UI.Page.repassword" ValidateRequest="false" %>
<%@ Import namespace="System.Collections.Generic" %>
<%@ Import namespace="System.Text" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="DTcms.Common" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	base.OnInit(e);
	StringBuilder templateBuilder = new StringBuilder(220000);

	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>找回密码 - ");
	templateBuilder.Append(Utils.ObjectToStr(config.webname));
	templateBuilder.Append("</title>\r\n<meta content=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webkeyword));
	templateBuilder.Append("\" name=\"keywords\">\r\n<meta content=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webdescription));
	templateBuilder.Append("\" name=\"description\">\r\n<link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/validate.css\" />\r\n<link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/ui/skins/Aqua/css/ligerui-all.css\" />\r\n<link media=\"screen\" type=\"text/css\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.templateskin));
	templateBuilder.Append("/css/style.css\" rel=\"stylesheet\">\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery-1.3.2.min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery.form.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery.validate.min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/messages_cn.js\"></");
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


	templateBuilder.Append("\r\n<!--/Header-->\r\n\r\n<div class=\"boxwrap\">\r\n  <div class=\"cart_box\">\r\n    <h1 class=\"main_tit\">\r\n      找回密码<strong>Reset Password</strong>\r\n    </h1>\r\n    ");
	if (action=="confirm")
	{

	templateBuilder.Append("\r\n    <!--填写问题-->\r\n    <script type=\"text/javascript\">\r\n      $(function(){\r\n	    //初始化表单\r\n		AjaxInitForm('pwd_form', 'btnSubmit', 1);\r\n	  });\r\n    </");
	templateBuilder.Append("script>\r\n    <form id=\"pwd_form\" name=\"pwd_form\" url=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=user_getpassword\">\r\n    <div class=\"form_box\">\r\n      <dl>\r\n        <dt>用户名：</dt>\r\n        <dd><input type=\"text\" name=\"txtUserName\" id=\"txtUserName\" class=\"input txt required\" /></dd>\r\n      </dl>\r\n      <dl>\r\n        <dt>验证码：</dt>\r\n        <dd>\r\n          <input id=\"txtCode\" name=\"txtCode\" class=\"input small required\" type=\"text\" maxlength=\"50\" style=\"ime-mode:disabled;text-transform:uppercase;\">\r\n          <a id=\"verifyCode\" href=\"javascript:;\" onclick=\"ToggleCode(this, '");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/verify_code.ashx');return false;\"><img src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/verify_code.ashx\" width=\"80\" height=\"22\" align=\"absmiddle\" /> 看不清楚？</a>\r\n        </dd>\r\n      </dl>\r\n      <dl>\r\n        <dt></dt>\r\n        <dd><input id=\"btnSubmit\" name=\"btnSubmit\" type=\"submit\" class=\"btn_submit\" value=\"确认提交\" /></dd>\r\n      </dl>\r\n    </div>\r\n    </form>\r\n    <!--/填写问题-->\r\n    \r\n    ");
	}
	else if (action=="reset")
	{

	templateBuilder.Append("\r\n    <!--重设密码-->\r\n    <script type=\"text/javascript\">\r\n      $(function(){\r\n	    //初始化表单\r\n		AjaxInitForm('pwd_form', 'btnSubmit', 0, 'turl');\r\n	  });\r\n    </");
	templateBuilder.Append("script>\r\n    <form id=\"pwd_form\" name=\"pwd_form\" url=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=user_repassword\">\r\n    <div class=\"form_box\">\r\n      <dl>\r\n        <dt>用户名：</dt>\r\n        <dd>");
	templateBuilder.Append(Utils.ObjectToStr(model.user_name));
	templateBuilder.Append("<input type=\"hidden\" id=\"hideCode\" name=\"hideCode\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(model.str_code));
	templateBuilder.Append("\" /></dd>\r\n      </dl>\r\n      <dl>\r\n        <dt>重设新密码：</dt>\r\n        <dd><input type=\"password\" name=\"txtPassword\" id=\"txtPassword\" class=\"input txt required\" minlength=\"6\" maxlength=\"20\" /></dd>\r\n      </dl>\r\n      <dl>\r\n        <dt>确认新密码：</dt>\r\n        <dd><input type=\"password\" name=\"txtPassword1\" id=\"txtPassword1\" class=\"input txt required\" equalTo=\"#txtPassword\" minlength=\"6\" maxlength=\"20\" /></dd>\r\n      </dl>\r\n      <dl>\r\n        <dt>验证码：</dt>\r\n        <dd>\r\n          <input id=\"txtCode\" name=\"txtCode\" class=\"input small required\" type=\"text\" maxlength=\"50\" style=\"ime-mode:disabled;text-transform:uppercase;\">\r\n          <a id=\"verifyCode\" href=\"javascript:;\" onclick=\"ToggleCode(this, '");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/verify_code.ashx');return false;\"><img src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/verify_code.ashx\" width=\"80\" height=\"22\" align=\"absmiddle\" /> 看不清楚？</a>\r\n        </dd>\r\n      </dl>\r\n      <dl>\r\n        <dt></dt>\r\n        <dd><input id=\"btnSubmit\" name=\"btnSubmit\" type=\"submit\" class=\"btn_submit\" value=\"确认提交\" /></dd>\r\n      </dl>\r\n    </div>\r\n    <input id=\"turl\" type=\"hidden\" value=\"");
	templateBuilder.Append(linkurl("repassword","succeed"));

	templateBuilder.Append("\" />\r\n    </form>\r\n    \r\n    <!--/重设密码-->\r\n    ");
	}
	else if (action=="succeed")
	{

	templateBuilder.Append("\r\n    <!--成功提示-->\r\n    <div class=\"msg_tips\">\r\n      <div class=\"ico\"></div>\r\n      <div class=\"msg\">\r\n        <strong>登录密码重设成功啦！</strong>\r\n        <p>恭喜您，您的密码重置成功！</p>\r\n        <p>请记住新密码，您可以点击这里进入<a href=\"");
	templateBuilder.Append(linkurl("login"));

	templateBuilder.Append("\">登录</a>会员中心。</p>\r\n        <p>如有其它问题，请立即与我们客服人员联系。</p>\r\n      </div>\r\n    </div>\r\n    <!--成功提示-->\r\n    ");
	}
	else if (action=="error")
	{

	templateBuilder.Append("\r\n    <!--验证出错-->\r\n    <div class=\"msg_tips\">\r\n      <div class=\"ico error\"></div>\r\n      <div class=\"msg\">\r\n        <strong>验证失败，验证字符串已被使用或已过期。</strong>\r\n        <p>1、请确认邮件验证字符串的URL是否正确</p>\r\n        <p>2、每次申请的验证字符串只能使用一次。</p>\r\n        <p>3、每次申请的有效时间为24小时，过期请重新提交。</p>\r\n      </div>\r\n    </div>\r\n    <!--验证出错-->\r\n    ");
	}	//end if


	templateBuilder.Append("\r\n    <div class=\"clear\"></div>\r\n  </div>\r\n</div>\r\n\r\n<div class=\"clear\"></div>\r\n\r\n<!--Footer-->\r\n");

	templateBuilder.Append("<div class=\"footer\">\r\n  <div class=\"footer_inner\">\r\n    <div class=\"clear\"></div>\r\n    <div class=\"copyright\" style=\"text-align:center\">\r\n      <p>版权所有 重庆热图科技有限公司 渝ICP备11064298号 © Copyright 2012 - 2013. rehu.co  All Rights Reserved.\r\n      </p>\r\n    </div>\r\n  </div>\r\n</div>");


	templateBuilder.Append("\r\n<!--/Footer-->\r\n</body>\r\n</html>\r\n");
	Response.Write(templateBuilder.ToString());
}
</script>
