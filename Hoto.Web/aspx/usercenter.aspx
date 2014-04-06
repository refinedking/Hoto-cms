<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.UI.Page.usercenter" ValidateRequest="false" %>
<%@ Import namespace="System.Collections.Generic" %>
<%@ Import namespace="System.Text" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="DTcms.Common" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	base.OnInit(e);
	StringBuilder templateBuilder = new StringBuilder(220000);

	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>会员中心 - ");
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
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/calendar.js\"></");
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


	templateBuilder.Append("\r\n    <!--/Sidebar-->\r\n  </div>\r\n  \r\n  <div class=\"right757\">\r\n    ");
	if (action=="index")
	{

	templateBuilder.Append("\r\n    <!--会员中心-->\r\n    <h1 class=\"main_tit\">\r\n      <span><a href=\"");
	templateBuilder.Append(linkurl("usercenter","exit"));

	templateBuilder.Append("\">退出</a></span>\r\n      会员中心\r\n      <strong>Profile</strong>\r\n    </h1>\r\n    <!--会员中心-->\r\n    <div class=\"main_head\">\r\n      <div class=\"avatarbox\">\r\n        ");
	if (userModel.avatar!="")
	{

	templateBuilder.Append("\r\n        <img src=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.avatar));
	templateBuilder.Append("\" />\r\n        ");
	}
	else
	{

	templateBuilder.Append("\r\n        <img src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.templateskin));
	templateBuilder.Append("/images/user_avatar.png\" alt=\"求真像\" />\r\n        ");
	}	//end if


	templateBuilder.Append("\r\n        <span><a href=\"");
	templateBuilder.Append(linkurl("usercenter","avatar"));

	templateBuilder.Append("\">设置头像</a></span>\r\n      </div>\r\n      <div class=\"tips_box\">\r\n        <h3>尊敬的<span class=\"red\">");
	templateBuilder.Append(Utils.ObjectToStr(userModel.user_name));
	templateBuilder.Append("</span>，欢迎您！</h3>\r\n        <dl>\r\n          <dt>会员等级：</dt>\r\n          <dd>");
	templateBuilder.Append(Utils.ObjectToStr(groupModel.title));
	templateBuilder.Append("</dd>\r\n        </dl>\r\n        <dl>\r\n          <dt>个人成长值：</dt>\r\n          <dd>");
	templateBuilder.Append(Utils.ObjectToStr(userModel.exp));
	templateBuilder.Append("点</dd>\r\n        </dl>\r\n        \r\n        <dl>\r\n          <dt>本次登录IP：</dt>\r\n          <dd>");
	templateBuilder.Append(Utils.ObjectToStr(curr_login_ip));
	templateBuilder.Append("</dd>\r\n        </dl>\r\n        <dl>\r\n          <dt>上次登录IP：</dt>\r\n          <dd>");
	templateBuilder.Append(Utils.ObjectToStr(pre_login_ip));
	templateBuilder.Append("</dd>\r\n        </dl>\r\n        <dl>\r\n          <dt>注册时间：</dt>\r\n          <dd>");
	templateBuilder.Append(Utils.ObjectToStr(userModel.reg_time));
	templateBuilder.Append("</dd>\r\n        </dl>\r\n        <dl>\r\n          <dt>上次登录时间：</dt>\r\n          <dd>");
	templateBuilder.Append(Utils.ObjectToStr(pre_login_time));
	templateBuilder.Append("</dd>\r\n        </dl>\r\n      </div>\r\n    </div>\r\n    <div class=\"line20\"></div>\r\n    <div class=\"tips_box\">\r\n      <h2>账户统计</h2>\r\n      <dl>\r\n        <dt>账户余额：</dt>\r\n        <dd><b class=\"red\">￥");
	templateBuilder.Append(Utils.ObjectToStr(userModel.amount));
	templateBuilder.Append("</b> 元</dd>\r\n      </dl>\r\n      <dl>\r\n        <dt>账户积分：</dt>\r\n        <dd><b class=\"red\">");
	templateBuilder.Append(Utils.ObjectToStr(userModel.point));
	templateBuilder.Append("</b> 分</dd>\r\n      </dl>\r\n      <dl>\r\n        <dt>未完成订单：</dt>\r\n        <dd><b class=\"red\">");
	templateBuilder.Append(get_user_order_count("status<3 and user_id="+userModel.id).ToString());
	

	templateBuilder.Append("</b> 个</dd>\r\n      </dl>\r\n      <dl>\r\n        <dt>未读短信：</dt>\r\n        <dd><b class=\"red\">");
	templateBuilder.Append(get_user_message_count("type<3 and is_read=0 and accept_user_name='"+userModel.user_name+"'").ToString());
	

	templateBuilder.Append("</b>条</dd>\r\n      </dl>\r\n    </div>\r\n    <!--/会员中心-->\r\n    ");
	}
	else if (action=="password")
	{

	templateBuilder.Append("\r\n    <!--修改密码-->\r\n    <link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/validate.css\" />\r\n    <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery.form.js\"></");
	templateBuilder.Append("script>\r\n    <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery.validate.min.js\"></");
	templateBuilder.Append("script>\r\n    <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/messages_cn.js\"></");
	templateBuilder.Append("script>\r\n    <script type=\"text/javascript\">\r\n      $(function(){\r\n	    //初始化表单\r\n		AjaxInitForm('pwd_form', 'btnSubmit', 1);\r\n	  });\r\n    </");
	templateBuilder.Append("script>\r\n    <h1 class=\"main_tit\">\r\n      <span><a href=\"");
	templateBuilder.Append(linkurl("usercenter","index"));

	templateBuilder.Append("\">管理首页</a></span>\r\n      修改密码\r\n      <strong>Password</strong>\r\n    </h1>\r\n    <form name=\"pwd_form\" id=\"pwd_form\" url=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=user_password_edit\">\r\n    <div class=\"form_box\">\r\n      <dl>\r\n        <dt>用户名：</dt>\r\n        <dd>");
	templateBuilder.Append(Utils.ObjectToStr(userModel.user_name));
	templateBuilder.Append("</dd>\r\n      </dl>\r\n      <dl>\r\n        <dt>旧密码：</dt>\r\n        <dd><input name=\"txtOldPassword\" id=\"txtOldPassword\" type=\"password\" class=\"input txt required\" minlength=\"6\" maxlength=\"50\" /></dd>\r\n      </dl>\r\n      <dl>\r\n        <dt>新密码：</dt>\r\n        <dd><input name=\"txtPassword\" id=\"txtPassword\" type=\"password\" class=\"input txt required\" minlength=\"6\" maxlength=\"50\" /></dd>\r\n      </dl>\r\n      <dl>\r\n        <dt>确认新密码：</dt>\r\n        <dd><input name=\"txtPassword1\" id=\"txtPassword1\" type=\"password\" class=\"input txt required\" equalTo=\"#txtPassword\" minlength=\"6\" maxlength=\"20\" /></dd>\r\n      </dl>\r\n      <dl>\r\n        <dt></dt>\r\n        <dd><input name=\"btnSubmit\" id=\"btnSubmit\" type=\"submit\" class=\"btn_submit\" value=\"确认修改\" /></dd>\r\n      </dl>\r\n    </div>\r\n    </form>\r\n    <!--/修改密码-->\r\n    ");
	}
	else if (action=="proinfo")
	{

	templateBuilder.Append("\r\n    <!--修改资料-->\r\n    <link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/validate.css\" />\r\n    <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery.form.js\"></");
	templateBuilder.Append("script>\r\n    <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery.validate.min.js\"></");
	templateBuilder.Append("script>\r\n    <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/messages_cn.js\"></");
	templateBuilder.Append("script>\r\n    <script type=\"text/javascript\">\r\n      $(function(){\r\n	    //初始化表单\r\n		AjaxInitForm('info_form', 'btnSubmit', 1);\r\n	  });\r\n    </");
	templateBuilder.Append("script>\r\n    <form name=\"info_form\" id=\"info_form\" url=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=user_info_edit\">\r\n    <h1 class=\"main_tit\">\r\n      <span><a href=\"");
	templateBuilder.Append(linkurl("usercenter","index"));

	templateBuilder.Append("\">管理首页</a></span>\r\n      个人资料\r\n      <strong>Information</strong>\r\n    </h1>\r\n    \r\n    <div class=\"form_box\">\r\n      <dl>\r\n        <dt>用户名：</dt>\r\n        <dd>");
	templateBuilder.Append(Utils.ObjectToStr(userModel.user_name));
	templateBuilder.Append("</dd>\r\n      </dl>\r\n      <dl>\r\n        <dt>Email：</dt>\r\n        <dd><input name=\"txtEmail\" id=\"txtEmail\" type=\"text\" class=\"input txt required email\" maxlength=\"50\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.email));
	templateBuilder.Append("\" readonly=\"readonly\" /></dd>\r\n      </dl>\r\n      <dl>\r\n        <dt>昵称：</dt>\r\n        <dd><input name=\"txtNickName\" id=\"txtNickName\" type=\"text\" class=\"input txt required\" maxlength=\"50\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.nick_name));
	templateBuilder.Append("\" /></dd>\r\n      </dl>\r\n      <dl>\r\n        <dt>性别：</dt>\r\n        <dd>\r\n          ");
	if (userModel.sex=="男")
	{

	templateBuilder.Append("\r\n            <label><input name=\"rblSex\" type=\"radio\" value=\"男\" checked=\"checked\" />男</label> \r\n            <label><input name=\"rblSex\" type=\"radio\" value=\"女\" />女</label>\r\n            <label><input name=\"rblSex\" type=\"radio\" value=\"保密\" />保密</label>\r\n            ");
	}
	else if (userModel.sex=="女")
	{

	templateBuilder.Append("\r\n            <label><input name=\"rblSex\" type=\"radio\" value=\"男\" />男</label>\r\n            <label><input name=\"rblSex\" type=\"radio\" value=\"女\" checked=\"checked\" />女</label>\r\n            <label><input name=\"rblSex\" type=\"radio\" value=\"保密\" />保密</label>\r\n            ");
	}
	else
	{

	templateBuilder.Append("\r\n            <label><input name=\"rblSex\" type=\"radio\" value=\"男\" />男</label>\r\n            <label><input name=\"rblSex\" type=\"radio\" value=\"女\" />女</label>\r\n            <label><input name=\"rblSex\" type=\"radio\" value=\"保密\" checked=\"checked\" />保密</label>\r\n            ");
	}	//end if


	templateBuilder.Append("\r\n        </dd>\r\n      </dl>\r\n      <dl>\r\n        <dt>生日：</dt>\r\n        <dd>\r\n          ");
	if (userModel.birthday==null)
	{

	templateBuilder.Append("\r\n          <input name=\"txtBirthday\" id=\"txtBirthday\" type=\"text\" class=\"input txt required dateISO\" maxlength=\"30\" onclick=\"return Calendar('txtBirthday');\" />\r\n          ");
	}
	else
	{

	templateBuilder.Append("\r\n          <input name=\"txtBirthday\" id=\"txtBirthday\" type=\"text\" class=\"input txt required dateISO\" maxlength=\"30\" onclick=\"return Calendar('txtBirthday');\" value=\"");	templateBuilder.Append(Utils.ObjectToDateTime(userModel.birthday).ToString("yyyy-M-d"));

	templateBuilder.Append("\" />\r\n          ");
	}	//end if


	templateBuilder.Append("\r\n        </dd>\r\n      </dl>\r\n      <dl>\r\n        <dt>联系电话：</dt>\r\n        <dd><input name=\"txtTelphone\" id=\"txtTelphone\" type=\"text\" class=\"input txt\" maxlength=\"50\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.telphone));
	templateBuilder.Append("\" /></dd>\r\n      </dl>\r\n      <dl>\r\n        <dt>手机号码：</dt>\r\n        <dd><input name=\"txtMobile\" id=\"txtMobile\" type=\"text\" class=\"input txt\" maxlength=\"20\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.mobile));
	templateBuilder.Append("\" /></dd>\r\n      </dl>\r\n      <dl>\r\n        <dt>在线QQ：</dt>\r\n        <dd><input name=\"txtQQ\" id=\"txtQQ\" type=\"text\" class=\"input txt\" maxlength=\"20\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.qq));
	templateBuilder.Append("\" /></dd>\r\n      </dl>\r\n      <dl>\r\n        <dt>联系地址：</dt>\r\n        <dd><input name=\"txtAddress\" id=\"txtAddress\" type=\"text\" class=\"input wide\" maxlength=\"250\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.address));
	templateBuilder.Append("\" /></dd>\r\n      </dl>\r\n      <dl>\r\n        <dt></dt>\r\n        <dd><input name=\"btnSubmit\" id=\"btnSubmit\" type=\"submit\" class=\"btn_submit\" value=\"确认修改\" /></dd>\r\n      </dl>\r\n    </div>\r\n    </form>\r\n    <!--/修改资料-->\r\n    ");
	}
	else if (action=="avatar")
	{

	templateBuilder.Append("\r\n    <!--设置头像-->\r\n    <link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/jquery.Jcrop.css\" type=\"text/css\" />\r\n    <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery.form.js\"></");
	templateBuilder.Append("script>\r\n    <script src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery.Jcrop.min.js\" type=\"text/javascript\"></");
	templateBuilder.Append("script>\r\n    <script src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.templateskin));
	templateBuilder.Append("/js/avatar.js\" type=\"text/javascript\"></");
	templateBuilder.Append("script>\r\n    <h1 class=\"main_tit\">\r\n      <span><a href=\"");
	templateBuilder.Append(linkurl("usercenter","index"));

	templateBuilder.Append("\">管理首页</a></span>\r\n      头像设置\r\n      <strong>Avatar</strong>\r\n    </h1>\r\n    \r\n    <strong>当前我的头像</strong>\r\n    <p>如果您还没有设置自己的头像，系统会显示为默认头像，您需要自己上传一张新照片来作为自己的个人头像。</p>\r\n    <div class=\"img_box\">\r\n      ");
	if (userModel.avatar!="")
	{

	templateBuilder.Append("\r\n        <img src=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.avatar));
	templateBuilder.Append("\" width=\"180\" height=\"180\" />\r\n      ");
	}
	else
	{

	templateBuilder.Append("\r\n        <img src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.templateskin));
	templateBuilder.Append("/images/user_avatar.png\" width=\"180\" height=\"180\" />\r\n      ");
	}	//end if


	templateBuilder.Append("\r\n    </div>\r\n    <strong>设置我的新头像</strong>\r\n    <p>上传成功后，请裁剪合适的头像确认保存后才能生效。</p>\r\n    <form id=\"upload_form\" name=\"upload_form\">\r\n      <div class=\"avatar_upload\">\r\n        <a href=\"javascript:;\" class=\"files\"><input type=\"file\" id=\"FileUpload\" name=\"FileUpload\" onchange=\"Upload('");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("');\" /></a>\r\n        <span class=\"uploading\">正在上传，请稍候...</span>\r\n      </div>\r\n      <div class=\"clear\"></div>\r\n      <div class=\"avatar_box\">\r\n        <div class=\"avatar_big_warp\">\r\n          <div class=\"avatar_big_box\">\r\n            <div class=\"avatar_big_pic\">\r\n              <img id=\"target\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.templateskin));
	templateBuilder.Append("/images/pic_bg.png\" />\r\n            </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"avatar_small_warp\">\r\n          <div class=\"avatar_small_box\">\r\n            <div class=\"avatar_small_pic\"><img id=\"preview\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.templateskin));
	templateBuilder.Append("/images/pic_bg.png\" /></div>\r\n          </div>\r\n          <p style=\"text-align:center;\"><strong>头像预览区</strong></p>\r\n          <p style=\"text-align:center;\"><input id=\"btnSubmit\" name=\"btnSubmit\" type=\"button\" class=\"btn btn-success\" value=\"确定保存\" onclick=\"CropSubmit('");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("');return false;\" /></p>\r\n          <p>提示：生成头像大小180*180相素上传图片后，左侧选取图片合适大小，点击下面的保存按钮。</p>\r\n        </div>\r\n      </div>\r\n      <input id=\"hideFileName\" name=\"hideFileName\" type=\"hidden\" />\r\n      <input id=\"hideX1\" name=\"hideX1\" type=\"hidden\" value=\"0\" />\r\n      <input id=\"hideY1\" name=\"hideY1\" type=\"hidden\" value=\"0\" />\r\n      <input id=\"hideWidth\" name=\"hideWidth\" type=\"hidden\" value=\"0\" />\r\n      <input id=\"hideHeight\" name=\"hideHeight\" type=\"hidden\" value=\"0\" />\r\n    </form>\r\n    <!--/设置头像-->\r\n    ");
	}
	else if (action=="invite")
	{

	templateBuilder.Append("\r\n    <!--邀请码-->\r\n    <h1 class=\"main_tit\">\r\n      <span><a href=\"javascript:;\" onclick=\"clickSubmit('");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=user_invite_code');\">申请邀请码</a></span>\r\n      我的邀请码\r\n      <strong>Invite</strong>\r\n    </h1>\r\n    \r\n    <p>说明：您购买的邀请码会在失效之后由系统定时清理，不会长期驻留在列表中</p>\r\n    <div class=\"line10\"></div>\r\n    <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"ftable\">\r\n      <tr>\r\n        <th align=\"left\">邀请码</th>\r\n        <th width=\"150\">申请时间</th>\r\n        <th width=\"150\">过期时间</th>\r\n        <th width=\"80\">已使用次数</th>\r\n        <th width=\"80\">状态</th>\r\n      </tr>\r\n      ");
	DataTable list1 = get_user_invite_list(0, "user_name='"+userModel.user_name+"'");
	

	templateBuilder.Append(" <!--取得一个DataTable-->\r\n      ");
	int dr__loop__id=0;
	foreach(DataRow dr in list1.Rows)
	{
		dr__loop__id++;


	templateBuilder.Append("\r\n      <tr>\r\n        <td>" + Utils.ObjectToStr(dr["str_code"]) + " &nbsp; <a href=\"javascript:;\" onclick=\"copyText('邀请码：" + Utils.ObjectToStr(dr["str_code"]) + "');\">[复制]</a></td>\r\n        <td align=\"center\">" + Utils.ObjectToStr(dr["add_time"]) + "</td>\r\n        <td align=\"center\">\r\n          ");
	if (Utils.ObjectToStr(dr["eff_time"])=="")
	{

	templateBuilder.Append("\r\n          无限制\r\n          ");
	}
	else
	{

	templateBuilder.Append("\r\n          " + Utils.ObjectToStr(dr["eff_time"]) + "\r\n          ");
	}	//end if


	templateBuilder.Append("\r\n        </td>\r\n        <td align=\"center\">" + Utils.ObjectToStr(dr["count"]) + "</td>\r\n        <td align=\"center\">\r\n          ");
	if (get_invite_status(Utils.ObjectToStr(dr["str_code"])))
	{

	templateBuilder.Append("\r\n          有效\r\n          ");
	}
	else
	{

	templateBuilder.Append("\r\n          已失效\r\n          ");
	}	//end if


	templateBuilder.Append("\r\n        </td>\r\n      </tr>\r\n      ");
	}	//end loop


	if (list1.Rows.Count<1)
	{

	templateBuilder.Append("\r\n      <tr><td align=\"center\" colspan=\"5\">暂无记录</td></tr>\r\n      ");
	}	//end if


	templateBuilder.Append("\r\n    </table>\r\n    <!--/邀请码-->\r\n    ");
	}	//end if


	templateBuilder.Append("\r\n    \r\n  </div>\r\n  <div class=\"clear\"></div>\r\n</div>\r\n\r\n<div class=\"clear\"></div>\r\n\r\n<!--Footer-->\r\n");

	templateBuilder.Append("<div class=\"footer\">\r\n  <div class=\"footer_inner\">\r\n    <div class=\"clear\"></div>\r\n    <div class=\"copyright\" style=\"text-align:center\">\r\n      <p>版权所有 重庆热图科技有限公司 渝ICP备11064298号 © Copyright 2012 - 2013. rehu.co  All Rights Reserved.\r\n      </p>\r\n    </div>\r\n  </div>\r\n</div>");


	templateBuilder.Append("\r\n<!--/Footer-->\r\n</body>\r\n</html>\r\n");
	Response.Write(templateBuilder.ToString());
}
</script>
