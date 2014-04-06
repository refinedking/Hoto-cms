using System;
namespace Hoto.Model
{
    /// <summary>
    /// 信息顶和踩
    /// </summary>
    [Serializable]
    public partial class article_diggs
    {
        public article_diggs()
        { }
        #region Model
        private int _id;
        private int _digg_good = 0;
        private int _digg_bad = 0;
        /// <summary>
        /// 主表ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 赞一下
        /// </summary>
        public int digg_good
        {
            set { _digg_good = value; }
            get { return _digg_good; }
        }
        /// <summary>
        /// 踩一下
        /// </summary>
        public int digg_bad
        {
            set { _digg_bad = value; }
            get { return _digg_bad; }
        }
        #endregion Model

    }
}