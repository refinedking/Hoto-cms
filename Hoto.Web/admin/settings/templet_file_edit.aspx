<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templet_file_edit.aspx.cs" Inherits="DTcms.Web.admin.settings.templet_file_edit" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<link href="../images/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../js/function.js"></script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
<div class="navigation"><a href="javascript:history.go(-1);" class="back">后退</a>首页 &gt; 控制面板 &gt; 模板管理 &gt; 编辑模板：<%=pathName%></div>
<div id="contentTab">
    <ul class="tab_nav">
        <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">编辑模板信息</a></li>
    </ul>
    <div class="tab_con" style="display:block;">
        <table class="form_table">
            <col width="180px"><col>
            <tbody>
            <tr>
                <th>文件名称：</th>
                <td><%=fileName %></td>
            </tr>
            <tr>
                <th valign="top">文件内容：</th>
                <td><asp:TextBox ID="txtContent" runat="server" CssClass="txtInput" TextMode="MultiLine" style="width:98%;height:450px;"></asp:TextBox></td>
            </tr>
            <tr>
                <th></th>
                <td><asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btnSubmit" onclick="btnSubmit_Click" /></td>
            </tr>
            </tbody>
        </table>
    </div>
    
</div>
</form>
</body>
</html>
