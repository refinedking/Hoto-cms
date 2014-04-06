using System;
using System.Data;
using System.Collections.Generic;

namespace Hoto.BLL
{
    /// <summary>
    /// 支付方式
    /// </summary>
    public partial class payment
    {
        private readonly DAL.payment dal = new DAL.payment();
        public payment()
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
        /// 返回标题名称
        /// </summary>
        public string GetTitle(int id)
        {
            return dal.GetTitle(id);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.payment model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.payment model)
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
        public Model.payment GetModel(int id)
        {
            return dal.GetModel(id);
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