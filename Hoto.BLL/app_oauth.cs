using System;
using System.Data;
using System.Collections.Generic;

namespace Hoto.BLL
{
    /// <summary>
    /// 开放平台
    /// </summary>
    public partial class app_oauth
    {
        private readonly DAL.app_oauth dal = new DAL.app_oauth();
        public app_oauth()
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
        /// 增加一条数据
        /// </summary>
        public int Add(Model.app_oauth model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            dal.UpdateField(id, strValue);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.app_oauth model)
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
        public Model.app_oauth GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 根据接口目录返回一个实体
        /// </summary>
        public Model.app_oauth GetModel(string api_path)
        {
            return dal.GetModel(api_path);
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