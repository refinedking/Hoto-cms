<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="DTcms.Web.admin.download.edit" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>编辑下载信息</title>
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
    //删除附件Li节点
    function DelAttachLi(obj) {
        $.ligerDialog.confirm("确定要删除吗？", "提示信息", function (result) {
            if (result) {
                $(obj).parent().remove(); //删除节点
            }
        });
    }
</script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
<div class="navigation"><a href="javascript:history.go(-1);" class="back">后退</a>首页 &gt; 下载管理 &gt; 编辑信息</div>
<div id="contentTab">
    <ul class="tab_nav">
        <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">基本信息</a></li>
        <li><a onclick="tabs('#contentTab',1);" href="javascript:;">详细描述</a></li>
        <li><a onclick="tabs('#contentTab',2);" href="javascript:;">SEO选项</a></li>
    </ul>

    <div class="tab_con" style="display:block;">
        <table class="form_table">
            <col width="150px"><col>
            <tbody>
            <tr>
                <th>所属类别：</th>
                <td><asp:DropDownList id="ddlCategoryId" CssClass="select2 required" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <th>标题名称：</th>
                <td><asp:TextBox ID="txtTitle" runat="server" CssClass="txtInput normal required" maxlength="100" /></td>
            </tr>
            <tr>
                <th>推荐类型：</th>
                <td>
                    <asp:CheckBoxList ID="cblItem" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="1">允许评论</asp:ListItem>
                        <asp:ListItem Value="1">推荐</asp:ListItem>
                        <asp:ListItem Value="1">隐藏</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <th>排序数字：</th>
                <td><asp:TextBox ID="txtSortId" runat="server" CssClass="txtInput small required digits" maxlength="10">99</asp:TextBox></td>
            </tr>
            <tr>
                <th>浏览次数：</th>
                <td><asp:TextBox ID="txtClick" runat="server" CssClass="txtInput small required digits" maxlength="10">0</asp:TextBox></td>
            </tr>
            <tr>
                <th valign="top" style="padding-top:10px;">上传附件：</th>
                <td>
                    <a href="javascript:;" class="files"><input type="file" id="FileUpload2" name="FileUpload2" onchange="AttachUpload('AttachList','FileUpload2');" /></a>
                    <span class="uploading">正在上传，请稍候...</span>
                </td>
            </tr>
            <tr>
                <th></th>
                <td>
                    <div id="AttachList" class="attach_list">
                      <ul>
                        <asp:Repeater ID="rptAttach" runat="server">
                        <ItemTemplate>
                        <li>
                          <input name="hidFileName" type="hidden" value="<%#Eval("id")%>|<%#Eval("title")%>|<%#Eval("file_path")%>" />
                          <b class="close" title="删除" onclick="DelAttachLi(this);"></b>
                          <span class="right">下载积分：<input name="txtPoint" type="text" class="input2" value="<%#Eval("point") %>" onkeydown="return checkNumber(event);" /></span>
                          <span class="title">附件：<%#Eval("title")%></span>
                          <span><%#Convert.ToInt32(Eval("file_size")) < 1024 ? Eval("file_size").ToString() + "KB" : Convert.ToInt32(Eval("file_size"))/1024 + "MB"%></span>
                          <span>人气：<%#Eval("down_num")%></span>
                          <a href="javascript:;" class="upfile"><input type="file" name="FileUpdate" onchange="AttachUpdate('hidFileName',this);" /></a>
                          <span class="uploading">正在更新...</span>
                        </li>
                        </ItemTemplate>
                        </asp:Repeater>
                      </ul>
                    </div>
                </td>
            </tr>
            </tbody>
        </table>
    </div>

    <div class="tab_con">
        <table class="form_table">
            <col width="150px"><col>
            <tbody>
            <tr>
                <th>赞成人数：</th>
                <td><asp:TextBox ID="txtDiggGood" runat="server" CssClass="txtInput small required digits" maxlength="10">0</asp:TextBox></td>
            </tr>
            <tr>
                <th>反对人数：</th>
                <td><asp:TextBox ID="txtDiggBad" runat="server" CssClass="txtInput small required digits" maxlength="10">0</asp:TextBox></td>
            </tr>
            <tr>
                <th>URL链接：</th>
                <td><asp:TextBox ID="txtLinkUrl" runat="server" CssClass="txtInput normal" maxlength="255"></asp:TextBox><label>URL跳转地址</label></td>
            </tr>
            <tr>
                <th>显示图片：</th>
                <td>
                    <asp:TextBox ID="txtImgUrl" runat="server" CssClass="txtInput normal left" maxlength="255"></asp:TextBox>
                    <a href="javascript:;" class="files"><input type="file" id="FileUpload" name="FileUpload" onchange="Upload('SingleFile', 'txtImgUrl', 'FileUpload');" /></a>
                    <span class="uploading">正在上传，请稍候...</span>
                </td>
            </tr>
            <tr>
                <th valign="top">详细描述：</th>
                <td>
                    <textarea id="txtContent" cols="100" rows="8" style="width:99%;height:350px;visibility:hidden;" runat="server"></textarea>
                </td>
            </tr>
            </tbody>
        </table>
    </div>

    <div class="tab_con">
        <table class="form_table">
            <col width="150px"><col>
            <tbody>
            <tr>
                <th>SEO标题：</th>
                <td><asp:TextBox ID="txtSeoTitle" runat="server" maxlength="255" CssClass="txtInput normal" /></td>
            </tr>
            <tr>
                <th>SEO关健字：</th>
                <td><asp:TextBox ID="txtSeoKeywords" runat="server" maxlength="255" TextMode="MultiLine" CssClass="small" /></td>
            </tr>
            <tr>
                <th>SEO描述：</th>
                <td><asp:TextBox ID="txtSeoDescription" runat="server" maxlength="255" TextMode="MultiLine" CssClass="small" /></td>
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
