using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.manager
{
    public partial class role_list : DTcms.Web.UI.ManagePage
    {
        public string keywords = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["keywords"]))
            {
                this.keywords = Request.QueryString["keywords"].Trim();
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("sys_role", DTEnums.ActionEnum.View.ToString()); //检查权限
                //取得管理员信息
                Model.manager model = GetAdminInfo();
                RptBind("role_type>=" + model.role_type + CombSqlTxt(this.keywords));
            }
        }

        #region 数据绑定
        private void RptBind(string _strWhere)
        {
            this.txtKeywords.Text = this.keywords;
            BLL.manager_role bll = new BLL.manager_role();
            this.rptList.DataSource = bll.GetList(_strWhere);
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
                strTemp.Append(" and role_name like '%" + _keywords + "%'");
            }

            return strTemp.ToString();
        }
        #endregion

        #region 返回角色类型名称
        protected string GetTypeName(int role_type)
        {
            string str = "";
            switch (role_type)
            {
                case 1:
                    str = "超级用户";
                    break;
                default:
                    str = "系统用户";
                    break;
            }
            return str;
        }
        #endregion

        //删除操作
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("sys_role", DTEnums.ActionEnum.Delete.ToString()); //检查权限
            BLL.manager_role bll = new BLL.manager_role();
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
            JscriptMsg("批量删除成功啦！", Utils.CombUrlTxt("role_list.aspx", "keywords={0}", this.keywords), "Success");
        }

        //查询操作
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("role_list.aspx", "keywords={0}", txtKeywords.Text.Trim()));
        }
    }
}