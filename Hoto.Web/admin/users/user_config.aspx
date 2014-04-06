<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user_config.aspx.cs" Inherits="DTcms.Web.admin.users.user_config" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>用户参数配置</title>
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
            invalidHandler: function (e, validator) {
                parent.jsprint("有 " + validator.numberOfInvalids() + " 项填写有误，请检查！", "", "Warning");
            },
            errorPlacement: function (lable, element) {
                //可见元素显示错误提示
                if (element.parents(".tab_con").css('display') != 'none') {
                    element.ligerTip({ content: lable.html(), appendIdTo: lable });
                }
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
<div class="navigation">首页 &gt; 用户管理 &gt; 用户参数配置</div>
<div id="contentTab">
    <ul class="tab_nav">
        <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">基本参数设置</a></li>
        <li><a onclick="tabs('#contentTab',1);" href="javascript:void(0);">用户积分策略</a></li>
    </ul>

    <div class="tab_con" style="display:block;">
        <table class="form_table">
            <col width="180px"><col>
            <tbody>
            <tr>
                <th>新用户注册设置：</th>
                <td>
                    <asp:RadioButtonList ID="regstatus" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="1">开放注册 </asp:ListItem>
                        <asp:ListItem Value="0">不允许注册 </asp:ListItem>
                        <asp:ListItem Value="2">仅通过邀请注册</asp:ListItem>
                    </asp:RadioButtonList>
                    <label></label>
                </td>
            </tr>
            <tr>
                <th>用户名保留关健字：</th>
                <td><asp:TextBox ID="regkeywords" runat="server" CssClass="txtInput normal" maxlength="500"></asp:TextBox>
                    <label>以英文逗号“,”分隔开</label></td>
            </tr>
            <tr>
                <th>新用户注册验证：</th>
                <td>
                    <asp:RadioButtonList ID="regverify" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="0">无验证 </asp:ListItem>
                        <asp:ListItem Value="1">Email验证 </asp:ListItem>
                        <asp:ListItem Value="2">人工审核</asp:ListItem>
                    </asp:RadioButtonList>
                    <label></label>
                </td>
            </tr>
            <tr>
                <th>IP注册间隔限制：</th>
                <td><asp:TextBox ID="regctrl" runat="server" CssClass="txtInput small required digits" maxlength="10">0</asp:TextBox>小时<label>*同一IP的注册间隔，0为不限制。</label></td>
            </tr>
            <tr>
                <th>Email验证请求有效期：</th>
                <td><asp:TextBox ID="regemailexpired" runat="server" CssClass="txtInput small required digits" maxlength="10">0</asp:TextBox>天<label>*新用户注册Email验证有效期，0为不限制。</label></td>
            </tr>
            <tr>
                <th>同一Email注册不同用户：</th>
                <td>
                    <asp:RadioButtonList ID="regemailditto" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="1">允许</asp:ListItem>
                        <asp:ListItem Value="0">不允许 </asp:ListItem>
                    </asp:RadioButtonList>
                    <label></label>
                </td>
            </tr>
            <tr>
                <th>允许Email登录：</th>
                <td>
                    <asp:RadioButtonList ID="emaillogin" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="1">允许</asp:ListItem>
                        <asp:ListItem Value="0">不允许 </asp:ListItem>
                    </asp:RadioButtonList>
                    <label></label>
                </td>
            </tr>
            <tr>
                <th>注册许可协议：</th>
                <td>
                    <asp:RadioButtonList ID="regrules" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="0">关闭 </asp:ListItem>
                        <asp:ListItem Value="1">开启</asp:ListItem>
                    </asp:RadioButtonList>
                    <label></label>
                </td>
            </tr>
            <tr>
                <th>许可协议内容：</th>
                <td><asp:TextBox ID="regrulestxt" runat="server" TextMode="MultiLine" CssClass="small"></asp:TextBox>
                    <label>支持HTML格式</label></td>
            </tr>
            <tr>
                <th>注册欢迎短信息：</th>
                <td>
                    <asp:RadioButtonList ID="regmsgstatus" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="0">关闭 </asp:ListItem>
                        <asp:ListItem Value="1">开启</asp:ListItem>
                    </asp:RadioButtonList>
                    <label></label>
                </td>
            </tr>
            <tr>
                <th>欢迎短信息内容：</th>
                <td><asp:TextBox ID="regmsgtxt" runat="server" TextMode="MultiLine" CssClass="small"></asp:TextBox><label>支持HTML格式</label></td>
            </tr>
            </tbody>
        </table>
    </div>

    <div class="tab_con">
        <table class="form_table">
            <col width="180px"><col>
            <tbody>
            <tr>
                <th>邀请码使用期限：</th>
                <td><asp:TextBox ID="invitecodeexpired" runat="server" CssClass="txtInput small required digits" maxlength="10">0</asp:TextBox>天<label>*邀请码有效天数，0为不限制。</label></td>
            </tr>
            <tr>
                <th>邀请码可使用次数：</th>
                <td><asp:TextBox ID="invitecodecount" runat="server" CssClass="txtInput small required digits" maxlength="10">0</asp:TextBox>次<label>*邀请码使用次数，0为不限制。</label></td>
            </tr>
            <tr>
                <th>每天可申请邀请码数量：</th>
                <td><asp:TextBox ID="invitecodenum" runat="server" CssClass="txtInput small required digits" maxlength="10">0</asp:TextBox>个<label>*每天可申请邀请码数量，0为不限制。</label></td>
            </tr>
            <tr>
                <th>现金/积分兑换比例：</th>
                <td><asp:TextBox ID="pointcashrate" runat="server" CssClass="txtInput small required number" maxlength="10">0</asp:TextBox>个<label>*1元等于多少个积分，0为禁用兑换功能。</label></td>
            </tr>
            <tr>
                <th>邀请注册获得积分：</th>
                <td><asp:TextBox ID="pointinvitenum" runat="server" CssClass="txtInput small required digits" maxlength="10">0</asp:TextBox>分<label>*邀请一个注册成功用户所获得的积分。</label></td>
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
