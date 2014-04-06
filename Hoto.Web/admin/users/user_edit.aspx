<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user_edit.aspx.cs" Inherits="DTcms.Web.admin.users.user_edit" ValidateRequest="false" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>编辑会员信息</title>
<link href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
<link href="../images/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../scripts/jquery/jquery.form.js"></script>
<script type="text/javascript" src="../../scripts/jquery/jquery.validate.min.js"></script> 
<script type="text/javascript" src="../../scripts/jquery/messages_cn.js"></script>
<script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
<script type="text/javascript" src="../js/function.js"></script>
<script type="text/javascript">
    //表单验证
    $(function () {
        $("#form1").validate({
            invalidHandler: function (e, validator) {
                parent.jsprint("有 " + validator.numberOfInvalids() + " 项填写有误，请检查！", "", "Warning");
            },
            errorPlacement: function (lable, element) {
                //可见元素显示错误提示
                if (element.parents(".tab_con").css('display') != 'none') {
                    element.ligerTip({ content: lable.html(), appendIdTo: lable });
                }
            },
            rules: {
                txtUserName: {
                    required: true,
                    minlength: 2,
                    maxlength: 100,
                    remote: {
                        type: "post",
                        url: "../../tools/admin_ajax.ashx?action=validate_username",
                        data: {
                            username: function () {
                                return $("#txtUserName").val();
                            },
                            oldusername: function () {
                                return $("#hidUserName").val();
                            }
                        },
                        dataType: "html",
                        dataFilter: function (data, type) {
                            if (data == "true")
                                return true;
                            else
                                return false;
                        }
                    }
                }
            },
            success: function (lable) {
                lable.ligerHideTip();
            },
            messages: {
                txtUserName: {
                    required: "输入(2-100)位字符",
                    minlength: "必须大于2位字符",
                    maxlength: "必须小于100位字符",
                    remote: "抱歉，该用户名不可用"
                }
            }
        });
    });
</script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
<div class="navigation"><a href="javascript:history.go(-1);" class="back">后退</a>首页 &gt; 会员管理 &gt; 编辑会员信息</div>
<div id="contentTab">
    <ul class="tab_nav">
        <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">基本信息</a></li>
        <li><a onclick="tabs('#contentTab',1);" href="javascript:;">账户信息</a></li>
    </ul>

    <div class="tab_con" style="display:block;">
        <table class="form_table">
            <col width="150px"><col>
            <tbody>
            <tr>
                <th>所属组别：</th>
                <td><asp:DropDownList id="ddlGroupId" CssClass="select2 required" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <th>用户状态：</th>
                <td>
                    <asp:RadioButtonList ID="rblIsLock" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="0">正常</asp:ListItem>
                        <asp:ListItem Value="1">待验证</asp:ListItem>
                        <asp:ListItem Value="2">待审核</asp:ListItem>
                        <asp:ListItem Value="3">禁用会员</asp:ListItem>
                    </asp:RadioButtonList>
                    <label>*禁用的会员将无法登录。</label>
                </td>
            </tr>
            <tr>
                <th>用户名：</th>
                <td>
                    <asp:HiddenField ID="hidUserName" runat="server" Value="" />
                    <asp:TextBox ID="txtUserName" runat="server" CssClass="txtInput normal" minlength="2" maxlength="100" /><label>*登录的用户名，支持中文。</label>
                </td>
            </tr>
            <tr>
                <th>密 码：</th>
                <td><asp:TextBox ID="txtPassword" runat="server" CssClass="txtInput normal required" minlength="6" maxlength="100" TextMode="Password"></asp:TextBox><label>*登录的密码，至少6位。</label></td>
            </tr>
            <tr>
                <th>邮 箱：</th>
                <td><asp:TextBox ID="txtEmail" runat="server" CssClass="txtInput normal required email" maxlength="100"></asp:TextBox><label>*取回密码时用到。</label></td>
            </tr>
            <tr>
                <th>昵 称：</th>
                <td><asp:TextBox ID="txtNickName" runat="server" CssClass="txtInput normal required" maxlength="100"></asp:TextBox><label>*会员的真实姓名或昵称</label></td>
            </tr>
            <tr>
                <th>头 像：</th>
                <td>
                    <asp:TextBox ID="txtAvatar" runat="server" CssClass="txtInput normal left"></asp:TextBox>
                    <a href="javascript:;" class="files"><input type="file" id="FileUpload" name="FileUpload" onchange="Upload('SingleFile', 'txtAvatar', 'FileUpload');" /></a>
                    <span class="uploading">正在上传，请稍候...</span>
                </td>
            </tr>
            <tr>
                <th>性 别：</th>
                <td>
                    <asp:RadioButtonList ID="rblSex" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="保密">保密 </asp:ListItem>
                        <asp:ListItem Value="男">男 </asp:ListItem>
                        <asp:ListItem Value="女">女</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <th>生 日：</th>
                <td><asp:TextBox ID="txtBirthday" runat="server" CssClass="txtInput normal dateISO" maxlength="20"></asp:TextBox><label></label></td>
            </tr>
            <tr>
                <th>电 话：</th>
                <td><asp:TextBox ID="txtTelphone" runat="server" CssClass="txtInput normal " maxlength="50"></asp:TextBox><label></label></td>
            </tr>
            <tr>
                <th>手 机：</th>
                <td><asp:TextBox ID="txtMobile" runat="server" CssClass="txtInput normal " maxlength="20"></asp:TextBox><label></label></td>
            </tr>
            <tr>
                <th>QQ号码：</th>
                <td><asp:TextBox ID="txtQQ" runat="server" CssClass="txtInput normal " maxlength="30"></asp:TextBox><label></label></td>
            </tr>
            <tr>
                <th>联系地址：</th>
                <td><asp:TextBox ID="txtAddress" runat="server" CssClass="txtInput normal " maxlength="255"></asp:TextBox><label></label></td>
            </tr>
            </tbody>
        </table>
    </div>
     <div class="tab_con">
        <table class="form_table">
            <col width="150px"><col>
            <tbody>
            <tr>
                <th>账户金额：</th>
                <td><asp:TextBox ID="txtAmount" runat="server" CssClass="txtInput small required number" maxlength="10">0</asp:TextBox>元<label>*</label></td>
            </tr>
            <tr>
                <th>账户积分：</th>
                <td><asp:TextBox ID="txtPoint" runat="server" CssClass="txtInput small required digits" maxlength="10">0</asp:TextBox>分<label>*积分也可做为交易。</label></td>
            </tr>
            <tr>
                <th>升级经验值：</th>
                <td><asp:TextBox ID="txtExp" runat="server" CssClass="txtInput small required digits" maxlength="10">0</asp:TextBox><label>*根据积分计算得来，与积分不同的是只增不减。</label></td>
            </tr>
            <tr>
                <th>注册时间：</th>
                <td><asp:Label ID="lblRegTime" Text="-" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th>注册IP：</th>
                <td><asp:Label ID="lblRegIP" Text="-" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th>最近登录时间：</th>
                <td><asp:Label ID="lblLastTime" Text="-" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th>最近登录IP：</th>
                <td><asp:Label ID="lblLastIP" Text="-" runat="server"></asp:Label></td>
            </tr>
            </tbody>
        </table>
    </div>

    <div class="foot_btn_box">
    <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btnSubmit" onclick="btnSubmit_Click" />
    &nbsp;<input name="重置" type="reset" class="btnSubmit" value="重 置" />
    </div>
</div>
</form>
</body>
</html>
