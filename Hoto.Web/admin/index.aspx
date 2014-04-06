<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="DTcms.Web.admin.index" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title><%=siteConfig.webname %> - 后台管理</title>
<link href="../scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
<link href="images/style.css" rel="stylesheet" type="text/css" />
<script src="../scripts/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
<script src="../scripts/ui/js/ligerBuild.min.js" type="text/javascript"></script>
<script src="js/function.js" type="text/javascript"></script>

<script type="text/javascript">
    var tab = null;
    var accordion = null;
    var tree = null;
    $(function () {
        //页面布局
        $("#global_layout").ligerLayout({ leftWidth: 180, height: '100%', topHeight: 65, bottomHeight: 24, allowTopResize: false, allowBottomResize: false, allowLeftCollapse: true, onHeightChanged: f_heightChanged });

        var height = $(".l-layout-center").height();

        //Tab
        $("#framecenter").ligerTab({ height: height });

        //左边导航面板
        $("#global_left_nav").ligerAccordion({ height: height - 25, speed: null });

        $(".l-link").hover(function () {
            $(this).addClass("l-link-over");
        }, function () {
            $(this).removeClass("l-link-over");
        });

        //设置频道菜单
        $("#global_channel_tree").ligerTree({
            url: '../tools/admin_ajax.ashx?action=sys_channel_load',
            checkbox: false,
            nodeWidth: 112,
            //attribute: ['nodename', 'url'],
            onSelect: function (node) {
                if (!node.data.url) return;
                var tabid = $(node.target).attr("tabid");
                if (!tabid) {
                    tabid = new Date().getTime();
                    $(node.target).attr("tabid", tabid)
                }
                f_addTab(tabid, node.data.text, node.data.url);
            }
        });

        //加载插件菜单
        loadPluginsNav();

        //快捷菜单
        var menu = $.ligerMenu({ width: 120, items:
		[
			{ text: '管理首页', click: itemclick },
			{ text: '修改密码', click: itemclick },
			{ line: true },
			{ text: '关闭菜单', click: itemclick }
		]
        });
        $("#tab-tools-nav").bind("click", function () {
            var offset = $(this).offset(); //取得事件对象的位置
            menu.show({ top: offset.top + 27, left: offset.left - 120 });
            return false;
        });

        tab = $("#framecenter").ligerGetTabManager();
        accordion = $("#global_left_nav").ligerGetAccordionManager();
        tree = $("#global_channel_tree").ligerGetTreeManager();
        //tree.expandAll(); //默认展开所有节点
        $("#pageloading_bg,#pageloading").hide();
    });

    //频道菜单异步加载函数，结合ligerMenu.js使用
    function loadChannelTree() {
        if (tree != null) {
            tree.clear();
            tree.loadData(null, "../tools/admin_ajax.ashx?action=sys_channel_load");
        }
    }

    //加载插件管理菜单
    function loadPluginsNav() {
        $.ajax({
            type: "POST",
            url: "../tools/admin_ajax.ashx?action=plugins_nav_load&time=" + Math.random(),
            timeout: 20000,
            beforeSend: function (XMLHttpRequest) {
                $("#global_plugins").html("<div style=\"line-height:30px; text-align:center;\">正在加载，请稍候...</div>");
            },
            success: function (data, textStatus) {
                $("#global_plugins").html(data);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $("#global_plugins").html("<div style=\"line-height:30px; text-align:center;\">加载插件菜单出错！</div>");
            }
        });
    }

    //快捷菜单回调函数
    function itemclick(item) {
        switch (item.text) {
            case "管理首页":
                f_addTab('home', '管理中心', 'center.aspx');
                break;
            case "快捷导航":
                //调用函数
                break;
            case "修改密码":
                f_addTab('manager_pwd', '修改密码', 'manager/manager_pwd.aspx');
                break;
            default:
                //关闭窗口
                break;
        }
    }
    function f_heightChanged(options) {
        if (tab)
            tab.addHeight(options.diff);
        if (accordion && options.middleHeight - 24 > 0)
            accordion.setHeight(options.middleHeight - 24);
    }
    //添加Tab，可传3个参数
    function f_addTab(tabid, text, url, iconcss) {
        if (arguments.length == 4) {
            tab.addTabItem({ tabid: tabid, text: text, url: url, iconcss: iconcss });
        } else {
            tab.addTabItem({ tabid: tabid, text: text, url: url });
        }
    }
    //提示Dialog并关闭Tab
    function f_errorTab(tit, msg) {
        $.ligerDialog.open({
            isDrag: false,
            allowClose: false,
            type: 'error',
            title: tit,
            content: msg,
            buttons: [{
                text: '确定',
                onclick: function (item, dialog, index) {
                    //查找当前iframe名称
                    var itemiframe = "#framecenter .l-tab-content .l-tab-content-item";
                    var curriframe = "";
                    $(itemiframe).each(function () {
                        if ($(this).css("display") != "none") {
                            curriframe = $(this).attr("tabid");
                            return false;
                        }
                    });
                    if (curriframe != "") {
                        tab.removeTabItem(curriframe);
                        dialog.close();
                    }
                }
            }]
        });
    }
