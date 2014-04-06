<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templet_list.aspx.cs" Inherits="DTcms.Web.admin.settings.templet_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>模板管理</title>
<link type="text/css" rel="stylesheet" href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" />
<link type="text/css" rel="stylesheet" href="../images/style.css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
<script type="text/javascript" src="../js/function.js"></script>
<script type="text/javascript">
    //放大图片
    $(function () {
        var x = 10;
        var y = 20;
        $(".imgtip").mouseover(function (e) {
            this.myTitle = this.title;
            this.title = "";
            var imgtip = "<div id='imgtip'><img src='" + $(this).attr("bigimg") + "' width='300' alt='预览图'/><\/div>"; //创建 div 元素
            $("body").append(imgtip); //把它追加到文档中						 
            $("#imgtip")
			    .css({
			        "top": (e.pageY + y) + "px",
			        "left": (e.pageX + x) + "px"
			    }).show("fast");   //设置x坐标和y坐标，并且显示
        }).mouseout(function () {
            this.title = this.myTitle;
            $("#imgtip").remove();  //移除 
        }).mousemove(function (e) {
            $("#imgtip")
			    .css({
			        "top": (e.pageY + y) + "px",
			        "left": (e.pageX + x) + "px"
			    });
        });
    });
</script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
    <div class="navigation">首页 &gt; 控制面板 &gt; 模板管理</div>
    <div class="tools_box">
	    <div class="tools_bar">
            <asp:LinkButton ID="lbtnStart" runat="server" CssClass="tools_btn" OnClientClick="return ExePostBack('lbtnStart', '启用该模板将会全部生成模板文件，确定继续吗？');" 
                onclick="lbtnStart_Click"><span><b class="import">启用模板</b></span></asp:LinkButton>
            <asp:LinkButton ID="lbtnRemark" runat="server" CssClass="tools_btn" OnClientClick="return ExePostBack('lbtnRemark', '将全部生成模板文件，可能比较耗时，确定要这样做吗？');" 
                onclick="lbtnRemark_Click"><span><b class="refresh">全部生成</b></span></asp:LinkButton>
            <asp:LinkButton ID="lbtnManage" runat="server" CssClass="tools_btn" 
                onclick="lbtnManage_Click"><span><b class="common">管理</b></span></asp:LinkButton>
        </div>
    </div>

    <asp:Repeater ID="rptList" runat="server">
    <HeaderTemplate>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th width="6%">选择</th>
        <th width="20%" align="left">模板名称</th>
        <th width="13%">作者</th>
        <th width="16%">创建日期</th>
        <th width="12%">版本号</th>
        <th align="left">适用版本</th>
        <th width="12%">状态</th>
      </tr>
    </HeaderTemplate>
    <ItemTemplate>
      <tr>
        <td align="center"><asp:CheckBox ID="chkId" CssClass="checkall" Checked='<%#Eval("skinname").ToString().ToLower() == siteConfig.templateskin ? true : false%>' runat="server" /><asp:HiddenField ID="hideSkinName" runat="server" Value='<%#Eval("skinname") %>' /></td>
        <td><%#Eval("name")%> <img src="../images/icon_view.gif" bigimg="<%#Eval("img")%>" title="查看预览图" class="imgtip" /></td>
        <td align="center"><%#Eval("author")%></td>
        <td align="center"><%#Eval("createdate")%></td>
        <td align="center"><%#Eval("version")%></td>
        <td><%#Eval("fordntver")%></td>
        <td align="center"><%#Eval("skinname").ToString().ToLower() == siteConfig.templateskin ? "<img src=\"../images/icon_correct.png\" title=\"正在使用中\" />" : "<img src=\"../images/icon_disable.png\" title=\"未启用\" />"%></td>
      </tr>
    </ItemTemplate>
    <FooterTemplate>
    </table>
    </FooterTemplate>
    </asp:Repeater>
    <div class="line10"></div>
</form>
</body>
</html>
