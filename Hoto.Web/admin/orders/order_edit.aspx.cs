using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DTcms.Common;

namespace DTcms.Web.admin.orders
{
    public partial class order_edit : Web.UI.ManagePage
    {
        private int id = 0;
        protected Model.orders model = new Model.orders();
        protected Model.users userModel = new Model.users();
        protected Model.user_groups groupModel = new Model.user_groups();
        protected Model.payment payModel = new Model.payment();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.id = DTRequest.GetQueryInt("id");
            if (this.id == 0)
            {
                JscriptMsg("传输参数不正确！", "back", "Error");
                return;
            }
            if (!new BLL.orders().Exists(this.id))
            {
                JscriptMsg("信息不存在或已被删除！", "back", "Error");
                return;
            }
            if (!Page.IsPostBack)
            {
                ShowInfo(this.id);
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.orders bll = new BLL.orders();
            model = bll.GetModel(_id);
            payModel = new BLL.payment().GetModel(model.payment_id);
            userModel = new BLL.users().GetModel(model.user_id);
            if (userModel != null)
            {
                groupModel = new BLL.user_groups().GetModel(userModel.group_id);
            }
            if (payModel == null)
            {
                payModel = new Model.payment();
            }
            this.rptList.DataSource = model.order_goods;
            this.rptList.DataBind();
            //订单状态
            if (model.status == 1)
            {
                if (payModel != null && payModel.type == 1)
                {
                    if (model.payment_status > 1)
                    {
                        this.lbtnConfirm.Enabled = true;
                    }
                }
                else
                {
                    this.lbtnConfirm.Enabled = true;
                }
            }
            else if (model.status == 2 && model.distribution_status == 1)
            {
                this.lbtnSend.Enabled = true;
            }
            else if (model.status == 2 && model.distribution_status == 2)
            {
                this.lbtnComplete.Enabled = true;
            }
            if (model.status < 3)
            {
                this.btnCancel.Visible = true;
            }
            //如果订单为已完成时，不能取消订单
            if (model.status == 3)
            {
                this.btnInvalid.Visible = true;
            }
        }
        #endregion

        //确认订单
        protected void lbtnConfirm_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("orders", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            BLL.orders bll = new BLL.orders();
            Model.orders model = bll.GetModel(this.id);
            //检查订单状态
            if (model == null || model.status > 1)
            {
                JscriptMsg("订单不符合要求，无法确认！", "", "Error");
                return;
            }
            //检查支付方式
            Model.payment payModel = new BLL.payment().GetModel(model.payment_id);
            if (payModel == null)
            {
                JscriptMsg("支付方式不存在，无法确认！", "", "Error");
                return;
            }
            //如果支付方式为线上支付，则检查付款状态
            if (payModel.type == 1)
            {
                if (model.payment_status != 2)
                {
                    JscriptMsg("订单未付款，无法确认！", "", "Error");
                    return;
                }
            }
            bll.UpdateField(this.id, "status=2,confirm_time='" + DateTime.Now + "'");
            JscriptMsg("订单确认成功！", "order_edit.aspx?id=" + this.id, "Success");
        }

        //商家发货
        protected void lbtnSend_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("orders", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            BLL.orders bll = new BLL.orders();
            Model.orders model = bll.GetModel(this.id);
            //检查订单状态
            if (model == null || model.status != 2)
            {
                JscriptMsg("订单不符合要求，无法发货！", "", "Error");
                return;
            }
            //检查支付方式
            Model.payment payModel = new BLL.payment().GetModel(model.payment_id);
            if (payModel == null)
            {
                JscriptMsg("支付方式不存在，无法发货！", "", "Error");
                return;
            }
            //如果支付方式为线上支付，则检查付款状态
            if (payModel.type == 1)
            {
                if (model.payment_status != 2)
                {
                    JscriptMsg("订单未付款，无法发货！", "", "Error");
                    return;
                }
            }
            bll.UpdateField(this.id, "distribution_status=2,distribution_time='" + DateTime.Now + "'");
            JscriptMsg("订单发货成功啦！", "order_edit.aspx?id=" + this.id, "Success");
        }

        //完成订单
        protected void lbtnComplete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("orders", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            BLL.orders bll = new BLL.orders();
            Model.orders model = bll.GetModel(this.id);
            //检查订单状态
            if (model == null || model.status != 2 || model.distribution_status != 2)
            {
                JscriptMsg("订单不符合要求，无法发货！", "", "Error");
                return;
            }
            //检查支付方式
            Model.payment payModel = new BLL.payment().GetModel(model.payment_id);
            if (payModel == null)
            {
                JscriptMsg("支付方式不存在，无法完成订单！", "", "Error");
                return;
            }
            //增加积分/经验值
            if (model.point > 0)
            {
                new BLL.point_log().Add(model.user_id, model.user_name, model.point, "购物获得积分，订单号：" + model.order_no);
            }
            //如果配送方式为先款后货，则检查付款状态
            if (payModel.type == 2)
            {
                bll.UpdateField(this.id, "status=3,complete_time='" + DateTime.Now + "'," + "payment_status=2,payment_time='" + DateTime.Now + "'");
            }
            else
            {
                bll.UpdateField(this.id, "status=3,complete_time='" + DateTime.Now + "'");
            }
            JscriptMsg("订单已经完成啦！", "order_edit.aspx?id=" + this.id, "Success");
        }

        //取消订单
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("orders", DTEnums.ActionEnum.Cancel.ToString()); //检查权限
            BLL.orders bll = new BLL.orders();
            Model.orders model = bll.GetModel(this.id);
            if (model == null && model.status > 2)
            {
                JscriptMsg("订单不符合要求，无法取消！", "", "Error");
                return;
            }
            bll.UpdateField(this.id, "status=4");
            JscriptMsg("订单取消成功啦！", "order_edit.aspx?id=" + this.id, "Success");
        }

        //作废订单
        protected void btnInvalid_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("orders", DTEnums.ActionEnum.Invalid.ToString()); //检查权限
            BLL.orders bll = new BLL.orders();
            Model.orders model = bll.GetModel(this.id);
            if (model == null && model.status != 3)
            {
                JscriptMsg("订单未完成，无法作废！", "", "Error");
                return;
            }
            bll.UpdateField(this.id, "status=5");
            JscriptMsg("订单取消成功啦！", "order_edit.aspx?id=" + this.id, "Success");
        }

    }
}