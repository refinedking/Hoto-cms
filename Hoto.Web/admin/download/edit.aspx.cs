using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DTcms.Common;

namespace DTcms.Web.admin.download
{
    public partial class edit : Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        private int channel_id;
        private int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = DTRequest.GetQueryString("action");
            this.channel_id = DTRequest.GetQueryInt("channel_id");
            if (this.channel_id == 0)
            {
                JscriptMsg("频道参数不正确！", "back", "Error");
                return;
            }
            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = DTEnums.ActionEnum.Edit.ToString();//修改类型
                this.id = DTRequest.GetQueryInt("id");
                if (this.id == 0)
                {
                    JscriptMsg("传输参数不正确！", "back", "Error");
                    return;
                }
                if (!new BLL.article().Exists(this.id))
                {
                    JscriptMsg("信息不存在或已被删除！", "back", "Error");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                TreeBind(this.channel_id); //绑定类别
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.article bll = new BLL.article();
            Model.article_download model = bll.GetDownloadModel(_id);

            ddlCategoryId.SelectedValue = model.category_id.ToString();
            txtTitle.Text = model.title;
            txtImgUrl.Text = model.img_url;
            txtLinkUrl.Text = model.link_url;
            if (model.is_msg == 1)
            {
                cblItem.Items[0].Selected = true;
            }
            if (model.is_red == 1)
            {
                cblItem.Items[1].Selected = true;
            }
            if (model.is_lock == 1)
            {
                cblItem.Items[2].Selected = true;
            }
            txtSortId.Text = model.sort_id.ToString();
            txtClick.Text = model.click.ToString();
            txtDiggGood.Text = model.digg_good.ToString();
            txtDiggBad.Text = model.digg_bad.ToString();
            txtContent.Value = model.content;
            txtSeoTitle.Text = model.seo_title;
            txtSeoKeywords.Text = model.seo_keywords;
            txtSeoDescription.Text = model.seo_description;
            //绑定附件
            rptAttach.DataSource = model.download_attachs;
            rptAttach.DataBind();
        }
        #endregion

