<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="DTcms.Web.admin.comment.list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>评论管理</title>
<link type="text/css" rel="stylesheet" href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" />
<link type="text/css" rel="stylesheet" href="../images/style.css" />
<link type="text/css" rel="stylesheet" href="../../css/pagination.css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
<script type="text/javascript" src="../js/function.js"></script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
    <div class="navigation">首页 &gt; 评论管理 &gt; 评论列表</div>
    <div class="tools_box">
	    <div class="tools_bar">
            <div class="search_box">
			    <asp:TextBox ID="txtKeywords" runat="server" CssClass="txtInput"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="搜 索" CssClass="btnSearch" onclick="btnSearch_Click" />
		    </div>
		    <a href="javascript:void(0);" onclick="checkAll(this);" class="tools_btn"><span><b class="all">全选</b></span></a>
            <asp:LinkButton ID="btnAudit" runat="server" CssClass="tools_btn" onclick="btnAudit_Click"><span><b class="stop">审核</b></span></asp:LinkButton>
            <asp:LinkButton ID="btnDelete" runat="server" CssClass="tools_btn"  
                OnClientClick="return ExePostBack('btnDelete');" onclick="btnDelete_Click"><span><b class="delete">批量删除</b></span></asp:LinkButton>
        </div>
        <div class="select_box">
            请选择：
            <asp:DropDownList ID="ddlProperty" runat="server" CssClass="select2" AutoPostBack="True" onselectedindexchanged="ddlProperty_SelectedIndexChanged">
                <asp:ListItem Value="" Selected="True">所有属性</asp:ListItem>
                <asp:ListItem Value="isLock">未审核</asp:ListItem>
                <asp:ListItem Value="unIsLock">已审核</asp:ListItem>
            </asp:DropDownList>
	    </div>
    </div>

    <!--列表展示.开始-->
    <asp:Repeater ID="rptList" runat="server">
    <HeaderTemplate>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
    </HeaderTemplate>
    <ItemTemplate>
      <tr>
        <td class="comment">
          <div class="title">
            <span class="note"><i><%#Eval("user_name")%></i><i><%#Eval("add_time")%></i><i class="reply"><a href="edit.aspx?id=<%#Eval("id")%>">回复</a></i></span>
            <b><asp:CheckBox ID="chkId" CssClass="checkall" runat="server" /><asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" /></b>
            <%#new DTcms.BLL.article().GetTitle(Convert.ToInt32(Eval("article_id")))%>
          </div>
          <div class="ask">
            <%#Convert.ToInt32(Eval("is_lock")) == 1 ? "<img src=\"../images/icon_audit.gif\" title=\"未审核\" />" : ""%><%#Eval("content")%>
            <%#Convert.ToInt32(Eval("is_reply")) == 1 ? "<div class=\"answer\">" +
                "<b>管理员回复：</b>" + Eval("reply_content") + "<span class=\"time\">" + Eval("reply_time") + "</span></div>" : ""%>
            <!--
            <div class="answer">
                <b>管理员回复：</b><-%#Eval("reply_content")%->
                <span class="time"><-%#Eval("reply_time")%-></span>
            </div>
            -->
          </div>
        </td>
      </tr>
    </ItemTemplate>
    <FooterTemplate>
      <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\">暂无记录</td></tr>" : ""%>
      </table>
    </FooterTemplate>
    </asp:Repeater>
    <!--列表展示.结束-->

   
    <div class="line15"></div>
    <div class="page_box">
      <div id="PageContent" runat="server" class="flickr right"></div>
      <div class="left">
         显示<asp:TextBox ID="txtPageNum" runat="server" CssClass="txtInput2 small2" onkeypress="return (/[\d]/.test(String.fromCharCode(event.keyCode)));" 
             ontextchanged="txtPageNum_TextChanged" AutoPostBack="True"></asp:TextBox>条/页
      </div>
    </div>
    <div class="line10"></div>
</form>
</body>
</html>
