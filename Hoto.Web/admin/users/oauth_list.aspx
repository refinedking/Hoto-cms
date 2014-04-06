<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="oauth_list.aspx.cs" Inherits="DTcms.Web.admin.users.oauth_list" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>OAuth管理</title>
<link type="text/css" rel="stylesheet" href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" />
<link type="text/css" rel="stylesheet" href="../images/style.css" />
<link type="text/css" rel="stylesheet" href="../../css/pagination.css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
<script type="text/javascript" src="../js/function.js"></script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
    <div class="navigation">首页 &gt; OAuth管理 &gt; OAuth列表</div>
    <div class="tools_box">
	    <div class="tools_bar">
            <div class="search_box">
			    <asp:TextBox ID="txtKeywords" runat="server" CssClass="txtInput"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="搜 索" CssClass="btnSearch" onclick="btnSearch_Click" />
		    </div>
            <a href="oauth_edit.aspx?action=<%=DTEnums.ActionEnum.Add %>" class="tools_btn"><span><b class="add">添加OAuth</b></span></a>
            <asp:LinkButton ID="btnSave" runat="server" CssClass="tools_btn" onclick="btnSave_Click"><span><b class="send">保存排序</b></span></asp:LinkButton>
            <a href="oauth_user_list.aspx" class="tools_btn"><span><b class="return">管理授权</b></span></a>
		    <a href="javascript:void(0);" onclick="checkAll(this);" class="tools_btn"><span><b class="all">全选</b></span></a>
            <asp:LinkButton ID="btnDelete" runat="server" CssClass="tools_btn"  
                OnClientClick="return ExePostBack('btnDelete');" onclick="btnDelete_Click"><span><b class="delete">批量删除</b></span></asp:LinkButton>
        </div>
    </div>

    <!--列表展示.开始-->
    <asp:Repeater ID="rptList" runat="server">
    <HeaderTemplate>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th width="6%">选择</th>
        <th width="15%" align="left">标题</th>
        <th width="10%" align="left">图标</th>
        <th width="12%" align="left">目录名称</th>
        <th align="left">描述</th>
        <th width="60">排序</th>
        <th width="110">状态</th>
        <th width="8%">操作</th>
      </tr>
    </HeaderTemplate>
    <ItemTemplate>
      <tr>
        <td align="center"><asp:CheckBox ID="chkId" CssClass="checkall" runat="server" /><asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" /></td>
        <td><a href="oauth_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>"><%#Eval("title")%></a></td>
        <td><img src="<%#Eval("img_url")%>" /></td>
        <td><%#Eval("api_path")%></td>
        <td><%#Eval("remark")%></td>
        <td align="center"><asp:TextBox ID="txtSortId" runat="server" Text='<%#Eval("sort_id")%>' CssClass="txtInput2 small2" onkeypress="return (/[\d]/.test(String.fromCharCode(event.keyCode)));" /></td>
        <td align="center"><%#Convert.ToInt32(Eval("is_lock")) == 1 ? "<img src=\"../images/icon_disable.png\" title=\"未启用\" />" : "<img src=\"../images/icon_correct.png\" title=\"正常\" />"%></td>
        <td align="center"><a href="oauth_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>">修改</a></td>
      </tr>
    </ItemTemplate>
    <FooterTemplate>
      <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"8\">暂无记录</td></tr>" : ""%>
      </table>
    </FooterTemplate>
    </asp:Repeater>
    <!--列表展示.结束-->

    <div class="line10"></div>
</form>
</body>
</html>
