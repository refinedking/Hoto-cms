using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.users
{
    public partial class group_list : Web.UI.ManagePage
    {
        public string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.keywords = DTRequest.GetQueryString("keywords");
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("user_groups", DTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind("id>0" + CombSqlTxt(this.keywords));
            }
        }

        #region 数据绑定
        private void RptBind(string _strWhere)
        {
            this.txtKeywords.Text = this.keywords;
            BLL.user_groups bll = new BLL.user_groups();
            this.rptList.DataSource = bll.GetList(0, _strWhere, "grade asc,id asc");
            this.rptList.DataBind();
        }
        #endregion

        #region 组合SQL查询语句
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

        //查询操作
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("group_list.aspx", "keywords={0}", txtKeywords.Text.Trim()));
        }

        //删除操作
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("user_groups", DTEnums.ActionEnum.Delete.ToString()); //检查权限
            BLL.user_groups bll = new BLL.user_groups();
            //批量删除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    bll.Delete(id);
                }
            }
            JscriptMsg("批量删除成功啦！", Utils.CombUrlTxt("group_list.aspx", "keywords={0}", txtKeywords.Text.Trim()), "Success");
        }

    }
}