<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="message_send.aspx.cs" Inherits="DTcms.Web.admin.users.message_send" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>编辑短信息</title>
<link href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
<link href="../images/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../scripts/jquery/jquery.form.js"></script>
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
<div class="navigation"><a href="javascript:history.go(-1);" class="back">后退</a>首页 &gt; 会员管理 &gt; 编辑短信息</div>
<div id="contentTab">
    <ul class="tab_nav">
        <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">发送短信息</a></li>
    </ul>

    <div class="tab_con" style="display:block;">
        <table class="form_table">
            <col width="150px"><col>
            <tbody>
            <tr>
                <th>收件人：</th>
                <td><asp:TextBox ID="txtUserName" runat="server" CssClass="txtInput normal required" /><label>*输入用户名，以英文逗号“,”分隔开。</label></td>
            </tr>
            <tr>
                <th>标 题：</th>
                <td><asp:TextBox ID="txtTitle" runat="server" CssClass="txtInput normal required" maxlength="100" /><label>*100个字符以内。</label></td>
            </tr>
            <tr>
                <th valign="top">内 容：</th>
                <td>
                    <textarea id="txtContent" cols="100" rows="8" style="width:99%;height:350px;visibility:hidden;" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <th></th>
                <td><asp:Button ID="btnSubmit" runat="server" Text="发送短信" CssClass="btnSubmit" onclick="btnSubmit_Click" /></td>
            </tr>
            </tbody>
        </table>
    </div>

</div>
</form>
</body>
</html>
