<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="DTcms.Web.admin.attribute.list" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>属性管理</title>
<link type="text/css" rel="stylesheet" href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" />
<link type="text/css" rel="stylesheet" href="../images/style.css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
<script type="text/javascript" src="../js/function.js"></script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
    <div class="navigation">首页 &gt; 扩展属性 &gt; 属性列表</div>
    <div class="tools_box">
	    <div class="tools_bar">
		    <a href="edit.aspx?action=<%=DTEnums.ActionEnum.Add %>&channel_id=<%=this.channel_id %>" class="tools_btn"><span><b class="add">添加属性</b></span></a>
		    <a href="javascript:void(0);" onclick="checkAll(this);" class="tools_btn"><span><b class="all">全选</b></span></a>
            <asp:LinkButton ID="btnDelete" runat="server" CssClass="tools_btn" onclick="btnDelete_Click" OnClientClick="return ExePostBack('btnDelete');"><span><b class="delete">批量删除</b></span></asp:LinkButton>
        </div>
    </div>

    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th width="6%">选择</th>
        <th width="20%" align="left">属性名称</th>
        <th align="left">备注</th>
        <th width="12%" align="left">类型</th>
        <th width="8%">排序</th>
        <th width="10%">操作</th>
      </tr>
    <asp:Repeater ID="rptList" runat="server">
    <ItemTemplate>
      <tr>
        <td align="center"><asp:CheckBox ID="chkId" CssClass="checkall" runat="server" /><asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" /></td>
        <td><a href="edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&channel_id=<%#this.channel_id %>&id=<%#Eval("id")%>"><%#Eval("title")%></a></td>
        <td><%#Eval("remark")%></td>
        <td><%#GetTypeCn(Convert.ToInt32(Eval("type")))%></td>
        <td align="center"><%#Eval("sort_id")%></td>
        <td align="center"><a href="edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&channel_id=<%#this.channel_id %>&id=<%#Eval("id")%>">修改</a></td>
      </tr>
    </ItemTemplate>
    <FooterTemplate><%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"6\">暂无记录</td></tr>" : ""%></FooterTemplate>
    </asp:Repeater>
    </table>
    
    <div class="line10"></div>
</form>
</body>
</html>
