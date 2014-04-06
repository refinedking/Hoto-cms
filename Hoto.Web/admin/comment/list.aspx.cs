using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DTcms.Common;

namespace DTcms.Web.admin.comment
{
    public partial class list : DTcms.Web.UI.ManagePage
    {
        protected int channel_id;
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected string property = string.Empty;
        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.channel_id = DTRequest.GetQueryInt("channel_id");
            this.keywords = DTRequest.GetQueryString("keywords");
            this.property = DTRequest.GetQueryString("property");

            this.pageSize = GetPageSize(15); //每页数量
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("sys_comment", DTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind("id>0" + CombSqlTxt(this.channel_id, this.keywords, this.property), "add_time desc");
            }
        }

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = DTRequest.GetQueryInt("page", 1);
            this.ddlProperty.SelectedValue = this.property;
            this.txtKeywords.Text = this.keywords;
            BLL.article_comment bll = new BLL.article_comment();
            this.rptList.DataSource = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("list.aspx", "channel_id={0}&keywords={1}&property={2}&page={3}",
                this.channel_id.ToString(), this.keywords, this.property, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(int _channel_id, string _keywords, string _property)
        {
            StringBuilder strTemp = new StringBuilder();
            if (_channel_id > 0)
            {
                strTemp.Append(" and channel_id=" + channel_id);
            }
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and (user_name like '%" + _keywords + "%' or title like '%" + _keywords + "%' or content like '%" + _keywords + "%')");
            }
            if (!string.IsNullOrEmpty(_property))
            {
                switch (_property)
                {
                    case "isLock":
                        strTemp.Append(" and is_lock=1");
                        break;
                    case "unIsLock":
                        strTemp.Append(" and is_lock=0");
                        break;
                }
            }
            return strTemp.ToString();
        }
        #endregion

        #region 返回评论每页数量=========================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("comment_page_size"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("list.aspx", "channel_id={0}&keywords={1}&property={2}}",
                this.channel_id.ToString(), txtKeywords.Text, this.property));
        }

        //筛选属性
        protected void ddlProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("list.aspx", "channel_id={0}&keywords={1}&property={2}",
               this.channel_id.ToString(), this.keywords, ddlProperty.SelectedValue));
        }

        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("comment_page_size", _pagesize.ToString(), 43200);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("list.aspx", "channel_id={0}&keywords={1}&property={2}",
            this.channel_id.ToString(), this.keywords, this.property));
        }

        //审核
        protected void btnAudit_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("sys_comment", DTEnums.ActionEnum.Audit.ToString()); //检查权限
            BLL.article_comment bll = new BLL.article_comment();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    bll.UpdateField(id, "is_lock=0");
                }
            }
            JscriptMsg("审核通过成功啦！", Utils.CombUrlTxt("list.aspx", "channel_id={0}&keywords={1}&property={2}",
                this.channel_id.ToString(), this.keywords, this.property), "Success");
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("sys_comment", DTEnums.ActionEnum.Delete.ToString()); //检查权限
            BLL.article_comment bll = new BLL.article_comment();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    bll.Delete(id);
                }
            }
            JscriptMsg("批量删除成功啦！", Utils.CombUrlTxt("list.aspx", "channel_id={0}&keywords={1}&property={2}",
                this.channel_id.ToString(), this.keywords, this.property), "Success");
        }

    }
}