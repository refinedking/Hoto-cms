using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DTcms.Common;

namespace DTcms.Web.admin.users
{
    public partial class oauth_list : Web.UI.ManagePage
    {
        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.keywords = DTRequest.GetQueryString("keywords");
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("app_oauth", DTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind("id>0" + CombSqlTxt(this.keywords), "sort_id asc,id desc");
            }
        }

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.txtKeywords.Text = this.keywords;
            BLL.app_oauth bll = new BLL.app_oauth();
            this.rptList.DataSource = bll.GetList(0, _strWhere, _orderby);
            this.rptList.DataBind();
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and title like '%" + _keywords + "%'");
            }
            return strTemp.ToString();
        }
        #endregion

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("oauth_list.aspx", "keywords={0}", txtKeywords.Text.Trim()));
        }

        //保存排序
        protected void btnSave_Click(object sender, EventArgs e)
        {
            BLL.app_oauth bll = new BLL.app_oauth();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                int sortId;
                if (!int.TryParse(((TextBox)rptList.Items[i].FindControl("txtSortId")).Text.Trim(), out sortId))
                {
                    sortId = 99;
                }
                bll.UpdateField(id, "sort_id=" + sortId.ToString());
            }
            JscriptMsg("保存排序成功啦！", Utils.CombUrlTxt("oauth_list.aspx", "keywords={0}", this.keywords), "Success");
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("app_oauth", DTEnums.ActionEnum.Delete.ToString()); //检查权限
            BLL.app_oauth bll = new BLL.app_oauth();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    bll.Delete(id);
                }
            }
            JscriptMsg("批量删除成功啦！", Utils.CombUrlTxt("oauth_list.aspx", "keywords={0}", this.keywords), "Success");
        }

    }
}