using System;
namespace Hoto.Model
{
    /// <summary>
    /// 信息属性值
    /// </summary>
    [Serializable]
    public partial class attribute_value
    {
        public attribute_value()
        { }
        #region Model
        private int _id;
        private int _article_id;
        private int _attribute_id;
        private string _title;
        private string _content;
        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 主表ID
        /// </summary>
        public int article_id
        {
            set { _article_id = value; }
            get { return _article_id; }
        }
        /// <summary>
        /// 属性ID
        /// </summary>
        public int attribute_id
        {
            set { _attribute_id = value; }
            get { return _attribute_id; }
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
        /// 内容
        /// </summary>
        public string content
        {
            set { _content = value; }
            get { return _content; }
        }
        #endregion Model

    }
}