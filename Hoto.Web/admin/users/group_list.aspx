<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="group_list.aspx.cs" Inherits="DTcms.Web.admin.users.group_list" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>用户组列表</title>
<link type="text/css" rel="stylesheet" href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" />
<link type="text/css" rel="stylesheet" href="../images/style.css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
<script type="text/javascript" src="../js/function.js"></script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
    <div class="navigation">首页 &gt; 会员管理 &gt; 会员组列表</div>
    <div class="tools_box">
	    <div class="tools_bar">
		    <div class="search_box">
			    <asp:TextBox ID="txtKeywords" runat="server" CssClass="txtInput"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="搜 索" CssClass="btnSearch" onclick="btnSearch_Click" />
		    </div>
		    <a href="group_edit.aspx?action=<%=DTEnums.ActionEnum.Add %>" class="tools_btn"><span><b class="add">添加组别</b></span></a>
		    <a href="javascript:void(0);" onclick="checkAll(this);" class="tools_btn"><span><b class="all">全选</b></span></a>
            <asp:LinkButton ID="btnDelete" runat="server" CssClass="tools_btn" onclick="btnDelete_Click" OnClientClick="return ExePostBack('btnDelete');"><span><b class="delete">批量删除</b></span></asp:LinkButton>
        </div>
    </div>

    <asp:Repeater ID="rptList" runat="server">
    <HeaderTemplate>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th width="6%">选择</th>
        <th align="left">组别名称</th>
        <th width="8%">等级值</th>
        <th width="12%">升级积分</th>
        <th width="12%">初始金额</th>
        <th width="12%">初始积分</th>
        <th width="9%">购物折扣</th>
        <th width="8%">注册组</th>
        <th width="6%">状态</th>
        <th width="10%">操作</th>
      </tr>
    </HeaderTemplate>
    <ItemTemplate>
      <tr>
        <td align="center"><asp:CheckBox ID="chkId" CssClass="checkall" runat="server" /><asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" /></td>
        <td><a href="group_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>"><%#Eval("title")%></a></td>
        <td align="center"><%#Eval("grade")%></td>
        <td align="center"><%#Eval("upgrade_exp")%></td>
        <td align="center"><%#Eval("amount")%></td>
        <td align="center"><%#Eval("point")%></td>
        <td align="center"><%#Eval("discount")%>%</td>
        <td align="center"><%#Convert.ToInt32(Eval("is_default")) == 1 ? "是" : "否"%></td>
        <td align="center"><%#Convert.ToInt32(Eval("is_lock")) == 1 ? "禁用" : "启用"%></td>
        <td align="center"><a href="group_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>">修改</a></td>
      </tr>
    </ItemTemplate>
    <FooterTemplate>
      <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"9\">暂无记录</td></tr>" : ""%>
      </table>
    </FooterTemplate>
    </asp:Repeater>
    <div class="line10"></div>
</form>
</body>
</html>
