using System;
using System.Collections.Generic;

namespace Hoto.Model
{
    /// <summary>
    /// 订单信息
    /// </summary>
    [Serializable]
    public partial class orders
    {
        public orders()
        { }
        #region Model
        private int _id;
        private string _order_no;
        private int _user_id = 0;
        private string _user_name = "";
        private int _payment_id = 0;
        private int _distribution_id = 0;
        private int _status = 1;
        private int _payment_status = 1;
        private int _distribution_status = 1;
        private string _delivery_name = "";
        private string _delivery_no = "";
        private string _accept_name;
        private string _post_code = "";
        private string _telphone = "";
        private string _mobile = "";
        private string _address = "";
        private string _message = "";
        private decimal _payable_amount = 0M;
        private decimal _real_amount = 0M;
        private decimal _payable_freight = 0M;
        private decimal _real_freight = 0M;
        private decimal _payment_fee = 0M;
        private decimal _order_amount = 0M;
        private int _point = 0;
        private DateTime _add_time = DateTime.Now;
        private DateTime? _payment_time;
        private DateTime? _confirm_time;
        private DateTime? _distribution_time;
        private DateTime? _complete_time;
        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 订单号
        /// </summary>
        public string order_no
        {
            set { _order_no = value; }
            get { return _order_no; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int user_id
        {
            set { _user_id = value; }
            get { return _user_id; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string user_name
        {
            set { _user_name = value; }
            get { return _user_name; }
        }
        /// <summary>
        /// 支付类型
        /// </summary>
        public int payment_id
        {
            set { _payment_id = value; }
            get { return _payment_id; }
        }
        /// <summary>
        /// 配送类型
        /// </summary>
        public int distribution_id
        {
            set { _distribution_id = value; }
            get { return _distribution_id; }
        }
        /// <summary>
        /// 订单状态1生成订单,2确认订单,3完成订单,4取消订单,5作废订单
        /// </summary>
        public int status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 支付状态1未支付,2已支付
        /// </summary>
        public int payment_status
        {
            set { _payment_status = value; }
            get { return _payment_status; }
        }
        /// <summary>
        /// 发货状态1未发货2已发货
        /// </summary>
        public int distribution_status
        {
            set { _distribution_status = value; }
            get { return _distribution_status; }
        }
        /// <summary>
        /// 快递名称
        /// </summary>
        public string delivery_name
        {
            set { _delivery_name = value; }
            get { return _delivery_name; }
        }
        /// <summary>
        /// 快递单号
        /// </summary>
        public string delivery_no
        {
            set { _delivery_no = value; }
            get { return _delivery_no; }
        }
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string accept_name
        {
            set { _accept_name = value; }
            get { return _accept_name; }
        }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string post_code
        {
            set { _post_code = value; }
            get { return _post_code; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string telphone
        {
            set { _telphone = value; }
            get { return _telphone; }
        }
        /// <summary>
        /// 手机
        /// </summary>
        public string mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
        }
        /// <summary>
        /// 收货地址
        /// </summary>
        public string address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 订单留言
        /// </summary>
        public string message
        {
            set { _message = value; }
            get { return _message; }
        }
        /// <summary>
        /// 应付商品总金额
        /// </summary>
        public decimal payable_amount
        {
            set { _payable_amount = value; }
            get { return _payable_amount; }
        }
        /// <summary>
        /// 实付商品总金额
        /// </summary>
        public decimal real_amount
        {
            set { _real_amount = value; }
            get { return _real_amount; }
        }
        /// <summary>
        /// 应付运费
        /// </summary>
        public decimal payable_freight
        {
            set { _payable_freight = value; }
            get { return _payable_freight; }
        }
        /// <summary>
        /// 实付运费
        /// </summary>
        public decimal real_freight
        {
            set { _real_freight = value; }
            get { return _real_freight; }
        }
        /// <summary>
        /// 支付手续费
        /// </summary>
        public decimal payment_fee
        {
            set { _payment_fee = value; }
            get { return _payment_fee; }
        }
        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal order_amount
        {
            set { _order_amount = value; }
            get { return _order_amount; }
        }
        /// <summary>
        /// 获得的积分
        /// </summary>
        public int point
        {
            set { _point = value; }
            get { return _point; }
        }
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime add_time
        {
            set { _add_time = value; }
            get { return _add_time; }
        }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? payment_time
        {
            set { _payment_time = value; }
            get { return _payment_time; }
        }
        /// <summary>
        /// 确认时间
        /// </summary>
        public DateTime? confirm_time
        {
            set { _confirm_time = value; }
            get { return _confirm_time; }
        }
        /// <summary>
        /// 发货时间
        /// </summary>
        public DateTime? distribution_time
        {
            set { _distribution_time = value; }
            get { return _distribution_time; }
        }
        /// <summary>
        /// 订单完成时间
        /// </summary>
        public DateTime? complete_time
        {
            set { _complete_time = value; }
            get { return _complete_time; }
        }
        #endregion Model

        private List<order_goods> _order_goods;
        /// <summary>
        /// 商品列表
        /// </summary>
        public List<order_goods> order_goods
        {
            set { _order_goods = value; }
            get { return _order_goods; }
        }
    }
}