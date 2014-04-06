using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.settings
{
    public partial class sys_channel_edit : DTcms.Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = DTRequest.GetQueryString("action");
            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = DTEnums.ActionEnum.Edit.ToString();//修改类型
                if (!int.TryParse(Request.QueryString["id"] as string, out this.id))
                {
                    JscriptMsg("传输参数不正确！", "back", "Error");
                    return;
                }
                if (!new DTcms.BLL.sys_channel().Exists(this.id))
                {
                    JscriptMsg("信息不存在或已被删除！", "back", "Error");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                TreeBind(this.ddlModelId); //绑定模型
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

        #region 绑定模型=================================
        private void TreeBind(DropDownList ddl)
        {
            DTcms.BLL.sys_model bll = new DTcms.BLL.sys_model();
            DataTable dt = bll.GetList("").Tables[0];

            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("请选择模型", ""));
            foreach (DataRow dr in dt.Rows)
            {
                ddl.Items.Add(new ListItem(dr["title"].ToString(), dr["id"].ToString()));
            }
        }
        #endregion

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.sys_channel bll = new BLL.sys_channel();
            Model.sys_channel model = bll.GetModel(_id);
            txtName.Text = model.name;
            hidName.Value = model.name;
            txtTitle.Text = model.title;
            ddlModelId.SelectedValue = model.model_id.ToString();
            txtSortId.Text = model.sort_id.ToString();
            //绑定URL配置列表
            rptList.DataSource = new BLL.url_rewrite().GetList(model.id.ToString());
            rptList.DataBind();
        }
        #endregion

        #region 根据类型返回文件名========================
        //private string GetPageName(string _name, string page_type)
        //{
        //    string result = "";
        //    switch (page_type)
        //    {
        //        case "index":
        //            result = _name + ".aspx";
        //            break;
        //        case "list":
        //            result = _name + "_list.aspx";
        //            break;
        //        case "detail":
        //            result = _name + "_show.aspx";
        //            break;
        //    }
        //    return result;
        //}
        #endregion

        #region 返回页面的类型============================
        protected string GetPageTypeName(string _type_name)
        {
            string result = "";
            switch (_type_name)
            {
                case "index":
                    result = "首页";
                    break;
                case "list":
                    result = "列表页";
                    break;
                case "detail":
                    result = "详细页";
                    break;
            }
            return result;
        }
        #endregion

        #region 查询频道的页面继承类======================
        private string GetInherit(int model_id, string page_type)
        {
            string result = "";
            BLL.sys_model bll = new BLL.sys_model();
            Model.sys_model model = bll.GetModel(model_id);
            if (model != null)
            {
                switch (page_type)
                {
                    case "index":
                        result = model.inherit_index;
                        break;
                    case "list":
                        result = model.inherit_list;
                        break;
                    case "detail":
                        result = model.inherit_detail;
                        break;
                }
            }
            return result;
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            Model.sys_channel model = new Model.sys_channel();
            BLL.sys_channel bll = new BLL.sys_channel();
            model.name = txtName.Text.Trim();
            model.title = txtTitle.Text.Trim();
            model.model_id = int.Parse(ddlModelId.SelectedValue);
            model.sort_id = int.Parse(txtSortId.Text.Trim());
            int new_id = bll.Add(model); //保存
            if (new_id < 1)
            {
                return false;
            }
            #region 保存URL配置.开始===================================
            BLL.url_rewrite bll2 = new BLL.url_rewrite();
            bll2.Remove("channel", new_id.ToString()); //先删除
            string[] itemTypeArr = Request.Form.GetValues("item_type");
            string[] itemNameArr = Request.Form.GetValues("item_name");
            string[] itemPathArr = Request.Form.GetValues("item_path");
            string[] itemPatternArr = Request.Form.GetValues("item_pattern");
            string[] itemQuerystringArr = Request.Form.GetValues("item_querystring");
            string[] itemTempletArr = Request.Form.GetValues("item_templet");
            string[] itemPageArr = Request.Form.GetValues("item_page");
            if (itemTypeArr != null && itemNameArr != null && itemPathArr != null
                && itemPatternArr != null && itemQuerystringArr != null && itemTempletArr != null && itemPageArr != null)
            {
                if ((itemTypeArr.Length == itemNameArr.Length) && (itemNameArr.Length == itemPathArr.Length) && (itemPathArr.Length == itemPatternArr.Length)
                    && (itemPatternArr.Length == itemQuerystringArr.Length) && (itemQuerystringArr.Length == itemTempletArr.Length))
                {
                    for (int i = 0; i < itemTypeArr.Length; i++)
                    {
                        bll2.Add(new Model.url_rewrite
                        {
                            name = itemNameArr[i].Trim(),
                            path = itemPathArr[i].Trim(),
                            pattern = itemPatternArr[i].Trim(),
                            //page = GetPageName(model.name, itemTypeArr[i].Trim()), //源页面地址
                            page = itemPageArr[i].Trim(),
                            querystring = itemQuerystringArr[i].Trim(),
                            templet = itemTempletArr[i].Trim(),
                            channel = new_id.ToString(),
                            type = itemTypeArr[i].Trim(),
                            inherit = GetInherit(model.model_id, itemTypeArr[i].Trim()) //继承的类名
                        });
                    }
                }
            }
            #endregion 保存URL配置.结束

            return true;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            DTcms.BLL.sys_channel bll = new DTcms.BLL.sys_channel();
            DTcms.Model.sys_channel model = bll.GetModel(_id);
            model.name = txtName.Text.Trim();
            model.title = txtTitle.Text.Trim();
            model.model_id = int.Parse(ddlModelId.SelectedValue);
            model.sort_id = int.Parse(txtSortId.Text.Trim());
            if (!bll.Update(model))
            {
                return false;
            }

            #region 保存URL配置.开始===================================
            BLL.url_rewrite bll2 = new BLL.url_rewrite();
            bll2.Remove("channel", model.id.ToString()); //先删除
            string[] itemTypeArr = Request.Form.GetValues("item_type");
            string[] itemNameArr = Request.Form.GetValues("item_name");
            string[] itemPathArr = Request.Form.GetValues("item_path");
            string[] itemPatternArr = Request.Form.GetValues("item_pattern");
            string[] itemQuerystringArr = Request.Form.GetValues("item_querystring");
            string[] itemTempletArr = Request.Form.GetValues("item_templet");
            string[] itemPageArr = Request.Form.GetValues("item_page");
            if (itemTypeArr != null && itemNameArr != null && itemPathArr != null
                && itemPatternArr != null && itemQuerystringArr != null && itemTempletArr != null)
            {
                if ((itemTypeArr.Length == itemNameArr.Length) && (itemNameArr.Length == itemPathArr.Length) && (itemPathArr.Length == itemPatternArr.Length)
                    && (itemPatternArr.Length == itemQuerystringArr.Length) && (itemQuerystringArr.Length == itemTempletArr.Length))
                {
                    for (int i = 0; i < itemTypeArr.Length; i++)
                    {
                        bll2.Add(new Model.url_rewrite
                        {
                            name = itemNameArr[i].Trim(),
                            path = itemPathArr[i].Trim(),
                            pattern = itemPatternArr[i].Trim(),
                            //page = GetPageName(model.name, itemTypeArr[i].Trim()), //源页面地址
                            page = itemPageArr[i].Trim(),
                            querystring = itemQuerystringArr[i].Trim(),
                            templet = itemTempletArr[i].Trim(),
                            channel = model.id.ToString(),
                            type = itemTypeArr[i].Trim(),
                            inherit = GetInherit(model.model_id, itemTypeArr[i].Trim()) //继承的类名
                        });
                    }
                }
            }
            #endregion 保存URL配置.结束

            return true;
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("sys_channel", DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("修改信息成功啦！", "sys_channel_list.aspx", "Success", "parent.loadChannelTree");
            }
            else //添加
            {
                ChkAdminLevel("sys_channel", DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("添加信息成功啦！", "sys_channel_list.aspx", "Success", "parent.loadChannelTree");
            }
        }

    }
}