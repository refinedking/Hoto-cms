using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Hoto.Common;

namespace Hoto.Web.UI
{
    public partial class BasePage : System.Web.UI.Page
    {
        /// <summary>
        /// 统计订单数量
        /// </summary>
        /// <param name="strwhere">查询条件</param>
        /// <returns>Int</returns>
        protected int get_user_order_count(string strwhere)
        {
            return new Hoto.BLL.orders().GetCount(strwhere);
        }

        /// <summary>
        /// 订单列表
        /// </summary>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>DataTable</returns>
        protected DataTable get_order_list(int top, string strwhere)
        {
            return new Hoto.BLL.orders().GetList(top, strwhere, "add_time desc,id desc").Tables[0];
        }

        /// <summary>
        /// 订单分页列表
        /// </summary>
        /// <param name="page_size">页面大小</param>
        /// <param name="page_index">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="totalcount">总记录数</param>
        /// <returns>DateTable</returns>
        protected DataTable get_order_list(int page_size, int page_index, string strwhere, out int totalcount)
        {
            return new Hoto.BLL.orders().GetList(page_size, page_index, strwhere, "add_time desc,id desc", out totalcount).Tables[0];
        }

        /// <summary>
        /// 返回订单商品列表
        /// </summary>
        /// <param name="order_id">订单</param>
        /// <returns>List</returns>
        protected List<Hoto.Model.article_goods> get_order_goods_list(int order_id)
        {
            Hoto.Model.orders model = new Hoto.BLL.orders().GetModel(order_id);
            if (model == null)
            {
                return null;
            }
            List<Hoto.Model.article_goods> ls = new List<Hoto.Model.article_goods>();
            if (model.order_goods != null)
            {
                foreach (Hoto.Model.order_goods modelt in model.order_goods)
                {
                    Hoto.Model.article_goods goodsModel = new Hoto.BLL.article().GetGoodsModel(modelt.goods_id);
                    if (goodsModel != null)
                    {
                        ls.Add(goodsModel);
                    }
                }
            }
            return ls;
        }

        /// <summary>
        /// 返回订单状态
        /// </summary>
        /// <param name="_id">订单ID</param>
        /// <returns>String</returns>
        protected string get_order_status(int _id)
        {
            string _title = "";
            Hoto.Model.orders model = new Hoto.BLL.orders().GetModel(_id);
            switch (model.status)
            {
                case 1:
                    _title = "等待确认";
                    Hoto.Model.payment payModel = new Hoto.BLL.payment().GetModel(model.payment_id);
                    if (payModel != null && payModel.type == 1)
                    {
                        if (model.payment_status > 1)
                        {
                            _title = "付款成功";
                        }
                        else
                        {
                            _title = "等待付款";
                        }
                    }
                    break;
                case 2:
                    if (model.distribution_status > 1)
                    {
                        _title = "已发货";
                    }
                    else
                    {
                        _title = "待发货";
                    }
                    break;
                case 3:
                    _title = "交易完成";
                    break;
                case 4:
                    _title = "订单取消";
                    break;
                case 5:
                    _title = "订单作废";
                    break;
            }

            return _title;
        }

        /// <summary>
        /// 返回订单是否需要在线支付
        /// </summary>
        /// <param name="order_id">订单ID</param>
        /// <returns>bool</returns>
        protected bool get_order_payment_status(int order_id)
        {
            Hoto.Model.orders model = new Hoto.BLL.orders().GetModel(order_id);
            if (model == null)
            {
                return false;
            }
            if (model.status > 1)
            {
                return false;
            }
            Hoto.Model.payment payModel = new Hoto.BLL.payment().GetModel(model.payment_id);
            if (payModel == null)
            {
                return false;
            }
            if (payModel.type == 1 && model.payment_status == 1)
            {
                return true;
            }
            return false;
        }

    }
}
