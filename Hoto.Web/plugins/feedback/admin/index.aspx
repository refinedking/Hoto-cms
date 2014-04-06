<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="DTcms.Web.Plugin.Feedback.admin.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>在线留言管理</title>
<link type="text/css" rel="stylesheet" href="../../../scripts/ui/skins/Aqua/css/ligerui-all.css" />
<link type="text/css" rel="stylesheet" href="../../../<%=siteConfig.webmanagepath %>/images/style.css" />
<link type="text/css" rel="stylesheet" href="../../../css/pagination.css" />
<script type="text/javascript" src="../../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../../scripts/ui/js/ligerBuild.min.js"></script>
<script type="text/javascript" src="../../../<%=siteConfig.webmanagepath %>/js/function.js"></script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
    <div class="navigation">首页 &gt; 插件管理 &gt; 在线留言</div>
    <div class="tools_box">
	    <div class="tools_bar">
            <div class="search_box">
			    <asp:TextBox ID="txtKeywords" runat="server" CssClass="txtInput"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="搜 索" CssClass="btnSearch" />
		    </div>
		    <a href="javascript:void(0);" onclick="checkAll(this);" class="tools_btn"><span><b class="all">全选</b></span></a>
            <asp:LinkButton ID="lbtnUnLock" runat="server" CssClass="tools_btn"
                OnClientClick="return ExePostBack('lbtnUnLock','审核后前台将显示该信息，确定继续吗？');" onclick="lbtnUnLock_Click"><span><b class="stop">批量审核</b></span></asp:LinkButton>
            <asp:LinkButton ID="btnDelete" runat="server" CssClass="tools_btn"  
                OnClientClick="return ExePostBack('btnDelete');" onclick="btnDelete_Click"><span><b class="delete">批量删除</b></span></asp:LinkButton>
        </div>
    </div>

    <!--列表展示.开始-->
    <asp:Repeater ID="rptList" runat="server">
    <HeaderTemplate>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th width="6%">选择</th>
        <th align="left">标题</th>
        <th width="16%" align="left">用户</th>
        <th width="16%" align="left">发布时间</th>
        <th width="80">状态</th>
        <th width="8%">操作</th>
      </tr>
    </HeaderTemplate>
    <ItemTemplate>
      <tr>
        <td align="center"><asp:CheckBox ID="chkId" CssClass="checkall" runat="server" /><asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" /></td>
        <td><%# Convert.ToInt32(Eval("is_lock")) == 1 ? "<img src=\"../../../" + siteConfig.webmanagepath + "/images/icon_audit.gif\" title=\"未审核\" />" : ""%> <a href="reply.aspx?id=<%#Eval("id")%>"><%#Eval("title")%></a></td>
        <td><%#Eval("user_name")%></td>
        <td><%#string.Format("{0:g}",Eval("add_time"))%></td>
        <td align="center">
          <%#Eval("reply_content").ToString() == "" ? "未回复" : "已回复"%>
        </td>
        <td align="center"><a href="reply.aspx?id=<%#Eval("id")%>">回复</a></td>
      </tr>
    </ItemTemplate>
    <FooterTemplate>
      <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"7\">暂无记录</td></tr>" : ""%>
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
