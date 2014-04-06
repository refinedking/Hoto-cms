using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Hoto.Common;

namespace Hoto.Web.UI.Page
{
    public partial class repassword: Web.UI.BasePage
    {
        protected string action;
        protected Hoto.Model.user_code model;

        /// <summary>
        /// 重写父类的虚方法,此方法将在Init事件前执行
        /// </summary>
        protected override void ShowPage()
        {
            action = HotoRequest.GetQueryString("action");
            if (action == "reset")
            {
                string strcode = HotoRequest.GetQueryString("code");
                if (strcode != null)
                {
                    model = new Hoto.BLL.user_code().GetModel(strcode);
                    if (model == null)
                    {
                        HttpContext.Current.Response.Redirect(linkurl("repassword", "error"));
                        return;
                    }
                }
            }
        }

    }
}
