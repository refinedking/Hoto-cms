using System;
using System.Data;
using System.Collections.Generic;

namespace Hoto.BLL
{
    /// <summary>
    /// 用户生成码
    /// </summary>
    public partial class user_code
    {
        private readonly DAL.user_code dal = new DAL.user_code();
        public user_code()
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
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string type, string user_name)
        {
            return dal.Exists(type, user_name);
        }

        /// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            return dal.GetCount(strWhere);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.user_code model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.user_code model)
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
        /// 根据条件批量删除
        /// </summary>
        public bool Delete(string strWhere)
        {
            return dal.Delete(strWhere);
        }

        /// <summary>
        /// 根据生成码得到一个对象实体
        /// </summary>
        public Model.user_code GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 根据生成码得到一个对象实体
        /// </summary>
        public Model.user_code GetModel(string str_code)
        {
            return dal.GetModel(str_code);
        }

        /// <summary>
        /// 根据用户名得到一个对象实体
        /// </summary>
        public Model.user_code GetModel(string user_name, string code_type)
        {
            return dal.GetModel(user_name, code_type);
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