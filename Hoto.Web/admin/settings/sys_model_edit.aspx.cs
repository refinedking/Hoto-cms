using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.settings
{
    public partial class sys_model_edit : Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;

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
                    if (!new BLL.sys_model().Exists(this.id))
                    {
                        JscriptMsg("信息不存在或已被删除！", "back", "Error");
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
            BLL.sys_model bll = new BLL.sys_model();
            Model.sys_model model = bll.GetModel(_id);
            txtTitle.Text = model.title;
            txtSortId.Text = model.sort_id.ToString();
            txtInheritIndex.Text = model.inherit_index;
            txtInheritList.Text = model.inherit_list;
            txtInheritDetail.Text = model.inherit_detail;
            rptNavList.DataSource = model.sys_model_navs;
            rptNavList.DataBind();
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = true;
            Model.sys_model model = new Model.sys_model();
            BLL.sys_model bll = new BLL.sys_model();

            string nav_title = Request.Form["nav_title"];
            string nav_url = Request.Form["nav_url"];
            string nav_sort = Request.Form["nav_sort"];
            if (!string.IsNullOrEmpty(nav_title) && !string.IsNullOrEmpty(nav_url) && !string.IsNullOrEmpty(nav_sort))
            {
                try
                {
                    string[] titleArr = nav_title.Split(',');
                    string[] urlArr = nav_url.Split(',');
                    string[] sortArr = nav_sort.Split(',');
                    List<DTcms.Model.sys_model_nav> ls = new List<Model.sys_model_nav>();
                    for (int i = 0; i < titleArr.Length; i++)
                    {
                        ls.Add(new DTcms.Model.sys_model_nav { title = titleArr[i], nav_url = urlArr[i], sort_id = int.Parse(sortArr[i].Trim()) });
                    }
                    model.sys_model_navs = ls;
                }
                catch
                {
                    result = false;
                }
            }
            model.title = txtTitle.Text.Trim();
            model.sort_id = int.Parse(txtSortId.Text.Trim());
            model.inherit_index = txtInheritIndex.Text.Trim();
            model.inherit_list = txtInheritList.Text.Trim();
            model.inherit_detail = txtInheritDetail.Text.Trim();
            model.is_sys = 0;
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
            BLL.sys_model bll = new BLL.sys_model();
            Model.sys_model model = bll.GetModel(_id);

            string nav_id = Request.Form["nav_id"];
            string nav_title = Request.Form["nav_title"];
            string nav_url = Request.Form["nav_url"];
            string nav_sort = Request.Form["nav_sort"];
            if (!string.IsNullOrEmpty(nav_id) && !string.IsNullOrEmpty(nav_title) && !string.IsNullOrEmpty(nav_url) && !string.IsNullOrEmpty(nav_sort))
            {
                try
                {
                    string[] idArr = nav_id.Split(',');
                    string[] titleArr = nav_title.Split(',');
                    string[] urlArr = nav_url.Split(',');
                    string[] sortArr = nav_sort.Split(',');
                    List<DTcms.Model.sys_model_nav> ls = new List<Model.sys_model_nav>();
                    for (int i = 0; i < titleArr.Length; i++)
                    {
                        ls.Add(new DTcms.Model.sys_model_nav { id = int.Parse(idArr[i]), model_id = model.id, title = titleArr[i], nav_url = urlArr[i], sort_id = int.Parse(sortArr[i].Trim()) });
                    }
                    model.sys_model_navs = ls;
                }
                catch
                {
                    result = false;
                }
            }
            model.title = txtTitle.Text.Trim();
            model.sort_id = int.Parse(txtSortId.Text.Trim());
            model.inherit_index = txtInheritIndex.Text.Trim();
            model.inherit_list = txtInheritList.Text.Trim();
            model.inherit_detail = txtInheritDetail.Text.Trim();
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
                ChkAdminLevel("sys_model", DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("修改模型成功啦！", "sys_model_list.aspx", "Success", "parent.loadChannelTree");
            }
            else //添加
            {
                ChkAdminLevel("sys_model", DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("添加模型成功啦！", "sys_model_list.aspx", "Success", "parent.loadChannelTree");
            }
        }
    }
}