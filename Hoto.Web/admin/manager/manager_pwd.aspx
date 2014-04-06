<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="manager_pwd.aspx.cs" Inherits="DTcms.Web.admin.manager.manager_pwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>修改管理员密码</title>
<link href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
<link href="../images/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../scripts/jquery/jquery.validate.min.js"></script> 
<script type="text/javascript" src="../../scripts/jquery/messages_cn.js"></script>
<script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
<script type="text/javascript" src="../js/function.js"></script>
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
<div class="navigation">首页 &gt; 控制面板 &gt; 管理员管理</div>
<div id="contentTab">
    <ul class="tab_nav">
        <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">修改密码</a></li>
    </ul>
    <div class="tab_con" style="display:block;">
        <table class="form_table">
            <col width="180px"><col>
            <tbody>
            <tr>
                <th>用户名：</th>
                <td><asp:TextBox ID="txtUserName" runat="server" CssClass="txtInput normal required" minlength="2" maxlength="100" ReadOnly="true"/><label>*</label></td>
            </tr>
            <tr>
                <th>旧登录密码：</th>
                <td><asp:TextBox ID="txtOldPwd" runat="server" CssClass="txtInput normal required" 
                        minlength="2" maxlength="100" TextMode="Password"></asp:TextBox><label>*</label></td>
            </tr>
            <tr>
                <th>新登录密码：</th>
                <td><asp:TextBox ID="txtUserPwd" runat="server" CssClass="txtInput normal required" 
                        minlength="2" maxlength="100" TextMode="Password"></asp:TextBox><label>*</label></td>
            </tr>
            <tr>
                <th>确认新密码：</th>
                <td><asp:TextBox ID="txtUserPwd1" runat="server" CssClass="txtInput normal required" 
                        minlength="6" maxlength="100" TextMode="Password" equalTo="#txtUserPwd"></asp:TextBox><label>*</label></td>
            </tr>
            <tr>
                <th>姓名：</th>
                <td><asp:TextBox ID="txtRealName" runat="server" CssClass="txtInput normal required" minlength="2" maxlength="30"></asp:TextBox><label>*</label></td>
            </tr>
            <tr>
                <th>电话：</th>
                <td><asp:TextBox ID="txtTelephone" runat="server" CssClass="txtInput normal" minlength="2" maxlength="30"></asp:TextBox></td>
            </tr>
            <tr>
                <th>邮箱：</th>
                <td><asp:TextBox ID="txtEmail" runat="server" CssClass="txtInput normal email" minlength="2" maxlength="50"></asp:TextBox></td>
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
