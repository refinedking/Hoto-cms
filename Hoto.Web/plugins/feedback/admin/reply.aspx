<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reply.aspx.cs" Inherits="DTcms.Web.Plugin.Feedback.admin.reply" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>回复留言信息</title>
<link href="../../../scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
<link href="../../../admin/images/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../../scripts/jquery/jquery.validate.min.js"></script> 
<script type="text/javascript" src="../../../scripts/jquery/messages_cn.js"></script>
<script type="text/javascript" src="../../../scripts/ui/js/ligerBuild.min.js"></script>
<script type="text/javascript" src="../../../admin/js/function.js"></script>
<script type="text/javascript">
    //表单验证
    $(function () {
        $("#form1").validate({
            errorPlacement: function (lable, element) {
                element.ligerTip({ content: lable.html(), appendIdTo: lable });
            },
            success: function (lable) {
                lable.ligerHideTip();
            }
        });
    });
</script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
<div class="navigation"><a href="javascript:history.go(-1);" class="back">后退</a>首页 &gt; 插件管理 &gt; 在线留言</div>
<div id="contentTab">
    <ul class="tab_nav">
        <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">回复留言</a></li>
    </ul>
    <div class="tab_con" style="display:block;">
        <table class="form_table">
            <col width="180px"><col>
            <tbody>
            <tr>
                <th>联系人：</th>
                <td><%=model.user_name %></td>
            </tr>
            <tr>
                <th>联系电话：</th>
                <td><%=model.user_tel %></td>
            </tr>
            <tr>
                <th>在线QQ：</th>
                <td><%=model.user_qq %></td>
            </tr>
            <tr>
                <th>电子邮箱：</th>
                <td><%=model.user_email %></td>
            </tr>
            <tr>
                <th>留言时间：</th>
                <td><%=model.add_time %></td>
            </tr>
            <tr>
                <th>留言标题：</th>
                <td><%=model.title %></td>
            </tr>
            <tr>
                <th>留言内容：</th>
                <td><%=model.content %></td>
            </tr>
            <tr>
                <th>回复内容：</th>
                <td><asp:TextBox ID="txtReContent" runat="server" maxlength="255" TextMode="MultiLine" CssClass="small" /></td>
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
