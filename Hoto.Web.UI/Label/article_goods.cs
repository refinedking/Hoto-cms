using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Hoto.Web.UI
{
    public partial class BasePage : System.Web.UI.Page
    {
        /// <summary>
        /// 商品列表
        /// </summary>
        /// <param name="channel_id">频道ID</param>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>DataTable</returns>
        protected DataTable get_goods_list(int channel_id, int category_id, int top, string strwhere)
        {
            DataTable dt = new DataTable();
            if (channel_id > 0)
            {
                string _where = "channel_id=" + channel_id;
                if (category_id > 0)
                {
                    _where += " and category_id in(select id from dt_category where channel_id=" + channel_id + " and class_list like '%," + category_id + ",%')";
                }
                if (!string.IsNullOrEmpty(strwhere))
                {
                    _where += " and " + strwhere;
                }
                dt = new Hoto.BLL.article().GetGoodsList(top, _where, "sort_id asc,add_time desc").Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 商品列表
        /// </summary>
        /// <param name="channel_id">频道ID</param>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>DataTable</returns>
        protected DataTable get_goods_list(int channel_id, int category_id, int top, string strwhere, string orderby)
        {
            DataTable dt = new DataTable();
            if (channel_id > 0)
            {
                string _where = "channel_id=" + channel_id;
                if (category_id > 0)
                {
                    _where += " and category_id in(select id from dt_category where channel_id=" + channel_id + " and class_list like '%," + category_id + ",%')";
                }
                if (!string.IsNullOrEmpty(strwhere))
                {
                    _where += " and " + strwhere;
                }
                dt = new Hoto.BLL.article().GetGoodsList(top, _where, orderby).Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 商品分页列表
        /// </summary>
        /// <param name="channel_id">频道ID</param>
        /// <param name="page_size">页面大小</param>
        /// <param name="page_index">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="totalcount">总记录数</param>
        /// <returns>DateTable</returns>
        protected DataTable get_goods_list(int channel_id, int category_id, int page_size, int page_index, string strwhere, out int totalcount)
        {
            DataTable dt = new DataTable();
            if (channel_id > 0)
            {
                string _where = "channel_id=" + channel_id;
                if (category_id > 0)
                {
                    _where += " and category_id in(select id from dt_category where channel_id=" + channel_id + " and class_list like '%," + category_id + ",%')";
                }
                if (!string.IsNullOrEmpty(strwhere))
                {
                    _where += " and " + strwhere;
                }
                dt = new Hoto.BLL.article().GetGoodsList(page_size, page_index, _where, "sort_id asc,add_time desc", out totalcount).Tables[0];
            }
            else
            {
                totalcount = 0;
            }
            return dt;
        }

        /// <summary>
        /// 返回相应的商品图片
        /// </summary>
        /// <param name="goods_id">商品ID</param>
        /// <returns>String</returns>
        protected string get_goods_img_url(int goods_id)
        {
            Hoto.Model.article_goods model = new Hoto.BLL.article().GetGoodsModel(goods_id);
            if (model != null)
            {
                return model.img_url;
            }
            return "";
        }

        /// <summary>
        /// 返回对应商品的会员价格
        /// </summary>
        /// <param name="goods_id">商品ID</param>
        /// <returns>Decimal</returns>
        protected decimal get_user_goods_price(int goods_id)
        {
            Hoto.Model.users userModel = GetUserInfo();
            if (userModel == null)
            {
                return -1;
            }
            Hoto.Model.article_goods model = new Hoto.BLL.article().GetGoodsModel(goods_id);
            if (model != null)
            {
                if (model.goods_group_prices != null)
                {
                    Hoto.Model.goods_group_price priceModel = model.goods_group_prices.Find(p => p.group_id == userModel.group_id);
                    if (priceModel != null)
                    {
                        return priceModel.price;
                    }
                }
                return model.sell_price;
            }
            return -1;
        }

    }
}
