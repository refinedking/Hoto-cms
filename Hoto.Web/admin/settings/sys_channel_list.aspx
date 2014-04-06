<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sys_channel_list.aspx.cs" Inherits="DTcms.Web.admin.settings.sys_channel_list" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>系统频道列表</title>
<link type="text/css" rel="stylesheet" href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" />
<link type="text/css" rel="stylesheet" href="../images/style.css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
<script type="text/javascript" src="../js/function.js"></script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
    <div class="navigation">首页 &gt; 控制面板 &gt; 系统频道</div>
    <div class="tools_box">
	    <div class="tools_bar">
		    <div class="search_box">
			    <asp:TextBox ID="txtKeywords" runat="server" CssClass="txtInput"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="搜 索" CssClass="btnSearch" onclick="btnSearch_Click" />
		    </div>
		    <a href="sys_channel_edit.aspx?action=<%=DTEnums.ActionEnum.Add %>" class="tools_btn"><span><b class="add">添加频道</b></span></a>
		    <a href="javascript:void(0);" onclick="checkAll(this);" class="tools_btn"><span><b class="all">全选</b></span></a>
            <asp:LinkButton ID="btnDelete" runat="server" CssClass="tools_btn" onclick="btnDelete_Click" OnClientClick="return ExePostBack('btnDelete', '在删除频道之前，请确认您已经删除了该频道下的所有内容，以免造成数据冗余，确定继续删除吗？');"><span><b class="delete">批量删除</b></span></asp:LinkButton>
        </div>
    </div>

    <asp:Repeater ID="rptList" runat="server">
    <HeaderTemplate>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th width="6%">选择</th>
        <th width="6%">编号</th>
        <th align="left">频道标题</th>
        <th align="left" width="12%">频道名称</th>
        <th width="12%" align="left">所属模型</th>
        <th width="15%">排序</th>
        <th width="10%">操作</th>
      </tr>
    </HeaderTemplate>
    <ItemTemplate>
      <tr>
        <td align="center"><asp:CheckBox ID="chkId" CssClass="checkall" runat="server" /><asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" /></td>
        <td align="center"><%#Eval("id")%></td>
        <td><a href="sys_channel_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>"><%#Eval("title")%></a></td>
        <td><%#Eval("name")%></td>
        <td><%#Eval("model_title")%></td>
        <td align="center"><%#Eval("sort_id")%></td>
        <td align="center"><a href="sys_channel_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>">修改</a></td>
      </tr>
    </ItemTemplate>
    <FooterTemplate>
      <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"7\">暂无记录</td></tr>" : ""%>
      </table>
    </FooterTemplate>
    </asp:Repeater>
    <div class="line10"></div>
</form>
</body>
</html>
