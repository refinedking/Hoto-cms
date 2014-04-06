<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templet_file_list.aspx.cs" Inherits="DTcms.Web.admin.settings.templet_file_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>模板管理</title>
<link type="text/css" rel="stylesheet" href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" />
<link type="text/css" rel="stylesheet" href="../images/style.css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
<script type="text/javascript" src="../js/function.js"></script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
    <div class="navigation"><a href="javascript:history.go(-1);" class="back">后退</a>首页 &gt; 控制面板 &gt; 模板管理 &gt; 编辑模板：<%=skinName%></div>
    <div class="tools_box">
	    <div class="tools_bar">
            <a href="javascript:void(0);" onclick="checkAll(this);" class="tools_btn"><span><b class="all">全选</b></span></a>
            <asp:LinkButton ID="btnDelete" runat="server" CssClass="tools_btn" onclick="btnDelete_Click" OnClientClick="return ExePostBack('btnDelete','此操作将会彻底删除文件，确定要继续吗？');"><span><b class="delete">批量删除</b></span></asp:LinkButton>
        </div>
    </div>

    <asp:Repeater ID="rptList" runat="server">
    <HeaderTemplate>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th width="30">选择</th>
        <th align="left">文件名称</th>
        <th width="20%" align="left">创建时间</th>
        <th width="20%" align="left">最后修改时间</th>
        <th width="8%">操作</th>
      </tr>
    </HeaderTemplate>
    <ItemTemplate>
      <tr>
        <td align="center"><asp:CheckBox ID="chkId" CssClass="checkall" runat="server" /><asp:HiddenField ID="hideName" runat="server" Value='<%#Eval("name") %>' /></td>
        <td><a href="templet_file_edit.aspx?path=<%#Eval("skinname")%>&filename=<%#Eval("name")%>"><%#Eval("name")%></a></td>
        <td><%#Eval("creationtime")%></td>
        <td><%#Eval("updatetime")%></td>
        <td align="center"><a href="templet_file_edit.aspx?path=<%#Eval("skinname")%>&filename=<%#Eval("name")%>">编辑</a></td>
      </tr>
    </ItemTemplate>
    <FooterTemplate>
    </table>
    </FooterTemplate>
    </asp:Repeater>
    <div class="line10"></div>
</form>
</body>
</html>
