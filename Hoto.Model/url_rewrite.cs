using System;

namespace Hoto.Model
{
    /// <summary>
    /// URL重写实体类
    /// </summary>
    [Serializable]
    public partial class url_rewrite
    {
        //无参构造函数
        public url_rewrite() { }

        #region 成员变量
        private string _name = "";
        private string _path = "";
        private string _pattern = "";
        private string _page = "";
        private string _querystring = "";
        private string _templet = "";
        private string _channel = "";
        private string _type = "";
        private string _inherit = "";

        /// <summary>
        /// 名称标识
        /// </summary>
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// URL重写表达式
        /// </summary>
        public string path
        {
            get { return _path; }
            set { _path = value; }
        }

        /// <summary>
        /// 正则表达式
        /// </summary>
        public string pattern
        {
            get { return _pattern; }
            set { _pattern = value; }
        }

        /// <summary>
        /// 源页面地址
        /// </summary>
        public string page
        {
            get { return _page; }
            set { _page = value; }
        }

        /// <summary>
        /// 传输参数
        /// </summary>
        public string querystring
        {
            get { return _querystring; }
            set { _querystring = value; }
        }

        /// <summary>
        /// 模板文件名称
        /// </summary>
        public string templet
        {
            get { return _templet; }
            set { _templet = value; }
        }

        /// <summary>
        /// 所属频道ID
        /// </summary>
        public string channel
        {
            get { return _channel; }
            set { _channel = value; }
        }

        /// <summary>
        /// 频道类型
        /// </summary>
        public string type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// 页面继承的类名
        /// </summary>
        public string inherit
        {
            get { return _inherit; }
            set { _inherit = value; }
        }

        #endregion
    }
}
