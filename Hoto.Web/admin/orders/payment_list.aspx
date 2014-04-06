<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="payment_list.aspx.cs" Inherits="DTcms.Web.admin.orders.payment_list" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>支付方式列表</title>
<link type="text/css" rel="stylesheet" href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" />
<link type="text/css" rel="stylesheet" href="../images/style.css" />
<link type="text/css" rel="stylesheet" href="../../css/pagination.css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
<script type="text/javascript" src="../js/function.js"></script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
    <div class="navigation">首页 &gt; 销售管理 &gt; 支付方式列表</div>
    <div id="navtips" class="navtips">请设置正确的在线支付API的账户信息，否则无法正常收款！<a href="javascript:CloseTip('navtips');" class="close">关闭</a></div>
    <div class="line10"></div>
    <!--列表展示.开始-->
    <asp:Repeater ID="rptList" runat="server">
    <HeaderTemplate>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th width="6%">序号</th>
        <th width="15%" align="left">名称</th>
        <th width="15%">图标</th>
        <th align="left">支付描述</th>
        <th width="8%">排序</th>
        <th width="8%">状态</th>
        <th width="6%">操作</th>
      </tr>
    </HeaderTemplate>
    <ItemTemplate>
      <tr>
        <td align="center"><%#Eval("id")%></td>
        <td><%#Eval("title")%></td>
        <td align="center"><img width="125" height="45" src="<%#Eval("img_url")%>" /></td>
        <td><%#Eval("remark")%></td>
        <td align="center"><%#Eval("sort_id")%></td>
        <td align="center"><%#Convert.ToInt32(Eval("is_lock")) == 1 ? "已停用" : "正常"%></td>
        <td align="center"><a href="payment_edit.aspx?id=<%#Eval("id")%>">配置</a></td>
      </tr>
    </ItemTemplate>
    <FooterTemplate>
      <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"7\">暂无记录</td></tr>" : ""%>
      </table>
    </FooterTemplate>
    </asp:Repeater>
    <!--列表展示.结束-->

    <div class="line10"></div>
</form>
</body>
</html>
