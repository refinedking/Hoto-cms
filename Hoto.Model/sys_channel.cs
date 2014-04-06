using System;
namespace Hoto.Model
{
    /// <summary>
    /// 系统频道
    /// </summary>
    [Serializable]
    public partial class sys_channel
    {
        public sys_channel()
        { }
        #region Model
        private int _id;
        private int _model_id = 0;
        private string _name;
        private string _title;
        private int _sort_id = 0;
        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 所属模型ID
        /// </summary>
        public int model_id
        {
            set { _model_id = value; }
            get { return _model_id; }
        }
        /// <summary>
        /// 频道标识
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 频道标题
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 排序数字
        /// </summary>
        public int sort_id
        {
            set { _sort_id = value; }
            get { return _sort_id; }
        }
        #endregion Model
    }
}