</script>
</head>
<body style="padding:0px;">
<form id="form1" runat="server">
    <div class="pageloading_bg" id="pageloading_bg"></div>
    <div id="pageloading">数据加载中，请稍等...</div>
    <div id="global_layout" class="layout" style="width:100%">
        <!--头部-->
  <div position="top" class="header">
            <div class="header_box">
                <div class="header_right">
                    <span><b><%=admin_info.user_name %>（<%=new DTcms.BLL.manager_role().GetTitle(admin_info.role_id) %>）</b>您好，欢迎光临</span>
                    <br /><a href="javascript:f_addTab('home','管理中心','center.aspx')">管理中心</a> | 
                    <a target="_blank" href="../">预览网站</a> | 
                    <asp:LinkButton ID="lbtnExit" runat="server" onclick="lbtnExit_Click">安全退出</asp:LinkButton>
                </div>
                <a class="logo">DTcms Logo</a>
            </div>
        </div>
        <!--左边-->
        <div position="left"  title="管理菜单" id="global_left_nav"> 
            <div title="频道管理" iconcss="menu-icon-model" class="l-scroll">
                <ul id="global_channel_tree" style="margin-top:3px;">
                    <!--
                    <li isexpand="false"><span>资讯动态</span>
                        <ul> 
                            <li url="http://bbs.it134.cn"><span>内容管理</span></li>
                            <li url="demos/base/drag.htm"><span>栏目类别</span></li>
                        </ul>
                    </li>
                     -->
                </ul>
            </div>
            <%if (siteConfig.memberstatus == 1)
              { %>
            <div title="会员管理" iconcss="menu-icon-member">
                <ul class="nlist">
                    <%if (IsAdminLevel("users", DTEnums.ActionEnum.View.ToString()))
                      {%>
                    <li><a href="javascript:f_addTab('user_list','会员信息管理','users/user_list.aspx')">会员信息管理</a></li>
                    <%}
                      if (IsAdminLevel("user_groups", DTEnums.ActionEnum.View.ToString()))
                      { %>
                    <li><a href="javascript:f_addTab('user_groups','会员组别管理','users/group_list.aspx')">会员组别管理</a></li>
                    <%}
                      if (IsAdminLevel("user_message", DTEnums.ActionEnum.View.ToString()))
                      { %>
                    <li><a href="javascript:f_addTab('user_message','会员短信管理','users/message_list.aspx')">会员短信管理</a></li>
                    <%}
                      if (IsAdminLevel("amount_log", DTEnums.ActionEnum.View.ToString()))
                      { %>
                    <li><a href="javascript:f_addTab('amount_log','会员消费记录','users/amount_log.aspx')">会员消费记录</a></li>
                    <%}
                      if (IsAdminLevel("point_log", DTEnums.ActionEnum.View.ToString()))
                      { %>
                    <li><a href="javascript:f_addTab('point_log','会员积分记录','users/point_log.aspx')">会员积分记录</a></li>
                    <%}
                      if (IsAdminLevel("mail_template", DTEnums.ActionEnum.View.ToString()))
                      { %>
                    <li><a href="javascript:f_addTab('mail_template','邮件模板管理','users/mail_template_list.aspx')">邮件模板管理</a></li>
                    <%}
                      if (IsAdminLevel("app_oauth", DTEnums.ActionEnum.View.ToString()))
                      { %>
                    <li><a href="javascript:f_addTab('app_oauth','OAuth平台设置','users/oauth_list.aspx')">OAuth平台设置</a></li>
                    <%}
                      if (IsAdminLevel("user_config", DTEnums.ActionEnum.View.ToString()))
                      { %>
                    <li><a class="l-link" href="javascript:f_addTab('user_config','会员参数配置','users/user_config.aspx')">会员参数配置</a></li>
                    <%} %>
                </ul>
            </div>
            <div title="销售管理" iconcss="menu-icon-order">
                <ul class="nlist">
                    <%if (IsAdminLevel("orders", DTEnums.ActionEnum.View.ToString()))
                      {%>
                    <li><a href="javascript:f_addTab('orders','商品订单列表','orders/order_list.aspx')">商品订单列表</a></li>
                    <%}
                      if (IsAdminLevel("user_config", DTEnums.ActionEnum.View.ToString()))
                      { %>
                    <li><a href="javascript:f_addTab('order_payment','支付方式设置','orders/payment_list.aspx')">支付方式设置</a></li>
                    <%}
                      if (IsAdminLevel("user_config", DTEnums.ActionEnum.View.ToString()))
                      { %>
                    <li><a href="javascript:f_addTab('distribution','配送方式设置','orders/distribution_list.aspx')">配送方式设置</a></li>
                    <%} %>
                </ul>
            </div>
            <%} %>
            <%if (IsAdminLevel("sys_plugin", DTEnums.ActionEnum.View.ToString()))
              { %>
            <div title="插件管理" iconcss="menu-icon-plugins">
                <ul id="global_plugins" class="nlist">
                    <!--
                    <li><a class="l-link" href="javascript:f_addTab('listpage21','广告管理','demos/case/listpage21.htm')">广告管理</a></li>
                    <li><a class="l-link" href="javascript:f_addTab('listpage22','新闻采集','demos/case/listpage22.htm')">新闻采集</a></li>
                    <li><a class="l-link" href="javascript:f_addTab('listpage23','问卷调查','demos/case/listpage23.htm')">问卷调查</a></li>
                    <li><a class="l-link" href="javascript:f_addTab('listpage24','自定义表单','demos/case/listpage24.htm')">自定义表单</a></li>
                    <li><a class="l-link" href="javascript:f_addTab('listpage25','在线留言','demos/case/listpage25.htm')">在线留言</a></li>
                    <li><a class="l-link" href="javascript:f_addTab('listpage26','友情链接','demos/case/listpage25.htm')">友情链接</a></li>
                    <li><a class="l-link" href="javascript:f_addTab('listpage27','Tag标签','demos/case/listpage25.htm')">Tag标签</a></li>
                    <li><a class="l-link" href="javascript:f_addTab('listpage28','整合接口','demos/case/listpage25.htm')">整合接口</a></li>
                    -->
                </ul>
            </div>
            <%} %>
            <div title="控制面板" iconcss="menu-icon-setting">
                <ul class="nlist">
                    <%if (IsAdminLevel("sys_config", DTEnums.ActionEnum.View.ToString()))
                      { %>
                    <li><a class="l-link" href="javascript:f_addTab('sys_config','系统参数设置','settings/sys_config.aspx')">系统参数设置</a></li>
                    <%} if (IsAdminLevel("sys_model", DTEnums.ActionEnum.View.ToString()))
                      { %>
                    <li><a class="l-link" href="javascript:f_addTab('sys_model','系统模型配置','settings/sys_model_list.aspx')">系统模型配置</a></li>
                    <%} if (IsAdminLevel("sys_channel", DTEnums.ActionEnum.View.ToString()))
                      { %>
                    <li><a class="l-link" href="javascript:f_addTab('sys_channel','系统频道设置','settings/sys_channel_list.aspx')">系统频道设置</a></li>
                    <%} if (IsAdminLevel("sys_plugin", DTEnums.ActionEnum.View.ToString()))
                      { %>
                    <li><a class="l-link" href="javascript:f_addTab('plugin_list','系统插件管理','settings/plugin_list.aspx')">系统插件管理</a></li>
                    <%} if (IsAdminLevel("sys_templet", DTEnums.ActionEnum.View.ToString()))
                      { %>
                    <li><a class="l-link" href="javascript:f_addTab('templet_list','系统模板管理','settings/templet_list.aspx')">系统模板管理</a></li>
                    <%} if (IsAdminLevel("sys_log", DTEnums.ActionEnum.View.ToString()))
                      { %>
                    <li><a class="l-link" href="javascript:f_addTab('manager_log','系统日志管理','manager/manager_log.aspx')">系统日志管理</a></li>
                    <%} %>
                    <li><a class="l-link" href="javascript:f_addTab('comment_list','内容评论管理','comment/list.aspx')">内容评论管理</a></li>
                    <%if (IsAdminLevel("sys_manager", DTEnums.ActionEnum.View.ToString()))
                      { %>
                    <li><a class="l-link" href="javascript:f_addTab('sys_manager','管理员管理','manager/manager_list.aspx')">管理员管理</a></li>
                    <%}%>
                </ul>
            </div>
        </div>
        <div position="center" id="framecenter" toolsid="tab-tools-nav"> 
            <div tabid="home" title="管理中心" iconcss="tab-icon-home" style="height:300px" >
                <iframe frameborder="0" name="sysMain" src="center.aspx"></iframe>
            </div> 
        </div> 
        <div position="bottom" class="footer">
            <div class="copyright">Copyright &copy; 2009 - 2012. dtcms.net. All Rights Reserved.</div>
        </div>
    </div>
</form>
</body>
</html>
