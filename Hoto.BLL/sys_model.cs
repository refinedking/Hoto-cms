using System;
using System.Data;
using System.Collections.Generic;
using Hoto.Model;

namespace Hoto.BLL
{
	/// <summary>
	/// 系统模型
	/// </summary>
	public partial class sys_model
	{
        private readonly Hoto.DAL.sys_model dal = new Hoto.DAL.sys_model();
		public sys_model()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			return dal.Exists(id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Hoto.Model.sys_model model)
		{
            return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Hoto.Model.sys_model model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			return dal.Delete(id);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Hoto.Model.sys_model GetModel(int id)
		{
			return dal.GetModel(id);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

		#endregion  Method
	}

    /// <summary>
    /// 系统模型菜单
    /// </summary>
    public partial class sys_model_nav
    {
        private readonly Hoto.DAL.sys_model_nav dal = new DAL.sys_model_nav();
        public sys_model_nav()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        #endregion  Method
    }
}

