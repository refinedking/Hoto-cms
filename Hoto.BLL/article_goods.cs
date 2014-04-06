using System;
using System.Data;
using System.Collections.Generic;

namespace Hoto.BLL
{
    /// <summary>
    /// 商品模型
    /// </summary>
    public partial class article
    {
        #region  Method
        /// <summary>
        /// 修改商品副表一列数据
        /// </summary>
        public void UpdateGoodsField(int id, string strValue)
        {
            dal.UpdateGoodsField(id, strValue);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.article_goods model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.article_goods model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.article_goods GetGoodsModel(int id)
        {
            return dal.GetGoodsModel(id);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetGoodsList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetGoodsList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetGoodsList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetGoodsList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
        #endregion  Method
    }
}