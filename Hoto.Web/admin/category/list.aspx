<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="DTcms.Web.admin.category.list" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>类别管理</title>
<link type="text/css" rel="stylesheet" href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" />
<link type="text/css" rel="stylesheet" href="../images/style.css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
<script type="text/javascript" src="../js/function.js"></script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
    <div class="navigation">首页 &gt; 类别管理 &gt; 类别列表</div>
    <div class="tools_box">
	    <div class="tools_bar">
            <a href="edit.aspx?action=<%=DTEnums.ActionEnum.Add %>&channel_id=<%=this.channel_id %>" class="tools_btn"><span><b class="add">添加类别</b></span></a>
            <asp:LinkButton ID="btnSave" runat="server" CssClass="tools_btn" onclick="btnSave_Click"><span><b class="send">保存排序</b></span></asp:LinkButton>
		    <a href="javascript:void(0);" onclick="checkAll(this);" class="tools_btn"><span><b class="all">全选</b></span></a>
            <asp:LinkButton ID="btnDelete" runat="server" CssClass="tools_btn" onclick="btnDelete_Click" OnClientClick="return ExePostBack('btnDelete', '本操作会删除本类别和下属类别，确定要继续吗？');"><span><b class="delete">批量删除</b></span></asp:LinkButton>
        </div>
    </div>

    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th width="6%">选择</th>
        <th width="6%">编号</th>
        <th align="left" width="13%">调用别名</th>
        <th align="left">类别名称</th>
        <th align="left" width="80">排序</th>
        <th width="12%">操作</th>
      </tr>
    <asp:Repeater ID="rptList" runat="server" onitemdatabound="rptList_ItemDataBound">
    <ItemTemplate>
      <tr>
        <td align="center">
            <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" />
            <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
            <asp:HiddenField ID="hidLayer" Value='<%#Eval("class_layer") %>' runat="server" />
        </td>
        <td align="center"><%#Eval("id")%></td>
        <td><%#Eval("call_index")%></td>
        <td>
            <asp:Literal ID="LitFirst" runat="server"></asp:Literal>
            <a href="edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&channel_id=<%#this.channel_id %>&id=<%#Eval("id")%>"><%#Eval("title")%></a>
        </td>
        <td><asp:TextBox ID="txtSortId" runat="server" Text='<%#Eval("sort_id")%>' CssClass="txtInput2 small2" /></td>
        <td align="center">
            <a href="edit.aspx?action=<%#DTEnums.ActionEnum.Add %>&channel_id=<%#this.channel_id %>&id=<%#Eval("id")%>">添加子类</a>
            <a href="edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&channel_id=<%#this.channel_id %>&id=<%#Eval("id")%>">修改</a>
        </td>
      </tr>
    </ItemTemplate>
    <FooterTemplate><%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"7\">暂无记录</td></tr>" : ""%></FooterTemplate>
    </asp:Repeater>
    </table>
    <div class="line10"></div>
</form>
</body>
</html>