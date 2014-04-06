using System;
namespace Hoto.Model
{
    /// <summary>
    /// 订单商品列表
    /// </summary>
    [Serializable]
    public partial class order_goods
    {
        public order_goods()
        { }
        #region Model
        private int _id;
        private int _order_id;
        private int _goods_id;
        private string _goods_name;
        private decimal _goods_price = 0M;
        private decimal _real_price = 0M;
        private int _quantity = 0;
        private int _point = 0;
        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 订单ID
        /// </summary>
        public int order_id
        {
            set { _order_id = value; }
            get { return _order_id; }
        }
        /// <summary>
        /// 商品ID
        /// </summary>
        public int goods_id
        {
            set { _goods_id = value; }
            get { return _goods_id; }
        }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string goods_name
        {
            set { _goods_name = value; }
            get { return _goods_name; }
        }
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal goods_price
        {
            set { _goods_price = value; }
            get { return _goods_price; }
        }
        /// <summary>
        /// 实际价格
        /// </summary>
        public decimal real_price
        {
            set { _real_price = value; }
            get { return _real_price; }
        }
        /// <summary>
        /// 订购数量
        /// </summary>
        public int quantity
        {
            set { _quantity = value; }
            get { return _quantity; }
        }
        /// <summary>
        /// 购物积分
        /// </summary>
        public int point
        {
            set { _point = value; }
            get { return _point; }
        }
        #endregion Model

    }
}