<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mail_template_edit.aspx.cs" Inherits="DTcms.Web.admin.users.mail_template_edit" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>编辑模板信息</title>
<link href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
<link href="../images/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../scripts/jquery/jquery.validate.min.js"></script> 
<script type="text/javascript" src="../../scripts/jquery/messages_cn.js"></script>
<script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
<script type="text/javascript" src="../js/function.js"></script>
<script type="text/javascript" charset="utf-8" src="../../editor/kindeditor-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../editor/lang/zh_CN.js"></script>
<script type="text/javascript">
    //加载编辑器
    $(function () {
        var editor = KindEditor.create('textarea[name="txtContent"]', {
            resizeType: 1,
            uploadJson: '../../tools/upload_ajax.ashx?action=EditorFile&IsWater=1',
            fileManagerJson: '../../tools/upload_ajax.ashx?action=ManagerFile',
            allowFileManager: true
        });

    });
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
<div class="navigation"><a href="javascript:history.go(-1);" class="back">后退</a>首页 &gt; 会员管理 &gt; 邮件模板管理</div>
<div id="contentTab">
    <ul class="tab_nav">
        <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">编辑模板信息</a></li>
    </ul>

    <div class="tab_con" style="display:block;">
        <table class="form_table">
            <col width="150px"><col>
            <tbody>
            <tr>
                <th>模板名称：</th>
                <td><asp:TextBox ID="txtTitle" runat="server" CssClass="txtInput normal required" maxlength="100" /><label>*该模板的名称</label></td>
            </tr>
            <tr>
                <th>调用别名：</th>
                <td><asp:TextBox ID="txtCallIndex" runat="server" CssClass="txtInput normal required" maxlength="50"></asp:TextBox><label>该模板的调用别名，只允许字母、数字、下划线</label></td>
            </tr>
            <tr>
                <th>邮件标题：</th>
                <td><asp:TextBox ID="txtMailTitle" runat="server" CssClass="txtInput normal required" maxlength="100" /><label>*该模板的邮件标题</label></td>
            </tr>
            <tr>
                <th valign="top">邮件内容：</th>
                <td>
                    <textarea id="txtContent" cols="100" rows="8" style="width:99%;height:350px;visibility:hidden;" runat="server"></textarea>
                </td>
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
