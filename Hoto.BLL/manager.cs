using System;
using System.Data;
using System.Collections.Generic;

namespace Hoto.BLL
{
    /// <summary>
    /// 管理员
    /// </summary>
    public partial class manager
    {
        private readonly DAL.manager dal = new DAL.manager();
        public manager()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

         /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        public bool Exists(string user_name)
        {
            return dal.Exists(user_name);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.manager model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.manager model)
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
        public Hoto.Model.manager GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 根据用户名密码返回一个实体
        /// </summary>
        public Model.manager GetModel(string user_name, string user_pwd)
        {
            return dal.GetModel(user_name, user_pwd);
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
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
        
        #endregion  Method
    }
}