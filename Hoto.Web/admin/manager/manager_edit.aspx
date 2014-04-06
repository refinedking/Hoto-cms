<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="manager_edit.aspx.cs" Inherits="DTcms.Web.admin.manager.manager_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>编辑管理员</title>
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
<div class="navigation"><a href="javascript:history.go(-1);" class="back">后退</a>首页 &gt; 控制面板 &gt; 管理员管理</div>
<div id="contentTab">
    <ul class="tab_nav">
        <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">编辑管理员信息</a></li>
    </ul>
    <div class="tab_con" style="display:block;">
        <table class="form_table">
            <col width="180px"><col>
            <tbody>
            <tr>
                <th>管理角色：</th>
                <td>
                    <asp:DropDownList ID="ddlRoleId" runat="server" CssClass="select2 required"/>
                </td>
            </tr>
            <tr>
                <th>账户状态：</th>
                <td>
                    <asp:RadioButtonList ID="rblIsLock" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="0">启用 </asp:ListItem>
                        <asp:ListItem Value="1">禁用 </asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <th>用户名：</th>
                <td><asp:TextBox ID="txtUserName" runat="server" CssClass="txtInput normal required" minlength="2" maxlength="100"></asp:TextBox><label>*</label></td>
            </tr>
            <tr>
                <th>登录密码：</th>
                <td><asp:TextBox ID="txtUserPwd" runat="server" CssClass="txtInput normal required" 
                        minlength="2" maxlength="100" TextMode="Password"></asp:TextBox><label>*</label></td>
            </tr>
            <tr>
                <th>确认密码：</th>
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
