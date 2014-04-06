using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Hoto.DBUtility;
using Hoto.Common;

namespace Hoto.DAL
{
    /// <summary>
    /// 数据访问类:查询
    /// </summary>
    public partial class search
    {
        public search()
        { }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" id,channel_id,title,content,add_time ");
            strSql.Append(" FROM dt_article ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by add_time desc");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM dt_article");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(HotoPagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(HotoPagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
    }
}
