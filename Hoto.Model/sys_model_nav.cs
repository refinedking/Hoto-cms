using System;
namespace Hoto.Model
{
    /// <summary>
    /// 系统模型菜单
    /// </summary>
    [Serializable]
    public partial class sys_model_nav
    {
        public sys_model_nav()
        { }
        #region Model
        private int _id;
        private int _model_id = 0;
        private string _title;
        private string _nav_url = "";
        private int _sort_id = 99;
        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 模型ID
        /// </summary>
        public int model_id
        {
            set { _model_id = value; }
            get { return _model_id; }
        }
        /// <summary>
        /// 导航标题
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string nav_url
        {
            set { _nav_url = value; }
            get { return _nav_url; }
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