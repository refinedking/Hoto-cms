<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="plugin_list.aspx.cs" Inherits="DTcms.Web.admin.settings.plugin_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>插件管理</title>
<link type="text/css" rel="stylesheet" href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" />
<link type="text/css" rel="stylesheet" href="../images/style.css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
<script type="text/javascript" src="../js/function.js"></script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
    <div class="navigation">首页 &gt; 控制面板 &gt; 插件管理</div>
    <div class="tools_box">
	    <div class="tools_bar">
		    <a href="javascript:void(0);" onclick="checkAll(this);" class="tools_btn"><span><b class="all">全选</b></span></a>
            <asp:LinkButton ID="btnInstall" runat="server" CssClass="tools_btn" OnClientClick="return ExePostBack('btnInstall', '已安装的插件不执行重复安装，确定继续吗？');" 
                onclick="btnInstall_Click"><span><b class="import">批量安装</b></span></asp:LinkButton>
                <asp:LinkButton ID="lbtnRemark" runat="server" CssClass="tools_btn" onclick="lbtnRemark_Click"><span><b class="refresh">生成模板</b></span></asp:LinkButton>
            <asp:LinkButton ID="btnUninstall" runat="server" CssClass="tools_btn" OnClientClick="return ExePostBack('btnUninstall', '卸载插件不会删除插件目录，确定继续吗？');" 
                onclick="btnUninstall_Click"><span><b class="delete">批量卸载</b></span></asp:LinkButton>
        </div>
    </div>

    <asp:Repeater ID="rptList" runat="server">
    <HeaderTemplate>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th width="6%">选择</th>
        <th width="20%" align="left">插件名称</th>
        <th width="15%">目录</th>
        <th width="10%">作者</th>
        <th width="10%">版本号</th>
        <th  align="left">备注</th>
        <th width="12%">状态</th>
      </tr>
    </HeaderTemplate>
    <ItemTemplate>
      <tr>
        <td align="center"><asp:CheckBox ID="chkId" CssClass="checkall" runat="server" /><asp:HiddenField ID="hidDirName" Value='<%#Eval("directory")%>' runat="server" /></td>
        <td><%#Eval("name")%></td>
         <td align="center"><%#Eval("directory")%></td>
        <td align="center"><%#Eval("author")%></td>
        <td align="center"><%#Eval("version")%></td>
        <td><%#Eval("description")%></td>
        <td align="center"><%#Convert.ToInt32(Eval("isload")) == 1 ? "<img src=\"../images/icon_correct.png\" title=\"已安装\" />" : "<img src=\"../images/icon_disable.png\" title=\"未激活\" />"%></td>
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
