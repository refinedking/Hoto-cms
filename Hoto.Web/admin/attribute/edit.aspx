<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="DTcms.Web.admin.attribute.edit" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>编辑属性信息</title>
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
    <div class="navigation"><a href="javascript:history.go(-1);" class="back">后退</a>首页 &gt; 扩展属性 &gt; 编辑属性</div>
<div id="contentTab">
    <ul class="tab_nav">
        <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">属性信息</a></li>
    </ul>
    <div class="tab_con" style="display:block;">
        <table class="form_table">
            <col width="180px"><col>
            <tbody>
            <tr>
                <th>属性名称：</th>
                <td><asp:TextBox ID="txtTitle" runat="server" CssClass="txtInput normal required" minlength="2" maxlength="100"></asp:TextBox><label>*</label></td>
            </tr>
            <tr>
                <th>备注说明：</th>
                <td><asp:TextBox ID="txtRemark" runat="server" maxlength="500" TextMode="MultiLine" CssClass="small required" /></td>
            </tr>
            <tr>
                <th>显示类型：</th>
                <td>
                    <asp:DropDownList id="ddlType" CssClass="select2 required" runat="server">
                        <asp:ListItem Value="">请选择类型...</asp:ListItem>
                        <asp:ListItem Value="0">输入框</asp:ListItem>
                        <asp:ListItem Value="1">下拉框</asp:ListItem>
                        <asp:ListItem Value="2">单选框</asp:ListItem>
                        <asp:ListItem Value="3">复选框</asp:ListItem>
                    </asp:DropDownList>
                    <label>*填写属性值时表示的方式</label>
                </td>
            </tr>
            <tr>
                <th>默认值：</th>
                <td><asp:TextBox ID="txtDefaultValue" runat="server" CssClass="txtInput normal" maxlength="500"></asp:TextBox><label>默认值可用“,”逗号分隔开</label></td>
            </tr>
            <tr>
                <th>排 序：</th>
                <td><asp:TextBox ID="txtSortId" runat="server" CssClass="txtInput normal small required digits">99</asp:TextBox><label>*</label></td>
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
