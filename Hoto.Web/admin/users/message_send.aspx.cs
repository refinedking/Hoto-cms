using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DTcms.Common;

namespace DTcms.Web.admin.users
{
    public partial class message_send : Web.UI.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = true;
            Model.user_message model = new Model.user_message();
            BLL.user_message bll = new BLL.user_message();

            model.title = txtTitle.Text.Trim();
            model.content = txtContent.Value;

            string[] arrUserName = txtUserName.Text.Trim().Split(',');
            if (arrUserName.Length > 0)
            {
                foreach (string username in arrUserName)
                {
                    if (new BLL.users().Exists(username))
                    {
                        model.accept_user_name = username;
                        if (bll.Add(model) < 1)
                        {
                            result = false;
                        }
                    }
                }
            }

            return result;
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("user_message", DTEnums.ActionEnum.Add.ToString()); //检查权限
            if (!DoAdd())
            {
                JscriptMsg("发送过程中发生错误啦！", "", "Error");
                return;
            }
            JscriptMsg("发信信息成功啦！", "message_list.aspx", "Success");
        }

    }
}