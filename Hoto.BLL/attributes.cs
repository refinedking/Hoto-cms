using System;
using System.Data;
using System.Collections.Generic;

namespace Hoto.BLL
{
	/// <summary>
	/// attributes
	/// </summary>
	public partial class attributes
	{
		private readonly DAL.attributes dal=new DAL.attributes();
		public attributes()
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
		public int  Add(Model.attributes model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Model.attributes model)
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
		public Hoto.Model.attributes GetModel(int id)
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
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		
		#endregion  Method
	}
}

