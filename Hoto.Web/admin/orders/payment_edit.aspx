<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="payment_edit.aspx.cs" Inherits="DTcms.Web.admin.orders.payment_edit" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>编辑支付方式</title>
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
<div class="navigation"><a href="javascript:history.go(-1);" class="back">后退</a>首页 &gt; 销售管理 &gt; 编辑支付方式</div>
<div id="contentTab">
    <ul class="tab_nav">
        <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">编辑支付方式</a></li>
    </ul>
    <div class="tab_con" style="display:block;">
        <table class="form_table">
            <col width="180px"><col>
            <tbody>
            <tr>
                <th>支付名称：</th>
                <td><asp:TextBox ID="txtTitle" runat="server" CssClass="txtInput normal required" minlength="2" maxlength="100"></asp:TextBox><label>*</label></td>
            </tr>
            <tr>
                <th>支付类型：</th>
                <td>
                    <asp:RadioButtonList ID="rblType" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="1">线上 </asp:ListItem>
                        <asp:ListItem Value="2">线下</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <th>启用状态：</th>
                <td>
                    <asp:RadioButtonList ID="rblIsLock" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="0">启用</asp:ListItem>
                        <asp:ListItem Value="1">禁用</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <th>排序：</th>
                <td><asp:TextBox ID="txtSortId" runat="server" CssClass="txtInput small required digits" maxlength="10">99</asp:TextBox><label>*整形数字，越小越向前。</label></td>
            </tr>
            <tr>
                <th>手续费类型：</th>
                <td>
                    <asp:RadioButtonList ID="rblPoundageType" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="1">百分比 </asp:ListItem>
                        <asp:ListItem Selected="True" Value="2">固定金额</asp:ListItem>
                    </asp:RadioButtonList>
                    <label>*选择百分比的计算公式：商品总金额+(商品总金额*百分比)+配送费用=订单总金额</label>
                </td>
            </tr>
            <tr>
                <th>支付手续费：</th>
                <td><asp:TextBox ID="txtPoundageAmount" runat="server" CssClass="txtInput normal small required number">0</asp:TextBox><label>*注意：百分比取值范围：0-100，固定金额单位为“元”</label></td>
            </tr>
            <%if (model.api_path.ToLower() == "alipay")
              { %>
            <tr>
                <th>支付宝账号：</th>
                <td><asp:TextBox ID="txtAlipaySellerEmail" runat="server" CssClass="txtInput normal required"></asp:TextBox><label>*签约支付宝账号或卖家支付宝帐户</label></td>
            </tr>
            <tr>
                <th>合作者身份(partner ID)：</th>
                <td><asp:TextBox ID="txtAlipayPartner" runat="server" CssClass="txtInput normal required"></asp:TextBox><label>*合作身份者ID，以2088开头由16位纯数字组成的字符串</label></td>
            </tr>
            <tr>
                <th>交易安全校验码(key)：</th>
                <td><asp:TextBox ID="txtAlipayKey" runat="server" CssClass="txtInput normal required"></asp:TextBox><label>*交易安全检验码，由数字和字母组成的32位字符串</label></td>
            </tr>
            <%}
              else if (model.api_path.ToLower() == "tenpay")
              {%>
            <tr>
                <th>财付通商户号：</th>
                <td><asp:TextBox ID="txtTenpayBargainorId" runat="server" CssClass="txtInput normal required"></asp:TextBox><label>*</label></td>
            </tr>
            <tr>
                <th>财付通密钥：</th>
                <td><asp:TextBox ID="txtTenpayKey" runat="server" CssClass="txtInput normal required"></asp:TextBox><label>*</label></td>
            </tr>
            <%} %>
            <tr>
                <th>显示图标：</th>
                <td>
                    <asp:TextBox ID="txtImgUrl" runat="server" CssClass="txtInput normal left" maxlength="255"></asp:TextBox>
                    <a href="javascript:;" class="files"><input type="file" id="FileUpload" name="FileUpload" onchange="Upload('SingleFile', 'txtImgUrl', 'FileUpload');" /></a>
                    <span class="uploading">正在上传，请稍候...</span>
                </td>
            </tr>
            <tr>
                <th>描述：</th>
                <td><asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="small"></asp:TextBox>
                    <label>支持HTML格式，限500字符</label></td>
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
