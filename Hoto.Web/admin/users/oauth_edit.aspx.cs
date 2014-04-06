using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.users
{
    public partial class oauth_edit : Web.UI.ManagePage
    {
        protected int id;
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = DTRequest.GetQueryString("action");
            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = DTEnums.ActionEnum.Edit.ToString();//修改类型
                this.id = DTRequest.GetQueryInt("id");
                if (this.id == 0)
                {
                    JscriptMsg("传输参数不正确！", "back", "Error");
                    return;
                }
                if (!new BLL.app_oauth().Exists(this.id))
                {
                    JscriptMsg("OAuth不存在或已删除！", "back", "Error");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.app_oauth bll = new BLL.app_oauth();
            Model.app_oauth model = bll.GetModel(_id);
            txtTitle.Text = model.title;
            rblIsLock.SelectedValue = model.is_lock.ToString();
            txtSortId.Text = model.sort_id.ToString();
            txtApiPath.Text = model.api_path;
            txtAppId.Text = model.app_id;
            txtAppKey.Text = model.app_key;
            txtImgUrl.Text = model.img_url;
            txtRemark.Text = model.remark;
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = true;
            Model.app_oauth model = new Model.app_oauth();
            BLL.app_oauth bll = new BLL.app_oauth();

            model.title = txtTitle.Text.Trim();
            model.is_lock = int.Parse(rblIsLock.SelectedValue);
            model.sort_id = int.Parse(txtSortId.Text.Trim());
            model.api_path = txtApiPath.Text.Trim();
            model.app_id = txtAppId.Text.Trim();
            model.app_key = txtAppKey.Text.Trim();
            model.img_url = txtImgUrl.Text.Trim();
            model.remark = txtRemark.Text;
            if (bll.Add(model) < 1)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = true;
            BLL.app_oauth bll = new BLL.app_oauth();
            Model.app_oauth model = bll.GetModel(_id);

            model.title = txtTitle.Text.Trim();
            model.is_lock = int.Parse(rblIsLock.SelectedValue);
            model.sort_id = int.Parse(txtSortId.Text.Trim());
            model.api_path = txtApiPath.Text.Trim();
            model.app_id = txtAppId.Text.Trim();
            model.app_key = txtAppKey.Text.Trim();
            model.img_url = txtImgUrl.Text.Trim();
            model.remark = txtRemark.Text;
            if (!bll.Update(model))
            {
                result = false;
            }

            return result;
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("app_oauth", DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("修改成功啦！", "oauth_list.aspx", "Success");
            }
            else //添加
            {
                ChkAdminLevel("app_oauth", DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("添加成功啦！", "oauth_list.aspx", "Success");
            }
        }
    }
}