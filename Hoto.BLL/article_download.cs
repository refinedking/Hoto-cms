using System;
using System.Data;
using System.Collections.Generic;

namespace Hoto.BLL
{
    /// <summary>
    /// 下载模型
    /// </summary>
    public partial class article
    {
        #region  Method
        /// <summary>
        /// 修改下载副表一列数据
        /// </summary>
        public void UpdateDownloadField(int id, string strValue)
        {
            dal.UpdateDownloadField(id, strValue);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.article_download model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.article_download model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.article_download GetDownloadModel(int id)
        {
            return dal.GetDownloadModel(id);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetDownloadList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetDownloadList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetDownloadList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetDownloadList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        #endregion  Method

    }
}