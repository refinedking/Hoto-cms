using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DTcms.Common;

namespace DTcms.Web.admin.comment
{
    public partial class edit : Web.UI.ManagePage
    {
        private int id = 0;
        protected Model.article_comment model = new Model.article_comment();

        protected void Page_Load(object sender, EventArgs e)
        {
            id = DTRequest.GetQueryInt("id");
            if (id == 0)
            {
                JscriptMsg("传输参数不正确！", "back", "Error");
                return;
            }
            if (!new BLL.article_comment().Exists(this.id))
            {
                JscriptMsg("信息不存在或已被删除！", "back", "Error");
                return;
            }
            if (!Page.IsPostBack)
            {
                ShowInfo(this.id);
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            ChkAdminLevel("sys_comment", DTEnums.ActionEnum.View.ToString()); //检查权限
            BLL.article_comment bll = new BLL.article_comment();
            model = bll.GetModel(_id);
            txtReContent.Text = Utils.ToTxt(model.reply_content);
            rblIsLock.SelectedValue = model.is_lock.ToString();
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("sys_comment", DTEnums.ActionEnum.Reply.ToString()); //检查权限
            BLL.article_comment bll = new BLL.article_comment();
            model = bll.GetModel(this.id);
            model.is_reply = 1;
            model.reply_content = Utils.ToHtml(txtReContent.Text);
            model.is_lock = int.Parse(rblIsLock.SelectedValue);
            model.reply_time = DateTime.Now;
            bll.Update(model);
            JscriptMsg("评论回复成功啦！", "list.aspx", "Success");
        }
    }
}