using System;
using System.Data;
using System.Collections.Generic;

namespace Hoto.BLL
{
	/// <summary>
    /// 信息顶和踩
	/// </summary>
	public partial class article_diggs
	{
        private readonly DAL.article_diggs dal = new DAL.article_diggs();
		public article_diggs()
		{}
		#region  Method
        /// <summary>
        /// 检查修息是否存在
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        /// <param name="id">主表ID</param>
        /// <param name="strValue">修改字段=值</param>
        public void UpdateField(int id, string strValue)
        {
            dal.UpdateField(id, strValue);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.article_diggs GetModel(int id)
        {
            return dal.GetModel(id);
        }

		#endregion  Method
	}
}

