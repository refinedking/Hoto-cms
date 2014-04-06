using System;
namespace Hoto.Model
{
    /// <summary>
    /// 信息属性
    /// </summary>
    [Serializable]
    public partial class attributes
    {
        public attributes()
        { }
        #region Model
        private int _id;
        private int _channel_id;
        private string _title;
        private string _remark;
        private int _type;
        private string _default_value;
        private int _sort_id = 99;
        private DateTime _add_time = DateTime.Now;
        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 所属频道ID
        /// </summary>
        public int channel_id
        {
            set { _channel_id = value; }
            get { return _channel_id; }
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
        /// 备注
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 显示类型0输入框1下拉框2单选框3复选择
        /// </summary>
        public int type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 默认值
        /// </summary>
        public string default_value
        {
            set { _default_value = value; }
            get { return _default_value; }
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
        /// 添加时间
        /// </summary>
        public DateTime add_time
        {
            set { _add_time = value; }
            get { return _add_time; }
        }
        #endregion Model

    }
}