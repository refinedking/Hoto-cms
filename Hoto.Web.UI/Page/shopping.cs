using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using Hoto.Common;

namespace Hoto.Web.UI.Page
{
    public partial class shopping : Web.UI.BasePage
    {
        protected string action = string.Empty;
        protected Hoto.Model.cart_total cartModel;
        protected Hoto.Model.users userModel;

        /// <summary>
        /// 重写父类的虚方法,此方法将在Init事件前执行
        /// </summary>
        protected override void ShowPage()
        {
            action = HotoRequest.GetQueryString("action");
            this.Init += new EventHandler(shopping_Init); //加入Init事件
        }
        /// <summary>
        /// 将在Init事件执行
        /// </summary>
        protected void shopping_Init(object sender, EventArgs e)
        {
            int group_id = 0;
            userModel = GetUserInfo();
            if (userModel != null)
            {
                group_id = userModel.group_id;
            }
            if (action == "confirm" && userModel == null)
            {
                //自动跳转URL
                HttpContext.Current.Response.Redirect(linkurl("login"));
            }
            cartModel = Web.UI.ShopCart.GetTotal(group_id);
        }
    }
}
