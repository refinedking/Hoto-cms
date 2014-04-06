using System;
using System.Data;
using System.Collections.Generic;
using Hoto.Common;

namespace Hoto.BLL
{
    /// <summary>
    /// 内容模型
    /// </summary>
    public partial class article
    {
        #region  Method
        /// <summary>
        /// 得到最前的内容页
        /// </summary>
        public string GetCallIndex()
        {
            return dal.GetCallIndex();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ContentExists(string call_index)
        {
            return dal.ContentExists(call_index);
        }

        /// <summary>
        /// 修改内容副表一列数据
        /// </summary>
        public void UpdateContentField(int id, string strValue)
        {
            dal.UpdateContentField(id, strValue);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.article_content model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.article_content model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.article_content GetContentModel(int id)
        {
            return dal.GetContentModel(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.article_content GetContentModel(string call_index)
        {
            return dal.GetContentModel(call_index);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetContentList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetContentList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetContentList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetContentList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        #endregion  Method
    }
}