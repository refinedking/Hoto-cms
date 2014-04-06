<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="group_edit.aspx.cs" Inherits="DTcms.Web.admin.users.group_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>编辑会员组</title>
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
<div class="navigation"><a href="javascript:history.go(-1);" class="back">后退</a>首页 &gt; 会员管理 &gt; 编辑会员组</div>
<div id="contentTab">
    <ul class="tab_nav">
        <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">编辑会员组</a></li>
    </ul>
    <div class="tab_con" style="display:block;">
        <table class="form_table">
            <col width="180px"><col>
            <tbody>
            <tr>
                <th>组别名称：</th>
                <td><asp:TextBox ID="txtTitle" runat="server" CssClass="txtInput normal required" minlength="2" maxlength="100"></asp:TextBox><label>*</label></td>
            </tr>
            <tr>
                <th>是否隐藏：</th>
                <td>
                    <asp:RadioButtonList ID="rblIsLock" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="1">是</asp:ListItem>
                        <asp:ListItem Selected="True" Value="0">否</asp:ListItem>
                    </asp:RadioButtonList>
                    <label>*隐藏后，用户将无法升级或显示该组别。</label>
                </td>
            </tr>
            <tr>
                <th>注册默认会员组：</th>
                <td>
                    <asp:RadioButtonList ID="rblIsDefault" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="1">是</asp:ListItem>
                        <asp:ListItem Selected="True" Value="0">否</asp:ListItem>
                    </asp:RadioButtonList>
                    <label>*用户注册成功后自动默认为该会员组，如果存在多条，则以等级值最小的为准。</label>
                </td>
            </tr>
            <tr>
                <th>参与自动升级：</th>
                <td>
                    <asp:RadioButtonList ID="rblIsUpgrade" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="1">是</asp:ListItem>
                        <asp:ListItem Value="0">否</asp:ListItem>
                    </asp:RadioButtonList>
                    <label>*如果是否，在满足升级条件下系统则不会自动升级为该会员组。</label>
                </td>
            </tr>
            <tr>
                <th>等级值：</th>
                <td><asp:TextBox ID="txtGrade" runat="server" CssClass="txtInput normal small required digits"></asp:TextBox><label>*升级顺序，取值范围1-100，等级值越大，会员等级越高。</label></td>
            </tr>
            <tr>
                <th>升级所需积分：</th>
                <td><asp:TextBox ID="txtUpgradeExp" runat="server" CssClass="txtInput normal small required digits"></asp:TextBox><label>*自动升级所需要的积分。</label></td>
            </tr>
            <tr>
                <th>初始金额：</th>
                <td><asp:TextBox ID="txtAmount" runat="server" CssClass="txtInput normal small required number"></asp:TextBox><label>*自动到该会员组赠送的金额，负数则扣减。</label></td>
            </tr>
            <tr>
                <th>初始积分：</th>
                <td><asp:TextBox ID="txtPoint" runat="server" CssClass="txtInput normal small required number"></asp:TextBox><label>*自动到该会员组赠送的积分，负数则扣减。</label></td>
            </tr>
            <tr>
                <th>购物折扣：</th>
                <td><asp:TextBox ID="txtDiscount" runat="server" CssClass="txtInput normal small required number"></asp:TextBox><label>*购物享受的折扣，取值范围：1-100。</label></td>
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
