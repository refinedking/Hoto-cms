using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.orders
{
    public partial class order_list : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected int status;
        protected int payment_status;
        protected int distribution_status;
        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.status = DTRequest.GetQueryInt("status");
            this.payment_status = DTRequest.GetQueryInt("payment_status");
            this.distribution_status = DTRequest.GetQueryInt("distribution_status");
            this.keywords = DTRequest.GetQueryString("keywords");

            this.pageSize = GetPageSize(15); //每页数量
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("orders", DTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind("id>0" + CombSqlTxt(this.status, this.payment_status, this.distribution_status, this.keywords), "status asc,add_time desc,id desc");
            }
        }

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = DTRequest.GetQueryInt("page", 1);
            if (this.status > 0)
            {
                this.ddlStatus.SelectedValue = this.status.ToString();
            }
            if (this.payment_status > 0)
            {
                this.ddlPaymentStatus.SelectedValue = this.payment_status.ToString();
            }
            if (this.distribution_status > 0)
            {
                this.ddlDistributionStatus.SelectedValue = this.distribution_status.ToString();
            }
            this.txtKeywords.Text = this.keywords;
            BLL.orders bll = new BLL.orders();
            this.rptList.DataSource = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&distribution_status={2}&keywords={3}&page={4}",
                this.status.ToString(), this.payment_status.ToString(), this.distribution_status.ToString(), this.keywords, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(int _status, int _payment_status, int _distribution_status, string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            if (_status > 0)
            {
                strTemp.Append(" and status=" + _status);
            }
            if (_payment_status > 0)
            {
                strTemp.Append(" and payment_status=" + _payment_status);
            }
            if (_distribution_status > 0)
            {
                strTemp.Append(" and distribution_status=" + _distribution_status);
            }
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and (order_no like '%" + _keywords + "%' or user_name like '%" + _keywords + "%' or accept_name like '%" + _keywords + "%')");
            }
            return strTemp.ToString();
        }
        #endregion

        #region 返回用户每页数量=========================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("order_list_page_size"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        #region 返回订单状态=============================
        protected string GetOrderStatus(int _id)
        {
            string _title = "";
            Model.orders model = new BLL.orders().GetModel(_id);
            switch (model.status)
            {
                case 1:
                    _title = "等待确认";
                    Model.payment payModel = new BLL.payment().GetModel(model.payment_id);
                    if (payModel != null && payModel.type == 1)
                    {
                        if (model.payment_status > 1)
                        {
                            _title = "付款成功";
                        }
                        else
                        {
                            _title = "等待付款";
                        }
                    }
                    break;
                case 2:
                    if (model.distribution_status > 1)
                    {
                        _title = "已发货";
                    }
                    else
                    {
                        _title = "待发货";
                    }
                    break;
                case 3:
                    _title = "交易完成";
                    break;
                case 4:
                    _title = "订单取消";
                    break;
                case 5:
                    _title = "订单作废";
                    break;
            }

            return _title;
        }
        #endregion

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&distribution_status={2}&keywords={3}",
                this.status.ToString(), this.payment_status.ToString(), this.distribution_status.ToString(), txtKeywords.Text));
        }

        //订单状态
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&distribution_status={2}&keywords={3}",
                ddlStatus.SelectedValue, this.payment_status.ToString(), this.distribution_status.ToString(), this.keywords));
        }

        //支付状态
        protected void ddlPaymentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&distribution_status={2}&keywords={3}",
                this.status.ToString(), ddlPaymentStatus.SelectedValue, this.distribution_status.ToString(), this.keywords));
        }

        //发货状态
        protected void ddlDistributionStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&distribution_status={2}&keywords={3}",
                this.status.ToString(), this.payment_status.ToString(), ddlDistributionStatus.SelectedValue, this.keywords));
        }

        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("order_list_page_size", _pagesize.ToString(), 43200);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("user_list.aspx", "status={0}&payment_status={1}&distribution_status={2}&keywords={3}",
                this.status.ToString(), this.payment_status.ToString(), this.distribution_status.ToString(), this.keywords));
        }

        //取消订单
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("orders", DTEnums.ActionEnum.Cancel.ToString()); //检查权限
            BLL.orders bll = new BLL.orders();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    Model.orders model = bll.GetModel(id);
                    if (model != null && model.status == 1)
                    {
                        bll.UpdateField(id, "status=4");
                    }
                }
            }
            JscriptMsg("符合的订单已取消！", Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&distribution_status={2}&keywords={3}",
                this.status.ToString(), this.payment_status.ToString(), this.distribution_status.ToString(), this.keywords), "Success");
        }

        //作废订单
        protected void btnInvalid_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("orders", DTEnums.ActionEnum.Invalid.ToString()); //检查权限
            BLL.orders bll = new BLL.orders();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    Model.orders model = bll.GetModel(id);
                    if (model != null && model.status == 3)
                    {
                        bll.UpdateField(id, "status=5");
                    }
                }
            }
            JscriptMsg("符合的订单已作废！", Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&distribution_status={2}&keywords={3}",
                this.status.ToString(), this.payment_status.ToString(), this.distribution_status.ToString(), this.keywords), "Success");
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("orders", DTEnums.ActionEnum.Delete.ToString()); //检查权限
            BLL.orders bll = new BLL.orders();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    Model.orders model = bll.GetModel(id);
                    if (model != null && model.status == 4)
                    {
                        bll.Delete(id);
                    }
                }
            }
            JscriptMsg("符合的订单已删除！", Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&distribution_status={2}&keywords={3}",
                this.status.ToString(), this.payment_status.ToString(), this.distribution_status.ToString(), this.keywords), "Success");
        }


    }
}