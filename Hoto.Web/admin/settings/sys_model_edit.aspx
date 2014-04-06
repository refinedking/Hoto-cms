<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sys_model_edit.aspx.cs" Inherits="DTcms.Web.admin.settings.sys_model_edit" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>编辑模型信息</title>
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
    //菜单事件处理
    $(function () {
        //初始化按钮事件
        $("#nav_box tr").each(function (i) {
            initButton(i);
        });
        //添加按钮(点击绑定)
        $("#navAddButton").click(function () {
            var navSize = $('#nav_box tr').size();
            var navRow = getTr(navSize);
            $("#nav_box").append(navRow);
            initButton(navSize);
        });
    });

    //表格行的菜单内容
    function getTr(indexValue) {
        var navRow = '<tr class="td_c">'
        + '<td><input name="nav_id" type="hidden" value="0" /><input name="nav_title" type="text" class="txtInput small" /></td>'
        + '<td><input name="nav_url" type="text" class="txtInput middle" /></td>'
        + '<td><input name="nav_sort" type="text" value="' + indexValue + '" class="txtInput" style="width:30px;" /></td>'
		+ '<td><img alt="删除" src="../images/icon_del.gif" class="operator" /></td>'
		+ '</tr>';
        return navRow;
    }

    //初始化按钮事件
    function initButton(indexValue) {
        //功能操作按钮
        $("#nav_box tr:eq(" + indexValue + ") .operator").each(function (i) {
            switch (i) {
                //删除                  
                case 0:
                    $(this).click(function () {
                        var obj = $(this);
                        $.ligerDialog.confirm("确定要删除吗？", "提示信息", function (result) {
                            if (result) {
                                obj.parent().parent().remove(); //删除节点
                            }
                        });
                    });
                    break;
            }
        });
    }

</script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
<div class="navigation"><a href="javascript:history.go(-1);" class="back">后退</a>首页 &gt; 控制面板 &gt; 系统模型</div>
<div id="contentTab">
    <ul class="tab_nav">
        <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">编辑模型信息</a></li>
    </ul>
    <div class="tab_con" style="display:block;">
        <table class="form_table">
            <col width="180px"><col>
            <tbody>
            <tr>
                <th>模型标题：</th>
                <td><asp:TextBox ID="txtTitle" runat="server" CssClass="txtInput normal required" minlength="2" maxlength="100"></asp:TextBox><label>*</label></td>
            </tr>
            <tr>
                <th>排 序：</th>
                <td><asp:TextBox ID="txtSortId" runat="server" CssClass="txtInput normal small required digits"></asp:TextBox><label>*</label></td>
            </tr>
            <tr>
                <th>首页类名：</th>
                <td><asp:TextBox ID="txtInheritIndex" runat="server" CssClass="txtInput normal"></asp:TextBox><label>非必填，前台模板生成的aspx文件继承的类名</label></td>
            </tr>
            <tr>
                <th>列表页类名：</th>
                <td><asp:TextBox ID="txtInheritList" runat="server" CssClass="txtInput normal"></asp:TextBox><label>非必填，前台模板生成的aspx文件继承的类名</label></td>
            </tr>
            <tr>
                <th>详细页类名：</th>
                <td><asp:TextBox ID="txtInheritDetail" runat="server" CssClass="txtInput normal"></asp:TextBox><label>非必填，前台模板生成的aspx文件继承的类名</label></td>
            </tr>
            <tr>
                <th>管理菜单：</th>
                <td><button id="navAddButton" type="button" class="btnSelect"><span class="add">添 加</span></button></td>
            </tr>
            <tr>
                <th></th>
                <td>
                    <table border="0" cellspacing="0" cellpadding="0" class="border_table">
                        <thead>
                        <tr>
                            <th width="120">标题</th>
                            <th width="230">管理文件路径</th>
                            <th width="50">排序</th>
                            <th width="50">操作</th>
                        </tr>
                        </thead>
                        <tbody id="nav_box">
                        <asp:Repeater ID="rptNavList" runat="server">
                        <ItemTemplate>
                        <tr class="td_c">
                            <td><input name="nav_id" type="hidden" value="<%#Eval("id") %>" /><input name="nav_title" type="text" value="<%#Eval("title")%>" class="txtInput small" /></td>
                            <td><input name="nav_url" type="text" value="<%#Eval("nav_url")%>" class="txtInput middle" /></td>
                            <td><input name="nav_sort" type="text" value="<%#Eval("sort_id")%>"  class="txtInput" style="width:30px;" /></td>
                            <td><img alt="删除" src="../images/icon_del.gif" class="operator" /></td>
                        </tr>
                        </ItemTemplate>
                        </asp:Repeater>
                        </tbody>
                    </table>
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
