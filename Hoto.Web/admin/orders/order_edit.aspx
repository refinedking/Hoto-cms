<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="order_edit.aspx.cs" Inherits="DTcms.Web.admin.orders.order_edit" ValidateRequest="false" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>编辑订单信息</title>
<link href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
<link href="../images/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
<script type="text/javascript" src="../js/function.js"></script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
<div class="navigation"><a href="javascript:history.go(-1);" class="back">后退</a>首页 &gt; 销售管理 &gt; 编辑订单信息</div>
<div id="contentTab">
    <ul class="tab_nav">
        <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">编辑订单信息</a></li>
    </ul>

    <div class="tab_con" style="display:block;">

        <!--订单状态操作.开始-->
        <div class="order_box">
            <h3>&gt;&gt;更改状态（订单号：<%=model.order_no %>）</h3>
            <%if (model.status < 4)
            { %>
            <div class="order_flow" style="width:460px;">
                <div class="order_flow_left">
                    <a title="订单已生成" class="order_flow_input">生成</a>
                    <span><p class="name">生成订单</p><p><%=model.add_time%></p></span>
                </div>
                <%if (payModel.type == 1)
                  { %>
                <%if (model.payment_status > 1)
                  { %>
                <div class="order_flow_arrive">
                    <a class="order_flow_input">支付</a>
                    <span><p class="name">已支付</p><p><%=model.payment_time%></p></span>
                </div>
                <%}
                  else
                  { %>
                <div class="order_flow_wait">
                    <a class="order_flow_input">支付</a>
                    <span><p class="name">等待支付</p></span>
                </div>
                <%} %>
                <%} %>
                <%if (model.status < 2 || model.confirm_time == null)
                  { %>
                <div class="order_flow_wait">
                    <asp:LinkButton ID="lbtnConfirm" runat="server" Text="确认" CssClass="order_flow_input" ToolTip="点击确认订单" Enabled="False" onclick="lbtnConfirm_Click" />
                    <span><p class="name">确认订单</p></span>
                </div>
                <%}
                  else
                  { %>
                <div class="order_flow_arrive">
                    <a title="订单已确认" class="order_flow_input">确认</a>
                    <span><p class="name">确认订单</p><p><%=model.confirm_time%></p></span>
                </div>
                <%} %>
                <%if (model.distribution_status < 2)
                  { %>
                <div class="order_flow_wait">
                    <asp:LinkButton ID="lbtnSend" runat="server" Text="发货" CssClass="order_flow_input" ToolTip="点击发货" Enabled="False" onclick="lbtnSend_Click" />
                    <span><p class="name">商家发货</p></span>
                </div>
                <%}
                  else
                  { %>
                <div class="order_flow_arrive">
                    <a title="订单已发货" class="order_flow_input">发货</a>
                    <span><p class="name">商家已发货</p><p><%=model.distribution_time%></p></span>
                </div>
                <%} %>
                <%if (model.status > 2)
                  { %>
                <div class="order_flow_right_arrive">
                    <a title="订单已完成" class="order_flow_input">完成</a>
                    <span><p class="name">订单已完成</p><p><%=model.complete_time%></p></span>
                </div>
                <%}
                  else
                  { %>
                <div class="order_flow_right_wait">
                    <asp:LinkButton ID="lbtnComplete" runat="server" Text="完成" CssClass="order_flow_input" ToolTip="点击完成订单" Enabled="False" onclick="lbtnComplete_Click" />
                    <span><p class="name">完成订单</p></span>
                </div>
                <%} %>
                <div class="clear"></div>
            </div>
            <%}
              else if (model.status == 4)
              { %>
              <div style="text-align:center;line-height:50px; font-size:20px; color:Red;">该订单已取消</div>
            <%}
              else if (model.status == 5)
              { %>
              <div style="text-align:center;line-height:50px; font-size:20px; color:Red;">该订单已作废</div>
            <%} %>
        </div>
        
        <!--订单状态操作.结束-->
        <div class="line10"></div>
        <asp:Repeater ID="rptList" runat="server">
        <HeaderTemplate>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="order_table" style="border-bottom:0;">
          <tr>
            <th align="left">商品名称</th>
            <th width="12%" align="left">销售价</th>
            <th width="12%" align="left">优惠价</th>
            <th width="10%" align="left">积分</th>
            <th width="10%" align="left">数量</th>
            <th width="12%" align="left">金额合计</th>
            <th width="12%" align="left">积分合计</th>
          </tr>
          </HeaderTemplate>
          <ItemTemplate>
          <tr>
            <td><%#Eval("goods_name")%></td>
            <td><%#Eval("goods_price")%></td>
            <td><%#Eval("real_price")%></td>
            <td><%#Convert.ToInt32(Eval("point")) > 0 ? "+" + Eval("point").ToString() : Eval("point").ToString()%></td>
            <td><%#Eval("quantity")%></td>
            <td><%#Convert.ToDecimal(Eval("real_price"))*Convert.ToInt32(Eval("quantity"))%></td>
            <td><%#Convert.ToInt32(Eval("point")) * Convert.ToInt32(Eval("quantity"))%></td>
          </tr>
          </ItemTemplate>
          <FooterTemplate>
            <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"5\">暂无记录</td></tr>" : ""%>
          </table>
         </FooterTemplate>
         </asp:Repeater>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="order_table" style="border-bottom:0;">
          <tr>
            <th width="50%" colspan="2">&gt;&gt;收货人信息</th>
            <th width="50%" colspan="2">&gt;&gt;会员信息</th>
          </tr>
          <tr>
            <td width="5%" class="col">收货人：</td>
            <td><%=model.accept_name %></td>
            <td width="5%" class="col">会员账户：</td>
            <td><%=model.user_name %></td>
          </tr>
          <tr>
            <td class="col">收货地址：</td>
            <td><%=model.address %></td>
            <td class="col">会员组别：</td>
            <td><%=groupModel.title %></td>
          </tr>
          <tr>
            <td class="col">邮编：</td>
            <td><%=model.post_code %></td>
            <td class="col">购物折扣：</td>
            <td><%=groupModel.discount %>%</td>
          </tr>
          <tr>
            <td class="col">手机：</td>
            <td><%=model.mobile %></td>
            <td class="col">账户余额：</td>
            <td><%=userModel.amount %></td>
          </tr>
          <tr>
            <td class="col">电话：</td>
            <td><%=model.telphone %></td>
            <td class="col">账户积分：</td>
            <td><%=userModel.point %>分</td>
          </tr>
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="order_table">
          <tr>
            <th width="50%" colspan="2">&gt;&gt;支付配送信息</th>
            <th width="50%" colspan="2">&gt;&gt;订单统计信息</th>
          </tr>
          <tr>
            <td width="5%" class="col">支付方式：</td>
            <td><%=new DTcms.BLL.payment().GetTitle(model.payment_id) %></td>
            <td width="5%" class="col">商品总金额：</td>
            <td><%=model.real_amount %></td>
          </tr>
          <tr>
            <td class="col">付款状态：</td>
            <td>
              <%if (payModel.type == 1)
                { %>
                <%=model.payment_status == 2 ? "已支付" : "未支付" %>
              <%}
                else
                { %>
                线下支付
              <%} %>
            </td>
            <td class="col">配送费用：</td>
            <td><%=model.real_freight %></td>
          </tr>
          <tr>
            <td class="col">配送方式：</td>
            <td><%=new DTcms.BLL.distribution().GetTitle(model.distribution_id) %></td>
            <td class="col">支付手续费：</td>
            <td><%=model.payment_fee %></td>
          </tr>
          <tr>
            <td class="col">发货状态：</td>
            <td><%=model.distribution_status == 2 ? "已发货" : "未发货" %></td>
            <td class="col">积分总额：</td>
            <td><%=model.point > 0 ? "+" + model.point.ToString() : model.point.ToString()%></td>
          </tr>
          <tr>
            <td class="col">用户留言：</td>
            <td><%=model.message %></td>
            <td class="col">订单总金额：</td>
            <td><%=model.order_amount %></td>
          </tr>
        </table>
        
         <div class="line10"></div>

    </div>
    <div class="foot_btn_box">
        <asp:Button ID="btnCancel" runat="server" Text="取消订单" CssClass="btnSubmit" Visible="false" onclick="btnCancel_Click" />&nbsp;
        <asp:Button ID="btnInvalid" runat="server" Text="作废订单" CssClass="btnSubmit" Visible="false" onclick="btnInvalid_Click" />&nbsp;
        <input type="button" id="btnPrint" value="打印订单" class="btnSubmit" onclick="parent.openDialog('商品打印预览', 'orders/order_print.aspx?id=<%=model.id %>', 850, 500);printview();" />&nbsp;
    </div>
</div>
</form>
</body>
</html>
