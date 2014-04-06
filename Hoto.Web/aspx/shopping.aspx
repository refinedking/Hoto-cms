<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.UI.Page.shopping" ValidateRequest="false" %>
<%@ Import namespace="System.Collections.Generic" %>
<%@ Import namespace="System.Text" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="DTcms.Common" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	base.OnInit(e);
	StringBuilder templateBuilder = new StringBuilder(220000);

	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>购物中心－");
	templateBuilder.Append(Utils.ObjectToStr(config.webname));
	templateBuilder.Append("</title>\r\n<link rel=\"stylesheet\" href=\"");
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
	templateBuilder.Append("/js/base.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.templateskin));
	templateBuilder.Append("/js/cart.js\"></");
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


	templateBuilder.Append("\r\n<!--/Header-->\r\n\r\n<div class=\"boxwrap\">\r\n  <div class=\"cart_box\">\r\n    ");
	if (action=="cart")
	{

	templateBuilder.Append("\r\n    <!--购物车-->\r\n    <h1 class=\"main_tit\">\r\n      <span><a onclick=\"DeleteCart(this,'");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("','0');\" href=\"javascript:;\">清空购物车</a></span>\r\n      我的购物车<strong>Shopping Cart</strong>\r\n    </h1>\r\n    <div class=\"cart_step\">\r\n      <ul>\r\n        <li class=\"selected\"><span>1</span>放进购物车</li>\r\n        <li><span>2</span>填写订单信息</li>\r\n        <li class=\"last\"><span>3</span>支付/确定订单</li>\r\n      </ul>\r\n    </div>\r\n    <div class=\"line20\"></div>\r\n    \r\n    <table width=\"938\" border=\"0\" align=\"center\" cellpadding=\"8\" cellspacing=\"0\" class=\"cart_table\">\r\n      <tr>\r\n        <th width=\"64\"></th>\r\n        <th align=\"left\">商品名称</th>\r\n        <th width=\"80\" align=\"center\">积分</th>\r\n        <th width=\"80\" align=\"center\">单价</th>\r\n        <th width=\"80\" align=\"center\">数量</th>\r\n        <th width=\"80\" align=\"center\">优惠</th>\r\n        <th width=\"100\" align=\"center\">金额小计</th>\r\n        <th width=\"100\" align=\"center\">积分小计</th>\r\n        <th width=\"50\" align=\"center\">操作</th>\r\n      </tr>\r\n      ");
	IList<DTcms.Model.cart_items> ls1 = get_cart_list();
	

	int modelt__loop__id=0;
	foreach(DTcms.Model.cart_items modelt in ls1)
	{
		modelt__loop__id++;


	templateBuilder.Append("\r\n      <tr>\r\n        <td><a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("goods_show",modelt.id));

	templateBuilder.Append("\"><img src=\"");
	templateBuilder.Append(Utils.ObjectToStr(modelt.img_url));
	templateBuilder.Append("\" class=\"img\" /></a></td>\r\n        <td><a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("goods_show",modelt.id));

	templateBuilder.Append("\">");
	templateBuilder.Append(Utils.ObjectToStr(modelt.title));
	templateBuilder.Append("</a></td>\r\n        <td align=\"center\">\r\n          ");
	if (modelt.point>0)
	{

	templateBuilder.Append("\r\n          +\r\n          ");
	}	//end if


	templateBuilder.Append("\r\n          ");
	templateBuilder.Append(Utils.ObjectToStr(modelt.point));
	templateBuilder.Append("\r\n        </td>\r\n        <td align=\"center\">￥");
	templateBuilder.Append(Utils.ObjectToStr(modelt.price));
	templateBuilder.Append("<input name=\"goods_price\" type=\"hidden\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(modelt.user_price));
	templateBuilder.Append("\" /></td>\r\n        <td align=\"center\">\r\n          <a href=\"javascript:;\" class=\"reduce\" title=\"减一\" onclick=\"CartComputNum(this, '");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("', '");
	templateBuilder.Append(Utils.ObjectToStr(modelt.id));
	templateBuilder.Append("', -1);\">减一</a>\r\n          <input type=\"text\" name=\"goods_quantity\" class=\"input\" style=\"width:30px;text-align:center;ime-mode:Disabled;\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(modelt.quantity));
	templateBuilder.Append("\" onblur=\"CartAmountTotal(this, '");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("', '");
	templateBuilder.Append(Utils.ObjectToStr(modelt.id));
	templateBuilder.Append("');\" onkeypress=\"return (/[\\d]/.test(String.fromCharCode(event.keyCode)))\" />\r\n          <a href=\"javascript:;\" class=\"subjoin\" title=\"加一\" onclick=\"CartComputNum(this,'");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("', '");
	templateBuilder.Append(Utils.ObjectToStr(modelt.id));
	templateBuilder.Append("', 1);\">加一</a>\r\n        </td>\r\n        <td align=\"center\">￥<label name=\"discount_amount\">");
	templateBuilder.Append(((modelt.price-modelt.user_price)*modelt.quantity).ToString());
	

	templateBuilder.Append("</label></td>\r\n        <td align=\"center\"><font color=\"#FF0000\" size=\"2\">￥<label name=\"real_amount\">");
	templateBuilder.Append((modelt.user_price*modelt.quantity).ToString());
	

	templateBuilder.Append("</label></font></td>\r\n        <td align=\"center\">\r\n          <font color=\"#FF0000\" size=\"2\">\r\n            ");
	if (modelt.point>0)
	{

	templateBuilder.Append("\r\n            +\r\n            ");
	}	//end if


	templateBuilder.Append("\r\n            <label name=\"point_count\">");
	templateBuilder.Append((modelt.point*modelt.quantity).ToString());
	

	templateBuilder.Append("</label>\r\n          </font>\r\n        </td>\r\n        <td align=\"center\"><a onclick=\"DeleteCart(this,'");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("','");
	templateBuilder.Append(Utils.ObjectToStr(modelt.id));
	templateBuilder.Append("');\" href=\"javascript:;\">删除</a></td>\r\n      </tr>\r\n      ");
	}	//end loop


	if (ls1.Count<1)
	{

	templateBuilder.Append("\r\n      <tr>\r\n        <td colspan=\"9\">\r\n          <div class=\"msg_tips\">\r\n            <div class=\"ico warning\"></div>\r\n            <div class=\"msg\">\r\n              <strong>购物车没有商品！</strong>\r\n              <p>您的购物车为空，<a href=\"");
	templateBuilder.Append(linkurl("index"));

	templateBuilder.Append("\">马上去购物</a>吧！</p>\r\n            </div>\r\n          </div>\r\n        </td>\r\n      </tr>\r\n      ");
	}	//end if


	templateBuilder.Append("\r\n      <tr>\r\n        <th colspan=\"9\" align=\"right\">\r\n          商品件数：");
	templateBuilder.Append(Utils.ObjectToStr(cartModel.total_quantity));
	templateBuilder.Append(" 件 &nbsp;&nbsp; 商品总金额（不含运费）：<font color=\"#FF0000\" size=\"2\">￥");
	templateBuilder.Append(Utils.ObjectToStr(cartModel.real_amount));
	templateBuilder.Append("</font>元 &nbsp;&nbsp; 总积分：<font color=\"#FF0000\" size=\"2\">");
	templateBuilder.Append(Utils.ObjectToStr(cartModel.total_point));
	templateBuilder.Append("分</font>\r\n        </th>\r\n      </tr>\r\n      \r\n	</table>\r\n    <div class=\"line20\"></div>\r\n    <div class=\"right\">\r\n      <a class=\"btn\" href=\"");
	templateBuilder.Append(linkurl("index"));

	templateBuilder.Append("\">继续购物</a>\r\n      ");
	if (cartModel.total_quantity==0)
	{

	templateBuilder.Append("\r\n      <a class=\"btn btn-gray\">不能结算</a>\r\n      ");
	}
	else
	{

	templateBuilder.Append("\r\n      <a class=\"btn btn-success marL10\" href=\"");
	templateBuilder.Append(linkurl("shopping","confirm"));

	templateBuilder.Append("\">马上去结算</a>\r\n      ");
	}	//end if


	templateBuilder.Append("\r\n    </div>\r\n    <div class=\"clear\"></div>\r\n    <!--/购物车-->\r\n    \r\n    ");
	}
	else if (action=="confirm")
	{

	templateBuilder.Append("\r\n    <link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/validate.css\" />\r\n	<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery.form.js\"></");
	templateBuilder.Append("script>\r\n    <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery.validate.min.js\"></");
	templateBuilder.Append("script>\r\n    <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/messages_cn.js\"></");
	templateBuilder.Append("script>\r\n    <script type=\"text/javascript\">\r\n      $(function () {\r\n          //表单提交\r\n          AjaxInitForm('order_form', 'btnSubmit', 0);\r\n      });\r\n    </");
	templateBuilder.Append("script>\r\n    <!--结算中心-->\r\n    <h1 class=\"main_tit\">\r\n      <span><a href=\"");
	templateBuilder.Append(linkurl("shopping","cart"));

	templateBuilder.Append("\">返回</a></span>\r\n      确认订单信息<strong>Confirm order</strong>\r\n    </h1>\r\n    <div class=\"cart_step\">\r\n      <ul>\r\n        <li class=\"item\"><span>1</span>放进购物车</li>\r\n        <li class=\"selected\"><span>2</span>填写订单信息</li>\r\n        <li class=\"last\"><span>3</span>支付/确定订单</li>\r\n      </ul>\r\n    </div>\r\n    <div class=\"line20\"></div>\r\n    \r\n    <form name=\"order_form\" id=\"order_form\" url=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=order_save\">\r\n    <h3 class=\"bar_tit\">1、收货信息</h3>\r\n    <div class=\"form_box\">\r\n      <dl>\r\n        <dt>收货人姓名：</dt>\r\n        <dd><input name=\"accept_name\" id=\"accept_name\" type=\"text\" class=\"input txt wide required\" maxlength=\"50\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.nick_name));
	templateBuilder.Append("\" /><em>*收货人姓名</em></dd>\r\n      </dl>\r\n      <dl>\r\n        <dt>收货地址：</dt>\r\n        <dd><input name=\"address\" id=\"address\" type=\"text\" class=\"input txt required\" maxlength=\"100\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.address));
	templateBuilder.Append("\" style=\"width:300px;\" /><em>*收货人的详细地址</em></dd>\r\n      </dl>\r\n      <dl>\r\n        <dt>邮政编码：</dt>\r\n        <dd><input name=\"post_code\" id=\"post_code\" type=\"txt\" class=\"input txt\" /><em>所在地区的邮政编码，非必填</em></dd>\r\n      </dl>\r\n      <dl>\r\n        <dt>手机号码：</dt>\r\n        <dd><input name=\"mobile\" id=\"mobile\" type=\"text\" class=\"input txt required\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.mobile));
	templateBuilder.Append("\" /><em>*收货人的手机号码</em></dd>\r\n      </dl>\r\n      <dl>\r\n        <dt>联系电话：</dt>\r\n        <dd><input name=\"telphone\" id=\"telphone\" type=\"text\" class=\"input txt\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.telphone));
	templateBuilder.Append("\" /><em>收货人的联系电话，非必填</em></dd>\r\n      </dl>\r\n    </div>\r\n    \r\n    <div class=\"line20\"></div>\r\n    <h3 class=\"bar_tit\">2、支付方式</h3>\r\n    <div class=\"form_box\">\r\n      ");
	DataTable list1 = get_payment_list(0, "is_lock=0");
	

	templateBuilder.Append(" <!--取得一个DataTable-->\r\n      ");
	int dr1__loop__id=0;
	foreach(DataRow dr1 in list1.Rows)
	{
		dr1__loop__id++;


	decimal poundage_amount = get_payment_poundage_amount(Utils.StrToInt(Utils.ObjectToStr(dr1["id"]), 0));
	

	templateBuilder.Append("\r\n      <dl>\r\n        <dt><label><input name=\"payment_id\" type=\"radio\" onclick=\"PaymentAmountTotal(this);\" value=\"" + Utils.ObjectToStr(dr1["id"]) + "\" /><input name=\"payment_price\" type=\"hidden\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(poundage_amount));
	templateBuilder.Append("\" /><img src=\"" + Utils.ObjectToStr(dr1["img_url"]) + "\" /></label></dt>\r\n        <dd>手续费：");
	templateBuilder.Append(Utils.ObjectToStr(poundage_amount));
	templateBuilder.Append("元<br />" + Utils.ObjectToStr(dr1["remark"]) + "</dd>\r\n      </dl>\r\n      ");
	}	//end loop


	templateBuilder.Append("\r\n    </div>\r\n    \r\n    <div class=\"line20\"></div>\r\n    <h3 class=\"bar_tit\">3、配送方式</h3>\r\n    <div class=\"form_box\">\r\n      ");
	DataTable list2 = get_distribution_list(0, "is_lock=0");
	

	templateBuilder.Append(" <!--取得一个DataTable-->\r\n      ");
	int dr2__loop__id=0;
	foreach(DataRow dr2 in list2.Rows)
	{
		dr2__loop__id++;


	templateBuilder.Append("\r\n      <dl>\r\n        <dt><label><input  name=\"distribution_id\" type=\"radio\" onclick=\"FreightAmountTotal(this);\" value=\"" + Utils.ObjectToStr(dr2["id"]) + "\" /><input name=\"distribution_price\" type=\"hidden\" value=\"" + Utils.ObjectToStr(dr2["amount"]) + "\" />" + Utils.ObjectToStr(dr2["title"]) + "</label></dt>\r\n        <dd>费用：" + Utils.ObjectToStr(dr2["amount"]) + "元<br />" + Utils.ObjectToStr(dr2["remark"]) + "</dd>\r\n      </dl>\r\n      ");
	}	//end loop


	templateBuilder.Append("\r\n    </div>\r\n    \r\n    <div class=\"line20\"></div>\r\n    <h3 class=\"bar_tit\">4、商品清单</h3>\r\n    <table width=\"938\" border=\"0\" align=\"center\" cellpadding=\"8\" cellspacing=\"0\" class=\"cart_table\">\r\n      <tr>\r\n        <th width=\"64\"></th>\r\n        <th align=\"left\">商品名称</th>\r\n        <th width=\"80\" align=\"center\">积分</th>\r\n        <th width=\"80\" align=\"center\">单价</th>\r\n        <th width=\"80\" align=\"center\">数量</th>\r\n        <th width=\"80\" align=\"center\">优惠</th>\r\n        <th width=\"100\" align=\"center\">金额小计</th>\r\n        <th width=\"100\" align=\"center\">积分小计</th>\r\n      </tr>\r\n      ");
	IList<DTcms.Model.cart_items> ls2 = get_cart_list();
	

	int modelt__loop__id=0;
	foreach(DTcms.Model.cart_items modelt in ls2)
	{
		modelt__loop__id++;


	templateBuilder.Append("\r\n      <tr>\r\n        <td><a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("goods_show",modelt.id));

	templateBuilder.Append("\"><img src=\"");
	templateBuilder.Append(Utils.ObjectToStr(modelt.img_url));
	templateBuilder.Append("\" class=\"img\" /></a></td>\r\n        <td><a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("goods_show",modelt.id));

	templateBuilder.Append("\">");
	templateBuilder.Append(Utils.ObjectToStr(modelt.title));
	templateBuilder.Append("</a></td>\r\n        <td align=\"center\">\r\n          ");
	if (modelt.point>0)
	{

	templateBuilder.Append("\r\n          +\r\n          ");
	}	//end if


	templateBuilder.Append("\r\n          ");
	templateBuilder.Append(Utils.ObjectToStr(modelt.point));
	templateBuilder.Append("\r\n        </td>\r\n        <td align=\"center\">￥");
	templateBuilder.Append(Utils.ObjectToStr(modelt.price));
	templateBuilder.Append("<input name=\"goods_price\" type=\"hidden\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(modelt.user_price));
	templateBuilder.Append("\" /></td>\r\n        <td align=\"center\">");
	templateBuilder.Append(Utils.ObjectToStr(modelt.quantity));
	templateBuilder.Append("</td>\r\n        <td align=\"center\">￥<label name=\"discount_amount\">");
	templateBuilder.Append(((modelt.price-modelt.user_price)*modelt.quantity).ToString());
	

	templateBuilder.Append("</label></td>\r\n        <td align=\"center\"><font color=\"#FF0000\" size=\"2\">￥<label name=\"real_amount\">");
	templateBuilder.Append((modelt.user_price*modelt.quantity).ToString());
	

	templateBuilder.Append("</label></font></td>\r\n        <td align=\"center\">\r\n          <font color=\"#FF0000\" size=\"2\">\r\n            ");
	if (modelt.point>0)
	{

	templateBuilder.Append("\r\n            +\r\n            ");
	}	//end if


	templateBuilder.Append("\r\n            <label name=\"point_count\">");
	templateBuilder.Append((modelt.point*modelt.quantity).ToString());
	

	templateBuilder.Append("</label>\r\n            </font>\r\n        </td>\r\n      </tr>\r\n      ");
	}	//end loop


	if (ls2.Count<1)
	{

	templateBuilder.Append("\r\n      <tr>\r\n        <td colspan=\"8\">\r\n          <div class=\"msg_tips\">\r\n            <div class=\"ico warning\"></div>\r\n            <div class=\"msg\">\r\n              <strong>购物车没有商品！</strong>\r\n              <p>您的购物车为空，<a href=\"");
	templateBuilder.Append(linkurl("index"));

	templateBuilder.Append("\">马上去购物</a>吧！</p>\r\n            </div>\r\n          </div>\r\n        </td>\r\n      </tr>\r\n      ");
	}	//end if


	templateBuilder.Append("\r\n	</table>\r\n    \r\n    <div class=\"line20\"></div>\r\n    <h3 class=\"bar_tit\">5、结算信息</h3>\r\n    <div>\r\n      <div class=\"left\">\r\n        <h4>订单留言<span>字数控制在100个字符内</span></h4>\r\n        <textarea name=\"message\" class=\"input\" style=\"width:250px;height:35px;\"></textarea>\r\n      </div>\r\n      \r\n      <div class=\"right\" style=\"text-align:right;line-height:40px;\">\r\n        商品件数：<font color=\"#FF0000\">");
	templateBuilder.Append(Utils.ObjectToStr(cartModel.total_quantity));
	templateBuilder.Append("</font> 件 &nbsp; &nbsp;&nbsp;总积分：<font color=\"#FF0000\">");
	templateBuilder.Append(Utils.ObjectToStr(cartModel.total_point));
	templateBuilder.Append("</font> 分 &nbsp; &nbsp;&nbsp;商品金额：<font color=\"#FF0000\">￥<label id=\"goods_amount\">");
	templateBuilder.Append(Utils.ObjectToStr(cartModel.real_amount));
	templateBuilder.Append("</label></font>&nbsp; &nbsp;+ &nbsp; 运费：<font color=\"#FF0000\">￥<label id=\"freight_amount\">0.00</label></font>&nbsp; &nbsp;+ &nbsp; 支付手续费：<font color=\"#FF0000\">￥<label id=\"payment_fee\">0.00</label></font>\r\n        \r\n        <br />\r\n        <b class=\"font18\">应付总金额：<font color=\"#FF0000\">￥<label id=\"order_amount\">");
	templateBuilder.Append(Utils.ObjectToStr(cartModel.real_amount));
	templateBuilder.Append("</label></font></b>\r\n      </div>\r\n    </div>\r\n\r\n    <div class=\"line20\"></div>\r\n    <div class=\"right\">\r\n      <a class=\"btn\" href=\"");
	templateBuilder.Append(linkurl("shopping","cart"));

	templateBuilder.Append("\">返回修改</a>\r\n      ");
	if (get_cart_quantity()>0)
	{

	templateBuilder.Append("\r\n      <input id=\"btnSubmit\" name=\"btnSubmit\" type=\"submit\" value=\"确认提交\" class=\"btn btn-success marL10\" />\r\n      ");
	}
	else
	{

	templateBuilder.Append("\r\n      <a class=\"btn btn-gray marL10\">确认提交</a>\r\n      ");
	}	//end if


	templateBuilder.Append("\r\n    </div>\r\n    <div class=\"clear\"></div>\r\n    <!--/结算中心-->\r\n    ");
	}	//end if


	templateBuilder.Append("\r\n  </div>\r\n</div>\r\n\r\n<div class=\"clear\"></div>\r\n\r\n<!--Footer-->\r\n");

	templateBuilder.Append("<div class=\"footer\">\r\n  <div class=\"footer_inner\">\r\n    <div class=\"clear\"></div>\r\n    <div class=\"copyright\" style=\"text-align:center\">\r\n      <p>版权所有 重庆热图科技有限公司 渝ICP备11064298号 © Copyright 2012 - 2013. rehu.co  All Rights Reserved.\r\n      </p>\r\n    </div>\r\n  </div>\r\n</div>");


	templateBuilder.Append("\r\n<!--/Footer-->\r\n</body>\r\n</html>\r\n");
	Response.Write(templateBuilder.ToString());
}
</script>
