using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Hoto.Common;
using LitJson;

namespace Hoto.Web.UI
{
    /// <summary>
    /// 购物车帮助类
    /// </summary>
    public partial class ShopCart
    {
        #region 基本增删改方法====================================
        /// <summary>
        /// 获得购物车列表
        /// </summary>
        public static IList<Hoto.Model.cart_items> GetList(int group_id)
        {
            IDictionary<string, int> dic = GetCart();
            if (dic != null)
            {
                IList<Hoto.Model.cart_items> iList = new List<Hoto.Model.cart_items>();

                foreach (var item in dic)
                {
                    Hoto.BLL.article bll = new Hoto.BLL.article();
                    Hoto.Model.article_goods model = bll.GetGoodsModel(Convert.ToInt32(item.Key));
                    if (model == null)
                    {
                        continue;
                    }
                    Hoto.Model.cart_items modelt = new Hoto.Model.cart_items();
                    modelt.id = model.id;
                    modelt.title = model.title;
                    modelt.img_url = model.img_url;
                    modelt.point = model.point;
                    modelt.price = model.sell_price;
                    modelt.user_price = model.sell_price;
                    modelt.stock_quantity = model.stock_quantity;
                    //会员价格
                    if (model.goods_group_prices != null)
                    {
                        Hoto.Model.goods_group_price gmodel = model.goods_group_prices.Find(p => p.group_id == group_id);
                        if (gmodel != null)
                        {
                            modelt.user_price = gmodel.price;
                        }
                    }
                    modelt.quantity = item.Value;
                    iList.Add(modelt);
                }
                return iList;
            }
            return null;
        }

        /// <summary>
        /// 添加到购物车
        /// </summary>
        public static bool Add(string Key, int Quantity)
        {
            IDictionary<string, int> dic = GetCart();
            if (dic != null)
            {
                if (dic.ContainsKey(Key))
                {
                    dic[Key] += Quantity;
                    AddCookies(JsonMapper.ToJson(dic));
                    return true;
                }
            }
            else
            {
                dic = new Dictionary<string, int>();
            }
            //不存在的则新增
            dic.Add(Key, Quantity);
            AddCookies(JsonMapper.ToJson(dic));
            return true;
        }

        /// <summary>
        /// 更新购物车数量
        /// </summary>
        public static bool Update(string Key, int Quantity)
        {
            if (Quantity == 0)
            {
                Clear(Key);
                return true;
            }
            IDictionary<string, int> dic = GetCart();
            if (dic != null && dic.ContainsKey(Key))
            {
                dic[Key] = Quantity;
                AddCookies(JsonMapper.ToJson(dic));
                return true;
            }
            return false;
        }

        /// <summary>
        /// 移除购物车
        /// </summary>
        /// <param name="Key">主键 0为清理所有的购物车信息</param>
        public static void Clear(string Key)
        {
            if (Key == "0")//为0的时候清理全部购物车cookies
            {
                HotoUtils.WriteCookie(HotoKeys.COOKIE_SHOPPING_CART, "", -43200);
            }
            else
            {
                IDictionary<string, int> dic = GetCart();
                if (dic != null)
                {
                    dic.Remove(Key);
                    AddCookies(JsonMapper.ToJson(dic));
                }
            }
        }
        #endregion

        #region 扩展方法==========================================
        public static Hoto.Model.cart_total GetTotal(int group_id)
        {
            Hoto.Model.cart_total model = new Hoto.Model.cart_total();
            IList<Hoto.Model.cart_items> iList = GetList(group_id);
            if (iList != null)
            {
                foreach (Hoto.Model.cart_items modelt in iList)
                {
                    model.total_num++;
                    model.total_quantity += modelt.quantity;
                    model.payable_amount += modelt.price * modelt.quantity;
                    model.real_amount += modelt.user_price * modelt.quantity;
                    model.total_point += modelt.point * modelt.quantity;
                }
            }
            return model;
        }
        #endregion

        #region 私有方法==========================================
        /// <summary>
        /// 获取cookies值
        /// </summary>
        private static IDictionary<string, int> GetCart()
        {
            IDictionary<string, int> dic = new Dictionary<string, int>();
            if (!string.IsNullOrEmpty(GetCookies()))
            {
                return JsonMapper.ToObject<Dictionary<string, int>>(GetCookies());
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 添加对象到cookies
        /// </summary>
        /// <param name="strValue"></param>
        private static void AddCookies(string strValue)
        {
            HotoUtils.WriteCookie(HotoKeys.COOKIE_SHOPPING_CART, strValue, 43200); //存储一个月
        }

        /// <summary>
        /// 获取cookies
        /// </summary>
        /// <returns></returns>
        private static string GetCookies()
        {
            return HotoUtils.GetCookie(HotoKeys.COOKIE_SHOPPING_CART);
        }

        #endregion
    }

}
