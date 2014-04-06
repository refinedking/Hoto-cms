<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user_list.aspx.cs" Inherits="DTcms.Web.admin.users.user_list" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>会员管理</title>
<link type="text/css" rel="stylesheet" href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" />
<link type="text/css" rel="stylesheet" href="../images/style.css" />
<link type="text/css" rel="stylesheet" href="../../css/pagination.css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
<script type="text/javascript" src="../js/function.js"></script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
    <div class="navigation">首页 &gt; 会员管理 &gt; 会员列表</div>
    <div class="tools_box">
	    <div class="tools_bar">
            <div class="search_box">
			    <asp:TextBox ID="txtKeywords" runat="server" CssClass="txtInput"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="搜 索" CssClass="btnSearch" onclick="btnSearch_Click" />
		    </div>
            <a href="user_edit.aspx?action=<%=DTEnums.ActionEnum.Add %>" class="tools_btn"><span><b class="add">添加会员</b></span></a>
		    <a href="javascript:void(0);" onclick="checkAll(this);" class="tools_btn"><span><b class="all">全选</b></span></a>
            <asp:LinkButton ID="btnDelete" runat="server" CssClass="tools_btn"  
                OnClientClick="return ExePostBack('btnDelete');" onclick="btnDelete_Click"><span><b class="delete">批量删除</b></span></asp:LinkButton>
        </div>
        <div class="select_box">
            请选择：<asp:DropDownList ID="ddlGroupId" runat="server" CssClass="select2" AutoPostBack="True" onselectedindexchanged="ddlGroupId_SelectedIndexChanged"></asp:DropDownList>&nbsp;
	    </div>
    </div>

    <!--列表展示.开始-->
    <asp:Repeater ID="rptList" runat="server">
    <HeaderTemplate>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th width="6%">选择</th>
        <th align="left" colspan="2">用户名</th>
        <th width="12%" align="left">会员组</th>
        <th width="12%" align="left">邮箱</th>
        <th width="8%">余额</th>
        <th width="8%">积分</th>
        <th width="8%">状态</th>
        <th width="6%">操作</th>
      </tr>
    </HeaderTemplate>
    <ItemTemplate>
      <tr>
        <td align="center"><asp:CheckBox ID="chkId" CssClass="checkall" runat="server" /><asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" /></td>
        <td width="50">
          <a href="user_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>"><img width="48" height="48" src="<%#Eval("avatar").ToString() != "" ? Eval("avatar") : "../images/default_user_avatar.gif" %>" /></a>
        </td>
        <td class="user_box">
          <h4><b><%#Eval("user_name")%></b> (昵称：<%#Eval("nick_name")%>)</h4>
          <i>注册时间：<%#string.Format("{0:g}",Eval("reg_time"))%></i>
          <span>
              <a class="amount" href="javascript:parent.f_addTab('amount_log','会员消费记录','users/amount_log.aspx?keywords=<%#Eval("user_name")%>');" title="消费记录">余额</a>
              <a class="point" href="javascript:parent.f_addTab('point_log','会员积分记录','users/point_log.aspx?keywords=<%#Eval("user_name")%>');" title="积分记录">积分</a>
              <a class="msg" href="javascript:parent.f_addTab('user_message','会员短信管理','users/message_list.aspx?keywords=<%#Eval("user_name")%>');" title="短信息记录">短信息</a>
          </span>
        </td>
        <td><%#new DTcms.BLL.user_groups().GetTitle(Convert.ToInt32(Eval("group_id")))%></td>
        <td><%#Eval("email")%></td>
        <td align="center"><%#Eval("amount")%></td>
        <td align="center"><%#Eval("point")%></td>
        <td align="center"><%#GetUserStatus(Convert.ToInt32(Eval("is_lock")))%></td>
        <td align="center"><a href="user_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>">修改</a></td>
      </tr>
    </ItemTemplate>
    <FooterTemplate>
      <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"11\">暂无记录</td></tr>" : ""%>
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
