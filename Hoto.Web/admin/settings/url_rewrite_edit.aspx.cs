using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.settings
{
    public partial class url_rewrite_edit : DTcms.Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        private string urlName;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = DTRequest.GetQueryString("action");
            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = DTEnums.ActionEnum.Edit.ToString();//修改类型
                this.urlName = DTRequest.GetQueryString("name");
                if (string.IsNullOrEmpty(this.urlName))
                {
                    JscriptMsg("传输参数不正确！", "back", "Error");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("sys_config", DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(urlName);
                }
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(string _urlName)
        {
            BLL.url_rewrite bll = new BLL.url_rewrite();
            Model.url_rewrite model = bll.GetInfo(_urlName);
            txtName.Text = model.name;
            txtName.ReadOnly = true;
            txtPath.Text = model.path;
            txtPattern.Text = model.pattern;
            txtPage.Text = model.page;
            txtQueryString.Text = model.querystring;
            txtTemplet.Text = model.templet;
            txtChannel.Text = model.channel;
            ddlType.SelectedValue = model.type;
            txtInherit.Text = model.inherit;
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            BLL.url_rewrite bll = new BLL.url_rewrite();
            Model.url_rewrite model = new Model.url_rewrite();
            model.name = txtName.Text.Trim();
            model.path = txtPath.Text.Trim();
            model.pattern = txtPattern.Text.Trim();
            model.page = txtPage.Text.Trim();
            model.querystring = txtQueryString.Text.Trim();
            model.templet = txtTemplet.Text.Trim();
            model.channel = txtChannel.Text.Trim();
            model.type = ddlType.SelectedValue;
            model.inherit = txtInherit.Text.Trim();
            return bll.Add(model);
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(string _urlName)
        {
            BLL.url_rewrite bll = new BLL.url_rewrite();
            Model.url_rewrite model = bll.GetInfo(_urlName);
            model.name = txtName.Text.Trim();
            model.path = txtPath.Text.Trim();
            model.pattern = txtPattern.Text.Trim();
            model.page = txtPage.Text.Trim();
            model.querystring = txtQueryString.Text.Trim();
            model.templet = txtTemplet.Text.Trim();
            model.channel = txtChannel.Text.Trim();
            model.type = ddlType.SelectedValue;
            model.inherit = txtInherit.Text.Trim();
            return bll.Edit(model);
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                if (!DoEdit(this.urlName))
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("修改信息成功啦！", "url_rewrite_list.aspx", "Success");
            }
            else //添加
            {
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("添加信息成功啦！", "url_rewrite_list.aspx", "Success");
            }
        }

    }
}