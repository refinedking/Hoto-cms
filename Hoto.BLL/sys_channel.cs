using System;
using System.Data;
using System.Collections.Generic;
using Hoto.Model;
namespace Hoto.BLL
{
	/// <summary>
	/// 系统频道
	/// </summary>
	public partial class sys_channel
	{
        private readonly Hoto.DAL.sys_channel dal = new Hoto.DAL.sys_channel();
		public sys_channel()
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
        /// 检查名称是否存在
        /// </summary>
        public bool Exists(string name)
        {
            return dal.Exists(name);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Hoto.Model.sys_channel model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Hoto.Model.sys_channel model)
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
		public Hoto.Model.sys_channel GetModel(int id)
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
}