        #region 绑定类别=================================
        private void TreeBind(int _channel_id)
        {
            BLL.category bll = new BLL.category();
            DataTable dt = bll.GetList(0, _channel_id);

            this.ddlCategoryId.Items.Clear();
            this.ddlCategoryId.Items.Add(new ListItem("请选择类别...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                int ClassLayer = int.Parse(dr["class_layer"].ToString());
                string Title = dr["title"].ToString().Trim();

                if (ClassLayer == 1)
                {
                    this.ddlCategoryId.Items.Add(new ListItem(Title, Id));
                }
                else
                {
                    Title = "├ " + Title;
                    Title = Utils.StringOfChar(ClassLayer - 1, "　") + Title;
                    this.ddlCategoryId.Items.Add(new ListItem(Title, Id));
                }
            }
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = true;
            Model.article_download model = new Model.article_download();
            BLL.article bll = new BLL.article();

            model.channel_id = this.channel_id;
            model.title = txtTitle.Text.Trim();
            model.category_id = int.Parse(ddlCategoryId.SelectedValue);
            model.link_url = txtLinkUrl.Text.Trim();
            model.img_url = txtImgUrl.Text.Trim();
            model.content = txtContent.Value;
            model.seo_title = txtSeoTitle.Text.Trim();
            model.seo_keywords = txtSeoKeywords.Text.Trim();
            model.seo_description = txtSeoDescription.Text.Trim();
            model.sort_id = int.Parse(txtSortId.Text.Trim());
            model.click = int.Parse(txtClick.Text.Trim());
            model.digg_good = int.Parse(txtDiggGood.Text.Trim());
            model.digg_bad = int.Parse(txtDiggBad.Text.Trim());
            model.is_msg = 0;
            model.is_red = 0;
            model.is_lock = 0;
            if (cblItem.Items[0].Selected == true)
            {
                model.is_msg = 1;
            }
            if (cblItem.Items[1].Selected == true)
            {
                model.is_red = 1;
            }
            if (cblItem.Items[2].Selected == true)
            {
                model.is_lock = 1;
            }
            //保存附件
            string hidFileList = Request.Params["hidFileName"];
            if (!string.IsNullOrEmpty(hidFileList))
            {
                string[] fileListArr = hidFileList.Split(',');
                string[] pointArr = Request.Form.GetValues("txtPoint");
                List<Model.download_attach> ls = new List<Model.download_attach>();
                for (int i = 0; i < fileListArr.Length; i++)
                {
                    string[] fileArr = fileListArr[i].Split('|');
                    if (fileArr.Length == 3)
                    {
                        int fileSize = Utils.GetFileSize(fileArr[2]);
                        string fileExt = Utils.GetFileExt(fileArr[2]);
                        int _point = int.Parse(pointArr[i]);
                        ls.Add(new Model.download_attach { id = int.Parse(fileArr[0]), title = fileArr[1], file_path = fileArr[2], file_size = fileSize, file_ext = fileExt, point=_point });
                    }
                }
                model.download_attachs = ls;
            }

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
            BLL.article bll = new BLL.article();
            Model.article_download model = bll.GetDownloadModel(_id);

            model.channel_id = this.channel_id;
            model.title = txtTitle.Text.Trim();
            model.category_id = int.Parse(ddlCategoryId.SelectedValue);
            model.link_url = txtLinkUrl.Text.Trim();
            model.img_url = txtImgUrl.Text.Trim();
            model.content = txtContent.Value;
            model.seo_title = txtSeoTitle.Text.Trim();
            model.seo_keywords = txtSeoKeywords.Text.Trim();
            model.seo_description = txtSeoDescription.Text.Trim();
            model.sort_id = int.Parse(txtSortId.Text.Trim());
            model.click = int.Parse(txtClick.Text.Trim());
            model.digg_good = int.Parse(txtDiggGood.Text.Trim());
            model.digg_bad = int.Parse(txtDiggBad.Text.Trim());
            model.is_msg = 0;
            model.is_red = 0;
            model.is_lock = 0;
            if (cblItem.Items[0].Selected == true)
            {
                model.is_msg = 1;
            }
            if (cblItem.Items[1].Selected == true)
            {
                model.is_red = 1;
            }
            if (cblItem.Items[2].Selected == true)
            {
                model.is_lock = 1;
            }

            //保存附件
            if (model.download_attachs != null)
            {
                model.download_attachs.Clear();
            }
            string hidFileList = Request.Params["hidFileName"];
            string[] pointArr = Request.Form.GetValues("txtPoint");
            if (!string.IsNullOrEmpty(hidFileList))
            {
                string[] fileListArr = hidFileList.Split(',');
                List<Model.download_attach> ls = new List<Model.download_attach>();
                for (int i = 0; i < fileListArr.Length; i++)
                {
                    string[] fileArr = fileListArr[i].Split('|');
                    if (fileArr.Length == 3)
                    {
                        int attach_id = int.Parse(fileArr[0]);
                        int fileSize = Utils.GetFileSize(fileArr[2]);
                        string fileExt = Utils.GetFileExt(fileArr[2]);
                        int _point = int.Parse(pointArr[i]);
                        //删除旧文件
                        if (attach_id > 0)
                        {
                            new BLL.download_attach().DeleteFile(attach_id, fileArr[2]);
                        }
                        ls.Add(new Model.download_attach { id = attach_id, article_id = _id, title = fileArr[1], file_path = fileArr[2], file_size = fileSize, file_ext = fileExt, point = _point });
                    }
                }
                model.download_attachs = ls;
            }

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
                ChkAdminLevel(channel_id, DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("修改成功啦！", "list.aspx?channel_id=" + this.channel_id, "Success");
            }
            else //添加
            {
                ChkAdminLevel(channel_id, DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("添加成功啦！", "list.aspx?channel_id=" + this.channel_id, "Success");
            }
        }

    }
}