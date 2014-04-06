using System;
namespace Hoto.Model
{
    /// <summary>
    /// 配送方式
    /// </summary>
    [Serializable]
    public partial class distribution
    {
        public distribution()
        { }
        #region Model
        private int _id;
        private string _title;
        private string _remark;
        private int _type = 0;
        private decimal _amount = 0M;
        private int _sort_id = 99;
        private int _is_lock = 0;
        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 付款方式0先款后货1先货后款
        /// </summary>
        public int type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 配送费用
        /// </summary>
        public decimal amount
        {
            set { _amount = value; }
            get { return _amount; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int sort_id
        {
            set { _sort_id = value; }
            get { return _sort_id; }
        }
        /// <summary>
        /// 是否不显示
        /// </summary>
        public int is_lock
        {
            set { _is_lock = value; }
            get { return _is_lock; }
        }
        #endregion Model

    }
}