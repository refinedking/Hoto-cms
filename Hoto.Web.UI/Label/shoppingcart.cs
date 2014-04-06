using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Hoto.Web.UI
{
    public partial class BasePage : System.Web.UI.Page
    {
        /// <summary>
        /// 返回购物车商品总数
        /// </summary>
        /// <returns>Int</returns>
        protected int get_cart_quantity()
        {
            int group_id = 0;
            Hoto.Model.users userModel = GetUserInfo();
            if (userModel != null)
            {
                group_id = userModel.group_id;
            }
            Hoto.Model.cart_total cartModel = Web.UI.ShopCart.GetTotal(group_id);
            return cartModel.total_quantity;
        }

        /// <summary>
        /// 返回购物车列表
        /// </summary>
        /// <returns>IList</returns>
        protected IList<Hoto.Model.cart_items> get_cart_list()
        {
            int group_id = 0;
            Hoto.Model.users userModel = GetUserInfo();
            if (userModel != null)
            {
                group_id = userModel.group_id;
            }
            IList<Hoto.Model.cart_items> ls = Web.UI.ShopCart.GetList(group_id);
            if (ls == null)
            {
                ls = new List<Hoto.Model.cart_items>();
            }
            return ls;
        }

    }
}
