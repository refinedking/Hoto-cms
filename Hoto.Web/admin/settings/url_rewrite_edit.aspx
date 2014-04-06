<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="url_rewrite_edit.aspx.cs" Inherits="DTcms.Web.admin.settings.url_rewrite_edit" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>编辑伪静态URL替换规则</title>
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
            success: function(lable){
                lable.ligerHideTip();
            }
        });
    });
</script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
<div class="navigation"><a href="javascript:history.go(-1);" class="back">后退</a>首页 &gt; 控制面板 &gt; 编辑伪静态URL替换规则</div>
<div id="navtips" class="navtips">管理此页面需要具备正则表达式知识，否则请不要随意更改！<a href="javascript:CloseTip('navtips');" class="close">关闭</a></div>
<div id="contentTab">
    <ul class="tab_nav">
        <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">编辑URL重写规则</a></li>
    </ul>
    <div class="tab_con" style="display:block;">
        <table class="form_table">
            <col width="180px"><col>
            <tbody>
            <tr>
                <th>名称：</th>
                <td><asp:TextBox ID="txtName" runat="server" CssClass="txtInput normal required" minlength="2" maxlength="100"></asp:TextBox><label>*唯一标识名称，不可修改</label></td>
            </tr>
            <tr>
                <th>URL重写：</th>
                <td><asp:TextBox ID="txtPath" runat="server" CssClass="txtInput normal required"></asp:TextBox><label>*URL重写表达式，如：test/{0}.html</label></td>
            </tr>
            <tr>
                <th>正则表达式：</th>
                <td><asp:TextBox ID="txtPattern" runat="server" CssClass="txtInput normal required"></asp:TextBox><label>*匹配重写的正则表达式，如：test/(\d+).html$</label></td>
            </tr>
            <tr>
                <th>源页面：</th>
                <td><asp:TextBox ID="txtPage" runat="server" CssClass="txtInput normal required"></asp:TextBox><label>源aspx页面地址</label></td>
            </tr>
            <tr>
                <th>传输参数：</th>
                <td><asp:TextBox ID="txtQueryString" runat="server" CssClass="txtInput normal"></asp:TextBox><label>需要通过URL传输参数，如：page=$1</label></td>
            </tr>
            <tr>
                <th>模板文件：</th>
                <td><asp:TextBox ID="txtTemplet" runat="server" CssClass="txtInput normal"></asp:TextBox><label>所对应的模板文件名称</label></td>
            </tr>
            <tr>
                <th>所属频道：</th>
                <td><asp:TextBox ID="txtChannel" runat="server" CssClass="txtInput normal digits"></asp:TextBox><label>频道的ID；如果不是频道，默认0。</label></td>
            </tr>
            <tr>
                <th>频道类型：</th>
                <td>
                    <asp:DropDownList ID="ddlType" runat="server" CssClass="select2">
                        <asp:ListItem Value="">不属于任何类型</asp:ListItem>
                        <asp:ListItem Value="index">首页</asp:ListItem>
                        <asp:ListItem Value="list">列表页</asp:ListItem>
                        <asp:ListItem Value="detail">详细页</asp:ListItem>
                        <asp:ListItem Value="no">禁用重写</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>继承类名：</th>
                <td><asp:TextBox ID="txtInherit" runat="server" CssClass="txtInput normal"></asp:TextBox><label>该页面所对应的类名</label></td>
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
